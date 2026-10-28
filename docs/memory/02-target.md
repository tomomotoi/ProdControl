# 02 · Целевая архитектура (C#/WPF)

## Стек и ограничения
- .NET Framework 4.8, C# 7.3, WPF. Без сторонних пакетов на старте.
- x86 для совместимости с Jet OLEDB (проверить на Windows; альтернатива — ACE).
- Культура ru-RU; русские подписи, английские идентификаторы типов/членов.
- Старый формат `.csproj`: новые файлы добавлять в `<ItemGroup>` вручную.

## Структура
```
ProdControlWPF/
  App.xaml(.cs)          composition root, глобальные ресурсы
  MainWindow.xaml(.cs)   только оболочка (меню, хост, статус)
  Views/                 UserControl и Window
  ViewModels/            состояние, команды, валидация
  Models/                POCO-модели
  Data/                  DbConnectionFactory, репозитории
  Domain/                расчёты, константы, бизнес-правила
  Services/              навигация, диалоги, отчёты, backup
  Controls/              переиспользуемые контролы
  Resources/             стили, иконки, строки
  Properties/
```

## MVVM-lite
- Свои `ObservableObject`, `RelayCommand`, `AsyncRelayCommand`; без MVVM-фреймворка.
- Code-behind — только то, что относится к View (фокус, выделение, размеры).
- ViewModel не знает о `MessageBox`, `OpenFileDialog`, `Window`; всё через сервисы.
- Ручная композиция в `App.xaml.cs`; без DI-контейнера, пока сервисов мало.

## Навигация и UI
- `MainWindow` — оболочка: меню, `ContentControl`, статус-бар, overlay занятости.
- `INavigationService.Navigate<TViewModel>()`; дочерние экраны — `UserControl`.
- Модальные окна — `Window` через `IDialogService`.
- Переиспользуемые элементы: панель поиска, редактируемая таблица, busy-overlay, диалог подтверждения, numeric-поле.
- `DataGrid` + `ObservableCollection`/`BindingList`; inline-edit и валидация — через ViewModel.
- Стили и цвета — в `ResourceDictionary`; не хардкодить в каждом View.

## Данные
- `IDbConnectionFactory` → `OleDbConnection`; строка подключения из конфигурации.
- Репозитории на ADO.NET: параметризованный SQL, `OleDbDataReader`/`DataTable` → POCO.
- Без EF/DataSet/Dapper на старте; DataSet-дизайнеры легаси не портировать.
- Транзакции — для многозаписных сохранений (накладные, массовый пересчёт).
- Деньги/округления: сохранять поведение легаси (`Math.Round(..., 2)`), не менять тип без проверки.
- Даты и числа — `CultureInfo` ru-RU; в SQL — параметры с явным `DbType`.

## Отчёты
- ReportViewer 11 WinForms через `WindowsFormsHost` + `IReportService`.
- RDLC — embedded resource; проверить наличие runtime на машине.
- Позже можно заменить на WPF-отчёты, но только после паритета.

## Ошибки и логирование
- `IDialogService` для сообщений; `ILogService` — файл в папке приложения.
- Не показывать пользователю стектрейсы; текст ошибок — по-русски.

## Тесты
- Расчёты и правила — в `Domain/`, чистыми функциями.
- Тестовый проект добавить после согласования (NuGet); до этого проверять сценарии вручную.

## Definition of done
- Проект собирается на Windows.
- Сценарий проверен по карточке фичи; поведение совпадает с легаси.
- Нет SQL-конкатенации, логики в code-behind и прямого доступа к БД из ViewModel.
- Обновлены `04-progress.md` и при необходимости `05-decisions.md`.
