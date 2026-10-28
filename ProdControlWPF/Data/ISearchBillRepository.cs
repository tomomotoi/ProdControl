using System.Collections.Generic;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public interface ISearchBillRepository
    {
        IReadOnlyList<EmployeeLookup> GetEmployees();

        IReadOnlyList<ArticulLookup> GetArticuls();

        IReadOnlyList<OperationLookup> GetOperations();

        IReadOnlyList<SearchBillRow> Search(SearchBillCriteria criteria);

        IReadOnlyList<SearchBillRow> GetAll();

        void SaveChanges(IReadOnlyList<int> deleteLogIds, IReadOnlyList<SearchBillUpdate> updates);
    }
}
