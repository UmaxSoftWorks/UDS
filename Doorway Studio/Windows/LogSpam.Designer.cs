namespace Doorway_Studio
{
    partial class LogSpam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogSpam));
            this.tipsTimer = new System.Windows.Forms.Timer(this.components);
            this.TipsTextBox = new System.Windows.Forms.TextBox();
            this.logContentTextBox = new System.Windows.Forms.TextBox();
            this.mainContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveLogSpamFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.logTabPage = new System.Windows.Forms.TabPage();
            this.logContentGroupBox = new System.Windows.Forms.GroupBox();
            this.logDoorwaysGroupBox = new System.Windows.Forms.GroupBox();
            this.logDoorwaysTreeView = new System.Windows.Forms.TreeView();
            this.mainImageList = new System.Windows.Forms.ImageList(this.components);
            this.spamTabPage = new System.Windows.Forms.TabPage();
            this.spamContentGroupBox = new System.Windows.Forms.GroupBox();
            this.spamContentTextBox = new System.Windows.Forms.TextBox();
            this.spamDoorwaysGroupBox = new System.Windows.Forms.GroupBox();
            this.spamDoorwaysTreeView = new System.Windows.Forms.TreeView();
            this.mainContextMenuStrip.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.logTabPage.SuspendLayout();
            this.logContentGroupBox.SuspendLayout();
            this.logDoorwaysGroupBox.SuspendLayout();
            this.spamTabPage.SuspendLayout();
            this.spamContentGroupBox.SuspendLayout();
            this.spamDoorwaysGroupBox.SuspendLayout();
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
            this.TipsTextBox.Location = new System.Drawing.Point(0, 365);
            this.TipsTextBox.Multiline = true;
            this.TipsTextBox.Name = "TipsTextBox";
            this.TipsTextBox.Size = new System.Drawing.Size(643, 35);
            this.TipsTextBox.TabIndex = 9;
            // 
            // logContentTextBox
            // 
            this.logContentTextBox.ContextMenuStrip = this.mainContextMenuStrip;
            this.logContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logContentTextBox.Location = new System.Drawing.Point(3, 16);
            this.logContentTextBox.Multiline = true;
            this.logContentTextBox.Name = "logContentTextBox";
            this.logContentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logContentTextBox.Size = new System.Drawing.Size(473, 314);
            this.logContentTextBox.TabIndex = 10;
            // 
            // mainContextMenuStrip
            // 
            this.mainContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToolStripMenuItem,
            this.toolStripSeparator2,
            this.selectAllToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.mainContextMenuStrip.Name = "mainContextMenuStrip";
            this.mainContextMenuStrip.Size = new System.Drawing.Size(123, 104);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
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
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(119, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // SaveLogSpamFileDialog
            // 
            this.SaveLogSpamFileDialog.Filter = "Text|*.txt";
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.logTabPage);
            this.mainTabControl.Controls.Add(this.spamTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(643, 365);
            this.mainTabControl.TabIndex = 11;
            // 
            // logTabPage
            // 
            this.logTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.logTabPage.Controls.Add(this.logContentGroupBox);
            this.logTabPage.Controls.Add(this.logDoorwaysGroupBox);
            this.logTabPage.Location = new System.Drawing.Point(4, 22);
            this.logTabPage.Name = "logTabPage";
            this.logTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.logTabPage.Size = new System.Drawing.Size(635, 339);
            this.logTabPage.TabIndex = 0;
            this.logTabPage.Text = "Log";
            // 
            // logContentGroupBox
            // 
            this.logContentGroupBox.Controls.Add(this.logContentTextBox);
            this.logContentGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logContentGroupBox.Location = new System.Drawing.Point(153, 3);
            this.logContentGroupBox.Name = "logContentGroupBox";
            this.logContentGroupBox.Size = new System.Drawing.Size(479, 333);
            this.logContentGroupBox.TabIndex = 12;
            this.logContentGroupBox.TabStop = false;
            // 
            // logDoorwaysGroupBox
            // 
            this.logDoorwaysGroupBox.Controls.Add(this.logDoorwaysTreeView);
            this.logDoorwaysGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.logDoorwaysGroupBox.Location = new System.Drawing.Point(3, 3);
            this.logDoorwaysGroupBox.Name = "logDoorwaysGroupBox";
            this.logDoorwaysGroupBox.Size = new System.Drawing.Size(150, 333);
            this.logDoorwaysGroupBox.TabIndex = 13;
            this.logDoorwaysGroupBox.TabStop = false;
            // 
            // logDoorwaysTreeView
            // 
            this.logDoorwaysTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logDoorwaysTreeView.ImageIndex = 0;
            this.logDoorwaysTreeView.ImageList = this.mainImageList;
            this.logDoorwaysTreeView.Location = new System.Drawing.Point(3, 16);
            this.logDoorwaysTreeView.Name = "logDoorwaysTreeView";
            this.logDoorwaysTreeView.SelectedImageIndex = 0;
            this.logDoorwaysTreeView.Size = new System.Drawing.Size(144, 314);
            this.logDoorwaysTreeView.TabIndex = 0;
            this.logDoorwaysTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.logDoorwaysTreeView_AfterSelect);
            // 
            // mainImageList
            // 
            this.mainImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("mainImageList.ImageStream")));
            this.mainImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.mainImageList.Images.SetKeyName(0, "WorkSpace.png");
            this.mainImageList.Images.SetKeyName(1, "Template.png");
            // 
            // spamTabPage
            // 
            this.spamTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.spamTabPage.Controls.Add(this.spamContentGroupBox);
            this.spamTabPage.Controls.Add(this.spamDoorwaysGroupBox);
            this.spamTabPage.Location = new System.Drawing.Point(4, 22);
            this.spamTabPage.Name = "spamTabPage";
            this.spamTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.spamTabPage.Size = new System.Drawing.Size(635, 339);
            this.spamTabPage.TabIndex = 1;
            this.spamTabPage.Text = "Spam";
            // 
            // spamContentGroupBox
            // 
            this.spamContentGroupBox.Controls.Add(this.spamContentTextBox);
            this.spamContentGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spamContentGroupBox.Location = new System.Drawing.Point(153, 3);
            this.spamContentGroupBox.Name = "spamContentGroupBox";
            this.spamContentGroupBox.Size = new System.Drawing.Size(479, 333);
            this.spamContentGroupBox.TabIndex = 15;
            this.spamContentGroupBox.TabStop = false;
            // 
            // spamContentTextBox
            // 
            this.spamContentTextBox.ContextMenuStrip = this.mainContextMenuStrip;
            this.spamContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spamContentTextBox.Location = new System.Drawing.Point(3, 16);
            this.spamContentTextBox.Multiline = true;
            this.spamContentTextBox.Name = "spamContentTextBox";
            this.spamContentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.spamContentTextBox.Size = new System.Drawing.Size(473, 314);
            this.spamContentTextBox.TabIndex = 13;
            // 
            // spamDoorwaysGroupBox
            // 
            this.spamDoorwaysGroupBox.Controls.Add(this.spamDoorwaysTreeView);
            this.spamDoorwaysGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.spamDoorwaysGroupBox.Location = new System.Drawing.Point(3, 3);
            this.spamDoorwaysGroupBox.Name = "spamDoorwaysGroupBox";
            this.spamDoorwaysGroupBox.Size = new System.Drawing.Size(150, 333);
            this.spamDoorwaysGroupBox.TabIndex = 14;
            this.spamDoorwaysGroupBox.TabStop = false;
            // 
            // spamDoorwaysTreeView
            // 
            this.spamDoorwaysTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spamDoorwaysTreeView.ImageIndex = 0;
            this.spamDoorwaysTreeView.ImageList = this.mainImageList;
            this.spamDoorwaysTreeView.Location = new System.Drawing.Point(3, 16);
            this.spamDoorwaysTreeView.Name = "spamDoorwaysTreeView";
            this.spamDoorwaysTreeView.SelectedImageIndex = 0;
            this.spamDoorwaysTreeView.Size = new System.Drawing.Size(144, 314);
            this.spamDoorwaysTreeView.TabIndex = 0;
            this.spamDoorwaysTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.spamDoorwaysTreeView_AfterSelect);
            // 
            // LogSpam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 400);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.TipsTextBox);
            this.Name = "LogSpam";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "/Spam: ";
            this.Load += new System.EventHandler(this.Log_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Log_FormClosing);
            this.mainContextMenuStrip.ResumeLayout(false);
            this.mainTabControl.ResumeLayout(false);
            this.logTabPage.ResumeLayout(false);
            this.logContentGroupBox.ResumeLayout(false);
            this.logContentGroupBox.PerformLayout();
            this.logDoorwaysGroupBox.ResumeLayout(false);
            this.spamTabPage.ResumeLayout(false);
            this.spamContentGroupBox.ResumeLayout(false);
            this.spamContentGroupBox.PerformLayout();
            this.spamDoorwaysGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tipsTimer;
        private System.Windows.Forms.TextBox TipsTextBox;
        private System.Windows.Forms.TextBox logContentTextBox;
        private System.Windows.Forms.ContextMenuStrip mainContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog SaveLogSpamFileDialog;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage logTabPage;
        private System.Windows.Forms.TabPage spamTabPage;
        private System.Windows.Forms.TextBox spamContentTextBox;
        private System.Windows.Forms.GroupBox logContentGroupBox;
        private System.Windows.Forms.GroupBox logDoorwaysGroupBox;
        private System.Windows.Forms.TreeView logDoorwaysTreeView;
        private System.Windows.Forms.ImageList mainImageList;
        private System.Windows.Forms.GroupBox spamContentGroupBox;
        private System.Windows.Forms.GroupBox spamDoorwaysGroupBox;
        private System.Windows.Forms.TreeView spamDoorwaysTreeView;
    }
}