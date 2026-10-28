using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProdControlWPF.Views
{
    public partial class NewKnittingInvoiceView : UserControl
    {
        private static readonly Regex IntegerPattern = new Regex(@"^\d*$", RegexOptions.Compiled);

        private static readonly Regex DecimalPattern = new Regex(@"^\d*([.,]\d{0,2})?$", RegexOptions.Compiled);

        public NewKnittingInvoiceView()
        {
            InitializeComponent();
        }

        private void OnIntegerPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;

            e.Handled = textBox == null || !IntegerPattern.IsMatch(BuildProspectiveText(textBox, e.Text));
        }

        private void OnIntegerPasting(object sender, DataObjectPastingEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox == null || !TryGetPastedText(e, out var pasted)
                || !IntegerPattern.IsMatch(BuildProspectiveText(textBox, pasted)))
            {
                e.CancelCommand();
            }
        }

        private void OnDecimalPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;

            e.Handled = textBox == null || !DecimalPattern.IsMatch(BuildProspectiveText(textBox, e.Text));
        }

        private void OnDecimalPasting(object sender, DataObjectPastingEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox == null || !TryGetPastedText(e, out var pasted)
                || !DecimalPattern.IsMatch(BuildProspectiveText(textBox, pasted)))
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
