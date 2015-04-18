using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Doorway_Studio
{
    public partial class LogSpam : Form
    {
        public LogSpam()
        {
            InitializeComponent();
        }

        private int showType;

        /// <summary>
        /// 0 - log; 1 - spam
        /// </summary>
        public int ShowType
        {
            get
            {
                return showType;
            }
            set
            {
                showType = value;
            }
        }

        private string taskName;

        public string TaskName
        {
            get
            {
                return taskName;
            }
            set
            {
                taskName = value;
            }
        }

        private int wsIndex;

        public int WSIndex
        {
            get
            {
                return wsIndex;
            }
            set
            {
                wsIndex = value;
            }
        }

        private int taskIndex;

        public int TaskIndex
        {
            get
            {
                return taskIndex;
            }
            set
            {
                taskIndex = value;
            }
        }

        private void FillOutForm()
        {
            this.Text = View.UILanguageResources.GetString("S0000075") + this.Text;

            logTabPage.Text = View.UILanguageResources.GetString("S0000075");

            logDoorwaysGroupBox.Text = View.UILanguageResources.GetString("S0000020");

            spamDoorwaysGroupBox.Text = View.UILanguageResources.GetString("S0000020");
        }

        private void Log_Load(object sender, EventArgs e)
        {
            this.Icon = Resource.MainIcon;
            FillOutForm();
            this.Text += taskName;

            logDoorwaysTreeView.Nodes.Clear();
            //Log Tree
            TreeNode[] doorwayNodes = new TreeNode[SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].Doorways.Count];
            for (int i = 0; i < SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].Doorways.Count; i++)
            {
                doorwayNodes[i] = new TreeNode("Doorway #" + (i + 1).ToString(), 1, 1);
            }
            TreeNode MainNode = new TreeNode(SharedData.WorkSpaces[wsIndex].Tasks[taskIndex].Name, 0, 0, doorwayNodes);

            logDoorwaysTreeView.Nodes.Add(MainNode);

            logDoorwaysTreeView.ExpandAll();

            spamDoorwaysTreeView.Nodes.Clear();
            //Spam Tree
            doorwayNodes = new TreeNode[SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].Doorways.Count];
            for (int i = 0; i < SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].Doorways.Count; i++)
            {
                doorwayNodes[i] = new TreeNode("Doorway #" + (i + 1).ToString(), 1, 1);
            }
            MainNode = new TreeNode(SharedData.WorkSpaces[wsIndex].Tasks[taskIndex].Name, 0, 0, doorwayNodes);

            spamDoorwaysTreeView.Nodes.Add(MainNode);

            spamDoorwaysTreeView.ExpandAll();


            mainTabControl.SelectedIndex = showType;

            tipsTimer.Start();
            tipsTimer_Tick(sender, e);
        }

        private void Log_FormClosing(object sender, FormClosingEventArgs e)
        {
            tipsTimer.Stop();
        }

        private void tipsTimer_Tick(object sender, EventArgs e)
        {
            TipsTextBox.Text = View.UILanguageResources.GetString("S000101" + new Random().Next(4, 7).ToString());
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveLogSpamFileDialog.FileName = string.Empty;
            SaveLogSpamFileDialog.ShowDialog();
            if (SaveLogSpamFileDialog.FileName == string.Empty)
            {
                return;
            }
            switch (mainContextMenuStrip.SourceControl.Name)
            {
                case "logContentTextBox":
                    {
                        File.WriteAllText(SaveLogSpamFileDialog.FileName, logContentTextBox.Text, Encoding.UTF8);
                        break;
                    }
                case "spamContentTextBox":
                    {
                        File.WriteAllText(SaveLogSpamFileDialog.FileName, spamContentTextBox.Text, Encoding.UTF8);
                        break;
                    }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (mainContextMenuStrip.SourceControl.Name)
            {
                case "logContentTextBox":
                    {
                        Clipboard.SetText(logContentTextBox.Text);
                        break;
                    }
                case "spamContentTextBox":
                    {
                        Clipboard.SetText(spamContentTextBox.Text);
                        break;
                    }
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (mainContextMenuStrip.SourceControl.Name)
            {
                case "logContentTextBox":
                    {
                        logContentTextBox.SelectAll();
                        break;
                    }
                case "spamContentTextBox":
                    {
                        spamContentTextBox.SelectAll();
                        break;
                    }
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (mainContextMenuStrip.SourceControl.Name)
            {
                case "logContentTextBox":
                    {
                        logContentTextBox.Text = string.Empty;
                        break;
                    }
                case "spamContentTextBox":
                    {
                        spamContentTextBox.Text = string.Empty;
                        break;
                    }
            }
        }

        private void logDoorwaysTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (logDoorwaysTreeView.SelectedNode.Index == -1 || logDoorwaysTreeView.SelectedNode == null)
            {
                logContentGroupBox.Text = string.Empty;
                logContentTextBox.Text = string.Empty;
                return;
            }

            logContentGroupBox.Text = logDoorwaysTreeView.SelectedNode.Text;
            if (logDoorwaysTreeView.SelectedNode.Parent == null)
            {
                logContentTextBox.Text = SharedData.WorkSpaces[wsIndex].Tasks[taskIndex].Log.ToString();
            }
            else
            {
                logContentTextBox.Text = SharedData.WorkSpaces[wsIndex].Tasks[taskIndex].Doorways[int.Parse(logDoorwaysTreeView.SelectedNode.Text.Substring(logDoorwaysTreeView.SelectedNode.Text.IndexOf("#") + 1)) - 1].Log.ToString();
            }
        }

        private void spamDoorwaysTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (spamDoorwaysTreeView.SelectedNode.Index == -1 || spamDoorwaysTreeView.SelectedNode.Parent == null || spamDoorwaysTreeView.SelectedNode == null)
            {
                spamContentGroupBox.Text = string.Empty;
                spamContentTextBox.Text = string.Empty;
                return;
            }

            spamContentGroupBox.Text = spamDoorwaysTreeView.SelectedNode.Text;
            spamContentTextBox.Text = SharedData.WorkSpaces[wsIndex].Tasks[taskIndex].Doorways[int.Parse(spamDoorwaysTreeView.SelectedNode.Text.Substring(spamDoorwaysTreeView.SelectedNode.Text.IndexOf("#") + 1)) - 1].Spam.ToString();
        }
    }
}
