namespace Doorway_Studio
{
    partial class Options
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
            this.tipsTimer = new System.Windows.Forms.Timer(this.components);
            this.TipsTextBox = new System.Windows.Forms.TextBox();
            this.startGroupBox = new System.Windows.Forms.GroupBox();
            this.startMinimizedCheckBox = new System.Windows.Forms.CheckBox();
            this.startWithWindowsCheckBox = new System.Windows.Forms.CheckBox();
            this.generalGroupBox = new System.Windows.Forms.GroupBox();
            this.generalClearFolderCheckBox = new System.Windows.Forms.CheckBox();
            this.generalDFTADLabel = new System.Windows.Forms.Label();
            this.generalDFTANumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.generalDFTALabel = new System.Windows.Forms.Label();
            this.generalMPRTNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.generalMPRTLabel = new System.Windows.Forms.Label();
            this.baloonsGroupBox = new System.Windows.Forms.GroupBox();
            this.baloonsSecLabel = new System.Windows.Forms.Label();
            this.baloonsShowTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.baloonsShowTimeLabel = new System.Windows.Forms.Label();
            this.baloonsCheckBox = new System.Windows.Forms.CheckBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.updateGroupBox = new System.Windows.Forms.GroupBox();
            this.UpdateAtStartUpCheckBox = new System.Windows.Forms.CheckBox();
            this.startGroupBox.SuspendLayout();
            this.generalGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.generalDFTANumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.generalMPRTNumericUpDown)).BeginInit();
            this.baloonsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.baloonsShowTimeNumericUpDown)).BeginInit();
            this.updateGroupBox.SuspendLayout();
            this.SuspendLayout();
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
            this.TipsTextBox.Location = new System.Drawing.Point(0, 274);
            this.TipsTextBox.Multiline = true;
            this.TipsTextBox.Name = "TipsTextBox";
            this.TipsTextBox.Size = new System.Drawing.Size(437, 35);
            this.TipsTextBox.TabIndex = 29;
            // 
            // startGroupBox
            // 
            this.startGroupBox.Controls.Add(this.startMinimizedCheckBox);
            this.startGroupBox.Controls.Add(this.startWithWindowsCheckBox);
            this.startGroupBox.Location = new System.Drawing.Point(12, 118);
            this.startGroupBox.Name = "startGroupBox";
            this.startGroupBox.Size = new System.Drawing.Size(413, 45);
            this.startGroupBox.TabIndex = 30;
            this.startGroupBox.TabStop = false;
            // 
            // startMinimizedCheckBox
            // 
            this.startMinimizedCheckBox.AutoSize = true;
            this.startMinimizedCheckBox.Enabled = false;
            this.startMinimizedCheckBox.Location = new System.Drawing.Point(223, 19);
            this.startMinimizedCheckBox.Name = "startMinimizedCheckBox";
            this.startMinimizedCheckBox.Size = new System.Drawing.Size(15, 14);
            this.startMinimizedCheckBox.TabIndex = 1;
            this.startMinimizedCheckBox.UseVisualStyleBackColor = true;
            // 
            // startWithWindowsCheckBox
            // 
            this.startWithWindowsCheckBox.AutoSize = true;
            this.startWithWindowsCheckBox.Location = new System.Drawing.Point(6, 19);
            this.startWithWindowsCheckBox.Name = "startWithWindowsCheckBox";
            this.startWithWindowsCheckBox.Size = new System.Drawing.Size(15, 14);
            this.startWithWindowsCheckBox.TabIndex = 0;
            this.startWithWindowsCheckBox.UseVisualStyleBackColor = true;
            this.startWithWindowsCheckBox.CheckedChanged += new System.EventHandler(this.startWithWindowsCheckBox_CheckedChanged);
            // 
            // generalGroupBox
            // 
            this.generalGroupBox.Controls.Add(this.generalClearFolderCheckBox);
            this.generalGroupBox.Controls.Add(this.generalDFTADLabel);
            this.generalGroupBox.Controls.Add(this.generalDFTANumericUpDown);
            this.generalGroupBox.Controls.Add(this.generalDFTALabel);
            this.generalGroupBox.Controls.Add(this.generalMPRTNumericUpDown);
            this.generalGroupBox.Controls.Add(this.generalMPRTLabel);
            this.generalGroupBox.Location = new System.Drawing.Point(12, 12);
            this.generalGroupBox.Name = "generalGroupBox";
            this.generalGroupBox.Size = new System.Drawing.Size(332, 100);
            this.generalGroupBox.TabIndex = 31;
            this.generalGroupBox.TabStop = false;
            // 
            // generalClearFolderCheckBox
            // 
            this.generalClearFolderCheckBox.AutoSize = true;
            this.generalClearFolderCheckBox.Location = new System.Drawing.Point(6, 71);
            this.generalClearFolderCheckBox.Name = "generalClearFolderCheckBox";
            this.generalClearFolderCheckBox.Size = new System.Drawing.Size(15, 14);
            this.generalClearFolderCheckBox.TabIndex = 7;
            this.generalClearFolderCheckBox.UseVisualStyleBackColor = true;
            // 
            // generalDFTADLabel
            // 
            this.generalDFTADLabel.AutoSize = true;
            this.generalDFTADLabel.Location = new System.Drawing.Point(285, 47);
            this.generalDFTADLabel.Name = "generalDFTADLabel";
            this.generalDFTADLabel.Size = new System.Drawing.Size(0, 13);
            this.generalDFTADLabel.TabIndex = 6;
            // 
            // generalDFTANumericUpDown
            // 
            this.generalDFTANumericUpDown.Location = new System.Drawing.Point(223, 45);
            this.generalDFTANumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.generalDFTANumericUpDown.Name = "generalDFTANumericUpDown";
            this.generalDFTANumericUpDown.Size = new System.Drawing.Size(57, 20);
            this.generalDFTANumericUpDown.TabIndex = 5;
            this.generalDFTANumericUpDown.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // generalDFTALabel
            // 
            this.generalDFTALabel.AutoSize = true;
            this.generalDFTALabel.Location = new System.Drawing.Point(8, 47);
            this.generalDFTALabel.Name = "generalDFTALabel";
            this.generalDFTALabel.Size = new System.Drawing.Size(0, 13);
            this.generalDFTALabel.TabIndex = 4;
            // 
            // generalMPRTNumericUpDown
            // 
            this.generalMPRTNumericUpDown.Location = new System.Drawing.Point(223, 19);
            this.generalMPRTNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.generalMPRTNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.generalMPRTNumericUpDown.Name = "generalMPRTNumericUpDown";
            this.generalMPRTNumericUpDown.Size = new System.Drawing.Size(57, 20);
            this.generalMPRTNumericUpDown.TabIndex = 3;
            this.generalMPRTNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // generalMPRTLabel
            // 
            this.generalMPRTLabel.AutoSize = true;
            this.generalMPRTLabel.Location = new System.Drawing.Point(8, 21);
            this.generalMPRTLabel.Name = "generalMPRTLabel";
            this.generalMPRTLabel.Size = new System.Drawing.Size(0, 13);
            this.generalMPRTLabel.TabIndex = 0;
            // 
            // baloonsGroupBox
            // 
            this.baloonsGroupBox.Controls.Add(this.baloonsSecLabel);
            this.baloonsGroupBox.Controls.Add(this.baloonsShowTimeNumericUpDown);
            this.baloonsGroupBox.Controls.Add(this.baloonsShowTimeLabel);
            this.baloonsGroupBox.Controls.Add(this.baloonsCheckBox);
            this.baloonsGroupBox.Location = new System.Drawing.Point(12, 220);
            this.baloonsGroupBox.Name = "baloonsGroupBox";
            this.baloonsGroupBox.Size = new System.Drawing.Size(413, 49);
            this.baloonsGroupBox.TabIndex = 32;
            this.baloonsGroupBox.TabStop = false;
            this.baloonsGroupBox.Text = "Baloons";
            // 
            // baloonsSecLabel
            // 
            this.baloonsSecLabel.AutoSize = true;
            this.baloonsSecLabel.Location = new System.Drawing.Point(363, 23);
            this.baloonsSecLabel.Name = "baloonsSecLabel";
            this.baloonsSecLabel.Size = new System.Drawing.Size(0, 13);
            this.baloonsSecLabel.TabIndex = 3;
            // 
            // baloonsShowTimeNumericUpDown
            // 
            this.baloonsShowTimeNumericUpDown.Enabled = false;
            this.baloonsShowTimeNumericUpDown.Location = new System.Drawing.Point(300, 19);
            this.baloonsShowTimeNumericUpDown.Name = "baloonsShowTimeNumericUpDown";
            this.baloonsShowTimeNumericUpDown.Size = new System.Drawing.Size(57, 20);
            this.baloonsShowTimeNumericUpDown.TabIndex = 2;
            this.baloonsShowTimeNumericUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // baloonsShowTimeLabel
            // 
            this.baloonsShowTimeLabel.AutoSize = true;
            this.baloonsShowTimeLabel.Location = new System.Drawing.Point(237, 23);
            this.baloonsShowTimeLabel.Name = "baloonsShowTimeLabel";
            this.baloonsShowTimeLabel.Size = new System.Drawing.Size(0, 13);
            this.baloonsShowTimeLabel.TabIndex = 1;
            // 
            // baloonsCheckBox
            // 
            this.baloonsCheckBox.AutoSize = true;
            this.baloonsCheckBox.Location = new System.Drawing.Point(6, 19);
            this.baloonsCheckBox.Name = "baloonsCheckBox";
            this.baloonsCheckBox.Size = new System.Drawing.Size(15, 14);
            this.baloonsCheckBox.TabIndex = 0;
            this.baloonsCheckBox.UseVisualStyleBackColor = true;
            this.baloonsCheckBox.CheckedChanged += new System.EventHandler(this.baloonsCheckBox_CheckedChanged);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(350, 12);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 33;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(350, 41);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 34;
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // updateGroupBox
            // 
            this.updateGroupBox.Controls.Add(this.UpdateAtStartUpCheckBox);
            this.updateGroupBox.Location = new System.Drawing.Point(12, 169);
            this.updateGroupBox.Name = "updateGroupBox";
            this.updateGroupBox.Size = new System.Drawing.Size(413, 45);
            this.updateGroupBox.TabIndex = 35;
            this.updateGroupBox.TabStop = false;
            // 
            // UpdateAtStartUpCheckBox
            // 
            this.UpdateAtStartUpCheckBox.AutoSize = true;
            this.UpdateAtStartUpCheckBox.Location = new System.Drawing.Point(6, 19);
            this.UpdateAtStartUpCheckBox.Name = "UpdateAtStartUpCheckBox";
            this.UpdateAtStartUpCheckBox.Size = new System.Drawing.Size(15, 14);
            this.UpdateAtStartUpCheckBox.TabIndex = 0;
            this.UpdateAtStartUpCheckBox.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 309);
            this.Controls.Add(this.updateGroupBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.baloonsGroupBox);
            this.Controls.Add(this.generalGroupBox);
            this.Controls.Add(this.startGroupBox);
            this.Controls.Add(this.TipsTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Options";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.Options_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Options_FormClosing);
            this.startGroupBox.ResumeLayout(false);
            this.startGroupBox.PerformLayout();
            this.generalGroupBox.ResumeLayout(false);
            this.generalGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.generalDFTANumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.generalMPRTNumericUpDown)).EndInit();
            this.baloonsGroupBox.ResumeLayout(false);
            this.baloonsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.baloonsShowTimeNumericUpDown)).EndInit();
            this.updateGroupBox.ResumeLayout(false);
            this.updateGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tipsTimer;
        private System.Windows.Forms.TextBox TipsTextBox;
        private System.Windows.Forms.GroupBox startGroupBox;
        private System.Windows.Forms.CheckBox startWithWindowsCheckBox;
        private System.Windows.Forms.CheckBox startMinimizedCheckBox;
        private System.Windows.Forms.GroupBox generalGroupBox;
        private System.Windows.Forms.GroupBox baloonsGroupBox;
        private System.Windows.Forms.Label baloonsSecLabel;
        private System.Windows.Forms.NumericUpDown baloonsShowTimeNumericUpDown;
        private System.Windows.Forms.Label baloonsShowTimeLabel;
        private System.Windows.Forms.CheckBox baloonsCheckBox;
        private System.Windows.Forms.CheckBox generalClearFolderCheckBox;
        private System.Windows.Forms.Label generalDFTADLabel;
        private System.Windows.Forms.NumericUpDown generalDFTANumericUpDown;
        private System.Windows.Forms.Label generalDFTALabel;
        private System.Windows.Forms.NumericUpDown generalMPRTNumericUpDown;
        private System.Windows.Forms.Label generalMPRTLabel;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox updateGroupBox;
        private System.Windows.Forms.CheckBox UpdateAtStartUpCheckBox;
    }
}