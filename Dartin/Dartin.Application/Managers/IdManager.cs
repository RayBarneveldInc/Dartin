using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;
using Caliburn.Micro;
using Dartin.Models;

namespace Dartin.Managers
{
    public class IdManager
    {
        private static IdManager _instance;

        private IdManager()
        {
            _instance = this;
        }

        public static IdManager GetInstance()
        {
            return _instance ?? new IdManager();
        }

        public static string GenerateId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}