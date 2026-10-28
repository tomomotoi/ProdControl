using System;

namespace ProdControlWPF.Models
{
    public sealed class EmployeeLookup
    {
        public EmployeeLookup(int employeeId, string employeeName)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName ?? string.Empty;
        }

        public int EmployeeId { get; }

        public string EmployeeName { get; }
    }

    public sealed class ArticulLookup
    {
        public ArticulLookup(int articulId, string articulName)
        {
            ArticulId = articulId;
            ArticulName = articulName ?? string.Empty;
        }

        public int ArticulId { get; }

        public string ArticulName { get; }
    }

    public sealed class OperationLookup
    {
        public OperationLookup(int operationId, string operationName, int operationTimeWithKoef)
        {
            OperationId = operationId;
            OperationName = operationName ?? string.Empty;
            OperationTimeWithKoef = operationTimeWithKoef;
        }

        public int OperationId { get; }

        public string OperationName { get; }

        public int OperationTimeWithKoef { get; }
    }

    public sealed class IdleOperationLookup
    {
        public IdleOperationLookup(int idleOperationId, string idleOperationName, double costPercentage)
        {
            IdleOperationId = idleOperationId;
            IdleOperationName = idleOperationName ?? string.Empty;
            CostPercentage = costPercentage;
        }

        public int IdleOperationId { get; }

        public string IdleOperationName { get; }

        public double CostPercentage { get; }
    }

    public sealed class OperationLogRow
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime DateExecution { get; set; }

        public int ArticulId { get; set; }

        public string ArticulName { get; set; }

        public string ExecutionTime { get; set; }

        public string Count { get; set; }

        public int OperationId { get; set; }

        public string OperationName { get; set; }

        public string WorkDay { get; set; }
    }

    public sealed class OperationLogEntry
    {
        public int EmployeeId { get; set; }

        public int OperationId { get; set; }

        public int ArticulId { get; set; }

        public DateTime DateExecution { get; set; }

        public int ExecutionTime { get; set; }

        public int Count { get; set; }

        public string DocumentNumber { get; set; }

        public int WorkDay { get; set; }
    }
}
