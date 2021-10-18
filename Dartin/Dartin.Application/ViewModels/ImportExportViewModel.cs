﻿using Caliburn.Micro;
using Dartin.Managers;
using Newtonsoft.Json;
using System.IO;

namespace Dartin.ViewModels
{
    class ImportExportViewModel : Screen, IViewModel
    {
        public string ViewName => throw new System.NotImplementedException();

        public static void Import()
        {
            // Create OpenFileDialog.
            Microsoft.Win32.OpenFileDialog openFileDlg = new();
            // Limit filetype.
            openFileDlg.Filter = "JSON Database|*.json";
            openFileDlg.Title = "Import a database file";

            // Launch OpenFileDialog by calling ShowDialog method.
            bool? result = openFileDlg.ShowDialog();

            // Try to import the file, with some validity checks.
            if ((bool)result)
            {
                var state = JsonConvert.DeserializeObject<State>(File.ReadAllText(openFileDlg.FileName));
                State.Instance.Merge(state);
            }
        }

        public static void Export()
        {
            // Create OpenFileDialog.
            Microsoft.Win32.SaveFileDialog saveFileDlg = new();
            // Limit filetype.
            saveFileDlg.Filter = "JSON Database|*.json";
            saveFileDlg.Title = "Save the database file";

            // Launch OpenFileDialog by calling ShowDialog method.
            saveFileDlg.ShowDialog();

            // Export the database to the designated location.
            if (saveFileDlg.FileName != "")
            {
                State.Instance.Save(saveFileDlg.FileName);
            }
        }

        public static void ClearDB()
        {
            State.Instance.Clear();
        }

        public static void MainMenu()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MainMenuViewModel());
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public ImportExportViewModel()
        {

        }
    }
}