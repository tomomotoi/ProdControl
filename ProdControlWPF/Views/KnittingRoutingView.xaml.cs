using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProdControlWPF.Views
{
    public partial class KnittingRoutingView : UserControl
    {
        private static readonly Regex DecimalPattern = new Regex(@"^\d*([.,]\d*)?$", RegexOptions.Compiled);

        public KnittingRoutingView()
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
