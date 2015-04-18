namespace Doorway_Studio
{
    partial class ImportPreset
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.WSLabel = new System.Windows.Forms.Label();
            this.WSComboBox = new System.Windows.Forms.ComboBox();
            this.openFileButton = new System.Windows.Forms.Button();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.IDLabel = new System.Windows.Forms.Label();
            this.TipsTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.commTextBox = new System.Windows.Forms.TextBox();
            this.tipsTimer = new System.Windows.Forms.Timer(this.components);
            this.OKButton = new System.Windows.Forms.Button();
            this.openPresestFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.commLabel = new System.Windows.Forms.Label();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(328, 92);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 27;
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // WSLabel
            // 
            this.WSLabel.AutoSize = true;
            this.WSLabel.Location = new System.Drawing.Point(12, 13);
            this.WSLabel.Name = "WSLabel";
            this.WSLabel.Size = new System.Drawing.Size(64, 13);
            this.WSLabel.TabIndex = 30;
            this.WSLabel.Text = "WorkSpace";
            // 
            // WSComboBox
            // 
            this.WSComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WSComboBox.FormattingEnabled = true;
            this.WSComboBox.Location = new System.Drawing.Point(82, 10);
            this.WSComboBox.Name = "WSComboBox";
            this.WSComboBox.Size = new System.Drawing.Size(240, 21);
            this.WSComboBox.TabIndex = 31;
            this.WSComboBox.SelectedIndexChanged += new System.EventHandler(this.WSComboBox_SelectedIndexChanged);
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(328, 121);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(75, 23);
            this.openFileButton.TabIndex = 2;
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(82, 37);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(240, 20);
            this.nameTextBox.TabIndex = 22;
            // 
            // IDLabel
            // 
            this.IDLabel.AutoSize = true;
            this.IDLabel.Location = new System.Drawing.Point(328, 40);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(18, 13);
            this.IDLabel.TabIndex = 23;
            this.IDLabel.Text = "ID";
            // 
            // TipsTextBox
            // 
            this.TipsTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.TipsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TipsTextBox.Enabled = false;
            this.TipsTextBox.Location = new System.Drawing.Point(0, 148);
            this.TipsTextBox.Multiline = true;
            this.TipsTextBox.Name = "TipsTextBox";
            this.TipsTextBox.Size = new System.Drawing.Size(412, 35);
            this.TipsTextBox.TabIndex = 28;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 40);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(0, 13);
            this.nameLabel.TabIndex = 21;
            // 
            // commTextBox
            // 
            this.commTextBox.Location = new System.Drawing.Point(82, 63);
            this.commTextBox.Multiline = true;
            this.commTextBox.Name = "commTextBox";
            this.commTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commTextBox.Size = new System.Drawing.Size(240, 52);
            this.commTextBox.TabIndex = 25;
            // 
            // tipsTimer
            // 
            this.tipsTimer.Interval = 5000;
            this.tipsTimer.Tick += new System.EventHandler(this.tipsTimer_Tick);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(328, 63);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 26;
            this.OKButton.Text = "Ok";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // openPresestFileDialog
            // 
            this.openPresestFileDialog.Filter = "Text|*.txt";
            // 
            // commLabel
            // 
            this.commLabel.AutoSize = true;
            this.commLabel.Location = new System.Drawing.Point(12, 66);
            this.commLabel.Name = "commLabel";
            this.commLabel.Size = new System.Drawing.Size(0, 13);
            this.commLabel.TabIndex = 24;
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(15, 121);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(307, 20);
            this.pathTextBox.TabIndex = 32;
            // 
            // ImportPreset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 183);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.WSLabel);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.WSComboBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.IDLabel);
            this.Controls.Add(this.TipsTextBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.commTextBox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.commLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportPreset";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ImportPreset_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImportPreset_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label WSLabel;
        private System.Windows.Forms.ComboBox WSComboBox;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.TextBox TipsTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox commTextBox;
        private System.Windows.Forms.Timer tipsTimer;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.OpenFileDialog openPresestFileDialog;
        private System.Windows.Forms.Label commLabel;
        private System.Windows.Forms.TextBox pathTextBox;

    }
}