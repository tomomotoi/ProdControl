using System.Collections.Generic;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public interface IRoutingRepository
    {
        IReadOnlyList<ArticulLookup> GetArticuls();

        IReadOnlyList<OperationItem> GetOperations();

        IReadOnlyList<CoefficientLookup> GetCoefficients();

        IReadOnlyList<RoutingEntry> GetRouting(int articulId);

        double? GetPositionRateTime(int operationId, int positionLevel);

        void Save(IList<RoutingEntry> inserts, IList<RoutingEntry> updates);
    }
}
