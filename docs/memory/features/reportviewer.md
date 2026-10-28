# Feature: ReportViewer (Отчет по сотруднику)

- Легаси: `ReportViewer.vb` (1–89), Designer (24–297), `ReportEmployee.rdlc`
- Цель: `Views/ReportViewerView.xaml` / `ViewModels/ReportViewerViewModel.cs`, `Controls/ReportViewerHost.cs`
- Статус: в работе
- Зависимости: `Employee`, представление `ReportEmployee`; ReportViewer 11 WinForms (ADR-006, ADR-029)

## 1. Назначение и сценарии
Пользователь открывает «Отчеты → Отчет по сотруднику», видит список сотрудников, выбирает одного
или нескольких, задаёт период и нажимает «Отчет». При успехе список скрывается и показывается
RDLC-отчёт «Отчет по сотруднику» (группировка: сотрудник → дата → номер документа). «Назад»
возвращает к списку сотрудников (даты и выделение сохраняются).

## 2. UI (только из Designer)
| Контрол | Тип | Назначение | Привязка/команда |
|---|---|---|---|
| dgvEmployee | DataGridView, MultiSelect | список сотрудников | `Employees`, `SelectedEmployees` |
| EmployeeID… | колонка, Visible=False | ID для отбора | `EmployeeId` |
| EmployeeName… | колонка | «Сотрудник» | `EmployeeName` |
| EmployeeNumber… | колонка | «Табельный номер» (строка с ведущими нулями) | `EmployeeNumber` |
| panelEmployee / panelReport | Panel | список / отчёт | `IsReportVisible` |
| ReportByEmployeeViewer | ReportViewer | RDLC `ReportEmployee.rdlc` | `Controls/ReportViewerHost` |
| Label2 / dtpFrom | Label + DateTimePicker | «Период от» / дата начала | `DateFrom` |
| Label3 / dtpTo | Label + DateTimePicker | «до» / дата конца | `DateTo` |
| btnLoadReport | Button | «Отчет» | `BuildReportCommand` |
| Button1 | Button | «Назад» | `BackCommand` |

## 3. События и команды
| Событие легаси | Действие | Команда/метод VM |
|---|---|---|
| ReportViewer_Load | загрузка `ReportEmployee` (результат не используется) и `Employee` | `LoadEmployees` в конструкторе (лишний Fill пропущен) |
| btnLoadReport.Click | проверки → выборка → RefreshReport → показать отчёт | `BuildReport` |
| Button1.Click | `panelEmployee.Visible = True`, `panelReport.Visible = False` | `Back` |
| dtpTo.ValueChanged | пустой обработчик | — |

## 4. Данные
- Сотрудники (только чтение, без ORDER BY): `SELECT EmployeeID, EmployeeName, EmployeeNumber FROM Employee`.
- Отчёт (только чтение): `SELECT EmployeeID, EmployeeName, DateExecution, DocumentNumber,
  OperationName, ArticulName, RateTime, NotRate, [Count] FROM ReportEmployee
  WHERE EmployeeID IN (?, …) AND DateExecution BETWEEN ? AND ?`.
- RDLC dataset `ReportEmployee`: `EmployeeName`, `DateExecution`, `DocumentNumber`, `OperationName`,
  `ArticulName`, `Count`, `EmployeeID`, `RateTime`, `NotRate`.
- Запись в БД отсутствует; транзакций нет. Все параметры — `OleDb`.

## 5. Бизнес-правила и формулы
- В легаси ID выбранных строк берутся из скрытой колонки 0 и склеиваются в строку `"id, id"`.
- Даты: `dtp.Value.Date`; проверка `dfFrom <= dfTo`; сравнение по календарным датам.
- Пустой результат: `Rows.Count <= 0` → «Нет данных по Сотруднику.», панель отчёта не показывается.
- `dataTable.Rows(0).SetField(6, 12)` перезаписывает `EmployeeID` первой строки; в RDLC это поле
  не выводится, поэтому в целевой версии не воспроизводится.
- RDLC «Итого по операции» = `RateTime * Count + NotRate`; суммы `Sum(RateTime)`, `Sum(NotRate)`,
  `Sum(Count)` по группам; формат даты `d`; страница A4 (21×29,7 см). Округлений в коде нет.

## 6. Валидация и edge cases
- Ничего не выбрано: «Выберите сотрудника для расчета» (Warning), выход.
- Начало периода позже конца: «Дата начала периода не может быть больше даты конца периода.».
- Нет строк по выбранным сотрудникам/периоду: «Нет данных по Сотруднику.».
- Несколько сотрудников: легаси-запрос `IN (?)` со строкой `"29, 43"` фактически возвращает
  только первого (проверено на БД); целевая версия строит `IN (?, ?)` и возвращает всех выбранных.
- Дубликаты имён сотрудников в RDLC сливаются в одну группу (группировка по `EmployeeName`).
- Ошибка загрузки списка: «Ошибка при загрузке данных: » + текст, «Ошибка» (в легаси не перехватывалась).
- Ошибка выборки/рендеринга: «Ошибка при формировании отчета: » + текст; старая панель не ломается.
- Пустые даты в целевом `DatePicker` трактуются как сегодняшняя дата (в легаси поле непустое).

## 7. Навигация и поведение окна
Меню «Отчеты → Отчет по сотруднику» → `ReportViewerViewModel` в `ContentControl`.
По умолчанию `DateFrom = DateTo = сегодня` (как `DateTimePicker`), видима панель сотрудников.
«Назад» переключает панели, не закрывая экран; список и даты не сбрасываются. При повторном
«Отчет» данные перечитываются и отчёт обновляется. Выбор строк — множественный (`Extended`, `FullRow`).

## 8. Паритет-чек-лист
- [x] Сценарии легаси выписаны; карточка сверена с `.vb`/Designer/RDLC
- [x] SQL и таблицы сверены с `.xsd`; мультивыбор проверен на копии БД
- [ ] Сборка VS18 MSBuild Debug: 0 ошибок/предупреждений
- [ ] VM-харнесс: проверки выбора/дат/пустых состояний/ошибок/«Назад»
- [ ] Рендер RDLC на копии БД
- [ ] UIA-сценарий: меню, выбор, «Отчет», «Назад»

## 9. Отклонения и открытые вопросы
- ADR-029: RDLC — embedded resource; ReportViewer 11 WinForms через `WindowsFormsHost`;
  сборки ReportViewer берутся из `ProdControl/packages` и копируются в output.
- Исправлен баг мультивыбора (`IN (?)` → список `?`); пропущены лишний `Fill(ReportEmployee)`
  и `SetField(6, 12)`, не влияющие на отчёт; добавлены безопасные сообщения об ошибках БД.
- `TimeExecutionReport.rdlc` в легаси не используется — не портируется.
- Открытый вопрос: установлен ли ReportViewer 11 runtime на целевых машинах (см. 05-decisions).
