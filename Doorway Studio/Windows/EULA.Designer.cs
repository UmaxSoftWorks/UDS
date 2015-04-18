namespace Doorway_Studio
{
    partial class EULA
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
            this.EULALinkLabel = new System.Windows.Forms.LinkLabel();
            this.EULACheckBox = new System.Windows.Forms.CheckBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.OLALabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // EULALinkLabel
            // 
            this.EULALinkLabel.AutoSize = true;
            this.EULALinkLabel.Location = new System.Drawing.Point(160, 9);
            this.EULALinkLabel.Name = "EULALinkLabel";
            this.EULALinkLabel.Size = new System.Drawing.Size(0, 13);
            this.EULALinkLabel.TabIndex = 1;
            this.EULALinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.EULALinkLabel_LinkClicked);
            // 
            // EULACheckBox
            // 
            this.EULACheckBox.AutoSize = true;
            this.EULACheckBox.Checked = true;
            this.EULACheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EULACheckBox.Location = new System.Drawing.Point(15, 32);
            this.EULACheckBox.Name = "EULACheckBox";
            this.EULACheckBox.Size = new System.Drawing.Size(15, 14);
            this.EULACheckBox.TabIndex = 2;
            this.EULACheckBox.UseVisualStyleBackColor = true;
            this.EULACheckBox.CheckedChanged += new System.EventHandler(this.EULACheckBox_CheckedChanged);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(188, 28);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // OLALabel
            // 
            this.OLALabel.AutoSize = true;
            this.OLALabel.Location = new System.Drawing.Point(12, 9);
            this.OLALabel.Name = "OLALabel";
            this.OLALabel.Size = new System.Drawing.Size(0, 13);
            this.OLALabel.TabIndex = 4;
            // 
            // EULA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 61);
            this.Controls.Add(this.OLALabel);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.EULACheckBox);
            this.Controls.Add(this.EULALinkLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EULA";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.EULA_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EULA_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel EULALinkLabel;
        private System.Windows.Forms.CheckBox EULACheckBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label OLALabel;

    }
}