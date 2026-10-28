using System.Collections.Generic;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public interface IKnittingSearchRepository
    {
        IReadOnlyList<EmployeeLookup> GetEmployees();

        IReadOnlyList<ArticulLookup> GetArticuls();

        IReadOnlyList<ClothingPartLookup> GetParts();

        IReadOnlyList<KnittingMachineLookup> GetMachines();

        IReadOnlyList<KnittingSearchRow> Search(KnittingSearchCriteria criteria);

        IReadOnlyList<KnittingSearchRow> GetAll();

        void Delete(int logId);

        void SaveUpdates(IReadOnlyList<KnittingSearchUpdate> updates);
    }
}
