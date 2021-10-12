using Caliburn.Micro;
using Dartin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;

namespace Dartin.ViewModels
{
    class MatchReportViewModel : Screen, IViewModel
    {
        private MatchDefinition _currentMatch;

        public string ViewName { get; }
        public string PlayerOne => PlayerString(0);
        public string PlayerTwo => PlayerString(1);
        public MatchDefinition MatchInfo1 => SetsPerPlayer(0);
        public MatchDefinition MatchInfo2 => SetsPerPlayer(1);
        public BindingList<MatchDefinition> ExistingMatches => State.Instance.Matches;
        public MatchDefinition Match => _currentMatch;

        public int TotalSetsWonP1 => TotalSetsWon(MatchInfo1);
        public int TotalSetsWonP2 => TotalSetsWon(MatchInfo2);
        public int TotalLegsWonP1 => TotalLegsWon(MatchInfo1);
        public int TotalLegsWonP2 => TotalLegsWon(MatchInfo2);
        public int MatchAverageP1 => MatchAverage(MatchInfo1);
        public int MatchAverageP2 => MatchAverage(MatchInfo2);
        public int TotalDartsThrownP1 => TotalDartsThrown(MatchInfo1);
        public int TotalDartsThrownP2 => TotalDartsThrown(MatchInfo2);
        public int TotalThreeDartersP1 => TotalThreeDarters(MatchInfo1);
        public int TotalThreeDartersP2 => TotalThreeDarters(MatchInfo2);
        public int TotalScoreAboveHundredP1 => TotalScoreByAmountPlusTwenty(MatchInfo1, 100);
        public int TotalScoreAboveHundredP2 => TotalScoreByAmountPlusTwenty(MatchInfo2, 100);


        public int MatchCount => State.Instance.Matches.Count;

        public int TotalDartsThrown(MatchDefinition match)
        {
            int Total = 0;

            match.Sets.ForEach(s => s.Legs.ForEach(l => l.Turns.ForEach(t => Total += t.Tosses.Count)));

            return Total;
        }

        public int TotalLegsWon(MatchDefinition match)
        {
            Player Player = match.Players[0];
            int Total = 0;

            match.Sets.ForEach(s =>
            {
                Total += s.Legs.FindAll(l =>
                {
                    if (l.Winner != null && l.Winner.Name == Player.Name)
                    {
                        return true;
                    }
                    return false;
                }).Count;

            });

            return Total;
        }

        public int TotalSetsWon(MatchDefinition match)
        {
            Player Player = match.Players[0];

            return match.Sets.FindAll(s =>
            {
                if (s.Winner != null && s.Winner.Name == Player.Name)
                {
                    return true;
                }
                return false;
            }).Count;
        }

        public int TotalThreeDarters(MatchDefinition match)
        {
            int Total = 0;
            int TempScore = 0;

            match.Sets.ForEach(s => s.Legs.ForEach(l => l.Turns.ForEach(t =>
            {
                t.Tosses.ForEach(t => TempScore += (t.Multiplier * t.Score));

                if (TempScore == 180)
                {
                    TempScore = 0;
                    Total++;
                }
            })));

            return Total;
        }

        public int TotalScoreByAmountPlusTwenty(MatchDefinition match, int number)
        {
            int Total = 0;
            int TempScore = 0;

            match.Sets.ForEach(s => s.Legs.ForEach(l => l.Turns.ForEach(t =>
            {
                t.Tosses.ForEach(t => TempScore += (t.Multiplier * t.Score));

                if (TempScore >= number && TempScore <= (TempScore + 20))
                {
                    TempScore = 0;
                    Total++;
                }
            })));

            return Total;
        }

        public int MatchAverage(MatchDefinition match)
        {
            int TotalThrows = 0;
            int TotalScore = 0;

            match.Sets.ForEach(s => s.Legs.ForEach(l => l.Turns.ForEach(t => t.Tosses.ForEach(to =>
            {
                TotalThrows++;
                TotalScore += (to.Multiplier * to.Score);
            }))));

            return TotalScore / TotalThrows;
        }

        public void ListViewSelection(object sender, MouseButtonEventArgs e)
        {
            var item = sender as MatchDefinition;
            if (item != null)
            {
                changeMatch(item);
            }
        }

        public void changeMatch(MatchDefinition selectedMatch)
        {
            _currentMatch = selectedMatch;
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public MatchDefinition SetsPerPlayer(int i)
        {
            var copyser = JsonConvert.SerializeObject(Match, Formatting.Indented);
            MatchDefinition deepcopy = JsonConvert.DeserializeObject<MatchDefinition>(copyser);

            Player player = deepcopy.Players[i];

            foreach (Set s in deepcopy.Sets)
            {
                foreach (Leg l in s.Legs)
                {
                    // TODO -> aanpassen naar ID
                    l.Turns = l.Turns.FindAll(t => t.Player.Name == player.Name);
                }
            }
            return deepcopy;
        }

        public MatchReportViewModel()
        {
            _currentMatch = State.Instance.Matches[2];
            //_currentMatch = new MatchDefinition();
            //Player p1 = new Player();
            //Player p2 = new Player();
            //p1.Name = "Jacco";
            //p2.Name = "Tjeerd";
            //_currentMatch.Players.Add(p1);
            //_currentMatch.Players.Add(p2);
            //_currentMatch.Players[1] = p2;

            //Toss t1 = new Toss();
            //t1.Score = 20;
            //t1.Multiplier = 3;

            //Toss t2 = new Toss();
            //t2.Score = 20;
            //t2.Multiplier = 2;

            //Toss t3 = new Toss();
            //t3.Score = 20;
            //t3.Multiplier = 1;

            //Turn tu = new Turn();
            //tu.Player = p1;
            //tu.TurnScore = 120;
            //tu.Tosses.Add(t1);
            //tu.Tosses.Add(t2);
            //tu.Tosses.Add(t3);

            //Leg l = new Leg();
            //l.Turns.Add(tu);
            ////l.Winner.Name = p2.Name;

            //Set s = new Set();
            //s.Legs = new List<Leg>();
            //s.LegsToWinSet = 1;

            //s.Legs.Add(l);
            //s.Winner = p2;

            //_currentMatch.Name = "Lmfao";
            //_currentMatch.Sets.Add(s);


            //State.Instance.Matches.Add(_currentMatch);
        }



        public string PlayerString(int id)
        {
            if (_currentMatch != null)
            {
                return _currentMatch.Players[id].Name;

            }
            else
            {
                return "Willem" + id;
            }
        }



    }
}
