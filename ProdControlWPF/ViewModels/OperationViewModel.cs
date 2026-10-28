using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ProdControlWPF.Data;
using ProdControlWPF.Infrastructure;
using ProdControlWPF.Models;
using ProdControlWPF.Services;

namespace ProdControlWPF.ViewModels
{
    public sealed class OperationViewModel : ObservableObject
    {
        private const string ErrorTitle = "Ошибка";
        private const string NotificationTitle = "Уведомление";
        private const string LoadErrorPrefix = "Ошибка при загрузке данных: ";
        private const string EmptyNameMessage =
            "Поле 'Название новой операции' не должно быть пустым!";
        private const string DuplicateMessage = "Такая операция уже существует!";
        private const string SuccessMessage =
            "Операция была успешно добавлена в базу данных!";
        private const string UsedMessage =
            "Данная операция используется в текущих производственных процессах!";
        private const string NoSelectionMessage = "Выберите операцию для удаления.";
        private const string NoPositionMessage = "Выберите профессию.";

        private readonly IOperationRepository _repository;
        private readonly IDialogService _dialogService;

        private string _newOperationName = string.Empty;
        private string _searchText = string.Empty;
        private string _selectedProfessionName = string.Empty;
        private OperationItem _selectedOperation;
        private PositionItem _selectedPosition;

        public OperationViewModel(IOperationRepository repository, IDialogService dialogService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Operations = new ObservableCollection<OperationItem>();
            Positions = new ObservableCollection<PositionItem>();

            AddOperationCommand = new RelayCommand(AddOperation);
            DeleteOperationCommand = new RelayCommand(DeleteOperation);

            Load();
        }

        public ObservableCollection<OperationItem> Operations { get; }

        public ObservableCollection<PositionItem> Positions { get; }

        public ICommand AddOperationCommand { get; }

        public ICommand DeleteOperationCommand { get; }

        public string NewOperationName
        {
            get { return _newOperationName; }
            set { SetProperty(ref _newOperationName, value ?? string.Empty); }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (SetProperty(ref _searchText, value ?? string.Empty))
                {
                    SelectFirstMatch();
                }
            }
        }

        public OperationItem SelectedOperation
        {
            get { return _selectedOperation; }
            set
            {
                if (!SetProperty(ref _selectedOperation, value))
                {
                    return;
                }

                SelectedProfessionName = GetProfessionName(value);
            }
        }

        public PositionItem SelectedPosition
        {
            get { return _selectedPosition; }
            set { SetProperty(ref _selectedPosition, value); }
        }

        public string SelectedProfessionName
        {
            get { return _selectedProfessionName; }
            private set { SetProperty(ref _selectedProfessionName, value); }
        }

        private void Load()
        {
            try
            {
                var positions = _repository.GetPositions();

                Positions.Clear();
                foreach (var position in positions)
                {
                    Positions.Add(position);
                }

                SelectedPosition = Positions.FirstOrDefault();

                ReloadOperations();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(LoadErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void AddOperation()
        {
            if (NewOperationName.Length == 0)
            {
                _dialogService.ShowError(EmptyNameMessage, ErrorTitle);
                return;
            }

            try
            {
                if (_repository.OperationNameExists(NewOperationName))
                {
                    _dialogService.ShowError(DuplicateMessage, ErrorTitle);
                    return;
                }

                var position = SelectedPosition;

                if (position == null)
                {
                    _dialogService.ShowError(NoPositionMessage, ErrorTitle);
                    return;
                }

                _repository.InsertOperation(NewOperationName, position.Id);
                ReloadOperations();

                _dialogService.ShowInfo(SuccessMessage, NotificationTitle);
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }

        private void DeleteOperation()
        {
            var operation = SelectedOperation;

            if (operation == null)
            {
                _dialogService.ShowError(NoSelectionMessage, ErrorTitle);
                return;
            }

            try
            {
                if (_repository.IsOperationUsed(operation.Id))
                {
                    _dialogService.ShowError(UsedMessage, ErrorTitle);
                    return;
                }

                _repository.DeleteOperation(operation.Id);
                ReloadOperations();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }

        private void ReloadOperations()
        {
            var operations = _repository.GetOperations();

            Operations.Clear();
            foreach (var operation in operations)
            {
                Operations.Add(operation);
            }

            SelectedOperation = Operations.FirstOrDefault();
        }

        private void SelectFirstMatch()
        {
            if (Operations.Count == 0)
            {
                SelectedOperation = null;
                return;
            }

            SelectedOperation = Operations.FirstOrDefault(operation =>
                operation.Name.StartsWith(SearchText, StringComparison.CurrentCultureIgnoreCase));
        }

        private string GetProfessionName(OperationItem operation)
        {
            if (operation == null || !operation.PositionId.HasValue)
            {
                return string.Empty;
            }

            var position = Positions.FirstOrDefault(item => item.Id == operation.PositionId.Value);

            return position == null ? string.Empty : position.Name;
        }
    }
}
