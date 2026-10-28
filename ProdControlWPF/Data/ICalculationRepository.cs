using System;
using System.Collections.Generic;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public interface ICalculationRepository
    {
        IReadOnlyList<CalculationRow> GetEmployees();

        int GetPositionId(int employeeId);

        IReadOnlyList<KnittingZoneTime> GetKnittingZoneTimes(
            int employeeId,
            bool usePeriod,
            DateTime dateFrom,
            DateTime dateTo);

        IReadOnlyList<SewingSalaryEntry> GetSewingSalaryEntries(
            int employeeId,
            bool usePeriod,
            DateTime dateFrom,
            DateTime dateTo);

        IReadOnlyList<SewingTimeEntry> GetSewingTimeEntries(
            int employeeId,
            bool usePeriod,
            DateTime dateFrom,
            DateTime dateTo);

        double? GetRoutingTime(int articulId, int operationId);

        double GetTariffPerMinute(int employeeId);

        IReadOnlyList<WorkDayEntry> GetPeriodWorkDays(
            int employeeId,
            DateTime dateFrom,
            DateTime dateTo);

        int? GetWorkDayForDate(int employeeId, DateTime date);
    }
}
