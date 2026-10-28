using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using ProdControlWPF.Data;
using ProdControlWPF.Domain;
using ProdControlWPF.Infrastructure;
using ProdControlWPF.Models;
using ProdControlWPF.Services;

namespace ProdControlWPF.ViewModels
{
    public sealed class KnittingRoutingViewModel : ObservableObject
    {
        private const string ErrorTitle = "Ошибка";
        private const string WarningTitle = "Внимание";
        private const string InformationTitle = "Информация";
        private const string SuccessTitle = "Успех";
        private const string DeleteConfirmationTitle = "Подтверждение удаления";
        private const string LoadErrorPrefix = "Ошибка при загрузке данных: ";
        private const string SaveErrorPrefix = "Ошибка при сохранении в БД: ";
        private const string AddErrorPrefix = "Ошибка при добавлении строки в таблицу: ";
        private const string DeleteErrorPrefix = "Ошибка при удалении строки: ";
        private const string NoArticulForSearchMessage = "Выберите артикул для поиска!";
        private const string NoArticulForSaveMessage = "Выберите артикул!";
        private const string NoRecordsMessage = "Записи для указанного артикула не найдены.";
        private const string EmptyTableMessage = "Таблица пуста. Нет данных для сохранения!";
        private const string DeleteAllMessage =
            "Вы удалили все строки из загруженной карты. Удалить эту технологическую карту из базы данных?";
        private const string DeleteRowMessage = "Вы действительно хотите удалить выбранную строку?";
        private const string NoRowSelectedMessage = "Выберите строку для удаления!";
        private const string SaveSuccessMessage = "Данные успешно сохранены!";
        private const string EmptyFieldMessage = "Поле '{0}' не заполнено!";
        private const string MissingIdMessage =
            "Не удалось найти ID в базе для детали '{0}' или оборудования '{1}'.";

        private readonly IKnittingRoutingRepository _repository;
        private readonly IDialogService _dialogService;

        private ArticulLookup _selectedArticul;
        private ClothingPartLookup _selectedPart;
        private KnittingMachineLookup _selectedMachine;
        private CoefficientLookup _selectedCoefficient1;
        private CoefficientLookup _selectedCoefficient2;
        private string _selectedModelPartAmount;
        private string _selectedMachineCarriageAmount;
        private string _avgValue = string.Empty;
        private string _withCoefficient = string.Empty;
        private string _withKit = string.Empty;
        private string _total = string.Empty;
        private string _norm = string.Empty;
        private KnittingRoutingRow _selectedRow;
        private bool _isLoadedFromDb;

        public KnittingRoutingViewModel(IKnittingRoutingRepository repository, IDialogService dialogService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Articuls = new ObservableCollection<ArticulLookup>();
            Parts = new ObservableCollection<ClothingPartLookup>();
            Machines = new ObservableCollection<KnittingMachineLookup>();
            Coefficients = new ObservableCollection<CoefficientLookup>();
            ModelPartAmounts = new ObservableCollection<string>(
                Enumerable.Range(1, 50).Select(value => value.ToString(CultureInfo.CurrentCulture)));
            MachineCarriageAmounts = new ObservableCollection<string>(
                Enumerable.Range(1, 4).Select(value => value.ToString(CultureInfo.CurrentCulture)));
            Rows = new ObservableCollection<KnittingRoutingRow>();

            SearchCommand = new RelayCommand(Search);
            AddCommand = new RelayCommand(Add);
            DeleteCommand = new RelayCommand(Delete);
            SaveCommand = new RelayCommand(Save);

            Load();
        }

        public ObservableCollection<ArticulLookup> Articuls { get; }

        public ObservableCollection<ClothingPartLookup> Parts { get; }

        public ObservableCollection<KnittingMachineLookup> Machines { get; }

        public ObservableCollection<CoefficientLookup> Coefficients { get; }

        public ObservableCollection<string> ModelPartAmounts { get; }

        public ObservableCollection<string> MachineCarriageAmounts { get; }

        public ObservableCollection<KnittingRoutingRow> Rows { get; }

        public ICommand SearchCommand { get; }

        public ICommand AddCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand SaveCommand { get; }

        public ArticulLookup SelectedArticul
        {
            get { return _selectedArticul; }
            set
            {
                if (SetProperty(ref _selectedArticul, value))
                {
                    ClearLoadedState();
                }
            }
        }

        public ClothingPartLookup SelectedPart
        {
            get { return _selectedPart; }
            set { SetProperty(ref _selectedPart, value); }
        }

        public KnittingMachineLookup SelectedMachine
        {
            get { return _selectedMachine; }
            set { SetProperty(ref _selectedMachine, value); }
        }

        public CoefficientLookup SelectedCoefficient1
        {
            get { return _selectedCoefficient1; }
            set
            {
                if (SetProperty(ref _selectedCoefficient1, value))
                {
                    CalculateWithCoefficient();
                }
            }
        }

        public CoefficientLookup SelectedCoefficient2
        {
            get { return _selectedCoefficient2; }
            set
            {
                if (SetProperty(ref _selectedCoefficient2, value))
                {
                    CalculateWithCoefficient();
                }
            }
        }

        public string SelectedModelPartAmount
        {
            get { return _selectedModelPartAmount; }
            set
            {
                if (SetProperty(ref _selectedModelPartAmount, value))
                {
                    CalculateWithKit();
                }
            }
        }

        public string SelectedMachineCarriageAmount
        {
            get { return _selectedMachineCarriageAmount; }
            set
            {
                if (SetProperty(ref _selectedMachineCarriageAmount, value))
                {
                    CalculateWithKit();
                }
            }
        }

        public string AvgValue
        {
            get { return _avgValue; }
            set
            {
                if (SetProperty(ref _avgValue, value ?? string.Empty))
                {
                    CalculateWithCoefficient();
                }
            }
        }

        public string WithCoefficient
        {
            get { return _withCoefficient; }
            private set { SetProperty(ref _withCoefficient, value ?? string.Empty); }
        }

        public string WithKit
        {
            get { return _withKit; }
            private set { SetProperty(ref _withKit, value ?? string.Empty); }
        }

        public string Total
        {
            get { return _total; }
            private set { SetProperty(ref _total, value ?? string.Empty); }
        }

        public string Norm
        {
            get { return _norm; }
            private set { SetProperty(ref _norm, value ?? string.Empty); }
        }

        public KnittingRoutingRow SelectedRow
        {
            get { return _selectedRow; }
            set { SetProperty(ref _selectedRow, value); }
        }

        private void Load()
        {
            try
            {
                var articuls = _repository.GetArticuls();
                var parts = _repository.GetParts();
                var machines = _repository.GetMachines();
                var coefficients = _repository.GetCoefficients();

                Articuls.Clear();
                foreach (var articul in articuls)
                {
                    Articuls.Add(articul);
                }

                Parts.Clear();
                foreach (var part in parts)
                {
                    Parts.Add(part);
                }

                Machines.Clear();
                foreach (var machine in machines)
                {
                    Machines.Add(machine);
                }

                Coefficients.Clear();
                foreach (var coefficient in coefficients)
                {
                    Coefficients.Add(coefficient);
                }

                SelectedArticul = Articuls.FirstOrDefault();
                SelectedPart = Parts.FirstOrDefault();
                SelectedMachine = Machines.FirstOrDefault();
                SelectedCoefficient1 = Coefficients.FirstOrDefault();
                SelectedCoefficient2 = Coefficients.FirstOrDefault();
                SelectedModelPartAmount = null;
                SelectedMachineCarriageAmount = null;
                AvgValue = string.Empty;
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(LoadErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void Search()
        {
            var articul = SelectedArticul;

            if (articul == null)
            {
                _dialogService.ShowWarning(NoArticulForSearchMessage, WarningTitle);
                return;
            }

            Rows.Clear();
            SelectedRow = null;
            Total = string.Empty;
            Norm = string.Empty;
            _isLoadedFromDb = false;

            try
            {
                var entries = _repository.GetRouting(articul.ArticulId);

                if (entries.Count == 0)
                {
                    _dialogService.ShowInfo(NoRecordsMessage, InformationTitle);
                    return;
                }

                var total = 0m;

                foreach (var entry in entries)
                {
                    Rows.Add(new KnittingRoutingRow
                    {
                        PartId = entry.PartId,
                        PartName = entry.PartName,
                        MachineId = entry.MachineId,
                        MachineName = entry.MachineName,
                        ModelPartAmount = entry.ProductPartsAmount.ToString(CultureInfo.CurrentCulture),
                        MachineCarriageAmount = entry.MachinePartsAmount.ToString(CultureInfo.CurrentCulture),
                        Coefficient1 = entry.K1.ToString(CultureInfo.CurrentCulture),
                        Coefficient2 = entry.K2.ToString(CultureInfo.CurrentCulture),
                        AvgValue = KnittingRoutingRules.FormatLoadedValue(entry.AvgRate),
                        WithCoefficient = KnittingRoutingRules.FormatLoadedValue(entry.CoefficientRate),
                        WithKit = KnittingRoutingRules.FormatLoadedValue(entry.KitRate)
                    });

                    total += Convert.ToDecimal(entry.KitRate);
                }

                Total = KnittingRoutingRules.FormatTotal(total);
                Norm = KnittingRoutingRules.CalculateNorm(total);
                SelectedRow = Rows.FirstOrDefault();
                _isLoadedFromDb = true;
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(LoadErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void Add()
        {
            var missingField = FindMissingField();

            if (missingField != null)
            {
                _dialogService.ShowWarning(
                    string.Format(EmptyFieldMessage, missingField),
                    WarningTitle);
                return;
            }

            try
            {
                var row = new KnittingRoutingRow
                {
                    PartId = SelectedPart.PartId,
                    PartName = SelectedPart.PartName,
                    MachineId = SelectedMachine.MachineId,
                    MachineName = SelectedMachine.DisplayName,
                    ModelPartAmount = SelectedModelPartAmount,
                    MachineCarriageAmount = SelectedMachineCarriageAmount,
                    Coefficient1 = SelectedCoefficient1.Display,
                    Coefficient2 = SelectedCoefficient2.Display,
                    AvgValue = AvgValue,
                    WithCoefficient = WithCoefficient,
                    WithKit = WithKit
                };

                Rows.Add(row);
                SelectedRow = row;
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(AddErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void Delete()
        {
            var row = SelectedRow;

            if (row == null)
            {
                _dialogService.ShowWarning(NoRowSelectedMessage, WarningTitle);
                return;
            }

            try
            {
                if (!_dialogService.Confirm(DeleteRowMessage, DeleteConfirmationTitle, DialogIcon.Question))
                {
                    return;
                }

                Rows.Remove(row);
                SelectedRow = Rows.LastOrDefault();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(DeleteErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void Save()
        {
            var articul = SelectedArticul;

            if (articul == null)
            {
                _dialogService.ShowWarning(NoArticulForSaveMessage, ErrorTitle);
                return;
            }

            var hasRows = Rows.Count > 0;

            if (!hasRows)
            {
                if (_isLoadedFromDb)
                {
                    if (!_dialogService.Confirm(DeleteAllMessage, DeleteConfirmationTitle, DialogIcon.Question))
                    {
                        return;
                    }
                }
                else
                {
                    _dialogService.ShowWarning(EmptyTableMessage, WarningTitle);
                    return;
                }
            }

            try
            {
                var entries = new List<KnittingRoutingEntry>();

                foreach (var row in Rows)
                {
                    if (row.PartId == 0 || row.MachineId == 0)
                    {
                        throw new InvalidOperationException(
                            string.Format(MissingIdMessage, row.PartName, row.MachineName));
                    }

                    entries.Add(new KnittingRoutingEntry
                    {
                        PartId = row.PartId,
                        PartName = row.PartName,
                        MachineId = row.MachineId,
                        MachineName = row.MachineName,
                        ProductPartsAmount = KnittingRoutingRules.ToInteger(
                            KnittingRoutingRules.ParseDecimal(row.ModelPartAmount)),
                        MachinePartsAmount = KnittingRoutingRules.ToInteger(
                            KnittingRoutingRules.ParseDecimal(row.MachineCarriageAmount)),
                        K1 = (double)KnittingRoutingRules.ParseDecimal(row.Coefficient1),
                        K2 = (double)KnittingRoutingRules.ParseDecimal(row.Coefficient2),
                        AvgRate = (double)KnittingRoutingRules.ParseDecimal(row.AvgValue),
                        CoefficientRate = (double)KnittingRoutingRules.ParseDecimal(row.WithCoefficient),
                        KitRate = (double)KnittingRoutingRules.ParseDecimal(row.WithKit)
                    });
                }

                _repository.Save(articul.ArticulId, entries);
                _isLoadedFromDb = hasRows;
                _dialogService.ShowInfo(SaveSuccessMessage, SuccessTitle);
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(SaveErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void CalculateWithCoefficient()
        {
            var average = KnittingRoutingRules.ParseDecimal(AvgValue);

            if (string.IsNullOrWhiteSpace(AvgValue) || average == 0)
            {
                WithCoefficient = string.Empty;
                WithKit = string.Empty;
                return;
            }

            var coefficient1 = SelectedCoefficient1 == null
                ? 0
                : KnittingRoutingRules.ParseDecimal(SelectedCoefficient1.Display);
            var coefficient2 = SelectedCoefficient2 == null
                ? 0
                : KnittingRoutingRules.ParseDecimal(SelectedCoefficient2.Display);

            WithCoefficient = KnittingRoutingRules.FormatWithCoefficient(
                KnittingRoutingRules.CalculateWithCoefficient(average, coefficient1, coefficient2));

            CalculateWithKit();
        }

        private void CalculateWithKit()
        {
            if (string.IsNullOrWhiteSpace(WithCoefficient))
            {
                WithKit = string.Empty;
                return;
            }

            var withCoefficient = KnittingRoutingRules.ParseDecimal(WithCoefficient);

            if (withCoefficient == 0)
            {
                WithKit = "0.000";
                return;
            }

            var modelPartAmount = KnittingRoutingRules.ParseDecimal(SelectedModelPartAmount);
            var machineCarriageAmount = KnittingRoutingRules.ParseDecimal(SelectedMachineCarriageAmount);

            WithKit = KnittingRoutingRules.FormatWithKit(
                KnittingRoutingRules.CalculateWithKit(
                    withCoefficient,
                    modelPartAmount,
                    machineCarriageAmount));
        }

        private string FindMissingField()
        {
            if (SelectedPart == null || string.IsNullOrWhiteSpace(SelectedPart.PartName))
            {
                return "Деталь";
            }

            if (SelectedMachine == null || string.IsNullOrWhiteSpace(SelectedMachine.DisplayName))
            {
                return "Станок";
            }

            if (string.IsNullOrWhiteSpace(SelectedModelPartAmount))
            {
                return "Количество деталей";
            }

            if (string.IsNullOrWhiteSpace(SelectedMachineCarriageAmount))
            {
                return "Количество кареток";
            }

            if (SelectedCoefficient1 == null)
            {
                return "Коэффициент 1";
            }

            if (SelectedCoefficient2 == null)
            {
                return "Коэффициент 2";
            }

            if (string.IsNullOrWhiteSpace(AvgValue))
            {
                return "Среднее значение";
            }

            if (string.IsNullOrWhiteSpace(WithCoefficient))
            {
                return "С коэффициентом";
            }

            if (string.IsNullOrWhiteSpace(WithKit))
            {
                return "С комплектом";
            }

            return null;
        }

        private void ClearLoadedState()
        {
            Rows.Clear();
            SelectedRow = null;
            Total = string.Empty;
            Norm = string.Empty;
            _isLoadedFromDb = false;
        }
    }
}
