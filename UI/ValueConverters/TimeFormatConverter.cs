using System;
using System.Globalization;
using System.Windows.Data;

namespace UI
{
    public class TimeFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeString = value.ToString();
            var decSeparator = culture.NumberFormat.NumberDecimalSeparator;
            var timeDecimal = System.Convert.ToDecimal(timeString.Replace(".", decSeparator));
            var ts = TimeSpan.FromHours((double)timeDecimal);
            var hours = ts.Hours.ToString();
            var minutes = ts.Minutes.ToString();
            if (hours == "0" & minutes == "0")
                return "0";
            if (minutes.Length < 2)
                minutes = "0" + minutes;
            return $"{hours}:{minutes}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }

    }
}