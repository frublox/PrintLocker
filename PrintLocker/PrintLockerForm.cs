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

        private void showWindow()
        {
            if (InvokeRequired)
            {
                Invoke(new Callback(() => TopMost = true));
                Invoke(new Callback(Show));
                Invoke(new Callback(Activate));
            }
            else
            {
                Show();
                Activate();
            }
        }

        private void checkPassword()
        {
            if (printLocker.CheckPassword(inputPassword.Text))
            {
                printLocker.AllowPrintJob();

                inputPassword.Text = "";
                labelNotification.Text = "";
                MinimiseToTray();
            }
            else
            {
                labelNotification.Text = "Incorrect password, please try again.";
            }
        }

        public void MinimiseToTray()
        {
            if (InvokeRequired)
            {
                Invoke(new Callback(() => WindowState = FormWindowState.Minimized));
                Invoke(new Callback(Hide));
            }
            else
            {
                WindowState = FormWindowState.Minimized;
                Hide();
            }
            
        }

        public void RestoreFromTray()
        {
            showWindow();

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
            printLocker.DeleteLastJob();
        }

        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            RestoreFromTray();
        }
    }
}
