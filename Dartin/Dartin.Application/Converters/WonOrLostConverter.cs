using Dartin.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;


namespace Dartin.Converters
{
    public class WonOrLostConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Player player = (Player)values[0];
            Guid winnerID = player.Id;
            Guid playerID;
            string text = $"{values[2].ToString()}";

            if ((int)values[1] == 1) playerID = (Guid)App.Current.Properties["playeroneID"]; else playerID = (Guid)App.Current.Properties["playertwoID"];
            if (values.Length == 4) {
                if (playerID == (Guid)values[3]) text += " - Started";
            }
            if (playerID == winnerID) text += " - Won";

            return text;
            //if (playerID == winnerID) return values[2].ToString() + " - Won"; else return values[2].ToString() + "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
