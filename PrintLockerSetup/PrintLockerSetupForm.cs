using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Printing;

namespace PrintLockerSetup
{
    public partial class PrintLockerSetupForm : Form
    {
        SHA256 mySHA256 = SHA256.Create();

        public PrintLockerSetupForm()
        {
            InitializeComponent();

            checkedListBoxQueues.Items.AddRange(getPrinterQueueNames());
            checkedListBoxQueues.CheckOnClick = true;
            
            readConfig();
        }

        private string[] getPrinterQueueNames()
        {
            LocalPrintServer server = new LocalPrintServer();

            List<string> names = new List<string>();

            foreach (PrintQueue queue in server.GetPrintQueues())
            {
                names.Add(queue.Name);
            }

            return names.ToArray<string>();
        }

        private void readConfig()
        {
            List<string> queuesToBlock = new List<string>(File.ReadAllLines(Prefs.ConfigFilepath));

            for (int i = 0, len = checkedListBoxQueues.Items.Count; i < len; i++)
            {
                var item = checkedListBoxQueues.Items[i];

                if (queuesToBlock.Contains(item.ToString()))
                {
                    checkedListBoxQueues.SetItemChecked(i, true);
                }
            }
        }

        private void saveConfig()
        {
            List<string> queueNames = new List<string>();
            
            foreach (var item in checkedListBoxQueues.CheckedItems)
            {
                queueNames.Add(item.ToString());
            }

            File.WriteAllLines(Prefs.ConfigFilepath, queueNames);

            labelStatus.ForeColor = Color.Green;
            labelStatus.Text = "Successfully saved the list of printers to block.";
        }

        private void setPassword()
        {
            string password = inputPassword.Text;
            byte[] hash = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));

            try
            {
                if (!Directory.Exists(Prefs.AppDir))
                {
                    Directory.CreateDirectory(Prefs.AppDir);
                }

                File.WriteAllBytes(Prefs.HashFilepath, hash);

                labelStatus.ForeColor = Color.Green;
                labelStatus.Text = "Password successfully set.";
            }
            catch (IOException)
            {
                labelStatus.ForeColor = Color.Red;
                labelStatus.Text = "Couldn't save the password to the disk due to an I/O error.";
            }
            catch (UnauthorizedAccessException)
            {
                labelStatus.ForeColor = Color.Red;
                labelStatus.Text = "Permission denied when trying to save the password to the disk.";
            }

            inputPassword.Text = "";
        }


        private void clearLog()
        {
            try
            {
                File.WriteAllText(Prefs.LogFilepath, "");
            }
            catch (IOException)
            {
                labelStatus.ForeColor = Color.Red;
                labelStatus.Text = "There was an I/O problem trying to clear the log file.";

            }
            catch (UnauthorizedAccessException)
            {
                labelStatus.ForeColor = Color.Red;
                labelStatus.Text = "Permission denied when trying to clear the log file.";
            }

            labelStatus.ForeColor = Color.Green;
            labelStatus.Text = "Successfully cleared the log file.";
        }

        private void buttonSetPassLoc_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();

            string path = folderBrowserDialog.SelectedPath + @"\password.hash";

            File.WriteAllText(Prefs.HashLocationFilepath, path);
            Prefs.HashFilepath = path;

            labelStatus.ForeColor = Color.Green;
            labelStatus.Text = "Password file will now be saved to: " + path;
        }

        private void buttonSetPassword_Click(object sender, EventArgs e)
        {
            setPassword();
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            clearLog();
        }

        private void buttonSaveConfig_Click(object sender, EventArgs e)
        {
            saveConfig();
        }
    }
}
