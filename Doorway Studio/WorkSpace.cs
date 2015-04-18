using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Doorway_Studio.Classes;
using Settings;

namespace Doorway_Studio
{
    class WorkSpace
    {
        public WorkSpace(int ID)
        {
            this.id = ID;
            this.tasks = new List<Task>();
            this.keywords = new List<KeyWord>();
            this.texts = new List<Text>();
            this.presets = new List<Preset>();
            this.templates = new List<Template>();

            this.statistics = new ItemStatistics();
        }
        public WorkSpace(int ID, string Name, string Comment) : this(ID)
        {
            this.Name = Name;
            this.comment = Comment;
        }

        //public Statistics

        #region Data
        private int id;
        private string comment;
        private List<Task> tasks;
        private List<KeyWord> keywords;
        private List<Text> texts;
        private List<Preset> presets;
        private List<Template> templates;

        private ItemStatistics statistics;
        #endregion

        #region Properties
        /// <summary>
        /// Задания
        /// </summary>
        public List<Task> Tasks
        {
            get
            {
                return tasks;
            }
            set
            {
                tasks = value;
            }
        }

        /// <summary>
        /// Кейворды
        /// </summary>
        public List<KeyWord> Keywords
        {
            get
            {
                return keywords;
            }
            set
            {
                keywords = value;
            }
        }

        /// <summary>
        /// Текст
        /// </summary>
        public List<Text> Texts
        {
            get
            {
                return texts;
            }
            set
            {
                texts = value;
            }
        }

        /// <summary>
        /// Presets
        /// </summary>
        public List<Preset> Presets
        {
            get
            {
                return presets;
            }
            set
            {
                presets = value;
            }
        }

        /// <summary>
        /// Templates
        /// </summary>
        public List<Template> Templates
        {
            get
            {
                return templates;
            }
            set
            {
                templates = value;
            }
        }

        /// <summary>
        /// ID of WorkSpace
        /// </summary>
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        /// <summary>
        /// Статистика
        /// </summary>
        public ItemStatistics Statistics
        {
            get
            {
                return statistics;
            }
            set
            {
                statistics = value;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Загрузка данных
        /// </summary>
        public void Load()
        {
            try
            {
                string directoryName = this.id.ToString();
                while (directoryName.Length < 7)
                {
                    directoryName = "0" + directoryName;
                }

                //Загрузка info.txt
                if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\info.txt"))
                {
                    return;
                }
                string[] data = File.ReadAllLines(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\info.txt", Encoding.UTF8);
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i].StartsWith("Name="))
                    {
                        this.Name = data[i].Substring(5);
                    }
                    else if (data[i].StartsWith("Comm="))
                    {
                        this.comment = data[i].Substring(5);
                    }
                }

                //Загрузка кейвордов
                LoadKeywords(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Keywords");
                //Загрузка текста
                LoadText(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Text");
                //Загрузка пресетов
                LoadPresets(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Presets");
                //Загрузка шаблонов
                LoadTemplates(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates");
                //Загрузка заданий
                LoadTasks(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Tasks");
                //Загрузка статистики
                this.statistics = LoadStatistics(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\statistics.txt");
            }
            catch (Exception)
            {
            }
        }

        private void LoadKeywords(string Path)
        {
            if (Directory.Exists(Path))
            {
                string[] data = Directory.GetDirectories(Path, "*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < data.Length; i++)
                {
                    try
                    {
                        int keywordID = int.Parse(data[i].Substring(data[i].LastIndexOf("\\") + 1));
                        string keywordName = string.Empty;
                        string keywordComment = string.Empty;
                        string[] keywordsData = new string[0];

                        string[] info = File.ReadAllLines(data[i] + "\\info.txt", Encoding.UTF8);

                        for (int j = 0; j < info.Length; j++)
                        {
                            if (info[j].StartsWith("Name="))
                            {
                                keywordName = info[j].Substring(5);
                            }
                            else if (info[j].StartsWith("Comm="))
                            {
                                keywordComment = info[j].Substring(5);
                            }
                        }

                        try
                        {
                            keywordsData = File.ReadAllLines(data[i] + "\\keywords.txt", Encoding.UTF8);
                        }
                        catch (Exception)
                        {
                            keywordsData = new string[0];
                        }
                        this.keywords.Add(new KeyWord(keywordID, keywordName, keywordComment));
                        this.keywords[this.keywords.Count - 1].Items = keywordsData;
                        //Загрузка статистики
                        this.keywords[this.keywords.Count - 1].Statistics = LoadStatistics(data[i] + "\\statistics.txt");
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void LoadText(string Path)
        {
            if (Directory.Exists(Path))
            {
                string[] data = Directory.GetDirectories(Path, "*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < data.Length; i++)
                {
                    try
                    {
                        int textID = int.Parse(data[i].Substring(data[i].LastIndexOf("\\") + 1));
                        string textName = string.Empty;
                        string textComment = string.Empty;
                        string textData = string.Empty;

                        string[] info = File.ReadAllLines(data[i] + "\\info.txt", Encoding.UTF8);

                        for (int j = 0; j < info.Length; j++)
                        {
                            if (info[j].StartsWith("Name="))
                            {
                                textName = info[j].Substring(5);
                            }
                            else if (info[j].StartsWith("Comm="))
                            {
                                textComment = info[j].Substring(5);
                            }
                        }

                        try
                        {
                            textData = File.ReadAllText(data[i] + "\\text.txt", Encoding.UTF8);
                        }
                        catch (Exception)
                        {
                            textData = string.Empty;
                        }
                        this.texts.Add(new Text(textID, textName, textComment));
                        this.texts[texts.Count - 1].Texts = textData;

                        //Загрузка статистики
                        this.texts[texts.Count - 1].Statistics = LoadStatistics(data[i] + "\\statistics.txt");
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void LoadPresets(string Path)
        {
            if (Directory.Exists(Path))
            {
                string[] data = Directory.GetDirectories(Path, "*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < data.Length; i++)
                {
                    try
                    {
                        int presetID = int.Parse(data[i].Substring(data[i].LastIndexOf("\\") + 1));
                        string presetName = string.Empty;
                        string presetComment = string.Empty;
                        int presetTemplateID = 0;
                        int presetTextID = 0;
                        
                        string[] info = File.ReadAllLines(data[i] + "\\info.txt", Encoding.UTF8);

                        for (int j = 0; j < info.Length; j++)
                        {
                            if (info[j].StartsWith("Name="))
                            {
                                presetName = info[j].Substring(5);
                            }
                            else if (info[j].StartsWith("Comm="))
                            {
                                presetComment = info[j].Substring(5);
                            }
                            else if (info[j].StartsWith("TemplateID="))
                            {
                                presetTemplateID = int.Parse(info[j].Substring(11));
                            }
                            else if (info[j].StartsWith("TextID="))
                            {
                                presetTextID = int.Parse(info[j].Substring(7));
                            }
                        }
                        this.presets.Add(new Preset(presetID, presetName, presetComment));

                        this.presets[this.presets.Count - 1].TemplateID = presetTemplateID;
                        this.presets[this.presets.Count - 1].TextID = presetTextID;

                        try
                        {
                            this.presets[this.presets.Count - 1].Settings = WorkSpace.LoadPresetSettings(data[i] + "\\settings.txt");
                        }
                        catch (Exception)
                        {
                            this.presets[this.presets.Count - 1].Settings = new PresetSettings();
                        }
                        //Загрузка статистики
                        this.presets[presets.Count - 1].Statistics = LoadStatistics(data[i] + "\\statistics.txt");

                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public static PresetSettings LoadPresetSettings(string Path)
        {
            return PresetSettings.Load(Path);
        }

        private void LoadTemplates(string Path)
        {
            if (Directory.Exists(Path))
            {
                string[] data = Directory.GetDirectories(Path, "*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < data.Length; i++)
                {
                    try
                    {
                        int templateID = int.Parse(data[i].Substring(data[i].LastIndexOf("\\") + 1));
                        string templateName = string.Empty;
                        string templateComment = string.Empty;
                        int templateEncodingType = 0;

                        string[] info = File.ReadAllLines(data[i] + "\\info.txt", Encoding.UTF8);

                        for (int j = 0; j < info.Length; j++)
                        {
                            if (info[j].StartsWith("Name="))
                            {
                                templateName = info[j].Substring(5);
                            }
                            else if (info[j].StartsWith("Comm="))
                            {
                                templateComment = info[j].Substring(5);
                            }
                            else if (info[j].StartsWith("Encoding="))
                            {
                                templateEncodingType = int.Parse(info[j].Substring(9));
                            }
                        }
                        this.templates.Add(new Template(templateID, templateName, templateComment, templateEncodingType));
                        info = File.ReadAllLines(data[i] + "\\Files\\info.txt", Encoding.UTF8);

                        for (int j = 0; j < info.Length; j++)
                        {
                            if (info[j].StartsWith("index="))
                            {
                                if (templateEncodingType == 0)
                                {
                                    this.templates[this.templates.Count - 1].Index = File.ReadAllText(data[i] + "\\Files\\" + info[j].Substring(6), Encoding.Default);
                                }
                                else
                                {
                                    this.templates[this.templates.Count - 1].Index = File.ReadAllText(data[i] + "\\Files\\" + info[j].Substring(6), Encoding.UTF8);
                                }
                            }
                            else if (info[j].StartsWith("category="))
                            {
                                this.templates[this.templates.Count - 1].Categories.Add(new TemplatePage(data[i] + "\\Files\\" + info[j].Substring(9),
                                                                                                         templateEncodingType == 0 ? Encoding.Default : Encoding.UTF8));
                            }
                            else if (info[j].StartsWith("page="))
                            {
                                this.templates[this.templates.Count - 1].Pages.Add(new TemplatePage(data[i] + "\\Files\\" + info[j].Substring(5),
                                                                                                    templateEncodingType == 0 ? Encoding.Default : Encoding.UTF8));
                            }
                            else if (info[j].StartsWith("static="))
                            {
                                this.templates[this.templates.Count - 1].StaticPages.Add(new TemplatePage(data[i] + "\\Files\\" + info[j].Substring(7),
                                                                                                    templateEncodingType == 0 ? Encoding.Default : Encoding.UTF8));
                            }
                            else if (info[j].StartsWith("custom="))
                            {
                                string keywords = info[j].Contains("|") ? info[j].Substring(info[j].IndexOf("|") + 1) : string.Empty;
                                string name = info[j].Contains("|") ? info[j].Substring(7, info[j].IndexOf("|") - 7) : info[j].Substring(7);

                                if (!string.IsNullOrEmpty(name))
                                {
                                    this.templates[this.templates.Count - 1].CustomPages.Add(new TemplatePage(data[i] + "\\Files\\" + name, templateEncodingType == 0 ? Encoding.Default : Encoding.UTF8)
                                                                                                 {
                                                                                                     CustomName = name,
                                                                                                     CustomKeywords = keywords.Split(new char[] {'|'})
                                                                                                 });
                                }
                            }
                            else if (info[j].StartsWith("map="))
                            {
                                this.templates[this.templates.Count - 1].Map.Add(new TemplatePage(data[i] + "\\Files\\" + info[j].Substring(4), templateEncodingType == 0 ? Encoding.Default : Encoding.UTF8));
                            }
                            else if (info[j].StartsWith("image="))
                            {
                                if (!info[j].Substring(6).StartsWith("http"))
                                {
                                    this.templates[this.templates.Count - 1].Images.Add(data[i] + "\\Files\\" + info[j].Substring(6));
                                }
                                else
                                {
                                    this.templates[this.templates.Count - 1].Images.Add(info[j].Substring(6));
                                }
                            }
                            else if (info[j].StartsWith("file="))
                            {
                                if (!info[j].Substring(5).StartsWith(data[i] + "\\Files\\"))
                                {
                                    this.templates[this.templates.Count - 1].Files.Add(data[i] + "\\Files\\" + info[j].Substring(5));
                                }
                                else
                                {
                                    this.templates[this.templates.Count - 1].Files.Add(info[j].Substring(5));
                                }
                            }
                        }

                        //Загрузка статистики
                        this.templates[templates.Count - 1].Statistics = LoadStatistics(data[i] + "\\statistics.txt");

                        // Normalize pages percentage
                        NormalizeTemplatePagesPercentage(i);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Normalizes usage percentage for template
        /// </summary>
        /// <param name="TemplateIndex"></param>
        private void NormalizeTemplatePagesPercentage(int TemplateIndex)
        {
            if (this.Templates[TemplateIndex].Pages.Count == 0)
            {
                return;
            }

            int overallPercentage = 0;
            int iteratorCount = 0;

            while (iteratorCount <= 5 && (overallPercentage = this.Templates[TemplateIndex].Pages.Sum(p => p.UsagePercent)) != 100)
            {
                int percentageDifferencePerPage = (int) ((100 - overallPercentage)/this.Templates[TemplateIndex].Pages.Count);

                if (percentageDifferencePerPage == 0)
                {
                    return;
                }

                for (int i = 0; i < this.Templates[TemplateIndex].Pages.Count; i++)
                {
                    this.Templates[TemplateIndex].Pages[i].UsagePercent += percentageDifferencePerPage;

                    if (this.Templates[TemplateIndex].Pages[i].UsagePercent < 0)
                    {
                        this.Templates[TemplateIndex].Pages[i].UsagePercent = -this.Templates[TemplateIndex].Pages[i].UsagePercent;
                    }
                }

                iteratorCount++;
            }
        }

        /// <summary>
        /// Загрузка заданий
        /// </summary>
        /// <param name="Path"></param>
        private void LoadTasks(string Path)
        {
            if (Directory.Exists(Path))
            {
                string[] data = Directory.GetDirectories(Path, "*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < data.Length; i++)
                {
                    try
                    {
                        int taskID = int.Parse(data[i].Substring(data[i].LastIndexOf("\\") + 1));
                        string taskName = string.Empty;
                        string taskComment = string.Empty;
                        int taskPresetID = 0;
                        int taskTemplateID = 0;
                        int taskTextID = 0;
                        DateTime taskStartTime = new DateTime();
                        int taskStatus = 0;
                        DateTime taskEndTime = new DateTime();

                        string taskLog = string.Empty;
                        List<string> taskDoorwaysLog = new List<string>();
                        List<string> taskDoorwaysSpam = new List<string>();

                        string[] info = File.ReadAllLines(data[i] + "\\info.txt", Encoding.UTF8);

                        for (int j = 0; j < info.Length; j++)
                        {
                            if (info[j].StartsWith("Name="))
                            {
                                taskName = info[j].Substring(5);
                            }
                            else if (info[j].StartsWith("Comm="))
                            {
                                taskComment = info[j].Substring(5);
                            }
                            else if (info[j].StartsWith("PresetID="))
                            {
                                taskPresetID = int.Parse(info[j].Substring(9));
                            }
                            else if (info[j].StartsWith("TemplateID="))
                            {
                                taskTemplateID = int.Parse(info[j].Substring(11));
                            }
                            else if (info[j].StartsWith("TextID="))
                            {
                                taskTextID = int.Parse(info[j].Substring(7));
                            }
                            else if (info[j].StartsWith("Status="))
                            {
                                taskStatus = int.Parse(info[j].Substring(7));
                            }
                            else if (info[j].StartsWith("StartTime="))
                            {
                                taskStartTime = DateTime.Parse(info[j].Substring(10));
                            }
                            else if (info[j].StartsWith("EndTime="))
                            {
                                try
                                {
                                    taskEndTime = DateTime.Parse(info[j].Substring(8));
                                }
                                catch (Exception)
                                {
                                    taskEndTime = new DateTime();
                                }
                            }
                            else if (info[j].StartsWith("Log="))
                            {
                                taskDoorwaysLog.Add(info[j].Substring(4).Replace("¥", "\r\n"));
                            }
                            else if (info[j].StartsWith("Spam="))
                            {
                                taskDoorwaysSpam.Add(info[j].Substring(5).Replace("¥", "\r\n"));
                            }
                            else if (info[j].StartsWith("TaskLog="))
                            {
                                taskLog = info[j].Substring(8).Replace("¥", "\r\n");
                            }
                        }
                        //Удаление старых и не нужных заданий
                        if (taskStartTime.AddDays(MainSettings.DeleteFinishedTasksAfterDays) < (DateTime.Now) && taskStatus == 5)
                        {
                            Directory.Delete(data[i], true);
                        }
                        else
                        {
                            this.tasks.Add(new Task(taskID, taskName, taskComment));

                            this.tasks[this.tasks.Count - 1].WSIndex = WorkSpace.GetWSIndex(this.id);

                            this.tasks[this.tasks.Count - 1].PresetID = taskPresetID;
                            this.tasks[this.tasks.Count - 1].TemplateID = taskTemplateID;
                            this.tasks[this.tasks.Count - 1].TextID = taskTextID;

                            this.tasks[this.tasks.Count - 1].Status = taskStatus;
                            this.tasks[this.tasks.Count - 1].StartTime = taskStartTime;
                            this.tasks[this.tasks.Count - 1].EndTime = taskEndTime;

                            this.tasks[this.tasks.Count - 1].Log = new StringBuilder(taskLog);

                            try
                            {
                                this.tasks[this.tasks.Count - 1].Settings = WorkSpace.LoadPresetSettings(data[i] + "\\settings.txt");
                                for (int k = 0; k < taskDoorwaysLog.Count; k++)
                                {
                                    if (this.tasks[this.tasks.Count - 1].Settings.GeneralCreateDoorways >= k)
                                    {
                                        this.tasks[this.tasks.Count - 1].Doorways[k].Log = new StringBuilder(taskDoorwaysLog[k]);
                                    }
                                }
                                for (int k = 0; k < taskDoorwaysSpam.Count; k++)
                                {
                                    if (this.tasks[this.tasks.Count - 1].Settings.GeneralCreateDoorways >= k)
                                    {
                                        this.tasks[this.tasks.Count - 1].Doorways[k].Spam = new StringBuilder(taskDoorwaysSpam[k]);
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                this.tasks[this.tasks.Count - 1].Settings = new PresetSettings();
                            }
                        }

                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private ItemStatistics LoadStatistics(string FilePath)
        {
            ItemStatistics currentStatistics = new ItemStatistics();
            if (!File.Exists(FilePath))
            {
                return currentStatistics;
            }
            try
            {
                string[] fileData = File.ReadAllLines(FilePath, Encoding.UTF8);
                for (int i = 0; i < fileData.Length; i++)
                {
                    try
                    {
                        int year = int.Parse(fileData[i].Substring(0, 4));
                        int week = int.Parse(fileData[i].Substring(fileData[i].IndexOf("-") + 1, fileData[i].IndexOf("=") - fileData[i].IndexOf("-") - 1));
                        int[] statistics = new int[5];
                        try
                        {
                            string[] tempStrings = fileData[i].Substring(fileData[i].IndexOf("=") + 1).Split('|');
                            for (int k = 0; k < 5; k++)
                            {
                                if (tempStrings[k] == string.Empty)
                                {
                                    statistics[k] = 0;
                                }
                                else
                                {
                                    statistics[k] = int.Parse(tempStrings[k]);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        //Добавление
                        int yearIndex = -1;
                        int weekIndex = -1;

                        if (currentStatistics.Years.Count == 0)
                        {
                            //Добавление года
                            currentStatistics.Years.Add(new ItemStatistics.StatisticsYears(year));
                            yearIndex = 0;
                            //Добавление недели
                            currentStatistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(week));
                            weekIndex = 0;
                        }
                        else
                        {
                            //Поиск года
                            for (int j = 0; j < currentStatistics.Years.Count; j++)
                            {
                                if (currentStatistics.Years[j].Year == year)
                                {
                                    yearIndex = j;
                                    break;
                                }
                            }
                            //Добавление года
                            if (yearIndex == -1)
                            {
                                currentStatistics.Years.Add(new ItemStatistics.StatisticsYears(year));
                                yearIndex = currentStatistics.Years.Count - 1;
                            }
                            //Поиск недели
                            if (currentStatistics.Years[yearIndex].Weeks.Count == 0)
                            {
                                //Добавление недели
                                currentStatistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(week));
                                weekIndex = 0;
                            }
                            else
                            {
                                //Поиск недели
                                for (int j = 0; j < currentStatistics.Years[yearIndex].Weeks.Count; j++)
                                {
                                    if (currentStatistics.Years[yearIndex].Weeks[j].Week == week)
                                    {
                                        weekIndex = j;
                                        break;
                                    }
                                }
                                //Добавление недели
                                if (weekIndex == -1)
                                {
                                    currentStatistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(week));
                                    weekIndex = currentStatistics.Years[yearIndex].Weeks.Count - 1;
                                }
                            }
                        }

                        //Добавление статистики
                        currentStatistics.Years[yearIndex].Weeks[weekIndex].TD = statistics[0];
                        currentStatistics.Years[yearIndex].Weeks[weekIndex].TT = statistics[1];
                        currentStatistics.Years[yearIndex].Weeks[weekIndex].DD = statistics[2];
                        currentStatistics.Years[yearIndex].Weeks[weekIndex].DT = statistics[3];
                        currentStatistics.Years[yearIndex].Weeks[weekIndex].PD = statistics[4];
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }

            return currentStatistics;
        }

        /// <summary>
        /// Сохранение данных
        /// </summary>
        public void Save()
        {
            try
            {
                string directoryName = this.id.ToString();
                while (directoryName.Length < 7)
                {
                    directoryName = "0" + directoryName;
                }
                //Создание папок
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Data"))
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Data");
                    File.SetAttributes(System.Windows.Forms.Application.StartupPath + "\\Data", FileAttributes.Normal);
                }
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName))
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName);
                    File.SetAttributes(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName, FileAttributes.Normal);
                }
                //Запись файлов
                //info.txt
                StringBuilder infoData = new StringBuilder(200);
                infoData.Append("Name=" + this.Name + "\r\n");
                infoData.Append("Comm=" + this.comment);
                File.WriteAllText(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\info.txt", infoData.ToString(), Encoding.UTF8);

                //Сохранение кейвордов
                SaveKeywords(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Keywords");

                //Сохранение текста
                SaveText(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Text");

                //Сохранение пресетов
                SavePresets(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Presets");

                //Сохранение шаблонов
                SaveTemplates(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates");

                //Сохранение заданий
                SaveTasks(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Tasks");

                //Сохранение статистики
                SaveStatistics(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\statistics.txt", this.statistics);
            }
            catch (Exception)
            {
            }
        }

        private void SaveKeywords(string Path)
        {
            //Создание папок
            Directory.CreateDirectory(Path);
            File.SetAttributes(Path, FileAttributes.Normal);
            for (int i = 0; i < this.keywords.Count; i++)
            {
                try
                {
                    string keywordsDirectoryName = this.Keywords[i].ID.ToString();
                    while (keywordsDirectoryName.Length < 7)
                    {
                        keywordsDirectoryName = "0" + keywordsDirectoryName;
                    }
                    Directory.CreateDirectory(Path + "\\" + keywordsDirectoryName);
                    File.SetAttributes(Path + "\\" + keywordsDirectoryName, FileAttributes.Normal);
                    //Запись файлов
                    StringBuilder infoData = new StringBuilder(200);
                    infoData.Append("Name=" + this.keywords[i].Name + "\r\n");
                    infoData.Append("Comm=" + this.keywords[i].Comment);

                    File.WriteAllText(Path + "\\" + keywordsDirectoryName + "\\info.txt", infoData.ToString(), Encoding.UTF8);

                    File.WriteAllLines(Path + "\\" + keywordsDirectoryName + "\\keywords.txt", this.keywords[i].Items, Encoding.UTF8);
                    //Запись статистики
                    SaveStatistics(Path + "\\" + keywordsDirectoryName + "\\statistics.txt", this.keywords[i].Statistics);
                }
                catch (Exception)
                {
                }
            }
        }

        private void SavePresets(string Path)
        {
            //Создание папок
            Directory.CreateDirectory(Path);
            File.SetAttributes(Path, FileAttributes.Normal);
            for (int i = 0; i < this.presets.Count; i++)
            {
                try
                {
                    string presetsDirectoryName = this.presets[i].ID.ToString();
                    while (presetsDirectoryName.Length < 7)
                    {
                        presetsDirectoryName = "0" + presetsDirectoryName;
                    }
                    Directory.CreateDirectory(Path + "\\" + presetsDirectoryName);
                    File.SetAttributes(Path + "\\" + presetsDirectoryName, FileAttributes.Normal);
                    //Запись файлов
                    StringBuilder infoData = new StringBuilder(200);
                    infoData.Append("Name=" + this.presets[i].Name + "\r\n");
                    infoData.Append("Comm=" + this.presets[i].Comment + "\r\n");
                    infoData.Append("TemplateID=" + this.presets[i].TemplateID.ToString() + "\r\n");
                    infoData.Append("TextID=" + this.presets[i].TextID.ToString() + "\r\n");

                    if (File.Exists(Path + "\\" + presetsDirectoryName + "\\info.txt"))
                    {
                        File.SetAttributes(Path + "\\" + presetsDirectoryName + "\\info.txt", FileAttributes.Normal);
                    }
                    File.WriteAllText(Path + "\\" + presetsDirectoryName + "\\info.txt", infoData.ToString(), Encoding.UTF8);
                    File.SetAttributes(Path + "\\" + presetsDirectoryName + "\\info.txt", FileAttributes.Normal);

                    if (File.Exists(Path + "\\" + presetsDirectoryName + "\\settings.txt"))
                    {
                        File.SetAttributes(Path + "\\" + presetsDirectoryName + "\\settings.txt", FileAttributes.Normal);
                    }
                    File.WriteAllText(Path + "\\" + presetsDirectoryName + "\\settings.txt", this.presets[i].Settings.ToString(), Encoding.UTF8);
                    File.SetAttributes(Path + "\\" + presetsDirectoryName + "\\settings.txt", FileAttributes.Normal);

                    //Запись статистики
                    SaveStatistics(Path + "\\" + presetsDirectoryName + "\\statistics.txt", this.presets[i].Statistics);
                }
                catch (Exception)
                {
                }
            }
        }

        private void SaveText(string Path)
        {
            //Создание папок
            Directory.CreateDirectory(Path);
            File.SetAttributes(Path, FileAttributes.Normal);
            for (int i = 0; i < this.texts.Count; i++)
            {
                try
                {
                    string textsDirectoryName = this.Texts[i].ID.ToString();
                    while (textsDirectoryName.Length < 7)
                    {
                        textsDirectoryName = "0" + textsDirectoryName;
                    }
                    Directory.CreateDirectory(Path + "\\" + textsDirectoryName);
                    File.SetAttributes(Path + "\\" + textsDirectoryName, FileAttributes.Normal);
                    //Запись файлов
                    StringBuilder infoData = new StringBuilder(200);
                    infoData.Append("Name=" + this.texts[i].Name + "\r\n");
                    infoData.Append("Comm=" + this.texts[i].Comment);

                    File.WriteAllText(Path + "\\" + textsDirectoryName + "\\info.txt", infoData.ToString(), Encoding.UTF8);

                    File.WriteAllText(Path + "\\" + textsDirectoryName + "\\text.txt", this.texts[i].Texts, Encoding.UTF8);

                    //Запись статистики
                    SaveStatistics(Path + "\\" + textsDirectoryName + "\\statistics.txt", this.texts[i].Statistics);
                }
                catch (Exception)
                {
                }
            }
        }

        private void SaveTemplates(string Path)
        {
            Directory.CreateDirectory(Path);
            File.SetAttributes(Path, FileAttributes.Normal);
            for (int i = 0; i < this.templates.Count; i++)
            {
                try
                {
                    string templateDirectoryName = this.templates[i].ID.ToString();
                    while (templateDirectoryName.Length < 7)
                    {
                        templateDirectoryName = "0" + templateDirectoryName;
                    }
                    if (!Directory.Exists(Path + "\\" + templateDirectoryName))
                    {
                        Directory.CreateDirectory(Path + "\\" + templateDirectoryName);
                        File.SetAttributes(Path + "\\" + templateDirectoryName, FileAttributes.Normal);
                    }
                    if (!Directory.Exists(Path + "\\" + templateDirectoryName + "\\Files"))
                    {
                        Directory.CreateDirectory(Path + "\\" + templateDirectoryName + "\\Files");
                        File.SetAttributes(Path + "\\" + templateDirectoryName + "\\Files", FileAttributes.Normal);
                    }
                    //Запись файлов
                    StringBuilder infoData = new StringBuilder(200);
                    infoData.Append("Name=" + this.templates[i].Name + "\r\n");
                    infoData.Append("Comm=" + this.templates[i].Comment + "\r\n");
                    infoData.Append("Encoding=" + this.templates[i].EncodingType.ToString());

                    File.WriteAllText(Path + "\\" + templateDirectoryName + "\\info.txt", infoData.ToString(), Encoding.UTF8);

                    infoData = new StringBuilder("index=index.html\r\n", 1000);

                    if (this.templates[i].EncodingType == 0)
                    {
                        File.WriteAllText(Path + "\\" + templateDirectoryName + "\\Files\\index.html", this.templates[i].Index, Encoding.Default);
                    }
                    else
                    {
                        File.WriteAllText(Path + "\\" + templateDirectoryName + "\\Files\\index.html", this.templates[i].Index, Encoding.UTF8);
                    }
                    for (int j = 0; j < this.templates[i].Categories.Count; j++)
                    {
                        string tempString = (j + 1).ToString();
                        while (tempString.Length < 3)
                        {
                            tempString = "0" + tempString;
                        }
                        infoData.Append("category=category-" + tempString + ".html\r\n");
                        if (this.templates[i].EncodingType == 0)
                        {
                            File.WriteAllText(Path + "\\" + templateDirectoryName + "\\Files\\category-" + tempString + ".html", this.templates[i].Categories[j].Content, Encoding.Default);
                        }
                        else
                        {
                            File.WriteAllText(Path + "\\" + templateDirectoryName + "\\Files\\category-" + tempString + ".html", this.templates[i].Categories[j].Content, Encoding.UTF8);
                        }
                    }
                    if (this.templates[i].Pages.Count == 0)
                    {
                        this.templates[i].Pages.Add(new TemplatePage(this.templates[i].Index));
                    }
                    for (int j = 0; j < this.templates[i].Pages.Count; j++)
                    {
                        string tempString = (j + 1).ToString();
                        while (tempString.Length < 3)
                        {
                            tempString = "0" + tempString;
                        }

                        infoData.Append("page=page-" + tempString + ".html|" + this.templates[i].Pages[j].UsagePercent.ToString() + "\r\n");
                        File.WriteAllText(Path + "\\" + templateDirectoryName + "\\Files\\page-" + tempString + ".html", this.templates[i].Pages[j].Content,
                                          this.templates[i].EncodingType == 0 ? Encoding.Default : Encoding.UTF8);
                    }
                    for (int j = 0; j < this.templates[i].Map.Count; j++)
                    {
                        string tempString = (j + 1).ToString();
                        while (tempString.Length < 3)
                        {
                            tempString = "0" + tempString;
                        }
                        infoData.Append("map=map-" + tempString + ".html\r\n");
                        if (this.templates[i].EncodingType == 0)
                        {
                            File.WriteAllText(Path + "\\" + templateDirectoryName + "\\Files\\map-" + tempString + ".html", this.templates[i].Map[j].Content, Encoding.Default);
                        }
                        else
                        {
                            File.WriteAllText(Path + "\\" + templateDirectoryName + "\\Files\\map-" + tempString + ".html", this.templates[i].Map[j].Content, Encoding.UTF8);
                        }
                    }
                    for (int j = 0; j < this.templates[i].StaticPages.Count; j++)
                    {
                        string tempString = (j + 1).ToString();
                        while (tempString.Length < 3)
                        {
                            tempString = "0" + tempString;
                        }
                        infoData.Append("static=static-" + tempString + ".html\r\n");
                        if (this.templates[i].EncodingType == 0)
                        {
                            File.WriteAllText(Path + "\\" + templateDirectoryName + "\\Files\\static-" + tempString + ".html", this.templates[i].StaticPages[j].Content, Encoding.Default);
                        }
                        else
                        {
                            File.WriteAllText(Path + "\\" + templateDirectoryName + "\\Files\\static-" + tempString + ".html", this.templates[i].StaticPages[j].Content, Encoding.UTF8);
                        }
                    }

                    for (int j = 0; j < this.templates[i].CustomPages.Count; j++)
                    {
                        string name = this.templates[i].CustomPages[j].CustomName;
                        string keywords = string.Join("|", this.templates[i].CustomPages[j].CustomKeywords);

                        if (string.IsNullOrEmpty(keywords))
                        {
                            infoData.Append("custom=" + name + "\r\n");
                        }
                        else
                        {
                            infoData.Append("custom=" + name + "|" + keywords + "\r\n");
                        }

                        File.WriteAllText(Path + "\\" + templateDirectoryName + "\\Files\\" + name, this.templates[i].CustomPages[j].Content,
                                          this.templates[i].EncodingType == 0 ? Encoding.Default : Encoding.UTF8);
                    }

                    for (int j = 0; j < this.templates[i].Images.Count; j++)
                    {
                        string value = this.templates[i].Images[j];
                        if(value.Contains("\\Files\\"))
                        {
                            value = value.Substring(this.templates[i].Images[j].IndexOf("\\Files\\") + 7);
                        }

                        infoData.Append("image=" + value + "\r\n");
                    }
                    for (int j = 0; j < this.templates[i].Files.Count; j++)
                    {
                        string value = this.templates[i].Files[j];
                        if (value.Contains("\\Files\\"))
                        {
                            value = value.Substring(this.templates[i].Files[j].IndexOf("\\Files\\") + 7);
                        }

                        infoData.Append("file=" + value + "\r\n");
                    }

                    File.WriteAllText(Path + "\\" + templateDirectoryName + "\\Files\\info.txt", infoData.ToString(), Encoding.UTF8);

                    //Запись статистики
                    SaveStatistics(Path + "\\" + templateDirectoryName + "\\statistics.txt", this.templates[i].Statistics);
                }
                catch (Exception)
                {
                }
            }
        }

        private void SaveTasks(string Path)
        {
            //Создание папок
            Directory.CreateDirectory(Path);
            File.SetAttributes(Path, FileAttributes.Normal);
            for (int i = 0; i < this.tasks.Count; i++)
            {
                try
                {
                    string tasksDirectoryName = this.tasks[i].ID.ToString();
                    while (tasksDirectoryName.Length < 7)
                    {
                        tasksDirectoryName = "0" + tasksDirectoryName;
                    }
                    Directory.CreateDirectory(Path + "\\" + tasksDirectoryName);
                    File.SetAttributes(Path + "\\" + tasksDirectoryName, FileAttributes.Normal);
                    //Запись файлов
                    StringBuilder infoData = new StringBuilder(10000);
                    infoData.Append("Name=" + this.tasks[i].Name + "\r\n");
                    infoData.Append("Comm=" + this.tasks[i].Comment + "\r\n");
                    infoData.Append("PresetID=" + this.tasks[i].PresetID.ToString() + "\r\n");
                    infoData.Append("TemplateID=" + this.tasks[i].TemplateID.ToString() + "\r\n");
                    infoData.Append("TextID=" + this.tasks[i].TextID.ToString() + "\r\n");
                    infoData.Append("Status=" + this.tasks[i].Status.ToString() + "\r\n");
                    infoData.Append("StartTime=" + this.tasks[i].StartTime.ToString() + "\r\n");
                    if (this.tasks[i].Status == 5)
                    {
                        infoData.Append("EndTime=" + this.tasks[i].EndTime.ToString() + "\r\n");
                    }
                    else
                    {
                        infoData.Append("EndTime=\r\n");
                    }
                    for (int k = 0; k < this.tasks[i].Doorways.Count; k++)
                    {
                        infoData.Append("Log=" + this.tasks[i].Doorways[k].Log.ToString().Replace("\r\n", "¥") + "\r\n");
                        infoData.Append("Spam=" + this.tasks[i].Doorways[k].Spam.ToString().Replace("\r\n", "¥") + "\r\n");
                    }
                    infoData.Append("TaskLog=" + this.tasks[i].Log.ToString().Replace("\r\n", "¥") + "\r\n");

                    if (File.Exists(Path + "\\" + tasksDirectoryName + "\\info.txt"))
                    {
                        File.SetAttributes(Path + "\\" + tasksDirectoryName + "\\info.txt", FileAttributes.Normal);
                    }
                    File.WriteAllText(Path + "\\" + tasksDirectoryName + "\\info.txt", infoData.ToString(), Encoding.UTF8);
                    File.SetAttributes(Path + "\\" + tasksDirectoryName + "\\info.txt", FileAttributes.Normal);

                    if (File.Exists(Path + "\\" + tasksDirectoryName + "\\settings.txt"))
                    {
                        File.SetAttributes(Path + "\\" + tasksDirectoryName + "\\settings.txt", FileAttributes.Normal);
                    }
                    File.WriteAllText(Path + "\\" + tasksDirectoryName + "\\settings.txt", this.tasks[i].Settings.ToString(), Encoding.UTF8);
                    File.SetAttributes(Path + "\\" + tasksDirectoryName + "\\settings.txt", FileAttributes.Normal);
                }
                catch (Exception)
                {
                }
            }
        }

        private void SaveStatistics(string FilePath, ItemStatistics Statistics)
        {
            try
            {
                StringBuilder data = new StringBuilder(1000);
                for (int i = 0; i < Statistics.Years.Count; i++)
                {
                    for (int j = 0; j < Statistics.Years[i].Weeks.Count; j++)
                    {
                        data.Append(Statistics.Years[i].Year.ToString() + "-");
                        string week = Statistics.Years[i].Weeks[j].Week.ToString();
                        if (week.Length < 2)
                        {
                            week = "0" + week;
                        }
                        data.Append(week + "=");
                        data.Append(Statistics.Years[i].Weeks[j].TD.ToString() + "|" + Statistics.Years[i].Weeks[j].TT.ToString() + "|" +
                            Statistics.Years[i].Weeks[j].DD.ToString() + "|" + Statistics.Years[i].Weeks[j].DT.ToString() + "|" +
                            Statistics.Years[i].Weeks[j].PD.ToString());
                        data.Append("\r\n");
                    }
                }
                File.WriteAllText(FilePath, data.ToString(), Encoding.UTF8);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Удаление WS
        /// </summary>
        public void Delete()
        {
            try
            {
                string directoryName = this.id.ToString();
                while (directoryName.Length < 7)
                {
                    directoryName = "0" + directoryName;
                }

                Directory.Delete(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName, true);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Удаление кейворда
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteKeyWord(int ID)
        {
            for (int i = 0; i < this.keywords.Count; i++)
            {
                if (this.keywords[i].ID == ID)
                {
                    DeleteKeyWordFolder(ID);
                    this.keywords.RemoveAt(i);
                    return;
                }
            }
        }

        private void DeleteKeyWordFolder(int ID)
        {
            try
            {
                string directoryName = this.id.ToString();
                while (directoryName.Length < 7)
                {
                    directoryName = "0" + directoryName;
                }

                string keywordsDirectoryName = ID.ToString();
                while (keywordsDirectoryName.Length < 7)
                {
                    keywordsDirectoryName = "0" + keywordsDirectoryName;
                }

                Directory.Delete(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Keywords\\" + keywordsDirectoryName, true);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Удаление шаблона
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteTemplate(int ID)
        {
            for (int i = 0; i < this.templates.Count; i++)
            {
                if (this.templates[i].ID == ID)
                {
                    DeleteTemplateFolder(ID);
                    this.templates.RemoveAt(i);
                    return;
                }
            }
        }

        private void DeleteTemplateFolder(int ID)
        {
            try
            {
                string directoryName = this.id.ToString();
                while (directoryName.Length < 7)
                {
                    directoryName = "0" + directoryName;
                }

                string templateDirectoryName = ID.ToString();
                while (templateDirectoryName.Length < 7)
                {
                    templateDirectoryName = "0" + templateDirectoryName;
                }

                Directory.Delete(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Templates\\" + templateDirectoryName, true);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Удаление задания
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteTask(int ID)
        {
            for (int i = 0; i < this.tasks.Count; i++)
            {
                if (this.tasks[i].ID == ID)
                {
                    //Stopping
                    if (this.tasks[i].Status == 1 || this.tasks[i].Status == 2 || this.tasks[i].Status == 3 || this.tasks[i].Status == 4)
                    {
                        this.tasks[i].Stop();
                    }
                    //Deleting statistics
                    if (this.tasks[i].Status != 5)
                    {
                        this.StatisticsRemoveData(this.tasks[i].Settings.KeywordsID, this.tasks[i].PresetID, this.tasks[i].TemplateID, this.tasks[i].TextID, this.tasks[i].Settings.GeneralCreateDoorways, this.tasks[i].StartTime);
                    }
                    //Deleting
                    DeleteTaskFolder(ID);
                    this.tasks.RemoveAt(i);
                    return;
                }
            }
        }

        private void DeleteTaskFolder(int ID)
        {
            try
            {
                string directoryName = this.id.ToString();
                while (directoryName.Length < 7)
                {
                    directoryName = "0" + directoryName;
                }

                string taskDirectoryName = ID.ToString();
                while (taskDirectoryName.Length < 7)
                {
                    taskDirectoryName = "0" + taskDirectoryName;
                }

                Directory.Delete(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Tasks\\" + taskDirectoryName, true);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Удаление текста
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteText(int ID)
        {
            for (int i = 0; i < this.texts.Count; i++)
            {
                if (this.texts[i].ID == ID)
                {
                    DeleteTextFolder(ID);
                    this.texts.RemoveAt(i);
                    return;
                }
            }
        }

        private void DeleteTextFolder(int ID)
        {
            try
            {
                string directoryName = this.id.ToString();
                while (directoryName.Length < 7)
                {
                    directoryName = "0" + directoryName;
                }

                string textDirectoryName = ID.ToString();
                while (textDirectoryName.Length < 7)
                {
                    textDirectoryName = "0" + textDirectoryName;
                }

                Directory.Delete(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Text\\" + textDirectoryName, true);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Удаление пресета
        /// </summary>
        /// <param name="ID"></param>
        public void DeletePreset(int ID)
        {
            for (int i = 0; i < this.presets.Count; i++)
            {
                if (this.presets[i].ID == ID)
                {
                    DeletePresetFolder(ID);
                    this.presets.RemoveAt(i);
                    return;
                }
            }
        }

        private void DeletePresetFolder(int ID)
        {
            try
            {
                string directoryName = this.id.ToString();
                while (directoryName.Length < 7)
                {
                    directoryName = "0" + directoryName;
                }

                string presetDirectoryName = ID.ToString();
                while (presetDirectoryName.Length < 7)
                {
                    presetDirectoryName = "0" + presetDirectoryName;
                }

                Directory.Delete(System.Windows.Forms.Application.StartupPath + "\\Data\\" + directoryName + "\\Presets\\" + presetDirectoryName, true);
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Statistics
        //Обновление статистики
        public List<int> StatisticsUpdate()
        {
            List<int> okTasks = new List<int>();
            for (int i = 0; i < this.tasks.Count; i++)
            {
                if (this.tasks[i].Status == 5 && this.tasks[i].Statistics.Ok)
                {
                    this.tasks[i].Statistics.Ok = false;
                    okTasks.Add(i);

                    StatisticsUpdateDataWorkSpace(this.tasks[i].Statistics.DD, this.tasks[i].Statistics.PD, this.tasks[i].StartTime);
                    StatisticsUpdateDataKeyword(GetKeywordIndex(this.tasks[i].Settings.KeywordsID), this.tasks[i].Statistics.DD, this.tasks[i].Statistics.PD, this.tasks[i].StartTime);
                    if (GetPresetIndex(this.tasks[i].PresetID) != -1)
                    {
                        StatisticsUpdateDataPreset(GetPresetIndex(this.tasks[i].PresetID), this.tasks[i].Statistics.DD, this.tasks[i].Statistics.PD, this.tasks[i].StartTime);
                    }
                    StatisticsUpdateDataTemplate(GetTemplateIndex(this.tasks[i].TemplateID), this.tasks[i].Statistics.DD, this.tasks[i].Statistics.PD, this.tasks[i].StartTime);
                    StatisticsUpdateDataText(GetTextIndex(this.tasks[i].TextID), this.tasks[i].Statistics.DD, this.tasks[i].Statistics.PD, this.tasks[i].StartTime);
                }
            }
            return okTasks;
        }

        private void StatisticsUpdateDataWorkSpace(int DoorwaysCount, int PagesCount, DateTime StartTime)
        {
            //Добавление года и недели
            int yearIndex = 0;
            int weekIndex = 0;
            if (this.Statistics.Years.Count == 0)
            {
                this.Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Statistics.Years.Count; i++)
                {
                    if (this.Statistics.Years[i].Year == StartTime.Year)
                    {
                        found = true;
                        yearIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
                    yearIndex = this.Statistics.Years.Count - 1;
                }
            }

            if (this.Statistics.Years[yearIndex].Weeks.Count == 0)
            {
                this.Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Statistics.Years[yearIndex].Weeks.Count; i++)
                {
                    if (this.Statistics.Years[yearIndex].Weeks[i].Week == ItemStatistics.GetWeekNumber(StartTime))
                    {
                        found = true;
                        weekIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
                    weekIndex = this.Statistics.Years[yearIndex].Weeks.Count - 1;
                }
            }
            //Добавление данных
            this.Statistics.Years[yearIndex].Weeks[weekIndex].TD++;
            this.Statistics.Years[yearIndex].Weeks[weekIndex].DD += DoorwaysCount;
            this.Statistics.Years[yearIndex].Weeks[weekIndex].PD += PagesCount;
        }

        private void StatisticsUpdateDataKeyword(int KeywordIndex, int DoorwaysCount, int PagesCount, DateTime StartTime)
        {
            if (KeywordIndex == -1)
            {
                return;
            }
            //Добавление года и недели
            int yearIndex = 0;
            int weekIndex = 0;
            if (this.Keywords[KeywordIndex].Statistics.Years.Count == 0)
            {
                this.Keywords[KeywordIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Keywords[KeywordIndex].Statistics.Years.Count; i++)
                {
                    if (this.Keywords[KeywordIndex].Statistics.Years[i].Year == StartTime.Year)
                    {
                        found = true;
                        yearIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Keywords[KeywordIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
                    yearIndex = this.Keywords[KeywordIndex].Statistics.Years.Count - 1;
                }
            }

            if (this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks.Count == 0)
            {
                this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks.Count; i++)
                {
                    if (this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks[i].Week == ItemStatistics.GetWeekNumber(StartTime))
                    {
                        found = true;
                        weekIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
                    weekIndex = this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks.Count - 1;
                }
            }
            //Добавление данных
            this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TD++;
            this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DD += DoorwaysCount;
            this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks[weekIndex].PD += PagesCount;
        }

        private void StatisticsUpdateDataPreset(int PresetIndex, int DoorwaysCount, int PagesCount, DateTime StartTime)
        {
            if (PresetIndex == -1)
            {
                return;
            }
            //Добавление года и недели
            int yearIndex = 0;
            int weekIndex = 0;
            if (this.Presets[PresetIndex].Statistics.Years.Count == 0)
            {
                this.Presets[PresetIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Presets[PresetIndex].Statistics.Years.Count; i++)
                {
                    if (this.Presets[PresetIndex].Statistics.Years[i].Year == StartTime.Year)
                    {
                        found = true;
                        yearIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Presets[PresetIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
                    yearIndex = this.Presets[PresetIndex].Statistics.Years.Count - 1;
                }
            }

            if (this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks.Count == 0)
            {
                this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks.Count; i++)
                {
                    if (this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks[i].Week == StartTime.Year)
                    {
                        found = true;
                        weekIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
                    weekIndex = this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks.Count - 1;
                }
            }
            //Добавление данных
            this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TD++;
            this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DD += DoorwaysCount;
            this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks[weekIndex].PD += PagesCount;
        }

        private void StatisticsUpdateDataTemplate(int TemplateIndex, int DoorwaysCount, int PagesCount, DateTime StartTime)
        {
            if (TemplateIndex == -1)
            {
                return;
            }
            //Добавление года и недели
            int yearIndex = 0;
            int weekIndex = 0;
            if (this.Templates[TemplateIndex].Statistics.Years.Count == 0)
            {
                this.Templates[TemplateIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Templates[TemplateIndex].Statistics.Years.Count; i++)
                {
                    if (this.Templates[TemplateIndex].Statistics.Years[i].Year == StartTime.Year)
                    {
                        found = true;
                        yearIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Templates[TemplateIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
                    yearIndex = this.Templates[TemplateIndex].Statistics.Years.Count - 1;
                }
            }

            if (this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks.Count == 0)
            {
                this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks.Count; i++)
                {
                    if (this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks[i].Week == ItemStatistics.GetWeekNumber(StartTime))
                    {
                        found = true;
                        weekIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
                    weekIndex = this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks.Count - 1;
                }
            }
            //Добавление данных
            this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TD++;
            this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DD += DoorwaysCount;
            this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks[weekIndex].PD += PagesCount;
        }

        private void StatisticsUpdateDataText(int TextIndex, int DoorwaysCount, int PagesCount, DateTime StartTime)
        {
            if (TextIndex == -1)
            {
                return;
            }
            //Добавление года и недели
            int yearIndex = 0;
            int weekIndex = 0;
            if (this.Texts[TextIndex].Statistics.Years.Count == 0)
            {
                this.Texts[TextIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Texts[TextIndex].Statistics.Years.Count; i++)
                {
                    if (this.Texts[TextIndex].Statistics.Years[i].Year == StartTime.Year)
                    {
                        found = true;
                        yearIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Texts[TextIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
                    yearIndex = this.Texts[TextIndex].Statistics.Years.Count - 1;
                }
            }

            if (this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks.Count == 0)
            {
                this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks.Count; i++)
                {
                    if (this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks[i].Week == ItemStatistics.GetWeekNumber(StartTime))
                    {
                        found = true;
                        weekIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
                    weekIndex = this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks.Count - 1;
                }
            }
            //Добавление данных
            this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TD++;
            this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DD += DoorwaysCount;
            this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks[weekIndex].PD += PagesCount;
        }

        /// <summary>
        /// Добавление данных к статистике
        /// </summary>
        /// <param name="KeywordIndex"></param>
        /// <param name="PresetIndex"></param>
        /// <param name="TemplateIndex"></param>
        /// <param name="TextIndex"></param>
        /// <param name="DoorwaysCount"></param>
        /// <param name="StartTime"></param>
        public void StatisticsAddData(int KeywordIndex, int PresetIndex, int TemplateIndex, int TextIndex, int DoorwaysCount, DateTime StartTime)
        {
            StatisticsAddDataWorkSpace(DoorwaysCount, StartTime);
            StatisticsAddDataKeyword(KeywordIndex, DoorwaysCount, StartTime);
            if (PresetIndex != -1)
            {
                StatisticsAddDataPreset(PresetIndex, DoorwaysCount, StartTime);
            }
            StatisticsAddDataTemplate(TemplateIndex, DoorwaysCount, StartTime);
            StatisticsAddDataText(TextIndex, DoorwaysCount, StartTime);
        }
        /// <summary>
        /// Добавление данных к статистике по ID
        /// </summary>
        /// <param name="KeywordID"></param>
        /// <param name="PresetID"></param>
        /// <param name="TemplateID"></param>
        /// <param name="TextID"></param>
        /// <param name="DoorwaysCount"></param>
        /// <param name="StartTime"></param>
        public void StatisticsAddDataID(int KeywordID, int PresetID, int TemplateID, int TextID, int DoorwaysCount, DateTime StartTime)
        {
            StatisticsAddDataWorkSpace(DoorwaysCount, StartTime);
            if (GetKeywordIndex(KeywordID) != -1)
            {
                StatisticsAddDataKeyword(GetKeywordIndex(KeywordID), DoorwaysCount, StartTime);
            }
            if (GetPresetIndex(PresetID) != -1)
            {
                StatisticsAddDataPreset(GetPresetIndex(PresetID), DoorwaysCount, StartTime);
            }
            if (GetTemplateIndex(TemplateID) != -1)
            {
                StatisticsAddDataTemplate(GetTemplateIndex(TemplateID), DoorwaysCount, StartTime);
            }
            if (GetTextIndex(TextID) != -1)
            {
                StatisticsAddDataText(GetTextIndex(TextID), DoorwaysCount, StartTime);
            }
        }
        
        private void StatisticsAddDataWorkSpace(int DoorwaysCount, DateTime StartTime)
        {
            //Добавление года и недели
            int yearIndex = 0;
            int weekIndex = 0;
            if (this.Statistics.Years.Count == 0)
            {
                this.Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Statistics.Years.Count; i++)
                {
                    if (this.Statistics.Years[i].Year == StartTime.Year)
                    {
                        found = true;
                        yearIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
                    yearIndex = this.Statistics.Years.Count - 1;
                }
            }

            if (this.Statistics.Years[yearIndex].Weeks.Count == 0)
            {
                this.Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Statistics.Years[yearIndex].Weeks.Count; i++)
                {
                    if (this.Statistics.Years[yearIndex].Weeks[i].Week == ItemStatistics.GetWeekNumber(StartTime))
                    {
                        found = true;
                        weekIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
                    weekIndex = this.Statistics.Years[yearIndex].Weeks.Count - 1;
                }
            }
            //Добавление данных
            this.Statistics.Years[yearIndex].Weeks[weekIndex].TT += 1;
            this.Statistics.Years[yearIndex].Weeks[weekIndex].DT += DoorwaysCount;
        }

        private void StatisticsAddDataKeyword(int KeywordIndex, int DoorwaysCount, DateTime StartTime)
        {
            //Добавление года и недели
            int yearIndex = 0;
            int weekIndex = 0;
            if (this.Keywords[KeywordIndex].Statistics.Years.Count == 0)
            {
                this.Keywords[KeywordIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Keywords[KeywordIndex].Statistics.Years.Count; i++)
                {
                    if (this.Keywords[KeywordIndex].Statistics.Years[i].Year == StartTime.Year)
                    {
                        found = true;
                        yearIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Keywords[KeywordIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
                    yearIndex = this.Keywords[KeywordIndex].Statistics.Years.Count - 1;
                }
            }

            if (this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks.Count == 0)
            {
                this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks.Count; i++)
                {
                    if (this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks[i].Week == ItemStatistics.GetWeekNumber(StartTime))
                    {
                        found = true;
                        weekIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
                    weekIndex = this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks.Count - 1;
                }
            }
            //Добавление данных
            this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT += 1;
            this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT += DoorwaysCount;
        }

        private void StatisticsAddDataPreset(int PresetIndex, int DoorwaysCount, DateTime StartTime)
        {
            //Добавление года и недели
            int yearIndex = 0;
            int weekIndex = 0;
            if (this.Presets[PresetIndex].Statistics.Years.Count == 0)
            {
                this.Presets[PresetIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Presets[PresetIndex].Statistics.Years.Count; i++)
                {
                    if (this.Presets[PresetIndex].Statistics.Years[i].Year == StartTime.Year)
                    {
                        found = true;
                        yearIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Presets[PresetIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
                    yearIndex = this.Presets[PresetIndex].Statistics.Years.Count - 1;
                }
            }

            if (this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks.Count == 0)
            {
                this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks.Count; i++)
                {
                    if (this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks[i].Week == StartTime.Year)
                    {
                        found = true;
                        weekIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
                    weekIndex = this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks.Count - 1;
                }
            }
            //Добавление данных
            this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT += 1;
            this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT += DoorwaysCount;
        }

        private void StatisticsAddDataTemplate(int TemplateIndex, int DoorwaysCount, DateTime StartTime)
        {
            //Добавление года и недели
            int yearIndex = 0;
            int weekIndex = 0;
            if (this.Templates[TemplateIndex].Statistics.Years.Count == 0)
            {
                this.Templates[TemplateIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Templates[TemplateIndex].Statistics.Years.Count; i++)
                {
                    if (this.Templates[TemplateIndex].Statistics.Years[i].Year == StartTime.Year)
                    {
                        found = true;
                        yearIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Templates[TemplateIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
                    yearIndex = this.Templates[TemplateIndex].Statistics.Years.Count - 1;
                }
            }

            if (this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks.Count == 0)
            {
                this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks.Count; i++)
                {
                    if (this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks[i].Week == ItemStatistics.GetWeekNumber(StartTime))
                    {
                        found = true;
                        weekIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
                    weekIndex = this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks.Count - 1;
                }
            }
            //Добавление данных
            this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT += 1;
            this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT += DoorwaysCount;
        }

        private void StatisticsAddDataText(int TextIndex, int DoorwaysCount, DateTime StartTime)
        {
            //Добавление года и недели
            int yearIndex = 0;
            int weekIndex = 0;
            if (this.Texts[TextIndex].Statistics.Years.Count == 0)
            {
                this.Texts[TextIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Texts[TextIndex].Statistics.Years.Count; i++)
                {
                    if (this.Texts[TextIndex].Statistics.Years[i].Year == StartTime.Year)
                    {
                        found = true;
                        yearIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Texts[TextIndex].Statistics.Years.Add(new ItemStatistics.StatisticsYears(StartTime.Year));
                    yearIndex = this.Texts[TextIndex].Statistics.Years.Count - 1;
                }
            }

            if (this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks.Count == 0)
            {
                this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks.Count; i++)
                {
                    if (this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks[i].Week == ItemStatistics.GetWeekNumber(StartTime))
                    {
                        found = true;
                        weekIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks.Add(new ItemStatistics.StatisticsWeeks(ItemStatistics.GetWeekNumber(StartTime)));
                    weekIndex = this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks.Count - 1;
                }
            }
            //Добавление данных
            this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT += 1;
            this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT += DoorwaysCount;
        }
        /// <summary>
        /// Удаление данных из статистики
        /// </summary>
        /// <param name="KeywordID"></param>
        /// <param name="PresetID"></param>
        /// <param name="TemplateID"></param>
        /// <param name="TextID"></param>
        /// <param name="DoorwaysCount"></param>
        /// <param name="StartTime"></param>
        public void StatisticsRemoveData(int KeywordID, int PresetID, int TemplateID, int TextID, int DoorwaysCount, DateTime StartTime)
        {
            StatisticsRemoveDataWorkSpace(DoorwaysCount, StartTime);
            if (GetKeywordIndex(KeywordID) != -1)
            {
                StatisticsRemoveDataKeyword(GetKeywordIndex(KeywordID), DoorwaysCount, StartTime);
            }
            if (GetPresetIndex(PresetID) != -1)
            {
                StatisticsRemoveDataPreset(GetPresetIndex(PresetID), DoorwaysCount, StartTime);
            }
            if (GetTemplateIndex(TemplateID) != -1)
            {
                StatisticsRemoveDataTemplate(GetTemplateIndex(TemplateID), DoorwaysCount, StartTime);
            }
            if (GetTextIndex(TextID) != -1)
            {
                StatisticsRemoveDataText(GetTextIndex(TextID), DoorwaysCount, StartTime);
            }
        }
        
        private void StatisticsRemoveDataWorkSpace(int DoorwaysCount, DateTime StartTime)
        {
            //Поиск года и недели
            int yearIndex = 0;
            int weekIndex = 0;
            if (this.Statistics.Years.Count == 0)
            {
                return;
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Statistics.Years.Count; i++)
                {
                    if (this.Statistics.Years[i].Year == StartTime.Year)
                    {
                        found = true;
                        yearIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    return;
                }
            }

            if (this.Statistics.Years[yearIndex].Weeks.Count == 0)
            {
                return;
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Statistics.Years[yearIndex].Weeks.Count; i++)
                {
                    if (this.Statistics.Years[yearIndex].Weeks[i].Week == ItemStatistics.GetWeekNumber(StartTime))
                    {
                        found = true;
                        weekIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    return;
                }
            }
            //Добавление данных
            this.Statistics.Years[yearIndex].Weeks[weekIndex].TT -= 1;
            this.Statistics.Years[yearIndex].Weeks[weekIndex].DT -= DoorwaysCount;
        }

        private void StatisticsRemoveDataKeyword(int KeywordIndex, int DoorwaysCount, DateTime StartTime)
        {
            //Поиск года и недели
            int yearIndex = 0;
            int weekIndex = 0;
            if (this.Keywords[KeywordIndex].Statistics.Years.Count == 0)
            {
                return;
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Keywords[KeywordIndex].Statistics.Years.Count; i++)
                {
                    if (this.Keywords[KeywordIndex].Statistics.Years[i].Year == StartTime.Year)
                    {
                        found = true;
                        yearIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    return;
                }
            }

            if (this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks.Count == 0)
            {
                return;
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks.Count; i++)
                {
                    if (this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks[i].Week == ItemStatistics.GetWeekNumber(StartTime))
                    {
                        found = true;
                        weekIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    return;
                }
            }
            //Добавление данных
            this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT -= 1;
            this.Keywords[KeywordIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT -= DoorwaysCount;
        }

        private void StatisticsRemoveDataPreset(int PresetIndex, int DoorwaysCount, DateTime StartTime)
        {
            //Поиск года и недели
            int yearIndex = 0;
            int weekIndex = 0;
            if (this.Presets[PresetIndex].Statistics.Years.Count == 0)
            {
                return;
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Presets[PresetIndex].Statistics.Years.Count; i++)
                {
                    if (this.Presets[PresetIndex].Statistics.Years[i].Year == StartTime.Year)
                    {
                        found = true;
                        yearIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    return;
                }
            }

            if (this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks.Count == 0)
            {
                return;
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks.Count; i++)
                {
                    if (this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks[i].Week == ItemStatistics.GetWeekNumber(StartTime))
                    {
                        found = true;
                        weekIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    return;
                }
            }
            //Добавление данных
            this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT -= 1;
            this.Presets[PresetIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT -= DoorwaysCount;
        }

        private void StatisticsRemoveDataTemplate(int TemplateIndex, int DoorwaysCount, DateTime StartTime)
        {
            //Поиск года и недели
            int yearIndex = 0;
            int weekIndex = 0;
            if (this.Templates[TemplateIndex].Statistics.Years.Count == 0)
            {
                return;
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Templates[TemplateIndex].Statistics.Years.Count; i++)
                {
                    if (this.Templates[TemplateIndex].Statistics.Years[i].Year == StartTime.Year)
                    {
                        found = true;
                        yearIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    return;
                }
            }

            if (this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks.Count == 0)
            {
                return;
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks.Count; i++)
                {
                    if (this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks[i].Week == ItemStatistics.GetWeekNumber(StartTime))
                    {
                        found = true;
                        weekIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    return;
                }
            }
            //Добавление данных
            this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT -= 1;
            this.Templates[TemplateIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT -= DoorwaysCount;
        }

        private void StatisticsRemoveDataText(int TextIndex, int DoorwaysCount, DateTime StartTime)
        {
            //Поиск года и недели
            int yearIndex = 0;
            int weekIndex = 0;
            if (this.Texts[TextIndex].Statistics.Years.Count == 0)
            {
                return;
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Texts[TextIndex].Statistics.Years.Count; i++)
                {
                    if (this.Texts[TextIndex].Statistics.Years[i].Year == StartTime.Year)
                    {
                        found = true;
                        yearIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    return;
                }
            }

            if (this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks.Count == 0)
            {
                return;
            }
            else
            {
                bool found = false;
                for (int i = 0; i < this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks.Count; i++)
                {
                    if (this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks[i].Week == ItemStatistics.GetWeekNumber(StartTime))
                    {
                        found = true;
                        weekIndex = i;
                        break;
                    }
                }
                if (!found)
                {
                    return;
                }
            }
            //Добавление данных
            this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks[weekIndex].TT -= 1;
            this.Texts[TextIndex].Statistics.Years[yearIndex].Weeks[weekIndex].DT -= DoorwaysCount;
        }

        #endregion

        /// <summary>
        /// Определение индекса по ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int GetKeywordIndex(int ID)
        {
            for (int i = 0; i < this.keywords.Count; i++)
            {
                if (keywords[i].ID == ID)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Определение индекса по ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int GetWSIndex(int ID)
        {
            for (int i = 0; i < SharedData.WorkSpaces.Count; i++)
            {
                if (SharedData.WorkSpaces[i].ID == ID)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Поиск индекса выполненого задания, если таких нет, то возвращается -1
        /// </summary>
        /// <returns></returns>
        public int GetFinishedTaskIndex()
        {
            for (int i = 0; i < this.tasks.Count; i++)
            {
                if (this.tasks[i].Status == 5)
                {
                    return i;
                }
            }

            return -1;
        }
        /// <summary>
        /// Определение индекса по ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int GetPresetIndex(int ID)
        {
            for (int i = 0; i < this.presets.Count; i++)
            {
                if (presets[i].ID == ID)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Определение индекса по ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int GetTemplateIndex(int ID)
        {
            for (int i = 0; i < this.templates.Count; i++)
            {
                if (templates[i].ID == ID)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Определение индекса по ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int GetTextIndex(int ID)
        {
            for (int i = 0; i < this.texts.Count; i++)
            {
                if (texts[i].ID == ID)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
