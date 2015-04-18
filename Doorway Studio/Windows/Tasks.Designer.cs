namespace Doorway_Studio
{
    partial class Tasks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tasks));
            this.mainTasksPanel = new System.Windows.Forms.Panel();
            this.actionsGroupBox = new System.Windows.Forms.GroupBox();
            this.actionsCopyButton = new System.Windows.Forms.Button();
            this.actionsDeleteButton = new System.Windows.Forms.Button();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.settingsMonthComboBox = new System.Windows.Forms.ComboBox();
            this.settingsMonthLabel = new System.Windows.Forms.Label();
            this.settingsYearComboBox = new System.Windows.Forms.ComboBox();
            this.settingsYearLabel = new System.Windows.Forms.Label();
            this.tasksWSComboBox = new System.Windows.Forms.ComboBox();
            this.tasksWSLabel = new System.Windows.Forms.Label();
            this.tasksGroupBox = new System.Windows.Forms.GroupBox();
            this.tasksTreeView = new System.Windows.Forms.TreeView();
            this.tasksImageList = new System.Windows.Forms.ImageList(this.components);
            this.mainTasksPanel.SuspendLayout();
            this.actionsGroupBox.SuspendLayout();
            this.settingsGroupBox.SuspendLayout();
            this.tasksGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTasksPanel
            // 
            this.mainTasksPanel.Controls.Add(this.actionsGroupBox);
            this.mainTasksPanel.Controls.Add(this.settingsGroupBox);
            this.mainTasksPanel.Controls.Add(this.tasksWSComboBox);
            this.mainTasksPanel.Controls.Add(this.tasksWSLabel);
            this.mainTasksPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mainTasksPanel.Location = new System.Drawing.Point(0, 299);
            this.mainTasksPanel.Name = "mainTasksPanel";
            this.mainTasksPanel.Size = new System.Drawing.Size(412, 95);
            this.mainTasksPanel.TabIndex = 2;
            // 
            // actionsGroupBox
            // 
            this.actionsGroupBox.Controls.Add(this.actionsCopyButton);
            this.actionsGroupBox.Controls.Add(this.actionsDeleteButton);
            this.actionsGroupBox.Location = new System.Drawing.Point(8, 34);
            this.actionsGroupBox.Name = "actionsGroupBox";
            this.actionsGroupBox.Size = new System.Drawing.Size(188, 51);
            this.actionsGroupBox.TabIndex = 11;
            this.actionsGroupBox.TabStop = false;
            // 
            // actionsCopyButton
            // 
            this.actionsCopyButton.Location = new System.Drawing.Point(107, 19);
            this.actionsCopyButton.Name = "actionsCopyButton";
            this.actionsCopyButton.Size = new System.Drawing.Size(75, 23);
            this.actionsCopyButton.TabIndex = 1;
            this.actionsCopyButton.UseVisualStyleBackColor = true;
            this.actionsCopyButton.Click += new System.EventHandler(this.actionsCopyButton_Click);
            // 
            // actionsDeleteButton
            // 
            this.actionsDeleteButton.Location = new System.Drawing.Point(6, 19);
            this.actionsDeleteButton.Name = "actionsDeleteButton";
            this.actionsDeleteButton.Size = new System.Drawing.Size(75, 23);
            this.actionsDeleteButton.TabIndex = 0;
            this.actionsDeleteButton.UseVisualStyleBackColor = true;
            this.actionsDeleteButton.Click += new System.EventHandler(this.actionsDeleteButton_Click);
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Controls.Add(this.settingsMonthComboBox);
            this.settingsGroupBox.Controls.Add(this.settingsMonthLabel);
            this.settingsGroupBox.Controls.Add(this.settingsYearComboBox);
            this.settingsGroupBox.Controls.Add(this.settingsYearLabel);
            this.settingsGroupBox.Location = new System.Drawing.Point(202, 10);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Size = new System.Drawing.Size(195, 75);
            this.settingsGroupBox.TabIndex = 10;
            this.settingsGroupBox.TabStop = false;
            // 
            // settingsMonthComboBox
            // 
            this.settingsMonthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.settingsMonthComboBox.FormattingEnabled = true;
            this.settingsMonthComboBox.Location = new System.Drawing.Point(70, 46);
            this.settingsMonthComboBox.Name = "settingsMonthComboBox";
            this.settingsMonthComboBox.Size = new System.Drawing.Size(121, 21);
            this.settingsMonthComboBox.TabIndex = 15;
            this.settingsMonthComboBox.SelectedIndexChanged += new System.EventHandler(this.settingsMonthComboBox_SelectedIndexChanged);
            // 
            // settingsMonthLabel
            // 
            this.settingsMonthLabel.AutoSize = true;
            this.settingsMonthLabel.Location = new System.Drawing.Point(6, 49);
            this.settingsMonthLabel.Name = "settingsMonthLabel";
            this.settingsMonthLabel.Size = new System.Drawing.Size(0, 13);
            this.settingsMonthLabel.TabIndex = 14;
            // 
            // settingsYearComboBox
            // 
            this.settingsYearComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.settingsYearComboBox.FormattingEnabled = true;
            this.settingsYearComboBox.Location = new System.Drawing.Point(70, 19);
            this.settingsYearComboBox.Name = "settingsYearComboBox";
            this.settingsYearComboBox.Size = new System.Drawing.Size(121, 21);
            this.settingsYearComboBox.TabIndex = 13;
            this.settingsYearComboBox.SelectedIndexChanged += new System.EventHandler(this.settingsYearComboBox_SelectedIndexChanged);
            // 
            // settingsYearLabel
            // 
            this.settingsYearLabel.AutoSize = true;
            this.settingsYearLabel.Location = new System.Drawing.Point(6, 22);
            this.settingsYearLabel.Name = "settingsYearLabel";
            this.settingsYearLabel.Size = new System.Drawing.Size(0, 13);
            this.settingsYearLabel.TabIndex = 12;
            // 
            // tasksWSComboBox
            // 
            this.tasksWSComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tasksWSComboBox.FormattingEnabled = true;
            this.tasksWSComboBox.Location = new System.Drawing.Point(75, 7);
            this.tasksWSComboBox.Name = "tasksWSComboBox";
            this.tasksWSComboBox.Size = new System.Drawing.Size(121, 21);
            this.tasksWSComboBox.TabIndex = 1;
            this.tasksWSComboBox.SelectedIndexChanged += new System.EventHandler(this.tasksWSComboBox_SelectedIndexChanged);
            // 
            // tasksWSLabel
            // 
            this.tasksWSLabel.AutoSize = true;
            this.tasksWSLabel.Location = new System.Drawing.Point(5, 10);
            this.tasksWSLabel.Name = "tasksWSLabel";
            this.tasksWSLabel.Size = new System.Drawing.Size(64, 13);
            this.tasksWSLabel.TabIndex = 0;
            this.tasksWSLabel.Text = "WorkSpace";
            // 
            // tasksGroupBox
            // 
            this.tasksGroupBox.Controls.Add(this.tasksTreeView);
            this.tasksGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tasksGroupBox.Location = new System.Drawing.Point(0, 0);
            this.tasksGroupBox.Name = "tasksGroupBox";
            this.tasksGroupBox.Size = new System.Drawing.Size(412, 299);
            this.tasksGroupBox.TabIndex = 3;
            this.tasksGroupBox.TabStop = false;
            // 
            // tasksTreeView
            // 
            this.tasksTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tasksTreeView.ImageIndex = 0;
            this.tasksTreeView.ImageList = this.tasksImageList;
            this.tasksTreeView.Location = new System.Drawing.Point(3, 16);
            this.tasksTreeView.Name = "tasksTreeView";
            this.tasksTreeView.SelectedImageIndex = 0;
            this.tasksTreeView.Size = new System.Drawing.Size(406, 280);
            this.tasksTreeView.TabIndex = 0;
            this.tasksTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tasksTreeView_NodeMouseDoubleClick);
            // 
            // tasksImageList
            // 
            this.tasksImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tasksImageList.ImageStream")));
            this.tasksImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.tasksImageList.Images.SetKeyName(0, "WorkSpace.png");
            this.tasksImageList.Images.SetKeyName(1, "Calendar.png");
            this.tasksImageList.Images.SetKeyName(2, "Item.png");
            // 
            // Tasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 394);
            this.Controls.Add(this.tasksGroupBox);
            this.Controls.Add(this.mainTasksPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Tasks";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.Tasks_Load);
            this.mainTasksPanel.ResumeLayout(false);
            this.mainTasksPanel.PerformLayout();
            this.actionsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.PerformLayout();
            this.tasksGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainTasksPanel;
        private System.Windows.Forms.GroupBox settingsGroupBox;
        private System.Windows.Forms.ComboBox settingsMonthComboBox;
        private System.Windows.Forms.Label settingsMonthLabel;
        private System.Windows.Forms.ComboBox settingsYearComboBox;
        private System.Windows.Forms.Label settingsYearLabel;
        private System.Windows.Forms.ComboBox tasksWSComboBox;
        private System.Windows.Forms.Label tasksWSLabel;
        private System.Windows.Forms.GroupBox tasksGroupBox;
        private System.Windows.Forms.TreeView tasksTreeView;
        private System.Windows.Forms.GroupBox actionsGroupBox;
        private System.Windows.Forms.Button actionsDeleteButton;
        private System.Windows.Forms.Button actionsCopyButton;
        private System.Windows.Forms.ImageList tasksImageList;
    }
}