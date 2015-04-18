namespace Automator.Windows
{
    partial class Executor
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
            this.startLabel = new System.Windows.Forms.Label();
            this.mainDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.stepNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.stepLabel = new System.Windows.Forms.Label();
            this.stepMinutesLabel = new System.Windows.Forms.Label();
            this.mainProgressBar = new System.Windows.Forms.ProgressBar();
            this.startButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.mainBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.stepNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Location = new System.Drawing.Point(12, 18);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(51, 13);
            this.startLabel.TabIndex = 0;
            this.startLabel.Text = "Start time";
            // 
            // mainDateTimePicker
            // 
            this.mainDateTimePicker.Location = new System.Drawing.Point(120, 12);
            this.mainDateTimePicker.MinDate = new System.DateTime(2011, 7, 1, 0, 0, 0, 0);
            this.mainDateTimePicker.Name = "mainDateTimePicker";
            this.mainDateTimePicker.Size = new System.Drawing.Size(262, 20);
            this.mainDateTimePicker.TabIndex = 1;
            // 
            // stepNumericUpDown
            // 
            this.stepNumericUpDown.Location = new System.Drawing.Point(120, 38);
            this.stepNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.stepNumericUpDown.Name = "stepNumericUpDown";
            this.stepNumericUpDown.Size = new System.Drawing.Size(63, 20);
            this.stepNumericUpDown.TabIndex = 2;
            this.stepNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // stepLabel
            // 
            this.stepLabel.AutoSize = true;
            this.stepLabel.Location = new System.Drawing.Point(12, 40);
            this.stepLabel.Name = "stepLabel";
            this.stepLabel.Size = new System.Drawing.Size(29, 13);
            this.stepLabel.TabIndex = 3;
            this.stepLabel.Text = "Step";
            // 
            // stepMinutesLabel
            // 
            this.stepMinutesLabel.AutoSize = true;
            this.stepMinutesLabel.Location = new System.Drawing.Point(189, 40);
            this.stepMinutesLabel.Name = "stepMinutesLabel";
            this.stepMinutesLabel.Size = new System.Drawing.Size(43, 13);
            this.stepMinutesLabel.TabIndex = 4;
            this.stepMinutesLabel.Text = "minutes";
            // 
            // mainProgressBar
            // 
            this.mainProgressBar.Location = new System.Drawing.Point(12, 64);
            this.mainProgressBar.Name = "mainProgressBar";
            this.mainProgressBar.Size = new System.Drawing.Size(370, 10);
            this.mainProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.mainProgressBar.TabIndex = 5;
            this.mainProgressBar.Visible = false;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(226, 80);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(307, 80);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButtonClick);
            // 
            // mainBackgroundWorker
            // 
            this.mainBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mainBackgroundWorkerDoWork);
            this.mainBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.mainBackgroundWorkerRunWorkerCompleted);
            // 
            // Executor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 109);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.mainProgressBar);
            this.Controls.Add(this.stepMinutesLabel);
            this.Controls.Add(this.stepLabel);
            this.Controls.Add(this.stepNumericUpDown);
            this.Controls.Add(this.mainDateTimePicker);
            this.Controls.Add(this.startLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Executor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Executor";
            this.Load += new System.EventHandler(this.ExecutorLoad);
            ((System.ComponentModel.ISupportInitialize)(this.stepNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.DateTimePicker mainDateTimePicker;
        private System.Windows.Forms.NumericUpDown stepNumericUpDown;
        private System.Windows.Forms.Label stepLabel;
        private System.Windows.Forms.Label stepMinutesLabel;
        private System.Windows.Forms.ProgressBar mainProgressBar;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button cancelButton;
        private System.ComponentModel.BackgroundWorker mainBackgroundWorker;
    }
}