# Feature: EmployeesForm (Сотрудники)

- Легаси: `EmployeesForm.vb` (1–275); Designer — контролы/колонки (24–374); вызов `mainForm.vb:295–299`
- Цель: `Views/EmployeesView.xaml`, `ViewModels/EmployeesViewModel.cs`,
  `Data/IEmployeesRepository`, `Data/EmployeesRepository`, `Models/EmployeesModels.cs`
- Статус: готово (MSBuild 0/0; VM-харнесс 67/67; UIA-сценарий)
- Зависимости: `IDbConnectionFactory`, `IDialogService`; таблицы `Employee`, `Position`

## 1. Назначение и сценарии
Справочник сотрудников: поиск по ФИО/номеру/должности/разряду, «Найти всех», добавление,
удаление и правка в режиме редактирования с сохранением в `Employee`.
При открытии грид пуст, загружается только `Position`, фильтр не выбран (`SelectedIndex = -1`).

## 2. UI (из Designer)
| Контрол | Тип | Назначение | Привязка/команда |
|---|---|---|---|
| `cbFilter` | ComboBox | «По ФИО», «По номеру», «По должности», «По разряду» | `FilterOptions` / `SelectedFilterIndex` |
| `txSearchBox` | TextBox | значение фильтра (ФИО/номер/разряд) | `SearchText` |
| `cbSearchPosition` | ComboBox | должность для фильтра №3 | `Positions` / `SelectedPosition` |
| `dgView` | DataGrid | сотрудники; колонки ФИО, №, Должность (combo), Разряд | `Employees` / `SelectedEmployee` |
| `chckbxAllowEdit` | CheckBox | «Режим редактирования» | `IsEditingMode` |
| `btnSearchAll` | Button | «Найти всех» | `SearchAllCommand` |
| `btnSearch` | Button | «Найти» | `SearchCommand` |
| `btnAdd` / `btnDelete` / `btnSave` | Button | «Добавить» / «Удалить» / «Сохранить» | команды; enabled = edit-mode |

Скрытые колонки легаси `EmployeeId`, `isChanged`, `isNew` не показывать; `isNew` — флаг строки.
Подписи: «Фильтр:», «ФИО:», «№ сотрудника:», «Должность:», «Разряд:».

## 3. События и команды
| Событие легаси | Действие | Команда/метод VM |
|---|---|---|
| `Load` | заполнить `Position`, `cbFilter.SelectedIndex = -1` | `Load()` в конструкторе |
| `cbFilter.SelectedIndexChanged` | очистить текст, показать нужное поле/подпись | setter `SelectedFilterIndex` |
| `btnSearchAll.Click` | очистить удаления, перечитать `Position`, `SELECT * FROM Employee` | `SearchAllCommand` |
| `btnSearch.Click` | один из 4 фильтров; при -1 — ничего | `SearchCommand` |
| `chckbxAllowEdit.CheckedChanged` | grid.ReadOnly = !edit; Add/Delete/Save.Enabled = edit | `IsEditingMode` |
| `btnAdd.Click` | добавить пустую строку, `isNew = true` | `AddCommand` |
| `btnDelete.Click` | запомнить ID, убрать строку; новую — без DELETE | `DeleteCommand` |
| `dgView.CellValueChanged` | правка существующей строки → запись в словарь update | `OnRowPropertyChanged` |
| `btnSave.Click` | DELETE → INSERT → UPDATE; сообщения | `SaveCommand` |

## 4. Данные
- Справочник: `SELECT PositionID, PositionName FROM Position`.
- Список: `SELECT EmployeeID, EmployeeName, EmployeeNumber, PositionID, PositionLevelID FROM Employee`.
- Фильтры: `WHERE EmployeeName LIKE ?` / `EmployeeNumber LIKE ?` с `%text%`;
  `WHERE PositionID = ?`; `WHERE PositionLevelID = ?` (разряд — `int.TryParse`).
- INSERT: `INSERT INTO Employee (EmployeeName, EmployeeNumber, PositionID, PositionLevelID) VALUES (?, ?, ?, ?)`.
- UPDATE: `UPDATE Employee SET EmployeeName = ?, EmployeeNumber = ?, PositionID = ?, PositionLevelID = ? WHERE EmployeeID = ?`.
- DELETE: `DELETE FROM Employee WHERE EmployeeID = ?`.
- Сохранение всех изменений — одна OleDb-транзакция (ADR-024); порядок как в легаси.
- `EmployeeName`/`EmployeeNumber` — строки; `PositionID`/`PositionLevelID` — int.

## 5. Бизнес-правила и формулы
- Фильтр ФИО/номера: `%` вокруг текста, пустой текст → все; должность — выбранный `PositionID`;
  разряд — целое, иначе «Поле 'Разряд' должно быть целочисленным!» / «Ошибка».
- Новая строка валидна, только если заполнены ФИО, номер, должность и разряд
  (сообщение «Для добавления нового поля все ячейки должны быть заполнены!» / «Ошибка»).
- Разряд существующей строки — целое; иначе «Поле 'Разряд должности' должно быть целочисленным!» / «Ошибка».
- После INSERT — перечитать всех (`SELECT * FROM Employee`), как `fetchDateAfterInsertion`.
- Сообщения: «Изменения сохранены в базе данных!» / «Нет изменений для сохранения!» — «Уведомление».
- Формат чисел — ru-RU; строка разряда без округлений (целое).

## 6. Валидация и edge cases
- Нет выбранного фильтра: «Найти» ничего не делает (легаси `Select Case -1`).
- Нет строки для удаления: «Выберите сотрудника для удаления.» (в легаси — исключение).
- Нет должности для фильтра: «Выберите должность.» (в легаси — исключение).
- Удаление новой строки не ставит DELETE (в легаси добавлялся ID 0); её правки не попадают в update.
- Удаление существующей строки убирает её из словаря update.
- Поиск/«Найти всех» сбрасывают несохранённые добавления/правки/удаления (в легаси грид
  очищался, а очереди оставались — потенциальные «фантомные» записи).
- Новые строки с ошибкой блокируют сохранение целиком (в легаси валидные вставлялись, невалидные
  пропускались с сообщением); при ошибке БД — откат транзакции и `ex.Message` / «Ошибка».
- Пустые `EmployeeName`/`EmployeeNumber` в БД отображаются пустыми строками; `PositionID`/`LevelID`
  могут быть NULL — тогда должность не выбрана, разряд пуст.

## 7. Навигация и поведение окна
Меню «Основные данные → Сотрудники» → `EmployeesViewModel` в `ContentControl` оболочки (ADR-007).
Каждое открытие — новое состояние; закрытия/модальности в легаси нет.

## 8. Паритет-чек-лист
- [x] Все 4 фильтра и «Найти всех» на копии БД
- [x] Добавление/удаление/правка и сохранение в транзакции
- [x] Сообщения и пустые состояния
- [x] Сборка VS18 MSBuild
- [x] UIA smoke/сценарий

## 9. Открытые вопросы
- Оставлять ли `cbFilter` без выбора при старте (легаси) или выбирать «По ФИО» — пока паритет.
- Редактирование колонок в edit-mode: в легаси колонки помечены `ReadOnly=True` (фактически
  правилась только новая строка), целевая версия разрешает правку (ADR-024, как ADR-016/018).
