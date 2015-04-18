using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using Doorway_Studio.Classes;

namespace Doorway_Studio
{
    public partial class ImportTemplate : Form
    {
        public ImportTemplate()
        {
            InitializeComponent();
        }

        private void ImportTemplate_Load(object sender, EventArgs e)
        {
            this.Icon = Resource.MainIcon;
            FillOutForm();
            this.encodingComboBox.SelectedIndex = 0;
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                WSComboBox.Items.Add(SharedData.WorkSpaces[i].Name);
            }
            this.WSComboBox.SelectedIndex = 0;
            tipsTimer.Start();
            tipsTimer_Tick(sender, e);
        }

        private void FillOutForm()
        {
            this.Text = View.UILanguageResources.GetString("S0000034") + ": " + View.UILanguageResources.GetString("S0000037");

            nameLabel.Text = View.UILanguageResources.GetString("S0000056");
            commLabel.Text = View.UILanguageResources.GetString("S0000053");
            cancelButton.Text = View.UILanguageResources.GetString("S0000060");

            encodingLabel.Text = View.UILanguageResources.GetString("S0000066");
        }

        private int GetNextTemplateID(int WS)
        {
            if (SharedData.WorkSpaces[WS].Templates.Count == 0)
            {
                return 0;
            }
            else
            {
                int max = 0;
                for (int i = 0; i < SharedData.WorkSpaces[WS].Templates.Count; i++)
                {
                    if (max < SharedData.WorkSpaces[WS].Templates[i].ID)
                    {
                        max = SharedData.WorkSpaces[WS].Templates[i].ID;
                    }
                }
                return max + 1;
            }
        }

        private void ImportTemplate_FormClosing(object sender, FormClosingEventArgs e)
        {
            tipsTimer.Stop();
        }

        private void tipsTimer_Tick(object sender, EventArgs e)
        {
            TipsTextBox.Text = View.UILanguageResources.GetString("S000100" + new Random().Next(7, 9).ToString());
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            //Добавление шаблона
            if (WSComboBox.SelectedIndex == -1)
            {
                return;
            }
            if (nameTextBox.Text == string.Empty)
            {
                return;
            }
            if (encodingComboBox.SelectedIndex == -1)
            {
                return;
            }
            if (temlateFolderTextBox.Text == string.Empty)
            {
                return;
            }
            if (!temlateFolderTextBox.Text.EndsWith("\\"))
            {
                temlateFolderTextBox.Text += "\\";
            }
            if (TemplateUniqueName() && CheckTemplate(temlateFolderTextBox.Text))
            {
                SharedData.WorkSpaces[WSComboBox.SelectedIndex].Templates.Add(new Template(int.Parse(IDLabel.Text), nameTextBox.Text, commTextBox.Text.Replace("\r\n", " "), encodingComboBox.SelectedIndex));
                SharedData.WorkSpaces[WSComboBox.SelectedIndex].Templates[SharedData.WorkSpaces[WSComboBox.SelectedIndex].Templates.Count - 1] = LoadAndSaveTemplate(temlateFolderTextBox.Text, SharedData.WorkSpaces[WSComboBox.SelectedIndex].Templates[SharedData.WorkSpaces[WSComboBox.SelectedIndex].Templates.Count - 1], encodingComboBox.SelectedIndex);
                SharedData.WorkSpaces[WSComboBox.SelectedIndex].Save();
                Close();
            }
        }

        private bool CheckTemplate(string Path)
        {
            if (!File.Exists(Path + "info.txt"))
            {
                return false;
            }
            string[] info = File.ReadAllLines(Path + "info.txt", Encoding.Default);
            for (int i = 0; i < info.Length; i++)
            {
                if (info[i].StartsWith("index="))
                {
                    return true;
                }
            }
            return false;
        }

        private Template LoadAndSaveTemplate(string Path, Template CurrentTemplate, int EncodingType)
        {
            string[] templateInfoContent = File.ReadAllLines(Path + "info.txt", Encoding.Default);
            int indexCount = 0;
            //Загрузка
            for (int i = 0; i < templateInfoContent.Length; i++)
            {
                try
                {
                    if (templateInfoContent[i].StartsWith("index="))
                    {
                        if (indexCount == 0)
                        {
                            if (EncodingType == 0)
                            {
                                CurrentTemplate.Index = File.ReadAllText(Path + templateInfoContent[i].Substring(6), Encoding.Default);
                            }
                            else
                            {
                                CurrentTemplate.Index = File.ReadAllText(Path + templateInfoContent[i].Substring(6), Encoding.UTF8);
                            }
                            indexCount++;
                        }
                    }
                    else if (templateInfoContent[i].StartsWith("category="))
                    {
                        CurrentTemplate.Categories.Add(new TemplatePage(Path + templateInfoContent[i].Substring(9), EncodingType == 0 ? Encoding.Default : Encoding.UTF8));
                    }
                    else if (templateInfoContent[i].StartsWith("page="))
                    {
                        CurrentTemplate.Pages.Add(new TemplatePage(Path + templateInfoContent[i].Substring(5), EncodingType == 0 ? Encoding.Default : Encoding.UTF8));
                    }
                    else if (templateInfoContent[i].StartsWith("static="))
                    {
                        CurrentTemplate.StaticPages.Add(new TemplatePage(Path + templateInfoContent[i].Substring(7), EncodingType == 0 ? Encoding.Default : Encoding.UTF8));
                    }
                    else if (templateInfoContent[i].StartsWith("custom="))
                    {
                        string keywords = templateInfoContent[i].Contains("|") ? templateInfoContent[i].Substring(templateInfoContent[i].IndexOf("|") + 1) : string.Empty;
                        string name = templateInfoContent[i].Contains("|") ? templateInfoContent[i].Substring(7, templateInfoContent[i].IndexOf("|") - 7) : templateInfoContent[i].Substring(7);

                        if (!string.IsNullOrEmpty(name))
                        {
                            CurrentTemplate.CustomPages.Add(new TemplatePage(Path + name, EncodingType == 0 ? Encoding.Default : Encoding.UTF8)
                                                                {
                                                                    CustomName = name,
                                                                    CustomKeywords = keywords.Split(new char[] { '|'})
                                                                });
                        }
                    }
                    else if (templateInfoContent[i].StartsWith("map="))
                    {
                        CurrentTemplate.Map.Add(new TemplatePage(Path + templateInfoContent[i].Substring(4), EncodingType == 0 ? Encoding.Default : Encoding.UTF8));
                    }
                    else if (templateInfoContent[i].StartsWith("image="))
                    {
                        if (!templateInfoContent[i].Substring(6).StartsWith("http"))
                        {
                            CurrentTemplate.Images.Add(Path + templateInfoContent[i].Substring(6));
                        }
                        else
                        {
                            CurrentTemplate.Images.Add(templateInfoContent[i].Substring(6));
                        }
                    }
                    else if (templateInfoContent[i].StartsWith("file="))
                    {
                        CurrentTemplate.Files.Add(Path + templateInfoContent[i].Substring(5));
                    }
                }
                catch (Exception)
                {
                }
            }
            //Сохранение
            //Создание папок
            string directoryName = SharedData.WorkSpaces[WSComboBox.SelectedIndex].ID.ToString();
            while (directoryName.Length < 7)
            {
                directoryName = "0" + directoryName;
            }
            //Создание папки
            if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName))
            {
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName);
                File.SetAttributes(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName, FileAttributes.Normal);
            }
            if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates"))
            {
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates");
                File.SetAttributes(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates", FileAttributes.Normal);
            }

            string templateName = CurrentTemplate.ID.ToString();
            while (templateName.Length < 7)
            {
                templateName = "0" + templateName;
            }

            Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName);
            File.SetAttributes(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName, FileAttributes.Normal);
            Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files");
            File.SetAttributes(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files", FileAttributes.Normal);
            //Запись info.txt
            StringBuilder infoData = new StringBuilder(200);
            infoData.Append("Name=" + CurrentTemplate.Name + "\r\n");
            infoData.Append("Comm=" + CurrentTemplate.Comment + "\r\n");
            infoData.Append("Encoding=" + EncodingType.ToString());

            File.WriteAllText(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\info.txt", infoData.ToString(), Encoding.UTF8);

            infoData = new StringBuilder(1000);
            infoData.Append("index=index.html\r\n");
            if (EncodingType == 0)
            {
                File.WriteAllText(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\index.html", CurrentTemplate.Index, Encoding.Default);
            }
            else
            {
                File.WriteAllText(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\index.html", CurrentTemplate.Index, Encoding.UTF8);
            }
            for (int i = 0; i < CurrentTemplate.Categories.Count; i++)
            {
                string tempString = (i + 1).ToString();
                while (tempString.Length < 3)
                {
                    tempString = "0" + tempString;
                }
                infoData.Append("category=category-" + tempString + ".html\r\n");
                if (EncodingType == 0)
                {
                    File.WriteAllText(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\category-" + tempString + ".html", CurrentTemplate.Index, Encoding.Default);
                }
                else
                {
                    File.WriteAllText(Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\category-" + tempString + ".html", CurrentTemplate.Index, Encoding.UTF8);
                }
            }
            if (CurrentTemplate.Pages.Count == 0)
            {
                CurrentTemplate.Pages.Add(new TemplatePage(CurrentTemplate.Index));
            }
            for (int i = 0; i < CurrentTemplate.Pages.Count; i++)
            {
                string tempString = (i + 1).ToString();
                while (tempString.Length < 3)
                {
                    tempString = "0" + tempString;
                }

                infoData.Append("page=page-" + tempString + ".html|" + CurrentTemplate.Pages[i].UsagePercent.ToString() + "\r\n");
                File.WriteAllText(Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\page-" + tempString + ".html",
                                  CurrentTemplate.Pages[i].Content, EncodingType == 0 ? Encoding.Default : Encoding.UTF8);
            }
            for (int i = 0; i < CurrentTemplate.StaticPages.Count; i++)
            {
                string tempString = (i + 1).ToString();
                while (tempString.Length < 3)
                {
                    tempString = "0" + tempString;
                }
                infoData.Append("static=static-" + tempString + ".html\r\n");

                File.WriteAllText(Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\static-" + tempString + ".html", CurrentTemplate.StaticPages[i].Content, EncodingType == 0 ? Encoding.Default : Encoding.UTF8);
            }
            for (int i = 0; i < CurrentTemplate.CustomPages.Count; i++)
            {
                string name = CurrentTemplate.CustomPages[i].CustomName;
                string keywords = string.Join("|", CurrentTemplate.CustomPages[i].CustomKeywords);

                if (string.IsNullOrEmpty(keywords))
                {
                    infoData.Append("custom=" + name + "\r\n");
                }
                else
                {
                    infoData.Append("custom=" + name + "|" + keywords + "\r\n");
                }

                File.WriteAllText(Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\" + name, CurrentTemplate.CustomPages[i].Content,
                                  EncodingType == 0 ? Encoding.Default : Encoding.UTF8);
            }
            for (int i = 0; i < CurrentTemplate.Map.Count; i++)
            {
                string tempString = (i + 1).ToString();
                while (tempString.Length < 3)
                {
                    tempString = "0" + tempString;
                }
                infoData.Append("map=map-" + tempString + ".html\r\n");
                if (EncodingType == 0)
                {
                    File.WriteAllText(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\map-" + tempString + ".html", CurrentTemplate.Map[i].Content, Encoding.Default);
                }
                else
                {
                    File.WriteAllText(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\map-" + tempString + ".html", CurrentTemplate.Map[i].Content, Encoding.UTF8);
                }
            }
            for (int i = 0; i < CurrentTemplate.Images.Count; i++)
            {
                if (!CurrentTemplate.Images[i].StartsWith("http:"))
                {
                    try
                    {
                        int tempInt = 0;
                        while (CurrentTemplate.Images[i].Substring(Path.Length).IndexOf("\\", tempInt + 1) >= 0 && tempInt >= 0)
                        {
                            if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\" + CurrentTemplate.Images[i].Substring(Path.Length, CurrentTemplate.Images[i].LastIndexOf("\\") - Path.Length).Substring(tempInt)))
                            {
                                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\" + CurrentTemplate.Images[i].Substring(Path.Length, CurrentTemplate.Images[i].LastIndexOf("\\") - Path.Length).Substring(tempInt));
                                File.SetAttributes(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\" + CurrentTemplate.Images[i].Substring(Path.Length, CurrentTemplate.Images[i].LastIndexOf("\\") - Path.Length).Substring(tempInt), FileAttributes.Normal);
                            }
                            tempInt = CurrentTemplate.Images[i].Substring(Path.Length).IndexOf("\\", tempInt + 1);
                        }
                        File.Copy(CurrentTemplate.Images[i], System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\" + CurrentTemplate.Images[i].Substring(Path.Length));
                        
                        infoData.Append("image=" + CurrentTemplate.Images[i].Substring(Path.Length) + "\r\n");
                        CurrentTemplate.Images[i] = System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\" + CurrentTemplate.Images[i].Substring(Path.Length);
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    infoData.Append("image=" + CurrentTemplate.Images[i] + "\r\n");
                }
            }
            for (int i = 0; i < CurrentTemplate.Files.Count; i++)
            {
                try
                {
                    int tempInt = 0;
                    while (CurrentTemplate.Files[i].Substring(Path.Length).IndexOf("\\", tempInt + 1) >= 0 && tempInt >= 0)
                    {
                        if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\" + CurrentTemplate.Files[i].Substring(Path.Length, CurrentTemplate.Files[i].LastIndexOf("\\") - Path.Length).Substring(tempInt)))
                        {
                            Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\" + CurrentTemplate.Files[i].Substring(Path.Length, CurrentTemplate.Files[i].LastIndexOf("\\") - Path.Length).Substring(tempInt));
                            File.SetAttributes(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\" + CurrentTemplate.Files[i].Substring(Path.Length, CurrentTemplate.Files[i].LastIndexOf("\\") - Path.Length).Substring(tempInt), FileAttributes.Normal);
                        }
                        tempInt = CurrentTemplate.Files[i].Substring(Path.Length).IndexOf("\\", tempInt + 1);
                    }
                    File.Copy(CurrentTemplate.Files[i], System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\" + CurrentTemplate.Files[i].Substring(Path.Length));
                    
                    infoData.Append("file=" + CurrentTemplate.Files[i].Substring(Path.Length) + "\r\n");
                    CurrentTemplate.Files[i] = System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\" + CurrentTemplate.Files[i].Substring(Path.Length);
                }
                catch (Exception)
                {
                }
            }
            File.WriteAllText(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateName + "\\Files\\info.txt", infoData.ToString(), Encoding.UTF8);

            return CurrentTemplate;
        }

        private bool TemplateUniqueName()
        {
            for (int i = 0; i < SharedData.WorkSpaces[WSComboBox.SelectedIndex].Templates.Count; i++)
            {
                if (nameTextBox.Text == SharedData.WorkSpaces[WSComboBox.SelectedIndex].Templates[i].Name)
                {
                    return false;
                }
            }
            return true;
        }

        private void openTemplateButton_Click(object sender, EventArgs e)
        {
            if (encodingComboBox.SelectedIndex == -1)
            {
                return;
            }
            templateFolderBrowserDialog.SelectedPath = string.Empty;
            templateFolderBrowserDialog.ShowDialog();
            if (templateFolderBrowserDialog.SelectedPath == string.Empty)
            {
                return;
            }
            temlateFolderTextBox.Text = templateFolderBrowserDialog.SelectedPath;
        }

        private void WSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IDLabel.Text = GetNextTemplateID(WSComboBox.SelectedIndex).ToString();
        }
    }
}
