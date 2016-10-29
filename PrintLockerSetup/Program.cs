using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PrintLockerSetup
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (fileMissingOrEmpty(Prefs.ConfigFilepath))
            {
                File.WriteAllText(Prefs.ConfigFilepath, "");
            }

            if (fileMissingOrEmpty(Prefs.HashLocationFilepath))
            {
                File.WriteAllText(Prefs.HashLocationFilepath, Prefs.AppDir + "password.hash");
            }

            Prefs.HashFilepath = File.ReadAllText(Prefs.HashLocationFilepath);

            if (fileMissingOrEmpty(Prefs.LogFilepath))
            {
                File.WriteAllText(Prefs.LogFilepath, "");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PrintLockerSetupForm());
        }

        private static bool fileMissingOrEmpty(string path)
        {
            return !File.Exists(path) || File.ReadAllBytes(path).Length == 0;
        }
    }
}
