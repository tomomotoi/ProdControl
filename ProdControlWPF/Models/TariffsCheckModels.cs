namespace ProdControlWPF.Models
{
    public sealed class TariffCandidate
    {
        public int RoutingId { get; set; }

        public int ArticulId { get; set; }

        public int OperationId { get; set; }

        public double TimeExecute { get; set; }

        public double PositionRateTime { get; set; }
    }

    public sealed class TariffUpdate
    {
        public TariffUpdate(int articulId, int operationId, double tariff)
        {
            ArticulId = articulId;
            OperationId = operationId;
            Tariff = tariff;
        }

        public int ArticulId { get; }

        public int OperationId { get; }

        public double Tariff { get; }
    }
}
