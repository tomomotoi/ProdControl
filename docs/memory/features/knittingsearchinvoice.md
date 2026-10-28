# Feature: KnittingSearchInvoiceForm (Поиск/редактирование накладных, вязальный цех)

- Легаси: `KnittingSearchInvoiceForm.vb` (1–557); Designer — только имена/свойства контролов (304–487)
- Цель: `Views/KnittingSearchInvoiceView.xaml`, `ViewModels/KnittingSearchInvoiceViewModel.cs`,
  `Data/KnittingSearchRepository`, `Models/KnittingSearchModels`
- Статус: готово (собран на Windows; репозиторий/VM на копии БД, UIA smoke)
- Зависимости: `IDbConnectionFactory`, `IDialogService`; `KnittingOperationLog`, `Employee`, `Articul`,
  `ClothingParts`, `KnittingMachines`

## 1. Назначение и сценарии
Поиск строк `KnittingOperationLog` по сотруднику, артикулу, комбинации или номеру квитанции, опционально за период.
«Найти все» — без фильтров. В edit-mode: правка даты/времени/количества/рабочего дня, удаление (сразу в БД),
сохранение изменённых строк. Сообщения: «Запись успешно удалена!», «Успешно обновлено записей: N».

## 2. UI (из Designer)
| Контрол | Тип | Назначение | Привязка/команда |
|---|---|---|---|
| cbFilter | ComboBox | 4 режима фильтра (см. §4) | `SelectedFilterIndex` |
| cbEmployee / cbArticul | ComboBox | сотрудник / артикул | `SelectedEmployee` / `SelectedArticul` |
| txDocNum | TextBox | номер квитанции | `DocumentNumber` |
| chckbxByPeriod + datePickerFrom/To | CheckBox + DateTimePicker | период, по умолчанию сегодня | `IsByPeriod`, `DateFrom`, `DateTo` |
| btnSearch / btnSearchAll | Button | «Найти» / «Найти все» | `SearchCommand` / `SearchAllCommand` |
| dgView | DataGridView | результаты, 11 видимых колонок | `Rows`, `SelectedRow` |
| chckbxEditingMode + btnDelete/btnSave | CheckBox + Button | edit-mode, удаление, сохранение | `IsEditingMode`, `DeleteCommand`, `SaveCommand` |
| колонки dgView | text | LogId, сотрудник, деталь, артикул, вяз. машина, маш. в зоне, номер квитанции — read-only; дата (`dd.MM.yyyy`), время, количество (`amount`), рабочий день — правятся; isChanged скрыта в легаси и не портируется (dirty в VM) | ячейки |

## 3. События и команды
| Событие легаси | Действие | Команда/метод VM |
|---|---|---|
| `Form_Load` | справочники, `cbFilter=0`, период off, read-only, кнопки off | `Load` |
| `cbFilter.SelectedIndexChanged` | показать нужные поля фильтра | `SelectedFilterIndex` |
| `chckbxByPeriod.CheckedChanged` | показать/скрыть даты | `IsByPeriod` |
| `chckbxEditingMode.CheckedChanged` | grid read-only, доступность кнопок | `IsEditingMode` |
| `btnSearch/btnSearchAll.Click` | SQL-поиск, сообщение при пустом результате | `SearchCommand`/`SearchAllCommand` |
| `dgView.CellValueChanged` | пометить строку изменённой | row.PropertyChanged |
| `dgView.CellClick` (DateExecution) | overlay DateTimePicker | DatePicker в ячейке |
| `btnDelete.Click` | подтверждение, DELETE по id, сообщение, убрать строку | `DeleteCommand` |
| `btnSave.Click` | UPDATE только изменённых строк в транзакции | `SaveCommand` |

## 4. Данные
- Справочники (без `ORDER BY`): `SELECT EmployeeID, EmployeeName FROM Employee`;
  `SELECT ArticulID, ArticulName FROM Articul`; `SELECT id, part_name FROM ClothingParts`;
  `SELECT id, machine_make + ' ' + machine_model AS machine_displayname FROM KnittingMachines`.
- Строки: `SELECT id, document_number, article_id, employee_id, part_id, amount, machine_id,
  date_execution, time_execution, machines_in_zone, work_day FROM KnittingOperationLog
  ORDER BY date_execution DESC, id DESC`.
- Фильтры (параметры): `employee_id = ?`; `article_id = ?`; оба; `document_number LIKE ?` (`%значение%`);
  период `date_execution >= ? AND date_execution <= ?`.
- `DELETE FROM KnittingOperationLog WHERE id = ?` (сразу, как в легаси).
- `UPDATE [KnittingOperationLog] SET [document_number]=?, [article_id]=?, [employee_id]=?, [part_id]=?,
  [amount]=?, [machine_id]=?, [date_execution]=?, [time_execution]=?, [machines_in_zone]=?, [work_day]=?
  WHERE [id]=?`; несколько строк — одной транзакцией.
- Типы: Int32 (ID nullable), Date, Double, текст; имена — из справочников по ID (не JOIN).

## 5. Бизнес-правила и формулы
- `cbFilter` по умолчанию 0; combo сотрудника/артикула выбирают первый элемент после загрузки.
- Если выбранного значения нет (пустой справочник), фильтр по нему не добавляется — поиск без условия.
- «Сотрудник + артикул» фильтрует только если заданы оба; иначе условие не добавляется.
- Период: `DateFrom.Date` и `DateTo.Date.AddDays(1).AddSeconds(-1)` (включительно, как в легаси).
- `DBNull` при чтении: числа → 0, дата → пусто, ID → пусто, номер → `""`.
- `DateTo` раньше `DateFrom` — пустой результат, без отдельного сообщения.
- Сохранение: `document_number` trim, пустой → `NULL`; ID пустые → `NULL`;
  amount/machines_in_zone/work_day → `int` с fallback 0; time — `double` через invariant-парсинг после
  замены `,`→`.` (как `GetDbValueDouble`); неверная/пустая дата → `NULL`.
- В легаси все колонки `ReadOnly=True`; фактически правится только дата (overlay). Целевая версия в
  edit-mode разрешает дату/время/количество/рабочий день и сразу применяет read-only вне режима (ADR-018).
- Числовой ввод: Count/workDay — только цифры; TimeExecution — цифры и один разделитель `.`/`,`.
- После сохранения строки остаются в гриде; флаг изменённости сбрасывается.

## 6. Валидация и edge cases
- Пустой результат: «Записи по заданным критериям не найдены.» / «Информация».
- Ошибка поиска: «Ошибка при поиске данных: » + `ex.Message` / «Ошибка».
- Удаление без строки/вне edit-mode — no-op; отказ в подтверждении — no-op.
- Ошибка удаления: «Ошибка при удалении из БД: » + `ex.Message` / «Ошибка», строка остаётся.
- Сохранение без изменённых строк: «Нет измененных данных для сохранения.» / «Информация».
- Ошибка сохранения: «Ошибка при сохранении изменений: » + `ex.Message` / «Ошибка»; транзакция откатывается.
- Ошибка загрузки: «Ошибка подключения к БД: » на первом справочнике (загрузка прекращается, как при сбое
  `conn.Open`), «Ошибка в справочнике X: » на остальных (остальные загружаются).

## 7. Навигация и поведение окна
Меню «Найти (Вязальный цех)» → `KnittingSearchInvoiceViewModel` в `ContentControl` (ADR-007), не MDI.
Заголовок легаси — «Найти накладную (вяз. цех.)». Кнопки «Закрыть» нет; возврат через меню.
Каждое открытие — новое состояние; правки/удаления не переносятся между открытиями.
Целевая шапка — адаптивный `WrapPanel` вместо фиксированных координат WinForms.

## 8. Паритет-чек-лист
- [x] Загрузка справочников, первый элемент в combo, начальный read-only
- [x] Видимость полей фильтра и периода
- [x] Поиск по 4 режимам + период + «Найти все», сортировка
- [x] Режим редактирования и числовой ввод (фильтр в UI; ручной набор не проверялся)
- [x] Удаление с подтверждением, сохранение изменённых строк в транзакции
- [x] Сообщения и пустые состояния
- [x] Сборка на Windows

Проверено 2026-10-22: MSBuild 0/0; репозиторий на копии БД — справочники, 4 фильтра, период, сортировка, пустой результат, UPDATE/DELETE; VM-харнесс — edit-mode, dirty, удаление/отмена, сообщения; UIA smoke — 11 колонок, 2 combo, 4 кнопки, edit-mode disabled.

## 9. Открытые вопросы
- Подтвердить на Windows edit-mode (в легаси правится только дата) и начальное состояние кнопок — ADR-018.
