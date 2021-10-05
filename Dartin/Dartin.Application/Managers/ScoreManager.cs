using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;
using Caliburn.Micro;
using Dartin.Models;

namespace Dartin.Managers
{
    public class ScoreManager
    {
        private static ScoreManager _instance;

        private ScoreManager()
        {
            _instance = this;
        }

        public static ScoreManager GetInstance()
        {
            return _instance ?? new ScoreManager();
        }

        public static void SetPlayerTurnScore()
        {
        }
    }
}