using System.Collections.Generic;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public interface IEmployeesRepository
    {
        IReadOnlyList<EmployeePositionLookup> GetPositions();

        IReadOnlyList<EmployeeRow> GetEmployees();

        IReadOnlyList<EmployeeRow> SearchByName(string employeeName);

        IReadOnlyList<EmployeeRow> SearchByNumber(string employeeNumber);

        IReadOnlyList<EmployeeRow> SearchByPosition(int positionId);

        IReadOnlyList<EmployeeRow> SearchByLevel(int positionLevel);

        void Save(
            IList<int> employeeIdsForRemoval,
            IList<EmployeeInsert> inserts,
            IList<EmployeeUpdate> updates);
    }
}
