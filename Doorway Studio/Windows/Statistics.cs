using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace Doorway_Studio
{
    public partial class Statistics : Form
    {
        public Statistics()
        {
            InitializeComponent();
        }

        private int type;

        /// <summary>
        /// Тип статистики:
        /// 0 - WS, 1 - Keywords, 2 - Templates, 3 - Templates, 4 - Presets
        /// </summary>
        public int Type
        {
            set
            {
                type = value;
            }
            get
            {
                return type;
            }
        }

        private int selectedWS;

        /// <summary>
        /// Selected WorkSpace
        /// </summary>
        public int SelectedWS
        {
            set
            {
                selectedWS = value;
            }
            get
            {
                return selectedWS;
            }
        }

        private void FillOutForm()
        {
            this.Text = View.UILanguageResources.GetString("S0000017");

            wsTabTSGroupBox.Text = View.UILanguageResources.GetString("S0000090");
            wsTabTMSGroupBox.Text = View.UILanguageResources.GetString("S0000091");
            wsTabTSTDTLabel.Text = View.UILanguageResources.GetString("S0000092");
            wsTabTMSTDTLabel.Text = View.UILanguageResources.GetString("S0000092");
            wsTabTSDDTLabel.Text = View.UILanguageResources.GetString("S0000093");
            wsTabTMSDDTLabel.Text = View.UILanguageResources.GetString("S0000093");
            wsTabTSPDLabel.Text = View.UILanguageResources.GetString("S0000094");
            wsTabTMSPDLabel.Text = View.UILanguageResources.GetString("S0000094");

            KeywordTabPage.Text = View.UILanguageResources.GetString("S0000036");
            keywordTabKeywordsLabel.Text = View.UILanguageResources.GetString("S0000036");
            keywordTabTSGroupBox.Text = View.UILanguageResources.GetString("S0000090");
            keywordTabTMSGroupBox.Text = View.UILanguageResources.GetString("S0000091");
            keywordTabTSTDTLabel.Text = View.UILanguageResources.GetString("S0000092");
            keywordTabTMSTDTLabel.Text = View.UILanguageResources.GetString("S0000092");
            keywordTabTSDDTLabel.Text = View.UILanguageResources.GetString("S0000093");
            keywordTabTMSDDTLabel.Text = View.UILanguageResources.GetString("S0000093");
            keywordTabTSPDLabel.Text = View.UILanguageResources.GetString("S0000094");
            keywordTabTMSPDLabel.Text = View.UILanguageResources.GetString("S0000094");

            TemplateTabPage.Text = View.UILanguageResources.GetString("S0000037");
            templateTabTemplatesLabel.Text = View.UILanguageResources.GetString("S0000037");
            templateTabTSGroupBox.Text = View.UILanguageResources.GetString("S0000090");
            templateTabTMSGroupBox.Text = View.UILanguageResources.GetString("S0000091");
            templateTabTSTDTLabel.Text = View.UILanguageResources.GetString("S0000092");
            templateTabTMSTDTLabel.Text = View.UILanguageResources.GetString("S0000092");
            templateTabTSDDTLabel.Text = View.UILanguageResources.GetString("S0000093");
            templateTabTMSDDTLabel.Text = View.UILanguageResources.GetString("S0000093");
            templateTabTSPDLabel.Text = View.UILanguageResources.GetString("S0000094");
            templateTabTMSPDLabel.Text = View.UILanguageResources.GetString("S0000094");

            TextTabPage.Text = View.UILanguageResources.GetString("S0000038");
            textTabTextsLabel.Text = View.UILanguageResources.GetString("S0000038");
            textTabTSGroupBox.Text = View.UILanguageResources.GetString("S0000090");
            textTabTMSGroupBox.Text = View.UILanguageResources.GetString("S0000091");
            textTabTSTDTLabel.Text = View.UILanguageResources.GetString("S0000092");
            textTabTMSTDTLabel.Text = View.UILanguageResources.GetString("S0000092");
            textTabTSDDTLabel.Text = View.UILanguageResources.GetString("S0000093");
            textTabTMSDDTLabel.Text = View.UILanguageResources.GetString("S0000093");
            textTabTSPDLabel.Text = View.UILanguageResources.GetString("S0000094");
            textTabTMSPDLabel.Text = View.UILanguageResources.GetString("S0000094");

            PresetTabPage.Text = View.UILanguageResources.GetString("S0000039");
            presetTabPresetsLabel.Text = View.UILanguageResources.GetString("S0000039");
            presetTabTSGroupBox.Text = View.UILanguageResources.GetString("S0000090");
            presetTabTMSGroupBox.Text = View.UILanguageResources.GetString("S0000091");
            presetTabTSTDTLabel.Text = View.UILanguageResources.GetString("S0000092");
            presetTabTMSTDTLabel.Text = View.UILanguageResources.GetString("S0000092");
            presetTabTSDDTLabel.Text = View.UILanguageResources.GetString("S0000093");
            presetTabTMSDDTLabel.Text = View.UILanguageResources.GetString("S0000093");
            presetTabTSPDLabel.Text = View.UILanguageResources.GetString("S0000094");
            presetTabTMSPDLabel.Text = View.UILanguageResources.GetString("S0000094");

            ChartTabPage.Text = View.UILanguageResources.GetString("S0000089");
            chartKeywordLabel.Text = View.UILanguageResources.GetString("S0000036");
            chartTemplateLabel.Text = View.UILanguageResources.GetString("S0000037");
            chartTextLabel.Text = View.UILanguageResources.GetString("S0000038");
            chartPresetLabel.Text = View.UILanguageResources.GetString("S0000039");
            chartSettingsGroupBox.Text = View.UILanguageResources.GetString("S0000095");
            chartSettingsTypeLabel.Text = View.UILanguageResources.GetString("S0000096");
            chartSettingsYearLabel.Text = View.UILanguageResources.GetString("S0000097");
            chartSettingsMonthLabel.Text = View.UILanguageResources.GetString("S0000098");

            chartSettingsTypeComboBox.Items.Clear();
            chartSettingsTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000019"));
            chartSettingsTypeComboBox.Items.Add(View.UILanguageResources.GetString("S0000020"));
        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            this.Icon = Resource.MainIcon;
            FillOutForm();
            switch (type)
            {
                case 0:
                    {
                        mainTabControl.SelectedIndex = 0;
                        break;
                    }
                case 1:
                    {
                        mainTabControl.SelectedIndex = 1;
                        break;
                    }
                case 2:
                    {
                        mainTabControl.SelectedIndex = 2;
                        break;
                    }
                case 3:
                    {
                        mainTabControl.SelectedIndex = 3;
                        break;
                    }
                case 4:
                    {
                        mainTabControl.SelectedIndex = 4;
                        break;
                    }
            }
            //Заполнение полей
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                wsTabWSComboBox.Items.Add(SharedData.WorkSpaces[i].Name);
                keywordTabWSComboBox.Items.Add(SharedData.WorkSpaces[i].Name);
                templateTabWSComboBox.Items.Add(SharedData.WorkSpaces[i].Name);
                textTabWSComboBox.Items.Add(SharedData.WorkSpaces[i].Name);
                presetTabWSComboBox.Items.Add(SharedData.WorkSpaces[i].Name);
                chartWSComboBox.Items.Add(SharedData.WorkSpaces[i].Name);
            }
            //Заполнение полей
            if (type == 0)
            {
                wsTabWSComboBox.SelectedIndex = selectedWS;
            }
            else
            {
                wsTabWSComboBox.SelectedIndex = 0;
            }

            chartSettingsTypeComboBox.SelectedIndex = 0;

            keywordTabWSComboBox.SelectedIndex = 0;
            templateTabWSComboBox.SelectedIndex = 0;
            textTabWSComboBox.SelectedIndex = 0;
            presetTabWSComboBox.SelectedIndex = 0;
            chartWSComboBox.SelectedIndex = 0;

            tipsTimer.Start();
            tipsTimer_Tick(sender, e);
        }

        private void wsTabWSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Общая статистика
            int TD = 0;
            int TT = 0;
            int DD = 0;
            int DT = 0;
            int PD = 0;
            for (int i = 0; i < SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years.Count; i++)
            {
                for (int j = 0; j < SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years[i].Weeks.Count; j++)
                {
                    TD += SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].TD;
                    TT += SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].TT;
                    DD += SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].DD;
                    DT += SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].DT;
                    PD += SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].PD;
                }
            }
            //Вывод статистики
            wsTabTSTDTNumLabel.Text = TD.ToString() + "/" + TT.ToString();
            wsTabTSDDTNumLabel.Text = DD.ToString() + "/" + DT.ToString();
            wsTabTSPDNumLabel.Text = PD.ToString();
            //Статистика за этот месяц
            List<int> weeks = ItemStatistics.WeeksInMonth(DateTime.Now);

            TD = 0;
            TT = 0;
            DD = 0;
            DT = 0;
            PD = 0;

            int yearIndex = -1;
            for (int i = 0; i < SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years.Count; i++)
            {
                if (SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years[i].Year == DateTime.Now.Year)
                {
                    yearIndex = i;
                    break;
                }
            }
            if (yearIndex != -1)
            {
                for (int i = 0; i < weeks.Count; i++)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; j++)
                    {
                        if (SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].Week == weeks[i])
                        {
                            TD += SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].TD;
                            TT += SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].TT;
                            DD += SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].DD;
                            DT += SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].DT;
                            PD += SharedData.WorkSpaces[wsTabWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].PD;
                        }
                    }
                }
            }

            //Вывод статистики
            wsTabTMSTDTNumLabel.Text = TD.ToString() + "/" + TT.ToString();
            wsTabTMSDDTNumLabel.Text = DD.ToString() + "/" + DT.ToString();
            wsTabTMSPDNumLabel.Text = PD.ToString();
        }

        private void keywordTabWSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            keywordTabKeywordsComboBox.Items.Clear();
            for (int i = 0; i < SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords.Count; i++)
            {
                keywordTabKeywordsComboBox.Items.Add(SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[i].Name);
            }
        }

        private void keywordTabKeywordsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (keywordTabKeywordsComboBox.SelectedIndex == -1)
            {
                return;
            }

            //Общая статистика
            int TD = 0;
            int TT = 0;
            int DD = 0;
            int DT = 0;
            int PD = 0;
            for (int i = 0; i < SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years.Count; i++)
            {
                for (int j = 0; j < SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years[i].Weeks.Count; j++)
                {
                    TD += SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].TD;
                    TT += SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].TT;
                    DD += SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].DD;
                    DT += SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].DT;
                    PD += SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].PD;
                }
            }
            //Вывод статистики
            keywordTabTSTDTNumLabel.Text = TD.ToString() + "/" + TT.ToString();
            keywordTabTSDDTNumLabel.Text = DD.ToString() + "/" + DT.ToString();
            keywordTabTSPDNumLabel.Text = PD.ToString();
            //Статистика за этот месяц
            List<int> weeks = ItemStatistics.WeeksInMonth(DateTime.Now);

            TD = 0;
            TT = 0;
            DD = 0;
            DT = 0;
            PD = 0;

            int yearIndex = -1;
            for (int i = 0; i < SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years.Count; i++)
            {
                if (SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years[i].Year == DateTime.Now.Year)
                {
                    yearIndex = i;
                    break;
                }
            }
            if (yearIndex != -1)
            {
                for (int i = 0; i < weeks.Count; i++)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; j++)
                    {
                        if (SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].Week == weeks[i])
                        {
                            TD += SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].TD;
                            TT += SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].TT;
                            DD += SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].DD;
                            DT += SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].DT;
                            PD += SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Keywords[keywordTabKeywordsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].PD;
                        }
                    }
                }
            }

            //Вывод статистики
            keywordTabTMSTDTNumLabel.Text = TD.ToString() + "/" + TT.ToString();
            keywordTabTMSDDTNumLabel.Text = DD.ToString() + "/" + DT.ToString();
            keywordTabTMSPDNumLabel.Text = PD.ToString();
        }

        private void templateTabWSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            templateTabTemplatesComboBox.Items.Clear();
            for (int i = 0; i < SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates.Count; i++)
            {
                templateTabTemplatesComboBox.Items.Add(SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Templates[i].Name);
            }
        }

        private void templateTabTemplatesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (templateTabTemplatesComboBox.SelectedIndex == -1)
            {
                return;
            }

            //Общая статистика
            int TD = 0;
            int TT = 0;
            int DD = 0;
            int DT = 0;
            int PD = 0;
            for (int i = 0; i < SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years.Count; i++)
            {
                for (int j = 0; j < SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years[i].Weeks.Count; j++)
                {
                    TD += SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].TD;
                    TT += SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].TT;
                    DD += SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].DD;
                    DT += SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].DT;
                    PD += SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].PD;
                }
            }
            //Вывод статистики
            templateTabTSTDTNumLabel.Text = TD.ToString() + "/" + TT.ToString();
            templateTabTSDDTNumLabel.Text = DD.ToString() + "/" + DT.ToString();
            templateTabTSPDNumLabel.Text = PD.ToString();
            //Статистика за этот месяц
            List<int> weeks = ItemStatistics.WeeksInMonth(DateTime.Now);

            TD = 0;
            TT = 0;
            DD = 0;
            DT = 0;
            PD = 0;

            int yearIndex = -1;
            for (int i = 0; i < SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years.Count; i++)
            {
                if (SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years[i].Year == DateTime.Now.Year)
                {
                    yearIndex = i;
                    break;
                }
            }
            if (yearIndex != -1)
            {
                for (int i = 0; i < weeks.Count; i++)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; j++)
                    {
                        if (SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].Week == weeks[i])
                        {
                            TD += SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].TD;
                            TT += SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].TT;
                            DD += SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].DD;
                            DT += SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].DT;
                            PD += SharedData.WorkSpaces[templateTabWSComboBox.SelectedIndex].Templates[templateTabTemplatesComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].PD;
                        }
                    }
                }
            }

            //Вывод статистики
            templateTabTMSTDTNumLabel.Text = TD.ToString() + "/" + TT.ToString();
            templateTabTMSDDTNumLabel.Text = DD.ToString() + "/" + DT.ToString();
            templateTabTMSPDNumLabel.Text = PD.ToString();
        }

        private void textTabWSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            textTabTextsComboBox.Items.Clear();
            for (int i = 0; i < SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts.Count; i++)
            {
                textTabTextsComboBox.Items.Add(SharedData.WorkSpaces[keywordTabWSComboBox.SelectedIndex].Texts[i].Name);
            }
        }

        private void textTabTextsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textTabTextsComboBox.SelectedIndex == -1)
            {
                return;
            }

            //Общая статистика
            int TD = 0;
            int TT = 0;
            int DD = 0;
            int DT = 0;
            int PD = 0;
            for (int i = 0; i < SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years.Count; i++)
            {
                for (int j = 0; j < SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years[i].Weeks.Count; j++)
                {
                    TD += SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].TD;
                    TT += SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].TT;
                    DD += SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].DD;
                    DT += SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].DT;
                    PD += SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].PD;
                }
            }
            //Вывод статистики
            textTabTSTDTNumLabel.Text = TD.ToString() + "/" + TT.ToString();
            textTabTSDDTNumLabel.Text = DD.ToString() + "/" + DT.ToString();
            textTabTSPDNumLabel.Text = PD.ToString();
            //Статистика за этот месяц
            List<int> weeks = ItemStatistics.WeeksInMonth(DateTime.Now);

            TD = 0;
            TT = 0;
            DD = 0;
            DT = 0;
            PD = 0;

            int yearIndex = -1;
            for (int i = 0; i < SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years.Count; i++)
            {
                if (SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years[i].Year == DateTime.Now.Year)
                {
                    yearIndex = i;
                    break;
                }
            }
            if (yearIndex != -1)
            {
                for (int i = 0; i < weeks.Count; i++)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; j++)
                    {
                        if (SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].Week == weeks[i])
                        {
                            TD += SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].TD;
                            TT += SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].TT;
                            DD += SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].DD;
                            DT += SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].DT;
                            PD += SharedData.WorkSpaces[textTabWSComboBox.SelectedIndex].Texts[textTabTextsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].PD;
                        }
                    }
                }
            }

            //Вывод статистики
            textTabTMSTDTNumLabel.Text = TD.ToString() + "/" + TT.ToString();
            textTabTMSDDTNumLabel.Text = DD.ToString() + "/" + DT.ToString();
            textTabTMSPDNumLabel.Text = PD.ToString();
        }

        private void presetTabWSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            presetTabPresetsComboBox.Items.Clear();
            for (int i = 0; i < SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets.Count; i++)
            {
                presetTabPresetsComboBox.Items.Add(SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[i].Name);
            }
        }

        private void presetTabPresetsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (presetTabPresetsComboBox.SelectedIndex == -1)
            {
                return;
            }

            //Общая статистика
            int TD = 0;
            int TT = 0;
            int DD = 0;
            int DT = 0;
            int PD = 0;
            for (int i = 0; i < SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years.Count; i++)
            {
                for (int j = 0; j < SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years[i].Weeks.Count; j++)
                {
                    TD += SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].TD;
                    TT += SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].TT;
                    DD += SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].DD;
                    DT += SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].DT;
                    PD += SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years[i].Weeks[j].PD;
                }
            }
            //Вывод статистики
            presetTabTSTDTNumLabel.Text = TD.ToString() + "/" + TT.ToString();
            presetTabTSDDTNumLabel.Text = DD.ToString() + "/" + DT.ToString();
            presetTabTSPDNumLabel.Text = PD.ToString();
            //Статистика за этот месяц
            List<int> weeks = ItemStatistics.WeeksInMonth(DateTime.Now);

            TD = 0;
            TT = 0;
            DD = 0;
            DT = 0;
            PD = 0;

            int yearIndex = -1;
            for (int i = 0; i < SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years.Count; i++)
            {
                if (SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years[i].Year == DateTime.Now.Year)
                {
                    yearIndex = i;
                    break;
                }
            }
            if (yearIndex != -1)
            {
                for (int i = 0; i < weeks.Count; i++)
                {
                    for (int j = 0; j < SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; j++)
                    {
                        if (SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].Week == weeks[i])
                        {
                            TD += SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].TD;
                            TT += SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].TT;
                            DD += SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].DD;
                            DT += SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].DT;
                            PD += SharedData.WorkSpaces[presetTabWSComboBox.SelectedIndex].Presets[presetTabPresetsComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[j].PD;
                        }
                    }
                }
            }

            //Вывод статистики
            presetTabTMSTDTNumLabel.Text = TD.ToString() + "/" + TT.ToString();
            presetTabTMSDDTNumLabel.Text = DD.ToString() + "/" + DT.ToString();
            presetTabTMSPDNumLabel.Text = PD.ToString();
        }

        private void chartWSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartKeywordComboBox.Items.Clear();
            for (int i = 0; i < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords.Count; i++)
            {
                chartKeywordComboBox.Items.Add(SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords[i].Name);
            }
            chartTemplateComboBox.Items.Clear();
            for (int i = 0; i < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates.Count; i++)
            {
                chartTemplateComboBox.Items.Add(SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates[i].Name);
            }
            chartTextComboBox.Items.Clear();
            for (int i = 0; i < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts.Count; i++)
            {
                chartTextComboBox.Items.Add(SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts[i].Name);
            }
            chartPresetComboBox.Items.Clear();
            for (int i = 0; i < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets.Count; i++)
            {
                chartPresetComboBox.Items.Add(SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets[i].Name);
            }
            //Заполнение нижних полей
            chartSettingsYearComboBox.Items.Clear();
            for (int i = 0; i < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years.Count; i++)
            {
                chartSettingsYearComboBox.Items.Add(SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years[i].Year.ToString());
            }
            if (chartSettingsYearComboBox.Items.Count > 0)
            {
                chartSettingsYearComboBox.SelectedIndex = 0;
            }
            chartSettingsMonthComboBox.Items.Clear();

            chartSettingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000262"));
            chartSettingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000263"));
            chartSettingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000264"));
            chartSettingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000265"));
            chartSettingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000266"));
            chartSettingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000267"));
            chartSettingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000268"));
            chartSettingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000269"));
            chartSettingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000270"));
            chartSettingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000271"));
            chartSettingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000272"));
            chartSettingsMonthComboBox.Items.Add(View.UILanguageResources.GetString("S0000273"));
        }

        private void chartKeywordComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chartKeywordComboBox.SelectedIndex != -1)
            {
                try
                {
                    chartTemplateComboBox.SelectedIndex = -1;
                    chartTextComboBox.SelectedIndex = -1;
                    chartPresetComboBox.SelectedIndex = -1;
                }
                catch (Exception)
                {
                }

                UpdateGraph(chartWSComboBox.SelectedIndex, 1, chartKeywordComboBox.SelectedIndex, chartSettingsTypeComboBox.SelectedIndex, int.Parse(chartSettingsYearComboBox.Items[chartSettingsYearComboBox.SelectedIndex].ToString()), chartSettingsMonthComboBox.SelectedIndex);
            }
        }

        private void chartTemplateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chartTemplateComboBox.SelectedIndex != -1)
            {
                try
                {
                    chartKeywordComboBox.SelectedIndex = -1;
                    chartTextComboBox.SelectedIndex = -1;
                    chartPresetComboBox.SelectedIndex = -1;
                }
                catch (Exception)
                {
                }

                UpdateGraph(chartWSComboBox.SelectedIndex, 2, chartTemplateComboBox.SelectedIndex, chartSettingsTypeComboBox.SelectedIndex, int.Parse(chartSettingsYearComboBox.Items[chartSettingsYearComboBox.SelectedIndex].ToString()), chartSettingsMonthComboBox.SelectedIndex);
            }
        }

        private void chartTextComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chartTextComboBox.SelectedIndex != -1)
            {
                try
                {
                    chartKeywordComboBox.SelectedIndex = -1;
                    chartTemplateComboBox.SelectedIndex = -1;
                    chartPresetComboBox.SelectedIndex = -1;
                }
                catch (Exception)
                {
                }

                UpdateGraph(chartWSComboBox.SelectedIndex, 3, chartTextComboBox.SelectedIndex, chartSettingsTypeComboBox.SelectedIndex, int.Parse(chartSettingsYearComboBox.Items[chartSettingsYearComboBox.SelectedIndex].ToString()), chartSettingsMonthComboBox.SelectedIndex);
            }
        }

        private void chartPresetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chartPresetComboBox.SelectedIndex != -1)
            {
                try
                {
                    chartKeywordComboBox.SelectedIndex = -1;
                    chartTemplateComboBox.SelectedIndex = -1;
                    chartTextComboBox.SelectedIndex = -1;
                }
                catch (Exception)
                {
                }

                UpdateGraph(chartWSComboBox.SelectedIndex, 4, chartPresetComboBox.SelectedIndex, chartSettingsTypeComboBox.SelectedIndex, int.Parse(chartSettingsYearComboBox.Items[chartSettingsYearComboBox.SelectedIndex].ToString()), chartSettingsMonthComboBox.SelectedIndex);
            }
        }

        private void chartSettingsYearComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                chartSettingsMonthComboBox.SelectedIndex = -1;
            }
            catch (Exception)
            {
            }

            UpdateGraph(chartWSComboBox.SelectedIndex, 0, 0, chartSettingsTypeComboBox.SelectedIndex, int.Parse(chartSettingsYearComboBox.Items[chartSettingsYearComboBox.SelectedIndex].ToString()), chartSettingsMonthComboBox.SelectedIndex);
        }

        /// <summary>
        /// Обновление графика
        /// </summary>
        /// <param name="PrimaryIndex">Индекс WS</param>
        /// <param name="GraphType">Тип: 0 - WS, 1 - keywords, 2 - templates, 3 - texts, 4 - presets</param>
        ///<param name="SecondaryIndex">Индекс второго параметра</param>
        /// <param name="Type">Тип: 0 - задания, 1 - дорвеи</param>
        /// <param name="Year">Год</param>
        /// <param name="Month">Месяц, -1 строить график за год</param>
        private void UpdateGraph(int PrimaryIndex, int GraphType, int SecondaryIndex, int Type, int Year, int Month)
        {
            //Построение графика
            GraphPane myPane = mainChart.GraphPane;

            myPane.CurveList.Clear();

            myPane.Title.Text = SharedData.WorkSpaces[PrimaryIndex].Name;
            myPane.XAxis.Title.Text = View.UILanguageResources.GetString("S0000259");
            myPane.YAxis.Title.Text = View.UILanguageResources.GetString("S0000260");

            //Поиск года
            int yearIndex = -1;
            for (int i = 0; i < SharedData.WorkSpaces[PrimaryIndex].Statistics.Years.Count; i++)
            {
                if (SharedData.WorkSpaces[PrimaryIndex].Statistics.Years[i].Year == Year)
                {
                    yearIndex = i;
                    break;
                }
            }
            if (yearIndex == -1)
            {
                mainChart.Invalidate();
                return;
            }

            //Поиск недель в месяце
            List<int> weeks = new List<int>();
            if (Month != -1)
            {
                weeks = ItemStatistics.WeeksInMonth(new DateTime(Year, Month + 1, 1));
            }


            PointPairList scheduledList = new PointPairList();
            PointPairList doneList = new PointPairList();


            try
            {
                switch (GraphType)
                {
                    //WS
                    case 0:
                        {
                            //За год
                            if (Month == -1)
                            {
                                //Запланированные на год вперед задания
                                for (int i = 0; i < 53; i++)
                                {
                                    int weekIndex = -1;
                                    //Поиск недели
                                    for (int k = 0; k < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; k++)
                                    {
                                        if (SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[k].Week == i)
                                        {
                                            weekIndex = k;
                                            break;
                                        }
                                    }

                                    if (weekIndex == -1)
                                    {
                                        scheduledList.Add(i, 0);
                                    }
                                    else
                                    {
                                        if (Type == 0)
                                        {
                                            scheduledList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT);
                                        }
                                        else
                                        {
                                            scheduledList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT);
                                        }
                                    }
                                }
                                //Выполненые задания
                                for (int i = 0; i < 53; i++)
                                {
                                    int weekIndex = -1;
                                    //Поиск недели
                                    for (int k = 0; k < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; k++)
                                    {
                                        if (SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[k].Week == i)
                                        {
                                            weekIndex = k;
                                            break;
                                        }
                                    }

                                    if (weekIndex == -1)
                                    {
                                        doneList.Add(i, 0);
                                    }
                                    else
                                    {
                                        if (Type == 0)
                                        {
                                            doneList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TD);
                                        }
                                        else
                                        {
                                            doneList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DD);
                                        }
                                    }
                                }
                            }
                            //За месяц
                            else
                            {
                                for (int i = 0; i < weeks.Count; i++)
                                {
                                    int weekIndex = -1;
                                    //Поиск недели
                                    for (int k = 0; k < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; k++)
                                    {
                                        if (SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[k].Week == weeks[i])
                                        {
                                            weekIndex = k;
                                            break;
                                        }
                                    }

                                    if (weekIndex != -1)
                                    {
                                        //Запланированные задания
                                        if (Type == 0)
                                        {
                                            scheduledList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT);
                                        }
                                        else
                                        {
                                            scheduledList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT);
                                        }
                                        //Выполненые задания
                                        if (Type == 0)
                                        {
                                            doneList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TD);
                                        }
                                        else
                                        {
                                            doneList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DD);
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    //keywords
                    case 1:
                        {
                            //За год
                            if (Month == -1)
                            {
                                //Запланированные на год вперед задания
                                for (int i = 0; i < 53; i++)
                                {
                                    int weekIndex = -1;
                                    //Поиск недели
                                    for (int k = 0; k < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords[chartKeywordComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; k++)
                                    {
                                        if (SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords[chartKeywordComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[k].Week == i)
                                        {
                                            weekIndex = k;
                                            break;
                                        }
                                    }

                                    if (weekIndex == -1)
                                    {
                                        scheduledList.Add(i, 0);
                                    }
                                    else
                                    {
                                        if (Type == 0)
                                        {
                                            scheduledList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords[chartKeywordComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT);
                                        }
                                        else
                                        {
                                            scheduledList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords[chartKeywordComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT);
                                        }
                                    }
                                }
                                //Выполненые задания
                                for (int i = 0; i < 53; i++)
                                {
                                    int weekIndex = -1;
                                    //Поиск недели
                                    for (int k = 0; k < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords[chartKeywordComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; k++)
                                    {
                                        if (SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords[chartKeywordComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[k].Week == i)
                                        {
                                            weekIndex = k;
                                            break;
                                        }
                                    }

                                    if (weekIndex == -1)
                                    {
                                        doneList.Add(i, 0);
                                    }
                                    else
                                    {
                                        if (Type == 0)
                                        {
                                            doneList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords[chartKeywordComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TD);
                                        }
                                        else
                                        {
                                            doneList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords[chartKeywordComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DD);
                                        }
                                    }
                                }
                            }
                            //За месяц
                            else
                            {
                                for (int i = 0; i < weeks.Count; i++)
                                {
                                    int weekIndex = -1;
                                    //Поиск недели
                                    for (int k = 0; k < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords[chartKeywordComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; k++)
                                    {
                                        if (SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords[chartKeywordComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[k].Week == weeks[i])
                                        {
                                            weekIndex = k;
                                            break;
                                        }
                                    }

                                    if (weekIndex != -1)
                                    {
                                        //Запланированные задания
                                        if (Type == 0)
                                        {
                                            scheduledList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords[chartKeywordComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT);
                                        }
                                        else
                                        {
                                            scheduledList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords[chartKeywordComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT);
                                        }
                                        //Выполненые задания
                                        if (Type == 0)
                                        {
                                            doneList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords[chartKeywordComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TD);
                                        }
                                        else
                                        {
                                            doneList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Keywords[chartKeywordComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DD);
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    //templates
                    case 2:
                        {
                            //За год
                            if (Month == -1)
                            {
                                //Запланированные на год вперед задания
                                for (int i = 0; i < 53; i++)
                                {
                                    int weekIndex = -1;
                                    //Поиск недели
                                    for (int k = 0; k < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates[chartTemplateComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; k++)
                                    {
                                        if (SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates[chartTemplateComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[k].Week == i)
                                        {
                                            weekIndex = k;
                                            break;
                                        }
                                    }

                                    if (weekIndex == -1)
                                    {
                                        scheduledList.Add(i, 0);
                                    }
                                    else
                                    {
                                        if (Type == 0)
                                        {
                                            scheduledList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates[chartTemplateComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT);
                                        }
                                        else
                                        {
                                            scheduledList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates[chartTemplateComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT);
                                        }
                                    }
                                }
                                //Выполненые задания
                                for (int i = 0; i < 53; i++)
                                {
                                    int weekIndex = -1;
                                    //Поиск недели
                                    for (int k = 0; k < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates[chartTemplateComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; k++)
                                    {
                                        if (SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates[chartTemplateComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[k].Week == i)
                                        {
                                            weekIndex = k;
                                            break;
                                        }
                                    }

                                    if (weekIndex == -1)
                                    {
                                        doneList.Add(i, 0);
                                    }
                                    else
                                    {
                                        if (Type == 0)
                                        {
                                            doneList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates[chartTemplateComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TD);
                                        }
                                        else
                                        {
                                            doneList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates[chartTemplateComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DD);
                                        }
                                    }
                                }
                            }
                            //За месяц
                            else
                            {
                                for (int i = 0; i < weeks.Count; i++)
                                {
                                    int weekIndex = -1;
                                    //Поиск недели
                                    for (int k = 0; k < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates[chartTemplateComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; k++)
                                    {
                                        if (SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates[chartTemplateComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[k].Week == weeks[i])
                                        {
                                            weekIndex = k;
                                            break;
                                        }
                                    }

                                    if (weekIndex != -1)
                                    {
                                        //Запланированные задания
                                        if (Type == 0)
                                        {
                                            scheduledList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates[chartTemplateComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT);
                                        }
                                        else
                                        {
                                            scheduledList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates[chartTemplateComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT);
                                        }
                                        //Выполненые задания
                                        if (Type == 0)
                                        {
                                            doneList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates[chartTemplateComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TD);
                                        }
                                        else
                                        {
                                            doneList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Templates[chartTemplateComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DD);
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    //texts
                    case 3:
                        {
                            //За год
                            if (Month == -1)
                            {
                                //Запланированные на год вперед задания
                                for (int i = 0; i < 53; i++)
                                {
                                    int weekIndex = -1;
                                    //Поиск недели
                                    for (int k = 0; k < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts[chartTextComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; k++)
                                    {
                                        if (SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts[chartTextComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[k].Week == i)
                                        {
                                            weekIndex = k;
                                            break;
                                        }
                                    }

                                    if (weekIndex == -1)
                                    {
                                        scheduledList.Add(i, 0);
                                    }
                                    else
                                    {
                                        if (Type == 0)
                                        {
                                            scheduledList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts[chartTextComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT);
                                        }
                                        else
                                        {
                                            scheduledList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts[chartTextComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT);
                                        }
                                    }
                                }
                                //Выполненые задания
                                for (int i = 0; i < 53; i++)
                                {
                                    int weekIndex = -1;
                                    //Поиск недели
                                    for (int k = 0; k < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts[chartTextComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; k++)
                                    {
                                        if (SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts[chartTextComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[k].Week == i)
                                        {
                                            weekIndex = k;
                                            break;
                                        }
                                    }

                                    if (weekIndex == -1)
                                    {
                                        doneList.Add(i, 0);
                                    }
                                    else
                                    {
                                        if (Type == 0)
                                        {
                                            doneList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts[chartTextComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TD);
                                        }
                                        else
                                        {
                                            doneList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts[chartTextComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DD);
                                        }
                                    }
                                }
                            }
                            //За месяц
                            else
                            {
                                for (int i = 0; i < weeks.Count; i++)
                                {
                                    int weekIndex = -1;
                                    //Поиск недели
                                    for (int k = 0; k < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts[chartTextComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; k++)
                                    {
                                        if (SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts[chartTextComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[k].Week == weeks[i])
                                        {
                                            weekIndex = k;
                                            break;
                                        }
                                    }

                                    if (weekIndex != -1)
                                    {
                                        //Запланированные задания
                                        if (Type == 0)
                                        {
                                            scheduledList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts[chartTextComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT);
                                        }
                                        else
                                        {
                                            scheduledList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts[chartTextComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT);
                                        }
                                        //Выполненые задания
                                        if (Type == 0)
                                        {
                                            doneList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts[chartTextComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TD);
                                        }
                                        else
                                        {
                                            doneList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Texts[chartTextComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DD);
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    //presets
                    case 4:
                        {
                            //За год
                            if (Month == -1)
                            {
                                //Запланированные на год вперед задания
                                for (int i = 0; i < 53; i++)
                                {
                                    int weekIndex = -1;
                                    //Поиск недели
                                    for (int k = 0; k < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets[chartPresetComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; k++)
                                    {
                                        if (SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets[chartPresetComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[k].Week == i)
                                        {
                                            weekIndex = k;
                                            break;
                                        }
                                    }

                                    if (weekIndex == -1)
                                    {
                                        scheduledList.Add(i, 0);
                                    }
                                    else
                                    {
                                        if (Type == 0)
                                        {
                                            scheduledList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets[chartPresetComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT);
                                        }
                                        else
                                        {
                                            scheduledList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets[chartPresetComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT);
                                        }
                                    }
                                }
                                //Выполненые задания
                                for (int i = 0; i < 53; i++)
                                {
                                    int weekIndex = -1;
                                    //Поиск недели
                                    for (int k = 0; k < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets[chartPresetComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; k++)
                                    {
                                        if (SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets[chartPresetComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[k].Week == i)
                                        {
                                            weekIndex = k;
                                            break;
                                        }
                                    }

                                    if (weekIndex == -1)
                                    {
                                        doneList.Add(i, 0);
                                    }
                                    else
                                    {
                                        if (Type == 0)
                                        {
                                            doneList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets[chartPresetComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TD);
                                        }
                                        else
                                        {
                                            doneList.Add(i, SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets[chartPresetComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DD);
                                        }
                                    }
                                }
                            }
                            //За месяц
                            else
                            {
                                for (int i = 0; i < weeks.Count; i++)
                                {
                                    int weekIndex = -1;
                                    //Поиск недели
                                    for (int k = 0; k < SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets[chartPresetComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks.Count; k++)
                                    {
                                        if (SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets[chartPresetComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[k].Week == weeks[i])
                                        {
                                            weekIndex = k;
                                            break;
                                        }
                                    }

                                    if (weekIndex != -1)
                                    {
                                        //Запланированные задания
                                        if (Type == 0)
                                        {
                                            scheduledList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets[chartPresetComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT);
                                        }
                                        else
                                        {
                                            scheduledList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets[chartPresetComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT);
                                        }
                                        //Выполненые задания
                                        if (Type == 0)
                                        {
                                            doneList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets[chartPresetComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TD);
                                        }
                                        else
                                        {
                                            doneList.Add(weeks[i], SharedData.WorkSpaces[chartWSComboBox.SelectedIndex].Presets[chartPresetComboBox.SelectedIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DD);
                                        }
                                    }
                                }
                            }
                            break;
                        }
                }
            }
            catch (Exception)
            {
            }

            LineItem doneCurve = myPane.AddCurve(View.UILanguageResources.GetString("S0000023"), doneList, Color.Blue, SymbolType.Circle);
            doneCurve.Line.Fill = new Fill(Color.White, Color.Green, 45F);
            doneCurve.Symbol.Fill = new Fill(Color.White);

            LineItem scheduledCurve = myPane.AddCurve(View.UILanguageResources.GetString("S0000261"), scheduledList, Color.Red, SymbolType.Circle);
            scheduledCurve.Line.Fill = new Fill(Color.White, Color.Red, 45F);
            scheduledCurve.Symbol.Fill = new Fill(Color.White);

            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);
            myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);

            mainChart.AxisChange();
            mainChart.Invalidate();
        }

        private void chartSettingsMonthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chartSettingsMonthComboBox.SelectedIndex == -1)
            {
                return;
            }
            UpdateGraph(chartWSComboBox.SelectedIndex, 0, 0, chartSettingsTypeComboBox.SelectedIndex, int.Parse(chartSettingsYearComboBox.Items[chartSettingsYearComboBox.SelectedIndex].ToString()), chartSettingsMonthComboBox.SelectedIndex);
        }

        private void tipsTimer_Tick(object sender, EventArgs e)
        {
            TipsTextBox.Text = View.UILanguageResources.GetString("S0001021");
        }
    }
}
