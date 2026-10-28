using System.Collections.Generic;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public interface INewBillRepository
    {
        IReadOnlyList<EmployeeLookup> GetEmployees();

        IReadOnlyList<ArticulLookup> GetArticuls();

        IReadOnlyList<IdleOperationLookup> GetIdleOperations();

        IReadOnlyList<OperationLookup> GetOperationsByArticul(int articulId);

        double GetIdlePercentage(int idleOperationId);

        void SaveOperationLog(IReadOnlyList<OperationLogEntry> entries);
    }
}
