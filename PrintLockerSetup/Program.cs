using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;

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

            makeLogFileWritable();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PrintLockerSetupForm());
        }

        private static bool fileMissingOrEmpty(string path)
        {
            return !File.Exists(path) || File.ReadAllBytes(path).Length == 0;
        }

        private static void makeLogFileWritable()
        {
            FileSecurity fileSecurity = File.GetAccessControl(Prefs.LogFilepath);

            fileSecurity.AddAccessRule(new FileSystemAccessRule(
                Environment.UserName, FileSystemRights.Write, AccessControlType.Allow));

            File.SetAccessControl(Prefs.LogFilepath, fileSecurity);
        }
    }
}
