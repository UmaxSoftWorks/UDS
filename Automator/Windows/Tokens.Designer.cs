namespace Automator.Windows
{
    partial class Tokens
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
            this.mainOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.mainGroupBox = new System.Windows.Forms.GroupBox();
            this.mainDataGridView = new System.Windows.Forms.DataGridView();
            this.fileMacrossMacrossColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileMacrossPathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileMacrossEncodingColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileMacrossTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionsGroupBox = new System.Windows.Forms.GroupBox();
            this.fileMacrossTypeComboBox = new System.Windows.Forms.ComboBox();
            this.fileMacrossTypeLabel = new System.Windows.Forms.Label();
            this.fileMacrossEncodingComboBox = new System.Windows.Forms.ComboBox();
            this.fileMacrossDeleteFileMacrossButton = new System.Windows.Forms.Button();
            this.fileMacrossEncodingLabel = new System.Windows.Forms.Label();
            this.fileMacrossFileSelectButton = new System.Windows.Forms.Button();
            this.fileMacrossPathTextBox = new System.Windows.Forms.TextBox();
            this.fileMacrossFileLabel = new System.Windows.Forms.Label();
            this.fileMacrossTextBox = new System.Windows.Forms.TextBox();
            this.fileMacrossLabel = new System.Windows.Forms.Label();
            this.fileMacrossAddFileMacrossButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.mainGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGridView)).BeginInit();
            this.actionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainOpenFileDialog
            // 
            this.mainOpenFileDialog.Filter = "Text|*.txt|All|*.*";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 412);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 30);
            this.panel1.TabIndex = 2;
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
            // mainGroupBox
            // 
            this.mainGroupBox.Controls.Add(this.mainDataGridView);
            this.mainGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainGroupBox.Location = new System.Drawing.Point(0, 102);
            this.mainGroupBox.Name = "mainGroupBox";
            this.mainGroupBox.Size = new System.Drawing.Size(624, 310);
            this.mainGroupBox.TabIndex = 4;
            this.mainGroupBox.TabStop = false;
            // 
            // mainDataGridView
            // 
            this.mainDataGridView.AllowUserToAddRows = false;
            this.mainDataGridView.AllowUserToDeleteRows = false;
            this.mainDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fileMacrossMacrossColumn,
            this.fileMacrossPathColumn,
            this.fileMacrossEncodingColumn,
            this.fileMacrossTypeColumn});
            this.mainDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDataGridView.Location = new System.Drawing.Point(3, 16);
            this.mainDataGridView.MultiSelect = false;
            this.mainDataGridView.Name = "mainDataGridView";
            this.mainDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mainDataGridView.Size = new System.Drawing.Size(618, 291);
            this.mainDataGridView.TabIndex = 0;
            // 
            // fileMacrossMacrossColumn
            // 
            this.fileMacrossMacrossColumn.HeaderText = "Token";
            this.fileMacrossMacrossColumn.Name = "fileMacrossMacrossColumn";
            // 
            // fileMacrossPathColumn
            // 
            this.fileMacrossPathColumn.HeaderText = "Path";
            this.fileMacrossPathColumn.Name = "fileMacrossPathColumn";
            this.fileMacrossPathColumn.ReadOnly = true;
            this.fileMacrossPathColumn.Width = 300;
            // 
            // fileMacrossEncodingColumn
            // 
            this.fileMacrossEncodingColumn.FillWeight = 70F;
            this.fileMacrossEncodingColumn.HeaderText = "Encoding";
            this.fileMacrossEncodingColumn.Name = "fileMacrossEncodingColumn";
            this.fileMacrossEncodingColumn.ReadOnly = true;
            this.fileMacrossEncodingColumn.Width = 70;
            // 
            // fileMacrossTypeColumn
            // 
            this.fileMacrossTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fileMacrossTypeColumn.FillWeight = 70F;
            this.fileMacrossTypeColumn.HeaderText = "Type";
            this.fileMacrossTypeColumn.Name = "fileMacrossTypeColumn";
            // 
            // actionsGroupBox
            // 
            this.actionsGroupBox.Controls.Add(this.fileMacrossTypeComboBox);
            this.actionsGroupBox.Controls.Add(this.fileMacrossTypeLabel);
            this.actionsGroupBox.Controls.Add(this.fileMacrossEncodingComboBox);
            this.actionsGroupBox.Controls.Add(this.fileMacrossDeleteFileMacrossButton);
            this.actionsGroupBox.Controls.Add(this.fileMacrossEncodingLabel);
            this.actionsGroupBox.Controls.Add(this.fileMacrossFileSelectButton);
            this.actionsGroupBox.Controls.Add(this.fileMacrossPathTextBox);
            this.actionsGroupBox.Controls.Add(this.fileMacrossFileLabel);
            this.actionsGroupBox.Controls.Add(this.fileMacrossTextBox);
            this.actionsGroupBox.Controls.Add(this.fileMacrossLabel);
            this.actionsGroupBox.Controls.Add(this.fileMacrossAddFileMacrossButton);
            this.actionsGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.actionsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.actionsGroupBox.Name = "actionsGroupBox";
            this.actionsGroupBox.Size = new System.Drawing.Size(624, 102);
            this.actionsGroupBox.TabIndex = 3;
            this.actionsGroupBox.TabStop = false;
            // 
            // fileMacrossTypeComboBox
            // 
            this.fileMacrossTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fileMacrossTypeComboBox.FormattingEnabled = true;
            this.fileMacrossTypeComboBox.Location = new System.Drawing.Point(262, 74);
            this.fileMacrossTypeComboBox.Name = "fileMacrossTypeComboBox";
            this.fileMacrossTypeComboBox.Size = new System.Drawing.Size(174, 21);
            this.fileMacrossTypeComboBox.TabIndex = 10;
            // 
            // fileMacrossTypeLabel
            // 
            this.fileMacrossTypeLabel.AutoSize = true;
            this.fileMacrossTypeLabel.Location = new System.Drawing.Point(221, 77);
            this.fileMacrossTypeLabel.Name = "fileMacrossTypeLabel";
            this.fileMacrossTypeLabel.Size = new System.Drawing.Size(0, 13);
            this.fileMacrossTypeLabel.TabIndex = 9;
            // 
            // fileMacrossEncodingComboBox
            // 
            this.fileMacrossEncodingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fileMacrossEncodingComboBox.FormattingEnabled = true;
            this.fileMacrossEncodingComboBox.Items.AddRange(new object[] {
            "Win-1251 (ANSI)",
            "UTF-8"});
            this.fileMacrossEncodingComboBox.Location = new System.Drawing.Point(71, 74);
            this.fileMacrossEncodingComboBox.Name = "fileMacrossEncodingComboBox";
            this.fileMacrossEncodingComboBox.Size = new System.Drawing.Size(106, 21);
            this.fileMacrossEncodingComboBox.TabIndex = 8;
            // 
            // fileMacrossDeleteFileMacrossButton
            // 
            this.fileMacrossDeleteFileMacrossButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fileMacrossDeleteFileMacrossButton.Location = new System.Drawing.Point(543, 72);
            this.fileMacrossDeleteFileMacrossButton.Name = "fileMacrossDeleteFileMacrossButton";
            this.fileMacrossDeleteFileMacrossButton.Size = new System.Drawing.Size(75, 23);
            this.fileMacrossDeleteFileMacrossButton.TabIndex = 7;
            this.fileMacrossDeleteFileMacrossButton.UseVisualStyleBackColor = true;
            this.fileMacrossDeleteFileMacrossButton.Click += new System.EventHandler(this.deleteFileMacrossButtonClick);
            // 
            // fileMacrossEncodingLabel
            // 
            this.fileMacrossEncodingLabel.AutoSize = true;
            this.fileMacrossEncodingLabel.Location = new System.Drawing.Point(6, 77);
            this.fileMacrossEncodingLabel.Name = "fileMacrossEncodingLabel";
            this.fileMacrossEncodingLabel.Size = new System.Drawing.Size(0, 13);
            this.fileMacrossEncodingLabel.TabIndex = 6;
            // 
            // fileMacrossFileSelectButton
            // 
            this.fileMacrossFileSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fileMacrossFileSelectButton.Location = new System.Drawing.Point(586, 17);
            this.fileMacrossFileSelectButton.Name = "fileMacrossFileSelectButton";
            this.fileMacrossFileSelectButton.Size = new System.Drawing.Size(32, 23);
            this.fileMacrossFileSelectButton.TabIndex = 5;
            this.fileMacrossFileSelectButton.Text = "...";
            this.fileMacrossFileSelectButton.UseVisualStyleBackColor = true;
            this.fileMacrossFileSelectButton.Click += new System.EventHandler(this.fileSelectButtonClick);
            // 
            // fileMacrossPathTextBox
            // 
            this.fileMacrossPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileMacrossPathTextBox.Location = new System.Drawing.Point(71, 19);
            this.fileMacrossPathTextBox.Name = "fileMacrossPathTextBox";
            this.fileMacrossPathTextBox.Size = new System.Drawing.Size(509, 20);
            this.fileMacrossPathTextBox.TabIndex = 4;
            // 
            // fileMacrossFileLabel
            // 
            this.fileMacrossFileLabel.AutoSize = true;
            this.fileMacrossFileLabel.Location = new System.Drawing.Point(6, 22);
            this.fileMacrossFileLabel.Name = "fileMacrossFileLabel";
            this.fileMacrossFileLabel.Size = new System.Drawing.Size(0, 13);
            this.fileMacrossFileLabel.TabIndex = 3;
            // 
            // fileMacrossTextBox
            // 
            this.fileMacrossTextBox.Location = new System.Drawing.Point(71, 48);
            this.fileMacrossTextBox.Name = "fileMacrossTextBox";
            this.fileMacrossTextBox.Size = new System.Drawing.Size(365, 20);
            this.fileMacrossTextBox.TabIndex = 2;
            // 
            // fileMacrossLabel
            // 
            this.fileMacrossLabel.AutoSize = true;
            this.fileMacrossLabel.Location = new System.Drawing.Point(6, 51);
            this.fileMacrossLabel.Name = "fileMacrossLabel";
            this.fileMacrossLabel.Size = new System.Drawing.Size(0, 13);
            this.fileMacrossLabel.TabIndex = 1;
            // 
            // fileMacrossAddFileMacrossButton
            // 
            this.fileMacrossAddFileMacrossButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fileMacrossAddFileMacrossButton.Location = new System.Drawing.Point(543, 46);
            this.fileMacrossAddFileMacrossButton.Name = "fileMacrossAddFileMacrossButton";
            this.fileMacrossAddFileMacrossButton.Size = new System.Drawing.Size(75, 23);
            this.fileMacrossAddFileMacrossButton.TabIndex = 0;
            this.fileMacrossAddFileMacrossButton.UseVisualStyleBackColor = true;
            this.fileMacrossAddFileMacrossButton.Click += new System.EventHandler(this.addFileMacrossButtonClick);
            // 
            // Tokens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.mainGroupBox);
            this.Controls.Add(this.actionsGroupBox);
            this.Controls.Add(this.panel1);
            this.Name = "Tokens";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tokens";
            this.Load += new System.EventHandler(this.TokensLoad);
            this.panel1.ResumeLayout(false);
            this.mainGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGridView)).EndInit();
            this.actionsGroupBox.ResumeLayout(false);
            this.actionsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog mainOpenFileDialog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.GroupBox mainGroupBox;
        private System.Windows.Forms.DataGridView mainDataGridView;
        private System.Windows.Forms.GroupBox actionsGroupBox;
        private System.Windows.Forms.ComboBox fileMacrossTypeComboBox;
        private System.Windows.Forms.Label fileMacrossTypeLabel;
        private System.Windows.Forms.ComboBox fileMacrossEncodingComboBox;
        private System.Windows.Forms.Button fileMacrossDeleteFileMacrossButton;
        private System.Windows.Forms.Label fileMacrossEncodingLabel;
        private System.Windows.Forms.Button fileMacrossFileSelectButton;
        private System.Windows.Forms.TextBox fileMacrossPathTextBox;
        private System.Windows.Forms.Label fileMacrossFileLabel;
        private System.Windows.Forms.TextBox fileMacrossTextBox;
        private System.Windows.Forms.Label fileMacrossLabel;
        private System.Windows.Forms.Button fileMacrossAddFileMacrossButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileMacrossMacrossColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileMacrossPathColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileMacrossEncodingColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileMacrossTypeColumn;
    }
}