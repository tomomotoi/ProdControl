using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using ProdControlWPF.Data;
using ProdControlWPF.Domain;
using ProdControlWPF.Infrastructure;
using ProdControlWPF.Models;
using ProdControlWPF.Services;

namespace ProdControlWPF.ViewModels
{
    public sealed class RoutingViewModel : ObservableObject
    {
        private const string ErrorTitle = "Ошибка";
        private const string WarningTitle = "Предупреждение";
        private const string TechnologicalCardTitle = "Технологическая Карта";
        private const string MasterDataTitle = "Master Data";
        private const string LoadErrorPrefix = "Ошибка при загрузке данных: ";
        private const string NoArticulMessage = "Выберите артикул.";
        private const string AddValidationMessage = "Перед добавлением поля должны быть заполнены!";
        private const string DuplicateMessage =
            "Операция уже добавлена в список. Вы уверены, что хотите добавить дубликат?";
        private const string UsedRowMessage =
            "Операция уже используется в тех карте. Удалить можно только новую операцию";
        private const string MissingTariffsMessage =
            "В базе отсутствовали расценки на все или некоторые операции из списка, они будут рассчитаны. " +
            "Пожалуйста сохраните изменения!";
        private const string MissingRateMessage =
            "Не удалось получить расценку за минуту для одной из операций. Вероятно в таблице PositionLevel " +
            "отсутствует соответствующий PositionLevel(разряд) и/или PositionID(профессия)";
        private const string NullRateMessage =
            "Результат запроса к БД вернул NULL!" + "\r\n" +
            "Вероятно для данного разряда отсутствует информация в БД.";
        private const string CoefficientFormatMessage =
            "Поле 'Доп. затраты' и/или поле 'Норма времени' не соответствуют следующему формату:" + "\r\n\r\n" +
            "###,## или ###.## (в случае английского языка системы)" + "\r\n\r\n" +
            "Обратите внимание на разделитель целой и дробной части!";
        private const string RowFormatMessage = "Неверный формат в строке {0}: поле '{1}'.";

        private readonly IRoutingRepository _repository;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly Dictionary<int, string> _operationNames = new Dictionary<int, string>();

        private ArticulLookup _selectedArticul;
        private OperationItem _selectedOperation;
        private CoefficientLookup _selectedCoefficient;
        private string _selectedLevel = string.Empty;
        private string _rate = string.Empty;
        private string _executionTime = string.Empty;
        private string _cost = string.Empty;
        private RoutingRow _selectedRow;
        private string _rowsCountText = "Количество записей: 0";
        private bool _trackRowChanges = true;

        public RoutingViewModel(
            IRoutingRepository repository,
            IDialogService dialogService,
            INavigationService navigationService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            Articuls = new ObservableCollection<ArticulLookup>();
            Operations = new ObservableCollection<OperationItem>();
            Coefficients = new ObservableCollection<CoefficientLookup>();
            Levels = new ObservableCollection<string> { string.Empty, "1", "2", "3", "4", "5" };
            Rows = new ObservableCollection<RoutingRow>();

            SearchCommand = new RelayCommand(Search);
            AddCommand = new RelayCommand(Add);
            DeleteCommand = new RelayCommand(Delete);
            SaveCommand = new RelayCommand(Save);
            CloseCommand = new RelayCommand(Close);

            Load();
        }

        public ObservableCollection<ArticulLookup> Articuls { get; }

        public ObservableCollection<OperationItem> Operations { get; }

        public ObservableCollection<CoefficientLookup> Coefficients { get; }

        public ObservableCollection<string> Levels { get; }

        public ObservableCollection<RoutingRow> Rows { get; }

        public ICommand SearchCommand { get; }

        public ICommand AddCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand CloseCommand { get; }

        public ArticulLookup SelectedArticul
        {
            get { return _selectedArticul; }
            set { SetProperty(ref _selectedArticul, value); }
        }

        public OperationItem SelectedOperation
        {
            get { return _selectedOperation; }
            set
            {
                if (SetProperty(ref _selectedOperation, value))
                {
                    RecalculateTariff();
                }
            }
        }

        public CoefficientLookup SelectedCoefficient
        {
            get { return _selectedCoefficient; }
            set
            {
                if (SetProperty(ref _selectedCoefficient, value))
                {
                    RecalculateExecutionTime();
                }
            }
        }

        public string SelectedLevel
        {
            get { return _selectedLevel; }
            set
            {
                if (SetProperty(ref _selectedLevel, value ?? string.Empty))
                {
                    RecalculateTariff();
                }
            }
        }

        public string Rate
        {
            get { return _rate; }
            set
            {
                if (SetProperty(ref _rate, value ?? string.Empty))
                {
                    RecalculateExecutionTime();
                }
            }
        }

        public string ExecutionTime
        {
            get { return _executionTime; }
            private set
            {
                if (SetProperty(ref _executionTime, value ?? string.Empty))
                {
                    RecalculateTariff();
                }
            }
        }

        public string Cost
        {
            get { return _cost; }
            private set { SetProperty(ref _cost, value ?? string.Empty); }
        }

        public RoutingRow SelectedRow
        {
            get { return _selectedRow; }
            set { SetProperty(ref _selectedRow, value); }
        }

        public string RowsCountText
        {
            get { return _rowsCountText; }
            private set { SetProperty(ref _rowsCountText, value); }
        }

        private void Load()
        {
            try
            {
                var articuls = _repository.GetArticuls();
                var operations = _repository.GetOperations();
                var coefficients = _repository.GetCoefficients();

                Articuls.Clear();
                foreach (var articul in articuls)
                {
                    Articuls.Add(articul);
                }

                Operations.Clear();
                _operationNames.Clear();
                foreach (var operation in operations)
                {
                    Operations.Add(operation);
                    _operationNames[operation.Id] = operation.Name;
                }

                Coefficients.Clear();
                foreach (var coefficient in coefficients)
                {
                    Coefficients.Add(coefficient);
                }

                SelectedArticul = Articuls.FirstOrDefault();
                SelectedOperation = Operations.FirstOrDefault();
                SelectedCoefficient = null;
                SelectedLevel = string.Empty;
                Rate = string.Empty;
                ExecutionTime = string.Empty;
                Cost = string.Empty;
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(LoadErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void Search()
        {
            var articul = SelectedArticul;

            if (articul == null)
            {
                _dialogService.ShowError(NoArticulMessage, ErrorTitle);
                return;
            }

            try
            {
                var entries = _repository.GetRouting(articul.ArticulId);
                LoadRows(entries);
                RecalculateMissingTariffs();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }

        private void LoadRows(IReadOnlyList<RoutingEntry> entries)
        {
            foreach (var row in Rows)
            {
                row.PropertyChanged -= OnRowPropertyChanged;
            }

            _trackRowChanges = false;

            try
            {
                Rows.Clear();
                SelectedRow = null;

                foreach (var entry in entries)
                {
                    var row = new RoutingRow
                    {
                        RoutingId = entry.RoutingId,
                        ArticulId = entry.ArticulId,
                        OperationId = entry.OperationId,
                        OperationName = GetOperationName(entry.OperationId),
                        DopCost = RoutingRules.FormatNumber(entry.DopCost),
                        ExecutionTime = RoutingRules.FormatNumber(entry.TimeExecute),
                        RateExecute = RoutingRules.FormatNumber(entry.RateExecute),
                        Level = entry.PositionLevel.ToString(CultureInfo.CurrentCulture),
                        Cost = RoutingRules.FormatNumber(entry.Tariff),
                        IsNew = false,
                        IsDirty = false
                    };

                    row.PropertyChanged += OnRowPropertyChanged;
                    Rows.Add(row);
                }

                SelectedRow = Rows.FirstOrDefault();
                UpdateRowsCount();
            }
            finally
            {
                _trackRowChanges = true;
            }
        }

        private void RecalculateMissingTariffs()
        {
            if (Rows.Count == 0)
            {
                return;
            }

            if (!IsZeroOrEmpty(Rows[0].Cost))
            {
                return;
            }

            _dialogService.ShowWarning(MissingTariffsMessage, WarningTitle);

            _trackRowChanges = false;

            try
            {
                foreach (var row in Rows)
                {
                    if (!RoutingRules.TryParseLevel(row.Level, out var level))
                    {
                        _dialogService.ShowWarning(MissingRateMessage, WarningTitle);
                        continue;
                    }

                    RoutingRules.TryParseNumber(row.ExecutionTime, out var executionTime);

                    var ratePerMinute = _repository.GetPositionRateTime(row.OperationId, level);

                    if (!ratePerMinute.HasValue)
                    {
                        _dialogService.ShowWarning(MissingRateMessage, WarningTitle);
                        continue;
                    }

                    row.Cost = RoutingRules.FormatNumber(
                        RoutingRules.CalculateTariff(executionTime, ratePerMinute.Value));
                    row.IsDirty = true;
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
            finally
            {
                _trackRowChanges = true;
            }
        }

        private void Add()
        {
            if (SelectedArticul == null
                || SelectedOperation == null
                || SelectedCoefficient == null
                || string.IsNullOrEmpty(Rate)
                || string.IsNullOrEmpty(ExecutionTime)
                || string.IsNullOrWhiteSpace(SelectedLevel)
                || string.IsNullOrEmpty(Cost))
            {
                _dialogService.ShowInfo(AddValidationMessage, string.Empty);
                return;
            }

            if (Rows.Any(row => row.OperationId == SelectedOperation.Id))
            {
                var confirmed = _dialogService.Confirm(
                    DuplicateMessage,
                    TechnologicalCardTitle,
                    DialogIcon.Question);

                if (!confirmed)
                {
                    return;
                }
            }

            var newRow = new RoutingRow
            {
                RoutingId = 0,
                ArticulId = SelectedArticul.ArticulId,
                OperationId = SelectedOperation.Id,
                OperationName = SelectedOperation.Name,
                DopCost = SelectedCoefficient.Display,
                ExecutionTime = ExecutionTime,
                RateExecute = Rate,
                Level = SelectedLevel,
                Cost = Cost,
                IsNew = true,
                IsDirty = false
            };

            _trackRowChanges = false;
            newRow.PropertyChanged += OnRowPropertyChanged;
            Rows.Add(newRow);
            SelectedRow = newRow;
            _trackRowChanges = true;

            UpdateRowsCount();
        }

        private void Delete()
        {
            var row = SelectedRow;

            if (row == null)
            {
                return;
            }

            if (!row.IsNew)
            {
                _dialogService.ShowInfo(UsedRowMessage, MasterDataTitle);
                return;
            }

            row.PropertyChanged -= OnRowPropertyChanged;
            Rows.Remove(row);
            SelectedRow = Rows.FirstOrDefault();
            UpdateRowsCount();
        }

        private void Save()
        {
            var articul = SelectedArticul;

            if (articul == null)
            {
                _dialogService.ShowError(NoArticulMessage, ErrorTitle);
                return;
            }

            var inserts = new List<RoutingEntry>();
            var insertRows = new List<RoutingRow>();
            var updates = new List<RoutingEntry>();
            var updateRows = new List<RoutingRow>();

            for (var index = 0; index < Rows.Count; index++)
            {
                var row = Rows[index];

                if (!row.IsNew && !row.IsDirty)
                {
                    continue;
                }

                if (!TryBuildEntry(row, articul.ArticulId, out var entry, out var fieldName))
                {
                    _dialogService.ShowError(
                        string.Format(RowFormatMessage, index + 1, fieldName),
                        ErrorTitle);
                    return;
                }

                if (row.IsNew)
                {
                    entry.RoutingId = 0;
                    inserts.Add(entry);
                    insertRows.Add(row);
                }
                else
                {
                    entry.RoutingId = row.RoutingId;
                    updates.Add(entry);
                    updateRows.Add(row);
                }
            }

            try
            {
                _repository.Save(inserts, updates);

                for (var index = 0; index < insertRows.Count; index++)
                {
                    insertRows[index].RoutingId = inserts[index].RoutingId;
                    insertRows[index].IsNew = false;
                    insertRows[index].IsDirty = false;
                }

                foreach (var row in updateRows)
                {
                    row.IsDirty = false;
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }

        private bool TryBuildEntry(
            RoutingRow row,
            int articulId,
            out RoutingEntry entry,
            out string fieldName)
        {
            entry = null;
            fieldName = null;

            if (!RoutingRules.TryParseNumber(row.DopCost, out var dopCost))
            {
                fieldName = "Коэф. уч. доп затра";
                return false;
            }

            if (!RoutingRules.TryParseNumber(row.ExecutionTime, out var executionTime))
            {
                fieldName = "Время с уч.коэф";
                return false;
            }

            if (!RoutingRules.TryParseNumber(row.RateExecute, out var rateExecute))
            {
                fieldName = "Норма выработки";
                return false;
            }

            if (!RoutingRules.TryParseLevel(row.Level, out var level))
            {
                fieldName = "Разряд";
                return false;
            }

            if (!RoutingRules.TryParseNumber(row.Cost, out var tariff))
            {
                fieldName = "Расценка";
                return false;
            }

            entry = new RoutingEntry
            {
                ArticulId = articulId,
                OperationId = row.OperationId,
                DopCost = dopCost,
                TimeExecute = executionTime,
                RateExecute = rateExecute,
                PositionLevel = level,
                Tariff = tariff
            };

            return true;
        }

        private void RecalculateExecutionTime()
        {
            if (SelectedCoefficient == null || string.IsNullOrWhiteSpace(Rate))
            {
                ExecutionTime = string.Empty;
                return;
            }

            if (!RoutingRules.TryParseNumber(Rate, out var rate))
            {
                ExecutionTime = string.Empty;
                _dialogService.ShowWarning(CoefficientFormatMessage, ErrorTitle);
                return;
            }

            ExecutionTime = RoutingRules.FormatNumber(
                RoutingRules.CalculateExecutionTime(SelectedCoefficient.Value, rate));
        }

        private void RecalculateTariff()
        {
            if (SelectedOperation == null
                || string.IsNullOrWhiteSpace(SelectedLevel)
                || string.IsNullOrWhiteSpace(ExecutionTime))
            {
                Cost = string.Empty;
                return;
            }

            if (!RoutingRules.TryParseLevel(SelectedLevel, out var level)
                || !RoutingRules.TryParseNumber(ExecutionTime, out var executionTime))
            {
                Cost = string.Empty;
                return;
            }

            try
            {
                var ratePerMinute = _repository.GetPositionRateTime(SelectedOperation.Id, level);

                if (!ratePerMinute.HasValue)
                {
                    Cost = string.Empty;
                    _dialogService.ShowWarning(NullRateMessage, ErrorTitle);
                    return;
                }

                Cost = RoutingRules.FormatNumber(
                    RoutingRules.CalculateTariff(executionTime, ratePerMinute.Value));
            }
            catch (Exception exception)
            {
                Cost = string.Empty;
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }

        private void OnRowPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!_trackRowChanges || e.PropertyName == nameof(RoutingRow.IsDirty))
            {
                return;
            }

            if (e.PropertyName != nameof(RoutingRow.DopCost)
                && e.PropertyName != nameof(RoutingRow.ExecutionTime)
                && e.PropertyName != nameof(RoutingRow.RateExecute)
                && e.PropertyName != nameof(RoutingRow.Level)
                && e.PropertyName != nameof(RoutingRow.Cost))
            {
                return;
            }

            if (sender is RoutingRow row)
            {
                row.IsDirty = true;
            }
        }

        private void UpdateRowsCount()
        {
            RowsCountText = "Количество записей: " + Rows.Count.ToString(CultureInfo.CurrentCulture);
        }

        private string GetOperationName(int operationId)
        {
            return _operationNames.TryGetValue(operationId, out var name)
                ? name
                : string.Empty;
        }

        private static bool IsZeroOrEmpty(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            return RoutingRules.TryParseNumber(value, out var number) && number == 0;
        }

        private void Close()
        {
            _navigationService.Navigate(new MessageViewModel("ProdControl", "Выберите раздел в меню."));
        }
    }
}
