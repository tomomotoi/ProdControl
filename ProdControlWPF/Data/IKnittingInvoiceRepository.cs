using System.Collections.Generic;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public interface IKnittingInvoiceRepository
    {
        IReadOnlyList<EmployeeLookup> GetEmployees();

        IReadOnlyList<ArticulLookup> GetArticuls();

        IReadOnlyList<ClothingPartLookup> GetParts();

        IReadOnlyList<KnittingMachineLookup> GetMachines();

        void SaveOperationLog(IReadOnlyList<KnittingOperationLogEntry> entries);
    }
}
