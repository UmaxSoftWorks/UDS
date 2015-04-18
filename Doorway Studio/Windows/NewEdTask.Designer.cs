namespace Doorway_Studio
{
    partial class NewEdTask
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
            this.topPanel = new System.Windows.Forms.Panel();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.StartDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.startTimeLabel = new System.Windows.Forms.Label();
            this.tipsTimer = new System.Windows.Forms.Timer(this.components);
            this.TipsTextBox = new System.Windows.Forms.TextBox();
            this.mainTaskControl = new Doorway_Studio.TaskControl();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.nameTextBox);
            this.topPanel.Controls.Add(this.nameLabel);
            this.topPanel.Controls.Add(this.cancelButton);
            this.topPanel.Controls.Add(this.OKButton);
            this.topPanel.Controls.Add(this.StartDateTimePicker);
            this.topPanel.Controls.Add(this.startTimeLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(625, 40);
            this.topPanel.TabIndex = 0;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(53, 12);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(124, 20);
            this.nameTextBox.TabIndex = 1;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 15);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(0, 13);
            this.nameLabel.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(538, 10);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(457, 10);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // StartDateTimePicker
            // 
            this.StartDateTimePicker.CustomFormat = "MMMM dd, yyyy hh:mm:ss tt";
            this.StartDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDateTimePicker.Location = new System.Drawing.Point(240, 12);
            this.StartDateTimePicker.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.StartDateTimePicker.Name = "StartDateTimePicker";
            this.StartDateTimePicker.Size = new System.Drawing.Size(211, 20);
            this.StartDateTimePicker.TabIndex = 3;
            // 
            // startTimeLabel
            // 
            this.startTimeLabel.AutoSize = true;
            this.startTimeLabel.Location = new System.Drawing.Point(183, 15);
            this.startTimeLabel.Name = "startTimeLabel";
            this.startTimeLabel.Size = new System.Drawing.Size(0, 13);
            this.startTimeLabel.TabIndex = 2;
            // 
            // tipsTimer
            // 
            this.tipsTimer.Interval = 5000;
            this.tipsTimer.Tick += new System.EventHandler(this.tipsTimer_Tick);
            // 
            // TipsTextBox
            // 
            this.TipsTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.TipsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TipsTextBox.Enabled = false;
            this.TipsTextBox.Location = new System.Drawing.Point(0, 477);
            this.TipsTextBox.Multiline = true;
            this.TipsTextBox.Name = "TipsTextBox";
            this.TipsTextBox.Size = new System.Drawing.Size(625, 35);
            this.TipsTextBox.TabIndex = 29;
            // 
            // mainTaskControl
            // 
            this.mainTaskControl.Location = new System.Drawing.Point(0, 39);
            this.mainTaskControl.Name = "mainTaskControl";
            this.mainTaskControl.SelectedPreset = -1;
            this.mainTaskControl.SelectedTemplate = -1;
            this.mainTaskControl.SelectedText = -1;
            this.mainTaskControl.SelectedWorkSpace = -1;
            this.mainTaskControl.Size = new System.Drawing.Size(624, 440);
            this.mainTaskControl.TabIndex = 1;
            // 
            // NewEdTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 512);
            this.Controls.Add(this.mainTaskControl);
            this.Controls.Add(this.TipsTextBox);
            this.Controls.Add(this.topPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "NewEdTask";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.NewEdTask_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewEdTask_FormClosing);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.DateTimePicker StartDateTimePicker;
        private System.Windows.Forms.Label startTimeLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Timer tipsTimer;
        private System.Windows.Forms.TextBox TipsTextBox;
        private TaskControl mainTaskControl;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label nameLabel;
    }
}