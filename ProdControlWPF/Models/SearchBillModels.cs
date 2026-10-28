using System;
using ProdControlWPF.Infrastructure;

namespace ProdControlWPF.Models
{
    public enum SearchBillFilterMode
    {
        Employee = 0,
        Articul = 1,
        EmployeeAndArticul = 2,
        DocumentNumber = 3
    }

    public sealed class SearchBillCriteria
    {
        public SearchBillFilterMode FilterMode { get; set; }

        public int? EmployeeId { get; set; }

        public int? ArticulId { get; set; }

        public string DocumentNumber { get; set; }

        public bool UsePeriod { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }

    public sealed class SearchBillUpdate
    {
        public int LogId { get; set; }

        public int EmployeeId { get; set; }

        public int OperationId { get; set; }

        public int ArticulId { get; set; }

        public DateTime? DateExecution { get; set; }

        public int TimeExecution { get; set; }

        public int Count { get; set; }

        public string DocumentNumber { get; set; }

        public int WorkDay { get; set; }
    }

    public sealed class SearchBillRow : ObservableObject
    {
        private DateTime? _dateExecution;
        private string _timeExecution = "0";
        private string _count = "0";
        private string _documentNumber = string.Empty;
        private string _workDay = "0";

        public int LogId { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public int OperationId { get; set; }

        public string OperationName { get; set; } = string.Empty;

        public int ArticulId { get; set; }

        public string ArticulName { get; set; } = string.Empty;

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

        public string DocumentNumber
        {
            get { return _documentNumber; }
            set { SetProperty(ref _documentNumber, value); }
        }

        public string WorkDay
        {
            get { return _workDay; }
            set { SetProperty(ref _workDay, value ?? string.Empty); }
        }
    }
}
