namespace Automator.Windows
{
    partial class FTP
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.openButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.mainOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainDataGridView = new System.Windows.Forms.DataGridView();
            this.ftpServerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ftpAccountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ftpPasswordColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ftpRemoteFolderColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.openButton);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 412);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 30);
            this.panel1.TabIndex = 1;
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(3, 3);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 2;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(546, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButtonClick);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(465, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButtonClick);
            // 
            // mainOpenFileDialog
            // 
            this.mainOpenFileDialog.Filter = "Text|*.txt|All|*.*";
            // 
            // mainDataGridView
            // 
            this.mainDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ftpServerColumn,
            this.ftpAccountColumn,
            this.ftpPasswordColumn,
            this.ftpRemoteFolderColumn});
            this.mainDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.mainDataGridView.Location = new System.Drawing.Point(0, 0);
            this.mainDataGridView.Name = "mainDataGridView";
            this.mainDataGridView.Size = new System.Drawing.Size(624, 412);
            this.mainDataGridView.TabIndex = 2;
            // 
            // ftpServerColumn
            // 
            this.ftpServerColumn.HeaderText = "Host";
            this.ftpServerColumn.Name = "ftpServerColumn";
            this.ftpServerColumn.Width = 200;
            // 
            // ftpAccountColumn
            // 
            this.ftpAccountColumn.HeaderText = "Account";
            this.ftpAccountColumn.Name = "ftpAccountColumn";
            // 
            // ftpPasswordColumn
            // 
            this.ftpPasswordColumn.HeaderText = "Password";
            this.ftpPasswordColumn.Name = "ftpPasswordColumn";
            // 
            // ftpRemoteFolderColumn
            // 
            this.ftpRemoteFolderColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ftpRemoteFolderColumn.HeaderText = "Remote Folder";
            this.ftpRemoteFolderColumn.Name = "ftpRemoteFolderColumn";
            // 
            // FTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.mainDataGridView);
            this.Controls.Add(this.panel1);
            this.Name = "FTP";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FTP";
            this.Load += new System.EventHandler(this.FTPLoad);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.OpenFileDialog mainOpenFileDialog;
        private System.Windows.Forms.DataGridView mainDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ftpServerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ftpAccountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ftpPasswordColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ftpRemoteFolderColumn;
    }
}