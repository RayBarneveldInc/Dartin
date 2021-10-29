using Dartin.Properties;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dartin.Converters
{
    public class WonOrLostConverter : MarkupExtension, IMultiValueConverter
    {
        private static WonOrLostConverter _instance; 
        public override object ProvideValue(IServiceProvider serviceProvider) 
        { 
            return _instance ??= new WonOrLostConverter();
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Guid winnerID = (Guid)values[0];
            Guid playerID;
            string text = $"{values[2]}";
            text += $" - Avg {values[3]}";
            if ((int)values[1] == 1) 
                playerID = (Guid)App.Current.Properties["playeroneID"];
            else 
                playerID = (Guid)App.Current.Properties["playertwoID"];

            //if (values.Length >= 5 && playerID == (Guid)values[4]) 
            //    text += " - Started";

            if (playerID == winnerID) 
                text += Resources.WonFormat;

            return text;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
