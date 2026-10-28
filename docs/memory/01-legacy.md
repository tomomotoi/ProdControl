# 01 · Легаси ProdControl (VB.NET/WinForms)

## Стек и структура
- VB.NET, WinForms, .NET Framework 4.8, MDI-оболочка, Access/Jet OLEDB.
- Strongly typed DataSets + TableAdapters, ReportViewer 11 (RDLC).
- Проект: `ProdControl/ProdControl/ProdControl.vbproj`. Точка входа — `mainForm`.
- 18 форм + 9 DataSet-дизайнеров + 2 отчёта. ~56k строк, из них ~50k — сгенерированные DataSet/Designer.
- **Не читать целиком**: `OperationDBDataSet*.Designer.vb`, `DBSet.Designer.vb`, `*.xsd`, `bin/`, `obj/`, `Back/`, `packages/`.

## Формы
| Файл | Назначение | Ключевое |
|---|---|---|
| `mainForm.vb` | MDI-оболочка, меню | навигация, backup/restore БД, очистка OperationLog |
| `newBill.vb` | Новая накладная (швейный цех) | артикул → операции из Routing; простой; запись в OperationLog |
| `SearchBillForm.vb` | Поиск/редактирование накладных | фильтры, период, inline-редактирование, удаление |
| `newBillWiz.vb` | Пустая форма-заглушка | не портировать без проверки |
| `CompletedOrders.vb` | Пустая форма-заглушка | не портировать без проверки |
| `calculationForm.vb` | Расчёт зарплаты | швейный и вязальный расчёт, экспорт CSV |
| `RoutingForm.vb` | Тех. карта (швейный) | операции, время, расценки, коэффициенты, тарифы |
| `KnittingRoutingForm.vb` | Тех. карта (вязание) | коэффициенты, комплекты, машины, детали |
| `newArticulForm.vb` | Новый артикул | INSERT в Articul |
| `OperationForm.vb` | Операции | CRUD, проверка использования в Routing |
| `TariffsCheckForm.vb` | Проверка расценок | массовый пересчёт тарифов по артикулам |
| `EmployeesForm.vb` | Сотрудники | CRUD, поиск/фильтры, должности/уровни |
| `CoefficientsForm.vb` | Коэффициенты доп. затрат | CRUD, режим редактирования |
| `DetailsForm.vb` | Детали изделий | CRUD `ClothingParts` |
| `MachinesForm.vb` | Оборудование | CRUD `KnittingMachines` |
| `NewKnittingInvoiceForm.vb` | Новая накладная (вязание) | запись в KnittingOperationLog |
| `KnittingSearchInvoiceForm.vb` | Поиск вязальных накладных | фильтры, inline-редактирование |
| `ReportViewer.vb` | Отчёты | ReportEmployee.rdlc, TimeExecutionReport.rdlc |

## Данные (Access)
- БД: `OperationDB.mdb`; строка `Provider=Microsoft.Jet.OLEDB.4.0; Data Source=|DataDirectory|\OperationDB.mdb`.
- Справочники: `Articul`, `Operation`, `Routing`, `Position`, `PositionLevel`, `Employee`, `Coefficients`, `ClothingParts`, `KnittingMachines`, `IdleOperation`.
- Швейный цех: `OperationLog`, `LogWorkDay`.
- Вязание: `KnittingOperationLog`, `KnittingRouting`.
- Отчёты/агрегаты: `ReportEmployee`, `ReportEmployee_old`, `NotRate`, `WorkDay`, `Tariff`, `RateExecute`, `TimeExecute`, `DopCost`.
- `ProductionControl2010ConnectionString` в `App.config` — наследие, в коде почти не используется.

## Ключевые бизнес-правила (сверять с кодом)
- Швейная ЗП: по `OperationLog` за период; если `ArticulID != 74` → `Count * Routing.Tariff`, иначе `PositionRateTime * TimeExecution`; округление 2 знака (`calculationForm.vb` ~350).
- Вязальная ЗП: группировка `KnittingOperationLog` по `machines_in_zone`; `SUM(time_execution) / machines_in_zone`; сумма по зонам; округление 2 знака (`calculationForm.vb` ~425).
- Новая накладная: операции фильтруются по техкарте артикула; время = время операции × процент простоя / 100; рабочий день ≤ 480 (`newBill.vb`).
- Тарифы/расценки: `RoutingForm`, `TariffsCheckForm`; формулы зависят от `PositionLevel`, `Operation`, коэффициентов.
- Резервные копии: при первом дне месяца и вручную; `Архив/OperationDB-dd.MM.yyyy--HH-mm-ss-ffff.mdb`; `CurrentDay.bin`; восстановление из файла/последней копии (`mainForm.vb`).

## Особенности легаси
- `Option Strict Off`: неявные преобразования и позднее связывание — в C# нужны явные `Convert`/`Parse` с культурой.
- MDI-дочерние формы, кириллические имена контролов, `InputBox`, `My.Settings`, `My.Computer`.
- Много SQL-конкатенации; в целевом коде — только параметры.
- Jet OLEDB 32-битный: целевой процесс, вероятно, x86 (см. ADR-004).
- Магическое число `ArticulID = 74` (артикул без расценки) — вынести в константу/настройку.
