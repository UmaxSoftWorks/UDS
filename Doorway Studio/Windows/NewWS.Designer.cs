namespace Doorway_Studio
{
    partial class NewWS
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
            this.OKButton = new System.Windows.Forms.Button();
            this.commTextBox = new System.Windows.Forms.TextBox();
            this.commLabel = new System.Windows.Forms.Label();
            this.IDLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.tipsTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(328, 64);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // TipsTextBox
            // 
            this.TipsTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.TipsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TipsTextBox.Enabled = false;
            this.TipsTextBox.Location = new System.Drawing.Point(0, 144);
            this.TipsTextBox.Multiline = true;
            this.TipsTextBox.Name = "TipsTextBox";
            this.TipsTextBox.Size = new System.Drawing.Size(412, 35);
            this.TipsTextBox.TabIndex = 8;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(328, 35);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "Ok";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // commTextBox
            // 
            this.commTextBox.Location = new System.Drawing.Point(74, 35);
            this.commTextBox.Multiline = true;
            this.commTextBox.Name = "commTextBox";
            this.commTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commTextBox.Size = new System.Drawing.Size(248, 104);
            this.commTextBox.TabIndex = 5;
            // 
            // commLabel
            // 
            this.commLabel.AutoSize = true;
            this.commLabel.Location = new System.Drawing.Point(12, 38);
            this.commLabel.Name = "commLabel";
            this.commLabel.Size = new System.Drawing.Size(0, 13);
            this.commLabel.TabIndex = 4;
            // 
            // IDLabel
            // 
            this.IDLabel.AutoSize = true;
            this.IDLabel.Location = new System.Drawing.Point(328, 9);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(18, 13);
            this.IDLabel.TabIndex = 3;
            this.IDLabel.Text = "ID";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(74, 6);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(248, 20);
            this.nameTextBox.TabIndex = 2;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(0, 13);
            this.nameLabel.TabIndex = 1;
            // 
            // tipsTimer
            // 
            this.tipsTimer.Interval = 5000;
            this.tipsTimer.Tick += new System.EventHandler(this.tipsTimer_Tick);
            // 
            // NewWS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 179);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.TipsTextBox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.commTextBox);
            this.Controls.Add(this.commLabel);
            this.Controls.Add(this.IDLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.nameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewWS";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.NewWS_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewWS_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox TipsTextBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.TextBox commTextBox;
        private System.Windows.Forms.Label commLabel;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Timer tipsTimer;
    }
}