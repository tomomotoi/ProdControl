using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ProdControlWPF.ViewModels;

namespace ProdControlWPF.Views
{
    public partial class WorkOrdersView : UserControl
    {
        private static readonly Regex IntegerPattern = new Regex(@"^\d*$", RegexOptions.Compiled);

        private static readonly Regex DecimalPattern = new Regex(@"^\d*([.,]\d*)?$", RegexOptions.Compiled);

        public WorkOrdersView()
        {
            InitializeComponent();
        }

        private void OnOrdersGridMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left
                || !(e.OriginalSource is DependencyObject source)
                || FindVisualParent<DataGridRow>(source) == null)
            {
                return;
            }

            if (DataContext is WorkOrdersViewModel viewModel
                && viewModel.OpenCommand.CanExecute(null))
            {
                viewModel.OpenCommand.Execute(null);
                e.Handled = true;
            }
        }

        private void OnNumericPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var pattern = GetPattern(e.OriginalSource as TextBox);

            if (pattern == null)
            {
                return;
            }

            var textBox = (TextBox)e.OriginalSource;
            e.Handled = !pattern.IsMatch(BuildProspectiveText(textBox, e.Text));
        }

        private void OnNumericPasting(object sender, DataObjectPastingEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            var pattern = GetPattern(textBox);

            if (pattern == null)
            {
                return;
            }

            if (!TryGetPastedText(e, out var pasted)
                || !pattern.IsMatch(BuildProspectiveText(textBox, pasted)))
            {
                e.CancelCommand();
            }
        }

        private static Regex GetPattern(TextBox textBox)
        {
            if (textBox == null)
            {
                return null;
            }

            var tag = textBox.Tag as string;

            if (tag == "int")
            {
                return IntegerPattern;
            }

            if (tag == "decimal")
            {
                return DecimalPattern;
            }

            return null;
        }

        private static bool TryGetPastedText(DataObjectPastingEventArgs e, out string text)
        {
            text = null;

            if (!e.SourceDataObject.GetDataPresent(DataFormats.UnicodeText, true))
            {
                return false;
            }

            text = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;
            return text != null;
        }

        private static string BuildProspectiveText(TextBox textBox, string inserted)
        {
            var current = textBox.Text ?? string.Empty;
            var start = Math.Min(textBox.SelectionStart, current.Length);
            var length = Math.Min(textBox.SelectionLength, current.Length - start);

            return current.Substring(0, start)
                + (inserted ?? string.Empty)
                + current.Substring(start + length);
        }

        private static T FindVisualParent<T>(DependencyObject child)
            where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);

            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as T;
        }
    }
}
