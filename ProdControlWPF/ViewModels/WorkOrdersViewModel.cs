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
    public sealed class WorkOrdersViewModel : ObservableObject
    {
        private const string ErrorTitle = "Ошибка";
        private const string WarningTitle = "Проверка";
        private const string SuccessTitle = "Успех";
        private const string EmptyLookupName = "— не указано —";
        private const string ConnectionError =
            "Не настроена строка подключения \"OrdersDB\" в App.config.";

        private readonly IWorkOrderRepository _repository;
        private readonly IDialogService _dialogService;
        private readonly WorkOrderMode _mode;
        private readonly List<WorkOrderRow> _allRows = new List<WorkOrderRow>();
        private readonly List<WorkOrderRow> _modeRows = new List<WorkOrderRow>();
        private readonly Dictionary<string, WorkOrderOrderInfo> _ordersByNumber =
            new Dictionary<string, WorkOrderOrderInfo>(StringComparer.Ordinal);
        private readonly Dictionary<int, WorkOrderLookup> _knittersById =
            new Dictionary<int, WorkOrderLookup>();
        private readonly Dictionary<int, WorkOrderLookup> _formationWorkersById =
            new Dictionary<int, WorkOrderLookup>();

        private WorkOrderRow _selectedRow;
        private WorkOrderEditor _editor;
        private bool _isEditorOpen;
        private string _searchText = string.Empty;
        private bool _isByDate;
        private DateTime _dateFrom = DateTime.Today.AddMonths(-1);
        private DateTime _dateTo = DateTime.Today;

        public WorkOrdersViewModel(
            IWorkOrderRepository repository,
            IDialogService dialogService,
            WorkOrderMode mode)
        {
            _repository = repository;
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _mode = mode;

            Rows = new ObservableCollection<WorkOrderRow>();
            Knitters = new ObservableCollection<WorkOrderLookup>();
            FormationWorkers = new ObservableCollection<WorkOrderLookup>();

            OpenCommand = new RelayCommand(Open, () => _selectedRow != null && !_isEditorOpen);
            ReloadCommand = new RelayCommand(() => LoadData());
            ResetFilterCommand = new RelayCommand(ResetFilter);
            SaveCommand = new RelayCommand(Save, () => _editor != null);
            CancelEditCommand = new RelayCommand(CancelEdit, () => _editor != null);
            MarkCompletedCommand = new RelayCommand(MarkCompleted, () => _editor != null);

            LoadData();
        }

        public ObservableCollection<WorkOrderRow> Rows { get; }

        public ObservableCollection<WorkOrderLookup> Knitters { get; }

        public ObservableCollection<WorkOrderLookup> FormationWorkers { get; }

        public WorkOrderMode Mode
        {
            get { return _mode; }
        }

        public string Title
        {
            get
            {
                return _mode == WorkOrderMode.Active
                    ? "Наряды: действующие"
                    : "Наряды: выполненные";
            }
        }

        public string Description
        {
            get
            {
                return _mode == WorkOrderMode.Active
                    ? "Наряды, по которым ещё не выполнен план."
                    : "Наряды с выполненным или перевыполненным планом.";
            }
        }

        public ICommand OpenCommand { get; }

        public ICommand ReloadCommand { get; }

        public ICommand ResetFilterCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand CancelEditCommand { get; }

        public ICommand MarkCompletedCommand { get; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (SetProperty(ref _searchText, value ?? string.Empty))
                {
                    ApplyFilter(_selectedRow?.Id);
                }
            }
        }

        public bool IsByDate
        {
            get { return _isByDate; }
            set
            {
                if (SetProperty(ref _isByDate, value))
                {
                    ApplyFilter(_selectedRow?.Id);
                }
            }
        }

        public DateTime DateFrom
        {
            get { return _dateFrom; }
            set
            {
                if (SetProperty(ref _dateFrom, value.Date))
                {
                    ApplyFilter(_selectedRow?.Id);
                }
            }
        }

        public DateTime DateTo
        {
            get { return _dateTo; }
            set
            {
                if (SetProperty(ref _dateTo, value.Date))
                {
                    ApplyFilter(_selectedRow?.Id);
                }
            }
        }

        public WorkOrderRow SelectedRow
        {
            get { return _selectedRow; }
            set
            {
                if (SetProperty(ref _selectedRow, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public WorkOrderEditor Editor
        {
            get { return _editor; }
            private set { SetProperty(ref _editor, value); }
        }

        public bool IsEditorOpen
        {
            get { return _isEditorOpen; }
            private set
            {
                if (SetProperty(ref _isEditorOpen, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string RowCountText
        {
            get { return string.Format("Найдено: {0} из {1}", Rows.Count, _modeRows.Count); }
        }

        public bool IsEmpty
        {
            get { return Rows.Count == 0; }
        }

        public string EmptyMessage
        {
            get
            {
                return _mode == WorkOrderMode.Active
                    ? "Действующие наряды по заданным критериям не найдены."
                    : "Выполненные наряды по заданным критериям не найдены.";
            }
        }

        private void ResetFilter()
        {
            _searchText = string.Empty;
            _isByDate = false;
            _dateFrom = DateTime.Today.AddMonths(-1);
            _dateTo = DateTime.Today;

            OnPropertyChanged(nameof(SearchText));
            OnPropertyChanged(nameof(IsByDate));
            OnPropertyChanged(nameof(DateFrom));
            OnPropertyChanged(nameof(DateTo));

            ApplyFilter(_selectedRow?.Id);
        }

        private bool LoadData()
        {
            return LoadData(_selectedRow?.Id);
        }

        private bool LoadData(int? selectedId)
        {
            if (_repository == null)
            {
                _dialogService.ShowError(ConnectionError, ErrorTitle);
                ApplyFilter(null);
                return false;
            }

            _allRows.Clear();
            _modeRows.Clear();
            _ordersByNumber.Clear();
            _knittersById.Clear();
            _formationWorkersById.Clear();
            Knitters.Clear();
            FormationWorkers.Clear();

            try
            {
                foreach (var knitter in _repository.GetKnitters())
                {
                    Knitters.Add(knitter);
                    _knittersById[knitter.Id] = knitter;
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(
                    "Ошибка загрузки справочника вязальщиц: " + exception.Message,
                    ErrorTitle);
            }

            Knitters.Insert(0, new WorkOrderLookup(0, EmptyLookupName));

            try
            {
                foreach (var worker in _repository.GetFormationWorkers())
                {
                    FormationWorkers.Add(worker);
                    _formationWorkersById[worker.Id] = worker;
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(
                    "Ошибка загрузки справочника формировщиц: " + exception.Message,
                    ErrorTitle);
            }

            FormationWorkers.Insert(0, new WorkOrderLookup(0, EmptyLookupName));

            try
            {
                foreach (var order in _repository.GetOrders())
                {
                    _ordersByNumber[order.OrderNumber] = order;
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(
                    "Ошибка загрузки заказов: " + exception.Message,
                    ErrorTitle);
            }

            try
            {
                foreach (var row in _repository.GetRows())
                {
                    ApplyOrderInfo(row);
                    ApplyLookupNames(row);
                    _allRows.Add(row);
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(
                    "Ошибка загрузки нарядов: " + exception.Message,
                    ErrorTitle);
                ApplyFilter(selectedId);
                return false;
            }

            var completed = _mode == WorkOrderMode.Completed;

            foreach (var row in _allRows)
            {
                if (row.IsCompleted == completed)
                {
                    _modeRows.Add(row);
                }
            }

            ApplyFilter(selectedId);
            return true;
        }

        private void ApplyFilter(int? selectedId)
        {
            IEnumerable<WorkOrderRow> filtered = _modeRows;

            var search = (_searchText ?? string.Empty).Trim();

            if (search.Length > 0)
            {
                filtered = filtered.Where(row => MatchesSearch(row, search));
            }

            if (_isByDate)
            {
                var from = _dateFrom.Date;
                var to = _dateTo.Date;

                if (from > to)
                {
                    var temp = from;
                    from = to;
                    to = temp;
                }

                filtered = filtered.Where(row =>
                    row.OrderDate.HasValue
                    && row.OrderDate.Value.Date >= from
                    && row.OrderDate.Value.Date <= to);
            }

            var currentId = selectedId ?? _selectedRow?.Id;

            Rows.Clear();

            foreach (var row in filtered)
            {
                Rows.Add(row);
            }

            SelectedRow = currentId.HasValue
                ? Rows.FirstOrDefault(row => row.Id == currentId.Value)
                : null;

            if (SelectedRow == null)
            {
                SelectedRow = Rows.FirstOrDefault();
            }

            OnPropertyChanged(nameof(RowCountText));
            OnPropertyChanged(nameof(IsEmpty));
            CommandManager.InvalidateRequerySuggested();
        }

        private static bool MatchesSearch(WorkOrderRow row, string search)
        {
            return Contains(row.Code, search)
                || Contains(row.OrderNumber, search)
                || Contains(row.CustomerName, search)
                || Contains(row.ArticleName, search)
                || Contains(row.ColorName, search)
                || Contains(row.Machine, search)
                || Contains(row.HeightDisplay, search)
                || Contains(row.SizeDisplay, search)
                || Contains(row.Knitter1Name, search)
                || Contains(row.Knitter2Name, search)
                || Contains(row.Knitter3Name, search);
        }

        private static bool Contains(string value, string search)
        {
            return !string.IsNullOrEmpty(value)
                && value.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        private void ApplyOrderInfo(WorkOrderRow row)
        {
            if (string.IsNullOrEmpty(row.OrderNumber)
                || !_ordersByNumber.TryGetValue(row.OrderNumber, out var order))
            {
                return;
            }

            row.CustomerName = order.CustomerName;
            row.ArticleName = order.ArticleName;
            row.ColorName = order.ColorName;
            row.EquipmentName = order.EquipmentName;
            row.OrderPlannedQuantity = order.PlannedQuantity;
        }

        private void ApplyLookupNames(WorkOrderRow row)
        {
            row.Knitter1Name = GetLookupName(_knittersById, row.Knitter1Id);
            row.Knitter2Name = GetLookupName(_knittersById, row.Knitter2Id);
            row.Knitter3Name = GetLookupName(_knittersById, row.Knitter3Id);
            row.VtoWorkerName = GetLookupName(_formationWorkersById, row.VtoWorkerId);
            row.WashWorkerName = GetLookupName(_formationWorkersById, row.WashWorkerId);
        }

        private static string GetLookupName(
            Dictionary<int, WorkOrderLookup> lookups,
            int? id)
        {
            if (!id.HasValue || !lookups.TryGetValue(id.Value, out var lookup))
            {
                return string.Empty;
            }

            return lookup.Name;
        }

        private void Open()
        {
            var row = _selectedRow;

            if (row == null)
            {
                return;
            }

            Editor = CreateEditor(row);
            IsEditorOpen = true;
        }

        private WorkOrderEditor CreateEditor(WorkOrderRow row)
        {
            var editor = new WorkOrderEditor
            {
                Id = row.Id,
                Code = row.Code,
                OrderNumber = row.OrderNumber,
                OrderDate = row.OrderDate,
                CustomerName = row.CustomerName,
                ArticleName = row.ArticleName,
                ColorName = row.ColorName,
                EquipmentName = row.EquipmentName,
                Machine = row.Machine,
                Height = row.Height,
                Size = row.Size,
                PlanQuantity = row.PlanQuantity,
                FactQuantity = row.FactQuantity,
                FactWeight = row.FactWeight,
                StatusText = row.StatusText,
                ProgressPercent = row.ProgressPercent,
                RemainingQuantity = row.RemainingQuantity,
                HeightNew = row.HeightNew,
                SizeNew = row.SizeNew,
                FactQuantityText = row.FactQuantity.ToString(CultureInfo.CurrentCulture),
                FactWeightText = WorkOrderRules.FormatNumber(row.FactWeight),
                FactDate = row.FactDate,
                Knitter1 = FindLookup(Knitters, _knittersById, row.Knitter1Id),
                Knitter1PercentText = WorkOrderRules.FormatNumber(row.Knitter1Percent),
                Knitter2 = FindLookup(Knitters, _knittersById, row.Knitter2Id),
                Knitter2PercentText = WorkOrderRules.FormatNumber(row.Knitter2Percent),
                Knitter3 = FindLookup(Knitters, _knittersById, row.Knitter3Id),
                Knitter3PercentText = WorkOrderRules.FormatNumber(row.Knitter3Percent),
                VtoWorker = FindLookup(FormationWorkers, _formationWorkersById, row.VtoWorkerId),
                VtoDate = row.VtoDate,
                WashWorker = FindLookup(FormationWorkers, _formationWorkersById, row.WashWorkerId),
                WashDate = row.WashDate
            };

            editor.Summary = WorkOrderRules.BuildSummary(
                row.OrderNumber,
                _allRows.Where(item => string.Equals(
                    item.OrderNumber,
                    row.OrderNumber,
                    StringComparison.Ordinal)),
                row.OrderPlannedQuantity);

            return editor;
        }

        private static WorkOrderLookup FindLookup(
            ObservableCollection<WorkOrderLookup> source,
            Dictionary<int, WorkOrderLookup> lookups,
            int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                return source.FirstOrDefault(item => item.IsEmpty);
            }

            if (lookups.TryGetValue(id.Value, out var lookup))
            {
                return lookup;
            }

            var placeholder = new WorkOrderLookup(id.Value, "Код " + id.Value);
            lookups[id.Value] = placeholder;
            source.Add(placeholder);
            return placeholder;
        }

        private void CancelEdit()
        {
            IsEditorOpen = false;
            Editor = null;
        }

        private void MarkCompleted()
        {
            if (_editor == null)
            {
                return;
            }

            var target = _editor.PlanQuantity > 0
                ? _editor.PlanQuantity
                : Math.Max(1, _editor.FactQuantity);

            _editor.FactQuantityText = target.ToString(CultureInfo.CurrentCulture);
            _editor.FactDate = DateTime.Today;

            Save();
        }

        private void Save()
        {
            if (_editor == null)
            {
                return;
            }

            if (!TryBuildUpdate(_editor, out var update, out var error))
            {
                _dialogService.ShowWarning(error, WarningTitle);
                return;
            }

            try
            {
                _repository.Update(update);
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(
                    "Ошибка при сохранении наряда: " + exception.Message,
                    ErrorTitle);
                return;
            }

            var id = _editor.Id;
            var code = _editor.Code;

            CancelEdit();

            var reloaded = LoadData(id);

            _dialogService.ShowInfo(
                reloaded
                    ? string.Format("Наряд {0} сохранён.", code)
                    : string.Format("Наряд {0} сохранён, но список не удалось перечитать.", code),
                SuccessTitle);
        }

        private static bool TryBuildUpdate(
            WorkOrderEditor editor,
            out WorkOrderUpdate update,
            out string error)
        {
            update = null;
            error = null;

            if (!WorkOrderRules.TryParseNonNegativeInt(editor.FactQuantityText, out var factQuantity))
            {
                error = "Фактическое количество должно быть целым неотрицательным числом.";
                return false;
            }

            if (!WorkOrderRules.TryParseNullableDecimal(editor.FactWeightText, out var factWeight))
            {
                error = "Фактический вес должен быть числом.";
                return false;
            }

            if (factWeight.HasValue && factWeight.Value < 0m)
            {
                error = "Фактический вес не может быть отрицательным.";
                return false;
            }

            if (!TryGetPercent(editor.Knitter1, editor.Knitter1PercentText, out var percent1, out error)
                || !TryGetPercent(editor.Knitter2, editor.Knitter2PercentText, out var percent2, out error)
                || !TryGetPercent(editor.Knitter3, editor.Knitter3PercentText, out var percent3, out error))
            {
                return false;
            }

            var selectedKnitters = new[]
            {
                GetLookupId(editor.Knitter1),
                GetLookupId(editor.Knitter2),
                GetLookupId(editor.Knitter3)
            }.Where(id => id.HasValue).Select(id => id.Value).ToList();

            if (selectedKnitters.Count != selectedKnitters.Distinct().Count())
            {
                error = "Одна и та же вязальщица выбрана несколько раз.";
                return false;
            }

            update = new WorkOrderUpdate
            {
                Id = editor.Id,
                FactQuantity = factQuantity,
                FactWeight = factWeight,
                FactDate = editor.FactDate,
                HeightNew = NormalizeText(editor.HeightNew),
                SizeNew = NormalizeText(editor.SizeNew),
                Knitter1Id = GetLookupId(editor.Knitter1),
                Knitter1Percent = percent1,
                Knitter2Id = GetLookupId(editor.Knitter2),
                Knitter2Percent = percent2,
                Knitter3Id = GetLookupId(editor.Knitter3),
                Knitter3Percent = percent3,
                VtoWorkerId = GetLookupId(editor.VtoWorker),
                VtoDate = editor.VtoDate,
                WashWorkerId = GetLookupId(editor.WashWorker),
                WashDate = editor.WashDate
            };

            return true;
        }

        private static bool TryGetPercent(
            WorkOrderLookup worker,
            string text,
            out decimal? percent,
            out string error)
        {
            percent = null;
            error = null;

            if (worker == null || worker.IsEmpty)
            {
                return true;
            }

            if (!WorkOrderRules.TryParsePercent(text, out var value))
            {
                error = string.Format(
                    "Укажите процент от 0 до 100 для вязальщицы {0}.",
                    worker.Name);
                return false;
            }

            percent = value;
            return true;
        }

        private static int? GetLookupId(WorkOrderLookup lookup)
        {
            return lookup != null && lookup.Id > 0 ? (int?)lookup.Id : null;
        }

        private static string NormalizeText(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}
