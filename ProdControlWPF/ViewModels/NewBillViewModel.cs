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
    public sealed class NewBillViewModel : ObservableObject
    {
        private const string WorkdayEmptyMessage = "Поле продолжительность рабочего времени не заполнено!";
        private const string WorkdayTitle = "Рабочий день";
        private const string WorkdayTooLongMessage = "Продолжительность рабочего дня не может превышать 480";
        private const string ExecutionTimeFormatMessage = "Неверный формат числа в поле 'Время выполнения'!";
        private const string CalculateErrorMessage =
            "Неверный формат даты в поле 'Время выполнения' или не удалось получить процент простоя из БД!";
        private const string CountFormatMessage = "Неверный формат числа в поле 'Количество'!";
        private const string WorkdayFormatMessage = "Неверный формат числа в поле 'Рабочий день'!";
        private const string ErrorTitle = "Ошибка";
        private const string SavedMessage = "Новые записи сохранены в Базе данных";
        private const string SaveTitle = "Сохранение";

        private readonly INewBillRepository _repository;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        private EmployeeLookup _selectedEmployee;
        private ArticulLookup _selectedArticul;
        private OperationLookup _selectedOperation;
        private IdleOperationLookup _selectedIdleOperation;
        private OperationLogRow _selectedRow;
        private DateTime _operationDate = DateTime.Today;
        private string _logNumber = string.Empty;
        private string _workday = "480";
        private string _count = "0";
        private string _operationTime = "0";
        private bool _isNonStandard;
        private bool _isOperationEnabled;

        public NewBillViewModel(
            INewBillRepository repository,
            IDialogService dialogService,
            INavigationService navigationService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            Employees = new ObservableCollection<EmployeeLookup>();
            Articuls = new ObservableCollection<ArticulLookup>();
            Operations = new ObservableCollection<OperationLookup>();
            IdleOperations = new ObservableCollection<IdleOperationLookup>();
            Rows = new ObservableCollection<OperationLogRow>();

            InsertCommand = new RelayCommand(Insert);
            DeleteCommand = new RelayCommand(Delete);
            SaveCommand = new RelayCommand(Save);
            CalculateCommand = new RelayCommand(Calculate);
            CloseCommand = new RelayCommand(Close);

            Load();
        }

        public event EventHandler WorkdayFocusRequested;

        public ObservableCollection<EmployeeLookup> Employees { get; }

        public ObservableCollection<ArticulLookup> Articuls { get; }

        public ObservableCollection<OperationLookup> Operations { get; }

        public ObservableCollection<IdleOperationLookup> IdleOperations { get; }

        public ObservableCollection<OperationLogRow> Rows { get; }

        public ICommand InsertCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand CalculateCommand { get; }

        public ICommand CloseCommand { get; }

        public EmployeeLookup SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                if (SetProperty(ref _selectedEmployee, value))
                {
                    Workday = string.Empty;
                }
            }
        }

        public ArticulLookup SelectedArticul
        {
            get { return _selectedArticul; }
            set
            {
                if (!SetProperty(ref _selectedArticul, value))
                {
                    return;
                }

                if (value == null)
                {
                    Operations.Clear();
                    SelectedOperation = null;
                    IsOperationEnabled = false;
                    return;
                }

                try
                {
                    LoadOperations(value.ArticulId);
                }
                catch (Exception exception)
                {
                    Operations.Clear();
                    SelectedOperation = null;
                    IsOperationEnabled = false;
                    _dialogService.ShowError(exception.Message, ErrorTitle);
                }
            }
        }

        public OperationLookup SelectedOperation
        {
            get { return _selectedOperation; }
            set
            {
                if (SetProperty(ref _selectedOperation, value) && value != null)
                {
                    OperationTime = value.OperationTimeWithKoef.ToString(CultureInfo.CurrentCulture);
                }
            }
        }

        public IdleOperationLookup SelectedIdleOperation
        {
            get { return _selectedIdleOperation; }
            set { SetProperty(ref _selectedIdleOperation, value); }
        }

        public OperationLogRow SelectedRow
        {
            get { return _selectedRow; }
            set { SetProperty(ref _selectedRow, value); }
        }

        public DateTime OperationDate
        {
            get { return _operationDate; }
            set
            {
                if (SetProperty(ref _operationDate, value))
                {
                    Workday = string.Empty;
                }
            }
        }

        public string LogNumber
        {
            get { return _logNumber; }
            set { SetProperty(ref _logNumber, value ?? string.Empty); }
        }

        public string Workday
        {
            get { return _workday; }
            set
            {
                var normalized = value ?? string.Empty;

                if (!SetProperty(ref _workday, normalized))
                {
                    return;
                }

                if (int.TryParse(normalized, NumberStyles.Integer, CultureInfo.CurrentCulture, out var parsed)
                    && parsed > BillRules.MaxWorkday)
                {
                    _workday = BillRules.MaxWorkday.ToString(CultureInfo.CurrentCulture);
                    OnPropertyChanged();
                    _dialogService.ShowError(WorkdayTooLongMessage, WorkdayTitle);
                }
            }
        }

        public string Count
        {
            get { return _count; }
            set { SetProperty(ref _count, value ?? string.Empty); }
        }

        public string OperationTime
        {
            get { return _operationTime; }
            set { SetProperty(ref _operationTime, value ?? string.Empty); }
        }

        public bool IsNonStandard
        {
            get { return _isNonStandard; }
            set
            {
                if (!SetProperty(ref _isNonStandard, value))
                {
                    return;
                }

                if (value)
                {
                    Count = "0";
                }
                else
                {
                    OperationTime = "0";
                }

                OnPropertyChanged(nameof(IsCountEnabled));
                OnPropertyChanged(nameof(IsOperationTimeEnabled));
                OnPropertyChanged(nameof(IsIdleTypeEnabled));
                OnPropertyChanged(nameof(IsCalculateEnabled));
            }
        }

        public bool IsCountEnabled
        {
            get { return !_isNonStandard; }
        }

        public bool IsOperationTimeEnabled
        {
            get { return _isNonStandard; }
        }

        public bool IsIdleTypeEnabled
        {
            get { return _isNonStandard; }
        }

        public bool IsCalculateEnabled
        {
            get { return _isNonStandard; }
        }

        public bool IsOperationEnabled
        {
            get { return _isOperationEnabled; }
            private set { SetProperty(ref _isOperationEnabled, value); }
        }

        private void Load()
        {
            try
            {
                foreach (var employee in _repository.GetEmployees())
                {
                    Employees.Add(employee);
                }

                foreach (var articul in _repository.GetArticuls())
                {
                    Articuls.Add(articul);
                }

                foreach (var idleOperation in _repository.GetIdleOperations())
                {
                    IdleOperations.Add(idleOperation);
                }

                SelectedEmployee = Employees.FirstOrDefault();
                SelectedArticul = Articuls.FirstOrDefault();
                SelectedIdleOperation = IdleOperations.FirstOrDefault();

                OperationDate = DateTime.Today;
                Workday = BillRules.MaxWorkday.ToString(CultureInfo.CurrentCulture);
                Count = "0";
                OperationTime = "0";
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }

        private void LoadOperations(int articulId)
        {
            var operations = _repository.GetOperationsByArticul(articulId);

            Operations.Clear();

            foreach (var operation in operations)
            {
                Operations.Add(operation);
            }

            SelectedOperation = Operations.FirstOrDefault();
            IsOperationEnabled = Operations.Count > 0;
        }

        private void Insert()
        {
            if (string.IsNullOrWhiteSpace(Workday))
            {
                _dialogService.ShowError(WorkdayEmptyMessage, WorkdayTitle);
                WorkdayFocusRequested?.Invoke(this, EventArgs.Empty);
                return;
            }

            if (SelectedEmployee == null || SelectedArticul == null || SelectedOperation == null)
            {
                _dialogService.ShowError("Выберите сотрудника, артикул и операцию.", ErrorTitle);
                return;
            }

            if (!BillRules.TryParseDouble(OperationTime, out var operationTime))
            {
                _dialogService.ShowError(ExecutionTimeFormatMessage, ErrorTitle);
                return;
            }

            Rows.Add(new OperationLogRow
            {
                EmployeeId = SelectedEmployee.EmployeeId,
                EmployeeName = SelectedEmployee.EmployeeName,
                DocumentNumber = LogNumber,
                DateExecution = OperationDate,
                ArticulId = SelectedArticul.ArticulId,
                ArticulName = SelectedArticul.ArticulName,
                ExecutionTime = BillRules.FormatDouble(operationTime),
                Count = Count,
                OperationId = SelectedOperation.OperationId,
                OperationName = SelectedOperation.OperationName,
                WorkDay = Workday
            });
        }

        private void Delete()
        {
            if (SelectedRow != null)
            {
                Rows.Remove(SelectedRow);
            }
        }

        private void Calculate()
        {
            if (!BillRules.TryParseDouble(OperationTime, out var operationTime))
            {
                _dialogService.ShowError(CalculateErrorMessage, ErrorTitle);
                return;
            }

            var percentage = 0d;

            if (SelectedIdleOperation != null)
            {
                try
                {
                    percentage = _repository.GetIdlePercentage(SelectedIdleOperation.IdleOperationId) / 100d;
                }
                catch (Exception exception)
                {
                    _dialogService.ShowError(exception.Message, ErrorTitle);
                }
            }

            OperationTime = BillRules.FormatDouble(BillRules.CalculateExecutionTime(operationTime, percentage));
        }

        private void Save()
        {
            var entries = new List<OperationLogEntry>(Rows.Count);

            foreach (var row in Rows)
            {
                if (!BillRules.TryParseLegacyInt32(row.ExecutionTime, out var executionTime))
                {
                    _dialogService.ShowError(ExecutionTimeFormatMessage, ErrorTitle);
                    return;
                }

                if (!BillRules.TryParseLegacyInt32(row.Count, out var count))
                {
                    _dialogService.ShowError(CountFormatMessage, ErrorTitle);
                    return;
                }

                if (!BillRules.TryParseLegacyInt32(row.WorkDay, out var workDay))
                {
                    _dialogService.ShowError(WorkdayFormatMessage, ErrorTitle);
                    return;
                }

                entries.Add(new OperationLogEntry
                {
                    EmployeeId = row.EmployeeId,
                    OperationId = row.OperationId,
                    ArticulId = row.ArticulId,
                    DateExecution = row.DateExecution.Date,
                    ExecutionTime = executionTime,
                    Count = count,
                    DocumentNumber = row.DocumentNumber,
                    WorkDay = workDay
                });
            }

            try
            {
                _repository.SaveOperationLog(entries);
                _dialogService.ShowInfo(SavedMessage, SaveTitle);
                Rows.Clear();
                SelectedRow = null;
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }

        private void Close()
        {
            _navigationService.Navigate(new MessageViewModel("ProdControl", "Выберите раздел в меню."));
        }
    }
}
