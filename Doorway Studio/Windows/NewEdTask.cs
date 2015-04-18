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
    public partial class NewEdTask : Form
    {
        public NewEdTask()
        {
            InitializeComponent();
        }

        /// <summary>
        /// New Task was added
        /// </summary>
        public bool Ok { get; set; }

        /// <summary>
        /// Режим редактирования
        /// </summary>
        public bool Edit { get; set; }

        /// <summary>
        /// Индекс WorkSpace
        /// </summary>
        public int WSIndex { get; set; }

        /// <summary>
        /// Индекс Task
        /// </summary>
        public int TaskIndex { get; set; }

        private void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (nameTextBox.Text == string.Empty)
                {
                    return;
                }
                if (mainTaskControl.SelectedWorkSpace == -1)
                {
                    return;
                }
                //Проверка настроек заполненности полей
                if (this.Edit)
                {
                    if (SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].Status == 6)
                    {
                        SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].Status = 0;
                    }
                    //Изменение данных в статистике
                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Count - 1].WSIndex = mainTaskControl.SelectedWorkSpace;
                    SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].Settings = mainTaskControl.GetSettings();
                    if (SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Count - 1].Settings.KeywordsID == -1)
                    {
                        throw new Exception();
                    }
                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].StatisticsRemoveData(SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].Settings.KeywordsID,
                                                                                                  SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].PresetID,
                                                                                                  SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].TemplateID,
                                                                                                  SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].TextID,
                                                                                                  SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].Settings.GeneralCreateDoorways,
                                                                                                  SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].StartTime);
                    if (mainTaskControl.SelectedPreset != -1)
                    {
                        SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].PresetID = SharedData.WorkSpaces[WSIndex].Presets[mainTaskControl.SelectedPreset].ID;
                    }
                    SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].TemplateID = SharedData.WorkSpaces[WSIndex].Templates[mainTaskControl.SelectedTemplate].ID;
                    SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].TextID = SharedData.WorkSpaces[WSIndex].Texts[mainTaskControl.SelectedText].ID;

                    SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].StartTime = StartDateTimePicker.Value;
                    //Добавление данных в статистику
                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].StatisticsAddData(mainTaskControl.SelectedKeyword, mainTaskControl.SelectedPreset, mainTaskControl.SelectedTemplate,
                                                                                               mainTaskControl.SelectedText,
                                                                                               SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks[
                                                                                                   SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Count - 1].Settings.
                                                                                                   GeneralCreateDoorways, StartDateTimePicker.Value);
                }
                else
                {
                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Add(new Task(GetNextTaskID(mainTaskControl.SelectedWorkSpace), nameTextBox.Text, string.Empty));
                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Count - 1].WSIndex = mainTaskControl.SelectedWorkSpace;
                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Count - 1].Settings = mainTaskControl.GetSettings();
                    if (SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Count - 1].Settings.KeywordsID == -1)
                    {
                        throw new Exception();
                    }
                    if (mainTaskControl.SelectedPreset != -1)
                    {
                        SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Count - 1].PresetID =
                            SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Presets[mainTaskControl.SelectedPreset].ID;
                    }
                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Count - 1].TemplateID =
                        SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Templates[mainTaskControl.SelectedTemplate].ID;
                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Count - 1].TextID =
                        SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Texts[mainTaskControl.SelectedText].ID;

                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Count - 1].Status = 0;
                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Count - 1].StartTime = StartDateTimePicker.Value;
                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Count - 1].EndTime = new DateTime();
                    //Добавление данных в статистику
                    SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].StatisticsAddData(mainTaskControl.SelectedKeyword, mainTaskControl.SelectedPreset, mainTaskControl.SelectedTemplate,
                                                                                               mainTaskControl.SelectedText,
                                                                                               SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks[
                                                                                                   SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Count - 1].Settings.
                                                                                                   GeneralCreateDoorways, StartDateTimePicker.Value);

                    Ok = true;
                }
                SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Save();
                Close();
            }
            catch (Exception ex)
            {
                //if (!this.edit && SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks[SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Count - 1].Settings == null)
                //{
                SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.RemoveAt(SharedData.WorkSpaces[mainTaskControl.SelectedWorkSpace].Tasks.Count - 1);
                //}

                if (ex.Message == "Page names!")
                {
                    // Page names error message
                    MessageBox.Show(View.UILanguageResources.GetString("S0000522"), View.UILanguageResources.GetString("S0000030"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Regular error message
                    MessageBox.Show(View.UILanguageResources.GetString("S0000030"), View.UILanguageResources.GetString("S0000030"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int GetNextTaskID(int WSIndex)
        {
            int NextID = 0;
            if (SharedData.WorkSpaces[WSIndex].Tasks.Count == 0)
            {
                return NextID;
            }
            for (int i = 0; i < SharedData.WorkSpaces[WSIndex].Tasks.Count; i++)
            {
                if (SharedData.WorkSpaces[WSIndex].Tasks[i].ID > NextID)
                {
                    NextID = SharedData.WorkSpaces[WSIndex].Tasks[i].ID;
                }
            }
            return NextID + 1;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FillOutForm()
        {
            this.Text = View.UILanguageResources.GetString("S0000076") + ": " + View.UILanguageResources.GetString("S0000019");
            
            nameLabel.Text = View.UILanguageResources.GetString("S0000056");
            startTimeLabel.Text = View.UILanguageResources.GetString("S0000077");

            cancelButton.Text = View.UILanguageResources.GetString("S0000060");
        }

        private void NewEdTask_Load(object sender, EventArgs e)
        {
            this.Icon = Resource.MainIcon;
            FillOutForm();
            //Установка даты и времени
            DateTime startTime = SharedData.SelectedDate;
            if (this.Edit)
            {
                nameTextBox.Text = SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].Name;
                nameTextBox.Enabled = false;
                StartDateTimePicker.Value = SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].StartTime;

                if (SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].Status != 0 && SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].Status != 6)
                {
                    StartDateTimePicker.Enabled = false;
                    OKButton.Enabled = false;
                }
                //Заполнение полей
                mainTaskControl.SelectedWorkSpace = WSIndex;
                //Preset
                try
                {
                    mainTaskControl.SelectedPreset = SharedData.WorkSpaces[WSIndex].GetPresetIndex(SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].PresetID);
                }
                catch (Exception)
                {
                }
                //Template
                try
                {
                    mainTaskControl.SelectedTemplate = SharedData.WorkSpaces[WSIndex].GetTemplateIndex(SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].TemplateID);
                }
                catch (Exception)
                {
                }
                //Text
                try
                {
                    mainTaskControl.SelectedText = SharedData.WorkSpaces[WSIndex].GetTextIndex(SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].TextID);
                }
                catch (Exception)
                {
                }
                //Settings
                mainTaskControl.SetSettings(SharedData.WorkSpaces[WSIndex].Tasks[TaskIndex].Settings);
            }
            else
            {
                startTime = startTime.AddHours(DateTime.Now.Hour);
                startTime = startTime.AddMinutes(DateTime.Now.Minute + 5);
                StartDateTimePicker.Value = startTime;
            }
            tipsTimer.Start();
            tipsTimer_Tick(sender, e);
        }

        private void NewEdTask_FormClosing(object sender, FormClosingEventArgs e)
        {
            tipsTimer.Stop();
        }

        private void tipsTimer_Tick(object sender, EventArgs e)
        {
            TipsTextBox.Text = View.UILanguageResources.GetString("S000101" + new Random().Next(7, 10).ToString());
        }
    }
}
