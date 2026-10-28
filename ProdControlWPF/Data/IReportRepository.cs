using System;
using System.Collections.Generic;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public interface IReportRepository
    {
        IReadOnlyList<ReportEmployeeListItem> GetEmployees();

        IReadOnlyList<EmployeeReportRow> GetEmployeeReport(
            IReadOnlyList<int> employeeIds,
            DateTime dateFrom,
            DateTime dateTo);
    }
}
