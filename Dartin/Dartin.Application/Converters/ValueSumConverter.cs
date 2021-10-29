using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dartin.Converters
{
    public class ValueSumConverter : MarkupExtension, IMultiValueConverter
    {
        private static ValueSumConverter _instance;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {

            return _instance ??= new ValueSumConverter();
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return (long)values[0] + (long)values[1];
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
