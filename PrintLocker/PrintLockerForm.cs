using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Printing;
using System.Threading;
using System.IO;

namespace PrintLocker
{
    public partial class PrintLockerForm : Form
    {
        bool printingDisabled = true;
        List<string> queuesToBlock;

        byte[] passwordHash;
        SHA256 mySHA256;

        private Thread jobMonitor;
        delegate void ShowCallback();

        public PrintLockerForm(byte[] passwordHash, List<string> queuesToBlock)
        {
            InitializeComponent();

            notifyIcon.Icon = SystemIcons.Application;

            this.passwordHash = passwordHash;
            this.queuesToBlock = queuesToBlock;

            mySHA256 = SHA256.Create();

            jobMonitor = new Thread(new ThreadStart(monitorQueues));
            jobMonitor.Start();
        }

        private byte[] computeHash(string password)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(password);

            return mySHA256.ComputeHash(bytes);
        }

        private void disableInput()
        {
            buttonSubmit.Enabled = false;
            inputPassword.Enabled = false;
            buttonLock.Enabled = true;
        }

        private void enableInput()
        {
            inputPassword.Enabled = true;
            buttonSubmit.Enabled = true;
            buttonLock.Enabled = false;
        }

        private void enablePrinting()
        {
            printingDisabled = false;

            labelStatus.ForeColor = Color.Green;
            labelStatus.Text = "Successfully enabled printing.";

            labelPrintingStatus.ForeColor = Color.Green;
            labelPrintingStatus.Text = "Enabled";

            inputPassword.Text = "";

            disableInput();
        }

        private void disablePrinting()
        {
            printingDisabled = true;

            labelStatus.ForeColor = Color.Red;

            labelPrintingStatus.ForeColor = Color.Red;
            labelPrintingStatus.Text = "Disabled";

            inputPassword.Text = "";

            enableInput();
        }

        private void lockPrinting()
        {
            disablePrinting();
            labelStatus.Text = "Printing has been re-locked.";
        }

        private void monitorQueues()
        {
            LocalPrintServer server = new LocalPrintServer();

            while (true)
            {
                pauseJobs(server);

                Thread.Sleep(2000);
            }
        }

        private void pauseJobs(LocalPrintServer server)
        {
            PrintQueue queue;

            foreach (string queueName in queuesToBlock)
            {
                queue = new PrintQueue(server, queueName);
                queue.Refresh();
                   
                foreach (PrintSystemJobInfo job in queue.GetPrintJobInfoCollection())
                {
                    job.Refresh();

                    logJob(job.JobIdentifier, job.NumberOfPages, job.TimeJobSubmitted, job.Submitter);

                    if (printingDisabled && !job.IsPaused && job.Submitter.Equals(Environment.UserName))
                    {
                        showWindow();
                        job.Pause();
                        job.Refresh();
                    }
                }
            }
        }

        private void logJob(int jobId, int numPages, DateTime timestamp, string user)
        {
            if (!File.ReadAllLines(Prefs.LogFilepath).Contains("Job ID " + jobId.ToString()))
            {
                string logLine = timestamp.ToLongTimeString() +
                    ", Job ID " + jobId.ToString() + ", User: " + user + ", " + numPages.ToString() + " pages\n";
                File.AppendAllText(Prefs.LogFilepath, logLine);
            }
        }

        private void showWindow()
        {
            if (InvokeRequired)
            {
                Invoke(new ShowCallback(Show));
            }
            else
            {
                Show();
            }
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            checkPassword();
        }

        private void checkPassword()
        {
            byte[] inputPasswordHash = computeHash(inputPassword.Text);

            if (passwordHash.SequenceEqual(inputPasswordHash))
            {
                enablePrinting();
            }
            else
            {
                disablePrinting();
                labelStatus.Text = "Incorrect password.";
            }
        }

        private void minimiseToTray()
        {
            WindowState = FormWindowState.Minimized;
            Hide();
            notifyIcon.BalloonTipTitle = "Print Locker";
            notifyIcon.BalloonTipText = "Print Locker is now minimised to the system tray, but will continue to run in the background.";
            notifyIcon.ShowBalloonTip(3000);
        }

        private void restoreFromTray()
        {
            showWindow();
            WindowState = FormWindowState.Normal;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            jobMonitor.Abort();
        }

        private void buttonLock_Click(object sender, EventArgs e)
        {
            lockPrinting();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                minimiseToTray();
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            restoreFromTray();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            minimiseToTray();
        }

        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            restoreFromTray();
        }
    }
}
