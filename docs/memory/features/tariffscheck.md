# Feature: TariffsCheckForm

- Легаси: `TariffsCheckForm.vb` (1–188), Designer (контролы/подписи)
- Цель: `Views/TariffsCheckView` / `ViewModels/TariffsCheckViewModel`
- Статус: готово (сборка 0/0; VM 59/59; UIA-сценарий на копии БД)
- Зависимости: `Articul`, `Routing`, `Operation`, `PositionLevel`; `IDialogService`

## 1. Назначение и сценарии
Пользователь выбирает артикулы (один/все) и запускает массовую проверку расценок.
Для каждой строки `Routing` с `Tariff = 0`, у которой есть `Operation` и `PositionLevel`
(`Operation.PositionID` + `Routing.PositionLevel`), расценка пересчитывается и записывается.
Успех: либо сообщение «новые расценки внесены», либо «операций без расценок не найдено».

## 2. UI (из Designer)
| Контрол | Тип | Назначение | Привязка/команда |
|---|---|---|---|
| `cbArticul` | ComboBox | все артикулы, Display=ArticulName, Value=ArticulID | `Articuls`/`SelectedArticul` |
| `btnSelectArticul` | Button | «Выбрать» — добавить выбранный артикул | `SelectArticulCommand` |
| `lbSelectedArticuls` | ListBox | выбранные артикулы, MultiExtended | `SelectedArticuls` |
| `btnRemoveFromList` | Button | «Удалить из списка» (первый выбранный) | `RemoveArticulCommand` |
| `chckbxSelectAll` | CheckBox | «Выбрать все» (заполняет/очищает список) | `SelectAll` |
| `btnClearList` | Button | «Очистить список» + снять чекбокс | `ClearListCommand` |
| `btnCheckTariffs` | Button | «Проверить расценки» | `CheckTariffsCommand` |

## 3. События и команды
| Событие легаси | Действие | Команда/метод VM |
|---|---|---|
| Load | загрузка `Articul` | конструктор VM → `Load` |
| btnSelectArticul.Click | добавить, если такого ID ещё нет | `SelectArticul` |
| btnRemoveFromList.Click | удалить выбранный (нет выбора — no-op) | `RemoveArticul` |
| chckbxSelectAll.CheckedChanged | всегда очистить; если Checked — добавить все | `SelectAll` setter |
| btnClearList.Click | очистить; если Checked — снять | `ClearList` |
| btnCheckTariffs.Click | подтверждение (all) → info → расчёт; пусто → ошибка | `CheckTariffs` |

## 4. Данные
- `Articul`: `SELECT ArticulID, ArticulName FROM Articul`.
- Кандидаты (одним запросом, ADR-023):
  `SELECT Routing.RoutingID, Routing.ArticulID, Routing.OperationID, Routing.TimeExecute, PositionLevel.PositionRateTime`
  `FROM (Routing INNER JOIN Operation ON Routing.OperationID = Operation.OperationID)`
  `INNER JOIN PositionLevel ON Routing.PositionLevel = PositionLevel.[PositionLevel]`
  `AND Operation.PositionID = PositionLevel.PositionID`
  `WHERE Routing.Tariff = 0 ORDER BY Routing.ArticulID, Routing.RoutingID`.
- Фильтрация по выбранным ID — в памяти (артикул 0 из `Routing` не входит в `Articul` и не проверяется, как в легаси).
- Запись — легаси-запрос `UPDATE Routing SET Tariff = ? WHERE ArticulID = ? AND OperationID = ?`
  (обновляет все строки пары артикул+операция); всё в одной OleDb-транзакции.

## 5. Бизнес-правила и формулы
- Только `Tariff = 0`; NULL не встречается, трактуется как «не 0» (паритет с VB-сравнением).
- `tariff = TimeExecute * PositionRateTime` (double).
- Паритет с `TableAdapter.Update(Tariff As Decimal)`: перед записью `(double)(decimal)tariff`
  (VB `CDec` округляет до 15 значащих цифр).
- `emptyFound = true` при любой обработанной строке, даже если результат 0.
- Культура ru-RU на UI; в БД — double.

## 6. Валидация и edge cases
- Список пуст → «Не было выбрано ни одного артикула для проверки расценок операций!» / «Ошибка».
- «Выбрать все» + список не пуст → подтверждение OK/Cancel; Cancel — выход.
- Нет кандидатов → info «Операций без расценок … не было найдено.».
- Ошибка БД/преобразования → `ex.Message` / «Ошибка» (текст легаси, без префикса), транзакция откатывается.
- Нет выбранного артикула в ComboBox → no-op (в легаси создавался пустой элемент).
- Повторное добавление по ID игнорируется.
- Паритет с WinForms DataSource: при загрузке выбирается первый артикул; после добавления/перезаполнения
  списка выбран первый элемент; после удаления — первый оставшийся.

## 7. Навигация и поведение окна
Меню «Основные данные → Проверка расценок работ» → `TariffsCheckViewModel` (UserControl в хосте),
`DataTemplate` в `App.xaml`; отдельного «Закрыть» нет. Расчёт синхронный, но быстрый (ADR-023).

## 8. Паритет-чек-лист
- [x] Сценарии из легаси воспроизведены
- [x] Формулы/округления совпадают
- [x] Ошибки и пустые состояния обработаны
- [x] Сборка проходит
- [x] Проверка на копии БД: все артикулы и подмножество; скорость
- Проверено 2026-10-22: MSBuild VS18 Debug 0/0; VM-харнесс 59/59; UIA: пустой список,
  добавление/удаление, расчёт, подтверждение «всех» (Cancel), «Выбрать все»/очистка.
  `GetMissingTariffs` ≈62 мс против ≈17,3 с у легаси (2677 SELECT-запросов).

## 9. Открытые вопросы
- Нужен ли прогресс/отмена для очень больших БД? Сейчас один SELECT + пачка UPDATE.
