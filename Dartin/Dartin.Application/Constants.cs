using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dartin
{
    public static class Constants
    {
        public const string test = "fadsfsd";
        public const string SavePath = "./Saved/";
        public const string SaveFileName = "saved.json";
        public static readonly string SaveFilePath = Path.Combine(SavePath, SaveFileName);
    }
}
