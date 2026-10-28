using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ProdControlWPF.Data;
using ProdControlWPF.Domain;
using ProdControlWPF.Infrastructure;
using ProdControlWPF.Models;
using ProdControlWPF.Services;

namespace ProdControlWPF.ViewModels
{
    public sealed class TariffsCheckViewModel : ObservableObject
    {
        private const string ErrorTitle = "Ошибка";
        private const string NotificationTitle = "Уведомление";
        private const string LoadErrorPrefix = "Ошибка при загрузке данных: ";
        private const string ConfirmAllMessage =
            "Вы уверены, что хотите запустить проверку наличия расценок работ для всех артикулов?";
        private const string CheckingAllMessage =
            "Идет проверка наличия расценок работ для всех артикулов, пожалуйста подождите окончания операции!";
        private const string CheckingSelectedMessage =
            "Идет проверка наличия расценок работ для выбранных артикулов, пожалуйста подождите окончания операции!";
        private const string NoArticulsMessage =
            "Не было выбрано ни одного артикула для проверки расценок операций!";
        private const string TariffsUpdatedMessage =
            "Операция завершена! Были найдены артикулы с операциями без расценки. " +
            "В базу данных были внесены новые расценки.";
        private const string TariffsNotFoundMessage =
            "Операция завершена! Операций без расценок под соответствующие артикулы не было найдено.";

        private readonly ITariffsCheckRepository _repository;
        private readonly IDialogService _dialogService;

        private ArticulLookup _selectedArticul;
        private ArticulLookup _selectedListItem;
        private bool _selectAll;

        public TariffsCheckViewModel(
            ITariffsCheckRepository repository,
            IDialogService dialogService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Articuls = new ObservableCollection<ArticulLookup>();
            SelectedArticuls = new ObservableCollection<ArticulLookup>();

            SelectArticulCommand = new RelayCommand(SelectArticul);
            RemoveArticulCommand = new RelayCommand(RemoveArticul);
            ClearListCommand = new RelayCommand(ClearList);
            CheckTariffsCommand = new RelayCommand(CheckTariffs);

            Load();
        }

        public ObservableCollection<ArticulLookup> Articuls { get; }

        public ObservableCollection<ArticulLookup> SelectedArticuls { get; }

        public ICommand SelectArticulCommand { get; }

        public ICommand RemoveArticulCommand { get; }

        public ICommand ClearListCommand { get; }

        public ICommand CheckTariffsCommand { get; }

        public ArticulLookup SelectedArticul
        {
            get { return _selectedArticul; }
            set { SetProperty(ref _selectedArticul, value); }
        }

        public ArticulLookup SelectedListItem
        {
            get { return _selectedListItem; }
            set { SetProperty(ref _selectedListItem, value); }
        }

        public bool SelectAll
        {
            get { return _selectAll; }
            set
            {
                if (!SetProperty(ref _selectAll, value))
                {
                    return;
                }

                SelectedArticuls.Clear();
                SelectedListItem = null;

                if (!value)
                {
                    return;
                }

                foreach (var articul in Articuls)
                {
                    SelectedArticuls.Add(articul);
                }

                SelectedListItem = SelectedArticuls.FirstOrDefault();
            }
        }

        private void Load()
        {
            try
            {
                var articuls = _repository.GetArticuls();

                Articuls.Clear();
                foreach (var articul in articuls)
                {
                    Articuls.Add(articul);
                }

                SelectedArticul = Articuls.FirstOrDefault();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(LoadErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void SelectArticul()
        {
            var articul = SelectedArticul;

            if (articul == null)
            {
                return;
            }

            if (SelectedArticuls.Any(item => item.ArticulId == articul.ArticulId))
            {
                return;
            }

            SelectedArticuls.Add(articul);
            SelectedListItem = SelectedArticuls.FirstOrDefault();
        }

        private void RemoveArticul()
        {
            var articul = SelectedListItem;

            if (articul == null)
            {
                return;
            }

            SelectedArticuls.Remove(articul);
            SelectedListItem = SelectedArticuls.FirstOrDefault();
        }

        private void ClearList()
        {
            SelectedArticuls.Clear();
            SelectedListItem = null;

            if (SelectAll)
            {
                SelectAll = false;
            }
        }

        private void CheckTariffs()
        {
            if (SelectAll && SelectedArticuls.Count > 0)
            {
                if (!_dialogService.Confirm(ConfirmAllMessage, NotificationTitle, DialogIcon.Information))
                {
                    return;
                }

                _dialogService.ShowInfo(CheckingAllMessage, NotificationTitle);
                CalculateTariffs();
                return;
            }

            if (!SelectAll && SelectedArticuls.Count > 0)
            {
                _dialogService.ShowInfo(CheckingSelectedMessage, NotificationTitle);
                CalculateTariffs();
                return;
            }

            _dialogService.ShowError(NoArticulsMessage, ErrorTitle);
        }

        private void CalculateTariffs()
        {
            var selectedIds = new HashSet<int>();
            foreach (var articul in SelectedArticuls)
            {
                selectedIds.Add(articul.ArticulId);
            }

            try
            {
                var candidates = _repository.GetMissingTariffs();
                var updates = new List<TariffUpdate>();

                foreach (var candidate in candidates)
                {
                    if (!selectedIds.Contains(candidate.ArticulId))
                    {
                        continue;
                    }

                    var tariff = TariffsCheckRules.CalculateTariff(
                        candidate.TimeExecute,
                        candidate.PositionRateTime);

                    updates.Add(new TariffUpdate(candidate.ArticulId, candidate.OperationId, tariff));
                }

                if (updates.Count > 0)
                {
                    _repository.SaveTariffs(updates);
                    _dialogService.ShowWarning(TariffsUpdatedMessage, NotificationTitle);
                }
                else
                {
                    _dialogService.ShowInfo(TariffsNotFoundMessage, NotificationTitle);
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }
    }
}
