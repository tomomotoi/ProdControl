# Feature: SearchBillForm (Поиск/редактирование накладных, швейный цех)

- Легаси: `SearchBillForm.vb` (1–332); Designer — только имена/свойства контролов (73–431)
- Цель: `Views/SearchBillView.xaml`, `ViewModels/SearchBillViewModel.cs`, `Data/SearchBillRepository`, `Models/SearchBillModels`
- Статус: в работе (код реализован; сборка/проверка на Windows не выполнены)
- Зависимости: `ISearchBillRepository` (`IDbConnectionFactory`), `IDialogService`; таблицы `OperationLog`, `Employee`, `Articul`, `Operation`

## 1. Назначение и сценарии
Поиск строк `OperationLog` по сотруднику, артикулу, сотруднику+артикулу или номеру квитанции; опционально за период.
Результат — таблица. В режиме редактирования можно изменить дату/время/количество/рабочий день, удалить строку, сохранить.
«Найти все» показывает всю таблицу без фильтров. Успех — «Изменения сохранены в базе данных!».
Удаление и правка копятся в памяти до «Сохранить»; поиск эти очереди не очищает.

## 2. UI (из Designer)
| Контрол | Тип | Назначение | Привязка/команда |
|---|---|---|---|
| cbFilter | ComboBox | «По сотруднику», «По артикулу», «Сотрудник + артикул», «По номеру квитанции» | `SelectedFilterIndex` |
| cbEmployee | ComboBox | сотрудник | `SelectedEmployee` |
| cbArticul | ComboBox | артикул | `SelectedArticul` |
| txDocNum | TextBox | номер квитанции | `DocumentNumber` |
| chckbxByPeriod | CheckBox | «За период» | `IsByPeriod` |
| datePickerFrom/To | DateTimePicker | «Дата с:/по:» (по умолчанию сегодня) | `DateFrom`/`DateTo` |
| btnSearch | Button | «Найти» | `SearchCommand` |
| btnSearchAll | Button | «Найти все» | `SearchAllCommand` |
| dgView | DataGridView | результаты; колонки ниже | `Rows`, `SelectedRow` |
| chckbxEditingMode | CheckBox | «Режим редактирования» | `IsEditingMode` |
| btnDelete | Button | «Удалить» | `DeleteCommand` |
| btnSave | Button | «Сохранить» | `SaveCommand` |
| dgView col 0 LogId | text | LogID, видима | read-only |
| dgView col 1 EmployeeName | combo | имя из `Employee` | read-only |
| dgView col 2 OperationName | combo | имя из `Operation` | read-only |
| dgView col 3 ArticulName | combo | имя из `Articul` | read-only |
| dgView col 4 DateExecution | date | дата выполнения | правится через DatePicker |
| dgView col 5 TimeExecution | text | время | правится в edit-mode |
| dgView col 6 Count | text | количество | правится в edit-mode |
| dgView col 7 DocumentNumber | text | номер квитанции | read-only |
| dgView col 8/9 | text/check | workDay; isChanged скрыта, всегда false | workDay правится; isChanged не портировать |

## 3. События и команды
| Событие легаси | Действие | Команда/метод VM |
|---|---|---|
| `SearchBillForm_Load` | загрузка справочников и `OperationLog1` (последний не используется), `cbFilter=0`, инициализация overlay-даты | `Load` |
| `cbFilter.SelectedIndexChanged` | видимость полей: 0 — сотрудник; 1 — артикул; 2 — оба; 3 — номер | `SelectedFilterIndex` |
| `chckbxByPeriod.CheckedChanged` | показать/скрыть даты | `IsByPeriod` |
| `btnSearch.Click` | query по фильтру + периоду | `SearchCommand` |
| `btnSearchAll.Click` | `SELECT * FROM OperationLog` | `SearchAllCommand` |
| `btnDelete.Click` | добавить LogID в список удаления, убрать строку | `DeleteCommand` |
| `btnSave.Click` | удалить, затем обновить, сообщения | `SaveCommand` |
| `dgView.CellValueChanged` | валидация int и запись snapshot в словарь по LogID | row PropertyChanged |
| `chckbxEditingMode.CheckedChanged` | grid ReadOnly, дата, кнопки | `IsEditingMode` |
| `dgView.CellDoubleClick` (col 4) | показать DateTimePicker и записать дату | inline DatePicker |
| `dgView.Scroll/CurrentCellChanged` | скрыть DateTimePicker | не требуется |

## 4. Данные
- Таблицы: `OperationLog` (LogID, EmployeeID, OperationID, ArticulID, DateExecution, TimeExecution, Count, DocumentNumber, workDay), `Employee`, `Articul`, `Operation`.
- Справочники: `SELECT EmployeeID, EmployeeName FROM Employee`; `SELECT ArticulID, ArticulName FROM Articul`; `SELECT OperationID, OperationName FROM Operation`.
- Все строки: `SELECT LogID, EmployeeID, OperationID, ArticulID, DateExecution, TimeExecution, [Count], DocumentNumber, workDay FROM OperationLog`.
- Фильтры (параметры): `EmployeeID = ?`; `ArticulID = ?`; оба; `DocumentNumber LIKE ?` (без добавления `%`); период `AND DateExecution >= ? AND DateExecution <= ?`.
- Удаление: `DELETE FROM OperationLog WHERE LogID = ?`.
- Обновление: `UPDATE OperationLog SET EmployeeID=?, OperationID=?, ArticulID=?, DateExecution=?, TimeExecution=?, [Count]=?, DocumentNumber=?, workDay=? WHERE LogID=?`.
- Имена в гриде — из справочников по ID (не JOIN: строки с «сиротскими» ID не теряются).
- Сохранение — одна транзакция OleDb (отличие от легаси, ADR).

## 5. Бизнес-правила и формулы
- `cbFilter` по умолчанию 0; в combo сотрудника/артикула выбран первый элемент.
- Период включается только при checked; границы включительно, сравниваются даты без времени.
- Snapshot для UPDATE создаётся при любом изменении строки, если `TimeExecution`, `Count`, `workDay` парсятся как `Integer` (VB `Integer.TryParse`, не `CInt`).
- Очереди удалений/обновлений живут до успешного сохранения и переживают новый поиск; после сохранения очищаются.
- Формат даты в UI — короткая дата (`d`, ru-RU).
- В легаси колонки 4–8 помечены `ReadOnly=True`, поэтому фактически правится только дата (через overlay-picker). Целевая версия разрешает в edit-mode править дату, время, количество и рабочий день — как задумано сообщением валидации и UPDATE; отклонение фиксируется ADR.
- Легаси не вызывает `setEditingControlsEditMode(False)` при загрузке: грид read-only, но «Удалить»/«Сохранить» активны. Целевая версия сразу применяет режим: кнопки недоступны вне edit-mode (ADR).
- После успешного сохранения грид не перезагружается; удалённые строки уже убраны, изменённые остаются с новыми значениями.

## 6. Валидация и edge cases
- Нецелые Time/Count/workDay: «Поля 'Время выполнения', 'Количество' и 'Рабочий день' должны быть целочисленными!» / «Ошибка»; snapshot не обновляется, значение в гриде остаётся.
- Нет изменений: «Нет изменений для сохранения!» / «Уведомление».
- Успех: «Изменения сохранены в базе данных!» / «Уведомление».
- Ошибки БД: `ex.Message` / «Ошибка».
- Удаление без выбранной строки: в легаси исключение; в целевой версии — no-op.
- Пустой сотрудник/артикул при поиске: в легаси SQL-ошибка; в целевой версии — сообщение «Выберите сотрудника/артикул.».
- Пустой `DocumentNumber`: `LIKE ''` (в легаси так же), совпадение только с пустыми.
- `DBNull` в числовых полях: отображается как 0; `DBNull` даты — пустая дата.
- Отсутствующий ID в справочнике: имя пустое (как пустая combo в легаси).
- Дата «с» позже «по»: пустой результат, без сообщения.

## 7. Навигация и поведение окна
Меню «Найти (Швейный цех)» → `SearchBillViewModel` в `ContentControl` (ADR-007), не MDI.
Заголовок легаси — «Найти накладную»; в оболочке статус — «Поиск накладных». Кнопки «Закрыть» в легаси нет; возврат — через меню.
Каждое открытие — новое состояние; pending-изменения не переносятся между открытиями.

## 8. Паритет-чек-лист
- [ ] Загрузка справочников и фильтров
- [ ] Поиск по 4 режимам + период + «Найти все»
- [ ] Видимость полей фильтра и периода
- [ ] Режим редактирования и блокировки
- [ ] Накопление/сохранение удалений и правок
- [ ] Сообщения и edge cases
- [ ] Сборка на Windows
## 9. Открытые вопросы
- Подтвердить на Windows edit-mode (в легаси фактически правится только дата) и начальное состояние кнопок; решение — ADR.
