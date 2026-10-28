using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ProdControlWPF.Data;
using ProdControlWPF.Infrastructure;
using ProdControlWPF.Models;
using ProdControlWPF.Services;

namespace ProdControlWPF.ViewModels
{
    public sealed class DetailsViewModel : ObservableObject
    {
        private const string ErrorTitle = "Ошибка";
        private const string WarningTitle = "Предупреждение";
        private const string SuccessTitle = "Успех";
        private const string ConfirmationTitle = "Подтверждение";
        private const string LoadErrorPrefix = "Ошибка загрузки данных: ";
        private const string AddErrorPrefix = "Ошибка при добавлении: ";
        private const string SaveErrorPrefix = "Ошибка при сохранении: ";
        private const string DeleteErrorPrefix = "Ошибка при удалении: ";
        private const string EmptyNameMessage = "Введите наименование детали";
        private const string NoSelectionForEditMessage =
            "Выберите запись в таблице для редактирования";
        private const string EmptyEditNameMessage =
            "Наименование детали не может быть пустым";
        private const string NoSelectionForDeleteMessage =
            "Выберите запись в таблице для удаления";
        private const string AddedMessage = "Деталь успешно добавлена";
        private const string SavedMessage = "Изменения успешно сохранены";
        private const string DeletedMessage = "Запись удалена";

        private readonly IDetailsRepository _repository;
        private readonly IDialogService _dialogService;

        private string _partName = string.Empty;
        private DetailItem _selectedDetail;

        public DetailsViewModel(IDetailsRepository repository, IDialogService dialogService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Details = new ObservableCollection<DetailItem>();

            AddCommand = new RelayCommand(AddDetail);
            SaveCommand = new RelayCommand(SaveDetail);
            DeleteCommand = new RelayCommand(DeleteDetail);
            ClearCommand = new RelayCommand(ClearFields);

            Load();
        }

        public ObservableCollection<DetailItem> Details { get; }

        public ICommand AddCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand ClearCommand { get; }

        public string PartName
        {
            get { return _partName; }
            set { SetProperty(ref _partName, value ?? string.Empty); }
        }

        public DetailItem SelectedDetail
        {
            get { return _selectedDetail; }
            set
            {
                if (SetProperty(ref _selectedDetail, value) && value != null)
                {
                    PartName = value.Name ?? string.Empty;
                }
            }
        }

        private void Load()
        {
            try
            {
                ReloadDetails();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(LoadErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void AddDetail()
        {
            if (string.IsNullOrWhiteSpace(PartName))
            {
                _dialogService.ShowWarning(EmptyNameMessage, WarningTitle);
                return;
            }

            try
            {
                _repository.InsertDetail(PartName.Trim());
                _dialogService.ShowInfo(AddedMessage, SuccessTitle);
                ClearFields();
                ReloadDetails();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(AddErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void SaveDetail()
        {
            var selectedDetail = SelectedDetail;

            if (selectedDetail == null)
            {
                _dialogService.ShowWarning(NoSelectionForEditMessage, WarningTitle);
                return;
            }

            if (string.IsNullOrWhiteSpace(PartName))
            {
                _dialogService.ShowWarning(EmptyEditNameMessage, WarningTitle);
                return;
            }

            try
            {
                _repository.UpdateDetail(selectedDetail.Id, PartName.Trim());
                _dialogService.ShowInfo(SavedMessage, SuccessTitle);
                ReloadDetails();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(SaveErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void DeleteDetail()
        {
            var selectedDetail = SelectedDetail;

            if (selectedDetail == null)
            {
                _dialogService.ShowWarning(NoSelectionForDeleteMessage, WarningTitle);
                return;
            }

            var partName = selectedDetail.Name ?? string.Empty;
            var question = string.Format(
                "Удалить деталь \"{0}\" (ID: {1})?",
                partName,
                selectedDetail.Id);

            if (!_dialogService.Confirm(question, ConfirmationTitle, DialogIcon.Question))
            {
                return;
            }

            try
            {
                _repository.DeleteDetail(selectedDetail.Id);
                _dialogService.ShowInfo(DeletedMessage, SuccessTitle);
                ClearFields();
                ReloadDetails();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(DeleteErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void ClearFields()
        {
            PartName = string.Empty;
            SelectedDetail = null;
        }

        private void ReloadDetails()
        {
            var details = _repository.GetDetails();

            Details.Clear();
            SelectedDetail = null;

            foreach (var detail in details)
            {
                Details.Add(detail);
            }
        }
    }
}
