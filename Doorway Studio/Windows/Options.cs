using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using Microsoft.Win32;

namespace Doorway_Studio
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void FillOutForm()
        {
            this.Text = View.UILanguageResources.GetString("S0000045");
            cancelButton.Text = View.UILanguageResources.GetString("S0000060");

            generalGroupBox.Text = View.UILanguageResources.GetString("S0000078");
            generalMPRTLabel.Text = View.UILanguageResources.GetString("S0000079");
            generalDFTALabel.Text = View.UILanguageResources.GetString("S0000080");
            generalDFTADLabel.Text = View.UILanguageResources.GetString("S0000081");
            generalClearFolderCheckBox.Text = View.UILanguageResources.GetString("S0000082");

            startGroupBox.Text = View.UILanguageResources.GetString("S0000006");
            startWithWindowsCheckBox.Text = View.UILanguageResources.GetString("S0000083");
            startMinimizedCheckBox.Text = View.UILanguageResources.GetString("S0000084");

            updateGroupBox.Text = View.UILanguageResources.GetString("S0000298");
            UpdateAtStartUpCheckBox.Text = View.UILanguageResources.GetString("S0000299");

            baloonsGroupBox.Text = View.UILanguageResources.GetString("S0000085");
            baloonsCheckBox.Text = View.UILanguageResources.GetString("S0000086");
            baloonsShowTimeLabel.Text = View.UILanguageResources.GetString("S0000087");
            baloonsSecLabel.Text = View.UILanguageResources.GetString("S0000088");
        }

        private void Options_Load(object sender, EventArgs e)
        {
            this.Icon = Resource.MainIcon;
            FillOutForm();
            //Редактирование настроек
            generalMPRTNumericUpDown.Maximum = 10;
            //Заполнение полей
            try
            {
                generalMPRTNumericUpDown.Value = MainSettings.MaxParallelTaks;
                generalDFTANumericUpDown.Value = MainSettings.DeleteFinishedTasksAfterDays;
                generalClearFolderCheckBox.Checked = MainSettings.ClearFolderWhereDoorwaysMastBeSaved;

                startWithWindowsCheckBox.Checked = MainSettings.StartWithWindows;
                startMinimizedCheckBox.Checked = MainSettings.MinimizedStart;
                UpdateAtStartUpCheckBox.Checked = MainSettings.UpdateAtStartUp;

                baloonsCheckBox.Checked = MainSettings.ShowBaloons;
                baloonsShowTimeNumericUpDown.Value = MainSettings.ShowBaloonsTime;
            }
            catch (Exception)
            {
            }
            tipsTimer.Start();
            tipsTimer_Tick(sender, e);
        }

        private void tipsTimer_Tick(object sender, EventArgs e)
        {
            TipsTextBox.Text = View.UILanguageResources.GetString("S000102" + new Random().Next(5, 7).ToString());
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            //Применение настроек
            MainSettings.MaxParallelTaks = (int)generalMPRTNumericUpDown.Value;
            MainSettings.DeleteFinishedTasksAfterDays = (int)generalDFTANumericUpDown.Value;
            MainSettings.ClearFolderWhereDoorwaysMastBeSaved = generalClearFolderCheckBox.Checked;
            MainSettings.StartWithWindows = startWithWindowsCheckBox.Checked;
            MainSettings.MinimizedStart = startMinimizedCheckBox.Checked;
            MainSettings.UpdateAtStartUp = UpdateAtStartUpCheckBox.Checked;
            MainSettings.ShowBaloons = baloonsCheckBox.Checked;
            MainSettings.ShowBaloonsTime = (int)baloonsShowTimeNumericUpDown.Value;
            //Созранение настроек
            try
            {
                StringBuilder data = new StringBuilder(100);
                data.Append("MaxTasks=" + ((int)generalMPRTNumericUpDown.Value).ToString() + "\r\n");
                data.Append("DeleteTasksAfter=" + ((int)generalDFTANumericUpDown.Value).ToString() + "\r\n");
                data.Append("ClearFolders=" + generalClearFolderCheckBox.Checked.ToString() + "\r\n");

                if (startWithWindowsCheckBox.Checked)
                {
                    try
                    {
                        //Добавление записи в реестр
                        RegistryKey regWorker = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        regWorker.SetValue("Umax Doorway Studio", Application.ExecutablePath);
                    }
                    catch (Exception)
                    {
                        startWithWindowsCheckBox.Checked = false;
                    }
                }
                else
                {
                    try
                    {
                        //Удаление записи из реестра
                        RegistryKey regWorker = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        regWorker.DeleteValue("Umax Doorway Studio", false);
                    }
                    catch (Exception)
                    {
                    }
                }
                data.Append("MinStart=" + startMinimizedCheckBox.Checked.ToString() + "\r\n");
                data.Append("UpdateOnStartUp=" + UpdateAtStartUpCheckBox.Checked.ToString() + "\r\n");
                data.Append("Baloons=" + baloonsCheckBox.Checked.ToString() + "\r\n");
                data.Append("BaloonsTime=" + ((int)baloonsShowTimeNumericUpDown.Value).ToString() + "\r\n");

                File.WriteAllText(Application.StartupPath + "\\options.ini", data.ToString(), Encoding.UTF8);
            }
            catch (Exception)
            {
            }
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            tipsTimer.Stop();
        }

        private void baloonsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (baloonsCheckBox.Checked)
            {
                baloonsShowTimeNumericUpDown.Enabled = true;
            }
            else
            {
                baloonsShowTimeNumericUpDown.Enabled = false;
            }
        }

        private void startWithWindowsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!startWithWindowsCheckBox.Checked)
            {
                startMinimizedCheckBox.Enabled = false;
                startMinimizedCheckBox.Checked = true;
            }
            else
            {
                startMinimizedCheckBox.Enabled = true;
            }
        }
    }
}
