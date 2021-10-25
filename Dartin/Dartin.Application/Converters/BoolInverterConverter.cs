using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dartin.Converters
{
    public class BoolInverterConverter : MarkupExtension, IValueConverter
    {
        private static BoolInverterConverter _instance;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ??= new BoolInverterConverter();
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
