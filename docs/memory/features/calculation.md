# Feature: calculationForm (Расчет / Норма выработки)

- Легаси: `calculationForm.vb` (1–466), Designer (24–295), меню `mainForm.vb:19–23`
- Цель: `Views/CalculationView.xaml` / `ViewModels/CalculationViewModel.cs`
- Статус: готово (сборка 0/0, VM-харнесс 129/129, UIA + экспорт)
- Зависимости: `Employee`, `OperationLog`, `Routing`, `PositionLevel`, `KnittingOperationLog`

## 1. Назначение и сценарии
Пользователь выбирает одного/нескольких сотрудников в таблице, дату (или период) и нажимает
«Рассчитать»: для должности 9 (вязальщица) — расчёт по `KnittingOperationLog`, для остальных —
по `OperationLog` + `Routing`. Заполняются «Заработная плата», «Норма Выроботки», «Время выполнения».
«Выгрузить» сохраняет выбранные строки в CSV (`;`, UTF-8). «Закрыть» — возврат на приветствие.

## 2. UI (только из Designer)
| Контрол | Тип | Назначение | Привязка/команда |
|---|---|---|---|
| dbGrid | DataGridView, MultiSelect | сотрудники + результаты | `Rows`, `SelectedRows` |
| EmployeeID… | колонка | «Номер сотрудника» | `EmployeeId` |
| EmployeeName… | колонка | «Имя сотрудника» | `EmployeeName` |
| EmployeeNumber… | колонка | «Табельный номер» | `EmployeeNumber` |
| PositionID… | колонка, Visible=False | «Должность» | `PositionId` (скрыта) |
| PositionLevelID… | колонка | «Разряд» | `PositionLevelId` |
| salary | колонка, ReadOnly | «Заработная плата» | `Salary` |
| timerate | колонка, ReadOnly | «Норма Выроботки» (опечатка легаси) | `Norm` |
| TimeExecution | колонка | «Время выполнения» | `TimeExecution` |
| cbDateRange | CheckBox | «За Период» | `IsByPeriod` |
| dtFrom / dtTo | DateTimePicker | «Дата тетради» / «По» | `DateFrom` / `DateTo` |
| lbFrom / lbTill | Label | «Дата тетради» / «По» | видимость `По` по `IsByPeriod` |
| btExport | Button | «Выгрузить» | `ExportCommand` |
| btnCalculate | Button | «Рассчитать» | `CalculateCommand` |
| Button1 | Button | «Закрыть» | `CloseCommand` |

## 3. События и команды
| Событие легаси | Действие | Команда/метод VM |
|---|---|---|
| Form_Load | `EmployeeTableAdapter.Fill` → грид | `LoadEmployees` в конструкторе |
| cbDateRange.CheckedChanged | скрыть/показать `dtTo` и `lbTill` | `IsByPeriod` + Visibility |
| btnCalculate.Click | расчёт выбранных строк | `Calculate` |
| btExport.Click | SaveFileDialog → CSV выбранных строк | `Export` |
| Button1.Click | `Me.Close()` | `Close` → welcome |

## 4. Данные
- Справочник: `SELECT EmployeeID, EmployeeName, EmployeeNumber, PositionID, PositionLevelID FROM Employee ORDER BY EmployeeID`.
- Должность: `SELECT PositionID FROM Employee WHERE EmployeeID = ?`.
- Вязание: `SELECT machines_in_zone, SUM(time_execution) AS zone_time FROM KnittingOperationLog WHERE employee_id = ? [AND date_execution BETWEEN ? AND ? | AND date_execution = ?] GROUP BY machines_in_zone`.
- Швейная ЗП: `SELECT OperationLog.[Count], Routing.Tariff, OperationLog.TimeExecution, OperationLog.ArticulID FROM OperationLog INNER JOIN Routing ON OperationLog.ArticulID = Routing.ArticulID AND OperationLog.OperationID = Routing.OperationID WHERE OperationLog.EmployeeID = ? AND (DateExecution BETWEEN ? AND ? | DateExecution = ?)`.
- Ставка: `SELECT PositionRateTime FROM PositionLevel WHERE PositionID = (SELECT PositionID FROM Employee WHERE EmployeeID = ?) AND PositionLevel.PositionLevel = (SELECT Employee.PositionLevelID FROM Employee WHERE EmployeeID = ?)`.
- Время швеи: `SELECT OperationID, ArticulID, TimeExecution, [Count] FROM OperationLog WHERE EmployeeID = ? AND (DateExecution BETWEEN ? AND ? | DateExecution = ?)`.
- Время из техкарты: `SELECT TimeExecute FROM Routing WHERE ArticulID = ? AND OperationID = ?`.
- Рабочие дни: `SELECT workDay, DateExecution FROM OperationLog WHERE EmployeeID = ? AND DateExecution BETWEEN ? AND ? GROUP BY workDay, DateExecution`; для дня — `SELECT workDay FROM OperationLog WHERE EmployeeID = ? AND DateExecution = ? GROUP BY DateExecution, workDay` (первая строка).
- Запись в БД: нет. Всё параметризовано; только чтение.

## 5. Бизнес-правила и формулы
- Вязальщица (PositionID = 9): `zoneSum = Σ(SUM(time_execution) / (machines_in_zone <= 0 ? 1 : machines_in_zone))`;
  ЗП = `Round(zoneSum, 2)` → формат `0.00`; «Время выполнения» = `zoneSum` → `0.00`;
  норма = `zoneSum / workDay * 100` → `0.00`.
- Швея/остальные: `salary += ArticulID == 74 ? PositionRateTime * TimeExecution : Count * Tariff`;
  ЗП = `Round(salary, 2).ToString()` (без принудительных нулей); время = `Σ(TimeExecution == 0 ? Round(TimeExecute * Count, 6) : TimeExecution)`;
  норма = `time / workDay * 100` → `0.00`; «Время выполнения» → `0.00`.
- Рабочий день: по умолчанию 480. Период: `Σ(workDay * кол-во различных DateExecution с этим workDay)`,
  если строк нет — 480. Один день: первый `workDay` за дату, иначе 480.
- `workDay <= 0` → норма 0. `machines_in_zone` NULL/0 → 1. Нет `PositionRateTime` → 0.
  `Math.Round` — банковское, 2 и 6 знаков; культура ru-RU (запятая).

## 6. Валидация и edge cases
- Нет выбранных строк: «Выберите сотрудника для расчета», «Предупреждение», Warning; выход.
- Должность не найдена/ошибка: сообщение «Ошибка при определении должности: » + текст; расчёт идёт по швейной ветке.
- Нет `TimeExecute` в Routing при `TimeExecution = 0` — ничего не прибавляется.
- Период выключен: используется только `DateFrom`; `DateTo` скрыт.
- Ошибка БД при расчёте: «Ошибка» + `ex.Message`, строка пропускается (легаси местами падал).
- Выгрузка без выбора: только строка заголовка (паритет). Ошибка записи файла — «Ошибка».

## 7. Навигация и поведение окна
Меню «Отчеты → Норма выработки» → `CalculationViewModel` в `ContentControl`; «Закрыть» → welcome.
`DateFrom` по умолчанию 06.07.2019 (значение Designer), `DateTo` — сегодня. Грид только для чтения,
выбор строк множественный (`Extended`, `FullRow`).

## 8. Паритет-чек-лист
- [x] Сценарии легаси выписаны
- [x] Формулы/округления сверены с `calculationForm.vb`
- [x] SQL и таблицы сверены с `.xsd`/репозиториями
- [x] Сборка VS18 MSBuild Debug: 0 ошибок/предупреждений
- [x] VM-харнесс 129/129 на копии БД (stub + реальные запросы)
- [x] UIA: меню, 101 строка, дата 12.02.2026 → 42,25 / 123,97 / 595,04,
      период/видимость `По`, «Закрыть» → welcome
- [x] UIA-экспорт: SaveFileDialog → CSV UTF-8 BOM, 8 колонок,
      `29;…;3485;2;5;42,25;123,97;595,04`, сообщение «Файл … сохранен.»

## 9. Отклонения (ADR-028)
- Исправлен фильтр одного дня в швейной ЗП: легаси `DateExecution >= @DateFrom` заменён на `=`.
- CSV: убрана потеря последнего символа последнего значения (легаси `Substring(0, Len-2)`);
  диалог сохранения по умолчанию предлагает CSV (`FilterIndex = 2`), сохранение проверено
  через реальный SaveFileDialog.
- Грид read-only (ручные правки легаси в БД не сохранялись); ошибки БД по строке
  не прерывают расчёт остальных выбранных, тексты сообщений легаси сохранены.
