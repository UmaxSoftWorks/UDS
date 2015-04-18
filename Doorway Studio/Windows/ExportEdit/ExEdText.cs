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
    public partial class ExEdText : Form
    {
        public ExEdText()
        {
            InitializeComponent();
        }

        private void ExEdText_Load(object sender, EventArgs e)
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
            this.Text = View.UILanguageResources.GetString("S0000064") + ": " + View.UILanguageResources.GetString("S0000038");
            textTreeGroupBox.Text = View.UILanguageResources.GetString("S0000038");
            textGroupBox.Text = View.UILanguageResources.GetString("S0000038");

            deleteButton.Text = View.UILanguageResources.GetString("S0000065");
            exportGroupBox.Text = View.UILanguageResources.GetString("S0000035");
            exportButton.Text = View.UILanguageResources.GetString("S0000035");
            cancelButton.Text = View.UILanguageResources.GetString("S0000060");
            encodingLabel.Text = View.UILanguageResources.GetString("S0000066");

            //Меню
            selectAllToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000069");
            copyToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000070");
            pasteToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000071");
            clearToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000072");
        }

        private void UpdateTreeView()
        {
            textTreeView.Nodes.Clear();
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                TreeNode[] textTreeNode = new TreeNode[SharedData.WorkSpaces[i].Texts.Count];
                for (int j = 0; j < SharedData.WorkSpaces[i].Texts.Count; j++)
                {
                    textTreeNode[j] = new TreeNode(SharedData.WorkSpaces[i].Texts[j].Name, 1, 1);
                }
                TreeNode mainTreeNode = new TreeNode(SharedData.WorkSpaces[i].Name, 0, 0, textTreeNode);
                mainTreeNode.Name = SharedData.WorkSpaces[i].Name;
                textTreeView.Nodes.Add(mainTreeNode);
                textTreeView.ExpandAll();
            }
        }

        private void textTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (textTreeView.SelectedNode == null)
            {
                return;
            }
            if (textTreeView.SelectedNode.Parent == null)
            {
                textTextBox.Text = string.Empty;
                return;
            }
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                if (SharedData.WorkSpaces[i].Name == textTreeView.SelectedNode.Parent.Text)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[i].Texts.Count; j++)
                    {
                        if (SharedData.WorkSpaces[i].Texts[j].Name == textTreeView.SelectedNode.Text)
                        {
                            textTextBox.Text = SharedData.WorkSpaces[i].Texts[j].Texts;
                            return;
                        }
                    }
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (textTreeView.SelectedNode == null)
            {
                return;
            }
            if (textTreeView.SelectedNode.Parent == null)
            {
                return;
            }
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                if (SharedData.WorkSpaces[i].Name == textTreeView.SelectedNode.Parent.Text)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[i].Texts.Count; j++)
                    {
                        if (SharedData.WorkSpaces[i].Texts[j].Name == textTreeView.SelectedNode.Text)
                        {
                            SharedData.WorkSpaces[i].DeleteText(SharedData.WorkSpaces[i].Texts[j].ID);
                            UpdateTreeView();
                            return;
                        }
                    }
                }
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (encodingComboBox.SelectedIndex == -1)
            {
                return;
            }
            saveTextFileDialog.FileName = string.Empty;
            saveTextFileDialog.ShowDialog();
            if (saveTextFileDialog.FileName == string.Empty)
            {
                return;
            }
            if (encodingComboBox.SelectedIndex == 0)
            {
                File.WriteAllText(saveTextFileDialog.FileName, textTextBox.Text, Encoding.Default);
            }
            else
            {
                File.WriteAllText(saveTextFileDialog.FileName, textTextBox.Text, Encoding.UTF8);
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (textTreeView.SelectedNode == null)
            {
                return;
            }
            if (textTreeView.SelectedNode.Parent == null)
            {
                return;
            }
            bool ok = false;
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                if (SharedData.WorkSpaces[i].Name == textTreeView.SelectedNode.Parent.Text)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[i].Keywords.Count; j++)
                    {
                        if (SharedData.WorkSpaces[i].Texts[j].Name == textTreeView.SelectedNode.Text)
                        {
                            SharedData.WorkSpaces[i].Texts[j].Texts = textTextBox.Text;
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tipsTimer_Tick(object sender, EventArgs e)
        {
            TipsTextBox.Text = View.UILanguageResources.GetString("S0001013");
        }

        private void ExEdText_FormClosing(object sender, FormClosingEventArgs e)
        {
            tipsTimer.Stop();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textTextBox.SelectAll();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textTextBox.Text);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textTextBox.Text = Clipboard.GetText();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textTextBox.Clear();
        }
    }
}
