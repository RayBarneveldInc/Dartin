using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Dartin.Converters
{
    public class PlusOneConverter : MarkupExtension, IValueConverter
    {
        private static PlusOneConverter _instance;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ??= new PlusOneConverter();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (dynamic)value + 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
