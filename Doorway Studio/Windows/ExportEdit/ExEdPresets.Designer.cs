namespace Doorway_Studio
{
    partial class ExEdPresets
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
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.deleteButton = new System.Windows.Forms.Button();
            this.SaveAsLabel = new System.Windows.Forms.Label();
            this.SaveAsTextBox = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SaveAsButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.TipsTextBox = new System.Windows.Forms.TextBox();
            this.tipsTimer = new System.Windows.Forms.Timer(this.components);
            this.savePresetFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainTaskControl = new Doorway_Studio.TaskControl();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.deleteButton);
            this.bottomPanel.Controls.Add(this.SaveAsLabel);
            this.bottomPanel.Controls.Add(this.SaveAsTextBox);
            this.bottomPanel.Controls.Add(this.SaveButton);
            this.bottomPanel.Controls.Add(this.SaveAsButton);
            this.bottomPanel.Controls.Add(this.ExportButton);
            this.bottomPanel.Controls.Add(this.TipsTextBox);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 438);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(625, 66);
            this.bottomPanel.TabIndex = 0;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(538, 6);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 35;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // SaveAsLabel
            // 
            this.SaveAsLabel.AutoSize = true;
            this.SaveAsLabel.Location = new System.Drawing.Point(140, 11);
            this.SaveAsLabel.Name = "SaveAsLabel";
            this.SaveAsLabel.Size = new System.Drawing.Size(0, 13);
            this.SaveAsLabel.TabIndex = 34;
            // 
            // SaveAsTextBox
            // 
            this.SaveAsTextBox.Location = new System.Drawing.Point(186, 8);
            this.SaveAsTextBox.Name = "SaveAsTextBox";
            this.SaveAsTextBox.Size = new System.Drawing.Size(125, 20);
            this.SaveAsTextBox.TabIndex = 33;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(457, 6);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 32;
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SaveAsButton
            // 
            this.SaveAsButton.Location = new System.Drawing.Point(317, 6);
            this.SaveAsButton.Name = "SaveAsButton";
            this.SaveAsButton.Size = new System.Drawing.Size(113, 23);
            this.SaveAsButton.TabIndex = 31;
            this.SaveAsButton.UseVisualStyleBackColor = true;
            this.SaveAsButton.Click += new System.EventHandler(this.SaveAsButton_Click);
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(12, 6);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(75, 23);
            this.ExportButton.TabIndex = 30;
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // TipsTextBox
            // 
            this.TipsTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.TipsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TipsTextBox.Enabled = false;
            this.TipsTextBox.Location = new System.Drawing.Point(0, 31);
            this.TipsTextBox.Multiline = true;
            this.TipsTextBox.Name = "TipsTextBox";
            this.TipsTextBox.Size = new System.Drawing.Size(625, 35);
            this.TipsTextBox.TabIndex = 29;
            // 
            // tipsTimer
            // 
            this.tipsTimer.Interval = 5000;
            this.tipsTimer.Tick += new System.EventHandler(this.tipsTimer_Tick);
            // 
            // savePresetFileDialog
            // 
            this.savePresetFileDialog.Filter = "Text|*.txt";
            // 
            // mainTaskControl
            // 
            this.mainTaskControl.Location = new System.Drawing.Point(0, 0);
            this.mainTaskControl.Name = "mainTaskControl";
            this.mainTaskControl.SelectedPreset = -1;
            this.mainTaskControl.SelectedTemplate = -1;
            this.mainTaskControl.SelectedText = -1;
            this.mainTaskControl.SelectedWorkSpace = -1;
            this.mainTaskControl.Size = new System.Drawing.Size(624, 440);
            this.mainTaskControl.TabIndex = 0;
            // 
            // ExEdPresets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 504);
            this.Controls.Add(this.mainTaskControl);
            this.Controls.Add(this.bottomPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExEdPresets";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ExEdPresets_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExEdPresets_FormClosing);
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.TextBox TipsTextBox;
        private System.Windows.Forms.Timer tipsTimer;
        private System.Windows.Forms.SaveFileDialog savePresetFileDialog;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button SaveAsButton;
        private System.Windows.Forms.Label SaveAsLabel;
        private System.Windows.Forms.TextBox SaveAsTextBox;
        private TaskControl mainTaskControl;
        private System.Windows.Forms.Button deleteButton;
    }
}