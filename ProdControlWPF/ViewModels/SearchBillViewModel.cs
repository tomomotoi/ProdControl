using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using ProdControlWPF.Data;
using ProdControlWPF.Infrastructure;
using ProdControlWPF.Models;
using ProdControlWPF.Services;

namespace ProdControlWPF.ViewModels
{
    public sealed class SearchBillViewModel : ObservableObject
    {
        private const string ErrorTitle = "Ошибка";
        private const string NoticeTitle = "Уведомление";
        private const string IntegerFieldsMessage =
            "Поля 'Время выполнения', 'Количество' и 'Рабочий день' должны быть целочисленными!";
        private const string SavedMessage = "Изменения сохранены в базе данных!";
        private const string NoChangesMessage = "Нет изменений для сохранения!";
        private const string EmployeeRequiredMessage = "Выберите сотрудника.";
        private const string ArticulRequiredMessage = "Выберите артикул.";

        private readonly ISearchBillRepository _repository;
        private readonly IDialogService _dialogService;
        private readonly Dictionary<int, EmployeeLookup> _employeesById =
            new Dictionary<int, EmployeeLookup>();
        private readonly Dictionary<int, ArticulLookup> _articulsById =
            new Dictionary<int, ArticulLookup>();
        private readonly Dictionary<int, OperationLookup> _operationsById =
            new Dictionary<int, OperationLookup>();
        private readonly List<int> _logIdsForRemoval = new List<int>();
        private readonly Dictionary<int, SearchBillUpdate> _logRowsForUpdate =
            new Dictionary<int, SearchBillUpdate>();

        private EmployeeLookup _selectedEmployee;
        private ArticulLookup _selectedArticul;
        private SearchBillRow _selectedRow;
        private int _selectedFilterIndex;
        private bool _isByPeriod;
        private bool _isEditingMode;
        private bool _trackRowChanges;
        private DateTime _dateFrom = DateTime.Today;
        private DateTime _dateTo = DateTime.Today;
        private string _documentNumber = string.Empty;

        public SearchBillViewModel(ISearchBillRepository repository, IDialogService dialogService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Employees = new ObservableCollection<EmployeeLookup>();
            Articuls = new ObservableCollection<ArticulLookup>();
            Rows = new ObservableCollection<SearchBillRow>();

            FilterOptions = new[]
            {
                "По сотруднику",
                "По артикулу",
                "Сотрудник + артикул",
                "По номеру квитанции"
            };

            SearchCommand = new RelayCommand(Search);
            SearchAllCommand = new RelayCommand(SearchAll);
            DeleteCommand = new RelayCommand(Delete);
            SaveCommand = new RelayCommand(Save);

            Load();
        }

        public ObservableCollection<EmployeeLookup> Employees { get; }

        public ObservableCollection<ArticulLookup> Articuls { get; }

        public ObservableCollection<SearchBillRow> Rows { get; }

        public IReadOnlyList<string> FilterOptions { get; }

        public ICommand SearchCommand { get; }

        public ICommand SearchAllCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand SaveCommand { get; }

        public int SelectedFilterIndex
        {
            get { return _selectedFilterIndex; }
            set
            {
                var normalized = value < 0 ? 0 : value > 3 ? 3 : value;

                if (!SetProperty(ref _selectedFilterIndex, normalized))
                {
                    return;
                }

                OnPropertyChanged(nameof(IsEmployeeFilterVisible));
                OnPropertyChanged(nameof(IsArticulFilterVisible));
                OnPropertyChanged(nameof(IsDocumentNumberVisible));
            }
        }

        public bool IsEmployeeFilterVisible
        {
            get { return _selectedFilterIndex == 0 || _selectedFilterIndex == 2; }
        }

        public bool IsArticulFilterVisible
        {
            get { return _selectedFilterIndex == 1 || _selectedFilterIndex == 2; }
        }

        public bool IsDocumentNumberVisible
        {
            get { return _selectedFilterIndex == 3; }
        }

        public EmployeeLookup SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { SetProperty(ref _selectedEmployee, value); }
        }

        public ArticulLookup SelectedArticul
        {
            get { return _selectedArticul; }
            set { SetProperty(ref _selectedArticul, value); }
        }

        public SearchBillRow SelectedRow
        {
            get { return _selectedRow; }
            set { SetProperty(ref _selectedRow, value); }
        }

        public bool IsByPeriod
        {
            get { return _isByPeriod; }
            set { SetProperty(ref _isByPeriod, value); }
        }

        public bool IsEditingMode
        {
            get { return _isEditingMode; }
            set
            {
                if (SetProperty(ref _isEditingMode, value))
                {
                    OnPropertyChanged(nameof(IsGridReadOnly));
                }
            }
        }

        public bool IsGridReadOnly
        {
            get { return !_isEditingMode; }
        }

        public DateTime DateFrom
        {
            get { return _dateFrom; }
            set { SetProperty(ref _dateFrom, value); }
        }

        public DateTime DateTo
        {
            get { return _dateTo; }
            set { SetProperty(ref _dateTo, value); }
        }

        public string DocumentNumber
        {
            get { return _documentNumber; }
            set { SetProperty(ref _documentNumber, value ?? string.Empty); }
        }

        private void Load()
        {
            try
            {
                foreach (var employee in _repository.GetEmployees())
                {
                    Employees.Add(employee);
                    _employeesById[employee.EmployeeId] = employee;
                }

                foreach (var articul in _repository.GetArticuls())
                {
                    Articuls.Add(articul);
                    _articulsById[articul.ArticulId] = articul;
                }

                foreach (var operation in _repository.GetOperations())
                {
                    _operationsById[operation.OperationId] = operation;
                }

                SelectedEmployee = Employees.FirstOrDefault();
                SelectedArticul = Articuls.FirstOrDefault();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }

            DateFrom = DateTime.Today;
            DateTo = DateTime.Today;
        }

        private void Search()
        {
            var criteria = new SearchBillCriteria
            {
                FilterMode = (SearchBillFilterMode)_selectedFilterIndex,
                UsePeriod = _isByPeriod,
                DateFrom = _dateFrom,
                DateTo = _dateTo,
                DocumentNumber = _documentNumber
            };

            if (criteria.FilterMode == SearchBillFilterMode.Employee
                || criteria.FilterMode == SearchBillFilterMode.EmployeeAndArticul)
            {
                if (SelectedEmployee == null)
                {
                    _dialogService.ShowError(EmployeeRequiredMessage, ErrorTitle);
                    return;
                }

                criteria.EmployeeId = SelectedEmployee.EmployeeId;
            }

            if (criteria.FilterMode == SearchBillFilterMode.Articul
                || criteria.FilterMode == SearchBillFilterMode.EmployeeAndArticul)
            {
                if (SelectedArticul == null)
                {
                    _dialogService.ShowError(ArticulRequiredMessage, ErrorTitle);
                    return;
                }

                criteria.ArticulId = SelectedArticul.ArticulId;
            }

            LoadRows(() => _repository.Search(criteria));
        }

        private void SearchAll()
        {
            LoadRows(_repository.GetAll);
        }

        private void LoadRows(Func<IReadOnlyList<SearchBillRow>> loader)
        {
            foreach (var row in Rows)
            {
                row.PropertyChanged -= OnRowPropertyChanged;
            }

            _trackRowChanges = false;

            try
            {
                var rows = loader();

                Rows.Clear();
                SelectedRow = null;

                foreach (var row in rows)
                {
                    ApplyLookupNames(row);
                    row.PropertyChanged += OnRowPropertyChanged;
                    Rows.Add(row);
                }

                SelectedRow = Rows.FirstOrDefault();
            }
            catch (Exception exception)
            {
                Rows.Clear();
                SelectedRow = null;
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
            finally
            {
                _trackRowChanges = true;
            }
        }

        private void ApplyLookupNames(SearchBillRow row)
        {
            row.EmployeeName = _employeesById.TryGetValue(row.EmployeeId, out var employee)
                ? employee.EmployeeName
                : string.Empty;

            row.OperationName = _operationsById.TryGetValue(row.OperationId, out var operation)
                ? operation.OperationName
                : string.Empty;

            row.ArticulName = _articulsById.TryGetValue(row.ArticulId, out var articul)
                ? articul.ArticulName
                : string.Empty;
        }

        private void OnRowPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_trackRowChanges)
            {
                return;
            }

            if (e.PropertyName != nameof(SearchBillRow.DateExecution)
                && e.PropertyName != nameof(SearchBillRow.TimeExecution)
                && e.PropertyName != nameof(SearchBillRow.Count)
                && e.PropertyName != nameof(SearchBillRow.WorkDay))
            {
                return;
            }

            var row = sender as SearchBillRow;

            if (row == null)
            {
                return;
            }

            if (!int.TryParse(row.TimeExecution, NumberStyles.Integer, CultureInfo.CurrentCulture, out var timeExecution)
                || !int.TryParse(row.Count, NumberStyles.Integer, CultureInfo.CurrentCulture, out var count)
                || !int.TryParse(row.WorkDay, NumberStyles.Integer, CultureInfo.CurrentCulture, out var workDay))
            {
                _dialogService.ShowError(IntegerFieldsMessage, ErrorTitle);
                return;
            }

            _logRowsForUpdate[row.LogId] = new SearchBillUpdate
            {
                LogId = row.LogId,
                EmployeeId = row.EmployeeId,
                OperationId = row.OperationId,
                ArticulId = row.ArticulId,
                DateExecution = row.DateExecution,
                TimeExecution = timeExecution,
                Count = count,
                DocumentNumber = row.DocumentNumber,
                WorkDay = workDay
            };
        }

        private void Delete()
        {
            var row = SelectedRow;

            if (row == null)
            {
                return;
            }

            row.PropertyChanged -= OnRowPropertyChanged;
            _logIdsForRemoval.Add(row.LogId);

            var index = Rows.IndexOf(row);
            Rows.Remove(row);

            SelectedRow = Rows.Count == 0
                ? null
                : Rows[Math.Max(0, Math.Min(index, Rows.Count - 1))];
        }

        private void Save()
        {
            if (_logIdsForRemoval.Count == 0 && _logRowsForUpdate.Count == 0)
            {
                _dialogService.ShowInfo(NoChangesMessage, NoticeTitle);
                return;
            }

            try
            {
                _repository.SaveChanges(
                    _logIdsForRemoval.ToList(),
                    _logRowsForUpdate.Values.ToList());

                _logIdsForRemoval.Clear();
                _logRowsForUpdate.Clear();

                _dialogService.ShowInfo(SavedMessage, NoticeTitle);
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }
    }
}
