using System;
using ProdControlWPF.Infrastructure;

namespace ProdControlWPF.Models
{
    public sealed class CalculationRow : ObservableObject
    {
        private string _salary = string.Empty;
        private string _norm = string.Empty;
        private string _timeExecution = string.Empty;

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string EmployeeNumber { get; set; } = string.Empty;

        public int PositionId { get; set; }

        public int PositionLevelId { get; set; }

        public string Salary
        {
            get { return _salary; }
            set { SetProperty(ref _salary, value ?? string.Empty); }
        }

        public string Norm
        {
            get { return _norm; }
            set { SetProperty(ref _norm, value ?? string.Empty); }
        }

        public string TimeExecution
        {
            get { return _timeExecution; }
            set { SetProperty(ref _timeExecution, value ?? string.Empty); }
        }
    }

    public sealed class KnittingZoneTime
    {
        public int MachinesInZone { get; set; }

        public double TotalTime { get; set; }
    }

    public sealed class SewingSalaryEntry
    {
        public double Count { get; set; }

        public double Tariff { get; set; }

        public double TimeExecution { get; set; }

        public int ArticulId { get; set; }
    }

    public sealed class SewingTimeEntry
    {
        public int OperationId { get; set; }

        public int ArticulId { get; set; }

        public double TimeExecution { get; set; }

        public double Count { get; set; }
    }

    public sealed class WorkDayEntry
    {
        public int WorkDay { get; set; }

        public DateTime DateExecution { get; set; }
    }
}
