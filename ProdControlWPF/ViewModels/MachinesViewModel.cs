using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using ProdControlWPF.Data;
using ProdControlWPF.Infrastructure;
using ProdControlWPF.Models;
using ProdControlWPF.Services;

namespace ProdControlWPF.ViewModels
{
    public sealed class MachinesViewModel : ObservableObject
    {
        private const string ErrorTitle = "Ошибка";
        private const string WarningTitle = "Предупреждение";
        private const string SuccessTitle = "Успех";
        private const string ConfirmationTitle = "Подтверждение";
        private const string LoadErrorPrefix = "Ошибка загрузки данных: ";
        private const string AddErrorPrefix = "Ошибка при добавлении: ";
        private const string SaveErrorPrefix = "Ошибка при сохранении: ";
        private const string DeleteErrorPrefix = "Ошибка при удалении: ";
        private const string EmptyFieldsMessage = "Заполните марку и модель оборудования";
        private const string NoSelectionForEditMessage =
            "Выберите запись для редактирования из таблицы";
        private const string EmptyEditFieldsMessage = "Поля марки и модели не могут быть пустыми";
        private const string NoSelectionForDeleteMessage = "Выберите запись для удаления";
        private const string AddedMessage = "Запись успешно добавлена";
        private const string SavedMessage = "Изменения успешно сохранены";
        private const string DeletedMessage = "Запись удалена";

        private readonly IMachinesRepository _repository;
        private readonly IDialogService _dialogService;

        private string _machineId = string.Empty;
        private string _make = string.Empty;
        private string _model = string.Empty;
        private MachineItem _selectedMachine;

        public MachinesViewModel(IMachinesRepository repository, IDialogService dialogService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Machines = new ObservableCollection<MachineItem>();

            AddCommand = new RelayCommand(AddMachine);
            SaveCommand = new RelayCommand(SaveMachine);
            DeleteCommand = new RelayCommand(DeleteMachine);
            ClearCommand = new RelayCommand(ClearFields);

            Load();
        }

        public ObservableCollection<MachineItem> Machines { get; }

        public ICommand AddCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand ClearCommand { get; }

        public string MachineId
        {
            get { return _machineId; }
            private set { SetProperty(ref _machineId, value ?? string.Empty); }
        }

        public string Make
        {
            get { return _make; }
            set { SetProperty(ref _make, value ?? string.Empty); }
        }

        public string Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value ?? string.Empty); }
        }

        public MachineItem SelectedMachine
        {
            get { return _selectedMachine; }
            set
            {
                if (SetProperty(ref _selectedMachine, value) && value != null)
                {
                    MachineId = value.Id.ToString(CultureInfo.CurrentCulture);
                    Make = value.Make ?? string.Empty;
                    Model = value.Model ?? string.Empty;
                }
            }
        }

        private void Load()
        {
            try
            {
                ReloadMachines();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(LoadErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void AddMachine()
        {
            if (string.IsNullOrWhiteSpace(Make) || string.IsNullOrWhiteSpace(Model))
            {
                _dialogService.ShowWarning(EmptyFieldsMessage, WarningTitle);
                return;
            }

            try
            {
                _repository.InsertMachine(Make.Trim(), Model.Trim());
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(AddErrorPrefix + exception.Message, ErrorTitle);
                return;
            }

            _dialogService.ShowInfo(AddedMessage, SuccessTitle);
            ClearFields();
            ReloadAfterWrite();
        }

        private void SaveMachine()
        {
            if (string.IsNullOrEmpty(MachineId))
            {
                _dialogService.ShowWarning(NoSelectionForEditMessage, WarningTitle);
                return;
            }

            if (string.IsNullOrWhiteSpace(Make) || string.IsNullOrWhiteSpace(Model))
            {
                _dialogService.ShowWarning(EmptyEditFieldsMessage, WarningTitle);
                return;
            }

            try
            {
                var id = Convert.ToInt32(MachineId, CultureInfo.CurrentCulture);
                _repository.UpdateMachine(id, Make.Trim(), Model.Trim());
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(SaveErrorPrefix + exception.Message, ErrorTitle);
                return;
            }

            _dialogService.ShowInfo(SavedMessage, SuccessTitle);
            ReloadAfterWrite();
        }

        private void DeleteMachine()
        {
            if (string.IsNullOrEmpty(MachineId))
            {
                _dialogService.ShowWarning(NoSelectionForDeleteMessage, WarningTitle);
                return;
            }

            var question = string.Format(
                "Удалить оборудование с ID {0}?",
                MachineId);

            if (!_dialogService.Confirm(question, ConfirmationTitle, DialogIcon.Question))
            {
                return;
            }

            try
            {
                var id = Convert.ToInt32(MachineId, CultureInfo.CurrentCulture);
                _repository.DeleteMachine(id);
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(DeleteErrorPrefix + exception.Message, ErrorTitle);
                return;
            }

            _dialogService.ShowInfo(DeletedMessage, SuccessTitle);
            ClearFields();
            ReloadAfterWrite();
        }

        private void ClearFields()
        {
            MachineId = string.Empty;
            Make = string.Empty;
            Model = string.Empty;
            SelectedMachine = null;
        }

        private void ReloadAfterWrite()
        {
            try
            {
                ReloadMachines();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(LoadErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void ReloadMachines()
        {
            var machines = _repository.GetMachines();

            SelectedMachine = null;
            Machines.Clear();

            foreach (var machine in machines)
            {
                Machines.Add(machine);
            }
        }
    }
}
