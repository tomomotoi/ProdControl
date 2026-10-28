using System.Collections.Generic;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public interface ITariffsCheckRepository
    {
        IReadOnlyList<ArticulLookup> GetArticuls();

        IReadOnlyList<TariffCandidate> GetMissingTariffs();

        void SaveTariffs(IList<TariffUpdate> updates);
    }
}
