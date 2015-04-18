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
    public partial class ExEdTemplates : Form
    {
        public ExEdTemplates()
        {
            InitializeComponent();
        }

        private void ExEdTemplates_Load(object sender, EventArgs e)
        {
            this.Icon = Resource.MainIcon;
            FillOutForm();
            UpdateTreeView();
            tipsTimer.Start();
            tipsTimer_Tick(sender, e);
        }
        
        private void FillOutForm()
        {
            this.Text = View.UILanguageResources.GetString("S0000064") + ": " + View.UILanguageResources.GetString("S0000037");
            templatesTreeGroupBox.Text = View.UILanguageResources.GetString("S0000037");
            contentGroupBox.Text = View.UILanguageResources.GetString("S0000068");

            deleteButton.Text = View.UILanguageResources.GetString("S0000065");
            exportButton.Text = View.UILanguageResources.GetString("S0000035");
            applyButton.Text = View.UILanguageResources.GetString("S0000067");
            cancelButton.Text = View.UILanguageResources.GetString("S0000060");

            //Меню
            selectAllToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000069");
            copyToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000070");
            pasteToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000071");
            clearToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000072");
        }

        private void UpdateTreeView()
        {
            templatesTreeView.Nodes.Clear();
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                TreeNode[] templatesTreeNode = new TreeNode[SharedData.WorkSpaces[i].Templates.Count];
                TreeNode[][] contentTreeNode = new TreeNode[SharedData.WorkSpaces[i].Templates.Count][];
                for (int j = 0; j < SharedData.WorkSpaces[i].Templates.Count; j++)
                {
                    contentTreeNode[j] = new TreeNode[1 + SharedData.WorkSpaces[i].Templates[j].Categories.Count + SharedData.WorkSpaces[i].Templates[j].Pages.Count + SharedData.WorkSpaces[i].Templates[j].StaticPages.Count + SharedData.WorkSpaces[i].Templates[j].Map.Count + SharedData.WorkSpaces[i].Templates[j].CustomPages.Count];
                    contentTreeNode[j][0] = new TreeNode("Index", 2, 2);
                    for (int k = 0; k < SharedData.WorkSpaces[i].Templates[j].Categories.Count; k++)
                    {
                        contentTreeNode[j][k + 1] = new TreeNode("Category #" + (k + 1).ToString(), 2, 2);
                    }
                    for (int k = 0; k < SharedData.WorkSpaces[i].Templates[j].Pages.Count; k++)
                    {
                        contentTreeNode[j][k + 1 + SharedData.WorkSpaces[i].Templates[j].Categories.Count] = new TreeNode("Page #" + (k + 1).ToString(), 2, 2);
                    }
                    for (int k = 0; k < SharedData.WorkSpaces[i].Templates[j].StaticPages.Count; k++)
                    {
                        contentTreeNode[j][k + 1 + SharedData.WorkSpaces[i].Templates[j].Categories.Count + SharedData.WorkSpaces[i].Templates[j].Pages.Count] = new TreeNode("Static #" + (k + 1).ToString(), 2, 2);
                    }
                    for (int k = 0; k < SharedData.WorkSpaces[i].Templates[j].Map.Count; k++)
                    {
                        contentTreeNode[j][k + 1 + SharedData.WorkSpaces[i].Templates[j].Categories.Count + SharedData.WorkSpaces[i].Templates[j].Pages.Count + SharedData.WorkSpaces[i].Templates[j].StaticPages.Count] = new TreeNode("Map #" + (k + 1).ToString(), 2, 2);
                    }
                    for (int k = 0; k < SharedData.WorkSpaces[i].Templates[j].CustomPages.Count; k++)
                    {
                        contentTreeNode[j][k + 1 + SharedData.WorkSpaces[i].Templates[j].Categories.Count + SharedData.WorkSpaces[i].Templates[j].Pages.Count + SharedData.WorkSpaces[i].Templates[j].StaticPages.Count + SharedData.WorkSpaces[i].Templates[j].Map.Count] = new TreeNode("Custom #" + (k + 1).ToString(), 2, 2);
                    }
                    templatesTreeNode[j] = new TreeNode(SharedData.WorkSpaces[i].Templates[j].Name, 1, 1, contentTreeNode[j]);
                }
                TreeNode mainTreeNode = new TreeNode(SharedData.WorkSpaces[i].Name, 0, 0, templatesTreeNode);
                mainTreeNode.Name = SharedData.WorkSpaces[i].Name;
                templatesTreeView.Nodes.Add(mainTreeNode);
                mainTreeNode.ExpandAll();
            }
        }

        private void ExEdTemplates_FormClosing(object sender, FormClosingEventArgs e)
        {
            tipsTimer.Stop();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contentTextBox.SelectAll();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(contentTextBox.Text);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contentTextBox.Text = Clipboard.GetText();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contentTextBox.Text = string.Empty;
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            //Определение шаблона
            if (templatesTreeView.SelectedNode == null)
            {
                contentTextBox.Text = string.Empty;
                return;
            }
            if (templatesTreeView.SelectedNode.Parent == null)
            {
                contentTextBox.Text = string.Empty;
                return;
            }

            contentFolderBrowserDialog.SelectedPath = string.Empty;
            contentFolderBrowserDialog.ShowDialog();
            if (contentFolderBrowserDialog.SelectedPath == string.Empty)
            {
                return;
            }

            if (templatesTreeView.SelectedNode.Parent.Parent == null)
            {
                for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
                {
                    if (SharedData.WorkSpaces[i].Name == templatesTreeView.SelectedNode.Parent.Text)
                    {
                        for (int j = 0; j < SharedData.WorkSpaces[i].Templates.Count; j++)
                        {
                            if (SharedData.WorkSpaces[i].Templates[j].Name == templatesTreeView.SelectedNode.Text)
                            {
                                ExportTemplate(contentFolderBrowserDialog.SelectedPath, SharedData.WorkSpaces[i].ID, SharedData.WorkSpaces[i].Templates[j].ID);
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
                {
                    if (SharedData.WorkSpaces[i].Name == templatesTreeView.SelectedNode.Parent.Parent.Text)
                    {
                        for (int j = 0; j < SharedData.WorkSpaces[i].Templates.Count; j++)
                        {
                            if (SharedData.WorkSpaces[i].Templates[j].Name == templatesTreeView.SelectedNode.Parent.Text)
                            {
                                ExportTemplate(contentFolderBrowserDialog.SelectedPath, SharedData.WorkSpaces[i].ID, SharedData.WorkSpaces[i].Templates[j].ID);
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void ExportTemplate(string Path, int WSID, int TemplateID)
        {
            if (!Path.EndsWith("\\"))
            {
                Path += "\\";
            }
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                if (SharedData.WorkSpaces[i].ID == WSID)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[i].Templates.Count; j++)
                    {
                        if (SharedData.WorkSpaces[i].Templates[j].ID == TemplateID)
                        {
                            //Export
                            try
                            {
                                string WSDirectoryName = WSID.ToString();
                                while (WSDirectoryName.Length < 7)
                                {
                                    WSDirectoryName = "0" + WSDirectoryName;
                                }
                                string templateDirectoryName = TemplateID.ToString();
                                while (templateDirectoryName.Length < 7)
                                {
                                    templateDirectoryName = "0" + templateDirectoryName;
                                }
                                //Получение списка файлов для копирования
                                string sourceDirectory = System.Windows.Forms.Application.StartupPath + "\\Data\\" + WSDirectoryName + "\\Templates\\" + templateDirectoryName + "\\Files\\";
                                string[] fileList = Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories);
                                for (int k = 0; k < fileList.Length; k++)
                                {
                                    try
                                    {
                                        //создание подпапки, если надо
                                        if (fileList[k].Substring(sourceDirectory.Length).Contains("\\"))
                                        {
                                            string tempDirectory = fileList[k].Substring(sourceDirectory.Length).Substring(0, fileList[k].Substring(sourceDirectory.Length).LastIndexOf("\\") + 1);
                                            if (!Directory.Exists(Path + tempDirectory))
                                            {
                                                Directory.CreateDirectory(Path + tempDirectory);
                                                File.SetAttributes(Path + tempDirectory, FileAttributes.Normal);
                                            }
                                        }
                                        File.Copy(fileList[k], Path + fileList[k].Substring(sourceDirectory.Length));
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                            }
                            catch (Exception)
                            {
                            }
                            return;
                        }
                    }
                }
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            ApplyChanges();
            Close();
        }

        private void tipsTimer_Tick(object sender, EventArgs e)
        {
            TipsTextBox.Text = View.UILanguageResources.GetString("S0001013");
        }

        private void templatesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (templatesTreeView.SelectedNode == null)
            {
                contentTextBox.Text = string.Empty;
                return;
            }
            if (templatesTreeView.SelectedNode.Parent == null)
            {
                contentTextBox.Text = string.Empty;
                return;
            }
            if (templatesTreeView.SelectedNode.Parent.Parent == null)
            {
                contentTextBox.Text = string.Empty;
                return;
            }
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                if (SharedData.WorkSpaces[i].Name == templatesTreeView.SelectedNode.Parent.Parent.Text)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[i].Templates.Count; j++)
                    {
                        if (SharedData.WorkSpaces[i].Templates[j].Name == templatesTreeView.SelectedNode.Parent.Text)
                        {
                            if (templatesTreeView.SelectedNode.Text == "Index")
                            {
                                contentTextBox.Text = SharedData.WorkSpaces[i].Templates[j].Index;
                            }
                            else if (templatesTreeView.SelectedNode.Text.StartsWith("Category"))
                            {
                                int itemIndex = int.Parse(templatesTreeView.SelectedNode.Text.Substring(templatesTreeView.SelectedNode.Text.IndexOf("#") + 1)) - 1;
                                contentTextBox.Text = SharedData.WorkSpaces[i].Templates[j].Categories[itemIndex].Content;
                            }
                            else if (templatesTreeView.SelectedNode.Text.StartsWith("Page"))
                            {
                                int itemIndex = int.Parse(templatesTreeView.SelectedNode.Text.Substring(templatesTreeView.SelectedNode.Text.IndexOf("#") + 1)) - 1;
                                contentTextBox.Text = SharedData.WorkSpaces[i].Templates[j].Pages[itemIndex].Content;
                            }
                            else if (templatesTreeView.SelectedNode.Text.StartsWith("Static"))
                            {
                                int itemIndex = int.Parse(templatesTreeView.SelectedNode.Text.Substring(templatesTreeView.SelectedNode.Text.IndexOf("#") + 1)) - 1;
                                contentTextBox.Text = SharedData.WorkSpaces[i].Templates[j].StaticPages[itemIndex].Content;
                            }
                            else if (templatesTreeView.SelectedNode.Text.StartsWith("Custom"))
                            {
                                int itemIndex = int.Parse(templatesTreeView.SelectedNode.Text.Substring(templatesTreeView.SelectedNode.Text.IndexOf("#") + 1)) - 1;
                                contentTextBox.Text = SharedData.WorkSpaces[i].Templates[j].CustomPages[itemIndex].Content;
                            }
                            else if (templatesTreeView.SelectedNode.Text.StartsWith("Map"))
                            {
                                int itemIndex = int.Parse(templatesTreeView.SelectedNode.Text.Substring(templatesTreeView.SelectedNode.Text.IndexOf("#") + 1)) - 1;
                                contentTextBox.Text = SharedData.WorkSpaces[i].Templates[j].Map[itemIndex].Content;
                            }
                            SharedData.WorkSpaces[i].Save();
                            return;
                        }
                    }
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (templatesTreeView.SelectedNode == null)
            {
                contentTextBox.Text = string.Empty;
                return;
            }
            if (templatesTreeView.SelectedNode.Parent == null)
            {
                contentTextBox.Text = string.Empty;
                return;
            }
            if (templatesTreeView.SelectedNode.Parent.Parent == null)
            {
                for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
                {
                    if (SharedData.WorkSpaces[i].Name == templatesTreeView.SelectedNode.Parent.Text)
                    {
                        for (int j = 0; j < SharedData.WorkSpaces[i].Templates.Count; j++)
                        {
                            if (SharedData.WorkSpaces[i].Templates[j].Name == templatesTreeView.SelectedNode.Text)
                            {
                                SharedData.WorkSpaces[i].DeleteTemplate(SharedData.WorkSpaces[i].Templates[j].ID);
                                UpdateTreeView();
                                contentTextBox.Text = string.Empty;
                                return;
                            }
                        }
                    }
                }
                return;
            }
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                if (SharedData.WorkSpaces[i].Name == templatesTreeView.SelectedNode.Parent.Parent.Text)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[i].Templates.Count; j++)
                    {
                        if (SharedData.WorkSpaces[i].Templates[j].Name == templatesTreeView.SelectedNode.Parent.Text)
                        {
                            SharedData.WorkSpaces[i].DeleteTemplate(SharedData.WorkSpaces[i].Templates[j].ID);
                            UpdateTreeView();
                            contentTextBox.Text = string.Empty;
                            return;
                        }
                    }
                }
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            ApplyChanges();
        }

        private void ApplyChanges()
        {
            if (templatesTreeView.SelectedNode == null)
            {
                contentTextBox.Text = string.Empty;
                return;
            }
            if (templatesTreeView.SelectedNode.Parent == null)
            {
                contentTextBox.Text = string.Empty;
                return;
            }
            if (templatesTreeView.SelectedNode.Parent.Parent == null)
            {
                contentTextBox.Text = string.Empty;
                return;
            }
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                if (SharedData.WorkSpaces[i].Name == templatesTreeView.SelectedNode.Parent.Parent.Text)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[i].Templates.Count; j++)
                    {
                        if (SharedData.WorkSpaces[i].Templates[j].Name == templatesTreeView.SelectedNode.Parent.Text)
                        {
                            if (templatesTreeView.SelectedNode.Text == "Index")
                            {
                                SharedData.WorkSpaces[i].Templates[j].Index = contentTextBox.Text;
                            }
                            else if (templatesTreeView.SelectedNode.Text.StartsWith("Category"))
                            {
                                int itemIndex = int.Parse(templatesTreeView.SelectedNode.Text.Substring(templatesTreeView.SelectedNode.Text.IndexOf("#") + 1)) - 1;
                                SharedData.WorkSpaces[i].Templates[j].Categories[itemIndex].Content = contentTextBox.Text;
                            }
                            else if (templatesTreeView.SelectedNode.Text.StartsWith("Page"))
                            {
                                int itemIndex = int.Parse(templatesTreeView.SelectedNode.Text.Substring(templatesTreeView.SelectedNode.Text.IndexOf("#") + 1)) - 1;
                                SharedData.WorkSpaces[i].Templates[j].Pages[itemIndex].Content = contentTextBox.Text;
                            }
                            else if (templatesTreeView.SelectedNode.Text.StartsWith("Static"))
                            {
                                int itemIndex = int.Parse(templatesTreeView.SelectedNode.Text.Substring(templatesTreeView.SelectedNode.Text.IndexOf("#") + 1)) - 1;
                                SharedData.WorkSpaces[i].Templates[j].StaticPages[itemIndex].Content = contentTextBox.Text;
                            }
                            else if (templatesTreeView.SelectedNode.Text.StartsWith("Custom"))
                            {
                                int itemIndex = int.Parse(templatesTreeView.SelectedNode.Text.Substring(templatesTreeView.SelectedNode.Text.IndexOf("#") + 1)) - 1;
                                SharedData.WorkSpaces[i].Templates[j].CustomPages[itemIndex].Content = contentTextBox.Text;
                            }
                            else if (templatesTreeView.SelectedNode.Text.StartsWith("Map"))
                            {
                                int itemIndex = int.Parse(templatesTreeView.SelectedNode.Text.Substring(templatesTreeView.SelectedNode.Text.IndexOf("#") + 1)) - 1;
                                SharedData.WorkSpaces[i].Templates[j].Map[itemIndex].Content = contentTextBox.Text;
                            }
                            SharedData.WorkSpaces[i].Save();
                            return;
                        }
                    }
                }
            }
        }
    }
}
