namespace Doorway_Studio
{
    partial class ImportTemplate
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
            this.selectFolderButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.encodingComboBox = new System.Windows.Forms.ComboBox();
            this.IDLabel = new System.Windows.Forms.Label();
            this.logContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tipsTimer = new System.Windows.Forms.Timer(this.components);
            this.TipsTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.encodingLabel = new System.Windows.Forms.Label();
            this.commTextBox = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.WSLabel = new System.Windows.Forms.Label();
            this.WSComboBox = new System.Windows.Forms.ComboBox();
            this.commLabel = new System.Windows.Forms.Label();
            this.templateFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.temlateFolderTextBox = new System.Windows.Forms.TextBox();
            this.logContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectFolderButton
            // 
            this.selectFolderButton.Location = new System.Drawing.Point(328, 146);
            this.selectFolderButton.Name = "selectFolderButton";
            this.selectFolderButton.Size = new System.Drawing.Size(75, 23);
            this.selectFolderButton.TabIndex = 2;
            this.selectFolderButton.Text = "...";
            this.selectFolderButton.UseVisualStyleBackColor = true;
            this.selectFolderButton.Click += new System.EventHandler(this.openTemplateButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(328, 92);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 38;
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // encodingComboBox
            // 
            this.encodingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.encodingComboBox.FormattingEnabled = true;
            this.encodingComboBox.Items.AddRange(new object[] {
            "Win-1251 (ANSI)",
            "UTF-8"});
            this.encodingComboBox.Location = new System.Drawing.Point(82, 121);
            this.encodingComboBox.Name = "encodingComboBox";
            this.encodingComboBox.Size = new System.Drawing.Size(129, 21);
            this.encodingComboBox.TabIndex = 1;
            // 
            // IDLabel
            // 
            this.IDLabel.AutoSize = true;
            this.IDLabel.Location = new System.Drawing.Point(328, 40);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(18, 13);
            this.IDLabel.TabIndex = 34;
            this.IDLabel.Text = "ID";
            // 
            // logContextMenuStrip
            // 
            this.logContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator2,
            this.clearToolStripMenuItem});
            this.logContextMenuStrip.Name = "keywordsContextMenuStrip";
            this.logContextMenuStrip.Size = new System.Drawing.Size(123, 104);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(119, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(119, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.clearToolStripMenuItem.Text = "Clear";
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
            this.TipsTextBox.Location = new System.Drawing.Point(0, 173);
            this.TipsTextBox.Multiline = true;
            this.TipsTextBox.Name = "TipsTextBox";
            this.TipsTextBox.Size = new System.Drawing.Size(412, 35);
            this.TipsTextBox.TabIndex = 39;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 40);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(0, 13);
            this.nameLabel.TabIndex = 32;
            // 
            // encodingLabel
            // 
            this.encodingLabel.AutoSize = true;
            this.encodingLabel.Location = new System.Drawing.Point(12, 124);
            this.encodingLabel.Name = "encodingLabel";
            this.encodingLabel.Size = new System.Drawing.Size(0, 13);
            this.encodingLabel.TabIndex = 0;
            // 
            // commTextBox
            // 
            this.commTextBox.Location = new System.Drawing.Point(82, 63);
            this.commTextBox.Multiline = true;
            this.commTextBox.Name = "commTextBox";
            this.commTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commTextBox.Size = new System.Drawing.Size(240, 52);
            this.commTextBox.TabIndex = 36;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(328, 63);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 37;
            this.OKButton.Text = "Ok";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(82, 37);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(240, 20);
            this.nameTextBox.TabIndex = 33;
            // 
            // WSLabel
            // 
            this.WSLabel.AutoSize = true;
            this.WSLabel.Location = new System.Drawing.Point(12, 13);
            this.WSLabel.Name = "WSLabel";
            this.WSLabel.Size = new System.Drawing.Size(64, 13);
            this.WSLabel.TabIndex = 41;
            this.WSLabel.Text = "WorkSpace";
            // 
            // WSComboBox
            // 
            this.WSComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WSComboBox.FormattingEnabled = true;
            this.WSComboBox.Location = new System.Drawing.Point(82, 10);
            this.WSComboBox.Name = "WSComboBox";
            this.WSComboBox.Size = new System.Drawing.Size(240, 21);
            this.WSComboBox.TabIndex = 42;
            this.WSComboBox.SelectedIndexChanged += new System.EventHandler(this.WSComboBox_SelectedIndexChanged);
            // 
            // commLabel
            // 
            this.commLabel.AutoSize = true;
            this.commLabel.Location = new System.Drawing.Point(12, 66);
            this.commLabel.Name = "commLabel";
            this.commLabel.Size = new System.Drawing.Size(0, 13);
            this.commLabel.TabIndex = 35;
            // 
            // temlateFolderTextBox
            // 
            this.temlateFolderTextBox.Location = new System.Drawing.Point(82, 148);
            this.temlateFolderTextBox.Name = "temlateFolderTextBox";
            this.temlateFolderTextBox.Size = new System.Drawing.Size(240, 20);
            this.temlateFolderTextBox.TabIndex = 43;
            // 
            // ImportTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 208);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.temlateFolderTextBox);
            this.Controls.Add(this.encodingComboBox);
            this.Controls.Add(this.TipsTextBox);
            this.Controls.Add(this.selectFolderButton);
            this.Controls.Add(this.IDLabel);
            this.Controls.Add(this.encodingLabel);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.commTextBox);
            this.Controls.Add(this.commLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.WSComboBox);
            this.Controls.Add(this.WSLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportTemplate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ImportTemplate_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImportTemplate_FormClosing);
            this.logContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectFolderButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox encodingComboBox;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.ContextMenuStrip logContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.Timer tipsTimer;
        private System.Windows.Forms.TextBox TipsTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label encodingLabel;
        private System.Windows.Forms.TextBox commTextBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label WSLabel;
        private System.Windows.Forms.ComboBox WSComboBox;
        private System.Windows.Forms.Label commLabel;
        private System.Windows.Forms.FolderBrowserDialog templateFolderBrowserDialog;
        private System.Windows.Forms.TextBox temlateFolderTextBox;
    }
}