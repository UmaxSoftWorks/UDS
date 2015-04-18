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
    public partial class ImportBigText : Form
    {
        public ImportBigText()
        {
            InitializeComponent();
        }

        private void ImportBigText_Load(object sender, EventArgs e)
        {
            this.Icon = Resource.MainIcon;
            FillOutText();
            this.encodingComboBox.SelectedIndex = 0;
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                WSComboBox.Items.Add(SharedData.WorkSpaces[i].Name);
            }
            this.WSComboBox.SelectedIndex = 0;
            tipsTimer.Start();
            tipsTimer_Tick(sender, e);
        }

        private void FillOutText()
        {
            this.Text = View.UILanguageResources.GetString("S0000034") + ": " + View.UILanguageResources.GetString("S0000038");

            nameLabel.Text = View.UILanguageResources.GetString("S0000056");
            commLabel.Text = View.UILanguageResources.GetString("S0000053");
            cancelButton.Text = View.UILanguageResources.GetString("S0000060");

            encodingLabel.Text = View.UILanguageResources.GetString("S0000066");
            openFileButton.Text = View.UILanguageResources.GetString("S0000117");
        }

        private int GetNextTextID(int WS)
        {
            if (SharedData.WorkSpaces[WS].Texts.Count == 0)
            {
                return 0;
            }
            else
            {
                int max = 0;
                for (int i = 0; i < SharedData.WorkSpaces[WS].Texts.Count; i++)
                {
                    if (max < SharedData.WorkSpaces[WS].Texts[i].ID)
                    {
                        max = SharedData.WorkSpaces[WS].Texts[i].ID;
                    }
                }
                return max + 1;
            }
        }

        private bool TextUniqueName()
        {
            for (int i = 0; i < SharedData.WorkSpaces[WSComboBox.SelectedIndex].Texts.Count; i++)
            {
                if (nameTextBox.Text == SharedData.WorkSpaces[WSComboBox.SelectedIndex].Texts[i].Name)
                {
                    return false;
                }
            }
            return true;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            //Добавление текста
            if (WSComboBox.SelectedIndex == -1)
            {
                return;
            }
            if (nameTextBox.Text == string.Empty)
            {
                return;
            }
            if (fileNameTextBox.Text == string.Empty)
            {
                return;
            }
            if (TextUniqueName())
            {
                SharedData.WorkSpaces[WSComboBox.SelectedIndex].Texts.Add(new Text(int.Parse(IDLabel.Text), nameTextBox.Text, commTextBox.Text.Replace("\r\n", " ")));
                if (encodingComboBox.SelectedIndex == 0)
                {
                    SharedData.WorkSpaces[WSComboBox.SelectedIndex].Texts[SharedData.WorkSpaces[WSComboBox.SelectedIndex].Texts.Count - 1].Texts = File.ReadAllText(fileNameTextBox.Text, Encoding.Default);
                }
                else
                {
                    SharedData.WorkSpaces[WSComboBox.SelectedIndex].Texts[SharedData.WorkSpaces[WSComboBox.SelectedIndex].Texts.Count - 1].Texts = File.ReadAllText(fileNameTextBox.Text, Encoding.UTF8);
                }
                SharedData.WorkSpaces[WSComboBox.SelectedIndex].Save();
                Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            if (encodingComboBox.SelectedIndex == -1)
            {
                return;
            }
            openTextFileDialog.FileName = string.Empty;
            openTextFileDialog.ShowDialog();
            if (openTextFileDialog.FileName == string.Empty)
            {
                return;
            }
            fileNameTextBox.Text = openTextFileDialog.FileName;
        }

        private void tipsTimer_Tick(object sender, EventArgs e)
        {
            if (new Random().Next(9, 11) == 9)
            {
                TipsTextBox.Text = View.UILanguageResources.GetString("S0001009");
            }
            else
            {
                TipsTextBox.Text = View.UILanguageResources.GetString("S0001010");
            }
        }

        private void WSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IDLabel.Text = GetNextTextID(WSComboBox.SelectedIndex).ToString();
        }
    }
}
