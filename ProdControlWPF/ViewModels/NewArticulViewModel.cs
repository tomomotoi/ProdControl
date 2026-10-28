using System;
using System.Windows.Input;
using ProdControlWPF.Data;
using ProdControlWPF.Infrastructure;
using ProdControlWPF.Services;

namespace ProdControlWPF.ViewModels
{
    public sealed class NewArticulViewModel : ObservableObject
    {
        private const string SuccessTitle = "Артикул";
        private const string SaveErrorPrefix = "Ошибка при сохранении артикула: ";
        private const string ErrorTitle = "Ошибка";

        private readonly INewArticulRepository _repository;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        private string _articulName = string.Empty;
        private string _description = string.Empty;

        public NewArticulViewModel(
            INewArticulRepository repository,
            IDialogService dialogService,
            INavigationService navigationService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            SaveCommand = new RelayCommand(Save);
            CloseCommand = new RelayCommand(Close);
        }

        public string ArticulName
        {
            get { return _articulName; }
            set { SetProperty(ref _articulName, value ?? string.Empty); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value ?? string.Empty); }
        }

        public ICommand SaveCommand { get; }

        public ICommand CloseCommand { get; }

        private void Save()
        {
            try
            {
                var inserted = _repository.InsertArticul(ArticulName, Description);

                if (inserted > 0)
                {
                    _dialogService.ShowInfo(
                        "Новый Артикул " + ArticulName + " сохранен",
                        SuccessTitle);
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(SaveErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void Close()
        {
            _navigationService.Navigate(new MessageViewModel("ProdControl", "Выберите раздел в меню."));
        }
    }
}
