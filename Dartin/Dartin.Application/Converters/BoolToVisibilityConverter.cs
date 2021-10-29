using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dartin.Converters
{
    public class BoolToVisibilityConverter : MarkupExtension, IValueConverter
    {
        private static BoolToVisibilityConverter _instance;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ??= new BoolToVisibilityConverter();
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : (object)Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
