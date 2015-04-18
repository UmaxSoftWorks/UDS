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
    public partial class ImportKeywords : Form
    {
        public ImportKeywords()
        {
            InitializeComponent();
        }

        private void ImportKeywords_Load(object sender, EventArgs e)
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
            this.Text = View.UILanguageResources.GetString("S0000034") + ": " + View.UILanguageResources.GetString("S0000036");

            nameLabel.Text = View.UILanguageResources.GetString("S0000056");
            commLabel.Text = View.UILanguageResources.GetString("S0000053");
            cancelButton.Text = View.UILanguageResources.GetString("S0000060");

            keywordsGroupBox.Text = View.UILanguageResources.GetString("S0000036");
            encodingLabel.Text = View.UILanguageResources.GetString("S0000066");
            openFileButton.Text = View.UILanguageResources.GetString("S0000074");

            //Меню
            selectAllToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000069");
            copyToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000070");
            pasteToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000071");
            clearToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000072");
        }

        private int GetNextKeywordsID(int WS)
        {
            if (SharedData.WorkSpaces[WS].Keywords.Count == 0)
            {
                return 0;
            }
            else
            {
                int max = 0;
                for (int i = 0; i < SharedData.WorkSpaces[WS].Keywords.Count; i++)
                {
                    if (max < SharedData.WorkSpaces[WS].Keywords[i].ID)
                    {
                        max = SharedData.WorkSpaces[WS].Keywords[i].ID;
                    }
                }
                return max + 1;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            //Добавление кейворда
            if (WSComboBox.SelectedIndex == -1)
            {
                return;
            }
            if (nameTextBox.Text == string.Empty)
            {
                return;
            }
            if (keywordsTextBox.Text == string.Empty)
            {
                return;
            }
            if (UniqueKeywordsName())
            {
                SharedData.WorkSpaces[WSComboBox.SelectedIndex].Keywords.Add(new KeyWord(int.Parse(IDLabel.Text), nameTextBox.Text, commTextBox.Text.Replace("\r\n", " ")));
                SharedData.WorkSpaces[WSComboBox.SelectedIndex].Keywords[SharedData.WorkSpaces[WSComboBox.SelectedIndex].Keywords.Count - 1].Items = keywordsTextBox.Text.Replace("\r\n", "¥").Split(new char[] { '¥' }, StringSplitOptions.RemoveEmptyEntries);
                SharedData.WorkSpaces[WSComboBox.SelectedIndex].Save();
                Close();
            }
        }

        private bool UniqueKeywordsName()
        {
            for (int i = 0; i < SharedData.WorkSpaces[WSComboBox.SelectedIndex].Keywords.Count; i++)
            {
                if (nameTextBox.Text == SharedData.WorkSpaces[WSComboBox.SelectedIndex].Keywords[i].Name)
                {
                    return false;
                }
            }
            return true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
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

        private void tipsTimer_Tick(object sender, EventArgs e)
        {
            TipsTextBox.Text = View.UILanguageResources.GetString("S000100" + new Random().Next(5, 7).ToString());
        }

        private void ImportKeywords_FormClosing(object sender, FormClosingEventArgs e)
        {
            tipsTimer.Stop();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            if (encodingComboBox.SelectedIndex == -1)
            {
                return;
            }
            openKeywordsFileDialog.FileName = string.Empty;
            openKeywordsFileDialog.ShowDialog();
            if (openKeywordsFileDialog.FileName == string.Empty)
            {
                return;
            }
            try
            {
                if (encodingComboBox.SelectedIndex == 0)
                {
                    keywordsTextBox.Text = File.ReadAllText(openKeywordsFileDialog.FileName, Encoding.Default);
                }
                else
                {
                    keywordsTextBox.Text = File.ReadAllText(openKeywordsFileDialog.FileName, Encoding.UTF8);
                }
            }
            catch (Exception)
            {
            }
        }

        private void WSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IDLabel.Text = GetNextKeywordsID(WSComboBox.SelectedIndex).ToString();
        }
    }
}
