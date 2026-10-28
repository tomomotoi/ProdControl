namespace ProdControlWPF.Models
{
    public sealed class KnittingRoutingEntry
    {
        public int PartId { get; set; }

        public string PartName { get; set; } = string.Empty;

        public int MachineId { get; set; }

        public string MachineName { get; set; } = string.Empty;

        public int ProductPartsAmount { get; set; }

        public int MachinePartsAmount { get; set; }

        public double K1 { get; set; }

        public double K2 { get; set; }

        public double AvgRate { get; set; }

        public double CoefficientRate { get; set; }

        public double KitRate { get; set; }
    }

    public sealed class KnittingRoutingRow
    {
        public int PartId { get; set; }

        public string PartName { get; set; } = string.Empty;

        public int MachineId { get; set; }

        public string MachineName { get; set; } = string.Empty;

        public string ModelPartAmount { get; set; } = string.Empty;

        public string MachineCarriageAmount { get; set; } = string.Empty;

        public string Coefficient1 { get; set; } = string.Empty;

        public string Coefficient2 { get; set; } = string.Empty;

        public string AvgValue { get; set; } = string.Empty;

        public string WithCoefficient { get; set; } = string.Empty;

        public string WithKit { get; set; } = string.Empty;
    }
}
