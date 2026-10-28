using System.Windows;

namespace ProdControlWPF.Views
{
    public partial class PromptWindow : Window
    {
        public PromptWindow(string prompt, string title)
        {
            InitializeComponent();

            Title = title;
            PromptText.Text = prompt;
            Loaded += OnLoaded;
        }

        public string Answer
        {
            get { return AnswerTextBox.Text; }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AnswerTextBox.Focus();
            AnswerTextBox.SelectAll();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
