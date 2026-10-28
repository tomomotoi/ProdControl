using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ProdControlWPF.ViewModels;

namespace ProdControlWPF.Views
{
    public partial class NewBillView : UserControl
    {
        private NewBillViewModel _viewModel;

        public NewBillView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
            Unloaded += OnUnloaded;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DetachViewModel();

            _viewModel = e.NewValue as NewBillViewModel;

            if (_viewModel != null)
            {
                _viewModel.WorkdayFocusRequested += OnWorkdayFocusRequested;
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            DetachViewModel();
        }

        private void DetachViewModel()
        {
            if (_viewModel == null)
            {
                return;
            }

            _viewModel.WorkdayFocusRequested -= OnWorkdayFocusRequested;
            _viewModel = null;
        }

        private void OnWorkdayFocusRequested(object sender, System.EventArgs e)
        {
            WorkdayTextBox.Focus();
            WorkdayTextBox.SelectAll();
        }

        private void OnDigitsOnlyPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsDigitsOnly(e.Text);
        }

        private void OnDigitsOnlyPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (!e.SourceDataObject.GetDataPresent(DataFormats.UnicodeText, true))
            {
                e.CancelCommand();
                return;
            }

            var text = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;

            if (!IsDigitsOnly(text))
            {
                e.CancelCommand();
            }
        }

        private static bool IsDigitsOnly(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return true;
            }

            foreach (var character in text)
            {
                if (!char.IsNumber(character))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
