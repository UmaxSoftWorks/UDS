namespace Automator
{
    partial class Main
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
            this.mainPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textButton = new System.Windows.Forms.Button();
            this.keywordsButton = new System.Windows.Forms.Button();
            this.KTUsageComboBox = new System.Windows.Forms.ComboBox();
            this.KTUsageLabel = new System.Windows.Forms.Label();
            this.tagsButton = new System.Windows.Forms.Button();
            this.tokensButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.ftpButton = new System.Windows.Forms.Button();
            this.urlsButton = new System.Windows.Forms.Button();
            this.tasksLabel = new System.Windows.Forms.Label();
            this.tasksNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.textCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.textLabel = new System.Windows.Forms.Label();
            this.keywordsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.keywordsLabel = new System.Windows.Forms.Label();
            this.templateComboBox = new System.Windows.Forms.ComboBox();
            this.templateLabel = new System.Windows.Forms.Label();
            this.presetComboBox = new System.Windows.Forms.ComboBox();
            this.presetLabel = new System.Windows.Forms.Label();
            this.workSpaceComboBox = new System.Windows.Forms.ComboBox();
            this.workSpaceLabel = new System.Windows.Forms.Label();
            this.mainMenuStrip.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tasksNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPropertyGrid
            // 
            this.mainPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.mainPropertyGrid.Name = "mainPropertyGrid";
            this.mainPropertyGrid.Size = new System.Drawing.Size(519, 438);
            this.mainPropertyGrid.TabIndex = 0;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(784, 24);
            this.mainMenuStrip.TabIndex = 1;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItemClick);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(100, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItemExitToolStripMenuItemClick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItemClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textButton);
            this.splitContainer1.Panel1.Controls.Add(this.keywordsButton);
            this.splitContainer1.Panel1.Controls.Add(this.KTUsageComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.KTUsageLabel);
            this.splitContainer1.Panel1.Controls.Add(this.tagsButton);
            this.splitContainer1.Panel1.Controls.Add(this.tokensButton);
            this.splitContainer1.Panel1.Controls.Add(this.startButton);
            this.splitContainer1.Panel1.Controls.Add(this.ftpButton);
            this.splitContainer1.Panel1.Controls.Add(this.urlsButton);
            this.splitContainer1.Panel1.Controls.Add(this.tasksLabel);
            this.splitContainer1.Panel1.Controls.Add(this.tasksNumericUpDown);
            this.splitContainer1.Panel1.Controls.Add(this.textCheckedListBox);
            this.splitContainer1.Panel1.Controls.Add(this.textLabel);
            this.splitContainer1.Panel1.Controls.Add(this.keywordsCheckedListBox);
            this.splitContainer1.Panel1.Controls.Add(this.keywordsLabel);
            this.splitContainer1.Panel1.Controls.Add(this.templateComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.templateLabel);
            this.splitContainer1.Panel1.Controls.Add(this.presetComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.presetLabel);
            this.splitContainer1.Panel1.Controls.Add(this.workSpaceComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.workSpaceLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mainPropertyGrid);
            this.splitContainer1.Size = new System.Drawing.Size(784, 438);
            this.splitContainer1.SplitterDistance = 261;
            this.splitContainer1.TabIndex = 2;
            // 
            // textButton
            // 
            this.textButton.Location = new System.Drawing.Point(6, 233);
            this.textButton.Name = "textButton";
            this.textButton.Size = new System.Drawing.Size(61, 23);
            this.textButton.TabIndex = 20;
            this.textButton.Text = "V/X";
            this.textButton.UseVisualStyleBackColor = true;
            this.textButton.Click += new System.EventHandler(this.textButton_Click);
            // 
            // keywordsButton
            // 
            this.keywordsButton.Location = new System.Drawing.Point(6, 104);
            this.keywordsButton.Name = "keywordsButton";
            this.keywordsButton.Size = new System.Drawing.Size(61, 23);
            this.keywordsButton.TabIndex = 19;
            this.keywordsButton.Text = "V/X";
            this.keywordsButton.UseVisualStyleBackColor = true;
            this.keywordsButton.Click += new System.EventHandler(this.keywordsButton_Click);
            // 
            // KTUsageComboBox
            // 
            this.KTUsageComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.KTUsageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KTUsageComboBox.FormattingEnabled = true;
            this.KTUsageComboBox.Location = new System.Drawing.Point(6, 357);
            this.KTUsageComboBox.Name = "KTUsageComboBox";
            this.KTUsageComboBox.Size = new System.Drawing.Size(123, 21);
            this.KTUsageComboBox.TabIndex = 18;
            // 
            // KTUsageLabel
            // 
            this.KTUsageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.KTUsageLabel.AutoSize = true;
            this.KTUsageLabel.Location = new System.Drawing.Point(3, 333);
            this.KTUsageLabel.Name = "KTUsageLabel";
            this.KTUsageLabel.Size = new System.Drawing.Size(118, 13);
            this.KTUsageLabel.TabIndex = 17;
            this.KTUsageLabel.Text = "Keywords && Text usage";
            // 
            // tagsButton
            // 
            this.tagsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tagsButton.Location = new System.Drawing.Point(135, 357);
            this.tagsButton.Name = "tagsButton";
            this.tagsButton.Size = new System.Drawing.Size(123, 23);
            this.tagsButton.TabIndex = 16;
            this.tagsButton.Text = "Tags";
            this.tagsButton.UseVisualStyleBackColor = true;
            this.tagsButton.Click += new System.EventHandler(this.tagsButtonClick);
            // 
            // tokensButton
            // 
            this.tokensButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tokensButton.Location = new System.Drawing.Point(135, 386);
            this.tokensButton.Name = "tokensButton";
            this.tokensButton.Size = new System.Drawing.Size(123, 23);
            this.tokensButton.TabIndex = 15;
            this.tokensButton.Text = "Tokens";
            this.tokensButton.UseVisualStyleBackColor = true;
            this.tokensButton.Click += new System.EventHandler(this.tokensButtonClick);
            // 
            // startButton
            // 
            this.startButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.startButton.Location = new System.Drawing.Point(0, 415);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(261, 23);
            this.startButton.TabIndex = 14;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButtonClick);
            // 
            // ftpButton
            // 
            this.ftpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ftpButton.Location = new System.Drawing.Point(74, 386);
            this.ftpButton.Name = "ftpButton";
            this.ftpButton.Size = new System.Drawing.Size(55, 23);
            this.ftpButton.TabIndex = 13;
            this.ftpButton.Text = "FTP";
            this.ftpButton.UseVisualStyleBackColor = true;
            this.ftpButton.Click += new System.EventHandler(this.ftpButtonClick);
            // 
            // urlsButton
            // 
            this.urlsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.urlsButton.Location = new System.Drawing.Point(6, 386);
            this.urlsButton.Name = "urlsButton";
            this.urlsButton.Size = new System.Drawing.Size(55, 23);
            this.urlsButton.TabIndex = 12;
            this.urlsButton.Text = "URLs";
            this.urlsButton.UseVisualStyleBackColor = true;
            this.urlsButton.Click += new System.EventHandler(this.urlsButtonClick);
            // 
            // tasksLabel
            // 
            this.tasksLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tasksLabel.AutoSize = true;
            this.tasksLabel.Location = new System.Drawing.Point(132, 333);
            this.tasksLabel.Name = "tasksLabel";
            this.tasksLabel.Size = new System.Drawing.Size(36, 13);
            this.tasksLabel.TabIndex = 11;
            this.tasksLabel.Text = "Tasks";
            // 
            // tasksNumericUpDown
            // 
            this.tasksNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tasksNumericUpDown.Location = new System.Drawing.Point(202, 331);
            this.tasksNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.tasksNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tasksNumericUpDown.Name = "tasksNumericUpDown";
            this.tasksNumericUpDown.Size = new System.Drawing.Size(57, 20);
            this.tasksNumericUpDown.TabIndex = 10;
            this.tasksNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textCheckedListBox
            // 
            this.textCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textCheckedListBox.FormattingEnabled = true;
            this.textCheckedListBox.Location = new System.Drawing.Point(73, 214);
            this.textCheckedListBox.Name = "textCheckedListBox";
            this.textCheckedListBox.Size = new System.Drawing.Size(185, 109);
            this.textCheckedListBox.TabIndex = 9;
            // 
            // textLabel
            // 
            this.textLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textLabel.AutoSize = true;
            this.textLabel.Location = new System.Drawing.Point(3, 217);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(28, 13);
            this.textLabel.TabIndex = 8;
            this.textLabel.Text = "Text";
            // 
            // keywordsCheckedListBox
            // 
            this.keywordsCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.keywordsCheckedListBox.FormattingEnabled = true;
            this.keywordsCheckedListBox.Location = new System.Drawing.Point(73, 84);
            this.keywordsCheckedListBox.Name = "keywordsCheckedListBox";
            this.keywordsCheckedListBox.Size = new System.Drawing.Size(185, 124);
            this.keywordsCheckedListBox.TabIndex = 7;
            // 
            // keywordsLabel
            // 
            this.keywordsLabel.AutoSize = true;
            this.keywordsLabel.Location = new System.Drawing.Point(3, 88);
            this.keywordsLabel.Name = "keywordsLabel";
            this.keywordsLabel.Size = new System.Drawing.Size(53, 13);
            this.keywordsLabel.TabIndex = 6;
            this.keywordsLabel.Text = "Keywords";
            // 
            // templateComboBox
            // 
            this.templateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.templateComboBox.FormattingEnabled = true;
            this.templateComboBox.Location = new System.Drawing.Point(73, 57);
            this.templateComboBox.Name = "templateComboBox";
            this.templateComboBox.Size = new System.Drawing.Size(185, 21);
            this.templateComboBox.TabIndex = 5;
            // 
            // templateLabel
            // 
            this.templateLabel.AutoSize = true;
            this.templateLabel.Location = new System.Drawing.Point(3, 60);
            this.templateLabel.Name = "templateLabel";
            this.templateLabel.Size = new System.Drawing.Size(51, 13);
            this.templateLabel.TabIndex = 4;
            this.templateLabel.Text = "Template";
            // 
            // presetComboBox
            // 
            this.presetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.presetComboBox.FormattingEnabled = true;
            this.presetComboBox.Location = new System.Drawing.Point(73, 30);
            this.presetComboBox.Name = "presetComboBox";
            this.presetComboBox.Size = new System.Drawing.Size(185, 21);
            this.presetComboBox.TabIndex = 3;
            this.presetComboBox.SelectedIndexChanged += new System.EventHandler(this.presetComboBoxSelectedIndexChanged);
            // 
            // presetLabel
            // 
            this.presetLabel.AutoSize = true;
            this.presetLabel.Location = new System.Drawing.Point(3, 33);
            this.presetLabel.Name = "presetLabel";
            this.presetLabel.Size = new System.Drawing.Size(37, 13);
            this.presetLabel.TabIndex = 2;
            this.presetLabel.Text = "Preset";
            // 
            // workSpaceComboBox
            // 
            this.workSpaceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.workSpaceComboBox.FormattingEnabled = true;
            this.workSpaceComboBox.Location = new System.Drawing.Point(73, 3);
            this.workSpaceComboBox.Name = "workSpaceComboBox";
            this.workSpaceComboBox.Size = new System.Drawing.Size(185, 21);
            this.workSpaceComboBox.TabIndex = 1;
            this.workSpaceComboBox.SelectedIndexChanged += new System.EventHandler(this.workSpaceComboBoxSelectedIndexChanged);
            // 
            // workSpaceLabel
            // 
            this.workSpaceLabel.AutoSize = true;
            this.workSpaceLabel.Location = new System.Drawing.Point(3, 6);
            this.workSpaceLabel.Name = "workSpaceLabel";
            this.workSpaceLabel.Size = new System.Drawing.Size(64, 13);
            this.workSpaceLabel.TabIndex = 0;
            this.workSpaceLabel.Text = "WorkSpace";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Doorway Studio Automator";
            this.Load += new System.EventHandler(this.MainLoad);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tasksNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid mainPropertyGrid;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ComboBox workSpaceComboBox;
        private System.Windows.Forms.Label workSpaceLabel;
        private System.Windows.Forms.ComboBox templateComboBox;
        private System.Windows.Forms.Label templateLabel;
        private System.Windows.Forms.ComboBox presetComboBox;
        private System.Windows.Forms.Label presetLabel;
        private System.Windows.Forms.Label keywordsLabel;
        private System.Windows.Forms.CheckedListBox keywordsCheckedListBox;
        private System.Windows.Forms.CheckedListBox textCheckedListBox;
        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.Label tasksLabel;
        private System.Windows.Forms.NumericUpDown tasksNumericUpDown;
        private System.Windows.Forms.Button urlsButton;
        private System.Windows.Forms.Button ftpButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button tokensButton;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button tagsButton;
        private System.Windows.Forms.ComboBox KTUsageComboBox;
        private System.Windows.Forms.Label KTUsageLabel;
        private System.Windows.Forms.Button keywordsButton;
        private System.Windows.Forms.Button textButton;
    }
}