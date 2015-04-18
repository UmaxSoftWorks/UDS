using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Automator.Classes;
using Automator.Enums;
using Automator.Properties;
using Automator.Windows;
using Doorway_Studio;
using Settings;

namespace Automator
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            this.Initialize();
        }

        private void Initialize()
        {
            this.Settings = new PresetSettings();
            this.FTP = new List<FTPSettings>();
            this.FileTokens = new List<FileMacross>();
            this.URLs = new List<string>();
            this.Tags = new List<TagSettings>();
        }

        protected PresetSettings Settings { get; set; }

        protected List<FTPSettings> FTP { get; set; }

        protected List<FileMacross> FileTokens { get; set; }

        protected List<string> URLs { get; set; }

        protected List<TagSettings> Tags { get; set; }

        private void MainLoad(object sender, EventArgs e)
        {
            this.Icon = Resources.umaxsoft;

            this.Text = "Umax " + this.Text;

            this.InitializeUI();

            workSpaceComboBox.Items.Clear();
            for (int i = 0; i < Data.Instance.Items.Count; i++)
            {
                workSpaceComboBox.Items.Add(Data.Instance.Items[i].Name);
            }

            if (workSpaceComboBox.Items.Count != 0)
            {
                workSpaceComboBox.SelectedIndex = 0;
            }

            this.ApplySetting();
        }

        private void InitializeUI()
        {
            fileToolStripMenuItem.Text = UI.Manager.GetString("S000009");
            newToolStripMenuItem.Text = UI.Manager.GetString("S000010");
            openToolStripMenuItem.Text = UI.Manager.GetString("S000011");
            exitToolStripMenuItem.Text = UI.Manager.GetString("S000012");

            helpToolStripMenuItem.Text = UI.Manager.GetString("S000013");
            aboutToolStripMenuItem.Text = UI.Manager.GetString("S000014");

            presetLabel.Text = UI.Manager.GetString("S000015");
            templateLabel.Text = UI.Manager.GetString("S000016");
            keywordsLabel.Text = UI.Manager.GetString("S000017");
            textLabel.Text = UI.Manager.GetString("S000018");
            tasksLabel.Text = UI.Manager.GetString("S000019");

            urlsButton.Text = UI.Manager.GetString("S000020");
            tokensButton.Text = UI.Manager.GetString("S000021");
            tagsButton.Text = UI.Manager.GetString("S000045");
            startButton.Text = UI.Manager.GetString("S000022");
            KTUsageLabel.Text = UI.Manager.GetString("S000046");
        }

        private void ApplySetting()
        {
            this.Settings.SetResourceManager(UI.Manager);
            this.mainPropertyGrid.SelectedObject = this.Settings;

            this.URLs = this.Settings.GeneralDoorwayUrls.ToList();
            this.FTP = this.Settings.FTPSettings.ToList();
            this.FileTokens = this.Settings.FileMacroses.ToList();
            this.Tags = this.Settings.TagSettings.ToList();

            KTUsageComboBox.Items.Clear();
            foreach (ItemUsage value in Enum.GetValues(typeof(ItemUsage)))
            {
                KTUsageComboBox.Items.Add(new ItemUsageComboBoxItem(value));
            }
            KTUsageComboBox.SelectedIndex = 0;
        }

        private void ftpButtonClick(object sender, EventArgs e)
        {
            using (FTP window = new FTP())
            {
                window.Settings = this.FTP;
                window.ShowDialog();

                this.FTP = window.Settings;
            }
        }

        private void urlsButtonClick(object sender, EventArgs e)
        {
            using (URLS window = new URLS())
            {
                window.URLs = this.URLs;

                window.ShowDialog();

                this.URLs = window.URLs;
            }
        }

        private void workSpaceComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (workSpaceComboBox.SelectedIndex == -1)
            {
                return;
            }

            // Fill presets
            presetComboBox.Items.Clear();
            for (int i = 0; i < Data.Instance.Items[workSpaceComboBox.SelectedIndex].Presets.Count; i++)
            {
                presetComboBox.Items.Add(Data.Instance.Items[workSpaceComboBox.SelectedIndex].Presets[i].Name);
            }

            // Fill template
            templateComboBox.Items.Clear();
            for (int i = 0; i < Data.Instance.Items[workSpaceComboBox.SelectedIndex].Templates.Count; i++)
            {
                templateComboBox.Items.Add(Data.Instance.Items[workSpaceComboBox.SelectedIndex].Templates[i].Name);
            }

            if (templateComboBox.Items.Count != 0)
            {
                templateComboBox.SelectedIndex = 0;
            }

            // Fill keywords
            keywordsCheckedListBox.Items.Clear();
            for (int i = 0; i < Data.Instance.Items[workSpaceComboBox.SelectedIndex].Keywords.Count; i++)
            {
                keywordsCheckedListBox.Items.Add(Data.Instance.Items[workSpaceComboBox.SelectedIndex].Keywords[i].Name);
            }

            // Fill Text
            textCheckedListBox.Items.Clear();
            for (int i = 0; i < Data.Instance.Items[workSpaceComboBox.SelectedIndex].Texts.Count; i++)
            {
                textCheckedListBox.Items.Add(Data.Instance.Items[workSpaceComboBox.SelectedIndex].Texts[i].Name);
            }
        }

        private void presetComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (presetComboBox.SelectedIndex == -1)
            {
                return;
            }

            // Change template
            int index = -1;

            for (int i = 0; i < Data.Instance.Items[workSpaceComboBox.SelectedIndex].Templates.Count; i++)
            {
                if (Data.Instance.Items[workSpaceComboBox.SelectedIndex].Presets[presetComboBox.SelectedIndex].TemplateID == Data.Instance.Items[workSpaceComboBox.SelectedIndex].Templates[i].ID)
                {
                    index = i;
                    break;
                }
            }

            templateComboBox.SelectedIndex = index;

            // Change keywords
            for (int i = 0; i < keywordsCheckedListBox.Items.Count; i++)
            {
                keywordsCheckedListBox.SetItemChecked(i, false);
            }

            index = -1;

            for (int i = 0; i < Data.Instance.Items[workSpaceComboBox.SelectedIndex].Keywords.Count; i++)
            {
                if (Data.Instance.Items[workSpaceComboBox.SelectedIndex].Presets[presetComboBox.SelectedIndex].Settings.KeywordsID == Data.Instance.Items[workSpaceComboBox.SelectedIndex].Keywords[i].ID)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                keywordsCheckedListBox.SetItemChecked(index, true);
            }

            // Change text
            for (int i = 0; i < textCheckedListBox.Items.Count; i++)
            {
                textCheckedListBox.SetItemChecked(i, false);
            }

            index = -1;

            for (int i = 0; i < Data.Instance.Items[workSpaceComboBox.SelectedIndex].Texts.Count; i++)
            {
                if (Data.Instance.Items[workSpaceComboBox.SelectedIndex].Presets[presetComboBox.SelectedIndex].TextID == Data.Instance.Items[workSpaceComboBox.SelectedIndex].Texts[i].ID)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                textCheckedListBox.SetItemChecked(index, true);
            }

            // Load new task
            this.Settings = Data.Instance.Items[workSpaceComboBox.SelectedIndex].Presets[presetComboBox.SelectedIndex].Settings;
            this.ApplySetting();
        }

        private void exitToolStripMenuItemExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void newToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.Initialize();
            this.ApplySetting();
        }

        private void openToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (Tasks window = new Tasks())
            {
                window.ShowDialog();

                if (window.Task != null)
                {
                    this.Initialize();

                    // Set workspace
                    workSpaceComboBox.SelectedIndex = window.WorkSpaceIndex;

                    // Set preset
                    int index = -1;
                    for (int i = 0; i < Data.Instance.Items[window.WorkSpaceIndex].Presets.Count; i++)
                    {
                        if (Data.Instance.Items[window.WorkSpaceIndex].Presets[i].ID == window.Task.PresetID)
                        {
                            index = i;
                            break;
                        }
                    }

                    presetComboBox.SelectedIndex = index;

                    // Set template
                    index = -1;
                    for (int i = 0; i < Data.Instance.Items[window.WorkSpaceIndex].Templates.Count; i++)
                    {
                        if (Data.Instance.Items[window.WorkSpaceIndex].Templates[i].ID == window.Task.TemplateID)
                        {
                            index = i;
                            break;
                        }
                    }

                    templateComboBox.SelectedIndex = index;

                    // Set keywords
                    for (int i = 0; i < keywordsCheckedListBox.Items.Count; i++)
                    {
                        keywordsCheckedListBox.SetItemChecked(i, false);
                    }

                    index = -1;

                    for (int i = 0; i < Data.Instance.Items[window.WorkSpaceIndex].Keywords.Count; i++)
                    {
                        if (Data.Instance.Items[window.WorkSpaceIndex].Keywords[i].ID == window.Task.Settings.KeywordsID)
                        {
                            index = i;
                            break;
                        }
                    }

                    if (index != -1)
                    {
                        keywordsCheckedListBox.SetItemChecked(index, true);
                    }

                    // Set text
                    for (int i = 0; i < textCheckedListBox.Items.Count; i++)
                    {
                        textCheckedListBox.SetItemChecked(i, false);
                    }

                    index = -1;

                    for (int i = 0; i < Data.Instance.Items[window.WorkSpaceIndex].Texts.Count; i++)
                    {
                        if (Data.Instance.Items[window.WorkSpaceIndex].Texts[i].ID == window.Task.TextID)
                        {
                            index = i;
                            break;
                        }
                    }

                    if (index != -1)
                    {
                        textCheckedListBox.SetItemChecked(index, true);
                    }

                    this.Settings = window.Task.Settings;
                    this.ApplySetting();
                }
            }
        }

        private void tokensButtonClick(object sender, EventArgs e)
        {
            using (Tokens window = new Tokens())
            {
                window.Settings = this.FileTokens;

                window.ShowDialog();

                this.FileTokens = window.Settings;
            }
        }

        private void startButtonClick(object sender, EventArgs e)
        {
            // Check settings
            if (workSpaceComboBox.SelectedIndex == -1)
            {
                return;
            }

            if (templateComboBox.SelectedIndex == -1)
            {
                return;
            }

            if (keywordsCheckedListBox.CheckedItems.Count == 0)
            {
                return;
            }

            List<int> keywords = new List<int>();
            for (int i = 0; i < keywordsCheckedListBox.Items.Count; i++)
            {
                if (keywordsCheckedListBox.GetItemChecked(i))
                {
                    keywords.Add(i);
                }
            }

            if (textCheckedListBox.CheckedItems.Count == 0)
            {
                return;
            }

            List<int> text = new List<int>();
            for (int i = 0; i < textCheckedListBox.Items.Count; i++)
            {
                if (textCheckedListBox.GetItemChecked(i))
                {
                    text.Add(i);
                }
            }

            // Execute
            using (Executor window = new Executor())
            {
                window.WorkSpaceIndex = workSpaceComboBox.SelectedIndex;
                window.PresetIndex = presetComboBox.SelectedIndex;
                window.TemplateIndex = templateComboBox.SelectedIndex;

                window.KeywordsIndexes = keywords;
                window.TextIndexes = text;
                window.KeywordsAndTextUsage = KTUsageComboBox.SelectedItem == null || !(KTUsageComboBox.SelectedItem is ItemUsageComboBoxItem)
                                                  ? ItemUsage.Random
                                                  : (KTUsageComboBox.SelectedItem as ItemUsageComboBoxItem).Usage;

                window.TaskCount = (int)tasksNumericUpDown.Value;

                window.URLs = this.URLs;
                window.FileTokens = this.FileTokens;
                window.FTP = this.FTP;
                window.Tags = this.Tags;
                window.Settings = this.Settings;

                window.ShowDialog();
            }
        }

        private void aboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            MessageBox.Show(this.Text, UI.Manager.GetString("S000014"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tagsButtonClick(object sender, EventArgs e)
        {
            using (Tags window = new Tags())
            {
                window.Settings = this.Tags;
                window.ShowDialog();

                this.Tags = window.Settings;
            }
        }

        bool keywordsChecked = false;
        private void keywordsButton_Click(object sender, EventArgs e)
        {
            keywordsChecked = !keywordsChecked;

            for (int i = 0; i < keywordsCheckedListBox.Items.Count; i++)
            {
                keywordsCheckedListBox.SetItemChecked(i, keywordsChecked);
            }
        }

        bool textChecked = false;
        private void textButton_Click(object sender, EventArgs e)
        {
            textChecked = !textChecked;

            for (int i = 0; i < textCheckedListBox.Items.Count; i++)
            {
                textCheckedListBox.SetItemChecked(i, textChecked);
            }
        }
    }
}
