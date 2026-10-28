namespace ProdControlWPF.Services
{
    public enum DialogIcon
    {
        Information,
        Warning,
        Error,
        Question
    }

    public interface IDialogService
    {
        void ShowInfo(string message, string title);
        void ShowWarning(string message, string title);
        void ShowError(string message, string title);
        bool Confirm(string message, string title, DialogIcon icon);
        string Prompt(string message, string title);
        string OpenFile(string filter, string initialDirectory);
        string SaveFile(string filter, string fileName);
    }
}
