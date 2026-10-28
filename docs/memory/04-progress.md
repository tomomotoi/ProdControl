# 04 · Прогресс

Статусы: `не начато` · `в работе` · `готово` · `пропущено` (пустая/устаревшая форма).

## Текущий фокус
M1: `newArticulForm` портирован (код готов, ждёт сборки и проверки на Windows);
`OperationForm` готов (сборка + проверка на копии БД).
M3: `KnittingRoutingForm`, `NewKnittingInvoiceForm` и `KnittingSearchInvoiceForm` готовы (сборка + проверка на копии БД).
`RoutingForm` готов (сборка 0/0, VM-харнесс и полный UIA-сценарий на копии БД).
`TariffsCheckForm` готов (сборка 0/0, VM-харнесс 59/59, полный UIA-сценарий; ADR-023).
`EmployeesForm` готов (сборка 0/0, VM-харнесс 67/67, UIA-сценарий; ADR-024).
`CoefficientsForm` готов (сборка 0/0, VM-харнесс 48/48, UIA-сценарий; ADR-025).
`DetailsForm` готов (сборка 0/0, VM-харнесс 60/60, UIA-сценарий; ADR-026).
`MachinesForm` готов (сборка 0/0, VM-харнесс 100/100, UIA-сценарий; ADR-027).
M4: `calculationForm` готов (сборка 0/0, VM-харнесс 129/129, UIA + экспорт; ADR-028).
Следующий шаг — закрыть M2: прогнать `SearchBillForm` и `newBill` на копии БД (сборка проходит).
Осталось по M1: проверить `newArticulForm` на Windows.

## mainForm — состояние
Сделано:
- M0: `ObservableObject`/`RelayCommand`, `IDbConnectionFactory`, диалоги, навигация, стили, композиция в `App`.
- Оболочка: меню 1:1 с легаси, статус-бар, экраны-заглушки для непортированных форм.
- Утилиты: копия БД в `Архив`, восстановление из файла/последней копии, очистка архива, очистка `OperationLog` с предварительным backup.
- Авто-копия 1-го числа и `CurrentDay.bin` (совместим с легаси-форматом).
- Сборка VS18 MSBuild: 0 ошибок/предупреждений; smoke-запуск успешен.

Осталось:
- Вручную проверить backup/restore/clear на копии БД.
- Проверить клики по меню и навигацию на Windows.
- По мере порта форм заменить заглушки реальными маршрутами.

## newBill — состояние
Сделано:
- Карточка `features/newbill.md` (98 строк), сверена с `newBill.vb`.
- `Models/NewBillModels`, `Domain/BillRules` (лимит 480, расчёт простоя, VB-округление `CInt`).
- `Data/INewBillRepository` + `NewBillRepository`: справочники, операции по Routing, процент простоя, пакетный INSERT в `OperationLog` в транзакции (ADR-014).
- `ViewModels/NewBillViewModel`: режимы, умолчания, фильтр операций, вставка/удаление, расчёт, сохранение, сообщения.
- `Views/NewBillView` + code-behind: numeric-ввод, фокус workday; маршрут меню «Новая (Швейный цех)» заменён на реальный экран.

Проверено:
- Сборка VS18 MSBuild (`Debug`): 0 ошибок, 0 предупреждений.
- Запуск приложения и переход меню «Новая (Швейный цех)» → форма загружается (справочники читаются, ошибок нет).

Осталось:
- Проверить сценарии: техкарта/без техкарты, оба режима, расчёт простоя, сохранение и очистка грида, ограничение 480.
- Подтвердить округление `CInt` на дробном времени.
- После проверки: перенести `OperationDB.mdb` в `ProdControlWPF/Database/`, переключить `csproj`, включить `.mdb` в git (ADR-015).

## SearchBillForm — состояние
Сделано:
- Карточка `features/searchbill.md` (100 строк), сверена с `SearchBillForm.vb`.
- `Models/SearchBillModels`, `Data/ISearchBillRepository` + `SearchBillRepository`: справочники, 4 режима фильтра, период, удаление/обновление в транзакции (ADR-016).
- `ViewModels/SearchBillViewModel`: фильтры, видимость полей, накопление правок/удалений, валидация int, сообщения.
- `Views/SearchBillView` + code-behind, маршрут меню «Найти (Швейный цех)» заменён на реальный экран.

Осталось:
- Собрать на Windows (ожидается 0 ошибок).
- Проверить сценарии: все 4 фильтра, период, «Найти все», edit-mode, удаление/сохранение, ошибки int, пустой результат.
- Сверить с легаси отклонения из ADR-016 (edit-mode и начальное состояние кнопок).

## RoutingForm — состояние
Сделано:
- Карточка `features/routing.md` (99 строк), сверена с `RoutingForm.vb` (1–463) и Designer (контролы/колонки).
- `Models/RoutingModels`, `Domain/RoutingRules` (время `Round(коэф.×норма, 2)`, расценка без округления,
  парсинг чисел с `.`/`,` под ru-RU).
- `Data/IRoutingRepository` + `RoutingRepository`: артикулы, операции, коэффициенты (ORDER BY value),
  SELECT Routing по артикулу (ORDER BY OperationID), ставка `PositionLevel`, INSERT/UPDATE в транзакции,
  `RoutingID` через `@@IDENTITY` (ADR-021).
- `ViewModels/RoutingViewModel`: загрузка, поиск, пересчёт отсутствующих расценок, добавление/дубликат,
  удаление только новых строк, dirty-строки, сохранение, счётчик, сообщения легаси.
- `Views/RoutingView` + code-behind (фильтр чисел/разряда); маршрут меню «Основные данные → Тех.карта»
  заменён на реальный экран; ADR-021.

Проверено 2026-10-22:
- MSBuild VS18 Debug: 0 ошибок, 0 предупреждений.
- Репозиторий на копии БД: 2677 артикулов, 151 операция, 18 коэффициентов; 63 строки Routing
  для первого артикула; ставка `PositionLevel` читается; INSERT (RoutingID=32397) и UPDATE в транзакции
  с последующей проверкой значения; временная копия БД удалена.
- UIA smoke: меню «Основные данные → Тех.карта» открывает форму (4 combo, 5 кнопок, 1 грид, 3 поля ввода);
  «Найти» для артикула по умолчанию загружает 63 строки и счётчик «Количество записей: 63».
- VM-харнесс на копии БД: 70 проверок, 0 ошибок; добавление/дубликат Yes/No, удаление новой/загруженной,
  INSERT/UPDATE/`@@IDENTITY`, dirty, пустой поиск, отсутствие ставки в шапке/строке, форматы чисел,
  ошибка БД при сохранении, `Close`.
- UIA полный сценарий: выбор артикула/операции/коэффициента/разряда, расчёт 2,08/0,059072,
  «Добавить»→1, «Удалить»→0, «Добавить»→«Сохранить»→«Найти»→64 строки, счётчик 64.

Осталось:
- Перенос `OperationDB.mdb` по ADR-015 общий для проекта.

## TariffsCheckForm — состояние
Сделано:
- Карточка `features/tariffscheck.md`, сверена с `TariffsCheckForm.vb` (187 строк) и Designer.
- `Models/TariffsCheckModels`, `Domain/TariffsCheckRules` (`TimeExecute × PositionRateTime`,
  паритет с TableAdapter `Tariff As Decimal`).
- `Data/ITariffsCheckRepository` + `TariffsCheckRepository`: один SELECT нулевых строк с join
  `Operation`/`PositionLevel`, запись тем же UPDATE по `ArticulID`+`OperationID` в транзакции (ADR-023).
- `ViewModels/TariffsCheckViewModel`: загрузка, выбор/добавление/удаление, «Выбрать все»,
  «Очистить список», расчёт, сообщения и подтверждения легаси, паритет выбора WinForms DataSource.
- `Views/TariffsCheckView` + маршрут меню «Основные данные → Проверка расценок работ».

Проверено 2026-10-22:
- MSBuild VS18 Debug: 0 ошибок, 0 предупреждений.
- VM-харнесс на копии БД: 59 проверок, 0 ошибок (выбор/удаление/select-all/clear, подмножество,
  all + Cancel, нет кандидатов, ошибки load/select/save, decimal-паритет, дубли art+op, скорость).
- UIA полный сценарий: пустой список → ошибка; выбор артикула, добавление/удаление; расчёт →
  «Идет проверка…» → успех, в БД `0,075184`; «Выбрать все» (подтверждение Cancel) и очистка.
  UIA видит 14 реализованных элементов при виртуализации; полные 2677 проверены в VM-харнессе.
- Производительность: `GetMissingTariffs` ≈62 мс против ≈17,3 с у легаси-перебора (2677 SELECT).

Осталось:
- Перенос `OperationDB.mdb` по ADR-015 общий для проекта.

## KnittingRoutingForm — состояние
Сделано:
- Карточка `features/knittingrouting.md` (99 строк), сверена с `KnittingRoutingForm.vb` (1–515) и Designer.
- `Models/KnittingRoutingModels`, `Domain/KnittingRoutingRules` (F2/F3/F1, 480/итог, парсинг `,`/`.`,
  коэффициенты/М/П по умолчанию 1).
- `Data/IKnittingRoutingRepository` + `KnittingRoutingRepository`: 4 справочника, SELECT карты
  по `articul_id`, DELETE+INSERT в одной транзакции; ID детали/станка из строк (ADR-022).
- `ViewModels/KnittingRoutingViewModel`: загрузка, поиск/итог/норма, пересчёт, добавление,
  удаление с подтверждением, сохранение (включая удаление пустой загруженной карты), сообщения.
- `Views/KnittingRoutingView` + code-behind (фильтр десятичного ввода); маршрут меню
  «Основные данные → Тех.карта (Вязание)» заменён на реальный экран; ADR-022.

Проверено 2026-10-22:
- MSBuild VS18 Debug: 0 ошибок, 0 предупреждений.
- VM-харнесс на копии БД: 31 проверка, 0 ошибок (пустой поиск, формулы, seed/search,
  форматы F1/F2/F3, итог/норма, добавление/удаление, смена артикула, удаление всей карты,
  пустое сохранение, сохранение без артикула, add+save, очистка тестовых строк).
- UIA smoke: меню «Основные данные → Тех.карта (Вязание)» открывает форму; 4 кнопки,
  1 грид с 9 колонками, 7 combo, 5 полей ввода.

Осталось:
- Полный UI click-through (ручной набор в «Средн. зн.», клики «Добавить/Сохранить»).
- Перенос `OperationDB.mdb` по ADR-015 общий для проекта.

## NewKnittingInvoiceForm — состояние
Сделано:
- Карточка `features/newknittinginvoice.md`, сверена с `NewKnittingInvoiceForm.vb` (1–175).
- `Models/KnittingInvoiceModels`, `Data/IKnittingInvoiceRepository` + `KnittingInvoiceRepository` (4 справочника, INSERT в транзакции).
- `ViewModels/NewKnittingInvoiceViewModel`: загрузка, выбор по умолчанию, вставка/удаление строк, сохранение, сообщения, нормализация десятичного разделителя.
- `Views/NewKnittingInvoiceView` + code-behind (целые, время ≤2 знаков, вставка из буфера); маршрут меню «Новая (Вязальный цех)» заменён на реальный экран.

Проверено 2026-10-22:
- Схема `KnittingOperationLog` в Access: колонки/типы совпадают; SELECT-запросы справочников и INSERT прогнаны с rollback.
- MSBuild VS18 Debug: 0 ошибок, 0 предупреждений.
- VM-харнесс на копии БД: 101 сотрудник, 2677 артикулов, 12 деталей, 4 машины; вставка/удаление, сохранение и очистка, пустая таблица, обязательные поля, время/машины = 0, дата без времени. Тестовые строки удалены.
- UIA smoke: меню открывает форму, рендерятся 4 combo, 10 колонок, 6 полей ввода и 3 кнопки.

Осталось:
- Ручной ввод в UI (фильтр цифр и 2 знаков после запятой) — код реализован, вручную не набирался.
- Перенос `OperationDB.mdb` по ADR-015 общий для проекта.

## newArticulForm — состояние
Сделано:
- Карточка `features/newarticul.md` (проверена по `newArticulForm.vb` и Designer).
- `Data/INewArticulRepository` + `NewArticulRepository`: одиночный параметризованный INSERT в `Articul`
  (`ArticulName`, `ArticulText`).
- `ViewModels/NewArticulViewModel`: поля, `SaveCommand`, `CloseCommand`, сообщение об успехе,
  обработка ошибок БД; без валидации и очистки полей (паритет с легаси).
- `Views/NewArticulView` + code-behind; маршрут меню «Основные данные → Артикул» заменён на реальный экран;
  `DataTemplate` в `App.xaml`. ADR-019.

Проверено:
- XML/XAML и `csproj` синтаксически корректны (парсинг); сверка кода с `newArticulForm.vb`/Designer.
- Сборка недоступна в текущем окружении: WSL/Linux, MSBuild/VS не найдены.

Осталось:
- Собрать на Windows (ожидается 0 ошибок).
- Проверить сценарий: INSERT с названием/описанием, пустые поля, дубликат, сообщение, «Закрыть» → приветствие.
- Сверить с легаси отклонения из ADR-019.

## OperationForm — состояние
Сделано:
- Карточка `features/operation.md` (83 строки), сверена с `OperationForm.vb` (1–167) и Designer.
- `Models/OperationModels`, `Data/IOperationRepository` + `OperationRepository`: `Operation`, `Position`,
  проверка дубликата через `LIKE`, INSERT с нулями, проверка использования в `Routing`, DELETE; всё параметризовано.
- `ViewModels/OperationViewModel`: загрузка, выбор операции/профессии, поиск по префиксу, добавление/удаление, сообщения.
- `Views/OperationView` + code-behind; маршрут меню «Основные данные → Операция» заменён на реальный экран; ADR-020.

Проверено 2026-10-22:
- MSBuild VS18 Debug: 0 ошибок, 0 предупреждений.
- Репозиторий на копии БД: 151 операция, 11 профессий; INSERT/DELETE, поля `0,0,0`, `OperationMachin = NULL`,
  проверка использования в `Routing`, тестовая строка удалена.
- UIA: меню открывает форму, список и combo загружены, 3 поля ввода и 2 кнопки; добавление через UI → сообщение
  «Уведомление» и строка в БД; поиск выставляет операцию; удаление через UI → строка удалена без ошибок.

Осталось:
- Вручную проверить сообщения: пустое имя, дубликат, операция из `Routing`, удаление без выбора.
- Перенос `OperationDB.mdb` по ADR-015 общий для проекта.

## EmployeesForm — состояние
Сделано:
- Карточка `features/employees.md` (90 строк), сверена с `EmployeesForm.vb` (1–275) и Designer (24–374).
- `Models/EmployeesModels`, `Data/IEmployeesRepository` + `EmployeesRepository`: должности, список,
  4 фильтра, INSERT/UPDATE/DELETE в одной транзакции (ADR-024).
- `ViewModels/EmployeesViewModel`: фильтры, edit-mode, добавление/удаление, dirty/update, валидация,
  сообщения, сброс несохранённых изменений при поиске.
- `Views/EmployeesView` + code-behind; маршрут «Основные данные → Сотрудники», `DataTemplate` в `App.xaml`.

Проверено 2026-10-23:
- MSBuild VS18 Debug: 0 ошибок, 0 предупреждений.
- Репозиторий на копии БД: 101 сотрудник, 11 должностей; INSERT/UPDATE/DELETE, итоговое число 101.
- VM-харнесс на копии БД с реальным репозиторием: 67 проверок, 0 ошибок (фильтры, edit-mode,
  add/save/update/delete, валидация, сброс pending, ошибки load/save).
- UIA-сценарий: пустой грид → «Найти всех» 101, ФИО 2, должность 53, разряд 5 → 82,
  неверный разряд → диалог; add/delete, сброс pending, «Нет изменений для сохранения!»; `UI_SMOKE_OK`.

Осталось:
- Перенос `OperationDB.mdb` по ADR-015 общий для проекта.

## CoefficientsForm — состояние
Сделано:
- Карточка `features/coefficients.md` (проверена по `CoefficientsForm.vb` 1–69 и Designer 24–166).
- `Models/CoefficientsModels`, `Data/ICoefficientsRepository` + `CoefficientsRepository`:
  список по возрастанию, проверка дубликата, INSERT только `coefficient_value`,
  DELETE по `coefficient_id` + `coefficient_value`; всё параметризовано.
- `ViewModels/CoefficientsViewModel`: загрузка, режим редактирования, парсинг ru-RU
  (`IsNumeric`/`Double.Parse`), дубликат, сообщения легаси, безопасные ошибки БД.
- `Views/CoefficientsView` + code-behind; маршрут «Основные данные → Коэффициенты доп. затрат»;
  `DataTemplate` в `App.xaml`; UIA-имя элементов списка = значение.

Проверено 2026-10-23:
- MSBuild VS18 Rebuild Debug: 0 ошибок, 0 предупреждений.
- Репозиторий на копии БД: 18 коэффициентов, сортировка, exists, INSERT/DELETE,
  защита DELETE по значению, `date_added` не задаётся приложением; тестовая строка удалена.
- VM-харнесс на копии БД: 48 проверок, 0 ошибок (режим редактирования, `,`/пробелы/экспонента,
  отказ `1.5`, дубликат, add/delete, сохранение текста, 4 ошибки БД через stub).
- UIA-сценарий: меню открывает форму (18 строк, видимых 13 при виртуализации), кнопки disabled,
  checkbox, add `1,05`, дубликат, `abc`, удаление, повторный read-only; `UI_SMOKE_OK`.

Осталось:
- Перенос `OperationDB.mdb` по ADR-015 общий для проекта.

## DetailsForm — состояние
Сделано:
- Карточка `features/details.md`, сверена с `DetailsForm.vb` (1–150) и Designer (24–152).
- `Models/DetailsModels`, `Data/IDetailsRepository` + `DetailsRepository`: список по `id DESC`,
  INSERT/UPDATE/DELETE по `id`; всё параметризовано, без транзакций; `DBNull` → пустая строка.
- `ViewModels/DetailsViewModel`: загрузка, выбор строки → имя, add/save/delete/clear,
  валидация пустого имени, подтверждение удаления, сообщения и префиксы ошибок легаси.
- `Views/DetailsView` + code-behind; маршрут «Основные данные → Детали изделий» заменён
  на реальный экран; `DataTemplate` в `App.xaml`.

Проверено 2026-10-23:
- MSBuild VS18 Debug (`slnx`): 0 ошибок, 0 предупреждений.
- VM-харнесс на копии БД: 60 проверок, 0 ошибок (stub: выбор/очистка, пустое имя, trim,
  ошибки load/add/save/delete, cancel подтверждения, `null`-имя; реальный репозиторий:
  12 строк, сортировка, INSERT/UPDATE/DELETE, Memo-имя 400 символов, очистка тестовых строк).
- UIA-сценарий: меню открывает форму (12 строк, одно поле ввода, 4 кнопки); пустое имя,
  save/delete без выбора → предупреждения; выбор строки заполняет поле; добавление с пробелами
  (trim), сохранение, очистка; удаление с отменой и подтверждением; счётчик вернулся к 12.
  `UI_DETAILS_OK`; тестовая строка в БД удалена.

Осталось:
- Перенос `OperationDB.mdb` по ADR-015 общий для проекта.

## MachinesForm — состояние
Сделано:
- Карточка `features/machines.md`, сверена с `MachinesForm.vb` (1–150) и Designer (24–196).
- `Models/MachinesModels`, `Data/IMachinesRepository` + `MachinesRepository`: список по `id DESC`,
  INSERT/UPDATE/DELETE по `id`; всё параметризовано, без транзакций; `DBNull` → пустая строка.
- `ViewModels/MachinesViewModel`: загрузка, выбор строки → ID/марка/модель, add/save/delete/clear,
  валидация, подтверждение удаления, сообщения и префиксы ошибок легаси; reload после записи
  с отдельной ошибкой «Ошибка загрузки данных: » (паритет с `LoadData`).
- `Views/MachinesView` + code-behind; маршрут «Основные данные → Оборудование» заменён
  на реальный экран; `DataTemplate` в `App.xaml`; ADR-027.

Проверено 2026-10-23:
- MSBuild VS18 Debug (`slnx`): 0 ошибок, 0 предупреждений.
- VM-харнесс на копии БД: 100 проверок, 0 ошибок (stub: выбор/очистка, все валидации,
  ошибки load/write/reload, cancel подтверждения, `null`; реальный репозиторий: 4 строки,
  сортировка, INSERT/UPDATE/DELETE, NULL-строка, VM add/save/delete, очистка тестовых строк).
- UIA-сценарий: меню открывает форму (4 строки, 3 поля ввода, 4 кнопки); пустые add/save/delete
  → предупреждения; выбор строки заполняет ID/марку/модель; read-only ID; add с пробелами (trim),
  save, clear; delete cancel/yes; счётчик вернулся к 4. `UI_MACHINES_OK`; тестовые строки удалены.

Осталось:
- Перенос `OperationDB.mdb` по ADR-015 общий для проекта.

## KnittingSearchInvoiceForm — состояние
Сделано:
- Карточка `features/knittingsearchinvoice.md` (100 строк), сверена с `KnittingSearchInvoiceForm.vb` (1–557).
- `Models/KnittingSearchModels`, `Data/IKnittingSearchRepository` + `KnittingSearchRepository`: 4 справочника,
  4 режима фильтра, период, сортировка, удаление, пакетный UPDATE в транзакции.
- `ViewModels/KnittingSearchInvoiceViewModel`: загрузка, видимость фильтров, поиск/«Найти все», dirty-строки,
  удаление с подтверждением, сохранение, сообщения.
- `Views/KnittingSearchInvoiceView` + code-behind (дата через DatePicker, фильтр цифр/разделителя);
  маршрут меню «Найти (Вязальный цех)» заменён на реальный экран; `DataTemplate` в `App.xaml`. ADR-018.

Проверено 2026-10-22:
- MSBuild VS18 Debug: 0 ошибок, 0 предупреждений.
- Репозиторий на копии БД: 4 справочника (101/2677/12/4), все фильтры, период, сортировка,
  пустой результат, UPDATE с восстановлением значения, DELETE синтетической строки.
- VM-харнесс: начальное read-only, поиск/«Найти все», edit-mode, сохранение и сброс dirty,
  «нет изменений», удаление и отмена подтверждения, сообщения.
- UIA smoke: меню открывает форму, 1 DataGrid с 11 колонками, 2 combo, 4 кнопки,
  «Удалить»/«Сохранить» disabled вне edit-mode.

Осталось:
- Ручной набор в ячейки (фильтр цифр/разделителя) через клавиатуру не проверялся.
- Сверить с легаси отклонения из ADR-018.

## calculationForm — состояние
Сделано:
- Карточка `features/calculation.md` (92 строки), сверена с `calculationForm.vb` (1–466) и Designer.
- `Models/CalculationModels`, `Domain/CalculationRules` (вязание/швейный расчёт, рабочий день,
  VB-округление 2/6, форматы ru-RU).
- `Data/ICalculationRepository` + `CalculationRepository`: справочник сотрудников, должность,
  зоны вязания, швейная ЗП/время, ставка `PositionRateTime`, время из `Routing`, рабочие дни; только чтение.
- `ViewModels/CalculationViewModel`: загрузка, выбор строк, период, расчёт, CSV-выгрузка, сообщения, «Закрыть».
- `Views/CalculationView` + code-behind; маршрут меню «Отчеты → Норма выработки» заменён на реальный экран; ADR-028.

Проверено 2026-10-26:
- MSBuild VS18 Debug: 0 ошибок, 0 предупреждений.
- VM-харнесс на копии БД: 129 проверок, 0 ошибок (stub-ошибки/форматы/экспорт; реальные SQL:
  employee 29 за 12.02.2026 — 42,25 / 123,97 / 595,04, период 09–13.02; employee 43 с `ArticulID=74`;
  синтетические зоны вязания — 95,00 / 95,00 / 19,79; CSV с BOM и полными строками).
- UIA-сценарий: меню «Отчеты → Норма выработки», 101 строка и 8 колонок, выбор employee 29,
  дата 12.02.2026 → 42,25 / 123,97 / 595,04, видимость `По` по checkbox, «Закрыть» → welcome.
  `UI_CALCULATION_OK`.
- UIA-экспорт через реальный SaveFileDialog: CSV UTF-8 BOM, полный заголовок 8 колонок,
  строка `29;Юрчик Галина Владимировна;3485;2;5;42,25;123,97;595,04`, сообщение
  «Файл … сохранен.», файл удалён после проверки. `UI_CALCULATION_EXPORT_OK`.

Осталось:
- Ничего по форме: UI-сценарий и экспорт через SaveFileDialog проверены.

## Вехи
- [x] **M0. Каркас**: структура папок, `ObservableObject`/`RelayCommand`, `IDbConnectionFactory`, shell, стили, навигация.
- [ ] **M1. Справочники**: Articul, Operation, Employees, Coefficients, Details, Machines.
- [ ] **M2. Швейный цех**: RoutingForm ✅, TariffsCheckForm ✅, newBill, SearchBillForm.
- [ ] **M3. Вязание**: KnittingRoutingForm, NewKnittingInvoiceForm, KnittingSearchInvoiceForm.
- [x] **M4. Расчёты**: calculationForm (швейный + вязальный, CSV) ✅.
- [ ] **M5. Отчёты**: ReportViewer (RDLC).
- [ ] **M6. Утилиты**: backup/restore/clear OperationLog, `CurrentDay.bin` — код в `mainForm`, ждёт ручной проверки.

## Порядок работ
1. M0 → 2. M1 → 3. M2 → 4. M3 → 5. M4 → 6. M5 → 7. M6.
Внутри M1 начинать с простых CRUD (`DetailsForm`, `MachinesForm`), затем `EmployeesForm`, `OperationForm`, `newArticulForm`, `CoefficientsForm`.

## Формы
| Форма | Статус | Карточка |
|---|---|---|
| mainForm (shell) | в работе | `features/mainform-shell.md` |
| newBill | в работе (собран, экран открывается) | `features/newbill.md` |
| SearchBillForm | в работе (код готов, сборка/проверка на Windows) | `features/searchbill.md` |
| newBillWiz | пропущено | пустая |
| CompletedOrders | пропущено | пустая |
| calculationForm | готово (сборка 0/0, VM 129/129, UIA + экспорт) | `features/calculation.md` |
| RoutingForm | готово (собран, VM/UIA-сценарий проверен) | `features/routing.md` |
| KnittingRoutingForm | готово (собран; VM-харнесс 31/31, UIA smoke) | `features/knittingrouting.md` |
| newArticulForm | в работе (код готов, сборка/проверка на Windows) | `features/newarticul.md` |
| OperationForm | готово (собран, сценарий проверен) | `features/operation.md` |
| TariffsCheckForm | готово (собран, VM 59/59, UIA-сценарий) | `features/tariffscheck.md` |
| EmployeesForm | готово (собран, VM 67/67, UIA-сценарий) | `features/employees.md` |
| CoefficientsForm | готово (собран, VM 48/48, UIA-сценарий) | `features/coefficients.md` |
| DetailsForm | готово (собран, VM 60/60, UIA-сценарий) | `features/details.md` |
| MachinesForm | готово (собран, VM 100/100, UIA-сценарий) | `features/machines.md` |
| NewKnittingInvoiceForm | готово (собран, сценарий проверен) | `features/newknittinginvoice.md` |
| KnittingSearchInvoiceForm | готово (собран, сценарий проверен) | `features/knittingsearchinvoice.md` |
| ReportViewer | не начато | — |

## Журнал
| Дата | Изменение |
|---|---|
| 2026-10-21 | Создана память проекта: AGENTS.md, индекс, карта легаси, целевая архитектура, плейбук, прогресс, ADR, шаблон карточки. |
| 2026-10-21 | Реализованы M0 (MVVM-lite, OleDb factory, диалоги, навигация, стили) и оболочка `mainForm`: меню, заглушки разделов, backup/restore/clear, `CurrentDay.bin`. |
| 2026-10-21 | Сборка VS18 MSBuild: 0 ошибок/предупреждений; smoke-запуск — окно «Главное окно» без исключений. `CurrentDay.bin` создаётся. |
| 2026-10-21 | Портирован `newBill`: карточка, модели, правила, репозиторий, VM, View, маршрут меню; сохранение в транзакции (ADR-014). |
| 2026-10-21 | Сборка VS18 MSBuild: 0 ошибок/предупреждений; запуск успешен, форма `newBill` открывается из меню. |
| 2026-10-21 | Портирован `SearchBillForm`: карточка, модели, репозиторий, VM, View, маршрут меню, ADR-016. Сборка на Windows ещё не выполнялась. |
| 2026-10-22 | Портирован `NewKnittingInvoiceForm`: карточка, модели, репозиторий, VM, View, маршрут меню; ADR-017. Сборка 0/0, схема БД и сценарий проверены на копии, UIA smoke успешен. |
| 2026-10-22 | Портирован `KnittingSearchInvoiceForm`: карточка, модели, репозиторий, VM, View, маршрут меню; ADR-018. Сборка 0/0, репозиторий и VM проверены на копии БД, UIA smoke успешен. |
| 2026-10-22 | Портирован `newArticulForm`: карточка `features/newarticul.md`, репозиторий, VM, View, маршрут меню; ADR-019. Сборка на Windows и проверка сценария ещё не выполнены. |
| 2026-10-22 | Портирован `OperationForm`: карточка, модели, репозиторий, VM, View, маршрут меню; ADR-020. Сборка 0/0, репозиторий и сценарий добавления/поиска/удаления проверены на копии БД через UI. |
| 2026-10-22 | Портирован `RoutingForm`: карточка `features/routing.md`, модели, правила, репозиторий, VM, View, маршрут меню; ADR-021. Сборка VS18 0/0; репозиторий проверен на копии БД; UIA: открытие формы и «Найти» (63 строки). |
| 2026-10-22 | `RoutingForm` завершён: VM-харнесс 70/70 и полный UIA-сценарий (добавление/удаление/сохранение/поиск 64) на копии БД; ADR-021. |
| 2026-10-22 | Портирован `KnittingRoutingForm`: карточка `features/knittingrouting.md`, модели, правила, репозиторий, VM, View, маршрут меню; ADR-022. Сборка VS18 0/0, VM-харнесс 31/31 на копии БД, UIA smoke успешен. |
| 2026-10-22 | Портирован `TariffsCheckForm`: карточка `features/tariffscheck.md`, модели, правила, репозиторий, VM, View, маршрут меню; ADR-023. Сборка VS18 0/0, VM-харнесс 59/59, полный UIA-сценарий; проверка ускорена с ≈17,3 с до ≈62 мс. |
| 2026-10-23 | Портирован `EmployeesForm`: карточка `features/employees.md`, модели, репозиторий, VM, View, маршрут меню; ADR-024. Сборка VS18 0/0, репозиторий и VM-харнесс 67/67 на копии БД, UIA-сценарий (фильтры, edit-mode, сообщения, сброс pending) успешен. |
| 2026-10-23 | Портирован `CoefficientsForm`: карточка `features/coefficients.md`, модель, репозиторий, VM, View, маршрут меню; ADR-025. Сборка VS18 Rebuild 0/0, репозиторий и VM-харнесс 48/48 на копии БД, UIA-сценарий (checkbox, add/дубликат/ошибка/удаление) успешен. |
| 2026-10-23 | Портирован `DetailsForm`: карточка `features/details.md`, модель, репозиторий, VM, View, маршрут меню; ADR-026. Сборка VS18 0/0, VM-харнесс 60/60 на копии БД, полный UIA-сценарий (выбор, add/save/clear, delete cancel/yes) успешен. |
| 2026-10-23 | Портирован `MachinesForm`: карточка `features/machines.md`, модель, репозиторий, VM, View, маршрут меню; ADR-027. Сборка VS18 0/0, VM-харнесс 100/100 на копии БД, полный UIA-сценарий (выбор, add/save/clear, delete cancel/yes, read-only ID) успешен. |
| 2026-10-26 | Завершён `calculationForm` (M4): проверены формулы, SQL и CSV; сборка VS18 0/0, VM-харнесс 129/129 на копии БД, UIA-сценарий (расчёт employee 29, период, закрытие) и экспорт через SaveFileDialog (CSV/BOM/строки); ADR-028. |
