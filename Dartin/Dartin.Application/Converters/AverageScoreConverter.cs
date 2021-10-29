using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using Dartin.Models;

namespace Dartin.Converters
{
    public class AverageScoreConverter : MarkupExtension, IValueConverter
    {
        private static AverageScoreConverter _instance;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ??= new AverageScoreConverter();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int score = 0;
            switch (value)
            {
                case BindingList<Leg> legs:
                    score = legs.Sum(leg => leg.Turns.Sum(turn => turn.Score));
                    int legCount = legs.Sum(leg => leg.Turns.Count);
                    if (legCount == 0)
                        return 0;
                    return score / legCount;

                case BindingList<Turn> turns:
                    score = turns.Sum(turn => turn.Score);
                    int turnCount = turns.Count;
                    if (turnCount == 0)
                        return 0;
                    return score / turnCount;

                case BindingList<Toss> tosses:
                    score = tosses.Sum(toss => toss.TotalScore);
                    int tossCount = tosses.Count;
                    if (tossCount == 0)
                        return 0;
                    return score / tossCount;
                default:
                    return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
