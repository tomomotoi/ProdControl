using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ProdControlWPF.Models;
using ProdControlWPF.ViewModels;

namespace ProdControlWPF.Views
{
    public partial class ReportViewerView : UserControl
    {
        private ReportViewerViewModel _viewModel;

        public ReportViewerView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
            Unloaded += OnUnloaded;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DetachViewModel();

            _viewModel = e.NewValue as ReportViewerViewModel;

            if (_viewModel != null)
            {
                _viewModel.ReportReady += OnReportReady;
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            DetachViewModel();
            ReportHost.DisposeReport();
        }

        private void DetachViewModel()
        {
            if (_viewModel == null)
            {
                return;
            }

            _viewModel.ReportReady -= OnReportReady;
            _viewModel = null;
        }

        private void OnReportReady(object sender, EventArgs e)
        {
            if (_viewModel == null)
            {
                return;
            }

            try
            {
                ReportHost.LoadReport(_viewModel.ReportRows);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    "Ошибка при формировании отчета: " + exception.Message,
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                if (_viewModel.BackCommand.CanExecute(null))
                {
                    _viewModel.BackCommand.Execute(null);
                }
            }
        }

        private void OnGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as ReportViewerViewModel;

            if (viewModel == null)
            {
                return;
            }

            viewModel.SelectedEmployees = EmployeesGrid.SelectedItems
                .Cast<ReportEmployeeListItem>()
                .ToList();
        }
    }
}
