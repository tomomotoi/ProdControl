using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using ProdControlWPF.Data;
using ProdControlWPF.Infrastructure;
using ProdControlWPF.Models;
using ProdControlWPF.Services;

namespace ProdControlWPF.ViewModels
{
    public sealed class NewKnittingInvoiceViewModel : ObservableObject
    {
        private const string RequiredFieldsMessage = "Заполните все обязательные поля перед добавлением!";
        private const string WarningTitle = "Внимание";
        private const string DeleteInfoMessage = "Выберите строку для удаления!";
        private const string InfoTitle = "Информация";
        private const string EmptyTableMessage = "Таблица пуста. Нет данных для сохранения!";
        private const string SavedMessage = "Данные успешно сохранены!";
        private const string SuccessTitle = "Успех";
        private const string SaveErrorPrefix = "Ошибка при сохранении данных: ";
        private const string ErrorTitle = "Ошибка";
        private const string AmountFormatMessage = "Неверный формат числа в поле 'Количество'!";
        private const string ExecutionTimeFormatMessage = "Неверный формат числа в поле 'Время выполнения'!";
        private const string MachinesInZoneFormatMessage = "Неверный формат числа в поле 'Машин в зоне обслуживания'!";
        private const string WorkdayFormatMessage = "Неверный формат числа в поле 'Рабочий день'!";

        private readonly IKnittingInvoiceRepository _repository;
        private readonly IDialogService _dialogService;

        private EmployeeLookup _selectedEmployee;
        private ArticulLookup _selectedArticul;
        private ClothingPartLookup _selectedPart;
        private KnittingMachineLookup _selectedMachine;
        private KnittingInvoiceRow _selectedRow;
        private DateTime _operationDate = DateTime.Today;
        private string _receiptNumber = string.Empty;
        private string _count = string.Empty;
        private string _executionTime = string.Empty;
        private string _machinesInZone = string.Empty;
        private string _workday = string.Empty;

        public NewKnittingInvoiceViewModel(
            IKnittingInvoiceRepository repository,
            IDialogService dialogService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Employees = new ObservableCollection<EmployeeLookup>();
            Articuls = new ObservableCollection<ArticulLookup>();
            Parts = new ObservableCollection<ClothingPartLookup>();
            Machines = new ObservableCollection<KnittingMachineLookup>();
            Rows = new ObservableCollection<KnittingInvoiceRow>();

            InsertCommand = new RelayCommand(Insert);
            DeleteCommand = new RelayCommand(Delete);
            SaveCommand = new RelayCommand(Save);

            Load();
        }

        public ObservableCollection<EmployeeLookup> Employees { get; }

        public ObservableCollection<ArticulLookup> Articuls { get; }

        public ObservableCollection<ClothingPartLookup> Parts { get; }

        public ObservableCollection<KnittingMachineLookup> Machines { get; }

        public ObservableCollection<KnittingInvoiceRow> Rows { get; }

        public ICommand InsertCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand SaveCommand { get; }

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

        public ClothingPartLookup SelectedPart
        {
            get { return _selectedPart; }
            set { SetProperty(ref _selectedPart, value); }
        }

        public KnittingMachineLookup SelectedMachine
        {
            get { return _selectedMachine; }
            set { SetProperty(ref _selectedMachine, value); }
        }

        public KnittingInvoiceRow SelectedRow
        {
            get { return _selectedRow; }
            set { SetProperty(ref _selectedRow, value); }
        }

        public DateTime OperationDate
        {
            get { return _operationDate; }
            set { SetProperty(ref _operationDate, value); }
        }

        public string ReceiptNumber
        {
            get { return _receiptNumber; }
            set { SetProperty(ref _receiptNumber, value ?? string.Empty); }
        }

        public string Count
        {
            get { return _count; }
            set { SetProperty(ref _count, value ?? string.Empty); }
        }

        public string ExecutionTime
        {
            get { return _executionTime; }
            set { SetProperty(ref _executionTime, value ?? string.Empty); }
        }

        public string MachinesInZone
        {
            get { return _machinesInZone; }
            set { SetProperty(ref _machinesInZone, value ?? string.Empty); }
        }

        public string Workday
        {
            get { return _workday; }
            set { SetProperty(ref _workday, value ?? string.Empty); }
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

                foreach (var part in _repository.GetParts())
                {
                    Parts.Add(part);
                }

                foreach (var machine in _repository.GetMachines())
                {
                    Machines.Add(machine);
                }

                SelectedEmployee = Employees.FirstOrDefault();
                SelectedArticul = Articuls.FirstOrDefault();
                SelectedPart = Parts.FirstOrDefault();
                SelectedMachine = Machines.FirstOrDefault();
                OperationDate = DateTime.Today;
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }

        private void Insert()
        {
            if (SelectedEmployee == null
                || SelectedArticul == null
                || SelectedPart == null
                || SelectedMachine == null
                || string.IsNullOrWhiteSpace(ReceiptNumber)
                || string.IsNullOrWhiteSpace(Count)
                || string.IsNullOrWhiteSpace(Workday))
            {
                _dialogService.ShowWarning(RequiredFieldsMessage, WarningTitle);
                return;
            }

            Rows.Add(new KnittingInvoiceRow
            {
                EmployeeId = SelectedEmployee.EmployeeId,
                EmployeeName = SelectedEmployee.EmployeeName,
                DocumentNumber = ReceiptNumber,
                DateExecution = OperationDate.Date,
                ArticulId = SelectedArticul.ArticulId,
                ArticulName = SelectedArticul.ArticulName,
                PartId = SelectedPart.PartId,
                PartName = SelectedPart.PartName,
                MachineId = SelectedMachine.MachineId,
                MachineName = SelectedMachine.DisplayName,
                Amount = Count,
                ExecutionTime = string.IsNullOrWhiteSpace(ExecutionTime)
                    ? "0"
                    : NormalizeDecimalText(ExecutionTime),
                MachinesInZone = string.IsNullOrWhiteSpace(MachinesInZone) ? "0" : MachinesInZone,
                WorkDay = Workday
            });
        }

        private void Delete()
        {
            if (SelectedRow == null)
            {
                _dialogService.ShowInfo(DeleteInfoMessage, InfoTitle);
                return;
            }

            Rows.Remove(SelectedRow);
            SelectedRow = null;
        }

        private void Save()
        {
            if (Rows.Count == 0)
            {
                _dialogService.ShowWarning(EmptyTableMessage, WarningTitle);
                return;
            }

            var entries = new List<KnittingOperationLogEntry>(Rows.Count);

            foreach (var row in Rows)
            {
                if (!int.TryParse(row.Amount, NumberStyles.Integer, CultureInfo.CurrentCulture, out var amount))
                {
                    _dialogService.ShowError(AmountFormatMessage, ErrorTitle);
                    return;
                }

                if (!TryParseDouble(row.ExecutionTime, out var executionTime))
                {
                    _dialogService.ShowError(ExecutionTimeFormatMessage, ErrorTitle);
                    return;
                }

                if (!int.TryParse(row.MachinesInZone, NumberStyles.Integer, CultureInfo.CurrentCulture, out var machinesInZone))
                {
                    _dialogService.ShowError(MachinesInZoneFormatMessage, ErrorTitle);
                    return;
                }

                if (!int.TryParse(row.WorkDay, NumberStyles.Integer, CultureInfo.CurrentCulture, out var workDay))
                {
                    _dialogService.ShowError(WorkdayFormatMessage, ErrorTitle);
                    return;
                }

                entries.Add(new KnittingOperationLogEntry
                {
                    DocumentNumber = row.DocumentNumber,
                    ArticulId = row.ArticulId,
                    EmployeeId = row.EmployeeId,
                    PartId = row.PartId,
                    MachineId = row.MachineId,
                    Amount = amount,
                    DateExecution = row.DateExecution.Date,
                    ExecutionTime = executionTime,
                    MachinesInZone = machinesInZone,
                    WorkDay = workDay
                });
            }

            try
            {
                _repository.SaveOperationLog(entries);
                _dialogService.ShowInfo(SavedMessage, SuccessTitle);
                Rows.Clear();
                SelectedRow = null;
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(SaveErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private static bool TryParseDouble(string text, out double value)
        {
            return double.TryParse(
                NormalizeDecimalText(text),
                NumberStyles.Float,
                CultureInfo.CurrentCulture,
                out value);
        }

        private static string NormalizeDecimalText(string text)
        {
            var normalized = (text ?? string.Empty).Trim();
            var separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            if (separator == ".")
            {
                return normalized.Replace(',', '.');
            }

            if (separator == ",")
            {
                return normalized.Replace('.', ',');
            }

            return normalized;
        }
    }
}
