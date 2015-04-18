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
    public partial class Tasks : Form
    {
        public Tasks()
        {
            InitializeComponent();
        }

        private void Tasks_Load(object sender, EventArgs e)
        {
            this.Icon = Resource.MainIcon;
            FillOutForm();
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                tasksWSComboBox.Items.Add(SharedData.WorkSpaces[i].Name);
            }
            tasksWSComboBox.SelectedIndex = 0;

            // Select year, if only 1 present
            if (settingsYearComboBox.Items.Count == 1)
            {
                settingsYearComboBox.SelectedIndex = 0;
            }
        }

        private void FillOutForm()
        {
            this.Text = View.UILanguageResources.GetString("S0000019");
            tasksGroupBox.Text = View.UILanguageResources.GetString("S0000019");

            actionsGroupBox.Text = View.UILanguageResources.GetString("S0000052");
            actionsDeleteButton.Text = View.UILanguageResources.GetString("S0000065");
            actionsCopyButton.Text = View.UILanguageResources.GetString("S0000070");

            settingsGroupBox.Text = View.UILanguageResources.GetString("S0000095");
            settingsYearLabel.Text = View.UILanguageResources.GetString("S0000097");
            settingsMonthLabel.Text = View.UILanguageResources.GetString("S0000098");
        }

        private void tasksWSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tasksTreeView.Nodes.Clear();
            //Заполнение полей года
            List<int> years = new List<int>();
            for (int i = 0; i < SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks.Count; i++)
            {
                if (years.Count != 0)
                {
                    bool found = false;
                    //Поиск
                    for (int k = 0; k < years.Count; k++)
                    {
                        if (years[k] == SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Year)
                        {
                            found = true;
                        }
                    }
                    //Добавление
                    if (!found)
                    {
                        years.Add(SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Year);
                    }
                }
                else
                {
                    years.Add(SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Year);
                }
            }
            //Заполнение полей года
            settingsYearComboBox.Items.Clear();
            for (int i = 0; i < years.Count; i++)
            {
                settingsYearComboBox.Items.Add(years[i].ToString());
            }

            settingsMonthComboBox.Items.Clear();
            //Заполнение полей месяцев
            settingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000262"));
            settingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000263"));
            settingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000264"));
            settingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000265"));
            settingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000266"));
            settingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000267"));
            settingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000268"));
            settingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000269"));
            settingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000270"));
            settingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000271"));
            settingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000272"));
            settingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000273"));
        }

        private void settingsYearComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (settingsYearComboBox.SelectedIndex == -1)
            {
                return;
            }
            settingsMonthComboBox.SelectedIndex = -1;

            UpdateTreeView();
        }

        private void settingsMonthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (settingsMonthComboBox.SelectedIndex == -1)
            {
                return;
            }

            UpdateTreeView();
        }

        private void actionsDeleteButton_Click(object sender, EventArgs e)
        {
            if (tasksTreeView.SelectedNode == null)
            {
                return;
            }
            if (tasksTreeView.SelectedNode.Parent == null)
            {
                return;
            }
            if (tasksTreeView.SelectedNode.Parent.Parent == null)
            {
                return;
            }
            for (int i = 0; i < SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks.Count; i++)
            {
                if (SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].Name == tasksTreeView.SelectedNode.Text &&
                    SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Date.ToString().Substring(0, SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Date.ToString().IndexOf(" ")) == tasksTreeView.SelectedNode.Parent.Text)
                {
                    //Удаление
                    SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].DeleteTask(SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].ID);
                    UpdateTreeView();
                    return;
                }
            }
        }

        private void actionsCopyButton_Click(object sender, EventArgs e)
        {
            if (tasksTreeView.SelectedNode == null)
            {
                return;
            }
            if (tasksTreeView.SelectedNode.Parent == null)
            {
                return;
            }
            if (tasksTreeView.SelectedNode.Parent.Parent == null)
            {
                return;
            }
            for (int i = 0; i < SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks.Count; i++)
            {
                if (SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].Name == tasksTreeView.SelectedNode.Text &&
                    SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Date.ToString().Substring(0, SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Date.ToString().IndexOf(" ")) == tasksTreeView.SelectedNode.Parent.Text)
                {
                    //Создание нового задания
                    Task newTask = new Task(GetNextTaskID(tasksWSComboBox.SelectedIndex), SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].Name, SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].Comment);

                    //SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i];
                    newTask.Status = 0;

                    newTask.StartTime = DateTime.Now.Date;
                    newTask.StartTime = newTask.StartTime.AddHours(DateTime.Now.Hour + 1);
                    newTask.StartTime = newTask.StartTime.AddMinutes(DateTime.Now.Minute);

                    newTask.EndTime = new DateTime();

                    newTask.PresetID = SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].PresetID;
                    newTask.Settings = SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].Settings;
                    newTask.TemplateID = SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].TemplateID;
                    newTask.TextID = SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].TextID;
                    newTask.WSIndex = SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].WSIndex;

                    SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks.Add(newTask);
                    //Updating WS Statistics
                    
                    SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].StatisticsAddDataID(newTask.Settings.KeywordsID, newTask.PresetID, newTask.TemplateID, newTask.TextID, newTask.Settings.GeneralCreateDoorways, newTask.StartTime);

                    UpdateTreeView();
                    return;
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

        private void UpdateTreeView()
        {
            //Заполнение таблицы
            tasksTreeView.Nodes.Clear();

            if (settingsYearComboBox.SelectedIndex != -1)
            {
                if (settingsMonthComboBox.SelectedIndex != -1)
                {
                    //Месяцы
                    List<DateNodes> nodes = new List<DateNodes>();
                    for (int i = 0; i < SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks.Count; i++)
                    {
                        if (SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Month == (settingsMonthComboBox.SelectedIndex + 1))
                        {
                            if (nodes.Count != 0)
                            {
                                bool found = false;
                                //Поиск  и добавление
                                for (int k = 0; k < nodes.Count; k++)
                                {
                                    if (nodes[k].Date == SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Date)
                                    {
                                        nodes[k].Nodes.Add(new TreeNode(SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].Name, 2, 2));
                                        found = true;
                                    }
                                }
                                //Добавление новой даты
                                if (!found)
                                {
                                    nodes.Add(new DateNodes(SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Date));
                                    nodes[nodes.Count - 1].Nodes.Add(new TreeNode(SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].Name, 2, 2));
                                }
                            }
                            else
                            {
                                //Добавление новой даты
                                nodes.Add(new DateNodes(SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Date));
                                nodes[nodes.Count - 1].Nodes.Add(new TreeNode(SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].Name, 2, 2));
                            }
                        }
                    }

                    TreeNode mainTreeNode = new TreeNode(SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Name, 0, 0);
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        mainTreeNode.Nodes.Add(new TreeNode(nodes[i].Date.ToString().Substring(0, nodes[i].Date.ToString().IndexOf(" ")), 1, 1, nodes[i].Nodes.ToArray()));
                    }
                    tasksTreeView.Nodes.Add(mainTreeNode);
                    tasksTreeView.ExpandAll();
                }
                else
                {
                    //Года
                    List<DateNodes> nodes = new List<DateNodes>();
                    for (int i = 0; i < SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks.Count; i++)
                    {
                        if (nodes.Count != 0)
                        {
                            bool found = false;
                            //Поиск  и добавление
                            for (int k = 0; k < nodes.Count; k++)
                            {
                                if (nodes[k].Date == SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Date)
                                {
                                    nodes[k].Nodes.Add(new TreeNode(SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].Name, 2, 2));
                                    found = true;
                                }
                            }
                            //Добавление новой даты
                            if (!found)
                            {
                                nodes.Add(new DateNodes(SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Date));
                                nodes[nodes.Count - 1].Nodes.Add(new TreeNode(SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].Name, 2, 2));
                            }
                        }
                        else
                        {
                            //Добавление новой даты
                            nodes.Add(new DateNodes(SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Date));
                            nodes[nodes.Count - 1].Nodes.Add(new TreeNode(SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].Name, 2, 2));
                        }
                    }
                    TreeNode mainTreeNode = new TreeNode(SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Name, 0, 0);
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        mainTreeNode.Nodes.Add(new TreeNode(nodes[i].Date.ToString().Substring(0, nodes[i].Date.ToString().IndexOf(" ")), 1, 1, nodes[i].Nodes.ToArray()));
                    }
                    tasksTreeView.Nodes.Add(mainTreeNode);
                    tasksTreeView.ExpandAll();
                }
            }
        }

        class DateNodes
        {
            public DateNodes()
            {
                nodes = new List<TreeNode>();
            }

            public DateNodes(DateTime Date)
                : this()
            {
                this.date = Date;
            }

            private DateTime date;
            /// <summary>
            /// Дата
            /// </summary>
            public DateTime Date
            {
                set
                {
                    date = value;
                }
                get
                {
                    return date;
                }
            }

            private List<TreeNode> nodes;
            /// <summary>
            /// Ноды
            /// </summary>
            public List<TreeNode> Nodes
            {
                set
                {
                    nodes = value;
                }
                get
                {
                    return nodes;
                }
            }

        }

        private void tasksTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (tasksTreeView.SelectedNode == null)
            {
                return;
            }
            if (tasksTreeView.SelectedNode.Parent == null)
            {
                return;
            }
            if (tasksTreeView.SelectedNode.Parent.Parent == null)
            {
                return;
            }
            for (int i = 0; i < SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks.Count; i++)
            {
                if (SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].Name == tasksTreeView.SelectedNode.Text &&
                    SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Date.ToString().Substring(0, SharedData.WorkSpaces[tasksWSComboBox.SelectedIndex].Tasks[i].StartTime.Date.ToString().IndexOf(" ")) == tasksTreeView.SelectedNode.Parent.Text)
                {
                    //Открыте окна с настройками
                    NewEdTask NewEdTaskWindow = new NewEdTask();
                    NewEdTaskWindow.Edit = true;
                    NewEdTaskWindow.WSIndex = tasksWSComboBox.SelectedIndex;
                    NewEdTaskWindow.TaskIndex = i;
                    NewEdTaskWindow.ShowDialog();
                    return;
                }
            }
        }
    }
}
