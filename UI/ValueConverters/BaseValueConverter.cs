using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace UI
{
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
    where T : class, new()
    {
        #region Private Members

        private static T nConverter = null;

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return nConverter ?? (nConverter = new T());
        }

        
        
        #region Value Converter Methods

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        #endregion
    }
}
