using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProdControlWPF.Views
{
    public partial class RoutingView : UserControl
    {
        private const string DopCostHeader = "Коэф. уч. доп затра";
        private const string ExecutionTimeHeader = "Время с уч.коэф";
        private const string RateExecuteHeader = "Норма выработки";
        private const string LevelHeader = "Разряд";
        private const string CostHeader = "Расценка";

        private static readonly Regex IntegerPattern = new Regex(@"^\d*$", RegexOptions.Compiled);

        private static readonly Regex DecimalPattern = new Regex(@"^\d*([.,]\d*)?$", RegexOptions.Compiled);

        public RoutingView()
        {
            InitializeComponent();
        }

        private void OnDecimalPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;

            e.Handled = textBox == null
                ? !DecimalPattern.IsMatch(e.Text)
                : !DecimalPattern.IsMatch(BuildProspectiveText(textBox, e.Text));
        }

        private void OnDecimalPasting(object sender, DataObjectPastingEventArgs e)
        {
            var textBox = sender as TextBox;

            if (!TryGetPastedText(e, out var text)
                || (textBox != null && !DecimalPattern.IsMatch(BuildProspectiveText(textBox, text)))
                || (textBox == null && !DecimalPattern.IsMatch(text)))
            {
                e.CancelCommand();
            }
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

            if (header == LevelHeader)
            {
                return IntegerPattern;
            }

            if (header == DopCostHeader
                || header == ExecutionTimeHeader
                || header == RateExecuteHeader
                || header == CostHeader)
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
    }
}
