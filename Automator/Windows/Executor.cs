using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Automator.Enums;
using Automator.Items;
using Automator.Properties;
using Doorway_Studio;
using Settings;

namespace Automator.Windows
{
    public partial class Executor : Form
    {
        public Executor()
        {
            InitializeComponent();
        }

        private void ExecutorLoad(object sender, EventArgs e)
        {
            this.Icon = Resources.umaxsoft;

            this.InitializeUI();

            mainDateTimePicker.Value = DateTime.Now.AddHours(1);
        }

        private void InitializeUI()
        {
            this.Text = UI.Manager.GetString("S000039");

            startLabel.Text = UI.Manager.GetString("S000041");
            stepLabel.Text = UI.Manager.GetString("S000042");
            stepMinutesLabel.Text = UI.Manager.GetString("S000043");

            startButton.Text = UI.Manager.GetString("S000040");
            cancelButton.Text = UI.Manager.GetString("S000023");
        }

        public List<string> URLs { get; set; }

        public List<FileMacross> FileTokens { get; set; }

        public List<FTPSettings> FTP { get; set; }

        public List<TagSettings> Tags { get; set; }

        public PresetSettings Settings { get; set; }

        public int WorkSpaceIndex { get; set; }

        public int PresetIndex { get; set; }

        public int TemplateIndex { get; set; }

        public List<int> KeywordsIndexes { get; set; }

        public List<int> TextIndexes { get; set; }

        public int TaskCount { get; set; }

        private void cancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void startButtonClick(object sender, EventArgs e)
        {
            if (BlackList.CheckStudio() > 0)
            {
                MessageBox.Show("The Doorway Studio already running!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (BlackList.CheckAutomator() > 1)
            {
                MessageBox.Show("Automator already running!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mainProgressBar.Visible = true;
            mainBackgroundWorker.RunWorkerAsync();
        }

        private void mainBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            // Find latest task id
            int taskID = -1;
            for (int i = 0; i < Data.Instance.Items[this.WorkSpaceIndex].Tasks.Count; i++)
            {
                if (taskID < Data.Instance.Items[this.WorkSpaceIndex].Tasks[i].ID)
                {
                    taskID = Data.Instance.Items[this.WorkSpaceIndex].Tasks[i].ID;
                }
            }

            taskID++;

            Random random = new Random(Environment.TickCount);

            string workSpacePath = Path.Combine(Application.StartupPath, "Data");

            string stringWorkSpaceID = Data.Instance.Items[this.WorkSpaceIndex].ID.ToString();
            while (stringWorkSpaceID.Length < 7)
            {
                stringWorkSpaceID = "0" + stringWorkSpaceID;
            }

            workSpacePath = Path.Combine(Path.Combine(workSpacePath, stringWorkSpaceID), "Tasks");

            for (int i = 0; i < this.TaskCount; i++)
            {
                try
                {
                    // Prepare
                    Task task = new Task(taskID, "Auto " + mainDateTimePicker.Value.AddMinutes((int)this.stepNumericUpDown.Value * i));

                    task.StartTime = mainDateTimePicker.Value.AddMinutes((int)this.stepNumericUpDown.Value * i);
                    task.EndTime = mainDateTimePicker.Value.AddMinutes((int)this.stepNumericUpDown.Value * i);

                    task.PresetID = (this.PresetIndex == -1) ? -1 : Data.Instance.Items[this.WorkSpaceIndex].Presets[this.PresetIndex].ID;
                    task.TemplateID = Data.Instance.Items[this.WorkSpaceIndex].Templates[this.TemplateIndex].ID;

                    task.TextID = GetTextID(random);

                    task.Settings = this.Settings;
                    task.Settings.KeywordsID = GetKeywordsID(random);

                    // URL
                    if ((i * this.Settings.GeneralCreateDoorways) < this.URLs.Count)
                    {
                        List<string> urls = new List<string>();
                        for (int k = 0; k < this.Settings.GeneralCreateDoorways; k++)
                        {
                            if ((i * this.Settings.GeneralCreateDoorways + k) < this.URLs.Count)
                            {
                                urls.Add(this.URLs[(i * this.Settings.GeneralCreateDoorways + k)]);
                            }
                        }

                        this.Settings.GeneralDoorwayUrls = urls.ToArray();
                    }
                    else
                    {
                        this.Settings.GeneralDoorwayUrls = new string[0];
                    }

                    // File tokens
                    task.Settings.FileMacroses = this.FileTokens.ToArray();

                    // FTP settings
                    if ((i * this.Settings.GeneralCreateDoorways) < this.FTP.Count)
                    {
                        List<FTPSettings> ftp = new List<FTPSettings>();
                        for (int k = 0; k < this.Settings.GeneralCreateDoorways; k++)
                        {
                            if ((i * this.Settings.GeneralCreateDoorways + k) < this.FTP.Count)
                            {
                                ftp.Add(this.FTP[(i * this.Settings.GeneralCreateDoorways + k)]);
                            }
                        }

                        this.Settings.FTPSettings = ftp.ToArray();
                    }

                    // Tags settings
                    this.Settings.TagSettings = this.Tags.ToArray();

                    // ID
                    string stringTaskID = taskID.ToString();
                    while (stringTaskID.Length < 7)
                    {
                        stringTaskID = "0" + stringTaskID;
                    }

                    // Save
                    task.Save(Path.Combine(workSpacePath, stringTaskID));
                    Data.Instance.Items[this.WorkSpaceIndex].Tasks.Add(task);

                    taskID++;
                }
                catch (Exception) { }
            }
        }

        private void mainBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        internal ItemUsage KeywordsAndTextUsage { get; set; }

        private int usedTextIDCount = 0;
        private int GetTextID(Random random)
        {
            if (KeywordsAndTextUsage == ItemUsage.Random)
            {
                return Data.Instance.Items[this.WorkSpaceIndex].Texts[this.TextIndexes[random.Next(this.TextIndexes.Count)]].ID;
            }

            if (usedTextIDCount >= this.TextIndexes.Count)
            {
                usedTextIDCount = 0;
            }

            return Data.Instance.Items[this.WorkSpaceIndex].Texts[this.TextIndexes[usedTextIDCount++]].ID;
        }

        private int usedKeywordIDCount = 0;
        private int GetKeywordsID(Random random)
        {
            if (KeywordsAndTextUsage == ItemUsage.Random)
            {
                return Data.Instance.Items[this.WorkSpaceIndex].Keywords[this.KeywordsIndexes[random.Next(this.KeywordsIndexes.Count)]].ID;
            }

            if (usedKeywordIDCount >= this.KeywordsIndexes.Count)
            {
                usedKeywordIDCount = 0;
            }

            return Data.Instance.Items[this.WorkSpaceIndex].Keywords[this.KeywordsIndexes[usedKeywordIDCount++]].ID;
        }
    }
}
