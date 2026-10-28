# Feature: OperationForm (Операция)

- Легаси: `OperationForm.vb` (1–167); Designer — только контролы (25–233); вызов из `mainForm.vb:63–67`
- Цель: `Views/OperationView.xaml`, `ViewModels/OperationViewModel.cs`, `Data/IOperationRepository`, `Data/OperationRepository`, `Models/OperationModels.cs`
- Статус: готово (собран, основной сценарий проверен на копии БД)
- Зависимости: `IDbConnectionFactory`, `IDialogService`; таблицы `Operation`, `Position`, `Routing`

## 1. Назначение и сценарии
Справочник операций: просмотр, поиск, добавление и удаление. Открывается из меню
«Основные данные → Операция». Успех добавления — одна запись в `Operation` и сообщение
«Операция была успешно добавлена в базу данных!». Удаление — только если операция не используется
в `Routing`. Редактирования существующих операций в форме нет.

## 2. UI (из Designer)
| Контрол | Тип | Назначение | Привязка/команда |
|---|---|---|---|
| `txNewOperationName` | TextBox | название новой операции | `NewOperationName` |
| `cbProfession` | ComboBox | профессия (Position) | `Positions` / `SelectedPosition` |
| `btnAddOperation` | Button | «Добавить» | `AddOperationCommand` |
| `lbExistingOperations` | ListBox | существующие операции | `Operations` / `SelectedOperation` |
| `txSearchOperation` | TextBox | поиск по названию | `SearchText` |
| `txExistingOperationProfession` | TextBox (ReadOnly) | профессия выбранной операции | `SelectedProfessionName` |
| `btnDeleteOperation` | Button | «Удалить» | `DeleteOperationCommand` |

Подписи: «Название новой операции:», «Профессия:», «Список существующих операций:»,
«Поиск операции:», «Профессия существующей операции:».

## 3. События и команды
| Событие легаси | Действие | Команда/метод VM |
|---|---|---|
| `Load` | `Fill` `Operation`, затем `Position`; первый элемент выбирается биндингом | `Load()` в конструкторе |
| `btnAddOperation.Click` | проверка пустого имени и дубликата, INSERT, refresh, сообщение | `AddOperationCommand` |
| `btnDeleteOperation.Click` | проверка использования в `Routing`, DELETE, refresh | `DeleteOperationCommand` |
| `txSearchOperation.TextChanged` | `ListBox.FindString` — первое совпадение по префиксу; нет совпадения → без выбора | setter `SearchText` |
| `lbExistingOperations.SelectedIndexChanged` | JOIN-запрос имени профессии по `PositionID` | setter `SelectedOperation` |

## 4. Данные
- Чтение списка: `SELECT OperationID, OperationName, PositionID FROM Operation`
  (легаси заполняет все колонки; для формы нужны эти).
- Справочник профессий: `SELECT PositionID, PositionName FROM Position`.
- Проверка дубликата: `SELECT COUNT(*) FROM Operation WHERE OperationName LIKE ?`.
- INSERT: `INSERT INTO Operation (OperationName, OperationKoefAddCost, OperationTimewithKoef,
  OpetationRate, PositionID) VALUES (?, 0, 0, 0, ?)`. `OperationMachin` не задаётся (NULL).
- Проверка использования: `SELECT COUNT(*) FROM Routing WHERE OperationID = ?`.
- DELETE: `DELETE FROM Operation WHERE OperationID = ?`.
- Одиночные параметризованные команды без транзакций.

## 5. Бизнес-правила и формулы
- При добавлении три поля `OperationKoefAddCost`, `OperationTimewithKoef`, `OpetationRate` = 0.
- Дубликат определяется через `LIKE` без `%`: Access сравнивает без учёта регистра; спецсимволы
  `%`/`_` во введённом имени работают как шаблон — как в легаси.
- Поиск в списке — префикс названия, без учёта регистра; пустая строка выбирает первый элемент.
- Имя не trim-ится, регистр сохраняется.
- После добавления/удаления список перечитывается; выделяется первый элемент (как Fill).

## 6. Валидация и edge cases
- Пустое имя: «Поле 'Название новой операции' не должно быть пустым!» / «Ошибка».
- Дубликат: «Такая операция уже существует!» / «Ошибка».
- Используется в `Routing`: «Данная операция используется в текущих производственных процессах!» / «Ошибка».
- Ошибка БД при add/delete: текст `ex.Message` / «Ошибка» (как в легаси).
- Пустой список / нет выбора для удаления: целевое сообщение «Выберите операцию для удаления.»
  (в легаси удаление без выбора приводило к внутренней ошибке).
- Нет профессий в `Position`: целевое сообщение «Выберите профессию.» (в легаси — исключение).
- Ошибка загрузки справочников: «Ошибка при загрузке данных: » + `ex.Message` / «Ошибка».
- Удаление операции, используемой в `OperationLog`, не блокируется (в легаси проверки нет).

## 7. Навигация и поведение окна
Меню «Основные данные → Операция» → `OperationViewModel` в `ContentControl` оболочки (ADR-007).
Отдельной кнопки «Закрыть» в легаси нет; навигация — через меню. Каждое открытие — новое состояние.

## 8. Паритет-чек-лист
- [x] Маршрут меню открывает форму
- [x] Список операций и профессий загружается (151 операция, 11 профессий)
- [x] Добавление через UI: строка в БД, поля = 0, `OperationMachin` = NULL, сообщение об успехе
- [x] Поиск по префиксу выставляет выбранную операцию
- [x] Удаление через UI: тестовая строка удалена, ошибок нет
- [x] Профессия выбранной операции отображается (привязка `SelectedProfessionName`)
- [x] Сборка VS18 MSBuild Debug: 0 ошибок, 0 предупреждений
- [ ] Вручную: пустое имя, дубликат, операция из `Routing`, удаление без выбора

## 9. Открытые вопросы
- Исправление бага легаси в `checkIfOperationIsUsed` (параметр `@OperationName` вместо
  `@OperationID`) — см. ADR-020: целевая версия проверяет использование корректно.
- Добавлять ли подтверждение удаления: в легаси его нет, пока паритет.
