using System.Globalization;
using System.Text.RegularExpressions;

namespace UI.Helper
{
    public class SpentTimeValidation
    {
        public static bool CheckTimeFormatPattern(string spentTime)
        {
            var timeFormatPattern = @"^(?:0?[0-9]|1[0-9]|2[0-9]):[0-5][0-9]$";
            Regex timeFormatRegex = new Regex(timeFormatPattern);
            bool timeFormatRegexValid = timeFormatRegex.IsMatch(spentTime);
            return timeFormatRegexValid;
        }

        public static bool CheckDoubleFormatPattern(string spentTime)
        {
            var cultureInfo = CultureInfo.CurrentCulture;
            double result = 0.0;
            var doubleFormatPattern = $@"^(\d+\{cultureInfo.NumberFormat.NumberDecimalSeparator}?\d{{0,2}}$)[\d.]{{0,2}}$";
            Regex doubleFormatRegex = new Regex(doubleFormatPattern);
            bool doubleFormatRegexValid = doubleFormatRegex.IsMatch(spentTime);
            bool canConvert = double.TryParse(spentTime, out result);
            return (canConvert && doubleFormatRegexValid);
        }

        public static bool CheckGroupSeparator(string spentTime)
        {
            var cultureInfo = CultureInfo.CurrentCulture;
            string groupSeparator = cultureInfo.NumberFormat.NumberGroupSeparator;
            bool foundGroupSeparator = spentTime.Contains(groupSeparator);
            return foundGroupSeparator;
        }
    }
}