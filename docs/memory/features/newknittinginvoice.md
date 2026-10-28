# Feature: NewKnittingInvoiceForm (Новая накладная, вязальный цех)

- Легаси: `NewKnittingInvoiceForm.vb` (1–175); Designer — только имена/свойства контролов (81–421)
- Цель: `Views/NewKnittingInvoiceView.xaml`, `ViewModels/NewKnittingInvoiceViewModel.cs`, `Data/KnittingInvoiceRepository`, `Models/KnittingInvoiceModels`
- Статус: готово (собран; сценарий проверен харнессом на копии БД, UI smoke через UIA)
- Зависимости: `IDbConnectionFactory`, `IDialogService`; таблицы `Employee`, `Articul`, `ClothingParts`, `KnittingMachines`, `KnittingOperationLog`

## 1. Назначение и сценарии
Открыть из меню «Новая (Вязальный цех)». Заполнить шапку: номер квитанции, дата (по умолчанию сегодня),
рабочий день, артикул, количество, деталь, сотрудник, оборудование, время выполнения, машин в зоне обслуживания.
«Вставить» добавляет строку только в грид (не в БД), «Удалить» убирает выбранную строку, «Сохранить» пишет все
строки в `KnittingOperationLog` и очищает грид. Успех — «Данные успешно сохранены!».
Грид только для чтения: правка ячеек в легаси запрещена (все колонки `ReadOnly=True`).

## 2. UI (из Designer)
| Контрол | Тип | Назначение | Привязка/команда |
|---|---|---|---|
| tbReceiptNumber | TextBox | номер квитанции | `ReceiptNumber` |
| dtPickerOpDate | DateTimePicker | дата выполнения, по умолчанию `DateTime.Now` | `OperationDate` |
| tbWorkday | TextBox | рабочий день (целое) | `Workday` |
| cbArticul | ComboBox | артикул, `ArticulName`/`ArticulID` | `SelectedArticul` |
| tbCount | TextBox | количество (целое) | `Count` |
| cbOperation | ComboBox | деталь из `ClothingParts`, `part_name`/`id` | `SelectedPart` |
| cbEmployee | ComboBox | сотрудник, `EmployeeName`/`EmployeeID` | `SelectedEmployee` |
| cbKnittingMachines | ComboBox | оборудование, `machine_displayname`/`id` | `SelectedMachine` |
| tbExecutionTime | TextBox | время выполнения, до 2 знаков после разделителя | `ExecutionTime` |
| tbMachinesInZone | TextBox | машин в зоне обслуживания (целое) | `MachinesInZone` |
| btnInsert | Button | «Вставить» | `InsertCommand` |
| btnDelete | Button | «Удалить» | `DeleteCommand` |
| btnSave | Button | «Сохранить» | `SaveCommand` |
| dgOperationLog | DataGridView | 10 колонок (см. §4), все read-only, `AllCells` | `Rows`, `SelectedRow` |

## 3. События и команды
| Событие легаси | Действие | Команда/метод VM |
|---|---|---|
| `NewKnittingInvoiceForm_Load` | загрузка 4 справочников; дата = сегодня; `AllCells` | `Load` |
| `KeyPress` (tbWorkday, tbCount, tbMachinesInZone) | только цифры и control | numeric-поведение View |
| `KeyPress` (tbExecutionTime) | цифры, control, один разделитель, ≤2 знаков; `.`/`,` → системный разделитель | numeric-поведение View |
| `btnInsert.Click` | проверка полей, добавление строки в грид | `InsertCommand` |
| `btnDelete.Click` | удалить выбранную строку | `DeleteCommand` |
| `btnSave.Click` | вставка всех строк, сообщение, очистка | `SaveCommand` |

## 4. Данные
- Справочники (без `ORDER BY`, как в легаси): `SELECT EmployeeID, EmployeeName FROM Employee`;
  `SELECT ArticulID, ArticulName FROM Articul`; `SELECT id, part_name FROM ClothingParts`;
  `SELECT id, machine_make + ' ' + machine_model AS machine_displayname FROM KnittingMachines`.
- Сохранение: `INSERT INTO [KnittingOperationLog] ([document_number], [article_id], [employee_id], [part_id],
  [machine_id], [amount], [date_execution], [time_execution], [machines_in_zone], [work_day])
  VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)`.
- Типы: `document_number` — текст; `article_id`/`employee_id`/`part_id`/`machine_id`/`amount`/
  `machines_in_zone`/`work_day` — Int32; `date_execution` — Date; `time_execution` — Double.
- Порядок колонок грида: ФИО, Номер квитанции, Дата выполнения, Артикул, Деталь, Вяз. машина,
  Количество, Время выполнения, Машин в зоне обсл., Рабочий день. ID-поля хранятся в модели, но не показываются.
- Сохранение — одна транзакция OleDb (как в легаси).

## 5. Бизнес-правила и формулы
- Обязательны: сотрудник, артикул, деталь, оборудование, номер квитанции, количество, рабочий день.
- Пустые «Время выполнения» и «Машин в зоне обсл.» при вставке заменяются на `"0"` (как в легаси).
- Дата сохраняется без времени (`Date`; в легаси — `ToShortDateString`).
- Числа при сохранении: `Convert.ToInt32`/`Convert.ToDouble` в текущей культуре (ru-RU), время — `double`.
- Лимита рабочего дня 480 в легаси нет — не добавлять (отличается от швейной формы).
- После загрузки в combo выбираются первые элементы (поведение WinForms по умолчанию); прочие поля пустые.
- После успешного сохранения грид очищается; поля шапки не сбрасываются.

## 6. Валидация и edge cases
- Не заполнено обязательное: «Заполните все обязательные поля перед добавлением!» / «Внимание», строка не добавляется.
- Удаление без выбора: «Выберите строку для удаления!» / «Информация».
- Сохранение пустой таблицы: «Таблица пуста. Нет данных для сохранения!» / «Внимание».
- Успех: «Данные успешно сохранены!» / «Успех».
- Ошибка БД: «Ошибка при сохранении данных: » + `ex.Message` / «Ошибка» (как в легаси).
- Нецелые/нечисловые значения при сохранении: в легаси ловится общим catch; в целевой версии — адресные сообщения
  «Неверный формат числа в поле '…'!» (отклонение, дружелюбнее).
- Пустые справочники: combo пусты, «Вставить» выдаёт предупреждение; исключений нет.
- Нули и отрицательные значения легаси не проверяет; целевая версия сохраняет это поведение.

## 7. Навигация и поведение окна
Меню «Новая (Вязальный цех)» → `NewKnittingInvoiceViewModel` в `ContentControl` оболочки (ADR-007), не MDI.
Кнопки «Закрыть» в легаси нет — возврат через меню. Каждое открытие — новое состояние, грид не персистится.
Целевая версия использует адаптивную шапку (`WrapPanel`) вместо фиксированных координат WinForms.

## 8. Паритет-чек-лист
- [x] Загрузка справочников, дата по умолчанию
- [x] Валидация обязательных полей и пустых время/машин в зоне
- [x] Вставка/удаление строк без записи в БД
- [x] Сохранение в транзакции, сообщения, очистка грида
- [x] Числовой ввод (целые и время с 2 знаками)
- [x] Сборка на Windows

Проверено 2026-10-22: MSBuild Debug — 0 ошибок/предупреждений; UIA открывает меню «Новая (Вязальный цех)»,
рендерятся 4 combo, 10 колонок, 6 полей; VM-харнесс на копии БД проверил загрузку, вставку, удаление,
сохранение (строка появилась и удалена), пустую таблицу, обязательные поля, значения по умолчанию 0 и дату без времени.

## 9. Открытые вопросы
- Лимит 480 для вязального рабочего дня не добавляем — паритет с легаси (ADR-017).
- Сброс полей после сохранения не делаем — в легаси не сбрасываются (ADR-017).
