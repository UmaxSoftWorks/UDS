namespace Doorway_Studio
{
    partial class ExEdTemplates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExEdTemplates));
            this.templatesTreeGroupBox = new System.Windows.Forms.GroupBox();
            this.templatesTreeView = new System.Windows.Forms.TreeView();
            this.treeKeywordImageList = new System.Windows.Forms.ImageList(this.components);
            this.exportButton = new System.Windows.Forms.Button();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveContentFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tipsTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteButton = new System.Windows.Forms.Button();
            this.TipsTextBox = new System.Windows.Forms.TextBox();
            this.contentTextBox = new System.Windows.Forms.TextBox();
            this.contentGroupBox = new System.Windows.Forms.GroupBox();
            this.contentFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.applyButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.templatesTreeGroupBox.SuspendLayout();
            this.contentContextMenuStrip.SuspendLayout();
            this.contentGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // templatesTreeGroupBox
            // 
            this.templatesTreeGroupBox.Controls.Add(this.templatesTreeView);
            this.templatesTreeGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.templatesTreeGroupBox.Location = new System.Drawing.Point(0, 0);
            this.templatesTreeGroupBox.Name = "templatesTreeGroupBox";
            this.templatesTreeGroupBox.Size = new System.Drawing.Size(186, 326);
            this.templatesTreeGroupBox.TabIndex = 37;
            this.templatesTreeGroupBox.TabStop = false;
            // 
            // templatesTreeView
            // 
            this.templatesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templatesTreeView.ImageIndex = 0;
            this.templatesTreeView.ImageList = this.treeKeywordImageList;
            this.templatesTreeView.Location = new System.Drawing.Point(3, 16);
            this.templatesTreeView.Name = "templatesTreeView";
            this.templatesTreeView.SelectedImageIndex = 0;
            this.templatesTreeView.Size = new System.Drawing.Size(180, 307);
            this.templatesTreeView.TabIndex = 31;
            this.templatesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.templatesTreeView_AfterSelect);
            // 
            // treeKeywordImageList
            // 
            this.treeKeywordImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeKeywordImageList.ImageStream")));
            this.treeKeywordImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeKeywordImageList.Images.SetKeyName(0, "WorkSpace.png");
            this.treeKeywordImageList.Images.SetKeyName(1, "Template.png");
            this.treeKeywordImageList.Images.SetKeyName(2, "Item.png");
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(186, 3);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(117, 23);
            this.exportButton.TabIndex = 2;
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(390, 3);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 33;
            this.OKButton.Text = "Ok";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(471, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 34;
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveContentFileDialog
            // 
            this.saveContentFileDialog.Filter = "Text|*.txt";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // tipsTimer
            // 
            this.tipsTimer.Interval = 5000;
            this.tipsTimer.Tick += new System.EventHandler(this.tipsTimer_Tick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(64, 6);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // contentContextMenuStrip
            // 
            this.contentContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator2,
            this.clearToolStripMenuItem});
            this.contentContextMenuStrip.Name = "keywordsContextMenuStrip";
            this.contentContextMenuStrip.Size = new System.Drawing.Size(68, 104);
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
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(3, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(183, 23);
            this.deleteButton.TabIndex = 38;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
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
            this.TipsTextBox.TabIndex = 35;
            // 
            // contentTextBox
            // 
            this.contentTextBox.ContextMenuStrip = this.contentContextMenuStrip;
            this.contentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentTextBox.Location = new System.Drawing.Point(3, 16);
            this.contentTextBox.Multiline = true;
            this.contentTextBox.Name = "contentTextBox";
            this.contentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.contentTextBox.Size = new System.Drawing.Size(360, 307);
            this.contentTextBox.TabIndex = 4;
            // 
            // contentGroupBox
            // 
            this.contentGroupBox.Controls.Add(this.contentTextBox);
            this.contentGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentGroupBox.Location = new System.Drawing.Point(186, 0);
            this.contentGroupBox.Name = "contentGroupBox";
            this.contentGroupBox.Size = new System.Drawing.Size(366, 326);
            this.contentGroupBox.TabIndex = 36;
            this.contentGroupBox.TabStop = false;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(309, 3);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 39;
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.deleteButton);
            this.panel1.Controls.Add(this.exportButton);
            this.panel1.Controls.Add(this.applyButton);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.OKButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 326);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 32);
            this.panel1.TabIndex = 40;
            // 
            // ExEdTemplates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 393);
            this.Controls.Add(this.contentGroupBox);
            this.Controls.Add(this.templatesTreeGroupBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TipsTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "ExEdTemplates";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ExEdTemplates_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExEdTemplates_FormClosing);
            this.templatesTreeGroupBox.ResumeLayout(false);
            this.contentContextMenuStrip.ResumeLayout(false);
            this.contentGroupBox.ResumeLayout(false);
            this.contentGroupBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox templatesTreeGroupBox;
        private System.Windows.Forms.TreeView templatesTreeView;
        private System.Windows.Forms.ImageList treeKeywordImageList;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.SaveFileDialog saveContentFileDialog;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.Timer tipsTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contentContextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.TextBox TipsTextBox;
        private System.Windows.Forms.TextBox contentTextBox;
        private System.Windows.Forms.GroupBox contentGroupBox;
        private System.Windows.Forms.FolderBrowserDialog contentFolderBrowserDialog;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Panel panel1;
    }
}