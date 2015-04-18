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
    public partial class ImportText : Form
    {
        public ImportText()
        {
            InitializeComponent();
        }

        private void ImportText_Load(object sender, EventArgs e)
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

            textGroupBox.Text = View.UILanguageResources.GetString("S0000038");
            encodingLabel.Text = View.UILanguageResources.GetString("S0000066");
            openFileButton.Text = View.UILanguageResources.GetString("S0000074");
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IDLabel.Text = GetNextTextID(WSComboBox.SelectedIndex).ToString();
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
            if (textTextBox.Text == string.Empty)
            {
                return;
            }
            if (TextUniqueName())
            {
                SharedData.WorkSpaces[WSComboBox.SelectedIndex].Texts.Add(new Text(int.Parse(IDLabel.Text), nameTextBox.Text, commTextBox.Text.Replace("\r\n", " ")));
                SharedData.WorkSpaces[WSComboBox.SelectedIndex].Texts[SharedData.WorkSpaces[WSComboBox.SelectedIndex].Texts.Count - 1].Texts = textTextBox.Text;
                SharedData.WorkSpaces[WSComboBox.SelectedIndex].Save();
                Close();
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
            try
            {
                if (encodingComboBox.SelectedIndex == 0)
                {
                    textTextBox.Text = File.ReadAllText(openTextFileDialog.FileName, Encoding.Default);
                }
                else
                {
                    textTextBox.Text = File.ReadAllText(openTextFileDialog.FileName, Encoding.UTF8);
                }
            }
            catch (Exception)
            {
            }
        }

        private void ImportText_FormClosing(object sender, FormClosingEventArgs e)
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
            textTextBox.Text = string.Empty;
        }

    }
}
