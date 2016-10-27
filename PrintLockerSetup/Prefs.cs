using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PrintLockerSetup
{
    public static class Prefs
    {
        public static string ProgFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        public static string AppDir = ProgFiles + @"\PrintLocker\";
        public static string HashLocationFilepath = AppDir + "passwordLocation.txt";
        public static string HashFilepath;
        public static string ConfigFilepath = AppDir + "config.txt";

        public static string LogFilepath = AppDir + "jobs.log";
    }
}
