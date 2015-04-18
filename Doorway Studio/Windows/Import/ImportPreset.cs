using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Doorway_Studio
{
    public partial class ImportPreset : Form
    {
        public ImportPreset()
        {
            InitializeComponent();
        }

        private void ImportPreset_Load(object sender, EventArgs e)
        {
            this.Icon = Resource.MainIcon;
            FillOutForm();
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
            this.Text = View.UILanguageResources.GetString("S0000034") + ": " + View.UILanguageResources.GetString("S0000039");

            nameLabel.Text = View.UILanguageResources.GetString("S0000056");
            commLabel.Text = View.UILanguageResources.GetString("S0000053");
            cancelButton.Text = View.UILanguageResources.GetString("S0000060");
            openFileButton.Text = View.UILanguageResources.GetString("S0000074");
        }

        private int GetNextPresetID(int WS)
        {
            if (SharedData.WorkSpaces[WS].Presets.Count == 0)
            {
                return 0;
            }
            else
            {
                int max = 0;
                for (int i = 0; i < SharedData.WorkSpaces[WS].Presets.Count; i++)
                {
                    if (max < SharedData.WorkSpaces[WS].Presets[i].ID)
                    {
                        max = SharedData.WorkSpaces[WS].Presets[i].ID;
                    }
                }
                return max + 1;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            //Добавление пресета
            if (WSComboBox.SelectedIndex == -1)
            {
                return;
            }
            if (nameTextBox.Text == string.Empty)
            {
                return;
            }
            if (pathTextBox.Text == string.Empty)
            {
                return;
            }
            if (UniquePresetName())
            {
                SharedData.WorkSpaces[WSComboBox.SelectedIndex].Presets.Add(new Preset(int.Parse(IDLabel.Text), nameTextBox.Text, commTextBox.Text.Replace("\r\n", " ")));
                SharedData.WorkSpaces[WSComboBox.SelectedIndex].Presets[SharedData.WorkSpaces[WSComboBox.SelectedIndex].Presets.Count - 1].Settings = WorkSpace.LoadPresetSettings(pathTextBox.Text);
                SharedData.WorkSpaces[WSComboBox.SelectedIndex].Save();
                Close();
            }
        }

        private bool UniquePresetName()
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

        private void openFileButton_Click(object sender, EventArgs e)
        {
            openPresestFileDialog.FileName = string.Empty;
            openPresestFileDialog.FileName = string.Empty;
            openPresestFileDialog.ShowDialog();
            if (openPresestFileDialog.FileName == string.Empty)
            {
                return;
            }
            pathTextBox.Text = openPresestFileDialog.FileName;
        }

        private void tipsTimer_Tick(object sender, EventArgs e)
        {
            TipsTextBox.Text = View.UILanguageResources.GetString("S000101" + new Random().Next(1,3).ToString());
        }

        private void ImportPreset_FormClosing(object sender, FormClosingEventArgs e)
        {
            tipsTimer.Stop();
        }

        private void WSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IDLabel.Text = GetNextPresetID(WSComboBox.SelectedIndex).ToString();
        }
    }
}
