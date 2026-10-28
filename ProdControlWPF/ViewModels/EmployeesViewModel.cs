using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using ProdControlWPF.Data;
using ProdControlWPF.Infrastructure;
using ProdControlWPF.Models;
using ProdControlWPF.Services;

namespace ProdControlWPF.ViewModels
{
    public sealed class EmployeesViewModel : ObservableObject
    {
        private const string ErrorTitle = "Ошибка";
        private const string NoticeTitle = "Уведомление";
        private const string LoadErrorPrefix = "Ошибка при загрузке данных: ";
        private const string LevelSearchMessage = "Поле 'Разряд' должно быть целочисленным!";
        private const string LevelCellMessage = "Поле 'Разряд должности' должно быть целочисленным!";
        private const string RequiredFieldsMessage =
            "Для добавления нового поля все ячейки должны быть заполнены!";
        private const string SavedMessage = "Изменения сохранены в базе данных!";
        private const string NoChangesMessage = "Нет изменений для сохранения!";
        private const string NoSelectionMessage = "Выберите сотрудника для удаления.";
        private const string NoPositionMessage = "Выберите должность.";

        private readonly IEmployeesRepository _repository;
        private readonly IDialogService _dialogService;
        private readonly Dictionary<int, EmployeePositionLookup> _positionsById =
            new Dictionary<int, EmployeePositionLookup>();
        private readonly List<int> _employeeIdsForRemoval = new List<int>();
        private readonly Dictionary<int, EmployeeUpdate> _employeeRowsForUpdate =
            new Dictionary<int, EmployeeUpdate>();
        private readonly HashSet<EmployeeRow> _invalidRows = new HashSet<EmployeeRow>();

        private int _selectedFilterIndex = -1;
        private string _searchText = string.Empty;
        private EmployeePositionLookup _selectedPosition;
        private EmployeeRow _selectedEmployee;
        private bool _isEditingMode;
        private bool _trackRowChanges;

        public EmployeesViewModel(IEmployeesRepository repository, IDialogService dialogService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Positions = new ObservableCollection<EmployeePositionLookup>();
            Employees = new ObservableCollection<EmployeeRow>();

            FilterOptions = new[]
            {
                "По ФИО",
                "По номеру",
                "По должности",
                "По разряду"
            };

            SearchCommand = new RelayCommand(Search);
            SearchAllCommand = new RelayCommand(SearchAll);
            AddCommand = new RelayCommand(Add);
            DeleteCommand = new RelayCommand(Delete);
            SaveCommand = new RelayCommand(Save);

            Load();
        }

        public ObservableCollection<EmployeePositionLookup> Positions { get; }

        public ObservableCollection<EmployeeRow> Employees { get; }

        public IReadOnlyList<string> FilterOptions { get; }

        public ICommand SearchCommand { get; }

        public ICommand SearchAllCommand { get; }

        public ICommand AddCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand SaveCommand { get; }

        public int SelectedFilterIndex
        {
            get { return _selectedFilterIndex; }
            set
            {
                if (!SetProperty(ref _selectedFilterIndex, value))
                {
                    return;
                }

                SearchText = string.Empty;

                OnPropertyChanged(nameof(IsNameFilterVisible));
                OnPropertyChanged(nameof(IsNumberFilterVisible));
                OnPropertyChanged(nameof(IsPositionFilterVisible));
                OnPropertyChanged(nameof(IsLevelFilterVisible));
                OnPropertyChanged(nameof(IsSearchTextBoxVisible));
            }
        }

        public bool IsNameFilterVisible
        {
            get { return _selectedFilterIndex == 0; }
        }

        public bool IsNumberFilterVisible
        {
            get { return _selectedFilterIndex == 1; }
        }

        public bool IsPositionFilterVisible
        {
            get { return _selectedFilterIndex == 2; }
        }

        public bool IsLevelFilterVisible
        {
            get { return _selectedFilterIndex == 3; }
        }

        public bool IsSearchTextBoxVisible
        {
            get { return _selectedFilterIndex == 0 || _selectedFilterIndex == 1 || _selectedFilterIndex == 3; }
        }

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value ?? string.Empty); }
        }

        public EmployeePositionLookup SelectedPosition
        {
            get { return _selectedPosition; }
            set { SetProperty(ref _selectedPosition, value); }
        }

        public EmployeeRow SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { SetProperty(ref _selectedEmployee, value); }
        }

        public bool IsEditingMode
        {
            get { return _isEditingMode; }
            set
            {
                if (SetProperty(ref _isEditingMode, value))
                {
                    OnPropertyChanged(nameof(IsGridReadOnly));
                }
            }
        }

        public bool IsGridReadOnly
        {
            get { return !_isEditingMode; }
        }

        private void Load()
        {
            try
            {
                LoadPositions();
                SelectedPosition = Positions.FirstOrDefault();
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(LoadErrorPrefix + exception.Message, ErrorTitle);
            }
        }

        private void SearchAll()
        {
            ResetRowsAndChanges();

            try
            {
                LoadPositions();
                SelectedPosition = Positions.FirstOrDefault();
                LoadRows(_repository.GetEmployees());
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }

        private void Search()
        {
            if (_selectedFilterIndex < 0)
            {
                return;
            }

            Func<IReadOnlyList<EmployeeRow>> query;

            switch (_selectedFilterIndex)
            {
                case 0:
                    query = () => _repository.SearchByName(SearchText);
                    break;
                case 1:
                    query = () => _repository.SearchByNumber(SearchText);
                    break;
                case 2:
                    var position = SelectedPosition;

                    if (position == null)
                    {
                        _dialogService.ShowError(NoPositionMessage, ErrorTitle);
                        return;
                    }

                    query = () => _repository.SearchByPosition(position.PositionId);
                    break;
                case 3:
                    if (!int.TryParse(
                        SearchText,
                        NumberStyles.Integer,
                        CultureInfo.CurrentCulture,
                        out var positionLevel))
                    {
                        _dialogService.ShowError(LevelSearchMessage, ErrorTitle);
                        return;
                    }

                    query = () => _repository.SearchByLevel(positionLevel);
                    break;
                default:
                    return;
            }

            ResetRowsAndChanges();

            try
            {
                LoadRows(query());
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }

        private void Add()
        {
            var row = new EmployeeRow
            {
                IsNew = true,
                EmployeeName = string.Empty,
                EmployeeNumber = string.Empty,
                PositionLevel = string.Empty
            };

            row.PropertyChanged += OnRowPropertyChanged;
            Employees.Add(row);
            SelectedEmployee = row;
        }

        private void Delete()
        {
            var row = SelectedEmployee;

            if (row == null)
            {
                _dialogService.ShowError(NoSelectionMessage, ErrorTitle);
                return;
            }

            var index = Employees.IndexOf(row);
            row.PropertyChanged -= OnRowPropertyChanged;

            if (!row.IsNew)
            {
                _employeeIdsForRemoval.Add(row.EmployeeId);
                _employeeRowsForUpdate.Remove(row.EmployeeId);
            }

            _invalidRows.Remove(row);
            Employees.Remove(row);

            SelectedEmployee = Employees.Count == 0
                ? null
                : Employees[Math.Max(0, Math.Min(index, Employees.Count - 1))];
        }

        private void Save()
        {
            var inserts = new List<EmployeeInsert>();

            foreach (var row in Employees.Where(item => item.IsNew))
            {
                if (string.IsNullOrEmpty(row.EmployeeName)
                    || string.IsNullOrEmpty(row.EmployeeNumber)
                    || !row.PositionId.HasValue
                    || string.IsNullOrEmpty(row.PositionLevel))
                {
                    _dialogService.ShowError(RequiredFieldsMessage, ErrorTitle);
                    return;
                }

                if (!TryParsePositionLevel(row.PositionLevel, out var positionLevel))
                {
                    _dialogService.ShowError(LevelCellMessage, ErrorTitle);
                    return;
                }

                inserts.Add(new EmployeeInsert
                {
                    EmployeeName = row.EmployeeName,
                    EmployeeNumber = row.EmployeeNumber,
                    PositionId = row.PositionId.Value,
                    PositionLevel = positionLevel
                });
            }

            if (_invalidRows.Count > 0)
            {
                _dialogService.ShowError(
                    _invalidRows.Any(row => !row.PositionId.HasValue)
                        ? NoPositionMessage
                        : LevelCellMessage,
                    ErrorTitle);
                return;
            }

            var updates = _employeeRowsForUpdate.Values.ToList();

            if (_employeeIdsForRemoval.Count == 0 && inserts.Count == 0 && updates.Count == 0)
            {
                _dialogService.ShowInfo(NoChangesMessage, NoticeTitle);
                return;
            }

            try
            {
                _repository.Save(_employeeIdsForRemoval.ToList(), inserts, updates);

                if (inserts.Count > 0)
                {
                    ResetRowsAndChanges();
                    LoadRows(_repository.GetEmployees());
                }
                else
                {
                    _employeeIdsForRemoval.Clear();
                    _employeeRowsForUpdate.Clear();
                    _invalidRows.Clear();
                }

                _dialogService.ShowInfo(SavedMessage, NoticeTitle);
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, ErrorTitle);
            }
        }

        private void OnRowPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_trackRowChanges)
            {
                return;
            }

            var row = sender as EmployeeRow;

            if (row == null)
            {
                return;
            }

            if (e.PropertyName == nameof(EmployeeRow.PositionId))
            {
                ApplyPositionName(row);
            }

            if (!IsEditingMode)
            {
                return;
            }

            if (e.PropertyName != nameof(EmployeeRow.EmployeeName)
                && e.PropertyName != nameof(EmployeeRow.EmployeeNumber)
                && e.PropertyName != nameof(EmployeeRow.PositionId)
                && e.PropertyName != nameof(EmployeeRow.PositionLevel))
            {
                return;
            }

            if (row.IsNew)
            {
                return;
            }

            if (!row.PositionId.HasValue)
            {
                _employeeRowsForUpdate.Remove(row.EmployeeId);
                _invalidRows.Add(row);
                return;
            }

            if (!TryParsePositionLevel(row.PositionLevel, out var positionLevel))
            {
                _employeeRowsForUpdate.Remove(row.EmployeeId);
                _invalidRows.Add(row);
                _dialogService.ShowError(LevelCellMessage, ErrorTitle);
                return;
            }

            _invalidRows.Remove(row);
            _employeeRowsForUpdate[row.EmployeeId] = new EmployeeUpdate
            {
                EmployeeId = row.EmployeeId,
                EmployeeName = row.EmployeeName,
                EmployeeNumber = row.EmployeeNumber,
                PositionId = row.PositionId.Value,
                PositionLevel = positionLevel
            };
        }

        private void LoadPositions()
        {
            var positions = _repository.GetPositions();

            Positions.Clear();
            _positionsById.Clear();

            foreach (var position in positions)
            {
                Positions.Add(position);
                _positionsById[position.PositionId] = position;
            }
        }

        private void LoadRows(IReadOnlyList<EmployeeRow> rows)
        {
            _trackRowChanges = false;

            try
            {
                foreach (var row in rows)
                {
                    ApplyPositionName(row);
                    row.PropertyChanged += OnRowPropertyChanged;
                    Employees.Add(row);
                }

                SelectedEmployee = Employees.FirstOrDefault();
            }
            finally
            {
                _trackRowChanges = true;
            }
        }

        private void ResetRowsAndChanges()
        {
            _trackRowChanges = false;

            try
            {
                foreach (var row in Employees)
                {
                    row.PropertyChanged -= OnRowPropertyChanged;
                }

                Employees.Clear();
                SelectedEmployee = null;

                _employeeIdsForRemoval.Clear();
                _employeeRowsForUpdate.Clear();
                _invalidRows.Clear();
            }
            finally
            {
                _trackRowChanges = true;
            }
        }

        private void ApplyPositionName(EmployeeRow row)
        {
            if (row.PositionId.HasValue
                && _positionsById.TryGetValue(row.PositionId.Value, out var position))
            {
                row.PositionName = position.PositionName;
            }
            else
            {
                row.PositionName = string.Empty;
            }
        }

        private static bool TryParsePositionLevel(string text, out int positionLevel)
        {
            return int.TryParse(
                text,
                NumberStyles.Integer,
                CultureInfo.CurrentCulture,
                out positionLevel);
        }
    }
}
