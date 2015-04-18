using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using XPTable;
using System.Threading;
using System.IO;

namespace Doorway_Studio
{
    public partial class Main : Form
    {
        private bool WindowVisible;
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Icon = Resource.MainIcon;
            this.WindowVisible = true;
            this.tasksPanel.Width = mainCalendar.Width;
            SharedData.SelectedDate = DateTime.Today;
            SharedData.Exit = false;
            if (SharedData.WorkSpaces.Count > 0)
            {
                SharedData.SelectedWorkSpace = 0;
                WSCommentTextBox.Text = SharedData.WorkSpaces[0].Comment;
            }
            else
            {
                SharedData.SelectedWorkSpace = -1;
            }

            this.Text = "Umax " + this.Text;

            //Icon
            NotifyIcon.Icon = Resource.MainIcon;
            NotifyIcon.Text = this.Text;

            //Отображение новостей
            if (View.ShowNews)
            {
                showNewsToolStripMenuItem.Checked = true;
            }
            else
            {
                showNewsToolStripMenuItem.Checked = false;
                newsGroupBox.Visible = false;
            }

            //отображение подсказок
            if (View.ShowTips)
            {
                showTipsToolStripMenuItem.Checked = true;
            }
            else
            {
                showTipsToolStripMenuItem.Checked = false;
                bottomPanel.Visible = false;
            }

            //Язык
            switch (View.UILanguage)
            {
                case 0:
                    {
                        englishToolStripMenuItem.Checked = true;
                        russianToolStripMenuItem.Checked = false;
                        break;
                    }
                case 1:
                    {
                        englishToolStripMenuItem.Checked = false;
                        russianToolStripMenuItem.Checked = true;
                        break;
                    }
            }

            FillOutForm();

            IESound.Enabled = false;

            UpdateWSTable();
            UpdateTaskListTable();
            UpdateTaskTable();

            MainTimer.Start();
            tipsTimer.Start();
            tipsTimer_Tick(sender, e);

            //Check for updates
            if (MainSettings.UpdateAtStartUp)
            {
                string updateUrl = string.Empty;
                if (CheckVersion(out updateUrl))
                {
                    if (MessageBox.Show(View.UILanguageResources.GetString("S0000254"), View.UILanguageResources.GetString("S0000253"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Updater AUWindow = new Updater();
                        AUWindow.URL = updateUrl;
                        AUWindow.ShowDialog();
                    }
                }
            }
        }

        /// <summary>
        /// Перевод текста элементов формы
        /// </summary>
        private void FillOutForm()
        {
            //Главное меню
            fileToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000032");
            newWorkSpaceToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000033");

            tasksToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000019");
            tasksNewToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000054");
            tasksStatisticsToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000017");
            tasksDeleteAllFinishedTasksToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000293");
            tasksDeleteAllTasksToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000290");

            importToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000034");
            importKeywordsToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000036");
            importTemplatesToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000037");
            importTextToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000038");
            importBigTextToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000278");
            importPresetsToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000039");

            exportToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000035");
            exportKeywordsToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000036");
            exportTemplatesToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000037");
            exportTextToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000038");
            exportPresetsToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000039");

            exitToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000008");

            viewToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000040");

            languageToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000041");
            showNewsToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000042");
            showTipsToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000043");

            editToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000044");

            editKeywordsToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000036");
            editTemplatesToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000037");
            editTextToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000038");
            editPresetsToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000039");

            editOptionsToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000045");

            statisticsToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000017");
            statisticsKeywordsToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000036");
            statisticsTemplatesToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000037");
            statisticsTextToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000038");
            statisticsPresetsToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000039");

            helpToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000047");
            helpHelpToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000047");
            aboutToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000049");

            checkForUpdateToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000050");

            toolsToolStripMenuItem.Text = View.UILanguageResources.GetString("S0000051");

            //Меню у иконки в таскбаре
            showHideNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000031");
            newWorkSpaceNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000033");

            tasksNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000019");
            tasksNewNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000054");
            tasksStatisticsNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000017");
            tasksDeleteAllFinishedTasksNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000293");
            tasksDeleteAllTasksNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000290");

            importNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000034");
            importKeywordsNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000036");
            importTemplatesNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000037");
            importTextNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000038");
            importBigTextNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000278");
            importPresetsNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000039");

            exportNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000035");
            exportKeywordsNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000036");
            exportTemplatesNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000037");
            exportTextNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000038");
            exportPresetsNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000039");

            exitNotifyIconMenuItem.Text = View.UILanguageResources.GetString("S0000008");
            //Остальные элементы интерфейса
            WSActionsGroupBox.Text = View.UILanguageResources.GetString("S0000052");
            WSCommentGroupBox.Text = View.UILanguageResources.GetString("S0000053");
            newsGroupBox.Text = View.UILanguageResources.GetString("S0000018");
            statisticsGroupBox.Text = View.UILanguageResources.GetString("S0000017");
            tasksGroupBox.Text = View.UILanguageResources.GetString("S0000019");
            //Названия колонок таблиц
            TasksCModel.Columns[1].Text = View.UILanguageResources.GetString("S0000056");

            RunningTasksCModel.Columns[4].Text = View.UILanguageResources.GetString("S0000056");
            RunningTasksCModel.Columns[6].Text = View.UILanguageResources.GetString("S0000057");
            RunningTasksCModel.Columns[9].Text = View.UILanguageResources.GetString("S0000058");
            RunningTasksCModel.Columns[10].Text = View.UILanguageResources.GetString("S0000059");
        }

        private void DoubleClickIcon(object sender, EventArgs e)
        {
            if (this.WindowVisible)
            {
                this.WindowVisible = false;
                this.Visible = false;
            }
            else
            {
                this.WindowVisible = true;
                this.Visible = true;
            }
        }


        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Сворачивание в трей
            this.WindowVisible = false;
            this.Visible = false;

            if (SharedData.Exit)
            {
                MainTimer.Stop();
                tipsTimer.Stop();
                for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
                {
                    //Остановка заданий
                    for (int k = 0; k < SharedData.WorkSpaces[i].Tasks.Count; k++)
                    {
                        if (SharedData.WorkSpaces[i].Tasks[k].Status != 0 && SharedData.WorkSpaces[i].Tasks[k].Status != 5)
                        {
                            SharedData.WorkSpaces[i].Tasks[k].Stop();
                            SharedData.WorkSpaces[i].Tasks[k].Status = 0;
                        }
                    }
                    //Сохранение
                    SharedData.WorkSpaces[i].Save();
                }
                IESound.Enabled = true;
                NotifyIcon.Dispose();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void newWorkSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewWS newWSWindow = new NewWS();
            newWSWindow.ShowDialog();

            if (newWSWindow.Ok)
            {
                if (SharedData.WorkSpaces.Count != 0)
                {
                    SharedData.SelectedWorkSpace = 0;
                }
                UpdateWSTable();
            }
        }

        private void addWSButton_Click(object sender, EventArgs e)
        {
            NewWS newWSWindow = new NewWS();
            newWSWindow.ShowDialog();

            if (newWSWindow.Ok)
            {
                if (SharedData.WorkSpaces.Count != 0)
                {
                    SharedData.SelectedWorkSpace = 0;
                }
                UpdateWSTable();
            }
        }

        private void UpdateWSTable()
        {
            selectedTask = -1;
            //Обновление списка WS
            WSTable.BeginUpdate();
            WSTModel.Rows.Clear();
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                WSTModel.Rows.Add(new XPTable.Models.Row());
                WSTModel.Rows[i].Cells.Add(new XPTable.Models.Cell(string.Empty, Resource.WorkSpace));
                WSTModel.Rows[i].Cells.Add(new XPTable.Models.Cell(SharedData.WorkSpaces[i].Name));
                //Подсчет данных для статистики
                int TD = 0;
                int TT = 0;
                for (int k = 0; k < SharedData.WorkSpaces[i].Statistics.Years.Count; k++)
                {
                    for (int l = 0; l < SharedData.WorkSpaces[i].Statistics.Years[k].Weeks.Count; l++)
                    {
                        TD += SharedData.WorkSpaces[i].Statistics.Years[k].Weeks[l].TD;
                        TT += SharedData.WorkSpaces[i].Statistics.Years[k].Weeks[l].TT;
                    }
                }
                if (TT != 0)
                {
                    if ((TD / TT) > 1)
                    {
                        WSTModel.Rows[i].Cells.Add(new XPTable.Models.Cell(100));
                    }
                    else
                    {
                        float td = TD;
                        float tt = TT;
                        WSTModel.Rows[i].Cells.Add(new XPTable.Models.Cell((int)((td / tt) * 100)));
                    }
                }
                else
                {
                    WSTModel.Rows[i].Cells.Add(new XPTable.Models.Cell(100));
                }
            }
            WSTable.EndUpdate();

            if (WSTModel.Rows.Count > 0)
            {
                if (WSTModel.Rows.Count <= SharedData.SelectedWorkSpace)
                {
                    SharedData.SelectedWorkSpace = 0;
                    WSCommentTextBox.Text = SharedData.WorkSpaces[0].Comment;
                }
            }
            else
            {
                SharedData.SelectedWorkSpace = -1;
                WSCommentTextBox.Text = string.Empty;
            }
            //Обновление окна статистики
        }

        private void tasksGroupBox_SizeChanged(object sender, EventArgs e)
        {
            todayButton.Location = new Point(tasksGroupBox.Width - 23, 1);
            delTaskButton.Location = new Point(tasksGroupBox.Width - 45, 1);
            addTaskButton.Location = new Point(tasksGroupBox.Width - 67, 1);
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            newsLabel.Location = new Point(newsGroupBox.Width - 16, 1);
        }

        private void NotifyIconMenuShowHide_Click(object sender, EventArgs e)
        {
            if (this.WindowVisible)
            {
                this.WindowVisible = false;
                this.Visible = false;
            }
            else
            {
                this.WindowVisible = true;
                this.Visible = true;
            }
        }

        private void NotifyIconMenuExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(View.UILanguageResources.GetString("S0000001"), View.UILanguageResources.GetString("S0000002"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Завершение работы

                Visible = true;
                SharedData.Exit = true;
                Close();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(View.UILanguageResources.GetString("S0000001"), View.UILanguageResources.GetString("S0000002"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Завершение работы

                SharedData.Exit = true;
                Close();
            }
        }

        #region UI
        private void showNewsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showNewsToolStripMenuItem.Checked)
            {
                showNewsToolStripMenuItem.Checked = false;
                newsGroupBox.Visible = false;
            }
            else
            {
                showNewsToolStripMenuItem.Checked = true;
                newsGroupBox.Visible = true;
            }
            SaveViewSettings();
        }

        private void showTipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showTipsToolStripMenuItem.Checked)
            {
                showTipsToolStripMenuItem.Checked = false;
                bottomPanel.Visible = false;
            }
            else
            {
                showTipsToolStripMenuItem.Checked = true;
                bottomPanel.Visible = true;
            }
            SaveViewSettings();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (englishToolStripMenuItem.Checked)
            {
                englishToolStripMenuItem.Checked = false;
                russianToolStripMenuItem.Checked = true;
            }
            else
            {
                englishToolStripMenuItem.Checked = true;
                russianToolStripMenuItem.Checked = false;
            }
            SaveViewSettings();
            UpdateLanguage();
            FillOutForm();
        }

        private void russianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (russianToolStripMenuItem.Checked)
            {
                russianToolStripMenuItem.Checked = false;
                englishToolStripMenuItem.Checked = true;
            }
            else
            {
                russianToolStripMenuItem.Checked = true;
                englishToolStripMenuItem.Checked = false;
            }
            SaveViewSettings();
            UpdateLanguage();
            FillOutForm();
        }

        private void UpdateLanguage()
        {
            if (englishToolStripMenuItem.Checked)
            {
                View.UILanguage = 0;
            }
            else
            {
                View.UILanguage = 1;
            }

            switch (View.UILanguage)
            {
                case 0:
                    {
                        View.UILanguageResources = new System.Resources.ResourceManager("Doorway_Studio.English", typeof(English).Assembly);
                        break;
                    }
                case 1:
                    {
                        View.UILanguageResources = new System.Resources.ResourceManager("Doorway_Studio.Russian", typeof(Russian).Assembly);
                        break;
                    }
                default:
                    {
                        View.UILanguageResources = new System.Resources.ResourceManager("Doorway_Studio.English", typeof(English).Assembly);
                        break;
                    }
            }
        }

        private void SaveViewSettings()
        {
            StringBuilder data = new StringBuilder(100);
            data.Append("News=");
            data.Append(showNewsToolStripMenuItem.Checked.ToString());
            data.Append("\r\n");
            data.Append("Tips=");
            data.Append(showTipsToolStripMenuItem.Checked.ToString());
            data.Append("\r\n");
            data.Append("Language=");
            if (englishToolStripMenuItem.Checked)
            {
                data.Append("0");
            }
            else if (russianToolStripMenuItem.Checked)
            {
                data.Append("1");
            }

            File.WriteAllText(Application.StartupPath + "\\view.ini", data.ToString(), Encoding.UTF8);
        }
        #endregion


        private void WSTable_CellClick(object sender, XPTable.Events.CellMouseEventArgs e)
        {
            SharedData.SelectedWorkSpace = e.Row;
            WSCommentTextBox.Text = SharedData.WorkSpaces[e.Row].Comment;

            updateStatisticsCounter = 0;
            UpdateStatistics();
        }

        private void WSTable_CellDoubleClick(object sender, XPTable.Events.CellMouseEventArgs e)
        {
            if (SharedData.SelectedWorkSpace == -1)
            {
                return;
            }
            Statistics StatisticsWindow = new Statistics();
            StatisticsWindow.Type = 0;
            StatisticsWindow.SelectedWS = SharedData.SelectedWorkSpace;
            StatisticsWindow.ShowDialog();
        }

        private void editWSButton_Click(object sender, EventArgs e)
        {
            if (SharedData.SelectedWorkSpace == -1)
            {
                return;
            }
            Statistics StatisticsWindow = new Statistics();
            StatisticsWindow.Type = 0;
            StatisticsWindow.SelectedWS = SharedData.SelectedWorkSpace;
            StatisticsWindow.ShowDialog();
        }

        #region Import
        private void importKeywordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            ImportKeywords ImportKeywordsWindow = new ImportKeywords();
            ImportKeywordsWindow.ShowDialog();
        }

        private void importTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            ImportTemplate ImportTemplateWindow = new ImportTemplate();
            ImportTemplateWindow.ShowDialog();
        }

        private void importTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            ImportText ImportTextWindow = new ImportText();
            ImportTextWindow.ShowDialog();
        }

        private void importBigTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            ImportBigText ImportTextWindow = new ImportBigText();
            ImportTextWindow.ShowDialog();
        }

        private void importPresetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            ImportPreset ImportPresetWindow = new ImportPreset();
            ImportPresetWindow.ShowDialog();
        }
        #endregion

        #region ExportEdit
        private void exportKeywordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            ExEdKeywords ExEdKeywordsWindow = new ExEdKeywords();
            ExEdKeywordsWindow.ShowDialog();
        }

        private void editKeywordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            ExEdKeywords ExEdKeywordsWindow = new ExEdKeywords();
            ExEdKeywordsWindow.ShowDialog();
        }

        private void exportTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            ExEdText ExEdTextWindow = new ExEdText();
            ExEdTextWindow.ShowDialog();
        }

        private void editTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            ExEdText ExEdTextWindow = new ExEdText();
            ExEdTextWindow.ShowDialog();
        }

        private void exportPresetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            ExEdPresets ExEdPresetWindow = new ExEdPresets();
            ExEdPresetWindow.ShowDialog();
        }

        private void editPresetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            ExEdPresets ExEdPresetWindow = new ExEdPresets();
            ExEdPresetWindow.ShowDialog();
        }

        private void exportTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            ExEdTemplates ExEdTemplatesWindow = new ExEdTemplates();
            ExEdTemplatesWindow.ShowDialog();
        }

        private void editTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            ExEdTemplates ExEdTemplatesWindow = new ExEdTemplates();
            ExEdTemplatesWindow.ShowDialog();
        }
        #endregion

        private void deleteWSButton_Click(object sender, EventArgs e)
        {
            if (SharedData.SelectedWorkSpace == -1)
            {
                return;
            }
            if (MessageBox.Show(View.UILanguageResources.GetString("S0000024") + SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Name + "?", View.UILanguageResources.GetString("S0000025"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Delete();
                SharedData.WorkSpaces.RemoveAt(SharedData.SelectedWorkSpace);
                UpdateWSTable();
            }
        }

        private void mainCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            SharedData.SelectedDate = mainCalendar.SelectionStart;
            UpdateTaskListTable();
            UpdateTaskTable();
        }

        private void UpdateTaskListTable()
        {
            TasksTable.BeginUpdate();
            TasksTModel.Rows.Clear();
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                for (int j = 0; j < SharedData.WorkSpaces[i].Tasks.Count; j++)
                {
                    if (SharedData.WorkSpaces[i].Tasks[j].StartTime.Date == SharedData.SelectedDate.Date)
                    {
                        TasksTModel.Rows.Add(new XPTable.Models.Row());
                        switch (SharedData.WorkSpaces[i].Tasks[j].Status)
                        {
                            case 0:
                                {
                                    TasksTModel.Rows[TasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(string.Empty, Resource.SmallNew));
                                    break;
                                }
                            case 1:
                                {
                                    TasksTModel.Rows[TasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(string.Empty, Resource.SmallRunning));
                                    break;
                                }
                            case 2:
                                {
                                    TasksTModel.Rows[TasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(string.Empty, Resource.SmallPause));
                                    break;
                                }
                            case 3:
                            case 6:
                                {
                                    TasksTModel.Rows[TasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(string.Empty, Resource.SmallStop));
                                    break;
                                }
                            case 4:
                                {
                                    TasksTModel.Rows[TasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(string.Empty, Resource.SmallUp));
                                    break;
                                }
                            case 5:
                                {
                                    TasksTModel.Rows[TasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(string.Empty, Resource.SmallDone));
                                    break;
                                }
                        }
                        TasksTModel.Rows[TasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(SharedData.WorkSpaces[i].Tasks[j].Name));
                        TasksTModel.Rows[TasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(string.Empty));
                        TasksTModel.Rows[TasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(string.Empty));
                    }
                }
            }
            TasksTable.EndUpdate();
            if (TasksTModel.Rows.Count == 0)
            {
                selectedTask = -1;
            }
        }

        private void UpdateNews()
        {
            newsBrowser.Navigate("http://umaxsoftworks.com/PrivateArea/News/UDS&Top");
        }

        private int updateStatisticsCounter;
        private void UpdateStatistics()
        {
            //Checking for debuggers
            if (BlackList.CheckDebuggers())
            {
                MainTimer.Stop();
                SharedData.Exit = true;
                Close();
            }
            //Updating
            if (updateStatisticsCounter == 0)
            {
                string statistics = Resource.ResourceManager.GetString("Statistics");
                statistics = statistics.Replace("#Name#", "Umax");
                
                statistics = statistics.Replace("#NewsContent#", SharedData.ProjectNews);
                statistics = statistics.Replace("#Statistics#", View.UILanguageResources.GetString("S0000017"));
                statistics = statistics.Replace("#News#", View.UILanguageResources.GetString("S0000018"));
                statistics = statistics.Replace("#Tasks#", View.UILanguageResources.GetString("S0000019"));
                statistics = statistics.Replace("#Doorways#", View.UILanguageResources.GetString("S0000020"));
                statistics = statistics.Replace("#Pages#", View.UILanguageResources.GetString("S0000021"));
                statistics = statistics.Replace("#CurrentTasks#", View.UILanguageResources.GetString("S0000022"));

                if (SharedData.WorkSpaces.Count == 0)
                {
                    statistics = statistics.Replace("#WorkSpace#", string.Empty);
                    statistics = statistics.Replace("#TD#", "-");
                    statistics = statistics.Replace("#TT#", "-");
                    statistics = statistics.Replace("#DD#", "-");
                    statistics = statistics.Replace("#DT#", "-");
                    statistics = statistics.Replace("#PD#", "-");
                    statistics = statistics.Replace("#CTB#<li>#CTBValue#</li>#/CTB#", string.Empty);
                }
                else
                {
                    string baloonMessage = string.Empty;
                    for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
                    {
                        List<int> okTasks = SharedData.WorkSpaces[i].StatisticsUpdate();
                        if (MainSettings.ShowBaloons && okTasks.Count > 0)
                        {
                            for (int j = 0; j < okTasks.Count; j++)
                            {
                                baloonMessage += View.UILanguageResources.GetString("S0000023") + ": " + SharedData.WorkSpaces[i].Name + ": " + SharedData.WorkSpaces[i].Tasks[okTasks[j]].Name + "!\r\n";
                            }
                            if (baloonMessage.EndsWith("\r\n"))
                            {
                                baloonMessage = baloonMessage.Substring(0, baloonMessage.Length - 2);
                            }
                        }
                    }

                    GC.Collect();

                    if (MainSettings.ShowBaloons && baloonMessage != string.Empty)
                    {
                        ShowBaloon(baloonMessage);
                    }
                    UpdateWSTable();

                    statistics = statistics.Replace("#WorkSpace#", SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Name);
                    int statisticsNumber = 0;
                    for (int i = 0; i < SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Statistics.Years.Count; i++)
                    {
                        for (int j = 0; j < SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Statistics.Years[i].Weeks.Count; j++)
                        {
                            statisticsNumber += SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Statistics.Years[i].Weeks[j].TD;
                        }
                    }
                    statistics = statistics.Replace("#TD#", statisticsNumber.ToString());
                    statisticsNumber = 0;
                    for (int i = 0; i < SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Statistics.Years.Count; i++)
                    {
                        for (int j = 0; j < SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Statistics.Years[i].Weeks.Count; j++)
                        {
                            statisticsNumber += SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Statistics.Years[i].Weeks[j].TT;
                        }
                    }
                    statistics = statistics.Replace("#TT#", statisticsNumber.ToString());
                    statisticsNumber = 0;
                    for (int i = 0; i < SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Statistics.Years.Count; i++)
                    {
                        for (int j = 0; j < SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Statistics.Years[i].Weeks.Count; j++)
                        {
                            statisticsNumber += SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Statistics.Years[i].Weeks[j].DD;
                        }
                    }
                    statistics = statistics.Replace("#DD#", statisticsNumber.ToString());
                    statisticsNumber = 0;
                    for (int i = 0; i < SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Statistics.Years.Count; i++)
                    {
                        for (int j = 0; j < SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Statistics.Years[i].Weeks.Count; j++)
                        {
                            statisticsNumber += SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Statistics.Years[i].Weeks[j].DT;
                        }
                    }
                    statistics = statistics.Replace("#DT#", statisticsNumber.ToString());
                    statisticsNumber = 0;
                    for (int i = 0; i < SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Statistics.Years.Count; i++)
                    {
                        for (int j = 0; j < SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Statistics.Years[i].Weeks.Count; j++)
                        {
                            statisticsNumber += SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Statistics.Years[i].Weeks[j].PD;
                        }
                    }
                    statistics = statistics.Replace("#PD#", statisticsNumber.ToString());

                    string runningTasksTemplate = statistics.Substring(statistics.IndexOf("#CTB#"), statistics.IndexOf("#/CTB#") + 6 - statistics.IndexOf("#CTB#"));
                    runningTasksTemplate = runningTasksTemplate.Replace("#CTB#", string.Empty).Replace("#/CTB#", string.Empty);
                    string runningTasks = string.Empty;
                    for (int i = 0; i < SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Tasks.Count; i++)
                    {
                        if (SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Tasks[i].Status != 0 &&
                            SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Tasks[i].Status != 5 &&
                            SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Tasks[i].Status != 6)
                        {
                            runningTasks += runningTasksTemplate.Replace("#CTBValue#", SharedData.WorkSpaces[SharedData.SelectedWorkSpace].Tasks[i].Name);
                        }
                    }
                    statistics = statistics.Replace("#CTB#<li>#CTBValue#</li>#/CTB#", runningTasks);
                }
                statisticsBrowser.DocumentText = statistics;
            }
            updateStatisticsCounter++;

            if (updateStatisticsCounter >= 5)
            {
                updateStatisticsCounter = 0;
            }
        }

        private void UpdateTaskTable()
        {
            RunningTasksTable.BeginUpdate();
            RunningTasksTModel.Rows.Clear();

            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                for (int j = 0; j < SharedData.WorkSpaces[i].Tasks.Count; j++)
                {
                    if (SharedData.WorkSpaces[i].Tasks[j].Status != 0 && SharedData.WorkSpaces[i].Tasks[j].Status != 5 && SharedData.WorkSpaces[i].Tasks[j].StartTime < DateTime.Now)
                    {
                        try
                        {
                            RunningTasksTModel.Rows.Add(new XPTable.Models.Row());

                            RunningTasksTModel.Rows[RunningTasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(string.Empty));
                            RunningTasksTModel.Rows[RunningTasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(string.Empty));
                            RunningTasksTModel.Rows[RunningTasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(string.Empty));

                            RunningTasksTModel.Rows[RunningTasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(SharedData.WorkSpaces[i].Name));
                            RunningTasksTModel.Rows[RunningTasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(SharedData.WorkSpaces[i].Tasks[j].Name));

                            RunningTasksTModel.Rows[RunningTasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(SharedData.WorkSpaces[i].Tasks[j].WorkingStatus));

                            switch (SharedData.WorkSpaces[i].Tasks[j].Status)
                            {
                                case 1:
                                    {
                                        RunningTasksTModel.Rows[RunningTasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(View.UILanguageResources.GetString("S0000026")));
                                        break;
                                    }
                                case 2:
                                    {
                                        RunningTasksTModel.Rows[RunningTasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(View.UILanguageResources.GetString("S0000027")));
                                        break;
                                    }
                                case 3:
                                    {
                                        RunningTasksTModel.Rows[RunningTasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(View.UILanguageResources.GetString("S0000028")));
                                        break;
                                    }
                                case 4:
                                    {
                                        RunningTasksTModel.Rows[RunningTasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(View.UILanguageResources.GetString("S0000029")));
                                        break;
                                    }
                                case 6:
                                    {
                                        RunningTasksTModel.Rows[RunningTasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(View.UILanguageResources.GetString("S0000030")));
                                        break;
                                    }
                            }
                            RunningTasksTModel.Rows[RunningTasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(string.Empty));
                            RunningTasksTModel.Rows[RunningTasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(string.Empty));
                            RunningTasksTModel.Rows[RunningTasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell(SharedData.WorkSpaces[i].Tasks[j].StartTime.ToString()));
                            RunningTasksTModel.Rows[RunningTasksTModel.Rows.Count - 1].Cells.Add(new XPTable.Models.Cell((DateTime.Now - SharedData.WorkSpaces[i].Tasks[j].StartTime).ToString()));
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            RunningTasksTable.EndUpdate();
        }

        private void todayButton_Click(object sender, EventArgs e)
        {
            if (DateTime.Now > mainCalendar.MinDate)
            {
                mainCalendar.SelectionStart = DateTime.Now;
            }
        }

        #region Tasks
        private void addTaskButton_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            NewEdTask NewEdTaskWindow = new NewEdTask();
            NewEdTaskWindow.ShowDialog();
            if (NewEdTaskWindow.Ok)
            {
                UpdateStatistics();
                UpdateTaskListTable();
                UpdateTaskTable();
            }
        }

        private void delTaskButton_Click(object sender, EventArgs e)
        {
            if (selectedTask == -1)
            {
                return;
            }
            //Определение WS Index & Task Index для которого было сделан клик
            int taskCount = -1;
            int currentWSIndex = -1;
            int currentTaskIndex = -1;

            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                bool ok = false;
                for (int j = 0; j < SharedData.WorkSpaces[i].Tasks.Count; j++)
                {
                    if (SharedData.WorkSpaces[i].Tasks[j].StartTime.Date == SharedData.SelectedDate.Date)
                    {
                        taskCount++;
                        if (taskCount == selectedTask)
                        {
                            currentWSIndex = i;
                            currentTaskIndex = j;
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

            if (currentWSIndex == -1 || currentTaskIndex == -1)
            {
                return;
            }
            //Удаление задания
            try
            {
                SharedData.WorkSpaces[currentWSIndex].DeleteTask(SharedData.WorkSpaces[currentWSIndex].Tasks[currentTaskIndex].ID);
                selectedTask = -1;
            }
            catch (Exception)
            {
                MessageBox.Show(View.UILanguageResources.GetString("S0000030"), View.UILanguageResources.GetString("S0000030"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Обновление
            UpdateStatistics();
            UpdateTaskListTable();
            UpdateTaskTable();
        }

        private int selectedTask;
        private void TasksTable_CellClick(object sender, XPTable.Events.CellMouseEventArgs e)
        {
            selectedTask = e.Row;
        }

        /// <summary>
        /// Редактирование задания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TasksTable_CellDoubleClick(object sender, XPTable.Events.CellMouseEventArgs e)
        {
            int taskCount = -1;
            int currentWSIndex = -1;
            int currentTaskIndex = -1;

            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                bool ok = false;
                for (int j = 0; j < SharedData.WorkSpaces[i].Tasks.Count; j++)
                {
                    if (SharedData.WorkSpaces[i].Tasks[j].StartTime.Date == SharedData.SelectedDate.Date)
                    {
                        taskCount++;
                        if (taskCount == e.Row)
                        {
                            currentWSIndex = i;
                            currentTaskIndex = j;
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

            if (currentWSIndex == -1 || currentTaskIndex == -1)
            {
                return;
            }

            NewEdTask NewEdTaskWindow = new NewEdTask();
            NewEdTaskWindow.Edit = true;
            NewEdTaskWindow.WSIndex = currentWSIndex;
            NewEdTaskWindow.TaskIndex = currentTaskIndex;

            NewEdTaskWindow.ShowDialog();
        }
        #endregion

        private void TasksTable_CellButtonClicked(object sender, XPTable.Events.CellButtonEventArgs e)
        {
            //Определение WS Index & Task Index для которого было сделан клик
            int taskCount = -1;
            int currentWSIndex = -1;
            int currentTaskIndex = -1;

            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                bool ok = false;
                for (int j = 0; j < SharedData.WorkSpaces[i].Tasks.Count; j++)
                {
                    if (SharedData.WorkSpaces[i].Tasks[j].StartTime.Date == SharedData.SelectedDate.Date)
                    {
                        taskCount++;
                        if (taskCount == e.Row)
                        {
                            currentWSIndex = i;
                            currentTaskIndex = j;
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

            if (currentWSIndex == -1 || currentTaskIndex == -1)
            {
                return;
            }
            //Отображение окна с логом/спам-ссылками
            LogSpam LogSpamWindow = new LogSpam();
            LogSpamWindow.TaskName = SharedData.WorkSpaces[currentWSIndex].Tasks[currentTaskIndex].Name;
            LogSpamWindow.WSIndex = currentWSIndex;
            LogSpamWindow.TaskIndex = currentTaskIndex;
            if (e.Column == 3)
            {
                LogSpamWindow.ShowType = 1;
            }
            LogSpamWindow.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABox AboutWindow = new ABox();
            AboutWindow.ShowDialog();
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            //Запуск менеджера управления заданиями
            TasksManager();
        }

        private void TasksManager()
        {
            int tasksCount = 0;
            bool ok = false;
            //Searching running tasks
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                for (int j = 0; j < SharedData.WorkSpaces[i].Tasks.Count; j++)
                {
                    if (SharedData.WorkSpaces[i].Tasks[j].StartTime < DateTime.Now && ((SharedData.WorkSpaces[i].Tasks[j].Status == 1)
                        || (SharedData.WorkSpaces[i].Tasks[j].Status == 2) || (SharedData.WorkSpaces[i].Tasks[j].Status == 4)))
                    {
                        tasksCount++;
                    }
                }
            }
            //Starting
            if (tasksCount < MainSettings.MaxParallelTaks)
            {
                for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[i].Tasks.Count; j++)
                    {
                        if (SharedData.WorkSpaces[i].Tasks[j].StartTime < DateTime.Now && SharedData.WorkSpaces[i].Tasks[j].Status == 0)
                        {
                            //Изменение статистики
                            if ((SharedData.WorkSpaces[i].Tasks[j].StartTime.Year != DateTime.Now.Year) || (ItemStatistics.GetWeekNumber(SharedData.WorkSpaces[i].Tasks[j].StartTime) != ItemStatistics.GetWeekNumber(DateTime.Now)))
                            {
                                SharedData.WorkSpaces[i].StatisticsRemoveData(SharedData.WorkSpaces[i].Tasks[j].Settings.KeywordsID, SharedData.WorkSpaces[i].Tasks[j].PresetID, SharedData.WorkSpaces[i].Tasks[j].TemplateID, SharedData.WorkSpaces[i].Tasks[j].TextID, SharedData.WorkSpaces[i].Tasks[j].Settings.GeneralCreateDoorways, SharedData.WorkSpaces[i].Tasks[j].StartTime);
                                SharedData.WorkSpaces[i].StatisticsAddDataID(SharedData.WorkSpaces[i].Tasks[j].Settings.KeywordsID, SharedData.WorkSpaces[i].Tasks[j].PresetID, SharedData.WorkSpaces[i].Tasks[j].TemplateID, SharedData.WorkSpaces[i].Tasks[j].TextID, SharedData.WorkSpaces[i].Tasks[j].Settings.GeneralCreateDoorways, DateTime.Now);
                            }
                            SharedData.WorkSpaces[i].Tasks[j].StartTime = DateTime.Now;
                            SharedData.WorkSpaces[i].Tasks[j].WSIndex = i;
                            SharedData.WorkSpaces[i].Tasks[j].Start();
                            tasksCount++;
                        }
                        if (tasksCount >= MainSettings.MaxParallelTaks)
                        {
                            ok = true;
                            break;
                        }
                    }
                    if (ok)
                    {
                        break;
                    }
                }
            }
            //Updating UI
            UpdateStatistics();
            UpdateTaskListTable();
            UpdateTaskTable();
        }

        private void editOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options OptionsWindow = new Options();
            OptionsWindow.ShowDialog();
        }

        private bool ignoreMinimizedStart;
        private void Main_Shown(object sender, EventArgs e)
        {
            TasksManager();

            UpdateNews();

            if (ignoreMinimizedStart)
            {
                return;
            }

            if (MainSettings.MinimizedStart)
            {
                ignoreMinimizedStart = true;
                Close();
            }
        }

        private void RunningTasksTable_CellButtonClicked(object sender, XPTable.Events.CellButtonEventArgs e)
        {
            //Определение WS Index & Task Index для которого было сделан клик
            int currentWSIndex = -1;
            int currentTaskIndex = -1;

            //Определение рабочей области
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                if (SharedData.WorkSpaces[i].Name == RunningTasksTModel.Rows[e.Row].Cells[3].Text)
                {
                    currentWSIndex = i;
                    break;
                }
            }
            if (currentWSIndex == -1)
            {
                return;
            }
            //Определение задания
            for (int i = 0; i < SharedData.WorkSpaces[currentWSIndex].Tasks.Count; i++)
            {
                if (SharedData.WorkSpaces[currentWSIndex].Tasks[i].Name == RunningTasksTModel.Rows[e.Row].Cells[4].Text &&
                    SharedData.WorkSpaces[currentWSIndex].Tasks[i].StartTime.ToString() == RunningTasksTModel.Rows[e.Row].Cells[9].Text)
                {
                    currentTaskIndex = i;
                    break;
                }
            }

            if (currentWSIndex == -1 || currentTaskIndex == -1)
            {
                return;
            }

            //Действия
            if (e.Column == 0)
            {
                //Старт после паузы
                if (SharedData.WorkSpaces[currentWSIndex].Tasks[currentTaskIndex].Status == 2 ||
                    SharedData.WorkSpaces[currentWSIndex].Tasks[currentTaskIndex].Status == 3)
                {
                    SharedData.WorkSpaces[currentWSIndex].Tasks[currentTaskIndex].StartAfterPauseStop();
                }
            }
            else if (e.Column == 1)
            {
                //Пауза
                if (SharedData.WorkSpaces[currentWSIndex].Tasks[currentTaskIndex].Status == 1)
                {
                    SharedData.WorkSpaces[currentWSIndex].Tasks[currentTaskIndex].Pause();
                }
            }
            else if (e.Column == 2)
            {
                //Стоп
                if (SharedData.WorkSpaces[currentWSIndex].Tasks[currentTaskIndex].Status == 1 ||
                    SharedData.WorkSpaces[currentWSIndex].Tasks[currentTaskIndex].Status == 2 ||
                    SharedData.WorkSpaces[currentWSIndex].Tasks[currentTaskIndex].Status == 4)
                {
                    SharedData.WorkSpaces[currentWSIndex].Tasks[currentTaskIndex].Stop();
                }
            }
            else if (e.Column == 7 || e.Column == 8)
            {
                //Log/Spam
                LogSpam LogSpamWindow = new LogSpam();
                LogSpamWindow.TaskName = SharedData.WorkSpaces[currentWSIndex].Tasks[currentTaskIndex].Name;
                LogSpamWindow.WSIndex = currentWSIndex;
                LogSpamWindow.TaskIndex = currentTaskIndex;
                if (e.Column == 8)
                {
                    LogSpamWindow.ShowType = 1;
                }
                LogSpamWindow.ShowDialog();
            }
        }

        #region Statistics
        private void statisticsWorkSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            Statistics StatisticsWindow = new Statistics();
            StatisticsWindow.Type = 0;
            StatisticsWindow.ShowDialog();
        }

        private void statisticsKeywordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            Statistics StatisticsWindow = new Statistics();
            StatisticsWindow.Type = 1;
            StatisticsWindow.ShowDialog();
        }

        private void statisticsTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            Statistics StatisticsWindow = new Statistics();
            StatisticsWindow.Type = 2;
            StatisticsWindow.ShowDialog();
        }

        private void statisticsTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            Statistics StatisticsWindow = new Statistics();
            StatisticsWindow.Type = 3;
            StatisticsWindow.ShowDialog();
        }

        private void statisticsPresetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            Statistics StatisticsWindow = new Statistics();
            StatisticsWindow.Type = 4;
            StatisticsWindow.ShowDialog();
        }
        #endregion

        private void newsLabel_Click(object sender, EventArgs e)
        {
            UpdateNews();
        }
        private void ShowBaloon(string Message)
        {
            NotifyIcon.ShowBalloonTip(MainSettings.ShowBaloonsTime * 1000, this.Text, Message, ToolTipIcon.Info);
        }

        private void helpEULAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EULA EULAWindow = new EULA();
            EULAWindow.ShowDialog();
        }

        private void helpHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://umaxsoft.com/projects/uds/");
            }
            catch (Exception)
            {
            }
        }

        private void tasksNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            NewEdTask NewEdTaskWindow = new NewEdTask();
            NewEdTaskWindow.ShowDialog();
            if (NewEdTaskWindow.Ok)
            {
                UpdateStatistics();
                UpdateTaskListTable();
                UpdateTaskTable();
            }
        }

        private void tasksStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            Tasks TasksWindows = new Tasks();
            TasksWindows.ShowDialog();
        }

        private void tasksDeleteAllFinishedTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            //Message
            if (MessageBox.Show(View.UILanguageResources.GetString("S0000295"), View.UILanguageResources.GetString("S0000294"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Deleting
                for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
                {
                    while (SharedData.WorkSpaces[i].GetFinishedTaskIndex() != -1)
                    {
                        try
                        {
                            SharedData.WorkSpaces[i].DeleteTask(SharedData.WorkSpaces[i].Tasks[SharedData.WorkSpaces[i].GetFinishedTaskIndex()].ID);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }

        private void tasksDeleteAllTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SharedData.WorkSpaces.Count == 0)
            {
                return;
            }
            //Message
            if (MessageBox.Show(View.UILanguageResources.GetString("S0000292"), View.UILanguageResources.GetString("S0000291"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Deleting
                for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
                {
                    while (SharedData.WorkSpaces[i].Tasks.Count > 0)
                    {
                        SharedData.WorkSpaces[i].DeleteTask(SharedData.WorkSpaces[i].Tasks[0].ID);
                    }
                }
            }
        }

        #region Tools
        private void toolsMixerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Tools\\Mixer.exe"))
            {
                System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\Tools\\Mixer.exe");
            }
        }

        private void toolsKeySelectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Tools\\Key Selector.exe"))
            {
                System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\Tools\\Key Selector.exe");
            }
        }

        private void toolsKeyMakerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Tools\\Key Maker.exe"))
            {
                System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\Tools\\Key Maker.exe");
            }
        }

        private void toolsInfoGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Tools\\Info.txt Generator.exe"))
            {
                System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\Tools\\Info.txt Generator.exe");
            }
        }

        private void toolsTextCleanerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Tools\\Text Cleaner.exe"))
            {
                System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\Tools\\Text Cleaner.exe");
            }
        }

        private void toolsTextFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Tools\\Text Filter.exe"))
            {
                System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\Tools\\Text Filter.exe");
            }
        }

        private void templateEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Tools\\Template Editor.exe"))
            {
                System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\Tools\\Template Editor.exe");
            }
        }

        #endregion

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            if (this.WindowVisible)
            {
                this.WindowVisible = false;
                this.Visible = false;
            }
            else
            {
                this.WindowVisible = true;
                this.Visible = true;
            }
        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Проверка последней версии
            string updateUrl = string.Empty;
            if (CheckVersion(out updateUrl))
            {
                if (MessageBox.Show(View.UILanguageResources.GetString("S0000254"), View.UILanguageResources.GetString("S0000253"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Updater AUWindow = new Updater();
                    AUWindow.URL = updateUrl;
                    AUWindow.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(View.UILanguageResources.GetString("S0000255"), View.UILanguageResources.GetString("S0000253"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Проверка обновления
        /// </summary>
        /// <param name="Url">Link to archive</param>
        /// <returns>True - есть новая версия, False - нет новой версии</returns>
        private bool CheckVersion(out string Url)
        {
            Url = string.Empty;
            System.Net.WebClient downloader = new System.Net.WebClient();
            try
            {
                string downloadedData = downloader.DownloadString("http://umaxsoft.com/PrivateArea/Versions/UDS");

                //Разбивка данных на части
                string version = downloadedData.Substring(downloadedData.IndexOf("<version>") + 9, downloadedData.IndexOf("</version>") - downloadedData.IndexOf("<version>") - 9);
                Url = downloadedData.Substring(downloadedData.IndexOf("<url>") + 5, downloadedData.IndexOf("</url>") - downloadedData.IndexOf("<url>") - 5);
                //Проверка
                if (int.Parse(version.Replace(".", string.Empty)) > int.Parse(Application.ProductVersion.Replace(".", string.Empty)))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        private void tipsTimer_Tick(object sender, EventArgs e)
        {
            tipsTextBox.Text = View.UILanguageResources.GetString("S000102" + new Random().Next(0, 10).ToString());
        }

        private void automativeFTPUploaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Tools\\Automative FTP Uploader.exe"))
            {
                System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\Tools\\Automative FTP Uploader.exe");
            }
        }
    }
}
