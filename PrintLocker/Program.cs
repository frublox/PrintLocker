using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;

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

            if (fileMissingOrEmpty(Prefs.ConfigFilepath))
            {
                const string msg = "No printers have been configured to be blocked, so the default printer has been chosen. "
                    + "This can be changed in the setup program.";
                const string caption = "Missing Configuration";

                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (fileMissingOrEmpty(Prefs.HashLocationFilepath))
            {
                const string msg = "No location for the password file has been set. The default will be set when the setup program is run," 
                    + " and afterwards a custom location may be set.";
                const string caption = "Missing Password Location";

                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Prefs.HashFilepath = File.ReadAllText(Prefs.HashLocationFilepath);

            if (fileMissing(Prefs.LogFilepath))
            {
                const string msg = "A log file has not been created. One will automatically be created when the setup program is run.";
                const string caption = "Missing Log File";

                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (fileMissingOrEmpty(Prefs.HashFilepath))
            {
                const string msg = "No password has been set. Set one using the setup program.";
                const string caption = "Missing Password";

                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                passwordHash = File.ReadAllBytes(Prefs.HashFilepath);
                queuesToBlock = new List<string>(File.ReadAllLines(Prefs.ConfigFilepath));

                Application.Run(new PrintLockerForm(passwordHash, queuesToBlock));
            }
        }

        private static bool fileMissing(string path)
        {
            return !File.Exists(path);
        }

        private static bool fileMissingOrEmpty(string path)
        {
            return !File.Exists(path) || File.ReadAllBytes(path).Length == 0;
        }
    }
}
