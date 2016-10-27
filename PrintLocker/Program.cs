using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace PrintLocker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            byte[] passwordHash = null;
            List<string> queuesToBlock = null;

            if (!File.Exists(Prefs.HashLocationFilepath) || File.ReadAllLines(Prefs.HashLocationFilepath).Length == 0)
            {
                const string msg = "No location for the password file has been set. Set one using the setup program.";
                const string caption = "Missing Password Location";

                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Prefs.HashFilepath = File.ReadAllText(Prefs.HashLocationFilepath);

            if (!File.Exists(Prefs.LogFilepath))
            {
                const string msg = "A log file has not been created. One will automatically be created when the setup program is run.";
                const string caption = "Missing Log File";

                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!File.Exists(Prefs.HashFilepath) || File.ReadAllBytes(Prefs.HashFilepath).Length == 0)
            {
                const string msg = "No password has been set. Set one using the setup program.";
                const string caption = "Missing Password";

                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!File.Exists(Prefs.ConfigFilepath) || File.ReadAllLines(Prefs.ConfigFilepath).Length == 0)
            {
                const string msg = "No printers have been configured to be blocked. Set at least one using the setup program.";
                const string caption = "Missing Configuration";

                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            else
            {
                passwordHash = File.ReadAllBytes(Prefs.HashFilepath);
                queuesToBlock = new List<string>(File.ReadAllLines(Prefs.ConfigFilepath));
                Application.Run(new PrintLockerForm(passwordHash, queuesToBlock));
            }
        }
    }
}
