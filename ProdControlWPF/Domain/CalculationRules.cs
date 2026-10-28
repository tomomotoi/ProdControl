using System;
using System.Collections.Generic;
using System.Globalization;
using ProdControlWPF.Models;

namespace ProdControlWPF.Domain
{
    public static class CalculationRules
    {
        public const int NonRatedArticulId = 74;

        public const int KnittingPositionId = 9;

        public const int DefaultWorkDay = 480;

        private const int TimePrecision = 6;

        private const int MoneyPrecision = 2;

        public static double CalculateKnittingSalary(IEnumerable<KnittingZoneTime> zones)
        {
            var total = 0d;

            foreach (var zone in zones)
            {
                total += zone.TotalTime / NormalizeMachines(zone.MachinesInZone);
            }

            return total;
        }

        public static double CalculateSewingSalary(
            IEnumerable<SewingSalaryEntry> entries,
            double tariffPerMinute)
        {
            var salary = 0d;

            foreach (var entry in entries)
            {
                if (entry.ArticulId == NonRatedArticulId)
                {
                    salary += tariffPerMinute * entry.TimeExecution;
                }
                else
                {
                    salary += entry.Count * entry.Tariff;
                }
            }

            return salary;
        }

        public static double CalculateSewingTime(
            IEnumerable<SewingTimeEntry> entries,
            Func<int, int, double?> getRoutingTime)
        {
            var total = 0d;

            foreach (var entry in entries)
            {
                if (entry.TimeExecution == 0)
                {
                    var routingTime = getRoutingTime(entry.ArticulId, entry.OperationId);

                    if (routingTime.HasValue)
                    {
                        total += Math.Round(routingTime.Value * entry.Count, TimePrecision);
                    }
                }
                else
                {
                    total += entry.TimeExecution;
                }
            }

            return total;
        }

        public static int CalculatePeriodWorkDay(IEnumerable<WorkDayEntry> entries)
        {
            var datesByWorkDay = new Dictionary<int, HashSet<DateTime>>();

            foreach (var entry in entries)
            {
                HashSet<DateTime> dates;

                if (!datesByWorkDay.TryGetValue(entry.WorkDay, out dates))
                {
                    dates = new HashSet<DateTime>();
                    datesByWorkDay.Add(entry.WorkDay, dates);
                }

                dates.Add(entry.DateExecution.Date);
            }

            var total = 0;

            foreach (var pair in datesByWorkDay)
            {
                total += pair.Key * pair.Value.Count;
            }

            return total;
        }

        public static double CalculateNorm(double totalTime, int workDay)
        {
            return workDay > 0 ? totalTime / workDay : 0;
        }

        public static string FormatSewingSalary(double salary)
        {
            return Math.Round(salary, MoneyPrecision).ToString(CultureInfo.CurrentCulture);
        }

        public static string FormatRoundedFixed2(double value)
        {
            return Math.Round(value, MoneyPrecision).ToString("0.00", CultureInfo.CurrentCulture);
        }

        public static string FormatFixed2(double value)
        {
            return value.ToString("0.00", CultureInfo.CurrentCulture);
        }

        private static int NormalizeMachines(int machinesInZone)
        {
            return machinesInZone <= 0 ? 1 : machinesInZone;
        }
    }
}
