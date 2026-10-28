using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ProdControlWPF.Data;
using ProdControlWPF.Infrastructure;
using ProdControlWPF.Models;
using ProdControlWPF.Services;

namespace ProdControlWPF.ViewModels
{
    public sealed class ReportViewerViewModel : ObservableObject
    {
        private const string ErrorTitle = "Ошибка";
        private const string WarningTitle = "Предупреждение";
        private const string LoadErrorPrefix = "Ошибка при загрузке данных: ";
        private const string ReportErrorPrefix = "Ошибка при формировании отчета: ";
        private const string NoSelectionMessage = "Выберите сотрудника для расчета";
        private const string InvalidPeriodMessage =
            "Дата начала периода не может быть больше даты конца периода.";
        private const string NoDataMessage = "Нет данных по Сотруднику.";

        private readonly IReportRepository _repository;
        private readonly IDialogService _dialogService;
        private bool _isReportVisible;
        private DateTime? _dateFrom = DateTime.Today;
        private DateTime? _dateTo = DateTime.Today;
        private IReadOnlyList<EmployeeReportRow> _reportRows = new List<EmployeeReportRow>();

        public ReportViewerViewModel(IReportRepository repository, IDialogService dialogService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Employees = new ObservableCollection<ReportEmployeeListItem>();
            SelectedEmployees = new List<ReportEmployeeListItem>();

            BuildReportCommand = new RelayCommand(BuildReport);
            BackCommand = new RelayCommand(Back);

            LoadEmployees();
        }

        public ObservableCollection<ReportEmployeeListItem> Employees { get; }

        public IList<ReportEmployeeListItem> SelectedEmployees { get; set; }

        public ICommand BuildReportCommand { get; }

        public ICommand BackCommand { get; }

        public event EventHandler ReportReady;

        public bool IsReportVisible
        {
            get { return _isReportVisible; }
            private set
            {
                if (SetProperty(ref _isReportVisible, value))
                {
                    OnPropertyChanged(nameof(IsEmployeeListVisible));
                }
            }
        }

        public bool IsEmployeeListVisible
        {
            get { return !_isReportVisible; }
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

        public IReadOnlyList<EmployeeReportRow> ReportRows
        {
            get { return _reportRows; }
            private set
            {
                _reportRows = value ?? new List<EmployeeReportRow>();
                OnPropertyChanged();
            }
        }

        private void LoadEmployees()
        {
            try
            {
                var employees = _repository.GetEmployees();

                Employees.Clear();
                foreach (var employee in employees)
                {
                    Employees.Add(employee);
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(LoadErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void BuildReport()
        {
            var selected = SelectedEmployees == null
                ? new List<ReportEmployeeListItem>()
                : new List<ReportEmployeeListItem>(SelectedEmployees);

            if (selected.Count == 0)
            {
                _dialogService.ShowWarning(NoSelectionMessage, WarningTitle);
                return;
            }

            var dateFrom = (DateFrom ?? DateTime.Today).Date;
            var dateTo = (DateTo ?? DateTime.Today).Date;

            if (dateFrom > dateTo)
            {
                _dialogService.ShowWarning(InvalidPeriodMessage, WarningTitle);
                return;
            }

            var ids = new List<int>();
            foreach (var employee in selected)
            {
                if (!ids.Contains(employee.EmployeeId))
                {
                    ids.Add(employee.EmployeeId);
                }
            }

            try
            {
                var rows = _repository.GetEmployeeReport(ids, dateFrom, dateTo);

                if (rows == null || rows.Count == 0)
                {
                    _dialogService.ShowWarning(NoDataMessage, WarningTitle);
                    return;
                }

                ReportRows = rows;
                IsReportVisible = true;
                OnReportReady();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(ReportErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void Back()
        {
            IsReportVisible = false;
        }

        private void OnReportReady()
        {
            ReportReady?.Invoke(this, EventArgs.Empty);
        }
    }
}
