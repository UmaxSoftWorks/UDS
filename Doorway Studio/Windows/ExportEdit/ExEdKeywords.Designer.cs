namespace Doorway_Studio
{
    partial class ExEdKeywords
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExEdKeywords));
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keywordsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keywordsTextBox = new System.Windows.Forms.TextBox();
            this.TipsTextBox = new System.Windows.Forms.TextBox();
            this.keywordsGroupBox = new System.Windows.Forms.GroupBox();
            this.tipsTimer = new System.Windows.Forms.Timer(this.components);
            this.cancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.keywordsTreeGroupBox = new System.Windows.Forms.GroupBox();
            this.keywordsTreeView = new System.Windows.Forms.TreeView();
            this.treeKeywordImageList = new System.Windows.Forms.ImageList(this.components);
            this.exportGroupBox = new System.Windows.Forms.GroupBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.encodingComboBox = new System.Windows.Forms.ComboBox();
            this.encodingLabel = new System.Windows.Forms.Label();
            this.saveKeywordsFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.deleteButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.keywordsContextMenuStrip.SuspendLayout();
            this.keywordsGroupBox.SuspendLayout();
            this.keywordsTreeGroupBox.SuspendLayout();
            this.exportGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
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
            // keywordsTextBox
            // 
            this.keywordsTextBox.ContextMenuStrip = this.keywordsContextMenuStrip;
            this.keywordsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keywordsTextBox.Location = new System.Drawing.Point(3, 16);
            this.keywordsTextBox.Multiline = true;
            this.keywordsTextBox.Name = "keywordsTextBox";
            this.keywordsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.keywordsTextBox.Size = new System.Drawing.Size(369, 281);
            this.keywordsTextBox.TabIndex = 4;
            // 
            // TipsTextBox
            // 
            this.TipsTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.TipsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TipsTextBox.Enabled = false;
            this.TipsTextBox.Location = new System.Drawing.Point(0, 358);
            this.TipsTextBox.Multiline = true;
            this.TipsTextBox.Name = "TipsTextBox";
            this.TipsTextBox.Size = new System.Drawing.Size(552, 35);
            this.TipsTextBox.TabIndex = 28;
            // 
            // keywordsGroupBox
            // 
            this.keywordsGroupBox.Controls.Add(this.keywordsTextBox);
            this.keywordsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keywordsGroupBox.Location = new System.Drawing.Point(177, 0);
            this.keywordsGroupBox.Name = "keywordsGroupBox";
            this.keywordsGroupBox.Size = new System.Drawing.Size(375, 300);
            this.keywordsGroupBox.TabIndex = 29;
            this.keywordsGroupBox.TabStop = false;
            // 
            // tipsTimer
            // 
            this.tipsTimer.Interval = 5000;
            this.tipsTimer.Tick += new System.EventHandler(this.tipsTimer_Tick);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(462, 32);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 27;
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(462, 3);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 26;
            this.OKButton.Text = "Ok";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // keywordsTreeGroupBox
            // 
            this.keywordsTreeGroupBox.Controls.Add(this.keywordsTreeView);
            this.keywordsTreeGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.keywordsTreeGroupBox.Location = new System.Drawing.Point(0, 0);
            this.keywordsTreeGroupBox.Name = "keywordsTreeGroupBox";
            this.keywordsTreeGroupBox.Size = new System.Drawing.Size(177, 300);
            this.keywordsTreeGroupBox.TabIndex = 30;
            this.keywordsTreeGroupBox.TabStop = false;
            // 
            // keywordsTreeView
            // 
            this.keywordsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keywordsTreeView.ImageIndex = 0;
            this.keywordsTreeView.ImageList = this.treeKeywordImageList;
            this.keywordsTreeView.Location = new System.Drawing.Point(3, 16);
            this.keywordsTreeView.Name = "keywordsTreeView";
            this.keywordsTreeView.SelectedImageIndex = 0;
            this.keywordsTreeView.Size = new System.Drawing.Size(171, 281);
            this.keywordsTreeView.TabIndex = 31;
            this.keywordsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.keywordsTreeView_AfterSelect);
            // 
            // treeKeywordImageList
            // 
            this.treeKeywordImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeKeywordImageList.ImageStream")));
            this.treeKeywordImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeKeywordImageList.Images.SetKeyName(0, "WorkSpace.png");
            this.treeKeywordImageList.Images.SetKeyName(1, "Item.png");
            // 
            // exportGroupBox
            // 
            this.exportGroupBox.Controls.Add(this.exportButton);
            this.exportGroupBox.Controls.Add(this.encodingComboBox);
            this.exportGroupBox.Controls.Add(this.encodingLabel);
            this.exportGroupBox.Location = new System.Drawing.Point(183, 3);
            this.exportGroupBox.Name = "exportGroupBox";
            this.exportGroupBox.Size = new System.Drawing.Size(273, 52);
            this.exportGroupBox.TabIndex = 5;
            this.exportGroupBox.TabStop = false;
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(191, 18);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 2;
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // encodingComboBox
            // 
            this.encodingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.encodingComboBox.FormattingEnabled = true;
            this.encodingComboBox.Items.AddRange(new object[] {
            "Win-1251 (ANSI)",
            "UTF-8"});
            this.encodingComboBox.Location = new System.Drawing.Point(74, 20);
            this.encodingComboBox.Name = "encodingComboBox";
            this.encodingComboBox.Size = new System.Drawing.Size(111, 21);
            this.encodingComboBox.TabIndex = 1;
            // 
            // encodingLabel
            // 
            this.encodingLabel.AutoSize = true;
            this.encodingLabel.Location = new System.Drawing.Point(6, 23);
            this.encodingLabel.Name = "encodingLabel";
            this.encodingLabel.Size = new System.Drawing.Size(0, 13);
            this.encodingLabel.TabIndex = 0;
            // 
            // saveKeywordsFileDialog
            // 
            this.saveKeywordsFileDialog.Filter = "Text|*.txt";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(3, 21);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(174, 34);
            this.deleteButton.TabIndex = 31;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.deleteButton);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.OKButton);
            this.panel1.Controls.Add(this.exportGroupBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 300);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 58);
            this.panel1.TabIndex = 32;
            // 
            // ExEdKeywords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 393);
            this.Controls.Add(this.keywordsGroupBox);
            this.Controls.Add(this.keywordsTreeGroupBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TipsTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "ExEdKeywords";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ExEdKeywords_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExEdKeywords_FormClosing);
            this.keywordsContextMenuStrip.ResumeLayout(false);
            this.keywordsGroupBox.ResumeLayout(false);
            this.keywordsGroupBox.PerformLayout();
            this.keywordsTreeGroupBox.ResumeLayout(false);
            this.exportGroupBox.ResumeLayout(false);
            this.exportGroupBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip keywordsContextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.TextBox keywordsTextBox;
        private System.Windows.Forms.TextBox TipsTextBox;
        private System.Windows.Forms.GroupBox keywordsGroupBox;
        private System.Windows.Forms.Timer tipsTimer;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.GroupBox keywordsTreeGroupBox;
        private System.Windows.Forms.TreeView keywordsTreeView;
        private System.Windows.Forms.GroupBox exportGroupBox;
        private System.Windows.Forms.ComboBox encodingComboBox;
        private System.Windows.Forms.Label encodingLabel;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.SaveFileDialog saveKeywordsFileDialog;
        private System.Windows.Forms.ImageList treeKeywordImageList;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Panel panel1;
    }
}