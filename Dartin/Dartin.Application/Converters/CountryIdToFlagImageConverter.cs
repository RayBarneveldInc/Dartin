using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace Dartin.Converters
{
    class CountryIdToFlagImageConverter : MarkupExtension, IValueConverter
    {
        private static CountryIdToFlagImageConverter _instance;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {

            return _instance ??= new CountryIdToFlagImageConverter();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var countryId = value as string;

            if (countryId == null)
                return null;

            try
            {
                var path = $"/FamFamFam.Flags.Wpf;component/Images/{countryId.ToLower()}.png";
                var uri = new Uri(path, UriKind.Relative);
                var resourceStream = Application.GetResourceStream(uri);
                if (resourceStream == null)
                    return null;

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = resourceStream.Stream;
                bitmap.EndInit();
                return bitmap;
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
