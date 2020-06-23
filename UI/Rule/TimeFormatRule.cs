using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace UI.Rule
{
    public class TimeFormatRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double result = 0.0;
            string text = (string) value;

            var timeFormatPattern = @"^(?:0?[0-9]|1[0-9]|2[0-9]):[0-9][0-9]$";
            Regex timeFormatRegex = new Regex(timeFormatPattern);
            bool timeFormatRegexValid = timeFormatRegex.IsMatch(text);

            var doubleFormatPattern = $@"^(\d+\{cultureInfo.NumberFormat.NumberDecimalSeparator}?\d{{0,2}}$)[\d.]{{0,2}}$";
            Regex doubleFormatRegex = new Regex(doubleFormatPattern);
            bool doubleFormatRegexValid = doubleFormatRegex.IsMatch(text);

            bool validFlag = false;
            string groupSeparator = cultureInfo.NumberFormat.NumberGroupSeparator;
            bool canConvert = double.TryParse(text, out result);
            bool foundGroupSeparator = text.Contains(groupSeparator);
            validFlag =!foundGroupSeparator && ((canConvert && doubleFormatRegexValid) || timeFormatRegexValid);
            return new ValidationResult(validFlag, "Not a valid time format");
        }
    }
}