using System.Globalization;
using System.Windows.Controls;

namespace UI.Rule
{
    public class TimeFormatRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double result = 0.0;
            string text = (string) value;
            bool validFlag = false;
            string groupSeparator = cultureInfo.NumberFormat.NumberGroupSeparator;
            bool canConvert = double.TryParse(text, out result);
            bool foundGroupSeparator = text.Contains(groupSeparator);
            validFlag = canConvert && !foundGroupSeparator;
            return new ValidationResult(validFlag, "Not a valid time format");
        }
    }
}