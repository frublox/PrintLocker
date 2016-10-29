using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PrintLocker
{
    public partial class PrintLockerForm : Form
    {
        private PrintLocker printLocker;
        delegate void ShowCallback();

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

        public void showWindow()
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
