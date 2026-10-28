# Feature: newBill (Новая накладная, швейный цех)

- Легаси: `newBill.vb` (11–331); Designer — только имена/свойства контролов (180–283, 678–756)
- Цель: `Views/NewBillView.xaml`, `ViewModels/NewBillViewModel.cs`, `Data/NewBillRepository`, `Domain/BillRules`
- Статус: в работе (собран, экран открывается; сценарий сохранения не проверен)
- Зависимости: `IDbConnectionFactory`, `IDialogService`, `INavigationService`; таблицы `Employee`, `Articul`, `Operation`, `Routing`, `IdleOperation`, `OperationLog`

## 1. Назначение и сценарии
Открыть из меню «Новая (Швейный цех)». Выбрать сотрудника, артикул, дату, номер квитанции; операция фильтруется по техкарте (`Routing`) артикула.
Режимы: «нормированная» (по количеству) и «Не нормированная» (по времени + тип простоя + расчёт).
«Вставить» добавляет строку в грид (не в БД), «Сохранить» пишет все строки в `OperationLog` и очищает грид, «Удалить» убирает текущую строку, «Закрыть» закрывает форму.
Успех: строки в `OperationLog`, сообщение «Новые записи сохранены в Базе данных».

## 2. UI (из Designer)
| Контрол | Тип | Назначение | Привязка/команда |
|---|---|---|---|
| cbEmployee | ComboBox | сотрудник | `EmployeeName` / `EmployeeID` |
| cbArticul | ComboBox | артикул | `ArticulName` / `ArticulID` |
| cbOperation | ComboBox | операция | `OperationName` / `OperationID`, фильтр по Routing |
| cbTypeOfNonRateSituation | ComboBox | тип простоя | `idle_operation_name` / `idle_operation_id` |
| dtPickerOpDate | DateTimePicker | дата выполнения | по умолчанию сегодня |
| tbLogNum | TextBox | номер квитанции | `DocumentNumber` |
| tbWorkday | TextBox | рабочий день | по умолчанию 480, максимум 480 |
| tbCount | TextBox | количество | по умолчанию 0 |
| tbOperationTime | TextBox | время выполнения | по умолчанию 0 |
| cbIsStandard | CheckBox | «Не нормированная» | checked = не нормированная |
| btnInsert | Button | «Вставить» | `InsertCommand` |
| btnDelete | Button | «Удалить» | `DeleteCommand` |
| btnCalculateExecutionTime | Button | «Рассчитать время» | `CalculateCommand` |
| Button1 | Button | «Сохранить» | `SaveCommand` |
| Button3 | Button | «Закрыть» | `CloseCommand` |
| Button2 | Button | «Сохранить и Добавить», hidden | не портировать |
| dgOperationLog | DataGridView | строки накладной | 11 колонок; скрыты 0, 4, 8; read-only кроме `workDay` |
| ContextMenuStrip1 | ContextMenu | «Редактировать»/«Удалить» | обработчиков в легаси нет — no-op |

## 3. События и команды
| Событие легаси | Действие | Команда/метод VM |
|---|---|---|
| `newBill_Load` | загрузка справочников, умолчания 0/480/0, вызов смены артикула | `Load` |
| `cbIsStandard.CheckedChanged` | переключение режима (см. §5) | `IsNonStandard` setter |
| `cbOperation.SelectedIndexChanged` | время = `OperationTimewithKoef` | `SelectedOperation` setter |
| `cbArticul.SelectedIndexChanged` | операции по техкарте; нет техкарты → disable | `SelectedArticul` setter |
| `btnInsert.Click` | валидация workday, парсинг времени, добавление строки | `InsertCommand` |
| `btnDelete.Click` | удалить текущую строку (кроме placeholder) | `DeleteCommand` |
| `Button1.Click` | вставить все строки, сообщение, очистка | `SaveCommand` |
| `Button3.Click` | закрыть форму | `CloseCommand` |
| `tbWorkday.TextChanged` | >480 → сообщение и сброс в 480 | `Workday` setter |
| `KeyPress` (workday/count/time) | только цифры и control | numeric behavior |
| `dtPickerOpDate.ValueChanged`, `cbEmployee.SelectedValueChanged` | очистить workday | setters |
| `btnCalculateExecutionTime.Click` | время × процент простоя | `CalculateCommand` |

## 4. Данные
- Справочники: `SELECT EmployeeID, EmployeeName FROM Employee`; `SELECT ArticulID, ArticulName FROM Articul`;
  `SELECT idle_operation_id, idle_operation_name, cost_percentage FROM IdleOperation`.
- Операции по техкарте: `SELECT OperationID, OperationName, OperationTimewithKoef FROM Operation WHERE OperationID IN (SELECT OperationID FROM Routing WHERE ArticulID = ?) ORDER BY OperationID`.
- Сохранение: `INSERT INTO OperationLog (EmployeeID, OperationID, ArticulID, DateExecution, TimeExecution, Count, DocumentNumber, workDay) VALUES (?, ?, ?, ?, ?, ?, ?, ?)`.
- Типы: `TimeExecution`/`Count`/`workDay` — Int32; `DocumentNumber` — до 255; `DateExecution` — дата (`Date`).
- Целевое отличие: сохранение пачки в одной транзакции; при ошибке — rollback и сообщение (легаси писал построчно, возможна частичная запись).
- Легаси также заполняет `OperationLog1` при загрузке, но не использует — не портировать.

## 5. Бизнес-правила и формулы
- Workday > 480 → сообщение, значение 480; при вставке пустой workday недопустим.
- checked «Не нормированная»: time/idle/calculate enabled, count disabled и = 0. Unchecked: обратное, time = 0.
- Выбор операции: `tbOperationTime = OperationTimewithKoef` (int).
- Простой: `percentage = cost_percentage / 100`; `result = operationTime * percentage` (не прибавляется к 100%).
- Сохранение: строки грида конвертируются в Int32 как VB `CInt` (банковское округление, например 2.5 → 2); дата — `DateExecution.Date`.
- Номер квитанции и ФИО/названия берутся из выбранных элементов и пишутся в грид.

## 6. Валидация и edge cases
- Пустой workday: «Поле продолжительность рабочего времени не заполнено!» / «Рабочий день», фокус в поле.
- Workday > 480: «Продолжительность рабочего дня не может превышать 480» / «Рабочий день».
- Ошибка времени при вставке: «Неверный формат числа в поле 'Время выполнения'!» / «Ошибка».
- Ошибка расчёта: «Неверный формат даты в поле 'Время выполнения' или не удалось получить процент простоя из БД!» / «Ошибка».
- Успешное сохранение: «Новые записи сохранены в Базе данных» / «Сохранение».
- Исключения БД в легаси показываются `ex.Message`; целевой код — через `IDialogService`.
- Нет техкарты → операции пусты и combo disabled; выбранная операция сбрасывается.
- Пустые combo / пустой грид / отрицательные значения легаси не проверяет; в целевой версии — дружелюбные сообщения.
- Целевые guard-сообщения при сохранении: «Неверный формат числа в поле 'Количество'!» и «Неверный формат числа в поле 'Рабочий день'!» (в легаси — необработанное исключение).
- `OperationTimewithKoef = DBNull` → 0 в целевой версии (типизированный DataSet легаси падает).
- Простой не выбран/ошибка выборки → percentage 0 → результат 0 (сохранить поведение).
- «Сохранить» без строк всё равно показывает сообщение об успехе (сохранить поведение).

## 7. Навигация и поведение окна
Меню «Новая (Швейный цех)» → `NewBillViewModel` в `ContentControl` оболочки (ADR-007), не MDI.
«Закрыть» → возврат на приветственный экран. Каждое открытие — новое состояние; грид не персистится.
Отличие UI: ComboBox легаси были editable (DropDownStyle по умолчанию); в WPF — выбор из списка.

## 8. Паритет-чек-лист
- [ ] Загрузка справочников и умолчания
- [ ] Фильтр операций по техкарте и disabled при отсутствии
- [ ] Переключение режимов и блокировки полей
- [ ] Расчёт времени по проценту простоя
- [ ] Вставка/удаление строк, сохранение и очистка
- [ ] Сообщения и границы (480, пустые значения)
- [x] Сборка на Windows

## 9. Открытые вопросы
- Подтвердить на Windows точное округление VB `CInt` для дробного времени (2.5 → 2).
- Транзакция при сохранении — сознательное отличие от легаси; зафиксировать ADR при реализации.
