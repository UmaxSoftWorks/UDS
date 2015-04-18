using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

namespace Doorway_Studio
{
    public partial class ExEdKeywords : Form
    {
        public ExEdKeywords()
        {
            InitializeComponent();
        }

        private void ExEdKeywords_Load(object sender, EventArgs e)
        {
            this.Icon = Resource.MainIcon;
            FillOutForm();
            encodingComboBox.SelectedIndex = 0;
            UpdateTreeView();
            tipsTimer.Start();
            tipsTimer_Tick(sender, e);
        }

        private void FillOutForm()
        {
            this.Text = View.UILanguageResources.GetString("S0000064") + ": " + View.UILanguageResources.GetString("S0000036");
            keywordsTreeGroupBox.Text = View.UILanguageResources.GetString("S0000036");
            keywordsGroupBox.Text = View.UILanguageResources.GetString("S0000036");

            deleteButton.Text = View.UILanguageResources.GetString("S0000065");
            exportButton.Text = View.UILanguageResources.GetString("S0000035");
            encodingLabel.Text = View.UILanguageResources.GetString("S0000066");
            exportGroupBox.Text = View.UILanguageResources.GetString("S0000035");
            cancelButton.Text = View.UILanguageResources.GetString("S0000060");

            //Меню
            selectAllToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000069");
            copyToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000070");
            pasteToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000071");
            clearToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000072");
        }

        private void UpdateTreeView()
        {
            keywordsTreeView.Nodes.Clear();
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                TreeNode[] keywordsTreeNode = new TreeNode[SharedData.WorkSpaces[i].Keywords.Count];
                for (int j = 0; j < SharedData.WorkSpaces[i].Keywords.Count; j++)
                {
                    keywordsTreeNode[j] = new TreeNode(SharedData.WorkSpaces[i].Keywords[j].Name, 1, 1);
                }
                TreeNode mainTreeNode = new TreeNode(SharedData.WorkSpaces[i].Name, 0, 0, keywordsTreeNode);
                mainTreeNode.Name = SharedData.WorkSpaces[i].Name;
                keywordsTreeView.Nodes.Add(mainTreeNode);
                mainTreeNode.ExpandAll();
            }
        }

        private void ExEdKeywords_FormClosing(object sender, FormClosingEventArgs e)
        {
            tipsTimer.Stop();
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (encodingComboBox.SelectedIndex == -1)
            {
                return;
            }
            saveKeywordsFileDialog.FileName = string.Empty;
            saveKeywordsFileDialog.ShowDialog();
            if (saveKeywordsFileDialog.FileName == string.Empty)
            {
                return;
            }
            if (encodingComboBox.SelectedIndex == 0)
            {
                File.WriteAllText(saveKeywordsFileDialog.FileName, keywordsTextBox.Text, Encoding.Default);
            }
            else
            {
                File.WriteAllText(saveKeywordsFileDialog.FileName, keywordsTextBox.Text, Encoding.UTF8);
            }
        }

        private void tipsTimer_Tick(object sender, EventArgs e)
        {
            TipsTextBox.Text = View.UILanguageResources.GetString("S0001013");
        }

        private void keywordsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (keywordsTreeView.SelectedNode == null)
            {
                return;
            }
            if (keywordsTreeView.SelectedNode.Parent == null)
            {
                keywordsTextBox.Text = string.Empty;
                return;
            }
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                if (SharedData.WorkSpaces[i].Name == keywordsTreeView.SelectedNode.Parent.Text)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[i].Keywords.Count; j++)
                    {
                        if (SharedData.WorkSpaces[i].Keywords[j].Name == keywordsTreeView.SelectedNode.Text)
                        {
                            StringBuilder data = new StringBuilder(10000);
                            for (int k = 0; k < SharedData.WorkSpaces[i].Keywords[j].Items.Length; k++)
                            {
                                data.Append(SharedData.WorkSpaces[i].Keywords[j].Items[k] + "\r\n");
                            }
                            keywordsTextBox.Text = data.ToString();
                            return;
                        }
                    }
                }
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            keywordsTextBox.SelectAll();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(keywordsTextBox.Text);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            keywordsTextBox.Text = Clipboard.GetText();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            keywordsTextBox.Clear();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (keywordsTreeView.SelectedNode == null)
            {
                return;
            }
            if (keywordsTreeView.SelectedNode.Parent == null)
            {
                return;
            }
            bool ok = false;
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                if (SharedData.WorkSpaces[i].Name == keywordsTreeView.SelectedNode.Parent.Text)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[i].Keywords.Count; j++)
                    {
                        if (SharedData.WorkSpaces[i].Keywords[j].Name == keywordsTreeView.SelectedNode.Text)
                        {
                            SharedData.WorkSpaces[i].Keywords[j].Items = keywordsTextBox.Text.Replace("\r\n", "¥").Split(new char[] { '¥' }, StringSplitOptions.RemoveEmptyEntries);
                            SharedData.WorkSpaces[i].Save();
                            ok = true;
                            break;
                        }
                    }
                }
                if (ok)
                {
                    break;
                }
            }
            Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (keywordsTreeView.SelectedNode == null)
            {
                return;
            }
            if (keywordsTreeView.SelectedNode.Parent == null)
            {
                return;
            }
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                if (SharedData.WorkSpaces[i].Name == keywordsTreeView.SelectedNode.Parent.Text)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[i].Keywords.Count; j++)
                    {
                        if (SharedData.WorkSpaces[i].Keywords[j].Name == keywordsTreeView.SelectedNode.Text)
                        {
                            SharedData.WorkSpaces[i].DeleteKeyWord(SharedData.WorkSpaces[i].Keywords[j].ID);
                            UpdateTreeView();
                            return;
                        }
                    }
                }
            }
        }
    }
}
