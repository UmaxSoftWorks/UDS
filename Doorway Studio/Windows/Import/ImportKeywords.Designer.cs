namespace Doorway_Studio
{
    partial class ImportKeywords
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
            this.TipsTextBox = new System.Windows.Forms.TextBox();
            this.IDLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.commTextBox = new System.Windows.Forms.TextBox();
            this.tipsTimer = new System.Windows.Forms.Timer(this.components);
            this.OKButton = new System.Windows.Forms.Button();
            this.commLabel = new System.Windows.Forms.Label();
            this.keywordsGroupBox = new System.Windows.Forms.GroupBox();
            this.keywordsTextBox = new System.Windows.Forms.TextBox();
            this.keywordsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileButton = new System.Windows.Forms.Button();
            this.encodingComboBox = new System.Windows.Forms.ComboBox();
            this.encodingLabel = new System.Windows.Forms.Label();
            this.WSLabel = new System.Windows.Forms.Label();
            this.WSComboBox = new System.Windows.Forms.ComboBox();
            this.openKeywordsFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.keywordsGroupBox.SuspendLayout();
            this.keywordsContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(328, 92);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // TipsTextBox
            // 
            this.TipsTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.TipsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TipsTextBox.Enabled = false;
            this.TipsTextBox.Location = new System.Drawing.Point(0, 390);
            this.TipsTextBox.Multiline = true;
            this.TipsTextBox.Name = "TipsTextBox";
            this.TipsTextBox.Size = new System.Drawing.Size(412, 35);
            this.TipsTextBox.TabIndex = 16;
            // 
            // IDLabel
            // 
            this.IDLabel.AutoSize = true;
            this.IDLabel.Location = new System.Drawing.Point(328, 40);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(18, 13);
            this.IDLabel.TabIndex = 11;
            this.IDLabel.Text = "ID";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(82, 37);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(240, 20);
            this.nameTextBox.TabIndex = 10;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 40);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(0, 13);
            this.nameLabel.TabIndex = 9;
            // 
            // commTextBox
            // 
            this.commTextBox.Location = new System.Drawing.Point(82, 63);
            this.commTextBox.Multiline = true;
            this.commTextBox.Name = "commTextBox";
            this.commTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commTextBox.Size = new System.Drawing.Size(240, 52);
            this.commTextBox.TabIndex = 13;
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
            this.OKButton.TabIndex = 14;
            this.OKButton.Text = "Ok";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // commLabel
            // 
            this.commLabel.AutoSize = true;
            this.commLabel.Location = new System.Drawing.Point(12, 66);
            this.commLabel.Name = "commLabel";
            this.commLabel.Size = new System.Drawing.Size(0, 13);
            this.commLabel.TabIndex = 12;
            // 
            // keywordsGroupBox
            // 
            this.keywordsGroupBox.Controls.Add(this.keywordsTextBox);
            this.keywordsGroupBox.Controls.Add(this.openFileButton);
            this.keywordsGroupBox.Controls.Add(this.encodingComboBox);
            this.keywordsGroupBox.Controls.Add(this.encodingLabel);
            this.keywordsGroupBox.Location = new System.Drawing.Point(12, 121);
            this.keywordsGroupBox.Name = "keywordsGroupBox";
            this.keywordsGroupBox.Size = new System.Drawing.Size(391, 263);
            this.keywordsGroupBox.TabIndex = 17;
            this.keywordsGroupBox.TabStop = false;
            // 
            // keywordsTextBox
            // 
            this.keywordsTextBox.ContextMenuStrip = this.keywordsContextMenuStrip;
            this.keywordsTextBox.Location = new System.Drawing.Point(6, 46);
            this.keywordsTextBox.Multiline = true;
            this.keywordsTextBox.Name = "keywordsTextBox";
            this.keywordsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.keywordsTextBox.Size = new System.Drawing.Size(379, 211);
            this.keywordsTextBox.TabIndex = 4;
            // 
            // keywordsContextMenuStrip
            // 
            this.keywordsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator2,
            this.clearToolStripMenuItem});
            this.keywordsContextMenuStrip.Name = "keywordsContextMenuStrip";
            this.keywordsContextMenuStrip.Size = new System.Drawing.Size(68, 104);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(64, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(64, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(316, 17);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(69, 23);
            this.openFileButton.TabIndex = 2;
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // encodingComboBox
            // 
            this.encodingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.encodingComboBox.FormattingEnabled = true;
            this.encodingComboBox.Items.AddRange(new object[] {
            "Win-1251 (ANSI)",
            "UTF-8"});
            this.encodingComboBox.Location = new System.Drawing.Point(70, 19);
            this.encodingComboBox.Name = "encodingComboBox";
            this.encodingComboBox.Size = new System.Drawing.Size(129, 21);
            this.encodingComboBox.TabIndex = 1;
            // 
            // encodingLabel
            // 
            this.encodingLabel.AutoSize = true;
            this.encodingLabel.Location = new System.Drawing.Point(6, 22);
            this.encodingLabel.Name = "encodingLabel";
            this.encodingLabel.Size = new System.Drawing.Size(0, 13);
            this.encodingLabel.TabIndex = 0;
            // 
            // WSLabel
            // 
            this.WSLabel.AutoSize = true;
            this.WSLabel.Location = new System.Drawing.Point(12, 13);
            this.WSLabel.Name = "WSLabel";
            this.WSLabel.Size = new System.Drawing.Size(64, 13);
            this.WSLabel.TabIndex = 19;
            this.WSLabel.Text = "WorkSpace";
            // 
            // WSComboBox
            // 
            this.WSComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WSComboBox.FormattingEnabled = true;
            this.WSComboBox.Location = new System.Drawing.Point(82, 10);
            this.WSComboBox.Name = "WSComboBox";
            this.WSComboBox.Size = new System.Drawing.Size(240, 21);
            this.WSComboBox.TabIndex = 20;
            this.WSComboBox.SelectedIndexChanged += new System.EventHandler(this.WSComboBox_SelectedIndexChanged);
            // 
            // openKeywordsFileDialog
            // 
            this.openKeywordsFileDialog.Filter = "Text|*.txt";
            // 
            // ImportKeywords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 425);
            this.Controls.Add(this.TipsTextBox);
            this.Controls.Add(this.WSComboBox);
            this.Controls.Add(this.keywordsGroupBox);
            this.Controls.Add(this.WSLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.IDLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.commTextBox);
            this.Controls.Add(this.commLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportKeywords";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ImportKeywords_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImportKeywords_FormClosing);
            this.keywordsGroupBox.ResumeLayout(false);
            this.keywordsGroupBox.PerformLayout();
            this.keywordsContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox TipsTextBox;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox commTextBox;
        private System.Windows.Forms.Timer tipsTimer;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label commLabel;
        private System.Windows.Forms.GroupBox keywordsGroupBox;
        private System.Windows.Forms.TextBox keywordsTextBox;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.ComboBox encodingComboBox;
        private System.Windows.Forms.Label encodingLabel;
        private System.Windows.Forms.ContextMenuStrip keywordsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.Label WSLabel;
        private System.Windows.Forms.ComboBox WSComboBox;
        private System.Windows.Forms.OpenFileDialog openKeywordsFileDialog;
    }
}