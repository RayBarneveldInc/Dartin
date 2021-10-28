using Dartin.Extensions;
using Dartin.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dartin.Converters
{
    public class GuidToPlayerConverter : MarkupExtension, IValueConverter
    {
        private static GuidToPlayerConverter _instance;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_instance == null)
                _instance = new GuidToPlayerConverter();

            return _instance;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Guid)value).ToPlayer();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Player)value).Id;
        }
    }
}
