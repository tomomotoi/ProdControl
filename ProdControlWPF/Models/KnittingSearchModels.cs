using System;
using ProdControlWPF.Infrastructure;

namespace ProdControlWPF.Models
{
    public enum KnittingSearchFilterMode
    {
        Employee = 0,
        Articul = 1,
        EmployeeAndArticul = 2,
        DocumentNumber = 3
    }

    public sealed class KnittingSearchCriteria
    {
        public KnittingSearchFilterMode FilterMode { get; set; }

        public int? EmployeeId { get; set; }

        public int? ArticulId { get; set; }

        public string DocumentNumber { get; set; }

        public bool UsePeriod { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }

    public sealed class KnittingSearchUpdate
    {
        public int LogId { get; set; }

        public string DocumentNumber { get; set; }

        public int? ArticulId { get; set; }

        public int? EmployeeId { get; set; }

        public int? PartId { get; set; }

        public int Amount { get; set; }

        public int? MachineId { get; set; }

        public DateTime? DateExecution { get; set; }

        public double? TimeExecution { get; set; }

        public int MachinesInZone { get; set; }

        public int WorkDay { get; set; }
    }

    public sealed class KnittingSearchRow : ObservableObject
    {
        private DateTime? _dateExecution;
        private string _timeExecution = "0";
        private string _count = "0";
        private string _machinesInZone = "0";
        private string _documentNumber = string.Empty;
        private string _workDay = "0";
        private bool _isDirty;

        public int LogId { get; set; }

        public string DocumentNumber
        {
            get { return _documentNumber; }
            set { SetProperty(ref _documentNumber, value ?? string.Empty); }
        }

        public int? ArticulId { get; set; }

        public string ArticulName { get; set; } = string.Empty;

        public int? EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public int? PartId { get; set; }

        public string PartName { get; set; } = string.Empty;

        public int? MachineId { get; set; }

        public string MachineName { get; set; } = string.Empty;

        public DateTime? DateExecution
        {
            get { return _dateExecution; }
            set { SetProperty(ref _dateExecution, value); }
        }

        public string TimeExecution
        {
            get { return _timeExecution; }
            set { SetProperty(ref _timeExecution, value ?? string.Empty); }
        }

        public string Count
        {
            get { return _count; }
            set { SetProperty(ref _count, value ?? string.Empty); }
        }

        public string MachinesInZone
        {
            get { return _machinesInZone; }
            set { SetProperty(ref _machinesInZone, value ?? string.Empty); }
        }

        public string WorkDay
        {
            get { return _workDay; }
            set { SetProperty(ref _workDay, value ?? string.Empty); }
        }

        public bool IsDirty
        {
            get { return _isDirty; }
            set { SetProperty(ref _isDirty, value); }
        }
    }
}
