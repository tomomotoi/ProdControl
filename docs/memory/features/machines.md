# Feature: MachinesForm (Оборудование)

- Легаси: `MachinesForm.vb` (1–150); Designer (24–196); вызов `mainForm.vb:325–329`
- Цель: `Views/MachinesView.xaml`, `ViewModels/MachinesViewModel.cs`,
  `Data/IMachinesRepository`, `Data/MachinesRepository`, `Models/MachinesModels.cs`
- Статус: готово (собран; VM-харнесс 100/100, UIA-сценарий на копии БД)
- Зависимости: `IDbConnectionFactory`, `IDialogService`; таблица `KnittingMachines`

## 1. Назначение и сценарии
Справочник вязального оборудования: список станков, добавление, редактирование марки/модели, удаление.
Открывается из меню «Основные данные → Оборудование» (заголовок окна «Оборудование»).
Успех — строка `INSERT`/`UPDATE`/`DELETE` и перечитанный список (сортировка `id DESC`).

## 2. UI (из Designer)
| Контрол | Тип | Назначение | Привязка/команда |
|---|---|---|---|
| `labelList` | Label | «Список оборудования» | — |
| `dgvMachines` | DataGridView | колонки `ID`, «Марка оборудования», «Модель оборудования» (fill) | `Machines` / `SelectedMachine` |
| `gboxMachineParameters` | GroupBox | «Параметры оборудования» | — |
| `labelId` / `tbId` | Label / TextBox (Enabled=False) | ID выбранной записи | `MachineId` (read-only) |
| `labelMake` / `tbMake` | Label / TextBox | марка оборудования | `Make` |
| `labelModel` / `tbModel` | Label / TextBox | модель оборудования | `Model` |
| `btnAdd` | Button | «Добавить» | `AddCommand` |
| `btnSaveChanges` | Button | «Сохранить изменения» | `SaveCommand` |
| `btnDelete` | Button | «Удалить» | `DeleteCommand` |
| `btnClearFields` | Button | «Очистить поля» | `ClearCommand` |

## 3. События и команды
| Событие легаси | Действие | Команда/метод VM |
|---|---|---|
| `Load` | `SELECT id, machine_make, machine_model ... ORDER BY id DESC` | `Load()` в конструкторе |
| `dgvMachines.SelectionChanged` | ID/марка/модель выбранной строки → поля | сеттер `SelectedMachine` |
| `btnAdd.Click` | проверка пустоты, INSERT, успех, очистка, reload | `AddCommand` |
| `btnSaveChanges.Click` | проверка ID и пустоты, UPDATE по `id`, успех, reload | `SaveCommand` |
| `btnDelete.Click` | проверка ID, подтверждение, DELETE, успех, очистка, reload | `DeleteCommand` |
| `btnClearFields.Click` | очистить ID/марку/модель и выделение | `ClearCommand` |

## 4. Данные
- Схема: `KnittingMachines(id AutoNumber/Integer PK, machine_make Text(130) NULL,
  machine_model Text(130) NULL)`.
- Чтение: `SELECT [id], [machine_make], [machine_model] FROM [KnittingMachines] ORDER BY [id] DESC`;
  `DBNull` → пустая строка.
- INSERT: `INSERT INTO [KnittingMachines] ([machine_make], [machine_model]) VALUES (?, ?)`
  — `@make`, `@model` Text; ID генерирует Access.
- UPDATE: `UPDATE [KnittingMachines] SET [machine_make] = ?, [machine_model] = ? WHERE [id] = ?`
  — `@make`, `@model` Text, `@id` Integer.
- DELETE: `DELETE FROM [KnittingMachines] WHERE [id] = ?` — `@id` Integer.
- Одиночные параметризованные команды, без транзакций; после add/save/delete — reload.
- `Trim()` выполняется перед записью.

## 5. Бизнес-правила и формулы
- Сортировка только SQL: `id DESC` (новые сверху); вычислений/округлений нет.
- В БД пишутся `machine_make`/`machine_model` после `Trim`; `NULL` читается как `""`.
- После загрузки выделение не устанавливается (как в `DetailsForm`): поля пустые.
- При выборе строки поля заменяются её значениями; снятие выделения поля не очищает.
- После save список перечитывается, выделение не восстанавливается, но ID/марка/модель остаются
  в полях; «Удалить» работает по ID из поля, поэтому сразу после save удаляет ту же запись (паритет).
- После add/delete — `ClearFields()` (поля и выделение), затем reload.
- ID в поле только отображается и не редактируется пользователем.
- Если reload после успешной записи падает, показывается «Ошибка загрузки данных: » (как `LoadData`),
  успешная запись при этом уже выполнена; грид остаётся со старыми строками.
- Грид в целевой версии read-only: inline-правки легаси в БД не сохранялись, правка — через поля.

## 6. Валидация и edge cases
- Add, пустая/пробельная марка или модель:
  «Заполните марку и модель оборудования» / «Предупреждение» (Warning).
- Save без выбранного ID: «Выберите запись для редактирования из таблицы» / «Предупреждение».
- Save, пустая/пробельная марка или модель:
  «Поля марки и модели не могут быть пустыми» / «Предупреждение».
- Delete без выбранного ID: «Выберите запись для удаления» / «Предупреждение».
- Delete: подтверждение `Удалить оборудование с ID {id}?` / «Подтверждение», YesNo/Question;
  при «Нет» ничего не происходит.
- Успехи (Info): «Запись успешно добавлена», «Изменения успешно сохранены», «Запись удалена» /
  заголовок «Успех».
- Ошибки БД (Error): load — «Ошибка загрузки данных: » + `ex.Message`;
  add — «Ошибка при добавлении: »; save — «Ошибка при сохранении: »; delete — «Ошибка при удалении: ».
- Дубликаты марки/модели разрешены (проверки нет); `UPDATE`/`DELETE` без затронутых строк
  всё равно считаются успехом (паритет с легаси).

## 7. Навигация и поведение окна
Меню «Основные данные → Оборудование» → `MachinesViewModel` в `ContentControl` оболочки (ADR-007).
Отдельной кнопки «Закрыть» нет; каждое открытие — новое состояние.

## 8. Паритет-чек-лист
- [x] Маршрут меню открывает форму
- [x] Список загружается по `id DESC`, колонки ID/Марка/Модель
- [x] Выбор строки заполняет ID/марку/модель, снятие выбора их не очищает
- [x] Add/Save/Delete: все сообщения и подтверждение как в легаси
- [x] Clear: очищает поля и выделение
- [x] Ошибки БД и пустые поля обрабатываются
- [x] Сборка VS18 MSBuild Debug (slnx): 0 ошибок, 0 предупреждений
- [x] VM-харнесс 100/100 (stub: выбор/очистка, все валидации, ошибки load/write/reload,
  cancel подтверждения, `null`; реальный репозиторий: 4 строки, INSERT/UPDATE/DELETE, NULL)
- [x] UIA-сценарий: меню, 4 строки, выбор, пустые сообщения, add/save/clear,
  delete cancel/yes, read-only ID; `UI_MACHINES_OK`

## 9. Открытые вопросы
- Нет.
