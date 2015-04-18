using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Settings;

namespace Doorway_Studio
{
    class Task
    {
        public Task(int ID, string Name, string Comment)
        {
            this.id = ID;
            this.name = Name;
            this.comment = Comment;

            this.statistics = new StatisticData();

            this.log = new StringBuilder();

            this.doorways = new List<Doorway>();
            this.threads = new List<Thread>();
            this.done = new List<bool>();
        }

        #region Data
        private int wsIndex;
        private int id;
        private string name;
        private string comment;
        private PresetSettings settings;

        private DateTime startTime;
        private DateTime endTime;

        private int workingStatus;

        private int status;

        private int templateID;
        private int presetID;
        private int textID;

        private StringBuilder log;

        private List<Doorway> doorways;
        private List<Thread> threads;
        private List<bool> done;

        private Timer mainTimer;
        private StatisticData statistics;
        #endregion

        #region Properties
        /// <summary>
        /// ID of Task
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
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

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
        /// Настройки
        /// </summary>
        public PresetSettings Settings
        {
            get
            {
                return this.settings;
            }

            set
            {
                this.settings = value;
                this.doorways.Clear();

                // Создание нужного числа обьектов типа дорвей
                for (int i = 0; i < value.GeneralCreateDoorways; i++)
                {
                    this.doorways.Add(new Doorway(this.wsIndex, i));
                    Thread.Sleep(500);
                }
            }
        }

        /// <summary>
        /// Время старта задания
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }

        /// <summary>
        /// Время, когда выполнение задания завершилось
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        /// <summary>
        /// Статус выполнения; 0 - новое задание; 1 - задание выполняется;
        /// 2 - пауза; 3 - задание остановлено; 4 - выгрузка по фтп;
        /// 5 - готово; 6 - критическая ошибка
        /// </summary>
        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        /// <summary>
        /// Статус выполнения задания в %
        /// </summary>
        public int WorkingStatus
        {
            get
            {
                return workingStatus;
            }
            set
            {
                workingStatus = value;
            }
        }

        /// <summary>
        /// Template ID
        /// </summary>
        public int TemplateID
        {
            get
            {
                return templateID;
            }
            set
            {
                templateID = value;
            }
        }
        /// <summary>
        /// Text ID
        /// </summary>
        public int TextID
        {
            get
            {
                return textID;
            }
            set
            {

                textID = value;
            }
        }
        /// <summary>
        /// Preset ID
        /// </summary>
        public int PresetID
        {
            get
            {
                return presetID;
            }
            set
            {

                presetID = value;
            }
        }
        /// <summary>
        /// Довеи
        /// </summary>
        public List<Doorway> Doorways
        {
            get
            {
                return doorways;
            }
            set
            {
                doorways = value;
            }
        }
        /// <summary>
        /// Статистика
        /// </summary>
        public StatisticData Statistics
        {
            get
            {
                return this.statistics;
            }
            set
            {
                this.statistics = value;
            }
        }
        /// <summary>
        /// WorkSpace Index
        /// </summary>
        public int WSIndex
        {
            get
            {
                return this.wsIndex;
            }
            set
            {
                this.wsIndex = value;
            }
        }
        /// <summary>
        /// Log
        /// </summary>
        public StringBuilder Log
        {
            get
            {
                return this.log;
            }
            set
            {
                this.log = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Старт задания
        /// </summary>
        public void Start()
        {
            this.status = 1;
            this.startTime = DateTime.Now;
            this.log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000400") + "\r\n");
            StartThreads();
        }

        private void StartThreads()
        {
            //Подсчет количества потоков
            this.log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000401") + "\r\n");
            if (this.settings.GeneralThreads > this.settings.GeneralCreateDoorways)
            {
                this.settings.GeneralThreads = this.settings.GeneralCreateDoorways;
            }
            if (this.settings.GeneralCreateDoorways == 1)
            {
                this.settings.GeneralThreads = 1;
            }
            //Создание списков переменных
            this.threads = new List<Thread>();
            this.done = new List<bool>();
            //Подсчет кол-ва доров на поток
            int doorwaysPerThread = this.settings.GeneralCreateDoorways / this.settings.GeneralThreads;
            int startDoorwayIndex = 0;
            for (int i = 0; i < this.settings.GeneralThreads - 1; i++)
            {
                this.threads.Add(new Thread(Work));
                this.done.Add(false);
                this.threads[i].Start(new StartData(i, startDoorwayIndex, startDoorwayIndex + doorwaysPerThread));
                this.log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000402") + (i + 1).ToString() + ".\r\n");
                if (MainSettings.Debug)
                {
                    this.log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000404") + (i + 1).ToString() + View.UILanguageResources.GetString("S0000414") + (startDoorwayIndex + 1).ToString() + View.UILanguageResources.GetString("S0000415") + (startDoorwayIndex + 1 + doorwaysPerThread + 1).ToString() + ".\r\n");
                }
                startDoorwayIndex += doorwaysPerThread;
                Thread.Sleep(500);
            }
            this.threads.Add(new Thread(Work));
            this.done.Add(false);
            this.threads[this.threads.Count - 1].Start(new StartData(this.threads.Count - 1, startDoorwayIndex, this.settings.GeneralCreateDoorways));
            this.log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000402") + this.threads.Count.ToString() + ".\r\n");
            if (MainSettings.Debug)
            {
                this.log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000404") + this.settings.GeneralThreads.ToString() + View.UILanguageResources.GetString("S0000414") + (startDoorwayIndex + 1).ToString() + View.UILanguageResources.GetString("S0000415") + this.settings.GeneralCreateDoorways.ToString() + ".\r\n");
            }
            this.mainTimer = new Timer(new TimerCallback(mainTimerTick), null, 0, 100);
        }

        private void mainTimerTick(Object stateInfo)
        {
            UpdateWorkingStatus();
            if (this.status == 6)
            {
                this.mainTimer.Dispose();
                return;
            }
            //Проверка, все ли выполнилось
            bool allDone = true;
            for (int i = 0; i < this.done.Count; i++)
            {
                if (!this.done[i])
                {
                    allDone = false;
                    break;
                }
            }
            if (!allDone)
            {
                return;
            }
            //Поиск потоков для заливки файлов по фтп в фоне
            if (this.settings.FTPUpload && this.settings.FTPUploadInBackground)
            {
                for (int i = 0; i < this.doorways.Count; i++)
                {
                    for (int j = 0; j < this.doorways[i].Done.Count; j++)
                    {
                        if (!this.doorways[i].Done[j])
                        {
                            this.status = 4;
                            return;
                        }
                    }
                }
            }
            //Статистика
            this.endTime = DateTime.Now;
            this.statistics.Ok = true;
            this.statistics.DD = this.doorways.Count;
            for (int i = 0; i < this.doorways.Count; i++)
            {
                this.statistics.PD += this.doorways[i].PagesCount;
            }
            //Закругление таймера
            this.status = 5;
            this.mainTimer.Dispose();
            this.log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000403") + ".\r\n");
        }

        private Random random;

        private void Work(object Parameters)
        {
            StartData threadParameters = Parameters as StartData;
            int usedKeywords = 0;

            // Подготовка
            random = new Random(Environment.TickCount);
            for (int i = threadParameters.Start; i < threadParameters.End; i++)
            {
                this.log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000404") + (threadParameters.ThreadIndex + 1).ToString() + "." +
                                View.UILanguageResources.GetString("S0000405") + (i + 1).ToString() + View.UILanguageResources.GetString("S0000406") + "\r\n");
                //Модифицирование/заполнение настройками
                this.doorways[i].Settings = this.settings;
                this.doorways[i].TemplateID = this.templateID;
                this.doorways[i].TextID = this.textID;
                if (this.settings.KeywordsSelectType == 4)
                {
                    this.doorways[i].UseKeyWordsStartWith = usedKeywords;
                    usedKeywords += random.Next(this.settings.KeywordsSelectMin, this.settings.KeywordsSelectMax);
                    this.doorways[i].UseKeyWordsEndWith = usedKeywords;
                }
                if (this.doorways[i].Prepare() == -1)
                {
                    this.status = 6;
                    this.done[threadParameters.ThreadIndex] = true;
                    return;
                }
                Thread.Sleep(500);
            }

            // Запуск генерирования
            for (int i = threadParameters.Start; i < threadParameters.End; i++)
            {
                this.doorways[i].Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000400") + "\r\n");
                if (this.doorways[i].Start() != 0)
                {
                    this.doorways[i].Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000407") + "\r\n");
                    this.status = 6;
                    break;
                }

                if (this.status != 6)
                {
                    //Сохранение файлов
                    this.log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000404") + (threadParameters.ThreadIndex + 1).ToString() + "." +
                                    View.UILanguageResources.GetString("S0000405") + (i + 1).ToString() + View.UILanguageResources.GetString("S0000408") + "\r\n");
                    this.doorways[i].Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000409") + "\r\n");
                    this.doorways[i].Save();
                    //Выгрузка
                    this.log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000404") + (threadParameters.ThreadIndex + 1).ToString() + "." +
                                    View.UILanguageResources.GetString("S0000405") + (i + 1).ToString() + View.UILanguageResources.GetString("S0000410") + "\r\n");
                    if (this.Settings.FTPUpload)
                    {
                        this.doorways[i].Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000411") + "\r\n");
                        this.doorways[i].Upload();
                        //Удаление файлов
                        if (this.settings.FTPDelete)
                        {
                            this.doorways[i].DeleteFiles();
                        }
                    }
                    this.log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000404") + (threadParameters.ThreadIndex + 1).ToString() + "." +
                                    View.UILanguageResources.GetString("S0000405") + (i + 1).ToString() + View.UILanguageResources.GetString("S0000412") + ".\r\n");
                    this.doorways[i].Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000413") + ".\r\n");
                }
            }

            // Spam
            if (this.settings.Spam && this.settings.SpamSaveToFile)
            {
                SaveSpam();
            }

            // XRumer
            if (this.settings.XRumerUse && this.settings.GeneralDoorwayUrls.Length != 0)
            {
                SaveXRumerXML();
            }

            // Очистка памяти
            for (int i = threadParameters.Start; i < threadParameters.End; i++)
            {
                this.doorways[i].ClearMemory();
            }

            this.done[threadParameters.ThreadIndex] = true;
        }

        private string GetLinksFileName(int SiteIndex)
        {
            if (this.settings.GeneralDoorwayUrls.Length < SiteIndex)
            {
                return (SiteIndex + 1).ToString();
            }
            if (this.settings.GeneralDoorwayUrls[SiteIndex] == string.Empty)
            {
                return (SiteIndex + 1).ToString();
            }

            string site = this.settings.GeneralDoorwayUrls[SiteIndex].ToLower().Replace("http://", string.Empty).Replace("https://", string.Empty);
            if (site.Contains("/"))
            {
                site = site.Substring(0, site.LastIndexOf("/")).Replace("/", "-");
            }

            return site;
        }

        private void SaveXRumerXML()
        {
            if (string.IsNullOrEmpty(this.settings.XRumerDirectory))
            {
                return;
            }

            if (this.random == null)
            {
                random = new Random(Environment.TickCount);
            }

            try
            {
                // Preparing
                if (!Directory.Exists(this.settings.XRumerDirectory))
                {
                    Directory.CreateDirectory(this.settings.XRumerDirectory);
                }

                // Work
                for (int i = 0; i < this.doorways.Count; i++)
                {
                    if (i < this.settings.GeneralDoorwayUrls.Length && !string.IsNullOrEmpty(this.settings.GeneralDoorwayUrls[i]) && !string.IsNullOrEmpty(this.settings.XRumerTemplate))
                    {
                        string content = this.settings.XRumerTemplate;

                        while (content.Contains("[TEXT]"))
                        {
                            int startPosition = 0;
                            string tempString = string.Empty;
                            string text = this.settings.XRumerText;
                            
                            // Prepare text
                            while (text.Contains("[[") && text.Contains("]]"))
                            {
                                startPosition = text.IndexOf("[[");
                                int endPosition = text.IndexOf("]]");
                                int mixedEndPosition = text.IndexOf("]]]");

                                tempString = text.Substring(startPosition, (endPosition == mixedEndPosition ? mixedEndPosition + 3 : endPosition + 2) - startPosition);
                                text = text.Remove(startPosition, (endPosition == mixedEndPosition ? mixedEndPosition + 3 : endPosition + 2) - startPosition);

                                tempString = tempString.Replace("[[", string.Empty).Replace("]]", string.Empty);
                                string[] macroses = tempString.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                                text = text.Insert(startPosition, macroses[this.random.Next(macroses.Length)]);
                            }

                            int index = content.IndexOf("[TEXT]");
                            content = content.Remove(index, 6).Insert(index, text);
                        }

                        content = content.Replace("[KEY]", this.doorways[i].Pages[0].Keywords[0]).Replace("[LINK]", this.settings.GeneralDoorwayUrls[i]);

                        // Random
                        while (content.Contains("[RKEY]"))
                        {
                            int index = content.IndexOf("[RKEY]");

                            string key = string.Empty;
                            while (string.IsNullOrEmpty(key))
                            {
                                int pageIndex = this.random.Next(this.doorways[i].Pages.Count);
                                if (this.doorways[i].Pages[pageIndex].Keywords.Count > 0)
                                {
                                    key = this.doorways[i].Pages[pageIndex].Keywords[0];
                                }
                            }

                            content = content.Remove(index, 6).Insert(index, key);
                        }

                        while (content.Contains("[RLINK]"))
                        {
                            int index = content.IndexOf("[RLINK]");
                            content = content.Remove(index, 7).Insert(index, this.doorways[i].Pages[this.random.Next(this.doorways[i].Pages.Count)].URL ?? string.Empty);
                        }

                        // Random site
                        while (content.Contains("[RSITEKEY]"))
                        {
                            int index = content.IndexOf("[RSITEKEY]");
                            content = content.Remove(index, 10).Insert(index, this.doorways[this.random.Next(this.doorways.Count)].Pages[0].Keywords[0]);
                        }

                        while (content.Contains("[RSITELINK]"))
                        {
                            int index = content.IndexOf("[RSITELINK]");
                            content = content.Remove(index, 11).Insert(index, this.settings.GeneralDoorwayUrls[this.random.Next(this.settings.GeneralDoorwayUrls.Length)]);
                        }

                        // Save file
                        string fileName = Path.Combine(this.settings.XRumerDirectory, this.settings.GeneralDoorwayUrls[i].ToLower().Replace("http:", string.Empty).Replace("https:", string.Empty)
                                                                                          .Replace("'", string.Empty).Replace("/", string.Empty).Replace("\"", string.Empty).Replace(":", string.Empty)
                                                                                          .Replace("?", string.Empty).Replace("*", string.Empty).Replace("<", string.Empty)
                                                                                          .Replace(">", string.Empty).Replace("|", string.Empty).Replace("№", string.Empty)
                                                                                          .Replace("«", string.Empty).Replace("»", string.Empty).Replace("&", string.Empty) + ".xml");
                        File.WriteAllText(fileName, content, Encoding.UTF8);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void SaveSpam()
        {
            if (string.IsNullOrEmpty(this.settings.SpamSaveToFilePath))
            {
                return;
            }

            try
            {
                // Preparing
                if (!Directory.Exists(this.settings.SpamSaveToFilePath))
                {
                    Directory.CreateDirectory(this.settings.SpamSaveToFilePath);
                }

                // Work
                switch (this.settings.SpamSaveToFileType)
                {
                    // Один файл/Дорвей
                    case 0:
                        {
                            for (int i = 0; i < this.doorways.Count; i++)
                            {
                                File.WriteAllText(
                                    Path.Combine(this.settings.SpamSaveToFilePath, this.GetLinksFileName(i) + ".txt"),
                                    this.doorways[i].Spam.ToString().Trim(),
                                    this.settings.SpamSaveEncoding == 0 ? Encoding.Default : Encoding.UTF8);
                            }

                            break;
                        }

                    // Много файлов/Дорвей
                    case 1:
                        {
                            for (int i = 0; i < this.doorways.Count; i++)
                            {
                                string[] spamLinks = this.doorways[i].Spam.ToString().Split(new string[] { "\r\n\r\n\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                                for (int k = 0; k < spamLinks.Length; k++)
                                {
                                    if (spamLinks[k] == "\r\n")
                                    {
                                        continue;
                                    }

                                    File.WriteAllText(
                                        Path.Combine(this.settings.SpamSaveToFilePath,
                                                     this.GetLinksFileName(i) + "-" + (k + 1).ToString() + ".txt"),
                                        spamLinks[k].Trim(),
                                        this.settings.SpamSaveEncoding == 0 ? Encoding.Default : Encoding.UTF8);
                                }
                            }
                            break;
                        }

                    // Один файл/Дорвеи
                    case 2:
                        {
                            StringBuilder spamLinks = new StringBuilder(10000);
                            for (int i = 0; i < this.doorways.Count; i++)
                            {
                                spamLinks.Append(this.doorways[i].Spam);
                            }

                            File.AppendAllText(Path.Combine(this.settings.SpamSaveToFilePath, "Links.txt"),
                                               spamLinks.ToString().Trim() + "\r\n",
                                               this.settings.SpamSaveEncoding == 0 ? Encoding.Default : Encoding.UTF8);
                            break;
                        }

                    // Много файлов/Дорвеи
                    case 3:
                        {
                            for (int i = 0; i < this.doorways.Count; i++)
                            {
                                if (this.doorways[i].SpamSaved)
                                {
                                    continue;
                                }

                                string[] spamLinks = this.doorways[i].Spam.ToString().Split(new string[] {"\r\n\r\n\r\n\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                                for (int k = 0; k < spamLinks.Length; k++)
                                {
                                    if (spamLinks[k] == "\r\n")
                                    {
                                        continue;
                                    }

                                    string filePath = Path.Combine(this.settings.SpamSaveToFilePath, /*(i + 1).ToString() + "-" + */
                                                                   (k + 1).ToString() + ".txt");

                                    File.AppendAllText(filePath, spamLinks[k].Trim() + "\r\n",
                                                       this.settings.SpamSaveEncoding == 0 ? Encoding.Default : Encoding.UTF8);
                                }

                                this.doorways[i].SpamSaved = true;
                            }
                            break;
                        }
                }
            }
            catch (Exception)
            {
            }
        }


        /// <summary>
        /// Старт после паузы/остановки
        /// </summary>
        public void StartAfterPauseStop()
        {
            // Продолжение после паузы
            if (this.status == 2)
            {
                this.status = 1;
                for (int i = 0; i < this.threads.Count; i++)
                {
                    this.doorways[i].Pause = false;
                    //this.threads[i].Resume();
                }
            }

            // Продолжение после остановки
            else if (this.status == 3)
            {
                this.Start();
            }
        }

        /// <summary>
        /// Остановка выполнение задания
        /// </summary>
        public void Pause()
        {
            // Пауза
            if (this.status == 1)
            {
                this.status = 2;
                for (int i = 0; i < this.threads.Count; i++)
                {
                    this.doorways[i].Pause = true;
                    //this.threads[i].Suspend();
                }
            }
        }

        /// <summary>
        /// Остановка задания
        /// </summary>
        public void Stop()
        {
            this.status = 3;
            if (this.mainTimer != null)
            {
                this.mainTimer.Dispose();
            }

            // Закругление потоков
            for (int i = 0; i < this.threads.Count; i++)
            {
                this.threads[i].Abort();
            }

            // Очистка памяти
            for (int i = 0; i < this.doorways.Count; i++)
            {
                this.doorways[i].ClearMemory();
            }
        }

        private void UpdateWorkingStatus()
        {
            this.workingStatus = 0;
            int pagesTotal = 0;
            int pagesDone = 0;

            for (int i = 0; i < this.doorways.Count; i++)
            {
                pagesTotal += this.doorways[i].PagesCount;
                pagesDone += this.doorways[i].PagesDone;
            }

            if (pagesTotal == 0)
            {
                this.workingStatus = 0;
            }
            else
            {
                this.workingStatus = (int)(((float)pagesDone / (float)pagesTotal) * 100);
            }

            if (this.workingStatus >= 100)
            {
                this.workingStatus = 99;
            }
            if (this.workingStatus < 0)
            {
                this.workingStatus = 0;
            }
        }
        #endregion

        public class StatisticData
        {
            private bool ok;

            /// <summary>
            /// Статистика существует
            /// </summary>
            public bool Ok
            {
                get
                {
                    return this.ok;
                }

                set
                {
                    this.ok = value;
                }
            }
            private int dd;

            /// <summary>
            /// Количество созданных дорвеев
            /// </summary>
            public int DD
            {
                get
                {
                    return this.dd;
                }

                set
                {
                    this.dd = value;
                }
            }

            private int pd;

            /// <summary>
            /// Количество сгенерированных страниц.
            /// </summary>
            public int PD
            {
                get
                {
                    return this.pd;
                }

                set
                {
                    this.pd = value;
                }
            }
        }
    }
}
