using System;
using System.Globalization;

namespace ProdControlWPF.Domain
{
    public static class KnittingRoutingRules
    {
        public static decimal ParseDecimal(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0;
            }

            var normalized = text.Trim().Replace(" ", string.Empty).Replace("\u00A0", string.Empty);

            if (decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.CurrentCulture, out var result))
            {
                return result;
            }

            var invariant = normalized.Replace(",", ".");
            if (decimal.TryParse(invariant, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                return result;
            }

            var local = normalized.Replace(".", ",");
            if (decimal.TryParse(local, NumberStyles.Number, CultureInfo.CurrentCulture, out result))
            {
                return result;
            }

            return 0;
        }

        public static decimal CalculateWithCoefficient(decimal average, decimal coefficient1, decimal coefficient2)
        {
            return average * NormalizeToOne(coefficient1) * NormalizeToOne(coefficient2);
        }

        public static decimal CalculateWithKit(decimal withCoefficient, decimal modelPartAmount, decimal machineCarriageAmount)
        {
            return (withCoefficient * NormalizeToOne(modelPartAmount)) / NormalizeToOne(machineCarriageAmount);
        }

        public static string FormatWithCoefficient(decimal value)
        {
            return value.ToString("F2", CultureInfo.CurrentCulture);
        }

        public static string FormatWithKit(decimal value)
        {
            return value.ToString("F2", CultureInfo.CurrentCulture);
        }

        public static string FormatLoadedValue(double value)
        {
            return Convert.ToDecimal(value).ToString("F3", CultureInfo.CurrentCulture);
        }

        public static string FormatTotal(decimal value)
        {
            return value.ToString("F2", CultureInfo.CurrentCulture);
        }

        public static string CalculateNorm(decimal total)
        {
            return total > 0
                ? (480m / total).ToString("F1", CultureInfo.CurrentCulture)
                : "0";
        }

        public static int ToInteger(decimal value)
        {
            return Convert.ToInt32(value);
        }

        private static decimal NormalizeToOne(decimal value)
        {
            return value == 0 ? 1 : value;
        }
    }
}
