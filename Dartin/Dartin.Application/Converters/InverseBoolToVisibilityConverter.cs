using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dartin.Converters
{
    public class InverseBoolToVisibilityConverter : MarkupExtension, IValueConverter
    {
        private static InverseBoolToVisibilityConverter _instance;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_instance == null)
                _instance = new InverseBoolToVisibilityConverter();

            return _instance;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value ? Visibility.Visible : (object)Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}