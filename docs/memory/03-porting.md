# 03 · Плейбук портирования

## Золотое правило
Сначала карточка фичи, потом код. Легаси читается только для уточнения; не переписывай по памяти.
Один экран/сценарий за раз. Сохраняй поведение, а не структуру WinForms.

## VB.NET → C#
| VB | C# |
|---|---|
| `Imports` | `using` |
| `Module` | `static class` |
| `Handles` | подписка на событие / команда |
| `MyBase` | `base` |
| `My.Settings` | `ConfigurationManager` / настройки |
| `My.Computer.FileSystem` | `System.IO` |
| `DateAndTime.Today` | `DateTime.Today` |
| `Nothing` | `null` |
| `IsNothing(x)` | `x is null` |
| `CType(x, T)` | `(T)x` / `Convert.To...` |
| `DirectCast` | явное приведение |
| `&` | `+` / интерполяция |
| `Select Case` | `switch` |
| `For Each` | `foreach` |
| `If(cond, a, b)` | `cond ? a : b` |
| `\` (целочисленное деление) | `/` с явным приведением |
| `vbNull` | `null` / `DBNull.Value` |
| `Math.Round(x, 2)` | `Math.Round(x, 2)` (банковское округление по умолчанию) |

`Option Strict Off` означает неявные преобразования и позднее связывание. В C# делай явные
`Convert`/`Parse`/`TryParse` с `CultureInfo`; не полагайся на `dynamic`.

## WinForms → WPF
| WinForms | WPF |
|---|---|
| `Form` (дочерняя MDI) | `UserControl` + навигация; при необходимости отдельный `Window` |
| `Form` (модальная) | `Window.ShowDialog()` через `IDialogService` |
| `MenuStrip` | `Menu` |
| `ToolStrip` | `ToolBar`/`Button` |
| `DataGridView` | `DataGrid` |
| `ComboBox` | `ComboBox` + `ItemsSource`/`SelectedValue`/`DisplayMemberPath` |
| `DateTimePicker` | `DatePicker` |
| `CheckBox` | `CheckBox` + `IsChecked` |
| `TextBox` (число) | `TextBox` + numeric-поведение/контрол |
| `Panel.Visible` | `Visibility`/`ContentControl` |
| `MessageBox` | `IDialogService` |
| `InputBox` | свой prompt-диалог |
| `OpenFileDialog` | `Microsoft.Win32.OpenFileDialog` |
| `ReportViewer` | `WindowsFormsHost` + WinForms ReportViewer |
| `BackgroundWorker` | `Task`/`async` |
| `Timer` | `DispatcherTimer` |
| `DataTable` в гриде | `ObservableCollection<Model>` |

## Рецепт порта формы
1. Создай `features/<name>.md`; выпиши назначение и сценарии.
2. Из Designer возьми только имена/типы контролов и layout-группы; не копируй весь файл.
3. Из `.vb` выпиши события, SQL, формулы, проверки, сообщения.
4. Вынеси расчёты в `Domain/`; SQL и доступ к БД — в `Data/`.
5. Сделай ViewModel: состояние, команды, валидация, загрузка данных.
6. Сделай XAML на переиспользуемых стилях/контролах.
7. Подключи навигацию/диалоги; проверь сценарий и обнови прогресс.

## Частые сценарии
- **CRUD-справочник**: список + поля + «Добавить/Сохранить/Удалить/Очистить»; edit-mode, подтверждения, обновление после записи.
- **Поиск/фильтры**: фильтр по периоду/полю, «Показать все», сброс, пустой результат.
- **Накладная**: шапка + таблица строк + добавление/удаление + сохранение; операции фильтруются по техкарте.
- **Расчёт**: входные параметры → чистая функция → результат; сохранить округление и формат.
- **Отчёт**: выбор сотрудников/периода → ReportDataSource → RefreshReport; проверить RDLC.

## Подводные камни
- Не тащить `Microsoft.VisualBasic` и VB-совместимость; писать идиоматичный C#.
- Не портировать сгенерированные DataSet/TableAdapter-файлы; схема — из `.xsd`/запросов.
- Не менять формулы «по красоте»: сначала паритет, потом рефакторинг.
- Jet OLEDB 32-битный: не запускать целевой процесс как x64.
- Кириллические имена контролов легаси не переносить; в XAML — английские `x:Name`.
- `ArticulID = 74` и прочие магические числа — в константы с комментарием.
- Сообщения и форматы чисел/дат — как в легаси (ru-RU).

## Чек-лист фичи
- [ ] Карточка заполнена, сценарии и edge cases выписаны.
- [ ] SQL параметризован, таблицы/поля совпадают с Access.
- [ ] Расчёты вынесены и сверены на примерах.
- [ ] Навигация/диалоги/ошибки работают.
- [ ] Сборка на Windows проходит.
- [ ] Прогресс/решения обновлены.
