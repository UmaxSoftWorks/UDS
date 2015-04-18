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
    public partial class ExEdPresets : Form
    {
        public ExEdPresets()
        {
            InitializeComponent();
        }

        private void ExEdPresets_Load(object sender, EventArgs e)
        {
            this.Icon = Resource.MainIcon;
            FillOutForm();
            tipsTimer.Start();
            tipsTimer_Tick(sender, e);
        }

        private void FillOutForm()
        {
            this.Text = View.UILanguageResources.GetString("S0000064") + ": " + View.UILanguageResources.GetString("S0000039");

            ExportButton.Text = View.UILanguageResources.GetString("S0000035");
            SaveAsLabel.Text = View.UILanguageResources.GetString("S0000056");
            SaveAsButton.Text = View.UILanguageResources.GetString("S0000055");
            SaveButton.Text = View.UILanguageResources.GetString("S0000046");
            deleteButton.Text = View.UILanguageResources.GetString("S0000065");
        }

        private void ExEdPresets_FormClosing(object sender, FormClosingEventArgs e)
        {
            tipsTimer.Stop();
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            savePresetFileDialog.FileName = string.Empty;
            savePresetFileDialog.ShowDialog();
            if (savePresetFileDialog.FileName == string.Empty)
            {
                return;
            }
            try
            {
                File.WriteAllText(savePresetFileDialog.FileName, mainTaskControl.GetSettings().ToString(), Encoding.UTF8);
            }
            catch (Exception)
            {
                MessageBox.Show(View.UILanguageResources.GetString("S0000030"), View.UILanguageResources.GetString("S0000030"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAsButton_Click(object sender, EventArgs e)
        {
            if (SaveAsTextBox.Text == string.Empty)
            {
                return;
            }
            try
            {
                if (UniquePresetName(mainTaskControl.SelectedWorkSpace, SaveAsTextBox.Text))
                {
                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets.Add(new Preset(GetNextPresetID(mainTaskControl.SelectedWorkSpace), SaveAsTextBox.Text, string.Empty));
                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets.Count - 1].Settings = mainTaskControl.GetSettings();

                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets.Count - 1].TemplateID = SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Templates[mainTaskControl.SelectedTemplate].ID;
                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets.Count - 1].TextID = SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Texts[mainTaskControl.SelectedText].ID;

                    //Обновление интерфейса
                    int WS = mainTaskControl.SelectedWorkSpace;
                    mainTaskControl.SelectedWorkSpace = -1;
                    mainTaskControl.SelectedWorkSpace = WS;
                    mainTaskControl.SelectedPreset = SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets.Count - 1;

                    mainTaskControl.SelectedTemplate = SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets.Count - 1].TemplateID;
                    mainTaskControl.SelectedText = SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets.Count - 1].TextID;

                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Save();
                }
            }
            catch (Exception ex)
            {
                SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets.RemoveAt(SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets.Count - 1);
                if (ex.Message == "Page names!")
                {
                    // Page names error message
                    MessageBox.Show(View.UILanguageResources.GetString("S0000522"), View.UILanguageResources.GetString("S0000030"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(View.UILanguageResources.GetString("S0000030"), View.UILanguageResources.GetString("S0000030"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            SaveAsTextBox.Text = string.Empty;
        }

        private bool UniquePresetName(int WorkSpaceIndex, string Name)
        {
            for (int i = 0; i < SharedData.WorkSpaces[WorkSpaceIndex].Presets.Count; i++)
            {
                if (SharedData.WorkSpaces[WorkSpaceIndex].Presets[i].Name == Name)
                {
                    return false;
                }
            }
            return true;
        }
        private int GetNextPresetID(int WorkSpaceIndex)
        {
            if (SharedData.WorkSpaces[WorkSpaceIndex].Presets.Count == 0)
            {
                return 0;
            }
            int templateID = 0;
            for (int i = 0; i < SharedData.WorkSpaces[WorkSpaceIndex].Presets.Count; i++)
            {
                if (SharedData.WorkSpaces[WorkSpaceIndex].Presets[i].ID > templateID)
                {
                    templateID = SharedData.WorkSpaces[WorkSpaceIndex].Presets[i].ID;
                }
            }
            return templateID + 1;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (mainTaskControl.SelectedPreset == -1)
            {
                return;
            }
            try
            {
                SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets[mainTaskControl.SelectedPreset].Settings = mainTaskControl.GetSettings();
                SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets[mainTaskControl.SelectedPreset].TemplateID = SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Templates[mainTaskControl.SelectedTemplate].ID;
                SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets[mainTaskControl.SelectedPreset].TextID = SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Texts[mainTaskControl.SelectedText].ID;
                SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Save();
            }
            catch (Exception)
            {
                MessageBox.Show(View.UILanguageResources.GetString("S0000030"), View.UILanguageResources.GetString("S0000030"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tipsTimer_Tick(object sender, EventArgs e)
        {
            TipsTextBox.Text = View.UILanguageResources.GetString("S0001013");
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (mainTaskControl.SelectedPreset == -1)
            {
                return;
            }
            SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].DeletePreset(SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets[mainTaskControl.SelectedPreset].ID);

            //Обновление интерфейса
            int WS = mainTaskControl.SelectedWorkSpace;
            mainTaskControl.SelectedWorkSpace = -1;
            mainTaskControl.SelectedWorkSpace = WS;
            if (SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets.Count != 0)
            {
                mainTaskControl.SelectedPreset = 0;
                mainTaskControl.SelectedTemplate = SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].GetTemplateIndex(SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets.Count - 1].TemplateID);
                mainTaskControl.SelectedText = SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].GetTextIndex(SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets.Count - 1].TextID);
            }
            else
            {
                mainTaskControl.SelectedPreset = -1;
            }
        }
    }
}
