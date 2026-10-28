using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProdControlWPF.Views
{
    public partial class EmployeesView : UserControl
    {
        private const string PositionLevelHeader = "Разряд должности";

        private static readonly Regex IntegerPattern = new Regex(@"^\d*$", RegexOptions.Compiled);

        public EmployeesView()
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

            return (grid.CurrentColumn.Header as string) == PositionLevelHeader
                ? IntegerPattern
                : null;
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
            var start = System.Math.Min(textBox.SelectionStart, current.Length);
            var length = System.Math.Min(textBox.SelectionLength, current.Length - start);

            return current.Substring(0, start)
                + (inserted ?? string.Empty)
                + current.Substring(start + length);
        }
    }
}
