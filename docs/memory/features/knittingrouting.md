# Feature: KnittingRoutingForm (Тех. карта, вязание)

- Легаси: `KnittingRoutingForm.vb` (1–515), Designer (контролы/колонки).
- Цель: `Views/KnittingRoutingView` / `ViewModels/KnittingRoutingViewModel`.
- Статус: готово (собран; VM-харнесс 31/31 на копии БД, UIA smoke).
- Зависимости: `KnittingRouting`, `ClothingParts`, `KnittingMachines`, `Coefficients`, `Articul`.

## 1. Назначение и сценарии
Тех. карта вязания: выбрать артикул → «Найти» → загрузить строки из `KnittingRouting`
(деталь, оборудование, М, П, коэфф. 1/2, средн. зн., с коэфф., с компл.) → добавить строки
из шапки, удалить выбранную, сохранить. Итог — сумма «С компл.» и «Норма» (480 / итог).
Сохранение заменяет всю карту артикула: DELETE всех строк + INSERT текущих.

## 2. UI (из Designer)
| Контрол | Тип | Назначение |
|---|---|---|
| `cbArticle` | ComboBox | артикул (`ArticulName`/`ArticulID`) |
| `btnSeachArt` | Button | «Найти» |
| `cbKnittingPart` | ComboBox | деталь (`part_name`/`id`) |
| `cbKnittingMachine` | ComboBox | оборудование (`machine_displayname`/`id`) |
| `cbModelPartAmount` | ComboBox | М — 1…50 (строки) |
| `cbMachineCarriageAmount` | ComboBox | П — 1…4 (строки) |
| `cbCoeff1`, `cbCoeff2` | ComboBox | коэффициенты (`coefficient_value`/`coefficient_id`) |
| `tbAvgValue` | TextBox | «Средн. зн.» (ввод) |
| `tbWithCoefficient` | TextBox | «С коэфф.» = avg×k1×k2, read-only |
| `tbWithKit` | TextBox | «С компл.» = withCoeff×М/П, read-only |
| `tbTotal` | TextBox | «Итого» = Σ kit_rate, read-only |
| `tbRate` | TextBox | «Норма» = 480/итог, read-only |
| `dgView` | DataGridView | 9 read-only колонок (см. §4) |
| `btnAdd/btnDel/btSave` | Button | Добавить/Удалить/Сохранить; «Отменить» нет |

## 3. События и команды
| Событие легаси | Действие | Команда/метод VM |
|---|---|---|
| Load | заполнить 4 справочника; первые элементы выбраны | `Load()` |
| `tbAvgValue.TextChanged`, `cbCoeff1/2` change | пересчёт «С коэфф.» → «С компл.» | сеттеры |
| `cbModelPartAmount/cbMachineCarriageAmount` change | пересчёт «С компл.» | сеттеры |
| `cbArticle.SelectedIndexChanged` | очистить грид, «Итого», «Норма», `isLoadedFromDb=false` | сеттер |
| `btnSeachArt.Click` | SELECT карты; Σ kit_rate; норма; пустой результат | `SearchCommand` |
| `btnAdd.Click` | проверка 9 полей; добавить строку | `AddCommand` |
| `btnDel.Click` | подтверждение; удалить выбранную строку (без БД) | `DeleteCommand` |
| `btSave.Click` | DELETE по артикулу + INSERT строк в транзакции | `SaveCommand` |

## 4. Данные
- SELECT справочников: `Articul(ArticulID, ArticulName)`; `ClothingParts(id, part_name)`;
  `KnittingMachines(id, machine_make + ' ' + machine_model AS machine_displayname)`;
  `Coefficients(coefficient_id, coefficient_value)` (без ORDER BY, как Fill легаси).
- Загрузка: `cp.part_name, km.machine_make & ' ' & km.machine_model AS machine_display_name,
  kr.product_parts_amount, kr.machine_parts_amount, kr.k1, kr.k2, kr.avg_rate,
  kr.coefficient_rate, kr.kit_rate` + `cp.id`, `km.id`
  `FROM (KnittingRouting kr INNER JOIN ClothingParts cp ON kr.clothing_part_id = cp.id)
  INNER JOIN KnittingMachines km ON kr.machine_id = km.id WHERE kr.articul_id = ?`.
- `KnittingRouting`: `id, articul_id, clothing_part_id, machine_id, product_parts_amount,
  machine_parts_amount, k1, k2, avg_rate, coefficient_rate, kit_rate`.
- Сохранение (одна транзакция): `DELETE FROM KnittingRouting WHERE articul_id = ?`;
  `INSERT INTO KnittingRouting (articul_id, clothing_part_id, machine_id, product_parts_amount,
  machine_parts_amount, k1, k2, avg_rate, coefficient_rate, kit_rate) VALUES (?,?,?,?,?,?,?,?,?,?)`.
  М/П — `OleDbType.Integer` (из decimal), остальные числа — `OleDbType.Double`.
- `articul_id` в БД текстовый (xsd), но легаси передаёт `OleDbType.Integer` — сохранить; ID детали/станка брать из моделей строки (в легаси — повторный поиск по имени).

## 5. Бизнес-правила и формулы
- «С коэфф.» = `avg × k1 × k2`, `ToString("F2")`; пустой/0 коэффициент → 1;
  пустой или 0 avg → очистить «С коэфф.» и «С компл.».
- «С компл.» = `withCoeff × М / П`, `ToString("F2")`; пустые/0 М, П → 1;
  если withCoeff = 0 → `"0.000"`; если «С коэфф.» пусто → очистить.
- Загрузка: `avg_rate`, `coefficient_rate`, `kit_rate` → `F3`; итог = Σ `kit_rate` → `F2`;
  норма = `(480 / итог).ToString("F1")` при итог > 0, иначе `"0"`; нет строк → пусто.
- Парсинг чисел: текущая культура, затем замена `,`↔`.`; пустые/`DBNull` → 0; в БД М/П — целые (`Convert.ToInt32(decimal)`), k1/k2/avg/with/kit — double.

## 6. Валидация и edge cases
- Добавление: обязательны деталь, станок, М, П, коэфф. 1, коэфф. 2, средн. зн., с коэфф., с компл.;
  иначе «Поле '{имя}' не заполнено!» (Внимание), фокус на поле.
- Удаление без строки: «Выберите строку для удаления!»; подтверждение «Вы действительно хотите удалить выбранную строку?» (Да/Нет).
- Сохранение без артикула: «Выберите артикул!» (Ошибка).
- Сохранение без строк: если карта загружалась — «Вы удалили все строки из загруженной карты.
  Удалить эту технологическую карту из базы данных?» (Да → DELETE, Нет → отмена);
  иначе «Таблица пуста. Нет данных для сохранения!».
- Поиск без артикула: «Выберите артикул для поиска!»; пусто — «Записи для указанного
  артикула не найдены.» (Информация), поля итога очищены.
- Нет ID детали/станка: «Не удалось найти ID в базе для детали '…' или оборудования '…'.»
- Ошибки: загрузка — «Ошибка при загрузке данных: …»; сохранение — «Ошибка при сохранении
  в БД: …» (rollback); добавление/удаление — «Ошибка при добавлении/удалении строки: …».
- Успех — «Данные успешно сохранены!» (Успех), затем `isLoadedFromDb = hasRows`; сетка read-only, правки только через шапку.

## 7. Навигация и поведение окна
- Открывается из меню «Основные данные → Тех.карта (Вязание)» (маршрут `KnittingRoutingViewModel`); отдельной кнопки закрытия нет — смена раздела через меню.
- При открытии артикул/деталь/станок/коэффициенты — первые элементы; М/П не выбраны.
- Смена артикула очищает грид и итоговые поля.

## 8. Паритет-чек-лист
- [x] Карточка заполнена по `KnittingRoutingForm.vb` и Designer.
- [x] Сценарии воспроизведены (VM-харнесс: поиск/добавление/удаление/сохранение/очистка карты).
- [x] Формулы/округления совпадают (F2/F3/F1, 480).
- [x] Ошибки и пустые состояния обработаны.
- [x] Сборка на Windows (VS18 Debug 0/0); UIA smoke: меню, 4 кнопки, грид 9 колонок, 7 combo, 5 полей.

## 9. Отклонения и открытые вопросы
- Комбо M/П и коэффициентов в легаси были редактируемыми (`DropDownStyle.DropDown`); в WPF — выбор из списка (M 1…50, П 1…4), как в `RoutingForm` (ADR-022).
- Grid в легаси 9 read-only колонок; dirty-строк нет — сохранение всегда перезаписывает карту.
