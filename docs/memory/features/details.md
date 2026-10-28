# Feature: DetailsForm (Детали изделий)

- Легаси: `DetailsForm.vb` (1–150); Designer (24–152); вызов `mainForm.vb:331–336`
- Цель: `Views/DetailsView.xaml`, `ViewModels/DetailsViewModel.cs`,
  `Data/IDetailsRepository`, `Data/DetailsRepository`, `Models/DetailsModels.cs`
- Статус: готово (собран; VM-харнесс 60/60, UIA-сценарий на копии БД)
- Зависимости: `IDbConnectionFactory`, `IDialogService`; таблица `ClothingParts`

## 1. Назначение и сценарии
Справочник деталей изделий: список деталей, добавление, переименование и удаление.
Открывается из меню «Основные данные → Детали изделий» (заголовок окна «Детали»).
Успех — строка `INSERT`/`UPDATE`/`DELETE` и перечитанный список (сортировка `id DESC`).

## 2. UI (из Designer)
| Контрол | Тип | Назначение | Привязка/команда |
|---|---|---|---|
| `labelDetailsList` | Label | «Список деталей» | — |
| `dgvParts` | DataGridView | колонки `ID` (60 px) и «Наименование детали» (fill) | `Details` / `SelectedDetail` |
| `gbPartDetails` | GroupBox | «Свойства детали» | — |
| `labelDetailName` | Label | «Наименование детали:» | — |
| `tbPartName` | TextBox (Multiline, 255×95) | имя детали | `PartName` |
| `btnAdd` | Button | «Добавить» | `AddCommand` |
| `btnSave` | Button | «Сохранить изменения» | `SaveCommand` |
| `btnDelete` | Button | «Удалить» | `DeleteCommand` |
| `btnClear` | Button | «Очистить поля» | `ClearCommand` |

## 3. События и команды
| Событие легаси | Действие | Команда/метод VM |
|---|---|---|
| `Load` | `SELECT id, part_name ... ORDER BY id DESC` | `Load()` в конструкторе |
| `dgvParts.SelectionChanged` | имя выбранной строки → `tbPartName` | сеттер `SelectedDetail` |
| `btnAdd.Click` | проверка пустоты, INSERT, успех, очистка, reload | `AddCommand` |
| `btnSave.Click` | проверка выбора/пустоты, UPDATE по `id`, успех, reload | `SaveCommand` |
| `btnDelete.Click` | проверка выбора, подтверждение, DELETE, успех, очистка, reload | `DeleteCommand` |
| `btnClear.Click` | очистить текст и выделение | `ClearCommand` |

## 4. Данные
- Схема: `ClothingParts(id AutoNumber/Integer PK, part_name Memo/Text)`; в текущей БД
  `part_name` — `Required=True` (NULL вставить нельзя), но чтение защищено от `DBNull`.
- Чтение: `SELECT [id], [part_name] FROM [ClothingParts] ORDER BY [id] DESC`;
  `DBNull.part_name` → пустая строка.
- INSERT: `INSERT INTO [ClothingParts] ([part_name]) VALUES (?)` — `@partName`, Text.
- UPDATE: `UPDATE [ClothingParts] SET [part_name] = ? WHERE [id] = ?` — `@partName` Text, `@id` Integer.
- DELETE: `DELETE FROM [ClothingParts] WHERE [id] = ?` — `@id` Integer.
- Одиночные параметризованные команды, без транзакций; после add/save/delete — reload.
- `Trim()` выполняется перед записью (имя в БД без крайних пробелов).

## 5. Бизнес-правила и формулы
- Сортировка только SQL: `id DESC` (новые сверху); вычислений и округлений нет.
- В таблицу и поле пишется `part_name` как есть (после `Trim`); `NULL` читается как `""`.
- Выделение строки не выбирается автоматически: WinForms DataGridView после привязки
  `DataSource` не устанавливает `SelectedRows` (проверено пробой), поэтому после загрузки
  поле пустое. При выборе строки текст заменяется её именем; снятие выбора текст не очищает.
- После save список перечитывается, выделение не восстанавливается, введённый текст остаётся.
- После add/delete — `ClearFields()` (очистка текста и выделения), затем reload.

## 6. Валидация и edge cases
- Add, пустое/пробельное имя: «Введите наименование детали» / «Предупреждение» (Warning).
- Save без выбора: «Выберите запись в таблице для редактирования» / «Предупреждение».
- Save, пустое имя: «Наименование детали не может быть пустым» / «Предупреждение».
- Delete без выбора: «Выберите запись в таблице для удаления» / «Предупреждение».
- Delete: подтверждение `Удалить деталь "{part_name}" (ID: {id})?` / «Подтверждение», YesNo/Question;
  при «Нет» ничего не происходит.
- Успехи (Info): «Деталь успешно добавлена», «Изменения успешно сохранены», «Запись удалена» /
  заголовок «Успех».
- Ошибки БД (Error): load — «Ошибка загрузки данных: » + `ex.Message`;
  add — «Ошибка при добавлении: »; save — «Ошибка при сохранении: »; delete — «Ошибка при удалении: ».
- Дубликаты имён разрешены (проверки нет); пустую строку БД принимает, `NULL` — нет.

## 7. Навигация и поведение окна
Меню «Основные данные → Детали изделий» → `DetailsViewModel` в `ContentControl` оболочки (ADR-007).
Отдельной кнопки «Закрыть» нет; каждое открытие — новое состояние.

## 8. Паритет-чек-лист
- [x] Маршрут меню открывает форму
- [x] Список загружается по `id DESC`, колонки ID/Наименование
- [x] Выбор строки заполняет поле, снятие выбора его не очищает
- [x] Add/Save/Delete: все сообщения и подтверждение как в легаси
- [x] Clear: очищает текст и выделение
- [x] Ошибки БД и пустое имя обрабатываются
- [x] Сборка VS18 MSBuild Debug (slnx): 0 ошибок, 0 предупреждений
- [x] VM-харнесс 60/60 (stub + реальный репозиторий, memo 400 символов)
- [x] UIA-сценарий: меню, 12 строк, выбор, add/save/clear, delete cancel/yes

## 9. Открытые вопросы
- Нет.
