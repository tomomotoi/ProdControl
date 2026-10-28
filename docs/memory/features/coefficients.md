# Feature: CoefficientsForm (Коэффициенты доп. затрат)

- Легаси: `CoefficientsForm.vb` (1–69); Designer (24–166); вызов `mainForm.vb:301–304`
- Цель: `Views/CoefficientsView.xaml`, `ViewModels/CoefficientsViewModel.cs`,
  `Data/ICoefficientsRepository`, `Data/CoefficientsRepository`, `Models/CoefficientsModels.cs`
- Статус: готово (собран; VM-харнесс 48/48, UIA-сценарий на копии БД)
- Зависимости: `IDbConnectionFactory`, `IDialogService`; таблица `Coefficients`

## 1. Назначение и сценарии
Справочник коэффициентов дополнительных затрат: просмотр списка и добавление/удаление
в режиме редактирования. Открывается из меню «Основные данные → Коэффициенты доп. затрат».
Успех — одна строка `INSERT`/`DELETE` и перечитанный список. Подтверждений и сообщений
об успехе в легаси нет.

## 2. UI (из Designer)
| Контрол | Тип | Назначение | Привязка/команда |
|---|---|---|---|
| `labelNewCoefficient` | Label | «Новый коэффициент:» | — |
| `tbNewCoefficientValue` | TextBox | значение нового коэффициента | `NewCoefficientValue` |
| `btnAdd` | Button | «Добавить» (изначально disabled) | `AddCoefficientCommand` |
| `labelCoefficients` | Label | «Коэффициенты:» | — |
| `lbCoefficients` | ListBox | список (`coefficient_value`, `coefficient_id`) | `Coefficients` / `SelectedCoefficient` |
| `btnDelete` | Button | «Удалить» (изначально disabled) | `DeleteCoefficientCommand` |
| `ckbxEditMode` | CheckBox | «Режим редактирования» | `IsEditMode` |

## 3. События и команды
| Событие легаси | Действие | Команда/метод VM |
|---|---|---|
| `Load` | `FillBy` — список по возрастанию значения | `Load()` в конструкторе |
| `ckbxEditMode.CheckedChanged` | включить/выключить «Добавить»/«Удалить» | сеттер `IsEditMode` |
| `btnAdd.Click` | проверка числа и дубликата, INSERT, refresh | `AddCoefficientCommand` |
| `btnDelete.Click` | DELETE выбранной строки, refresh | `DeleteCoefficientCommand` |

## 4. Данные
- Чтение: `SELECT coefficient_id, coefficient_value FROM Coefficients ORDER BY coefficient_value ASC`
  (легаси `FillBy`; `ClearBeforeFill=True`).
- Проверка дубликата: `SELECT coefficient_value FROM Coefficients WHERE coefficient_value = ?`
  (`@newValue`, `DbType.Double`; проверяется наличие строк).
- INSERT: `INSERT INTO Coefficients (coefficient_value) VALUES (?)` (`OleDbType.Double`);
  `date_added` приложением не задаётся (nullable; в текущей БД у новых строк
  подставляется дата по умолчанию, форма её не читает).
- DELETE: `DELETE FROM Coefficients WHERE coefficient_id = ? AND coefficient_value = ?`
  (легаси TableAdapter: `@id` Integer + `@value` Double, `IsNull_coefficient_value = 0`).
- Одиночные параметризованные команды, без транзакций.

## 5. Бизнес-правила и формулы
- Число разбирается в текущей культуре ru-RU: аналог `IsNumeric` + `Double.Parse`
  (`NumberStyles.Float | AllowThousands`); дробный разделитель — `,`, тысячи — пробел,
  экспонента допускается; `1.5` считается неверным форматом.
- Дубликат — точное сравнение `double` через SQL (как легаси).
- Список сортируется SQL по `coefficient_value ASC`; отображение — стандартное
  `double.ToString()` текущей культуры, без округления и формата.
- После `FillBy`/refresh выбирается первый элемент; введённый текст не очищается.
- Округлений и вычислений нет.

## 6. Валидация и edge cases
- Пустое/нечисловое значение: «Число не соответствует числовому формату или не было задано!» /
  «Ошибка».
- Дубликат: «Данный коэффициент уже присутствует в базе данных!» / «Уведомление» (Info).
- Удаление без выбора: целевое сообщение «Выберите коэффициент для удаления.» / «Ошибка»
  (в легаси `Double.Parse("")` → необработанный `FormatException`).
- Ошибка БД при add/delete: `ex.Message` / «Ошибка» (в легаси add/delete не перехватывались).
- Ошибка проверки дубликата: показывается «Ошибка», INSERT не выполняется
  (в легаси после ошибки проверка возвращала False и INSERT всё равно шёл).
- Ошибка загрузки: «Ошибка при загрузке данных: » + `ex.Message` / «Ошибка».

## 7. Навигация и поведение окна
Меню «Основные данные → Коэффициенты доп. затрат» → `CoefficientsViewModel` в `ContentControl`
оболочки (ADR-007). Отдельной кнопки «Закрыть» в легаси нет; каждое открытие — новое состояние.

## 8. Паритет-чек-лист
- [x] Маршрут меню открывает форму
- [x] Список загружается по возрастанию, выбран первый элемент
- [x] Режим редактирования включает/выключает «Добавить»/«Удалить»
- [x] Добавление: число, дубликат, неверный формат, текст не очищается
- [x] Удаление: выбранная строка, удаление без выбора не падает
- [x] Сборка VS18 MSBuild Debug: 0 ошибок, 0 предупреждений
- [x] VM-харнесс на копии БД: 48/48 (сортировка, парсинг `,`/пробелов/экспоненты,
  отказ `1.5`, дубликат, add/delete, 4 ошибки БД через stub, guard режима)
- [x] UIA-сценарий: меню, 18 строк, checkbox, add `1,05`, дубликат, `abc`,
  удаление, read-only; тестовая строка в БД удалена

## 9. Отклонения и открытые вопросы
- Исправлены падения легаси: удаление без выбора и необработанные ошибки БД — ADR-025.
- Для доступности/UIA на `ListBoxItem` задан `AutomationProperties.Name` = значение
  (визуально и по данным ничего не меняет).
- `1.5` (точка) не принимается, как в легаси под ru-RU; уточнить, нужна ли мягкая
  нормализация разделителя, как в других портированных формах.
