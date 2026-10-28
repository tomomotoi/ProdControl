using System;
using System.Globalization;

namespace ProdControlWPF.Domain
{
    public static class BillRules
    {
        public const int MaxWorkday = 480;

        public static double CalculateExecutionTime(double operationTime, double idlePercentage)
        {
            return operationTime * idlePercentage;
        }

        public static bool TryParseDouble(string text, out double value)
        {
            return double.TryParse(
                text,
                NumberStyles.Float | NumberStyles.AllowThousands,
                CultureInfo.CurrentCulture,
                out value);
        }

        public static bool TryParseLegacyInt32(string text, out int value)
        {
            value = 0;

            if (!TryParseDouble(text, out var number))
            {
                return false;
            }

            if (number < int.MinValue || number > int.MaxValue)
            {
                return false;
            }

            value = (int)Math.Round(number, MidpointRounding.ToEven);
            return true;
        }

        public static string FormatDouble(double value)
        {
            return value.ToString(CultureInfo.CurrentCulture);
        }
    }
}
