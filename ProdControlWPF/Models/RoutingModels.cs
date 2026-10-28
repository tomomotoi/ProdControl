using System.Globalization;
using ProdControlWPF.Infrastructure;

namespace ProdControlWPF.Models
{
    public sealed class CoefficientLookup
    {
        public CoefficientLookup(int coefficientId, double value)
        {
            CoefficientId = coefficientId;
            Value = value;
        }

        public int CoefficientId { get; }

        public double Value { get; }

        public string Display
        {
            get { return Value.ToString(CultureInfo.CurrentCulture); }
        }
    }

    public sealed class RoutingEntry
    {
        public int RoutingId { get; set; }

        public int ArticulId { get; set; }

        public int OperationId { get; set; }

        public double DopCost { get; set; }

        public double TimeExecute { get; set; }

        public double RateExecute { get; set; }

        public int PositionLevel { get; set; }

        public double Tariff { get; set; }
    }

    public sealed class RoutingRow : ObservableObject
    {
        private string _dopCost = string.Empty;
        private string _executionTime = string.Empty;
        private string _rateExecute = string.Empty;
        private string _level = string.Empty;
        private string _cost = string.Empty;
        private bool _isDirty;

        public int RoutingId { get; set; }

        public int ArticulId { get; set; }

        public int OperationId { get; set; }

        public string OperationName { get; set; } = string.Empty;

        public string DopCost
        {
            get { return _dopCost; }
            set { SetProperty(ref _dopCost, value ?? string.Empty); }
        }

        public string ExecutionTime
        {
            get { return _executionTime; }
            set { SetProperty(ref _executionTime, value ?? string.Empty); }
        }

        public string RateExecute
        {
            get { return _rateExecute; }
            set { SetProperty(ref _rateExecute, value ?? string.Empty); }
        }

        public string Level
        {
            get { return _level; }
            set { SetProperty(ref _level, value ?? string.Empty); }
        }

        public string Cost
        {
            get { return _cost; }
            set { SetProperty(ref _cost, value ?? string.Empty); }
        }

        public bool IsNew { get; set; }

        public bool IsDirty
        {
            get { return _isDirty; }
            set { SetProperty(ref _isDirty, value); }
        }
    }
}
