using System.Collections.Generic;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public interface IKnittingRoutingRepository
    {
        IReadOnlyList<ArticulLookup> GetArticuls();

        IReadOnlyList<ClothingPartLookup> GetParts();

        IReadOnlyList<KnittingMachineLookup> GetMachines();

        IReadOnlyList<CoefficientLookup> GetCoefficients();

        IReadOnlyList<KnittingRoutingEntry> GetRouting(int articulId);

        void Save(int articulId, IReadOnlyList<KnittingRoutingEntry> entries);
    }
}
