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
    public sealed class KnittingSearchInvoiceViewModel : ObservableObject
    {
        private const string ErrorTitle = "Ошибка";
        private const string InfoTitle = "Информация";
        private const string SuccessTitle = "Успех";
        private const string ConfirmationTitle = "Подтверждение";
        private const string ConnectionErrorPrefix = "Ошибка подключения к БД: ";
        private const string SearchErrorPrefix = "Ошибка при поиске данных: ";
        private const string DeleteErrorPrefix = "Ошибка при удалении из БД: ";
        private const string SaveErrorPrefix = "Ошибка при сохранении изменений: ";
        private const string NoRecordsMessage = "Записи по заданным критериям не найдены.";
        private const string NoChangesMessage = "Нет измененных данных для сохранения.";
        private const string DeletedMessage = "Запись успешно удалена!";
        private const string DeleteConfirmationMessage = "Вы действительно хотите удалить выбранную запись?";

        private readonly IKnittingSearchRepository _repository;
        private readonly IDialogService _dialogService;
        private readonly Dictionary<int, EmployeeLookup> _employeesById =
            new Dictionary<int, EmployeeLookup>();
        private readonly Dictionary<int, ArticulLookup> _articulsById =
            new Dictionary<int, ArticulLookup>();
        private readonly Dictionary<int, ClothingPartLookup> _partsById =
            new Dictionary<int, ClothingPartLookup>();
        private readonly Dictionary<int, KnittingMachineLookup> _machinesById =
            new Dictionary<int, KnittingMachineLookup>();

        private EmployeeLookup _selectedEmployee;
        private ArticulLookup _selectedArticul;
        private KnittingSearchRow _selectedRow;
        private int _selectedFilterIndex;
        private bool _isByPeriod;
        private bool _isEditingMode;
        private bool _trackRowChanges;
        private DateTime _dateFrom = DateTime.Today;
        private DateTime _dateTo = DateTime.Today;
        private string _documentNumber = string.Empty;

        public KnittingSearchInvoiceViewModel(
            IKnittingSearchRepository repository,
            IDialogService dialogService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Employees = new ObservableCollection<EmployeeLookup>();
            Articuls = new ObservableCollection<ArticulLookup>();
            Rows = new ObservableCollection<KnittingSearchRow>();

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

        public ObservableCollection<KnittingSearchRow> Rows { get; }

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

        public KnittingSearchRow SelectedRow
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
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(ConnectionErrorPrefix + exception.Message, ErrorTitle);
                return;
            }

            try
            {
                foreach (var articul in _repository.GetArticuls())
                {
                    Articuls.Add(articul);
                    _articulsById[articul.ArticulId] = articul;
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError("Ошибка в справочнике Articul: " + exception.Message, ErrorTitle);
            }

            try
            {
                foreach (var part in _repository.GetParts())
                {
                    _partsById[part.PartId] = part;
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError("Ошибка в справочнике ClothingParts: " + exception.Message, ErrorTitle);
            }

            try
            {
                foreach (var machine in _repository.GetMachines())
                {
                    _machinesById[machine.MachineId] = machine;
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError("Ошибка в справочнике ОБОРУДОВАНИЕ: " + exception.Message, ErrorTitle);
            }

            SelectedEmployee = Employees.FirstOrDefault();
            SelectedArticul = Articuls.FirstOrDefault();
            DateFrom = DateTime.Today;
            DateTo = DateTime.Today;
        }

        private void Search()
        {
            var mode = (KnittingSearchFilterMode)_selectedFilterIndex;
            var criteria = new KnittingSearchCriteria
            {
                FilterMode = mode,
                UsePeriod = _isByPeriod,
                DateFrom = _dateFrom,
                DateTo = _dateTo,
                DocumentNumber = _documentNumber
            };

            if (mode == KnittingSearchFilterMode.Employee
                || mode == KnittingSearchFilterMode.EmployeeAndArticul)
            {
                criteria.EmployeeId = SelectedEmployee == null
                    ? (int?)null
                    : SelectedEmployee.EmployeeId;
            }

            if (mode == KnittingSearchFilterMode.Articul
                || mode == KnittingSearchFilterMode.EmployeeAndArticul)
            {
                criteria.ArticulId = SelectedArticul == null
                    ? (int?)null
                    : SelectedArticul.ArticulId;
            }

            LoadRows(() => _repository.Search(criteria));
        }

        private void SearchAll()
        {
            LoadRows(_repository.GetAll);
        }

        private void LoadRows(Func<IReadOnlyList<KnittingSearchRow>> loader)
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

                if (Rows.Count == 0)
                {
                    _dialogService.ShowInfo(NoRecordsMessage, InfoTitle);
                }
            }
            catch (Exception exception)
            {
                Rows.Clear();
                SelectedRow = null;
                _dialogService.ShowError(SearchErrorPrefix + exception.Message, ErrorTitle);
            }
            finally
            {
                _trackRowChanges = true;
            }
        }

        private void ApplyLookupNames(KnittingSearchRow row)
        {
            row.EmployeeName = row.EmployeeId.HasValue
                && _employeesById.TryGetValue(row.EmployeeId.Value, out var employee)
                    ? employee.EmployeeName
                    : string.Empty;

            row.ArticulName = row.ArticulId.HasValue
                && _articulsById.TryGetValue(row.ArticulId.Value, out var articul)
                    ? articul.ArticulName
                    : string.Empty;

            row.PartName = row.PartId.HasValue
                && _partsById.TryGetValue(row.PartId.Value, out var part)
                    ? part.PartName
                    : string.Empty;

            row.MachineName = row.MachineId.HasValue
                && _machinesById.TryGetValue(row.MachineId.Value, out var machine)
                    ? machine.DisplayName
                    : string.Empty;
        }

        private void OnRowPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_trackRowChanges || e.PropertyName == nameof(KnittingSearchRow.IsDirty))
            {
                return;
            }

            if (e.PropertyName != nameof(KnittingSearchRow.DateExecution)
                && e.PropertyName != nameof(KnittingSearchRow.TimeExecution)
                && e.PropertyName != nameof(KnittingSearchRow.Count)
                && e.PropertyName != nameof(KnittingSearchRow.WorkDay))
            {
                return;
            }

            if (sender is KnittingSearchRow row)
            {
                row.IsDirty = true;
            }
        }

        private void Delete()
        {
            if (!_isEditingMode)
            {
                return;
            }

            var row = _selectedRow;

            if (row == null)
            {
                return;
            }

            if (!_dialogService.Confirm(DeleteConfirmationMessage, ConfirmationTitle, DialogIcon.Question))
            {
                return;
            }

            try
            {
                _repository.Delete(row.LogId);
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(DeleteErrorPrefix + exception.Message, ErrorTitle);
                return;
            }

            row.PropertyChanged -= OnRowPropertyChanged;
            var index = Rows.IndexOf(row);

            if (index >= 0)
            {
                Rows.RemoveAt(index);
            }

            SelectedRow = Rows.Count == 0
                ? null
                : Rows[Math.Max(0, Math.Min(index, Rows.Count - 1))];

            _dialogService.ShowInfo(DeletedMessage, SuccessTitle);
        }

        private void Save()
        {
            if (!_isEditingMode)
            {
                return;
            }

            var dirtyRows = Rows.Where(row => row.IsDirty).ToList();

            if (dirtyRows.Count == 0)
            {
                _dialogService.ShowInfo(NoChangesMessage, InfoTitle);
                return;
            }

            try
            {
                _repository.SaveUpdates(dirtyRows.Select(BuildUpdate).ToList());
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(SaveErrorPrefix + exception.Message, ErrorTitle);
                return;
            }

            foreach (var row in dirtyRows)
            {
                row.IsDirty = false;
            }

            _dialogService.ShowInfo(
                string.Format("Успешно обновлено записей: {0}", dirtyRows.Count),
                SuccessTitle);
        }

        private static KnittingSearchUpdate BuildUpdate(KnittingSearchRow row)
        {
            return new KnittingSearchUpdate
            {
                LogId = row.LogId,
                DocumentNumber = string.IsNullOrWhiteSpace(row.DocumentNumber)
                    ? null
                    : row.DocumentNumber.Trim(),
                ArticulId = row.ArticulId,
                EmployeeId = row.EmployeeId,
                PartId = row.PartId,
                Amount = ParseIntOrZero(row.Count),
                MachineId = row.MachineId,
                DateExecution = row.DateExecution,
                TimeExecution = ParseTime(row.TimeExecution),
                MachinesInZone = ParseIntOrZero(row.MachinesInZone),
                WorkDay = ParseIntOrZero(row.WorkDay)
            };
        }

        private static int ParseIntOrZero(string value)
        {
            return int.TryParse(value, NumberStyles.Integer, CultureInfo.CurrentCulture, out var result)
                ? result
                : 0;
        }

        private static double? ParseTime(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var normalized = value.Replace(',', '.');

            return double.TryParse(
                normalized,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var result)
                ? result
                : (double?)null;
        }
    }
}
