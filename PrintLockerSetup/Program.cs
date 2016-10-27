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
            if (!File.Exists(Prefs.ConfigFilepath))
            {
                File.WriteAllText(Prefs.ConfigFilepath, "");
            }

            if (!File.Exists(Prefs.HashLocationFilepath))
            {
                File.WriteAllText(Prefs.HashLocationFilepath, Prefs.AppDir + "password.hash");
                Prefs.HashFilepath = File.ReadAllText(Prefs.HashLocationFilepath);
            }

            if (!File.Exists(Prefs.LogFilepath))
            {
                File.WriteAllText(Prefs.LogFilepath, "");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PrintLockerSetupForm());
        }
    }
}
