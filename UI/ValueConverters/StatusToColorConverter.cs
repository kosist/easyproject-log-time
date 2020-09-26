using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using UI.DataModel;

namespace UI
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StatusEnum && value != null)
            {
                StatusEnum status = (StatusEnum)value;
                var color = new SolidColorBrush(Colors.White);

                switch (status)
                {
                    case StatusEnum.Ok:
                        color = new SolidColorBrush(Colors.Green);
                        break;
                    case StatusEnum.NOk:
                        color = new SolidColorBrush(Colors.Red);
                        break;
                    default:
                        color = new SolidColorBrush(Colors.White);
                        break;
                }

                return color;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}