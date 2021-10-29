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
            switch (value)
            {
                case BindingList<Leg> legs:
                    return legs.Sum(leg => leg.Turns.Sum(turn => turn.Score)) / legs.Sum(leg => leg.Turns.Sum(turn => turn.Tosses.Count()));
                    //return SetScore(legs) / legs.Sum(leg => leg.Turns.Sum(turn => turn.Tosses.Count()));

                case BindingList<Turn> turns:
                    return turns.Sum(turn => turn.Score) / turns.Sum(turn => turn.Tosses.Count());
                    //return LegScore(turns) / turns.Sum(turn => turn.Tosses.Count());

                case BindingList<Toss> tosses:
                    return tosses.Sum(toss => toss.Score) / tosses.Count;
                    //return TurnScore(tosses) / tosses.Count;

                default:
                    return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private int SetScore(BindingList<Leg> legs)
        {
            int setTotal = 0;
            foreach(Leg leg in legs) setTotal += LegScore(leg.Turns);
            return setTotal;
        }
        private int LegScore(BindingList<Turn> turns)
        {
            int legTotal = 0;
            foreach(Turn turn in turns) legTotal += turn.Score;
            return legTotal;
        }

        private int TurnScore(BindingList<Toss> tosses)
        {
            int turnTotal = 0;
            foreach (Toss toss in tosses) turnTotal += toss.Score;
            return turnTotal;
        }

    }
}
