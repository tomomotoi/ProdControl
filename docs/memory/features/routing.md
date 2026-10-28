# Feature: RoutingForm (Тех. карта, швейный цех)

- Легаси: `RoutingForm.vb` (1–463), Designer (контролы/колонки).
- Цель: `Views/RoutingView` / `ViewModels/RoutingViewModel`.
- Статус: готово (собран; VM-харнесс 70 проверок + полный UIA-сценарий).
- Зависимости: `Routing`, `Articul`, `Operation`, `Coefficients`, `PositionLevel`, `Position`.

## 1. Назначение и сценарии
Тех. карта артикула: выбрать артикул → «Найти» → список операций из `Routing`
(сортировка по `OperationID`) → добавить/удалить новые строки, поправить значения,
пересчитать отсутствующие расценки и сохранить. Форма также считает время с коэффициентом
и расценку для добавляемой строки в шапке.

## 2. UI (из Designer)
| Контрол | Тип | Назначение |
|---|---|---|
| `cbArt` | ComboBox | артикул (`ArticulName`/`ArticulID`) |
| `btnSeachArt` | Button | «Найти» — загрузка тех. карты |
| `cbOperation` | ComboBox | операция (`OperationName`/`OperationID`) |
| `cbDopCost` | ComboBox | коэффициент из `Coefficients`, без выбора при старте |
| `txRate` | TextBox | «Норма времени» (ввод) |
| `txExecTime` | TextBox | «Время выполнения» = коэф. × норма, read-only |
| `cbLevel` | ComboBox | разряд: `""`, `1`…`5`, без выбора при старте |
| `txCost` | TextBox | «Расценка» = время × `PositionRateTime`, read-only |
| `dgView` | DataGridView | колонки: RoutingID(hidden), ArticulID(hidden), Операция(RO), Коэф., Время, Норма, Разряд, Расценка, isNew/isChanged(hidden) |
| `lbRowsAmount` | Label | «Количество записей: N» |
| `btnAdd/btnDel/btSave/btCancel` | Button | Добавить/Удалить/Сохранить/Отменить |

## 3. События и команды
| Событие легаси | Действие | Команда/метод VM |
|---|---|---|
| Load | справочники; `cbDopCost.SelectedIndex = -1` | `Load()` |
| `btnSeachArt.Click` | SELECT Routing по артикулу; пересчёт расценок | `SearchCommand` |
| `btnAdd.Click` | проверка полей, дубликата, добавление строки | `AddCommand` |
| `btnDel.Click` | удалить только `isNew`-строку | `DeleteCommand` |
| `btSave.Click` | INSERT новых, UPDATE изменённых | `SaveCommand` |
| `btCancel.Click` | закрыть форму | `CloseCommand` → приветствие |
| `cbDopCost`/`txRate` change | время = `Round(коэф.×норма, 2)` | сеттеры |
| `cbOperation`/`cbLevel`/`txExecTime` change | расценка = время × ставка | сеттеры |
| `dgView.CellValueChanged` col<7 | `isChanged = true` | dirty-флаг строки |

## 4. Данные
- `Articul`: `ArticulID`, `ArticulName`; `Operation`: `OperationID`, `OperationName`, `PositionID`.
- `Coefficients`: `coefficient_id`, `coefficient_value`, `ORDER BY coefficient_value`.
- `Routing`: `RoutingID, ArticulID, OperationID, DopCost, TimeExecute, RateExecute, PositionLevel, Tariff`.
- SELECT по артикулу: `WHERE ArticulID = ? ORDER BY OperationID`.
- INSERT: `(ArticulID, OperationID, DopCost, TimeExecute, RateExecute, PositionLevel, Tariff)`.
- UPDATE: `SET DopCost, TimeExecute, RateExecute, PositionLevel, Tariff WHERE RoutingID = ? AND ArticulID = ?`
  (как TableAdapter легаси; `OperationID`/`ArticulID` не меняются).
- Ставка: `SELECT PositionRateTime FROM PositionLevel INNER JOIN Operation ON PositionLevel.PositionID = Operation.PositionID WHERE Operation.OperationID = ? AND PositionLevel.PositionLevel = ?` (NULL, если нет).
- Сохранение — одной транзакцией; новые `RoutingID` через `@@IDENTITY`.

## 5. Бизнес-правила и формулы
- Время с коэффициентом: `Math.Round(coefficient * rate, 2)` (банковское округление).
- Расценка строки/шапки: `timeWithCoefficient * PositionRateTime`, без округления.
- Если первая строка после «Найти» имеет расценку `0`/пусто и строки есть:
  предупреждение «В базе отсутствовали расценки на все или некоторые операции из списка,
  они будут рассчитаны. Пожалуйста сохраните изменения!»; затем для каждой строки
  расценка пересчитывается и строка помечается изменённой.
- Нет ставки для операции/разряда: «Не удалось получить расценку за минуту для одной из
  операций. Вероятно в таблице PositionLevel отсутствует соответствующий PositionLevel(разряд)
  и/или PositionID(профессия)».
- Шапка: нет ставки → «Результат запроса к БД вернул NULL!…» + очистка «Расценки».
- Неверный формат коэффициента/нормы: «Поле 'Доп. затраты' и/или поле 'Норма времени'
  не соответствуют следующему формату: ###,## или ###.## …».
- Числа в UI — ru-RU; ввод принимает `.` и `,`, сохраняется через текущую культуру.

## 6. Валидация и edge cases
- Добавление: обязательны артикул, операция, коэффициент, время, норма, разряд, расценка;
  иначе «Перед добавлением поля должны быть заполнены!».
- Дубликат операции: подтверждение «Операция уже добавлена в список. Вы уверены, что
  хотите добавить дубликат?» (заголовок «Технологическая Карта»); «Нет» — не добавлять.
- Удаление загруженной строки: «Операция уже используется в тех карте. Удалить можно
  только новую операцию» (заголовок «Master Data»).
- Пустой результат поиска: очистить грид, счётчик `0` (в легаси счётчик оставался старым).
- Нет выбранного артикула при поиске/сохранении: адресное сообщение (в легаси —
  исключение `NullReferenceException`).
- Ошибка БД при сохранении: сообщение с текстом исключения, грид не очищается.
- После сохранения сообщения об успехе нет (как в легаси).
- Отклонения: ручной ввод «Расценки» в гриде помечает строку изменённой и сохраняется
  (в легаси колонка 7 не ставила dirty); после INSERT не выполняется пустой UPDATE;
  разряд выбирается из списка (без произвольного текста); множественное выделение
  заменено одиночным. См. ADR-021.

## 7. Навигация и поведение окна
- Открывается из меню «Основные данные → Тех.карта» (маршрут `RoutingViewModel`).
- «Отменить» возвращает на приветственный экран; отдельного окна нет.
- При открытии `cbDopCost` и `cbLevel` пустые, артикул/операция — первые в списке.

## 8. Паритет-чек-лист
- [x] Открытие формы и поиск по артикулу (UIA: 63 строки).
- [x] Добавление/дубликат, удаление, сохранение, пересчёт нулевых расценок (UIA: добавить→1, удалить→0, сохранить→найти 64).
- [x] Формулы/округления совпадают (харнесс: время 2 знака, расценка без округления).
- [x] Ошибки/пустые состояния обработаны (харнесс: 70 проверок, 0 ошибок).
- [x] Сборка на Windows (VS18 Debug, 0/0); репозиторий проверен на копии БД.

## 9. Открытые вопросы
- Нужен ли автоматический пересчёт расценки при правке «Времени» в гриде (легаси не считал)?
- Оставлять ли `double` или перейти на `decimal` (сначала паритет).
