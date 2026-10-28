using System;
using System.Collections.Generic;
using System.Globalization;
using ProdControlWPF.Models;

namespace ProdControlWPF.Domain
{
    public static class WorkOrderRules
    {
        public static bool IsCompleted(int planQuantity, int factQuantity)
        {
            return planQuantity > 0
                ? factQuantity >= planQuantity
                : factQuantity > 0;
        }

        public static decimal CalculateProgress(int planQuantity, int factQuantity)
        {
            if (planQuantity <= 0)
            {
                return factQuantity > 0 ? 100m : 0m;
            }

            var progress = factQuantity * 100m / planQuantity;

            return progress > 100m ? 100m : progress;
        }

        public static int CalculateRemaining(int planQuantity, int factQuantity)
        {
            var remaining = planQuantity - factQuantity;
            return remaining > 0 ? remaining : 0;
        }

        public static string GetStatusText(int planQuantity, int factQuantity)
        {
            if (IsCompleted(planQuantity, factQuantity))
            {
                return "Выполнен";
            }

            return factQuantity > 0 ? "В работе" : "Не начат";
        }

        public static WorkOrderOrderSummary BuildSummary(
            string orderNumber,
            IEnumerable<WorkOrderRow> rows,
            int? orderedQuantity)
        {
            var issued = 0;
            var fact = 0;

            if (rows != null)
            {
                foreach (var row in rows)
                {
                    issued += row.PlanQuantity;
                    fact += row.FactQuantity;
                }
            }

            var ordered = orderedQuantity ?? issued;
            var progress = ordered > 0
                ? Math.Min(100m, fact * 100m / ordered)
                : 0m;

            return new WorkOrderOrderSummary
            {
                OrderNumber = orderNumber ?? string.Empty,
                Ordered = ordered,
                Issued = issued,
                Fact = fact,
                ProgressPercent = progress
            };
        }

        public static string FormatNumber(decimal? value)
        {
            return value.HasValue
                ? value.Value.ToString("0.##", CultureInfo.CurrentCulture)
                : string.Empty;
        }

        public static string FormatNumber(decimal value)
        {
            return value.ToString("0.##", CultureInfo.CurrentCulture);
        }

        public static string FormatPercent(decimal value)
        {
            return value.ToString("0.#", CultureInfo.CurrentCulture) + "%";
        }

        public static string FormatDate(DateTime? value)
        {
            return value.HasValue
                ? value.Value.ToString("dd.MM.yyyy", CultureInfo.CurrentCulture)
                : string.Empty;
        }

        public static bool TryParseNonNegativeInt(string text, out int value)
        {
            value = 0;
            var normalized = NormalizeNumber(text);

            if (normalized.Length == 0)
            {
                return true;
            }

            if (int.TryParse(normalized, NumberStyles.Integer, CultureInfo.CurrentCulture, out value)
                || int.TryParse(normalized, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
            {
                return value >= 0;
            }

            return false;
        }

        public static bool TryParseNullableDecimal(string text, out decimal? value)
        {
            value = null;
            var normalized = NormalizeNumber(text);

            if (normalized.Length == 0)
            {
                return true;
            }

            if (decimal.TryParse(
                    normalized,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out var parsed)
                || decimal.TryParse(
                    normalized.Replace(',', '.'),
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out parsed))
            {
                value = parsed;
                return true;
            }

            return false;
        }

        public static bool TryParsePercent(string text, out decimal value)
        {
            value = 0m;

            if (!TryParseNullableDecimal(text, out var parsed) || !parsed.HasValue)
            {
                return false;
            }

            if (parsed.Value < 0m || parsed.Value > 100m)
            {
                return false;
            }

            value = parsed.Value;
            return true;
        }

        private static string NormalizeNumber(string text)
        {
            return (text ?? string.Empty)
                .Replace("\u00A0", string.Empty)
                .Replace(" ", string.Empty)
                .Trim();
        }
    }
}
