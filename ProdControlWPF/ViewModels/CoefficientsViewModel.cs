using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using ProdControlWPF.Data;
using ProdControlWPF.Infrastructure;
using ProdControlWPF.Models;
using ProdControlWPF.Services;

namespace ProdControlWPF.ViewModels
{
    public sealed class CoefficientsViewModel : ObservableObject
    {
        private const string ErrorTitle = "Ошибка";
        private const string NotificationTitle = "Уведомление";
        private const string LoadErrorPrefix = "Ошибка при загрузке данных: ";
        private const string InvalidNumberMessage =
            "Число не соответствует числовому формату или не было задано!";
        private const string DuplicateMessage =
            "Данный коэффициент уже присутствует в базе данных!";
        private const string NoSelectionMessage = "Выберите коэффициент для удаления.";

        private readonly ICoefficientsRepository _repository;
        private readonly IDialogService _dialogService;

        private string _newCoefficientValue = string.Empty;
        private bool _isEditMode;
        private CoefficientItem _selectedCoefficient;

        public CoefficientsViewModel(ICoefficientsRepository repository, IDialogService dialogService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Coefficients = new ObservableCollection<CoefficientItem>();

            AddCoefficientCommand = new RelayCommand(AddCoefficient, () => IsEditMode);
            DeleteCoefficientCommand = new RelayCommand(DeleteCoefficient, () => IsEditMode);

            Load();
        }

        public ObservableCollection<CoefficientItem> Coefficients { get; }

        public ICommand AddCoefficientCommand { get; }

        public ICommand DeleteCoefficientCommand { get; }

        public string NewCoefficientValue
        {
            get { return _newCoefficientValue; }
            set { SetProperty(ref _newCoefficientValue, value ?? string.Empty); }
        }

        public bool IsEditMode
        {
            get { return _isEditMode; }
            set { SetProperty(ref _isEditMode, value); }
        }

        public CoefficientItem SelectedCoefficient
        {
            get { return _selectedCoefficient; }
            set { SetProperty(ref _selectedCoefficient, value); }
        }

        private void Load()
        {
            try
            {
                ReloadCoefficients();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(LoadErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void AddCoefficient()
        {
            if (!IsEditMode)
            {
                return;
            }

            if (!TryParseCoefficient(NewCoefficientValue, out var value))
            {
                _dialogService.ShowError(InvalidNumberMessage, ErrorTitle);
                return;
            }

            try
            {
                if (_repository.CoefficientExists(value))
                {
                    _dialogService.ShowInfo(DuplicateMessage, NotificationTitle);
                    return;
                }

                _repository.InsertCoefficient(value);
                ReloadCoefficients();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }

        private void DeleteCoefficient()
        {
            if (!IsEditMode)
            {
                return;
            }

            var coefficient = SelectedCoefficient;

            if (coefficient == null)
            {
                _dialogService.ShowError(NoSelectionMessage, ErrorTitle);
                return;
            }

            try
            {
                _repository.DeleteCoefficient(coefficient.Id, coefficient.Value);
                ReloadCoefficients();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }

        private void ReloadCoefficients()
        {
            var coefficients = _repository.GetCoefficients();

            Coefficients.Clear();
            foreach (var coefficient in coefficients)
            {
                Coefficients.Add(coefficient);
            }

            SelectedCoefficient = Coefficients.FirstOrDefault();
        }

        private static bool TryParseCoefficient(string text, out double value)
        {
            return double.TryParse(
                text,
                NumberStyles.Float | NumberStyles.AllowThousands,
                CultureInfo.CurrentCulture,
                out value);
        }
    }
}
