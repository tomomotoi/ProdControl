using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using ProdControlWPF.Data;
using ProdControlWPF.Models;
using ProdControlWPF.Services;
using ProdControlWPF.ViewModels;

namespace ProdControlWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigureCulture();

            var connection = ConfigurationManager.ConnectionStrings["OperationDB"];

            if (connection == null || string.IsNullOrWhiteSpace(connection.ConnectionString))
            {
                MessageBox.Show(
                    "Не найдена строка подключения \"OperationDB\" в App.config.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Shutdown();
                return;
            }

            var ordersConnection = ConfigurationManager.ConnectionStrings["OrdersDB"];
            IWorkOrderRepository workOrderRepository = null;

            if (ordersConnection != null
                && !string.IsNullOrWhiteSpace(ordersConnection.ConnectionString))
            {
                var ordersConnectionFactory = new OleDbConnectionFactory(ordersConnection.ConnectionString);
                workOrderRepository = new WorkOrderRepository(ordersConnectionFactory);
            }
            else
            {
                MessageBox.Show(
                    "Не найдена строка подключения \"OrdersDB\" в App.config. Раздел «Наряды» будет недоступен.",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }

            var dialogService = new DialogService();
            var connectionFactory = new OleDbConnectionFactory(connection.ConnectionString);
            var operationLogRepository = new OperationLogRepository(connectionFactory);
            var newBillRepository = new NewBillRepository(connectionFactory);
            var searchBillRepository = new SearchBillRepository(connectionFactory);
            var knittingInvoiceRepository = new KnittingInvoiceRepository(connectionFactory);
            var knittingSearchRepository = new KnittingSearchRepository(connectionFactory);
            var articulRepository = new NewArticulRepository(connectionFactory);
            var operationRepository = new OperationRepository(connectionFactory);
            var employeesRepository = new EmployeesRepository(connectionFactory);
            var coefficientsRepository = new CoefficientsRepository(connectionFactory);
            var detailsRepository = new DetailsRepository(connectionFactory);
            var machinesRepository = new MachinesRepository(connectionFactory);
            var routingRepository = new RoutingRepository(connectionFactory);
            var knittingRoutingRepository = new KnittingRoutingRepository(connectionFactory);
            var tariffsCheckRepository = new TariffsCheckRepository(connectionFactory);
            var calculationRepository = new CalculationRepository(connectionFactory);
            var reportRepository = new ReportRepository(connectionFactory);
            var currentDayStore = new CurrentDayStore(dialogService);
            var maintenanceService = new DatabaseMaintenanceService(
                dialogService,
                operationLogRepository,
                currentDayStore);
            var navigationService = new NavigationService();
            var applicationService = new ApplicationService();
            Func<NewBillViewModel> newBillFactory =
                () => new NewBillViewModel(newBillRepository, dialogService, navigationService);
            Func<SearchBillViewModel> searchBillFactory =
                () => new SearchBillViewModel(searchBillRepository, dialogService);
            Func<NewKnittingInvoiceViewModel> newKnittingInvoiceFactory =
                () => new NewKnittingInvoiceViewModel(knittingInvoiceRepository, dialogService);
            Func<KnittingSearchInvoiceViewModel> knittingSearchInvoiceFactory =
                () => new KnittingSearchInvoiceViewModel(knittingSearchRepository, dialogService);
            Func<NewArticulViewModel> newArticulFactory =
                () => new NewArticulViewModel(articulRepository, dialogService, navigationService);
            Func<OperationViewModel> operationFactory =
                () => new OperationViewModel(operationRepository, dialogService);
            Func<EmployeesViewModel> employeesFactory =
                () => new EmployeesViewModel(employeesRepository, dialogService);
            Func<CoefficientsViewModel> coefficientsFactory =
                () => new CoefficientsViewModel(coefficientsRepository, dialogService);
            Func<DetailsViewModel> detailsFactory =
                () => new DetailsViewModel(detailsRepository, dialogService);
            Func<MachinesViewModel> machinesFactory =
                () => new MachinesViewModel(machinesRepository, dialogService);
            Func<RoutingViewModel> routingFactory =
                () => new RoutingViewModel(routingRepository, dialogService, navigationService);
            Func<KnittingRoutingViewModel> knittingRoutingFactory =
                () => new KnittingRoutingViewModel(knittingRoutingRepository, dialogService);
            Func<TariffsCheckViewModel> tariffsCheckFactory =
                () => new TariffsCheckViewModel(tariffsCheckRepository, dialogService);
            Func<CalculationViewModel> calculationFactory =
                () => new CalculationViewModel(calculationRepository, dialogService, navigationService);
            Func<ReportViewerViewModel> reportViewerFactory =
                () => new ReportViewerViewModel(reportRepository, dialogService);
            Func<WorkOrderMode, WorkOrdersViewModel> workOrdersFactory =
                mode => new WorkOrdersViewModel(workOrderRepository, dialogService, mode);

            var viewModel = new MainWindowViewModel(
                navigationService,
                dialogService,
                maintenanceService,
                applicationService,
                newBillFactory,
                searchBillFactory,
                newKnittingInvoiceFactory,
                knittingSearchInvoiceFactory,
                newArticulFactory,
                operationFactory,
                employeesFactory,
                coefficientsFactory,
                detailsFactory,
                machinesFactory,
                routingFactory,
                knittingRoutingFactory,
                tariffsCheckFactory,
                calculationFactory,
                reportViewerFactory,
                workOrdersFactory);

            var window = new MainWindow { DataContext = viewModel };
            navigationService.SetHost(window.ContentHost);
            viewModel.ShowWelcome();

            MainWindow = window;
            window.Show();

            maintenanceService.BackupIfFirstDayOfMonth();
            currentDayStore.Save(DateTime.Today.Day);
        }

        private static void ConfigureCulture()
        {
            var culture = new CultureInfo("ru-RU");

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(culture.IetfLanguageTag)));
        }
    }
}
