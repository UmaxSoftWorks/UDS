namespace Automator
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
            this.mainBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.exitButton = new System.Windows.Forms.Button();
            this.retryButton = new System.Windows.Forms.Button();
            this.mainProgressBar = new System.Windows.Forms.ProgressBar();
            this.actionText = new System.Windows.Forms.Label();
            this.licenseBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // mainBackgroundWorker
            // 
            this.mainBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mainBackgroundWorkerDoWork);
            this.mainBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.mainBackgroundWorkerRunWorkerCompleted);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(258, 44);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 15;
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButtonClick);
            // 
            // retryButton
            // 
            this.retryButton.Location = new System.Drawing.Point(154, 44);
            this.retryButton.Name = "retryButton";
            this.retryButton.Size = new System.Drawing.Size(98, 23);
            this.retryButton.TabIndex = 14;
            this.retryButton.UseVisualStyleBackColor = true;
            this.retryButton.Click += new System.EventHandler(this.retryButtonClick);
            // 
            // mainProgressBar
            // 
            this.mainProgressBar.Location = new System.Drawing.Point(14, 26);
            this.mainProgressBar.Name = "mainProgressBar";
            this.mainProgressBar.Size = new System.Drawing.Size(319, 12);
            this.mainProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.mainProgressBar.TabIndex = 12;
            // 
            // actionText
            // 
            this.actionText.AutoSize = true;
            this.actionText.Location = new System.Drawing.Point(12, 10);
            this.actionText.Name = "actionText";
            this.actionText.Size = new System.Drawing.Size(10, 13);
            this.actionText.TabIndex = 9;
            this.actionText.Text = "-";
            // 
            // licenseBackgroundWorker
            // 
            this.licenseBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.licenseBackgroundWorkerDoWork);
            this.licenseBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.licenseBackgroundWorkerRunWorkerCompleted);
            // 
            // StartUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 72);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.retryButton);
            this.Controls.Add(this.mainProgressBar);
            this.Controls.Add(this.actionText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "StartUp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.StartUpLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker mainBackgroundWorker;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button retryButton;
        private System.Windows.Forms.ProgressBar mainProgressBar;
        private System.Windows.Forms.Label actionText;
        private System.ComponentModel.BackgroundWorker licenseBackgroundWorker;
    }
}

