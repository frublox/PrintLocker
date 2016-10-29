using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PrintLocker
{
    public partial class PrintLockerForm : Form
    {
        private PrintLocker printLocker;
        delegate void Callback();

        public PrintLockerForm(byte[] passwordHash, List<string> queuesToBlock)
        {
            InitializeComponent();

            notifyIcon.Icon = SystemIcons.Application;

            printLocker = new PrintLocker(passwordHash, queuesToBlock, this);
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
            printLocker.PrintingDisabled = false;

            labelStatus.ForeColor = Color.Green;
            labelStatus.Text = "Successfully enabled printing.";

            labelPrintingStatus.ForeColor = Color.Green;
            labelPrintingStatus.Text = "Enabled";

            inputPassword.Text = "";

            disableInput();
        }

        private void disablePrinting()
        {
            printLocker.PrintingDisabled = true;

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
                enablePrinting();
            }
            else
            {
                disablePrinting();
                labelStatus.Text = "Incorrect password.";
            }
        }

        public void MinimiseToTray()
        {
            WindowState = FormWindowState.Minimized;
            Hide();
            notifyIcon.BalloonTipTitle = "Print Locker";
            notifyIcon.BalloonTipText = "Print Locker is now minimised to the system tray, but will continue to run in the background.";
            notifyIcon.ShowBalloonTip(3000);
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

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            checkPassword();
        }

        private void buttonLock_Click(object sender, EventArgs e)
        {
            lockPrinting();
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
    }
}
