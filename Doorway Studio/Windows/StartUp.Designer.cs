namespace Doorway_Studio
{
    partial class StartUp
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
            this.actionLabel = new System.Windows.Forms.Label();
            this.actionText = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.statusText = new System.Windows.Forms.Label();
            this.mainProgressBar = new System.Windows.Forms.ProgressBar();
            this.startdemoButton = new System.Windows.Forms.Button();
            this.retryButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // actionLabel
            // 
            this.actionLabel.AutoSize = true;
            this.actionLabel.Location = new System.Drawing.Point(11, 13);
            this.actionLabel.Name = "actionLabel";
            this.actionLabel.Size = new System.Drawing.Size(0, 13);
            this.actionLabel.TabIndex = 0;
            // 
            // actionText
            // 
            this.actionText.AutoSize = true;
            this.actionText.Location = new System.Drawing.Point(70, 13);
            this.actionText.Name = "actionText";
            this.actionText.Size = new System.Drawing.Size(10, 13);
            this.actionText.TabIndex = 1;
            this.actionText.Text = "-";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(11, 36);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 13);
            this.statusLabel.TabIndex = 2;
            // 
            // statusText
            // 
            this.statusText.AutoSize = true;
            this.statusText.Location = new System.Drawing.Point(70, 36);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(10, 13);
            this.statusText.TabIndex = 3;
            this.statusText.Text = "-";
            // 
            // mainProgressBar
            // 
            this.mainProgressBar.Location = new System.Drawing.Point(14, 54);
            this.mainProgressBar.Name = "mainProgressBar";
            this.mainProgressBar.Size = new System.Drawing.Size(319, 12);
            this.mainProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.mainProgressBar.TabIndex = 4;
            // 
            // startdemoButton
            // 
            this.startdemoButton.Location = new System.Drawing.Point(12, 72);
            this.startdemoButton.Name = "startdemoButton";
            this.startdemoButton.Size = new System.Drawing.Size(112, 23);
            this.startdemoButton.TabIndex = 5;
            this.startdemoButton.UseVisualStyleBackColor = true;
            this.startdemoButton.Click += new System.EventHandler(this.startdemoButton_Click);
            // 
            // retryButton
            // 
            this.retryButton.Location = new System.Drawing.Point(177, 72);
            this.retryButton.Name = "retryButton";
            this.retryButton.Size = new System.Drawing.Size(75, 23);
            this.retryButton.TabIndex = 6;
            this.retryButton.UseVisualStyleBackColor = true;
            this.retryButton.Click += new System.EventHandler(this.retryButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(258, 72);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 7;
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // mainTimer
            // 
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // StartUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 102);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.retryButton);
            this.Controls.Add(this.startdemoButton);
            this.Controls.Add(this.mainProgressBar);
            this.Controls.Add(this.statusText);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.actionText);
            this.Controls.Add(this.actionLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "StartUp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.StartUp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label actionLabel;
        private System.Windows.Forms.Label actionText;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label statusText;
        private System.Windows.Forms.ProgressBar mainProgressBar;
        private System.Windows.Forms.Button startdemoButton;
        private System.Windows.Forms.Button retryButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Timer mainTimer;
    }
}

