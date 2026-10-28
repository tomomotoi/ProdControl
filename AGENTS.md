# ProdControl: память проекта и правила для агента

## Цель
Переписать `ProdControl` (VB.NET + WinForms + .NET 4.8) на `ProdControlWPF` (C# + WPF + .NET 4.8)
с сохранением поведения. Легаси — источник истины, его не правим.

## Контекст: ~30k токенов
- Сначала читай `docs/memory/00-index.md`, затем только файлы, нужные текущей задаче.
- Никогда не читай целиком: `*.Designer.vb`, `*.xsd`, `*.resx`, `bin/`, `obj/`, `Back/`, `packages/`.
- Большие `.vb` читай диапазонами (`grep` + offset/limit). Designer — только для имён/свойств контролов.
- Перед кодом заполни карточку фичи `docs/memory/features/<name>.md` и дальше работай по ней.
- Одна сессия — одна форма/сценарий. Не пытайся переписать всё сразу.

## Карта репозитория
- `ProdControlWPF/` — целевой C# WPF проект (`ProdControlWPF.csproj`, `App.xaml`, `MainWindow.xaml`).
- `ProdControl/ProdControl/` — легаси VB.NET (`ProdControl.vbproj`); вложенный git-репозиторий. Только чтение.
- `ProdControlWPF.slnx` — решение с целевым проектом.
- `docs/memory/` — память проекта.
- `ProdControl/ProdControl/OperationDB.mdb` — рабочая БД Access; не редактировать вручную.

## Порядок работы
1. Прочитай `00-index.md` и `04-progress.md`.
2. Выбери одну фичу из прогресса.
3. Заполни/обнови карточку фичи по шаблону.
4. Реализуй слой в `ProdControlWPF/` по `02-target.md` и `03-porting.md`.
5. Собери проект (Windows/MSBuild). Если сборка недоступна — явно скажи об этом.
6. Обнови `04-progress.md`; важные решения — в `05-decisions.md`.

## Жёсткие правила
- Не менять `ProdControl/` и файлы БД.
- .NET Framework 4.8, C# 7.3, WPF. Без NuGet/фреймворков без согласования.
- Русский UI, английские имена типов/членов.
- SQL только параметризованный; никакой конкатенации пользовательских значений.
- Бизнес-логика — в ViewModel/Domain/Services, не в code-behind.
- Не портировать сгенерированные DataSet/Designer-файлы; использовать их как схему.
- Сохранять поведение, округления, культуру ru-RU и схему Access.
- Минимальные изменения; никаких массовых рефакторингов.

## Сборка
Windows + VS2022/MSBuild: `msbuild ProdControlWPF.slnx /p:Configuration=Debug`.
В WSL/Linux WPF net48 не собирается — не тратить время, проверять на Windows.
