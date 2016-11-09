using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace PrintLocker
{
    public partial class PrintLockerForm : Form
    {
        private PrintLocker printLocker;
        delegate void Callback();

        public PrintLockerForm(byte[] passwordHash, List<string> queuesToBlock)
        {
            InitializeComponent();

            Icon = new System.Drawing.Icon(Prefs.AppDir + "printer.ico");
            notifyIcon.Icon = Icon;

            printLocker = new PrintLocker(passwordHash, queuesToBlock, this);

            MinimiseToTray();
        }

        private void allowPrintJob()
        {
            MinimiseToTray();

            printLocker.PrintingDisabled = false;
            printLocker.ResumeLatestJob();

            inputPassword.Text = "";

            Thread.Sleep(500);

            printLocker.PrintingDisabled = true;
        }

        public void ShowWindow()
        {
            if (InvokeRequired)
            {
                Invoke(new Callback(Show));
            }
            else
            {
                Show();
            }
        }

        private void checkPassword()
        {
            if (printLocker.CheckPassword(inputPassword.Text))
            {
                allowPrintJob();
                labelNotification.Text = "";
            }
            else
            {
                labelNotification.Text = "Incorrect password, please try again.";
            }
        }

        public void MinimiseToTray()
        {
            WindowState = FormWindowState.Minimized;
            Hide();
        }

        public void RestoreFromTray()
        {
            ShowWindow();

            if (InvokeRequired)
            {
                Invoke(new Callback(() => WindowState = FormWindowState.Normal));
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason != CloseReason.WindowsShutDown)
            {
                e.Cancel = true;
                MinimiseToTray();
            }
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            checkPassword();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                MinimiseToTray();
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            RestoreFromTray();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            MinimiseToTray();
        }

        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            RestoreFromTray();
        }

        private void PrintLockerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            printLocker.ResumeJobs();
        }
    }
}
