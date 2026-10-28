namespace ProdControlWPF.ViewModels
{
    public sealed class MessageViewModel
    {
        public MessageViewModel(string title, string message)
        {
            Title = title ?? string.Empty;
            Message = message ?? string.Empty;
        }

        public string Title { get; }

        public string Message { get; }
    }
}
