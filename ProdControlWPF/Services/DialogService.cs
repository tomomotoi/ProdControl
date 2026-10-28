using System.Windows;
using Microsoft.Win32;
using ProdControlWPF.Views;

namespace ProdControlWPF.Services
{
    public sealed class DialogService : IDialogService
    {
        public void ShowInfo(string message, string title)
        {
            Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowWarning(string message, string title)
        {
            Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ShowError(string message, string title)
        {
            Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool Confirm(string message, string title, DialogIcon icon)
        {
            var result = Show(message, title, MessageBoxButton.OKCancel, ToImage(icon));
            return result == MessageBoxResult.OK;
        }

        public string Prompt(string message, string title)
        {
            var owner = GetOwner();
            var window = new PromptWindow(message, title);

            if (owner != null)
            {
                window.Owner = owner;
            }

            return window.ShowDialog() == true ? window.Answer : string.Empty;
        }

        public string OpenFile(string filter, string initialDirectory)
        {
            var dialog = new OpenFileDialog
            {
                Filter = filter,
                InitialDirectory = initialDirectory
            };

            var owner = GetOwner();
            var result = owner != null ? dialog.ShowDialog(owner) : dialog.ShowDialog();

            return result == true ? dialog.FileName : null;
        }

        public string SaveFile(string filter, string fileName)
        {
            var dialog = new SaveFileDialog
            {
                Filter = filter,
                FilterIndex = 2,
                RestoreDirectory = true,
                FileName = fileName ?? string.Empty
            };

            var owner = GetOwner();
            var result = owner != null ? dialog.ShowDialog(owner) : dialog.ShowDialog();

            return result == true ? dialog.FileName : null;
        }

        private static MessageBoxResult Show(string message, string title, MessageBoxButton button, MessageBoxImage image)
        {
            var owner = GetOwner();
            return owner != null
                ? MessageBox.Show(owner, message, title, button, image)
                : MessageBox.Show(message, title, button, image);
        }

        private static Window GetOwner()
        {
            var owner = Application.Current != null ? Application.Current.MainWindow : null;
            return owner != null && owner.IsVisible ? owner : null;
        }

        private static MessageBoxImage ToImage(DialogIcon icon)
        {
            switch (icon)
            {
                case DialogIcon.Warning:
                    return MessageBoxImage.Warning;
                case DialogIcon.Error:
                    return MessageBoxImage.Error;
                case DialogIcon.Question:
                    return MessageBoxImage.Question;
                default:
                    return MessageBoxImage.Information;
            }
        }
    }
}
