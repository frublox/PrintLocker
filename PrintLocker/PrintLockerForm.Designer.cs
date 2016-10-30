namespace PrintLocker
{
    partial class PrintLockerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.inputPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.labelPrintingStatus = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonLock = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputPassword
            // 
            this.inputPassword.Location = new System.Drawing.Point(76, 32);
            this.inputPassword.Name = "inputPassword";
            this.inputPassword.PasswordChar = '*';
            this.inputPassword.Size = new System.Drawing.Size(253, 20);
            this.inputPassword.TabIndex = 0;
            this.inputPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(311, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "You must enter a password to enable printing from this computer.";
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(335, 32);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(80, 21);
            this.buttonSubmit.TabIndex = 3;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Printing status:";
            // 
            // labelPrintingStatus
            // 
            this.labelPrintingStatus.AutoSize = true;
            this.labelPrintingStatus.ForeColor = System.Drawing.Color.Red;
            this.labelPrintingStatus.Location = new System.Drawing.Point(96, 87);
            this.labelPrintingStatus.Name = "labelPrintingStatus";
            this.labelPrintingStatus.Size = new System.Drawing.Size(48, 13);
            this.labelPrintingStatus.TabIndex = 5;
            this.labelPrintingStatus.Text = "Disabled";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStatus.Location = new System.Drawing.Point(14, 61);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 13);
            this.labelStatus.TabIndex = 6;
            // 
            // buttonLock
            // 
            this.buttonLock.Enabled = false;
            this.buttonLock.Location = new System.Drawing.Point(249, 82);
            this.buttonLock.Name = "buttonLock";
            this.buttonLock.Size = new System.Drawing.Size(80, 23);
            this.buttonLock.TabIndex = 7;
            this.buttonLock.Text = "Lock Printing";
            this.buttonLock.UseVisualStyleBackColor = true;
            this.buttonLock.Click += new System.EventHandler(this.buttonLock_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Print Locker has been minimised to the system tray, and is still running in the b" +
    "ackground.";
            this.notifyIcon.BalloonTipTitle = "Notice";
            this.notifyIcon.Text = "Print Locker";
            this.notifyIcon.Visible = true;
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_BalloonTipClicked);
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(335, 82);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // PrintLockerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 109);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonLock);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.labelPrintingStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.inputPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PrintLockerForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Locker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PrintLockerForm_FormClosed);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelPrintingStatus;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonLock;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button buttonCancel;
    }
}

