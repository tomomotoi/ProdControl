using System;
using System.Windows.Input;
using ProdControlWPF.Infrastructure;
using ProdControlWPF.Models;
using ProdControlWPF.Services;

namespace ProdControlWPF.ViewModels
{
    public sealed class MainWindowViewModel : ObservableObject
    {
        private const string NotImplementedMessage = "Раздел ещё не портирован.";
        private const string NewBillMenuTitle = "Новая (Швейный цех)";
        private const string SearchBillMenuTitle = "Найти (Швейный цех)";
        private const string NewKnittingInvoiceMenuTitle = "Новая (Вязальный цех)";
        private const string KnittingSearchInvoiceMenuTitle = "Найти (Вязальный цех)";
        private const string ArticulMenuTitle = "Артикул";
        private const string OperationMenuTitle = "Операция";
        private const string EmployeesMenuTitle = "Сотрудники";
        private const string CoefficientsMenuTitle = "Коэффициенты доп. затрат";
        private const string DetailsMenuTitle = "Детали изделий";
        private const string MachinesMenuTitle = "Оборудование";
        private const string RoutingMenuTitle = "Тех.карта";
        private const string KnittingRoutingMenuTitle = "Тех.карта (Вязание)";
        private const string TariffsCheckMenuTitle = "Проверка расценок работ";
        private const string CalculationMenuTitle = "Норма выработки";
        private const string ReportViewerMenuTitle = "Отчет по сотруднику";
        private const string WorkOrdersActiveMenuTitle = "Наряды → Действующие";
        private const string WorkOrdersCompletedMenuTitle = "Наряды → Выполненные";

        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly IDatabaseMaintenanceService _maintenanceService;
        private readonly IApplicationService _applicationService;
        private readonly Func<NewBillViewModel> _newBillFactory;
        private readonly Func<SearchBillViewModel> _searchBillFactory;
        private readonly Func<NewKnittingInvoiceViewModel> _newKnittingInvoiceFactory;
        private readonly Func<KnittingSearchInvoiceViewModel> _knittingSearchInvoiceFactory;
        private readonly Func<NewArticulViewModel> _newArticulFactory;
        private readonly Func<OperationViewModel> _operationFactory;
        private readonly Func<EmployeesViewModel> _employeesFactory;
        private readonly Func<CoefficientsViewModel> _coefficientsFactory;
        private readonly Func<DetailsViewModel> _detailsFactory;
        private readonly Func<MachinesViewModel> _machinesFactory;
        private readonly Func<RoutingViewModel> _routingFactory;
        private readonly Func<KnittingRoutingViewModel> _knittingRoutingFactory;
        private readonly Func<TariffsCheckViewModel> _tariffsCheckFactory;
        private readonly Func<CalculationViewModel> _calculationFactory;
        private readonly Func<ReportViewerViewModel> _reportViewerFactory;
        private readonly Func<WorkOrderMode, WorkOrdersViewModel> _workOrdersFactory;
        private string _statusMessage;

        public MainWindowViewModel(
            INavigationService navigationService,
            IDialogService dialogService,
            IDatabaseMaintenanceService maintenanceService,
            IApplicationService applicationService,
            Func<NewBillViewModel> newBillFactory,
            Func<SearchBillViewModel> searchBillFactory,
            Func<NewKnittingInvoiceViewModel> newKnittingInvoiceFactory,
            Func<KnittingSearchInvoiceViewModel> knittingSearchInvoiceFactory,
            Func<NewArticulViewModel> newArticulFactory,
            Func<OperationViewModel> operationFactory,
            Func<EmployeesViewModel> employeesFactory,
            Func<CoefficientsViewModel> coefficientsFactory,
            Func<DetailsViewModel> detailsFactory,
            Func<MachinesViewModel> machinesFactory,
            Func<RoutingViewModel> routingFactory,
            Func<KnittingRoutingViewModel> knittingRoutingFactory,
            Func<TariffsCheckViewModel> tariffsCheckFactory,
            Func<CalculationViewModel> calculationFactory,
            Func<ReportViewerViewModel> reportViewerFactory,
            Func<WorkOrderMode, WorkOrdersViewModel> workOrdersFactory)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _maintenanceService = maintenanceService ?? throw new ArgumentNullException(nameof(maintenanceService));
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
            _newBillFactory = newBillFactory ?? throw new ArgumentNullException(nameof(newBillFactory));
            _searchBillFactory = searchBillFactory ?? throw new ArgumentNullException(nameof(searchBillFactory));
            _newKnittingInvoiceFactory = newKnittingInvoiceFactory
                ?? throw new ArgumentNullException(nameof(newKnittingInvoiceFactory));
            _knittingSearchInvoiceFactory = knittingSearchInvoiceFactory
                ?? throw new ArgumentNullException(nameof(knittingSearchInvoiceFactory));
            _newArticulFactory = newArticulFactory
                ?? throw new ArgumentNullException(nameof(newArticulFactory));
            _operationFactory = operationFactory
                ?? throw new ArgumentNullException(nameof(operationFactory));
            _employeesFactory = employeesFactory
                ?? throw new ArgumentNullException(nameof(employeesFactory));
            _coefficientsFactory = coefficientsFactory
                ?? throw new ArgumentNullException(nameof(coefficientsFactory));
            _detailsFactory = detailsFactory
                ?? throw new ArgumentNullException(nameof(detailsFactory));
            _machinesFactory = machinesFactory
                ?? throw new ArgumentNullException(nameof(machinesFactory));
            _routingFactory = routingFactory
                ?? throw new ArgumentNullException(nameof(routingFactory));
            _knittingRoutingFactory = knittingRoutingFactory
                ?? throw new ArgumentNullException(nameof(knittingRoutingFactory));
            _tariffsCheckFactory = tariffsCheckFactory
                ?? throw new ArgumentNullException(nameof(tariffsCheckFactory));
            _calculationFactory = calculationFactory
                ?? throw new ArgumentNullException(nameof(calculationFactory));
            _reportViewerFactory = reportViewerFactory
                ?? throw new ArgumentNullException(nameof(reportViewerFactory));
            _workOrdersFactory = workOrdersFactory
                ?? throw new ArgumentNullException(nameof(workOrdersFactory));

            _statusMessage = "Готово";

            OpenScreenCommand = new RelayCommand<string>(OpenScreen);
            ClearOperationLogCommand = new RelayCommand(ClearOperationLog);
            BackupDatabaseCommand = new RelayCommand(_maintenanceService.BackupCurrentDatabase);
            RestoreFromFileCommand = new RelayCommand(RestoreFromFile);
            RestoreLatestCommand = new RelayCommand(RestoreLatest);
            ClearArchiveCommand = new RelayCommand(_maintenanceService.ClearArchive);
            ExitCommand = new RelayCommand(_applicationService.Exit);
        }

        public string StatusMessage
        {
            get { return _statusMessage; }
            private set { SetProperty(ref _statusMessage, value); }
        }

        public ICommand OpenScreenCommand { get; }
        public ICommand ClearOperationLogCommand { get; }
        public ICommand BackupDatabaseCommand { get; }
        public ICommand RestoreFromFileCommand { get; }
        public ICommand RestoreLatestCommand { get; }
        public ICommand ClearArchiveCommand { get; }
        public ICommand ExitCommand { get; }

        public void ShowWelcome()
        {
            _navigationService.Navigate(new MessageViewModel("ProdControl", "Выберите раздел в меню."));
            StatusMessage = "Готово";
        }

        private void OpenScreen(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return;
            }

            if (title == NewBillMenuTitle)
            {
                _navigationService.Navigate(_newBillFactory());
                StatusMessage = "Новая накладная";
                return;
            }

            if (title == SearchBillMenuTitle)
            {
                _navigationService.Navigate(_searchBillFactory());
                StatusMessage = "Поиск накладных";
                return;
            }

            if (title == NewKnittingInvoiceMenuTitle)
            {
                _navigationService.Navigate(_newKnittingInvoiceFactory());
                StatusMessage = "Новая накладная (вязание)";
                return;
            }

            if (title == KnittingSearchInvoiceMenuTitle)
            {
                _navigationService.Navigate(_knittingSearchInvoiceFactory());
                StatusMessage = "Поиск накладных (вязание)";
                return;
            }

            if (title == ArticulMenuTitle)
            {
                _navigationService.Navigate(_newArticulFactory());
                StatusMessage = "Артикул";
                return;
            }

            if (title == OperationMenuTitle)
            {
                _navigationService.Navigate(_operationFactory());
                StatusMessage = "Операция";
                return;
            }

            if (title == EmployeesMenuTitle)
            {
                _navigationService.Navigate(_employeesFactory());
                StatusMessage = "Сотрудники";
                return;
            }

            if (title == CoefficientsMenuTitle)
            {
                _navigationService.Navigate(_coefficientsFactory());
                StatusMessage = "Коэффициенты доп. затрат";
                return;
            }

            if (title == DetailsMenuTitle)
            {
                _navigationService.Navigate(_detailsFactory());
                StatusMessage = "Детали изделий";
                return;
            }

            if (title == MachinesMenuTitle)
            {
                _navigationService.Navigate(_machinesFactory());
                StatusMessage = "Оборудование";
                return;
            }

            if (title == RoutingMenuTitle)
            {
                _navigationService.Navigate(_routingFactory());
                StatusMessage = "Тех. карта";
                return;
            }

            if (title == KnittingRoutingMenuTitle)
            {
                _navigationService.Navigate(_knittingRoutingFactory());
                StatusMessage = "Тех. карта (вязание)";
                return;
            }

            if (title == TariffsCheckMenuTitle)
            {
                _navigationService.Navigate(_tariffsCheckFactory());
                StatusMessage = "Проверка расценок работ";
                return;
            }

            if (title == CalculationMenuTitle)
            {
                _navigationService.Navigate(_calculationFactory());
                StatusMessage = "Норма выработки";
                return;
            }

            if (title == ReportViewerMenuTitle)
            {
                _navigationService.Navigate(_reportViewerFactory());
                StatusMessage = "Отчет по сотруднику";
                return;
            }

            if (title == WorkOrdersActiveMenuTitle)
            {
                _navigationService.Navigate(_workOrdersFactory(WorkOrderMode.Active));
                StatusMessage = "Наряды: действующие";
                return;
            }

            if (title == WorkOrdersCompletedMenuTitle)
            {
                _navigationService.Navigate(_workOrdersFactory(WorkOrderMode.Completed));
                StatusMessage = "Наряды: выполненные";
                return;
            }

            _navigationService.Navigate(new MessageViewModel(title, NotImplementedMessage));
            StatusMessage = string.Format("{0} — в разработке", title);
        }

        private void ClearOperationLog()
        {
            var confirmed = _dialogService.Confirm(
                "Вы уверены, что хотите очистить таблицу OperationLog, с записями о последних операциях? После подтверждения операция не может быть отменена!",
                "Предупреждение",
                DialogIcon.Warning);

            if (!confirmed)
            {
                return;
            }

            _dialogService.ShowInfo(
                "Перед очисткой таблицы OperationLog будет произведено копирование базы данных.",
                "Уведомление");

            _maintenanceService.BackupCurrentDatabase();
            _maintenanceService.ClearOperationLog();
        }

        private void RestoreFromFile()
        {
            try
            {
                var fileName = _dialogService.OpenFile(
                    "Microsoft Database files (*.mdb)|*.mdb",
                    Environment.CurrentDirectory);

                if (fileName != null)
                {
                    _maintenanceService.RestoreFromFileName(fileName);
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, "Ошибка");
            }
        }

        private void RestoreLatest()
        {
            var confirmed = _dialogService.Confirm(
                "Вы уверены, что хотите восстановить базу данных по актуальной копии? Текущая копия и изменения в ней будут утрачены!",
                "Предупреждение",
                DialogIcon.Warning);

            if (confirmed)
            {
                _maintenanceService.RestoreLatestFromArchive();
            }
        }
    }
}
