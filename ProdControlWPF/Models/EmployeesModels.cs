using ProdControlWPF.Infrastructure;

namespace ProdControlWPF.Models
{
    public sealed class EmployeePositionLookup
    {
        public EmployeePositionLookup(int positionId, string positionName)
        {
            PositionId = positionId;
            PositionName = positionName ?? string.Empty;
        }

        public int PositionId { get; }

        public string PositionName { get; }
    }

    public sealed class EmployeeRow : ObservableObject
    {
        private string _employeeName = string.Empty;
        private string _employeeNumber = string.Empty;
        private int? _positionId;
        private string _positionName = string.Empty;
        private string _positionLevel = string.Empty;

        public int EmployeeId { get; set; }

        public bool IsNew { get; set; }

        public string EmployeeName
        {
            get { return _employeeName; }
            set { SetProperty(ref _employeeName, value ?? string.Empty); }
        }

        public string EmployeeNumber
        {
            get { return _employeeNumber; }
            set { SetProperty(ref _employeeNumber, value ?? string.Empty); }
        }

        public int? PositionId
        {
            get { return _positionId; }
            set { SetProperty(ref _positionId, value); }
        }

        public string PositionName
        {
            get { return _positionName; }
            set { SetProperty(ref _positionName, value ?? string.Empty); }
        }

        public string PositionLevel
        {
            get { return _positionLevel; }
            set { SetProperty(ref _positionLevel, value ?? string.Empty); }
        }
    }

    public sealed class EmployeeInsert
    {
        public string EmployeeName { get; set; }

        public string EmployeeNumber { get; set; }

        public int PositionId { get; set; }

        public int PositionLevel { get; set; }
    }

    public sealed class EmployeeUpdate
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeNumber { get; set; }

        public int PositionId { get; set; }

        public int PositionLevel { get; set; }
    }
}
