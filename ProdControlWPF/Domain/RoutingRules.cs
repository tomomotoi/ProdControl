using System;
using System.Globalization;

namespace ProdControlWPF.Domain
{
    public static class RoutingRules
    {
        public static double CalculateExecutionTime(double coefficient, double rate)
        {
            return Math.Round(coefficient * rate, 2);
        }

        public static double CalculateTariff(double executionTime, double ratePerMinute)
        {
            return executionTime * ratePerMinute;
        }

        public static bool TryParseNumber(string text, out double value)
        {
            value = 0;

            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            var separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var normalized = text.Trim();

            if (separator == ",")
            {
                normalized = normalized.Replace('.', ',');
            }
            else if (separator == ".")
            {
                normalized = normalized.Replace(',', '.');
            }

            return double.TryParse(
                normalized,
                NumberStyles.Float | NumberStyles.AllowThousands,
                CultureInfo.CurrentCulture,
                out value);
        }

        public static bool TryParseLevel(string text, out int level)
        {
            return int.TryParse(text, NumberStyles.Integer, CultureInfo.CurrentCulture, out level);
        }

        public static string FormatNumber(double value)
        {
            return value.ToString(CultureInfo.CurrentCulture);
        }
    }
}
