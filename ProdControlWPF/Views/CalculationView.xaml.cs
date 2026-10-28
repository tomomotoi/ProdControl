using System.Linq;
using System.Windows.Controls;
using ProdControlWPF.Models;
using ProdControlWPF.ViewModels;

namespace ProdControlWPF.Views
{
    public partial class CalculationView : UserControl
    {
        public CalculationView()
        {
            InitializeComponent();
        }

        private void OnGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as CalculationViewModel;

            if (viewModel == null)
            {
                return;
            }

            viewModel.SelectedRows = EmployeesGrid.SelectedItems
                .Cast<CalculationRow>()
                .ToList();
        }
    }
}
