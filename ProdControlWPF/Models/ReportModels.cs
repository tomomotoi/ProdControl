using System;

namespace ProdControlWPF.Models
{
    public sealed class ReportEmployeeListItem
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string EmployeeNumber { get; set; } = string.Empty;
    }

    public sealed class EmployeeReportRow
    {
        public string EmployeeName { get; set; } = string.Empty;

        public DateTime DateExecution { get; set; }

        public string DocumentNumber { get; set; } = string.Empty;

        public string OperationName { get; set; } = string.Empty;

        public string ArticulName { get; set; } = string.Empty;

        public int Count { get; set; }

        public int EmployeeId { get; set; }

        public double RateTime { get; set; }

        public int NotRate { get; set; }
    }
}
