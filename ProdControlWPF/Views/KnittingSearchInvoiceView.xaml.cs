using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProdControlWPF.Views
{
    public partial class KnittingSearchInvoiceView : UserControl
    {
        private const string TimeExecutionHeader = "Время выполнения";
        private const string CountHeader = "Количество";
        private const string WorkDayHeader = "Рабочий день";

        private static readonly Regex IntegerPattern = new Regex(@"^\d*$", RegexOptions.Compiled);

        private static readonly Regex DecimalPattern = new Regex(@"^\d*([.,]\d*)?$", RegexOptions.Compiled);

        public KnittingSearchInvoiceView()
        {
            InitializeComponent();
        }

        private void OnGridPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            var pattern = GetPattern(sender as DataGrid, textBox);

            if (pattern == null)
            {
                return;
            }

            e.Handled = !pattern.IsMatch(BuildProspectiveText(textBox, e.Text));
        }

        private void OnGridPasting(object sender, DataObjectPastingEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            var pattern = GetPattern(sender as DataGrid, textBox);

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

        private static Regex GetPattern(DataGrid grid, TextBox textBox)
        {
            if (grid == null || textBox == null || grid.CurrentColumn == null)
            {
                return null;
            }

            var header = grid.CurrentColumn.Header as string;

            if (header == TimeExecutionHeader)
            {
                return DecimalPattern;
            }

            if (header == CountHeader || header == WorkDayHeader)
            {
                return IntegerPattern;
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
    }
}
