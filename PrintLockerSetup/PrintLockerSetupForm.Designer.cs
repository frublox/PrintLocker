namespace PrintLockerSetup
{
    partial class PrintLockerSetupForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.inputPassword = new System.Windows.Forms.TextBox();
            this.buttonSetPassword = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.checkedListBoxQueues = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSaveConfig = new System.Windows.Forms.Button();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.buttonSetPassLoc = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBoxStartup = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Set a new password:";
            // 
            // inputPassword
            // 
            this.inputPassword.Location = new System.Drawing.Point(133, 14);
            this.inputPassword.Name = "inputPassword";
            this.inputPassword.Size = new System.Drawing.Size(325, 20);
            this.inputPassword.TabIndex = 1;
            this.inputPassword.UseSystemPasswordChar = true;
            // 
            // buttonSetPassword
            // 
            this.buttonSetPassword.Location = new System.Drawing.Point(464, 12);
            this.buttonSetPassword.Name = "buttonSetPassword";
            this.buttonSetPassword.Size = new System.Drawing.Size(75, 23);
            this.buttonSetPassword.TabIndex = 2;
            this.buttonSetPassword.Text = "Set";
            this.buttonSetPassword.UseVisualStyleBackColor = true;
            this.buttonSetPassword.Click += new System.EventHandler(this.buttonSetPassword_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 207);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 13);
            this.labelStatus.TabIndex = 3;
            // 
            // checkedListBoxQueues
            // 
            this.checkedListBoxQueues.FormattingEnabled = true;
            this.checkedListBoxQueues.Location = new System.Drawing.Point(133, 40);
            this.checkedListBoxQueues.Name = "checkedListBoxQueues";
            this.checkedListBoxQueues.Size = new System.Drawing.Size(325, 94);
            this.checkedListBoxQueues.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Printers to lock:";
            // 
            // buttonSaveConfig
            // 
            this.buttonSaveConfig.Location = new System.Drawing.Point(464, 40);
            this.buttonSaveConfig.Name = "buttonSaveConfig";
            this.buttonSaveConfig.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveConfig.TabIndex = 6;
            this.buttonSaveConfig.Text = "Save";
            this.buttonSaveConfig.UseVisualStyleBackColor = true;
            this.buttonSaveConfig.Click += new System.EventHandler(this.buttonSaveConfig_Click);
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Location = new System.Drawing.Point(133, 149);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(103, 23);
            this.buttonClearLog.TabIndex = 7;
            this.buttonClearLog.Text = "Clear Log";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // buttonSetPassLoc
            // 
            this.buttonSetPassLoc.Location = new System.Drawing.Point(242, 149);
            this.buttonSetPassLoc.Name = "buttonSetPassLoc";
            this.buttonSetPassLoc.Size = new System.Drawing.Size(129, 23);
            this.buttonSetPassLoc.TabIndex = 8;
            this.buttonSetPassLoc.Text = "Set Password Location";
            this.buttonSetPassLoc.UseVisualStyleBackColor = true;
            this.buttonSetPassLoc.Click += new System.EventHandler(this.buttonSetPassLoc_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Miscellaneous:";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // checkBoxStartup
            // 
            this.checkBoxStartup.AutoSize = true;
            this.checkBoxStartup.Location = new System.Drawing.Point(133, 178);
            this.checkBoxStartup.Name = "checkBoxStartup";
            this.checkBoxStartup.Size = new System.Drawing.Size(153, 17);
            this.checkBoxStartup.TabIndex = 11;
            this.checkBoxStartup.Text = "Run PrintLocker on startup";
            this.checkBoxStartup.UseVisualStyleBackColor = true;
            this.checkBoxStartup.Click += new System.EventHandler(this.checkBoxStartup_Click);
            // 
            // PrintLockerSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 229);
            this.Controls.Add(this.checkBoxStartup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonSetPassLoc);
            this.Controls.Add(this.buttonClearLog);
            this.Controls.Add(this.buttonSaveConfig);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkedListBoxQueues);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonSetPassword);
            this.Controls.Add(this.inputPassword);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "PrintLockerSetupForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Locker Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputPassword;
        private System.Windows.Forms.Button buttonSetPassword;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.CheckedListBox checkedListBoxQueues;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSaveConfig;
        private System.Windows.Forms.Button buttonClearLog;
        private System.Windows.Forms.Button buttonSetPassLoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.CheckBox checkBoxStartup;
    }
}

