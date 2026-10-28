using System;

namespace ProdControlWPF.Models
{
    public sealed class ClothingPartLookup
    {
        public ClothingPartLookup(int partId, string partName)
        {
            PartId = partId;
            PartName = partName ?? string.Empty;
        }

        public int PartId { get; }

        public string PartName { get; }
    }

    public sealed class KnittingMachineLookup
    {
        public KnittingMachineLookup(int machineId, string displayName)
        {
            MachineId = machineId;
            DisplayName = displayName ?? string.Empty;
        }

        public int MachineId { get; }

        public string DisplayName { get; }
    }

    public sealed class KnittingInvoiceRow
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime DateExecution { get; set; }

        public int ArticulId { get; set; }

        public string ArticulName { get; set; }

        public int PartId { get; set; }

        public string PartName { get; set; }

        public int MachineId { get; set; }

        public string MachineName { get; set; }

        public string Amount { get; set; }

        public string ExecutionTime { get; set; }

        public string MachinesInZone { get; set; }

        public string WorkDay { get; set; }
    }

    public sealed class KnittingOperationLogEntry
    {
        public string DocumentNumber { get; set; }

        public int ArticulId { get; set; }

        public int EmployeeId { get; set; }

        public int PartId { get; set; }

        public int MachineId { get; set; }

        public int Amount { get; set; }

        public DateTime DateExecution { get; set; }

        public double ExecutionTime { get; set; }

        public int MachinesInZone { get; set; }

        public int WorkDay { get; set; }
    }
}
