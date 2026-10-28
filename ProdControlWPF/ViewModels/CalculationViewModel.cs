using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ProdControlWPF.Data;
using ProdControlWPF.Domain;
using ProdControlWPF.Infrastructure;
using ProdControlWPF.Models;
using ProdControlWPF.Services;

namespace ProdControlWPF.ViewModels
{
    public sealed class CalculationViewModel : ObservableObject
    {
        private const string ErrorTitle = "Ошибка";
        private const string WarningTitle = "Предупреждение";
        private const string NotificationTitle = "Уведомление";
        private const string LoadErrorPrefix = "Ошибка при загрузке данных: ";
        private const string PositionErrorPrefix = "Ошибка при определении должности: ";
        private const string KnittingErrorPrefix = "Ошибка расчета для вязальщицы: ";
        private const string SaveErrorPrefix = "Ошибка при сохранении файла: ";
        private const string NoSelectionMessage = "Выберите сотрудника для расчета";
        private const string SaveSuccessFormat = "Файл {0} сохранен.";
        private const string ExportFilter =
            "xlsx files (*.xlsx)|*.xlsx|CSV files (*.csv)|*.csv|All files (*.*)|*.*";

        private readonly ICalculationRepository _repository;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        private bool _isByPeriod;
        private DateTime? _dateFrom = new DateTime(2019, 7, 6);
        private DateTime? _dateTo = DateTime.Today;

        public CalculationViewModel(
            ICalculationRepository repository,
            IDialogService dialogService,
            INavigationService navigationService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            Rows = new ObservableCollection<CalculationRow>();
            SelectedRows = new List<CalculationRow>();

            CalculateCommand = new RelayCommand(Calculate);
            ExportCommand = new RelayCommand(Export);
            CloseCommand = new RelayCommand(Close);

            LoadEmployees();
        }

        public ObservableCollection<CalculationRow> Rows { get; }

        public IList<CalculationRow> SelectedRows { get; set; }

        public ICommand CalculateCommand { get; }

        public ICommand ExportCommand { get; }

        public ICommand CloseCommand { get; }

        public bool IsByPeriod
        {
            get { return _isByPeriod; }
            set { SetProperty(ref _isByPeriod, value); }
        }

        public DateTime? DateFrom
        {
            get { return _dateFrom; }
            set { SetProperty(ref _dateFrom, value); }
        }

        public DateTime? DateTo
        {
            get { return _dateTo; }
            set { SetProperty(ref _dateTo, value); }
        }

        private void LoadEmployees()
        {
            try
            {
                var employees = _repository.GetEmployees();

                Rows.Clear();
                foreach (var employee in employees)
                {
                    Rows.Add(employee);
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(LoadErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void Calculate()
        {
            var selected = SelectedRows == null
                ? new List<CalculationRow>()
                : SelectedRows.ToList();

            if (selected.Count == 0)
            {
                _dialogService.ShowWarning(NoSelectionMessage, WarningTitle);
                return;
            }

            var dateFrom = (DateFrom ?? DateTime.Today).Date;
            var dateTo = (DateTo ?? DateTime.Today).Date;

            foreach (var row in selected)
            {
                CalculateRow(row, dateFrom, dateTo);
            }
        }

        private void CalculateRow(CalculationRow row, DateTime dateFrom, DateTime dateTo)
        {
            var positionId = 0;

            try
            {
                positionId = _repository.GetPositionId(row.EmployeeId);
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(PositionErrorPrefix + exception.Message, ErrorTitle);
            }

            if (positionId == CalculationRules.KnittingPositionId)
            {
                CalculateKnittingRow(row, dateFrom, dateTo);
                return;
            }

            CalculateSewingRow(row, dateFrom, dateTo);
        }

        private void CalculateKnittingRow(CalculationRow row, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var zones = _repository.GetKnittingZoneTimes(
                    row.EmployeeId,
                    IsByPeriod,
                    dateFrom,
                    dateTo);

                var totalTime = CalculationRules.CalculateKnittingSalary(zones);
                var workDay = GetWorkDay(row.EmployeeId, dateFrom, dateTo);
                var norm = CalculationRules.CalculateNorm(totalTime, workDay);

                row.Salary = CalculationRules.FormatRoundedFixed2(totalTime);
                row.Norm = CalculationRules.FormatFixed2(norm * 100);
                row.TimeExecution = CalculationRules.FormatFixed2(totalTime);
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(KnittingErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void CalculateSewingRow(CalculationRow row, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var salaryEntries = _repository.GetSewingSalaryEntries(
                    row.EmployeeId,
                    IsByPeriod,
                    dateFrom,
                    dateTo);

                var tariffPerMinute = GetTariffPerMinute(row.EmployeeId, salaryEntries);
                var salary = CalculationRules.CalculateSewingSalary(salaryEntries, tariffPerMinute);

                row.Salary = CalculationRules.FormatSewingSalary(salary);
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }

            try
            {
                var timeEntries = _repository.GetSewingTimeEntries(
                    row.EmployeeId,
                    IsByPeriod,
                    dateFrom,
                    dateTo);

                var totalTime = CalculationRules.CalculateSewingTime(
                    timeEntries,
                    _repository.GetRoutingTime);

                var workDay = GetWorkDay(row.EmployeeId, dateFrom, dateTo);
                var norm = CalculationRules.CalculateNorm(totalTime, workDay);

                row.Norm = CalculationRules.FormatFixed2(norm * 100);
                row.TimeExecution = CalculationRules.FormatFixed2(totalTime);
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }

        private double GetTariffPerMinute(
            int employeeId,
            IReadOnlyList<SewingSalaryEntry> salaryEntries)
        {
            if (!salaryEntries.Any(entry => entry.ArticulId == CalculationRules.NonRatedArticulId))
            {
                return 0;
            }

            try
            {
                return _repository.GetTariffPerMinute(employeeId);
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
                return 0;
            }
        }

        private int GetWorkDay(int employeeId, DateTime dateFrom, DateTime dateTo)
        {
            if (IsByPeriod)
            {
                var entries = _repository.GetPeriodWorkDays(employeeId, dateFrom, dateTo);

                return entries.Count == 0
                    ? CalculationRules.DefaultWorkDay
                    : CalculationRules.CalculatePeriodWorkDay(entries);
            }

            var workDay = _repository.GetWorkDayForDate(employeeId, dateFrom);

            return workDay ?? CalculationRules.DefaultWorkDay;
        }

        private void Export()
        {
            var fileName = _dialogService.SaveFile(ExportFilter, null);

            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            try
            {
                using (var writer = new StreamWriter(fileName, false, new UTF8Encoding(true)))
                {
                    writer.WriteLine(BuildCsvLine(GetHeaderValues()));

                    foreach (var row in SelectedRows ?? new List<CalculationRow>())
                    {
                        writer.WriteLine(BuildCsvLine(GetRowValues(row)));
                    }
                }

                _dialogService.ShowInfo(string.Format(SaveSuccessFormat, fileName), NotificationTitle);
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(SaveErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private static string[] GetHeaderValues()
        {
            return new[]
            {
                "Номер сотрудника",
                "Имя сотрудника",
                "Табельный номер",
                "Должность",
                "Разряд",
                "Заработная плата",
                "Норма Выроботки",
                "Время выполнения"
            };
        }

        private static string[] GetRowValues(CalculationRow row)
        {
            return new[]
            {
                row.EmployeeId.ToString(CultureInfo.CurrentCulture),
                row.EmployeeName ?? string.Empty,
                row.EmployeeNumber ?? string.Empty,
                row.PositionId.ToString(CultureInfo.CurrentCulture),
                row.PositionLevelId.ToString(CultureInfo.CurrentCulture),
                row.Salary ?? string.Empty,
                row.Norm ?? string.Empty,
                row.TimeExecution ?? string.Empty
            };
        }

        private static string BuildCsvLine(IEnumerable<string> values)
        {
            return string.Join(";", values);
        }

        private void Close()
        {
            _navigationService.Navigate(new MessageViewModel("ProdControl", "Выберите раздел в меню."));
        }
    }
}
