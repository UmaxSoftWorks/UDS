using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Doorway_Studio.Classes;
using Doorway_Studio.Helpers;
using Doorway_Studio.Images;
using Settings;
using TextGenerator;

namespace Doorway_Studio
{
    class Doorway
    {
        private Random random { get; set; }
        public Doorway(int WSIndex, int Index)
        {
            this.wsIndex = WSIndex;
            this.index = Index;

            this.Pages = new List<Page>();
            this.done = new List<bool>();

            this.fileMacroses = new List<FileMacrossData>();
            this.categories = new List<string>();

            this.doorwayFolder = string.Empty;

            this.Log = new StringBuilder(1000);
            this.Spam = new StringBuilder(1000);

            this.random = new Random(Environment.TickCount);

            this.Tags = new List<string>();
            this.textTokensReplacer = new TextTokensReplacer();
        }

        #region Data
        private int wsIndex;
        private int index;

        private PresetSettings settings;

        /// <summary>
        /// FTP uploading
        /// </summary>
        private List<bool> done;
        /// <summary>
        /// FTP uploading
        /// </summary>
        private List<Thread> threads;

        private int templateID;
        private int textID;

        //Только приватные данные
        private List<FileMacrossData> fileMacroses;
        private int templateIndex;
        private int textIndex;
        private int keywordsIndex;
        private int synonymsIndex;
        private int mergeIndex;
        private int categoriesDCIndex;
        private List<string> categories;

        private List<string> keywords;
        private string doorwayFolder;

        // Генератор текста
        private TextGenerator.TextGenerator TG;

        private ImageRepository imageRepository;
        private TextTokensReplacer textTokensReplacer;
        #endregion

        #region Properties
        /// <summary>
        /// Pages Count
        /// </summary>
        public int PagesCount { get; set; }

        /// <summary>
        /// Pause
        /// </summary>
        public bool Pause { get; set; }

        /// <summary>
        /// Страницы
        /// </summary>
        public List<Page> Pages { get; set; }

        /// <summary>
        /// Готовность выгрузки страниц по фтп, в фоне
        /// </summary>
        public List<bool> Done
        {
            set
            {
                this.done = value;
            }
            get
            {
                return this.done;
            }
        }

        public PresetSettings Settings
        {
            set
            {
                this.settings = value;
            }
            get
            {
                return this.settings;
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
        /// Log
        /// </summary>
        public StringBuilder Log { get; set; }

        /// <summary>
        /// Spam Links
        /// </summary>
        public StringBuilder Spam { get; set; }
        public bool SpamSaved { get; set; }

        /// <summary>
        /// Количество сгенерированных страниц
        /// </summary>
        public int PagesDone { get; set; }

        public int UseKeyWordsStartWith { get; set; }
        public int UseKeyWordsEndWith { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Подготовка
        /// </summary>
        public int Prepare()
        {
            try
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000416") + "\r\n");

                // Проверка, существующих файлов и изменение настроек
                // Шаблон
                templateIndex = SharedData.WorkSpaces[wsIndex].GetTemplateIndex(this.templateID);
                if (templateIndex == -1)
                {
                    this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000417") + ".\r\n");
                    return 1;
                }

                // Текст
                textIndex = SharedData.WorkSpaces[wsIndex].GetTextIndex(this.textID);
                if (textIndex == -1)
                {
                    this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000418") + ".\r\n");
                    return 1;
                }

                // Кейворды
                keywordsIndex = SharedData.WorkSpaces[wsIndex].GetKeywordIndex(this.settings.KeywordsID);
                if (keywordsIndex == -1)
                {
                    this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000419") + ".\r\n");
                    return 1;
                }

                // Синонимы
                if (this.settings.KeywordsSynonyms)
                {
                    synonymsIndex = SharedData.WorkSpaces[wsIndex].GetKeywordIndex(this.settings.KeywordsSynonymsID);
                    if (synonymsIndex == -1)
                    {
                        this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000420") + ".\r\n");
                        this.settings.KeywordsSynonyms = false;
                    }
                }
                // Склеиватели
                if (this.settings.KeywordsMerge)
                {
                    mergeIndex = SharedData.WorkSpaces[wsIndex].GetKeywordIndex(this.settings.KeywordsMergeID);
                    if (mergeIndex == -1)
                    {
                        this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000421") + ".\r\n");
                        this.settings.KeywordsMerge = false;
                    }
                }

                // Распределители по категориям
                if (this.settings.Categories && this.settings.CategoriesType == 0 && this.settings.CategoriesDistribute == 1)
                {
                    categoriesDCIndex = SharedData.WorkSpaces[wsIndex].GetKeywordIndex(this.settings.CategoriesDistributeContainsID);
                    if (categoriesDCIndex == -1)
                    {
                        this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000422") + ".\r\n");
                        this.settings.CategoriesDistribute = 0;
                    }
                }

                // Файловые макросы
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000423") + "\r\n");
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000491") + "\r\n");

                this.LoadFileMacrosesContent();

                // Tags
                this.LoadTags();
                
                // HTML SiteMap
                if (this.settings.SiteMapType != 0)
                {
                    //Поиск, есть ли файл для хтмл карты сайта
                    if (SharedData.WorkSpaces[this.wsIndex].Templates[this.templateIndex].Map.Count == 0 && (this.settings.SiteMapType < 3))
                    {
                        this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000424") + "\r\n");
                        this.settings.SiteMapType = 0;
                    }
                }

                // RSS
                if (this.settings.GeneralDoorwayUrls.Length <= index)
                {
                    this.settings.RSS = false;
                }
                //Выбор кейвордов
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000425") + "\r\n");
                SelectKeywords();
                //Создание папки
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000426") + "\r\n");
                MakeFolders();
                //Создание страниц
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000431") + "\r\n");
                MakePagesNamesAndFields();
                //Spam
                if (this.settings.Spam)
                {
                    MakeSpam();
                }
                return 0;
            }
            catch (Exception ex)
            {
                // Ошибка
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000435") + ex.Message.Replace("\r\n", " ") + "\r\nStackTrace: " + ex.StackTrace.Replace("\r\n", " ") + "\r\nTargetSite: " + ex.TargetSite.Name.Replace("\r\n", " ") + ".\r\n");
                return 1;
            }
        }

        private List<string> Tags { get; set; }

        private void LoadTags()
        {
            if (this.index < this.Settings.TagSettings.Length)
            {
                if (!File.Exists(this.Settings.TagSettings[this.index].File))
                {
                    return;
                }

                this.Tags = File.ReadAllLines(this.Settings.TagSettings[this.index].File, (this.Settings.TagSettings[this.index].EncodingType == 0) ? Encoding.Default : Encoding.UTF8).ToList();
            }
        }

        /// <summary>
        /// Старт
        /// </summary>
        /// <returns>0 - все ок; 1 - критическая ошибка</returns>
        public int Start()
        {
            try
            {
                this.PagesCount = this.Pages.Count;

                // Копирование файлов шаблонов
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000427") + "\r\n");
                this.CopyTemplateFiles();

                // Копирование/генерирование/парсинг картинок
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000428") + "\r\n");
                imageRepository = new ImageRepository(new ImageRepositoryContext()
                                                          {
                                                              Log = Log,
                                                              Random = this.random,
                                                              Settings = this.settings,
                                                              SiteDirectory = doorwayFolder,
                                                              SiteIndex = index,
                                                              TemplateIndex = templateIndex,
                                                              WorkSpaceIndex = wsIndex
                                                          });
                imageRepository.MakeImages();

                // Создание доп файлов (php)
                if (this.settings.PagesDoorwayType != 0)
                {
                    this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000429") + "\r\n");
                    this.MakePHPFile();
                }

                // robots.txt
                if (this.settings.Robots)
                {
                    this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000430") + "\r\n");
                    this.MakeRobots();
                }

                // Создание генератора текста
                this.MakeTextGenerator();

                // Генерирование страниц
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000433") + "\r\n");
                this.MakePages();

                // RSS
                if (this.settings.RSS)
                {
                    this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000434") + "\r\n");
                    this.MakeRSS();
                }

                // Remove unused images
                imageRepository.DeleteUnusedImages();
                return 0;
            }
            catch (Exception ex)
            {
                // Error
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000435") + ex.Message.Replace("\r\n", " ") + "\r\nStackTrace: " + ex.StackTrace.Replace("\r\n", " ") + ".\r\n");
                return 1;
            }
        }

        /// <summary>
        /// Prepare TextGenerator to use
        /// </summary>
        private void MakeTextGenerator()
        {
            switch (this.settings.TextGenration)
            {
                case 0:
                    {
                        this.TG = new TGMarkovClassic(this.settings);
                        break;
                    }
                case 1:
                    {
                        this.TG = new TGMarkov(this.settings);
                        break;
                    }
                case 2:
                    {
                        this.TG = new TGConceptualGraph(this.settings);
                        break;
                    }
                case 3:
                    {
                        this.TG = new TGBigram(this.settings);
                        break;
                    }
                case 4:
                    {
                        this.TG = new TGTrigram(this.settings);
                        break;
                    }
                case 5:
                    {
                        this.TG = new TGMix(this.settings);
                        break;
                    }
                case 6:
                    {
                        this.TG = new TGRandom(this.settings);
                        break;
                    }
                case 7:
                    {
                        this.TG = new TGCopyPaste(this.settings);
                        break;
                    }
                case 8:
                    {
                        this.TG = new TGLineByLineCopyPaste(this.settings);
                        break;
                    }
                case 9:
                    {
                        this.TG = new TGCommaConnection(this.settings);
                        break;
                    }
                case 10:
                    {
                        this.TG = new TGRandomCommaConnection(this.settings);
                        break;
                    }
                case 11:
                    {
                        this.TG = new TGReverse(this.settings);
                        break;
                    }
                case 12:
                    {
                        this.TG = new TGLineByLineReverse(this.settings);
                        break;
                    }
                case 13:
                    {
                        this.TG = new TGAdvancedReverse(this.settings);
                        break;
                    }
                case 14:
                    {
                        this.TG = new TGLineByLineAdvancedReverse(this.settings);
                        break;
                    }
                case 15:
                    {
                        this.TG = new TGBlockCopyPaste(this.settings);
                        break;
                    }
                case 16:
                    {
                        this.TG = new TGBlockReverse(this.settings);
                        break;
                    }
                case 17:
                    {
                        this.TG = new TGBlockAdvancedReverse(this.settings);
                        break;
                    }
                default:
                    {
                        this.TG = new TGMarkovClassic(this.settings);
                        break;
                    }
            }

            // Загрузка текста
            this.TG.Load(SharedData.WorkSpaces[wsIndex].Texts[textIndex].Texts);
        }

        private void SelectKeywords()
        {
            //Выборка используемых кейвордов
            this.keywords = new List<string>();

            //Random random = new Random(DateTime.Now.Millisecond);

            int selectNum = random.Next(this.settings.KeywordsSelectMin, this.settings.KeywordsSelectMax);

            switch (this.settings.KeywordsSelectType)
            {
                case 0:
                    {
                        this.keywords.AddRange(SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items);
                        break;
                    }
                case 1:
                    {
                        if (selectNum > SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items.Length)
                        {
                            selectNum = SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items.Length;
                        }

                        for (int i = 0; i < selectNum; i++)
                        {
                            this.keywords.Add(SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items[i]);
                        }
                        break;
                    }
                case 2:
                    {
                        if (selectNum > SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items.Length)
                        {
                            selectNum = SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items.Length;
                        }

                        for (int i = SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items.Length - selectNum; i < SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items.Length; i++)
                        {
                            this.keywords.Add(SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items[i]);
                        }
                        break;
                    }
                case 3:
                    {
                        if (selectNum > SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items.Length)
                        {
                            selectNum = SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items.Length;
                        }
                        List<bool> used = new List<bool>(SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items.Length);
                        for (int k = 0; k < SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items.Length; k++)
                        {
                            used.Add(false);
                        }

                        int i = 0;
                        int selectedIndex = 0;
                        while (i < selectNum)
                        {
                            selectedIndex = random.Next(SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items.Length);
                            if (!used[selectedIndex])
                            {
                                this.keywords.Add(SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items[selectedIndex]);
                                used[selectedIndex] = true;
                                i++;
                            }
                        }

                        break;
                    }
                case 4:
                    {
                        while (UseKeyWordsStartWith >= SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items.Length)
                        {
                            UseKeyWordsStartWith -= SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items.Length;
                            UseKeyWordsEndWith -= SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items.Length;
                        }

                        int keywordsCount = UseKeyWordsEndWith - UseKeyWordsStartWith;

                        while (this.keywords.Count < keywordsCount)
                        {
                            if (UseKeyWordsStartWith >= SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items.Length)
                            {
                                UseKeyWordsStartWith = 0;
                            }

                            this.keywords.Add(SharedData.WorkSpaces[wsIndex].Keywords[keywordsIndex].Items[UseKeyWordsStartWith]);
                            UseKeyWordsStartWith++;
                        }
                        break;
                    }
            }

            // Перемешивание кеев
            if (this.settings.KeywordsReorder)
            {
                for (int i = 0; i < this.keywords.Count - 1; i++)
                {
                    int selectedKeyword = random.Next(this.keywords.Count);

                    string tempKeyword = this.keywords[i];
                    this.keywords[i] = this.keywords[selectedKeyword];
                    this.keywords[selectedKeyword] = tempKeyword;
                }
            }
            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000436") + this.keywords.Count + View.UILanguageResources.GetString("S0000437") + ".\r\n");
        }

        /// <summary>
        /// Создание папки для дорвея
        /// </summary>
        private void MakeFolders()
        {
            // Подготовка папок
            if (!this.settings.GeneralSaveTo.EndsWith("\\"))
            {
                this.settings.GeneralSaveTo += "\\";
            }

            // Создание подпапок
            if (this.settings.GeneralCreateSubFolders)
            {
                switch (this.settings.GeneralSubFoldersType)
                {
                    case 0:
                        {
                            this.doorwayFolder = this.settings.GeneralSaveTo + this.keywords[0];
                            break;
                        }
                    case 1:
                        {
                            this.doorwayFolder = this.settings.GeneralSaveTo + (this.index + 1).ToString();
                            break;
                        }
                    case 2:
                        {
                            this.doorwayFolder = this.settings.GeneralSaveTo + this.keywords[0] + " " + (this.index + 1).ToString();
                            break;
                        }
                    case 3:
                        {
                            if (this.settings.GeneralDoorwayUrls.Length > this.index)
                            {
                                this.doorwayFolder = this.settings.GeneralSaveTo + this.settings.GeneralDoorwayUrls[this.index].Replace("http://", string.Empty).Replace(":", string.Empty).Replace("/", string.Empty);
                            }
                            else
                            {
                                this.doorwayFolder = this.settings.GeneralSaveTo + this.keywords[0] + " " + (this.index + 1).ToString();
                            }
                            break;
                        }
                }

                if (!this.doorwayFolder.EndsWith("\\"))
                {
                    this.doorwayFolder += "\\";
                }

                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000438") + this.doorwayFolder + "\r\n");

                // Удаление папки
                try
                {
                    if (Directory.Exists(doorwayFolder) && MainSettings.ClearFolderWhereDoorwaysMastBeSaved)
                    {
                        this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000438") + "\r\n");
                        Directory.Delete(doorwayFolder, true);
                    }
                }
                catch (Exception)
                {
                }

                // Handle situation, when another tasks created the same folder
                lock (IOHelper.LockedDirectories)
                {
                    if (Directory.Exists(this.doorwayFolder))
                    {
                        this.doorwayFolder = this.doorwayFolder.Trim('\\');

                        bool isNumber = false;
                        int iterator = 1;

                        int numberValue = 0;
                        try
                        {
                            string directoryName = this.doorwayFolder.Substring(this.doorwayFolder.LastIndexOf("\\") + 1);
                            isNumber = int.TryParse(directoryName, out numberValue);
                        }
                        catch (Exception)
                        {
                        }

                        if (isNumber)
                        {
                            iterator = numberValue;
                            this.doorwayFolder = this.doorwayFolder.Substring(0, this.doorwayFolder.LastIndexOf("\\") + 1);
                        }

                        while (true)
                        {
                            string folder = isNumber ? (this.doorwayFolder + iterator.ToString()) : (this.doorwayFolder + " " + iterator.ToString());

                            if (Directory.Exists(folder) || IOHelper.LockedDirectories.Contains(folder))
                            {
                                iterator++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        this.doorwayFolder = isNumber ? (this.doorwayFolder + iterator.ToString()) : (this.doorwayFolder + " " + iterator.ToString()) + "\\";
                    }

                    IOHelper.LockedDirectories.Add(this.doorwayFolder);

                    Directory.CreateDirectory(this.doorwayFolder);
                    File.SetAttributes(this.doorwayFolder, FileAttributes.Normal);
                    this.doorwayFolder.SetFileDirectoryDate(random, settings);

                    IOHelper.LockedDirectories.Remove(this.doorwayFolder);
                }
            }
            else
            {
                this.doorwayFolder = this.settings.GeneralSaveTo;
            }
        }

        /// <summary>
        /// Копирование файлов шаблонов
        /// </summary>
        private void CopyTemplateFiles()
        {
            // Генерирование имени папки, где лежат файлы
            string templateFolder = string.Empty;
            string temp = SharedData.WorkSpaces[this.wsIndex].ID.ToString();
            while (temp.Length < 7)
            {
                temp = "0" + temp;
            }

            templateFolder = System.Windows.Forms.Application.StartupPath + "\\Data\\" + temp + "\\Templates\\";
            temp = SharedData.WorkSpaces[this.wsIndex].Templates[this.templateIndex].ID.ToString();
            while (temp.Length < 7)
            {
                temp = "0" + temp;
            }

            templateFolder += temp + "\\Files\\";
            //this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000427") + "\r\n");
            for (int i = 0; i < SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Files.Count; i++)
            {
                try
                {
                    // Создание подпапок если нужно
                    if (SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Files[i].Substring(templateFolder.Length).Contains("\\"))
                    {
                        string fileFolder = doorwayFolder;
                        string tempFolder = SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Files[i].Substring(templateFolder.Length);
                        while (tempFolder.Contains("\\"))
                        {
                            string directory = Path.Combine(fileFolder, tempFolder.Substring(0, tempFolder.IndexOf("\\")));
                            Directory.CreateDirectory(directory);
                            File.SetAttributes(directory, FileAttributes.Normal);
                            IOHelper.SetFileDirectoryDate(directory, random, this.settings);
                            fileFolder = Path.Combine(fileFolder, tempFolder.Substring(0, tempFolder.IndexOf("\\")));
                            tempFolder = tempFolder.Substring(0 + tempFolder.IndexOf("\\") + 1);
                        }
                    }

                    // Копирование файла
                    string filename = Path.Combine(doorwayFolder, SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Files[i].Substring(templateFolder.Length));
                    File.Copy(SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Files[i], filename);
                    IOHelper.SetFileDirectoryDate(filename, random, this.settings);
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// Make Spam content
        /// </summary>
        private void MakeSpam()
        {
            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000432") + "\r\n");
            for (int i = 0; i < this.settings.SpamUrlTypeList.Length; i++)
            {
                for (int k = 0; k < this.Pages.Count; k++)
                {
                    if (!this.settings.StaticPagesIncludeIntoSiteMap && this.Pages[k].Type == 2)
                    {
                        continue;
                    }

                    if (textTokensReplacer.Context == null)
                    {
                        textTokensReplacer.Context = GetTextTokensReplacerContext();
                    }

                    string content = this.textTokensReplacer.ReplaceRandomTextTokens(this.settings.SpamUrlTypeList[i].Replace("[LINK]", this.Pages[k].URL));

                    content = this.textTokensReplacer.ReplaceRandomTextFileTokens(content);
                    content = this.textTokensReplacer.ReplaceFullFileContentTextTokens(content);

                    content = content.Replace("[KEY]", (this.Pages[k].Keywords.Count > 0) ? this.Pages[k].Keywords[0] : string.Empty) + "\r\n";

                    this.Spam.Append(content);
                }

                this.Spam.Append("\r\n\r\n\r\n\r\n");
            }
        }
        
        /// <summary>
        /// Создание списка категорий
        /// </summary>
        private void MakeCategories()
        {
            if (this.settings.Categories)
            {
                if (this.settings.CategoriesType == 0)
                {
                    // Dynamic
                    if (this.settings.CategoriesDistribute == 0)
                    {
                        int categoriesCount = this.random.Next(this.settings.CategoriesDynamicMin, this.settings.CategoriesDynamicMax);
                        if (this.keywords.Count <= categoriesCount)
                        {
                            categoriesCount = this.keywords.Count;
                        }

                        for (int i = 0; i < categoriesCount; i++)
                        {
                            int keywordIndex = this.random.Next(this.keywords.Count);
                            this.categories.Add(this.keywords[keywordIndex]);
                        }
                    }
                    else
                    {
                        this.categories = SharedData.WorkSpaces[this.wsIndex].Keywords[this.categoriesDCIndex].Items.ToList();
                    }

                    // Removing already used keywords
                    if (this.settings.CategoriesDynamicExcludeKeywords)
                    {
                        for (int i = 0; i < this.categories.Count; i++)
                        {
                            for (int k = 0; k < this.keywords.Count; k++)
                            {
                                if (this.categories[i].ToLower() == this.keywords[k].ToLower())
                                {
                                    this.keywords.RemoveAt(k);
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Static
                    if (this.settings.CategoriesStaticList.Length == 0)
                    {
                        this.settings.Categories = false;
                        return;
                    }

                    if (this.settings.CategoriesDynamicMin == 0 && this.settings.CategoriesDynamicMax == 0)
                    {
                        this.categories = this.settings.CategoriesStaticList.ToList();
                    }
                    else
                    {
                        // If categories count less than minimum
                        if (this.settings.CategoriesStaticList.Length < this.settings.CategoriesDynamicMin)
                        {
                            this.categories = this.settings.CategoriesStaticList.ToList();
                        }
                        else
                        {
                            // If categories count less than required
                            if (this.settings.CategoriesStaticList.Length < this.index * this.settings.CategoriesDynamicMax)
                            {
                                int categoriesCount = random.Next(this.settings.CategoriesDynamicMin, this.settings.CategoriesDynamicMax);
                                for (int i = 0; i < categoriesCount; i++)
                                {
                                    this.categories.Add(this.settings.CategoriesStaticList[random.Next(this.settings.CategoriesStaticList.Length)]);
                                }
                            }
                            else
                            {
                                int categoriesCount = random.Next(this.settings.CategoriesDynamicMin, this.settings.CategoriesDynamicMax);
                                if (this.settings.CategoriesStaticList.Length < categoriesCount)
                                {
                                    categoriesCount = this.settings.CategoriesStaticList.Length;
                                }

                                for (int i = this.index*this.settings.CategoriesDynamicMax; i < (this.index*this.settings.CategoriesDynamicMax + categoriesCount); i++)
                                {
                                    this.categories.Add(this.settings.CategoriesStaticList[i]);
                                }
                            }
                        }
                    }
                }

                // Cleaning
                for (int i = 0; i < this.categories.Count; i++)
                {
                    this.categories[i] = this.categories[i].Replace("\\", " ").Replace("/", " ").Replace("\t", "").Replace("\n", "").Replace("\r", "");
                }
                //Cleaning for dupes
                try
                {
                    for (int i = 0; i < this.categories.Count - 1; i++)
                    {
                        for (int k = i + 1; k < this.categories.Count; k++)
                        {
                            if (this.categories[i] == this.categories[k])
                            {
                                this.categories.RemoveAt(k);
                                k--;
                            }
                        }
                    }
                }
                catch (Exception) { }
            }
            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000448") + this.categories.Count.ToString() + View.UILanguageResources.GetString("S0000449") + ".\r\n");
        }
        
        //Преобразование кейвордов
        private void MakeKeywords()
        {
            //Чистка кейвордов
            for (int i = 0; i < this.keywords.Count; i++)
            {
                this.keywords[i] = this.keywords[i].Replace(".", " ").Replace(",", " ").Replace("\\", " ").Replace("/", " "); ;
                while (this.keywords[i].Contains("  "))
                {
                    this.keywords[i] = this.keywords[i].Replace("  ", " ");
                }
                this.keywords[i] = this.keywords[i].Trim();
            }
            if (this.settings.KeywordsMerge)
            {
                switch (this.settings.KeywordsMergeType)
                {
                    case 0:
                        {
                            //1:1
                            for (int i = 0; i < this.keywords.Count; i++)
                            {
                                if (SharedData.WorkSpaces[wsIndex].Keywords[mergeIndex].Items.Length < i)
                                {
                                    this.keywords[i] += " " + SharedData.WorkSpaces[wsIndex].Keywords[mergeIndex].Items[i];
                                }
                                else
                                {
                                    break;
                                }
                            }
                            break;
                        }
                    case 1:
                    case 2:
                        {
                            //1:N
                            //1:All
                            List<string> newKeywords = new List<string>();

                            for (int i = 0; i < this.keywords.Count; i++)
                            {
                                if (SharedData.WorkSpaces[wsIndex].Keywords[mergeIndex].Items.Length < i)
                                {
                                    int mergeKeywordsCount = 0;

                                    if (this.settings.KeywordsMergeType == 1)
                                    {
                                        //1:N
                                        mergeKeywordsCount = random.Next(this.settings.KeywordsMergeMin, this.settings.KeywordsMergeMax);
                                    }
                                    else
                                    {
                                        //1:All
                                        mergeKeywordsCount = SharedData.WorkSpaces[wsIndex].Keywords[mergeIndex].Items.Length;
                                    }

                                    for (int k = 0; k < mergeKeywordsCount; k++)
                                    {
                                        if (this.settings.KeywordsMergeType == 1)
                                        {
                                            //1:N
                                            newKeywords.Add(this.keywords[i] + " " + SharedData.WorkSpaces[wsIndex].Keywords[mergeIndex].Items[random.Next(SharedData.WorkSpaces[wsIndex].Keywords[mergeIndex].Items.Length)]);
                                        }
                                        else
                                        {
                                            //1:All
                                            newKeywords.Add(this.keywords[i] + " " + SharedData.WorkSpaces[wsIndex].Keywords[mergeIndex].Items[k]);
                                        }
                                    }
                                }
                                else
                                {
                                    newKeywords.Add(this.keywords[i]);
                                }
                            }
                            this.keywords = newKeywords;
                            newKeywords = new List<string>();
                            break;
                        }
                }
            }
            //Чистка кейвордов от мусора
            for (int i = 0; i < this.keywords.Count; i++)
            {
                this.keywords[i] = this.keywords[i].Replace("\t", "").Replace("\n", "").Replace("\r", "");
                while (this.keywords[i].Contains("  "))
                {
                    this.keywords[i] = this.keywords[i].Replace("  ", " ");
                }
                this.keywords[i] = this.keywords[i].Trim();
            }
        }

        private int fileNameContinueIndexNum;
        private int fileNameContinueCategoryNum;
        private int fileNameSiteMapNum;
        private int fileNamePageNum;
        private int fileNameCustomPageNum;

        /// <summary>
        /// Создание имени файла
        /// </summary>
        /// <param name="PageIndex">Индекс страницы</param>
        /// <returns></returns>
        private string MakeFileName(int PageIndex)
        {
            string fileName = string.Empty;
            switch (this.settings.PagesDoorwayType)
            {
                // Static
                case 0:
                    {
                        switch (this.Pages[PageIndex].Type)
                        {
                                //Index
                            case 0:
                                //Index Continue
                            case 6:
                                {
                                    if (this.Pages[PageIndex].Type == 0)
                                    {
                                        //Index
                                        fileName = "index";
                                    }
                                    else
                                    {
                                        //Index Continue
                                        fileName = this.settings.PagesStaticIndexContinuesNames.Replace("[Name]", "index").Replace("[N]", (fileNameContinueIndexNum + 1).ToString());
                                        fileNameContinueIndexNum++;
                                    }
                                    break;
                                }

                                // Page
                            case 1:
                                {
                                    fileName = this.settings.PagesStaticPageNames;
                                    switch (this.settings.PagesStaticNamesTypes)
                                    {
                                        //Keyword
                                        case 0:
                                        //Keyword (translit)
                                        case 1:
                                            {
                                                string temp = string.Empty;
                                                for (int i = 0; i < this.Pages[PageIndex].Keywords.Count; i++)
                                                {
                                                    temp += this.Pages[PageIndex].Keywords[i] + " ";
                                                }
                                                while (temp.EndsWith(" "))
                                                {
                                                    temp = temp.Substring(0, temp.Length - 1);
                                                }
                                                if (this.settings.PagesStaticNamesTypes == 1)
                                                {
                                                    temp = temp.Translit();
                                                }
                                                fileName = fileName.Replace("[Name]", temp.Replace(" ", this.settings.PagesStaticSeparator));
                                                break;
                                            }
                                        //Number (global)
                                        case 2:
                                        //Number (local)
                                        case 4:
                                        //Other
                                        case 3:
                                            {
                                                if (this.settings.PagesStaticNamesTypes == 2)
                                                {
                                                    fileName = fileName.Replace("[Name]", (fileNamePageNum + 1).ToString());
                                                }
                                                else
                                                {
                                                    fileName = fileName.Replace("[Name]", string.Empty);
                                                }
                                                break;
                                            }
                                    }
                                    //Number (local)
                                    if (this.settings.PagesStaticNamesTypes == 4 && this.settings.Categories && this.categories.Count > 0)
                                    {
                                        //Looking for Pages in current category
                                        int pageCount = 0;
                                        for (int i = 0; i < PageIndex; i++)
                                        {
                                            if (this.Pages[i].Type == 1 && this.Pages[i].Category == this.Pages[PageIndex].Category)
                                            {
                                                pageCount++;
                                            }
                                        }
                                        fileName = fileName.Replace("[N]", (pageCount + 1).ToString());
                                    }

                                    // Number (global)
                                    else
                                    {
                                        fileName = fileName.Replace("[N]", (fileNamePageNum + 1).ToString());
                                        fileNamePageNum++;
                                    }

                                    // Замена имени категории
                                    if (this.settings.Categories)
                                    {
                                        if (this.Pages[PageIndex].Category != -1)
                                        {
                                            string categoryName = this.settings.PagesStaticCategoriesNames;
                                            switch (this.settings.PagesStaticNamesTypes)
                                            {
                                                    //Keyword
                                                case 0:
                                                    //Keyword(translit)
                                                case 1:
                                                    {
                                                        categoryName = categoryName.Replace("[Name]",
                                                                                            this.settings.PagesStaticNamesTypes == 0
                                                                                                ? this.categories[this.Pages[PageIndex].Category]
                                                                                                : this.categories[this.Pages[PageIndex].Category].Translit());
                                                        break;
                                                    }
                                                //Number (global)
                                                case 2:
                                                //Number (local)
                                                case 4:
                                                    //Other
                                                case 3:
                                                    {
                                                        categoryName = categoryName.Replace("[Name]", (this.Pages[PageIndex].Category + 1).ToString());
                                                        break;
                                                    }
                                            }
                                            categoryName = categoryName.Replace("[N]", (this.Pages[PageIndex].Category + 1).ToString());

                                            if (categoryName.EndsWith("index") && categoryName != "index")
                                            {
                                                categoryName = categoryName.Substring(0, categoryName.LastIndexOf("index"));
                                            }

                                            fileName = fileName.Replace("[CName]", categoryName);
                                        }
                                        else
                                        {
                                            fileName = fileName.Replace("[CName]", string.Empty);
                                        }
                                    }
                                    else
                                    {
                                        fileName = fileName.Replace("[CName]", string.Empty);
                                    }

                                    // Random number
                                    while (fileName.Contains("[RN]"))
                                    {
                                        int startIndex = fileName.IndexOf("[RN]");
                                        fileName = fileName.Remove(startIndex, 4);
                                        fileName = fileName.Insert(startIndex, this.random.Next(0, 100).ToString());
                                    }

                                    break;
                                }

                            // Static page
                            case 2:
                                {
                                    switch (this.settings.PagesStaticNamesTypes)
                                    {
                                        case 0:
                                            {
                                                // Keyword
                                                fileName = this.Pages[PageIndex].Keywords[0];
                                                break;
                                            }

                                        case 1:
                                            {
                                                // Keyword (translit)
                                                fileName = this.Pages[PageIndex].Keywords[0].Translit();
                                                break;
                                            }

                                        default:
                                            {
                                                // Number
                                                // Other
                                                fileName = this.Pages[PageIndex].Keywords[0].Translit();
                                                break;
                                            }
                                    }

                                    // Random number
                                    while (fileName.Contains("[RN]"))
                                    {
                                        int startIndex = fileName.IndexOf("[RN]");
                                        fileName = fileName.Remove(startIndex, 4);
                                        fileName = fileName.Insert(startIndex, random.Next(0, 100).ToString());
                                    }
                                    break;
                                }
                                // Category
                            case 3:
                                // Category Continue
                            case 5:
                                {
                                    if (this.Pages[PageIndex].Type == 3)
                                    {
                                        fileName = this.settings.PagesStaticCategoriesNames;
                                    }
                                    else
                                    {
                                        fileName = this.settings.PagesStaticCategoriesContinuesNames.Replace("[N]", (fileNameContinueCategoryNum + 1).ToString());
                                        fileNameContinueCategoryNum++;
                                    }
                                    switch (this.settings.PagesStaticNamesTypes)
                                    {
                                        //Keyword
                                        case 0:
                                        //Keyword (translit)
                                        case 1:
                                            {
                                                fileName = fileName.Replace("[Name]",
                                                                            this.settings.PagesStaticNamesTypes == 0
                                                                                ? this.categories[this.Pages[PageIndex].Category]
                                                                                : this.categories[this.Pages[PageIndex].Category].Translit());

                                                if (this.Pages[PageIndex].Type == 2)
                                                {
                                                    fileName = fileName.Replace("[N]", string.Empty);
                                                }
                                                else
                                                {
                                                    fileName = fileName.Replace("[N]", (this.Pages[PageIndex].Category + 1).ToString());
                                                }
                                                break;
                                            }
                                        //Number (global)
                                        case 2:
                                        //Number (local)
                                        case 4:
                                        //Other
                                        case 3:
                                            {
                                                fileName = fileName.Replace("[Name]", (this.Pages[PageIndex].Category + 1).ToString());
                                                fileName = fileName.Replace("[N]", (this.Pages[PageIndex].Category + 1).ToString());
                                                break;
                                            }
                                    }
                                    break;
                                }
                                //SiteMap
                            case 4:
                                {
                                    fileName = this.settings.SiteMapHTMLName.Replace("[N]", (fileNameSiteMapNum + 1).ToString());
                                    fileNameSiteMapNum++;
                                    break;
                                }
                                //Custom page
                            case 7:
                                {
                                    fileName = SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages[fileNameCustomPageNum].CustomName;

                                    // Random number
                                    while (fileName.Contains("[RN]"))
                                    {
                                        int startIndex = fileName.IndexOf("[RN]");
                                        fileName = fileName.Remove(startIndex, 4);
                                        fileName = fileName.Insert(startIndex, random.Next(0, 100).ToString());
                                    }
                                    fileNameCustomPageNum++;
                                    break;
                                }
                        }

                        // Replace RWORD, RBWORD etc
                        if (textTokensReplacer.Context == null)
                        {
                            textTokensReplacer.Context = GetTextTokensReplacerContext();
                        }

                        fileName = this.settings.PagesStaticNamesTypes == 0
                                       ? textTokensReplacer.ReplaceRandomTextTokens(fileName)
                                       : textTokensReplacer.ReplaceRandomTextTokens(fileName).Translit();

                        // Replace random letters tokens
                        fileName = this.settings.PagesStaticNamesTypes == 0
                                       ? textTokensReplacer.ReplaceRandomLettersTokens(fileName)
                                       : textTokensReplacer.ReplaceRandomLettersTokens(fileName).Translit();

                        fileName = fileName.Replace(" ", this.settings.PagesStaticSeparator);
                        while (fileName.EndsWith("-"))
                        {
                            fileName = fileName.Substring(0, fileName.Length - 1);
                        }

                        //Уборка "плохих" символов
                        fileName = fileName.Replace("'", string.Empty).Replace("\"", string.Empty)
                            .Replace("?", string.Empty).Replace("*", string.Empty).Replace("<", string.Empty)
                            .Replace(">", string.Empty).Replace("|", string.Empty).Replace("№", string.Empty)
                            .Replace("«", string.Empty).Replace("»", string.Empty).Replace("&", string.Empty)
                            .Replace(":", string.Empty).Replace("#", string.Empty);

                        fileName = this.doorwayFolder + fileName;

                        fileName = fileName.Replace("/", "\\");
                        while (fileName.Contains("\\\\"))
                        {
                            fileName = fileName.Replace("\\\\", "\\");
                        }

                        // Tar
                        if (this.settings.GeneralArchive == 2 || this.settings.FTPUploadArchive == 2)
                        {
                            if ((fileName.Length - this.settings.PagesStaticExtension.Length) > 100)
                            {
                                fileName = fileName.Substring(0, 100 - this.settings.PagesStaticExtension.Length);
                            }
                        }

                        // Ограничение длины имени файла в 250 символов
                        if (fileName.Length >= 250)
                        {
                            fileName = fileName.Substring(0, 250 - this.settings.PagesStaticExtension.Length);
                        }
                        // Расширение
                        if (this.Pages[PageIndex].Type != 7)
                        {
                            fileName += this.settings.PagesStaticExtension;
                        }

                        break;
                    }

                // PHP + Text
                // PHP + SQLite
                case 1:
                case 2:
                    {
                        switch (this.Pages[PageIndex].Type)
                        {
                            case 0:
                                {
                                    fileName = "index";
                                    break;
                                }
                            case 1:
                                {
                                    fileName = this.settings.PagesDynamicPageNames + "-" + (fileNamePageNum + 1).ToString();
                                    fileNamePageNum++;
                                    break;
                                }
                            case 2:
                                {
                                    fileName = this.settings.PagesDynamicStaticPageNames + "-" + (PageIndex + 1).ToString();
                                    break;
                                }
                            case 3:
                                {
                                    fileName = this.settings.PagesDynamicCategoriesNames + "-" + this.Pages[PageIndex].Category.ToString();
                                    break;
                                }
                            case 4:
                                {
                                    fileName = "sitemap-" + this.Pages[PageIndex].Category.ToString() + "-" + (fileNameSiteMapNum + 1).ToString();
                                    fileNameSiteMapNum++;
                                    break;
                                }
                            case 5:
                                {
                                    fileName = this.settings.PagesDynamicCategoriesContinuesNames1 + "-" + this.Pages[PageIndex].Category.ToString() + this.settings.PagesDynamicCategoriesContinuesNames2 + "-" + (fileNameContinueCategoryNum + 1).ToString();
                                    fileNameContinueCategoryNum++;
                                    break;
                                }
                            case 6:
                                {
                                    fileName = this.settings.PagesDynamicIndexContinuesNames + "-" + (fileNameContinueIndexNum + 1).ToString();
                                    fileNameContinueIndexNum++;
                                    break;
                                }
                            case 7:
                                {
                                    fileName = SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages[fileNameCustomPageNum + 1].CustomName;
                                    fileNameCustomPageNum++;
                                    break;
                                }
                        }

                        if (this.settings.PagesDoorwayType == 1)
                        {
                            fileName = this.doorwayFolder + fileName + ".txt";
                        }

                        while (fileName.Contains("\\\\"))
                        {
                            fileName = fileName.Replace("\\\\", "\\");
                        }

                        break;
                    }
            }
            return fileName;
        }

        private void MakePagesNamesAndFields()
        {
            // Создание списка категорий
            if (this.settings.Categories)
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000450") + "\r\n");
                this.MakeCategories();
            }

            // Преобразование кейвордов
            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000451") + "\r\n");
            this.MakeKeywords();
            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000452") + "\r\n");

            // Создание индекса
            this.Pages.Add(new Page());
            this.Pages[0].Keywords.Add(this.keywords[0]);
            this.Pages[0].Category = -1;
            this.Pages[0].Name = this.MakeFileName(0);
            this.Pages[0].RelativeURL = string.IsNullOrEmpty(this.MakeRelativeURL(0)) ? "/" : this.MakeRelativeURL(0);
            this.Pages[0].URL = this.MakeAbsoluteUrl(0);
            this.Pages[0].Tag = this.MakeTag();

            // Статические страницы
            if (this.settings.StaticPages)
            {
                for (int i = 0; i < this.settings.StaticPagesList.Length; i++)
                {
                    this.Pages.Add(new Page());
                    this.Pages[this.Pages.Count - 1].Keywords.Add(this.settings.StaticPagesList[i]);
                    this.Pages[this.Pages.Count - 1].Type = 2;
                    this.Pages[this.Pages.Count - 1].Category = -1;
                    this.Pages[this.Pages.Count - 1].Name = this.MakeFileName(this.Pages.Count - 1);
                    this.Pages[this.Pages.Count - 1].RelativeURL = this.MakeRelativeURL(this.Pages.Count - 1);
                    this.Pages[this.Pages.Count - 1].URL = this.MakeAbsoluteUrl(this.Pages.Count - 1);
                }
            }

            // Категории
            if (this.settings.Categories)
            {
                // Категории
                for (int i = 0; i < this.categories.Count; i++)
                {
                    this.Pages.Add(new Page());
                    this.Pages[this.Pages.Count - 1].Type = 3;

                    // Кейворд
                    this.Pages[this.Pages.Count - 1].Keywords.Add(this.categories[i]);

                    // Категория
                    this.Pages[this.Pages.Count - 1].Category = i;

                    // Имя файла
                    this.Pages[this.Pages.Count - 1].Name = this.MakeFileName(this.Pages.Count - 1);

                    // URL
                    this.Pages[this.Pages.Count - 1].RelativeURL = this.MakeRelativeURL(this.Pages.Count - 1);
                    this.Pages[this.Pages.Count - 1].URL = this.MakeAbsoluteUrl(this.Pages.Count - 1);
                }
            }

            // Страницы
            int usedKeywords = 1;
            while (usedKeywords < this.keywords.Count)
            {
                int useKeywordsCount = 1;
                if (this.settings.TextGenrationKeywordsMoreThanOneOnPage)
                {
                    useKeywordsCount = random.Next(this.settings.TextGenrationKeywordsOnPageMin, this.settings.TextGenrationKeywordsOnPageMax);
                }
                //Добавление страницы
                this.Pages.Add(new Page());
                //Заполнение полей страницы
                //Кейворды
                for (int i = 0; i < useKeywordsCount; i++)
                {
                    this.Pages[this.Pages.Count - 1].Keywords.Add(this.keywords[usedKeywords + i]);
                }
                //Тип
                this.Pages[this.Pages.Count - 1].Type = 1;
                //Имя файла
                this.Pages[this.Pages.Count - 1].Category = GetCategory(this.Pages.Count - 1);
                //Имя файла
                this.Pages[this.Pages.Count - 1].Name = MakeFileName(this.Pages.Count - 1);
                //URL
                this.Pages[this.Pages.Count - 1].RelativeURL = this.MakeRelativeURL(this.Pages.Count - 1);
                this.Pages[this.Pages.Count - 1].URL = this.MakeAbsoluteUrl(this.Pages.Count - 1);

                // Tag
                this.Pages[this.Pages.Count - 1].Tag = this.MakeTag();

                usedKeywords += useKeywordsCount;
            }
            //Создание страниц категорий
            if (this.settings.Categories)
            {
                //Продолжение категорий
                if (this.settings.PagesStaticCategoriesContinues || this.settings.PagesDynamicCategoriesContinues)
                {
                    for (int i = 0; i < this.categories.Count; i++)
                    {
                        //Определение количества страниц для каждой категории
                        int pageCount = 0;
                        for (int k = 0; k < this.Pages.Count; k++)
                        {
                            if (this.Pages[k].Type == 1 && this.Pages[k].Category == i)
                            {
                                pageCount++;
                            }
                        }
                        switch (this.settings.PagesDoorwayType)
                        {
                            case 0:
                                {
                                    pageCount /= this.settings.PagesStaticKeysPerPageOnContinues;
                                    break;
                                }
                            case 1:
                                {
                                    pageCount /= this.settings.PagesDynamicKeysPerPageOnContinues;
                                    break;
                                }
                        }
                        pageCount--;

                        // Создание страниц
                        for (int k = 0; k < pageCount; k++)
                        {
                            this.Pages.Add(new Page());
                            this.Pages[this.Pages.Count - 1].Type = 5;

                            // Кейворд
                            this.Pages[this.Pages.Count - 1].Keywords.Add(this.categories[i]);

                            // Категория
                            this.Pages[this.Pages.Count - 1].Category = i;

                            // Имя файла
                            this.Pages[this.Pages.Count - 1].Name = this.MakeFileName(this.Pages.Count - 1);

                            // URL
                            this.Pages[this.Pages.Count - 1].URL = this.MakeAbsoluteUrl(this.Pages.Count - 1);
                        }
                    }
                }
            }

            // Создание страниц продолжения главной
            if (this.settings.PagesStaticIndexContinues || this.settings.PagesDynamicIndexContinues)
            {
                int pageCount = 0;
                for (int i = 0; i < this.Pages.Count; i++)
                {
                    if (this.Pages[i].Type == 1)
                    {
                        pageCount++;
                    }
                }

                switch (this.settings.PagesDoorwayType)
                {
                    case 0:
                        {
                            pageCount /= this.settings.PagesStaticKeysPerPageOnContinues;
                            break;
                        }
                    case 1:
                        {
                            pageCount /= this.settings.PagesDynamicKeysPerPageOnContinues;
                            break;
                        }
                }

                pageCount--;
                for (int i = 0; i < pageCount; i++)
                {
                    this.Pages.Add(new Page());
                    this.Pages[this.Pages.Count - 1].Type = 6;

                    // Кейворд
                    this.Pages[this.Pages.Count - 1].Keywords.Add(this.keywords[0]);

                    // Категория
                    this.Pages[this.Pages.Count - 1].Category = -1;

                    // Имя файла
                    this.Pages[this.Pages.Count - 1].Name = this.MakeFileName(this.Pages.Count - 1);

                    // URL
                    this.Pages[this.Pages.Count - 1].RelativeURL = this.MakeRelativeURL(this.Pages.Count - 1);
                    this.Pages[this.Pages.Count - 1].URL = this.MakeAbsoluteUrl(this.Pages.Count - 1);
                }
            }

            // Создание HTML карты сайта
            if (this.settings.SiteMap)
            {
                switch (this.settings.SiteMapType)
                {
                    //XML
                    case 0:
                        {
                            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000453") + "\r\n");
                            MakeXMLSiteMap();
                            break;
                        }
                    //HTML
                    case 1:
                        {
                            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000454") + "\r\n");
                            MakeHTMLSiteMap();
                            break;
                        }
                    //HTML + XML
                    case 2:
                        {
                            //HTML
                            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000454") + "\r\n");
                            MakeHTMLSiteMap();
                            //XML
                            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000453") + "\r\n");
                            MakeXMLSiteMap();
                            break;
                        }

                        // Auto HTML
                    case 3:
                        {
                            // Auto HTML
                            this.MakeAutoHTMLSiteMap();

                            break;
                        }

                        // XML + AutoHTML
                    case 4:
                        {
                            // Auto HTML
                            this.MakeAutoHTMLSiteMap();

                            // XML
                            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000453") + "\r\n");
                            this.MakeXMLSiteMap();
                            break;
                        }
                }
            }
            //Custom Pages
            for (int i = 0; i < SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages.Count; i++)
            {
                this.Pages.Add(new Page());
                this.Pages[this.Pages.Count - 1].Type = 7;

                //Кейворд
                if (SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages[i].CustomKeywords.Length == 0)
                {
                    this.Pages[this.Pages.Count - 1].Keywords.Add(this.keywords[0]);
                }
                else
                {
                    this.Pages[this.Pages.Count - 1].Keywords.Add(
                        SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages[i].CustomKeywords[
                            this.random.Next(SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages[i].CustomKeywords.Length)]);
                }
                //Категория
                this.Pages[this.Pages.Count - 1].Category = -1;
                //Имя файла
                this.Pages[this.Pages.Count - 1].Name = MakeFileName(this.Pages.Count - 1);
                //URL
                this.Pages[this.Pages.Count - 1].RelativeURL = this.MakeRelativeURL(this.Pages.Count - 1);
                this.Pages[this.Pages.Count - 1].URL = MakeAbsoluteUrl(this.Pages.Count - 1);
            }

            // Static Pages html sitemap data
            for (int i = 0; i < this.Pages.Count; i++)
            {
                if (this.Pages[i].Type == 2)
                {
                    this.Pages[i].HTMLSiteMapPageStart = 0;
                    this.Pages[i].HTMLSiteMapPageCount = this.Pages.Count;
                }
            }
        }

        private int usedTags;

        private string MakeTag()
        {
            if (this.Tags.Count == 0)
            {
                return string.Empty;
            }

            if (this.Tags.Count <= this.usedTags)
            {
                this.usedTags = 0;
            }

            string tag = this.Tags[this.usedTags];
            this.usedTags++;

            return tag;
        }

        private void MakeAutoHTMLSiteMap()
        {
            // Set one page html map
            this.settings.SiteMapHTMLType = 0;

            // Make sitemap pages
            this.MakeHTMLSiteMap();
        }

        private void MakeHTMLSiteMap()
        {
            int usedPages = 0;
            int totalPages = this.Pages.Count;
            if (this.settings.StaticPages && !this.settings.StaticPagesIncludeIntoSiteMap)
            {
                totalPages -= this.settings.StaticPagesList.Length;
            }

            int htmlSiteMapPages = 0;

            //Random random = new Random(DateTime.Now.Millisecond);

            while (usedPages < totalPages)
            {
                int usePages = 0;

                switch (this.settings.SiteMapHTMLType)
                {
                        //Onepage
                    case 0:
                        {
                            usePages = totalPages;
                            break;
                        }
                    case 1:
                        {
                            usePages = random.Next(this.settings.SiteMapHTMLLinksMin, this.settings.SiteMapHTMLLinksMax);
                            break;
                        }
                }
                if ((usePages + usedPages) >= totalPages)
                {
                    usePages = totalPages - usedPages;
                }
                //Добавление страницы
                this.Pages.Add(new Page());
                //Заполнение полей страницы
                // Тип
                this.Pages[this.Pages.Count - 1].Type = 4;

                // Категория
                this.Pages[this.Pages.Count - 1].Category = -1;

                // Стартовая страница
                this.Pages[this.Pages.Count - 1].HTMLSiteMapPageStart = usedPages;

                // Количество страниц
                this.Pages[this.Pages.Count - 1].HTMLSiteMapPageCount = usePages;

                // Имя файла
                this.Pages[this.Pages.Count - 1].Name = MakeFileName(this.Pages.Count - 1);

                // URL
                this.Pages[this.Pages.Count - 1].RelativeURL = this.MakeRelativeURL(this.Pages.Count - 1);
                this.Pages[this.Pages.Count - 1].URL = MakeAbsoluteUrl(this.Pages.Count - 1);
                usedPages += usePages;
                htmlSiteMapPages++;
            }
            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000455") + htmlSiteMapPages.ToString() + ".\r\n");
        }

        private void MakeXMLSiteMap()
        {
            if (this.settings.GeneralDoorwayUrls.Length <= this.index)
            {
                return;
            }

            // Создание
            StringBuilder siteMapContent = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\r\n", 100000);
            for (int i = 0; i < this.Pages.Count; i++)
            {
                if (this.Pages[i].Type == 2 && !this.settings.StaticPagesIncludeIntoSiteMap)
                {
                    continue;
                }
                siteMapContent.Append("<url><loc>");
                siteMapContent.Append(this.Pages[i].URL);
                siteMapContent.Append("</loc>\r\n");
                switch (this.Pages[i].Type)
                {
                    case 0:
                        {
                            siteMapContent.Append("<changefreq>daily</changefreq>\r\n<priority>1.0</priority>");
                            break;
                        }
                    case 1:
                    case 2:
                        {
                            siteMapContent.Append("<changefreq>monthly</changefreq>\r\n<priority>0.6</priority>");
                            break;
                        }
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        {
                            siteMapContent.Append("<changefreq>weekly</changefreq>\r\n<priority>0.8</priority>");
                            break;
                        }
                }
                siteMapContent.Append("</url>");
            }
            siteMapContent.Append("</urlset>");

            // Сохранение
            File.WriteAllText(this.doorwayFolder + "sitemap.xml", siteMapContent.ToString(), Encoding.UTF8);
            IOHelper.SetFileDirectoryDate(this.doorwayFolder + "sitemap.xml", random, this.settings);
            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000456") + ".\r\n");
        }

        private string MakeRelativeURL(int PageIndex)
        {
            return MakeCommonURL(PageIndex).MakeURLRelative();
        }

        private string MakeCommonURL(int PageIndex)
        {
            string url = string.Empty;
            switch (this.settings.PagesDoorwayType)
            {
                // Статические урлы
                case 0:
                    {
                        if (this.Pages[PageIndex].Type == 0)
                        {
                            switch (this.settings.MacrosesMainLinkType)
                            {
                                case 2:
                                    {
                                        url = "index" + this.settings.PagesStaticExtension;
                                        break;
                                    }

                                case 3:
                                    {
                                        url = this.settings.MacrosesMainLink;
                                        break;
                                    }

                                default:
                                    {
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            url = this.Pages[PageIndex].Name.Substring(this.doorwayFolder.Length);
                        }

                        break;
                    }

                // Динамические урлы
                case 1:
                    {
                        switch (this.Pages[PageIndex].Type)
                        {
                            case 0:
                                {
                                    switch (this.settings.MacrosesMainLinkType)
                                    {
                                        case 2:
                                            {
                                                url = "index.php";
                                                break;
                                            }

                                        case 3:
                                            {
                                                url = this.settings.MacrosesMainLink;
                                                break;
                                            }

                                        default:
                                            {
                                                break;
                                            }
                                    }

                                    break;
                                }

                            case 1:
                                {
                                    url = "index.php?" + this.settings.PagesDynamicPageNames + "=" + this.Pages[PageIndex].Name.Substring(this.Pages[PageIndex].Name.IndexOf("-") + 1, this.Pages[PageIndex].Name.LastIndexOf(".") - this.Pages[PageIndex].Name.IndexOf("-") - 1);
                                    break;
                                }

                            case 2:
                                {
                                    url = "index.php?" + this.settings.PagesDynamicStaticPageNames + "=" + this.Pages[PageIndex].Name.Substring(this.Pages[PageIndex].Name.IndexOf("-") + 1, this.Pages[PageIndex].Name.LastIndexOf(".") - this.Pages[PageIndex].Name.IndexOf("-") - 1);
                                    break;
                                }

                            case 3:
                                {
                                    url = "index.php?" + this.settings.PagesDynamicCategoriesNames + "=" + this.Pages[PageIndex].Name.Substring(this.Pages[PageIndex].Name.IndexOf("-") + 1, this.Pages[PageIndex].Name.LastIndexOf(".") - this.Pages[PageIndex].Name.IndexOf("-") - 1);
                                    break;
                                }

                            case 4:
                                {
                                    url = "index.php?sitemap=" + this.Pages[PageIndex].Name.Substring(this.Pages[PageIndex].Name.IndexOf("-") + 1, this.Pages[PageIndex].Name.LastIndexOf(".") - this.Pages[PageIndex].Name.IndexOf("-") - 1);
                                    break;
                                }

                            case 5:
                                {
                                    url = "index.php?" + this.settings.PagesDynamicCategoriesContinuesNames1 + "=" +
                                    this.Pages[PageIndex].Name.Substring(this.Pages[PageIndex].Name.IndexOf("-") + 1, this.Pages[PageIndex].Name.IndexOf("-", this.Pages[PageIndex].Name.IndexOf("-") + 1) - this.Pages[PageIndex].Name.IndexOf("-") - 1) +
                                    "&" + this.settings.PagesDynamicCategoriesContinuesNames2 + "=" + this.Pages[PageIndex].Name.Substring(this.Pages[PageIndex].Name.LastIndexOf("-") + 1, this.Pages[PageIndex].Name.LastIndexOf(".") - this.Pages[PageIndex].Name.LastIndexOf("-") - 1);
                                    break;
                                }

                            case 6:
                                {
                                    url = "index.php?" + this.settings.PagesDynamicIndexContinuesNames + "=" + this.Pages[PageIndex].Name.Substring(this.Pages[PageIndex].Name.IndexOf("-") + 1, this.Pages[PageIndex].Name.LastIndexOf(".") - this.Pages[PageIndex].Name.IndexOf("-") - 1);
                                    break;
                                }

                        }
                        break;
                    }
            }

            return url.Replace("\\", "/");
        }

        private string MakeAbsoluteUrl(int PageIndex)
        {
            string url = this.MakeCommonURL(PageIndex);

            if (this.settings.GeneralDoorwayUrls.Length > index)
            {
                url = this.settings.GeneralDoorwayUrls[index] + url;
            }
            else
            {
                url = "/" + url;
            }
            while (url.Contains("\\"))
            {
                url = url.Replace("\\", "/");
            }

            if (url.StartsWith("http://"))
            {
                while (url.IndexOf("//", 7) >= 0)
                {
                    url = "http://" + url.Substring(7).Replace("//", "/");
                }
            }
            else
            {
                while (url.Contains("//"))
                {
                    url = url.Replace("//", "/");
                }
            }
            return url;
        }

        private int currentCategory = 0;
        private int GetCategory(int PageIndex)
        {
            if (this.settings.CategoriesType == 0 && this.settings.CategoriesDistribute == 1)
            {
                // IfContains
                for (int i = 0; i < SharedData.WorkSpaces[wsIndex].Keywords[categoriesDCIndex].Items.Length; i++)
                {
                    if (this.Pages[PageIndex].Keywords[0].Contains(SharedData.WorkSpaces[wsIndex].Keywords[categoriesDCIndex].Items[i]))
                    {
                        // Поиск категории
                        for (int j = 0; j < this.categories.Count; j++)
                        {
                            if (this.Pages[PageIndex].Keywords[0].Contains(this.categories[j]))
                            {
                                return j;
                            }
                        }
                    }
                }

                return this.random.Next(this.categories.Count);
            }

            int category = currentCategory;
            currentCategory++;
            if (currentCategory >= this.categories.Count)
            {
                currentCategory = 0;
            }

            return category;
        }

        private void MakePages()
        {
            // Генерирование страниц
            for (int i = 0; i < this.Pages.Count; i++)
            {
                while (this.Pause)
                {
                    Thread.Sleep(1000);
                }

                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000457") + (i + 1).ToString() + View.UILanguageResources.GetString("S0000458"));
                switch (this.Pages[i].Type)
                {
                    case 0:
                        {
                            this.Log.Append("Index");
                            break;
                        }
                    case 1:
                        {
                            this.Log.Append(View.UILanguageResources.GetString("S0000174"));
                            break;
                        }
                    case 2:
                        {
                            this.Log.Append(View.UILanguageResources.GetString("S0000459"));
                            break;
                        }
                    case 3:
                        {
                            this.Log.Append(View.UILanguageResources.GetString("S0000460"));
                            break;
                        }
                    case 4:
                        {
                            this.Log.Append(View.UILanguageResources.GetString("S0000142"));
                            break;
                        }
                    case 5:
                        {
                            this.Log.Append(View.UILanguageResources.GetString("S0000461"));
                            break;
                        }
                    case 6:
                        {
                            this.Log.Append(View.UILanguageResources.GetString("S0000462"));
                            break;
                        }
                    case 7:
                        {
                            this.Log.Append(View.UILanguageResources.GetString("S0000498"));
                            break;
                        }
                }

                this.Log.Append("...\r\n");
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000463") + "\r\n");
                this.SetTemplateContent(i);
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000464") + "\r\n");

                this.ReplaceMacroses(i);

                // Check if images was used, and set proper flag to indicate that
                for (int k = 0; k < imageRepository.Images.Count; k++)
                {
                    if (this.Pages[i].Content.IndexOf(imageRepository.Images[k].Path) != -1)
                    {
                        imageRepository.Images[k].Used = true;
                    }
                }

                this.PagesDone++;
            }
        }

        private void SetTemplateContent(int PageIndex)
        {
            //Random random = new Random(DateTime.Now.Millisecond);
            //Загрузка шаблонного контента
            switch (this.Pages[PageIndex].Type)
            {
                    // Index
                case 0:
                    // Index Continue
                case 6:
                    {
                        this.Pages[PageIndex].Content = SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Index;
                        this.Log.AppendLine(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000520") + ": " + "Index");
                        break;
                    }

                    // Page
                case 1:
                    {
                        if (SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Pages.Count == 0)
                        {
                            this.Pages[PageIndex].Content = SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Index;
                            this.Log.AppendLine(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000520") + ": " + "Index");
                        }
                        else
                        {
                            if (SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Pages.Sum(p => p.UsagePercent) == 100)
                            {
                                int selectedPercent = this.random.Next(1, 100);
                                int iteratedPercent = 0;

                                for (int i = 0; i < SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Pages.Count; i++)
                                {
                                    int previousPercent = iteratedPercent;
                                    iteratedPercent += SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Pages[i].UsagePercent;

                                    if ((iteratedPercent >= 100) || (previousPercent <= selectedPercent && selectedPercent < iteratedPercent))
                                    {
                                        this.Pages[PageIndex].Content = SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Pages[i].Content;
                                        this.Log.AppendLine(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000520") + ": " +
                                                            SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Pages[i].Path);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                int idx = this.random.Next(SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Pages.Count);

                                this.Pages[PageIndex].Content = SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Pages[idx].Content;
                                this.Log.AppendLine(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000520") + ": " +
                                                    SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Pages[idx].Path);
                            }
                        }

                        break;
                    }

                    // Static Page
                case 2:
                    {
                        if (SharedData.WorkSpaces[wsIndex].Templates[templateIndex].StaticPages.Count == 0)
                        {
                            this.Pages[PageIndex].Content = SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Index;
                            this.Log.AppendLine(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000520") + ": " + "Index");
                        }
                        else
                        {
                            int idx = this.random.Next(SharedData.WorkSpaces[wsIndex].Templates[templateIndex].StaticPages.Count);
                            this.Pages[PageIndex].Content = SharedData.WorkSpaces[wsIndex].Templates[templateIndex].StaticPages[idx].Content;

                            this.Log.AppendLine(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000520") + ": " +
                                                SharedData.WorkSpaces[wsIndex].Templates[templateIndex].StaticPages[idx].Path);
                        }

                        break;
                    }
                    // Category
                case 3:
                    // Category Continue
                case 5:
                    {
                        if (SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Categories.Count == 0)
                        {
                            this.Pages[PageIndex].Content = SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Index;
                            this.Log.AppendLine(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000520") + ": " + "Index");
                        }
                        else
                        {
                            int idx = random.Next(SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Categories.Count);
                            this.Pages[PageIndex].Content = SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Categories[idx].Content;
                            this.Log.AppendLine(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000520") + ": " +
                                                SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Categories[idx].Path);
                        }

                        break;
                    }

                    // SiteMap
                case 4:
                    {
                        if (this.settings.SiteMapType == 1 || this.settings.SiteMapType == 2)
                        {
                            int idx = this.random.Next(SharedData.WorkSpaces[this.wsIndex].Templates[this.templateIndex].Map.Count);
                            this.Pages[PageIndex].Content = SharedData.WorkSpaces[this.wsIndex].Templates[this.templateIndex].Map[idx].Content;
                            this.Log.AppendLine(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000520") + ": " + SharedData.WorkSpaces[wsIndex].Templates[templateIndex].Map[idx].Path);
                        }
                        else
                        {
                            this.Pages[PageIndex].Content = Resource.AutoMap.Replace("#TITLE#", View.UILanguageResources.GetString("S0000516"));
                        }

                        break;
                    }

                    // Custom Page
                case 7:
                    {
                        int customPageNumber = 0;

                        for (int i = 0; i < PageIndex; i++)
                        {
                            if (this.Pages[i].Type == 7)
                            {
                                customPageNumber++;
                            }
                        }

                        this.Pages[PageIndex].Content = SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages[customPageNumber].Content;
                        this.Log.AppendLine(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000520") + ": " +
                                            SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages[customPageNumber].Path);
                        break;
                    }
            }
        }

        public void Save()
        {
            if (this.settings.PagesDoorwayType == 0 || this.settings.PagesDoorwayType == 1)
            {
                this.SaveToFiles();
            }
            else
            {
                this.SaveToDB();
            }

            //Сохранение в архив
            if (this.settings.GeneralArchive != 0)
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000465") + "\r\n");
                MakeArchive(0);
            }
        }

        private void SaveToDB()
        {
            string dbPath = Path.Combine(this.doorwayFolder, "database.db");

            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }

            // Initialize
            SQLiteConnection sql_con = new SQLiteConnection("Data Source=\"" + dbPath + "\";Version=3;New=False;Compress=False;");

            this.InitializeDBData(sql_con);

            // Save
            for (int i = 0; i < this.Pages.Count; i++)
            {
                string txtSQLQuery = "insert into UDS (key, value) values ('" + this.Pages[i].Name + "' , '" + this.Pages[i].Content.Replace("'", "&quotemark;") + "')";
                this.ExecuteDBQuery(sql_con, txtSQLQuery);
            }
        }

        private void ExecuteDBQuery(SQLiteConnection sql_con, string txtQuery)
        {
            if (sql_con.State == ConnectionState.Open)
            {
                sql_con.Close();
            }

            sql_con.Open();
            SQLiteCommand sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        private void InitializeDBData(SQLiteConnection sql_con)
        {
            DataSet DS = new DataSet();

            sql_con.Open();
            string CommandText = "select key, value from UDS";

            SQLiteDataAdapter DB = new SQLiteDataAdapter(CommandText, sql_con);
            DS.Reset();
            try
            {
                DB.Fill(DS);
            }
            catch (Exception)
            {
                // Create table
                string txtSQLQuery = "create table UDS(key varchar(255), value text)";
                this.ExecuteDBQuery(sql_con, txtSQLQuery);
                DB.Fill(DS);
            }

            sql_con.Close();
        }

        /// <summary>
        /// Сохранение
        /// </summary>
        private void SaveToFiles()
        {
            // Сохранение страниц
            int encoding = SharedData.WorkSpaces[this.wsIndex].Templates[SharedData.WorkSpaces[this.wsIndex].GetTemplateIndex(this.templateID)].EncodingType;
            for (int i = 0; i < this.Pages.Count; i++)
            {
                try
                {
                    // Создание папок
                    if (this.Pages[i].Name.Contains("\\"))
                    {
                        if (!Directory.Exists(this.Pages[i].Name.Substring(0, this.Pages[i].Name.LastIndexOf("\\"))))
                        {
                            string directory = this.Pages[i].Name.Substring(0, this.Pages[i].Name.LastIndexOf("\\"));
                            Directory.CreateDirectory(directory);
                            File.SetAttributes(directory, FileAttributes.Normal);
                            IOHelper.SetFileDirectoryDate(directory, random, this.settings);
                        }
                    }
                    this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000492") + this.Pages[i].Name + "...\r\n");

                    // Сохранение страницы
                    File.WriteAllText(this.Pages[i].Name, this.Pages[i].Content, encoding == 0 ? Encoding.Default : Encoding.UTF8);

                    // Set file creation and modification time
                    IOHelper.SetFileDirectoryDate(this.Pages[i].Name, random, settings);
                }
                catch (Exception)
                {
                    this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000493") + this.Pages[i].Name + ".\r\n");
                }
            }

            try
            {
                IOHelper.SetFileDirectoryDate(this.doorwayFolder, random, settings);
                IOHelper.SetDirectoryDateRecursive(this.doorwayFolder, settings);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Make Archive
        /// </summary>
        /// <param name="Type">0 - просто архив; 1 - архив для заливки по фтп</param>
        private void MakeArchive(int Type)
        {
            if (this.settings.GeneralArchiveName == string.Empty)
            {
                return;
            }

            string[] files = Directory.GetFiles(this.doorwayFolder, "*", SearchOption.AllDirectories);

            int archiveType = 0;
            string archiveName = this.doorwayFolder;

            switch (Type)
            {
                case 0:
                    {
                        archiveName += this.settings.GeneralArchiveName;
                        archiveType = this.settings.GeneralArchive;
                        break;
                    }

                case 1:
                    {
                        archiveName += this.settings.FTPUploadArchiveName;
                        archiveType = this.settings.FTPUploadArchive;
                        break;
                    }
            }

            switch (archiveType)
            {
                case 1:
                    {
                        //Создание Zip
                        using (ZipStorer zip = ZipStorer.Create(archiveName, string.Empty))
                        {
                            for (int i = 0; i < files.Length; i++)
                            {
                                zip.AddFile(ZipStorer.Compression.Deflate, files[i], files[i].Substring(this.doorwayFolder.Length), string.Empty);
                            }
                            //zip.Close();
                        }

                        break;
                    }

                case 2:
                    {
                        //Создание Tar.Gz
                        using (var outFile = File.Create(archiveName))
                        {
                            using (var outStream = new GZipStream(outFile, CompressionMode.Compress))
                            {
                                using (var writer = new TarStorer.TarWriter(outStream))
                                {
                                    for (int i = 0; i < files.Length; i++)
                                    {
                                        writer.Write(files[i], files[i].Substring(this.doorwayFolder.Length));
                                    }
                                }
                            }
                        }

                        break;
                    }
            }
            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000466") + "\r\n");

            // Удаление файлов
            for (int i = 0; i < files.Length; i++)
            {
                File.Delete(files[i]);
            }

            // Удаление папок
            files = Directory.GetDirectories(this.doorwayFolder, "*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; i++)
            {
                Directory.Delete(files[i], true);
            }
        }

        public void Upload()
        {
            try
            {
                // Сохранение в архив
                if (this.settings.FTPUploadArchive != 0)
                {
                    this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000465") + "\r\n");
                    this.MakeArchive(1);
                }

                // Проверка, есть ли настройки для загрузки фтп
                if (this.index >= this.settings.FTPSettings.Length)
                {
                    return;
                }

                // Поиск файлов
                string[] files = Directory.GetFiles(this.doorwayFolder, "*", SearchOption.AllDirectories);
                switch (this.settings.FTPUploadType)
                {
                    // Обычная выгрузка
                    case 0:
                        {
                            // Подготовка к заливке
                            // Создание списков переменных
                            this.threads = new List<Thread>();
                            this.done = new List<bool>();
                            string[] FTPFiles = new string[0];

                            // Подсчет кол-ва доров на поток
                            int pagesPerThread = files.Length / this.settings.GeneralThreads;
                            int startPageIndex = 0;
                            for (int i = 0; i < this.settings.FTPThreads - 1; i++)
                            {
                                // Подготовка списка файлов
                                FTPFiles = new string[pagesPerThread];
                                for (int k = startPageIndex; k < (startPageIndex + pagesPerThread); k++)
                                {
                                    FTPFiles[k - startPageIndex] = files[k];
                                }

                                // Запуск
                                this.threads.Add(new Thread(this.FTPWork));
                                this.done.Add(false);
                                this.threads[i].Start(new FTPStartData(i, startPageIndex, pagesPerThread, FTPFiles));
                                startPageIndex += pagesPerThread;
                            }

                            // Подготовка списка файлов
                            FTPFiles = new string[files.Length - startPageIndex];
                            for (int k = startPageIndex; k < files.Length; k++)
                            {
                                FTPFiles[k - startPageIndex] = files[k];
                            }

                            // Запуск
                            this.threads.Add(new Thread(this.FTPWork));
                            this.done.Add(false);
                            this.threads[this.threads.Count - 1].Start(new FTPStartData(this.threads.Count - 1, startPageIndex, this.settings.GeneralCreateDoorways, FTPFiles));

                            // Если заливка не в фоне
                            if (!this.settings.FTPUploadInBackground)
                            {
                                for (int i = 0; i < this.threads.Count; i++)
                                {
                                    if (this.threads[i].IsAlive)
                                    {
                                        this.threads[i].Join();
                                    }
                                }

                                // Удаление файлов после загрузки
                                if (this.settings.FTPDelete)
                                {
                                    this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000466") + "\r\n");

                                    // Удаление файлов
                                    this.DeleteFiles();
                                }
                            }

                            break;
                        }

                    // Создание проекта FileZilla
                    case 1:
                        {
                            this.MakeFileZilla(files);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000435") + ex.Message.Replace("\r\n", " ") + "\r\nStackTrace: " + ex.StackTrace.Replace("\r\n", " ") + "\r\nTargetSite: " + ex.TargetSite.Name.Replace("\r\n", " ") + ".\r\n");
            }
        }

        public void MakeFileZilla(string[] Files)
        {
            StringBuilder data = new StringBuilder(10000);
            //Работа с данными
            data.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\" ?>");
            data.Append("<FileZilla3>");
            data.Append("<Queue>");
            data.Append("<Server>");
            data.Append("<Host>" + this.settings.FTPSettings[index].Host + "</Host>");
            data.Append("<Port>21</Port>");
            data.Append("<Protocol>0</Protocol>");
            data.Append("<Type>0</Type>");
            data.Append("<User>" + this.settings.FTPSettings[index].Login + "</User>");
            data.Append("<Pass>" + this.settings.FTPSettings[index].Password + "</Pass>");
            data.Append("<Logontype>1</Logontype>");
            data.Append("<TimezoneOffset>0</TimezoneOffset>");
            data.Append("<PasvMode>MODE_DEFAULT</PasvMode>");
            data.Append("<MaximumMultipleConnections>0</MaximumMultipleConnections>");
            data.Append("<EncodingType>Binary</EncodingType>");
            data.Append("<BypassProxy>0</BypassProxy>");
            data.Append("<Name>РЅРЅ</Name>");

            FileInfo fileInfo;

            for (int i = 0; i < Files.Length; i++)
            {
                fileInfo = new FileInfo(Files[i]);

                data.Append("<File>");
                data.Append("<LocalFile>" + Files[i] + "</LocalFile>");
                data.Append("<RemoteFile>" + Files[i].Substring(Files[i].LastIndexOf("\\") + 1) + "</RemoteFile>");
                data.Append("<RemotePath>1 0");
                string folder = this.settings.FTPSettings[index].Folder;
                while (folder.Contains("/"))
                {
                    data.Append(" " + folder.Substring(0, folder.IndexOf("/")).Length.ToString() + " " + folder.Substring(0, folder.IndexOf("/")));
                    folder = folder.Substring(folder.IndexOf("/") + 1);
                }
                if (folder.Length > 0)
                {
                    data.Append(" " + folder.Length.ToString() + " " + folder);
                }

                folder = Files[i];
                folder = folder.Substring(0, Files[i].LastIndexOf("\\") + 1);
                folder = folder.Substring(this.doorwayFolder.Length);
                folder = folder.Replace("\\", "/");

                //folder = this.settings.FTPSettings[index].Folder + folder;
                //folder = folder.Replace("\\", "/");
                //folder = folder.Trim().Trim(new char[] { '/' });

                if (folder.Length > 0)
                {
                    if (folder.Contains("/"))
                    {
                        string[] subfolders = folder.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int k = 0; k < subfolders.Length; k++)
                        {
                            data.Append(" " + subfolders[k].Length.ToString() + " " + subfolders[k]);
                        }
                    }
                    else
                    {
                        data.Append(" " + folder.Length.ToString() + " " + folder);
                    }
                }
                data.Append("</RemotePath>");

                data.Append("<Download>0</Download>");
                data.Append("<Size>" + fileInfo.Length.ToString() + "</Size>");
                data.Append("<TransferMode>1</TransferMode>");
                data.Append("</File>");
            }

            data.Append("</Server>");
            data.Append("</Queue>");
            data.Append("</FileZilla3>");
            //Создание папки
            if (!Directory.Exists(this.settings.FTPUploadSaveTo))
            {
                Directory.CreateDirectory(this.settings.FTPUploadSaveTo);
            }
            //Запись файла
            File.WriteAllText(Path.Combine(this.settings.FTPUploadSaveTo, "FileZilla " + (index + 1).ToString() + ".xml"), data.ToString(), Encoding.UTF8);
            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000467") + ".\r\n");
        }

        /// <summary>
        /// Удаление всех файлов/папок в папке с дорвеями
        /// </summary>
        public void DeleteFiles()
        {
            try
            {
                Directory.Delete(this.doorwayFolder, true);
            }
            catch (Exception)
            {
            }
        }

        private void FTPWork(object Parameters)
        {
            FTPStartData StartData = Parameters as FTPStartData;
            FTP FTPConnection = new FTP();
            // Выгрузка файлов
            try
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000468") + (StartData.ThreadIndex + 1).ToString() + View.UILanguageResources.GetString("S0000469") + "\r\n");
                if (this.FTPLogin(ref FTPConnection) != 0)
                {
                    this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000468") + (StartData.ThreadIndex + 1).ToString() + View.UILanguageResources.GetString("S0000470") + ".\r\n");
                    this.done[StartData.ThreadIndex] = true;
                    return;
                }

                // Заход в папку
                if (this.FTPChangeDirectoryToMain(ref FTPConnection) != 0)
                {
                    this.done[StartData.ThreadIndex] = true;
                    return;
                }

                // Выгрузка файлов
                for (int i = 0; i < StartData.Files.Length; i++)
                {
                    this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000468") + (StartData.ThreadIndex + 1).ToString() + View.UILanguageResources.GetString("S0000471") + StartData.Files[i].Substring(this.doorwayFolder.Length) + "...\r\n");
                    try
                    {
                        if (StartData.Files[i].Substring(this.doorwayFolder.Length).Contains("\\"))
                        {
                            string fileDirectory = StartData.Files[i].Substring(this.doorwayFolder.Length);
                            fileDirectory = fileDirectory.Substring(0, fileDirectory.LastIndexOf("\\"));
                            fileDirectory = fileDirectory.Replace("\\", "/");

                            int folderNumber = 0;

                            if (!FTPConnection.GetWorkingDirectory().Contains(fileDirectory))
                            {
                                // Создание папки
                                try
                                {
                                    while (fileDirectory.Contains("/"))
                                    {
                                        if (FTPConnection.ListDirectories().Contains(fileDirectory.Substring(0, fileDirectory.IndexOf("/"))))
                                        {
                                            FTPConnection.ChangeDir(fileDirectory.Substring(0, fileDirectory.IndexOf("/")));
                                        }
                                        else
                                        {
                                            FTPConnection.MakeDir(fileDirectory.Substring(0, fileDirectory.IndexOf("/")));
                                            FTPConnection.ChangeDir(fileDirectory.Substring(0, fileDirectory.IndexOf("/")));
                                        }

                                        fileDirectory = fileDirectory.Substring(fileDirectory.IndexOf("/") + 1);
                                        folderNumber++;
                                    }

                                    if (!FTPConnection.ListDirectories().Contains(fileDirectory))
                                    {
                                        FTPConnection.MakeDir(fileDirectory);
                                        folderNumber++;
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }

                            FTPConnection.ChangeDir(fileDirectory);
                            folderNumber++;

                            // Заливка
                            FTPConnection.OpenUpload(StartData.Files[i], StartData.Files[i].Substring(StartData.Files[i].LastIndexOf("\\") + 1), false);
                            while (FTPConnection.DoUpload() > 0) { }

                            for (int k = 0; k < folderNumber; k++)
                            {
                                FTPConnection.ChangeDirUp();
                            }
                        }
                        else
                        {
                            // Заливка
                            FTPConnection.OpenUpload(StartData.Files[i], StartData.Files[i].Substring(StartData.Files[i].LastIndexOf("\\") + 1), false);
                            while (FTPConnection.DoUpload() > 0) { }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000468") + (StartData.ThreadIndex + 1).ToString() + View.UILanguageResources.GetString("S0000497") + StartData.Files[i].Substring(this.doorwayFolder.Length) + "...\r\n");
                    }
                }
                //Завершение соединения
                FTPConnection.Disconnect();
            }
            catch (Exception ex)
            {
                //Завершение соединения
                FTPConnection.Disconnect();

                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000468") + (StartData.ThreadIndex + 1).ToString() + ". " + ex.Message + ".\r\n");
                this.done[StartData.ThreadIndex] = true;
                return;
            }
            //Удаление файлов
            if (this.settings.FTPDelete && this.settings.FTPUploadInBackground)
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000466") + "\r\n");
                //Удаление файлов
                for (int i = 0; i < StartData.Files.Length; i++)
                {
                    File.Delete(StartData.Files[i]);
                }
                //Удаление папок
                /*StartData.Files = Directory.GetDirectories(this.doorwayFolder, "*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < StartData.Files.Length; i++)
                {
                    Directory.Delete(StartData.Files[i], true);
                }*/
            }
            this.done[StartData.ThreadIndex] = true;
        }

        private int FTPLogin(ref FTP FTPConnection)
        {
            try
            {
                FTPConnection.Connect(this.settings.FTPSettings[index].Host, this.settings.FTPSettings[index].Login, this.settings.FTPSettings[index].Password);
                //Заход в пользовательскую папку
                FTPConnection.ChangeDir("/");

                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private int FTPChangeDirectoryToMain(ref FTP FTPConnection)
        {
            try
            {
                string myfolder = this.settings.FTPSettings[index].Folder;
                if (myfolder != string.Empty)
                {
                    if (myfolder.Contains("/"))
                    {
                        while (myfolder.Contains("/"))
                        {
                            try
                            {
                                FTPConnection.ChangeDir(myfolder.Substring(0, myfolder.IndexOf("/")));
                            }
                            catch (Exception)
                            {
                                FTPConnection.MakeDir(myfolder.Substring(0, myfolder.IndexOf("/")));
                            }
                            myfolder = myfolder.Substring(myfolder.IndexOf("/") + 1);
                        }

                        FTPConnection.ChangeDir(myfolder);
                    }
                    else
                    {
                        FTPConnection.ChangeDir(myfolder);
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private void ReplaceMacroses(int PageIndex)
        {
            string Content = this.Pages[PageIndex].Content;

            // Replace random text tokens from files
            if (textTokensReplacer.Context == null)
            {
                textTokensReplacer.Context = GetTextTokensReplacerContext();
            }
            Content = this.textTokensReplacer.ReplaceRandomTextFileTokens(Content);
            Content = this.textTokensReplacer.ReplaceFullFileContentTextTokens(Content);

            // Замена случайных макросов
            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000472") + "\r\n");
            Content = this.ReplaceRandomTokens(Content);

            // Keywords
            this.ReplaceKeywordTokens(PageIndex, ref Content);

            // Замена блочных макросов
            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000473") + "\r\n");
            Content = this.ReplaceBlocksMacroses(PageIndex, Content);

            // Замена обычных макросов
            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000474") + "\r\n");
            this.ReplaceRegularTokens(PageIndex, ref Content);

            // Keywords
            this.ReplaceKeywordTokens(PageIndex, ref Content);

            // Замена макросов текста
            if (this.Pages[PageIndex].Type != 4)
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000475") + "\r\n");
                this.ReplaceTextTokens(PageIndex, ref Content);
            }
            this.Pages[PageIndex].Content = Content;

            // Замена пользовательских, файловых, макросов
            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000476") + "\r\n");
            this.ReplaceFileMacroses(PageIndex);

            // Шифровка
            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000499") + "\r\n");
            this.ReplaceEncryptMacroses(PageIndex);
        }

        private string ReplaceRandomTokens(string Content)
        {
            int startPosition = 0;
            while (Content.Contains("[RANDOM]") && Content.Contains("[/RANDOM]"))
            {
                // Вырезание
                string macrossContent = Content.Substring(Content.IndexOf("[RANDOM]"), Content.IndexOf("[/RANDOM]") + 9 - Content.IndexOf("[RANDOM]"));
                startPosition = Content.IndexOf("[RANDOM]");
                Content = Content.Remove(startPosition, macrossContent.Length);
                macrossContent = macrossContent.Replace("[RANDOM]", string.Empty).Replace("[/RANDOM]", string.Empty);

                // Обработка
                if (macrossContent.Contains("[OR]") && macrossContent.Contains("[/OR]"))
                {
                    macrossContent = macrossContent.Replace("[OR]", string.Empty);
                    string[] ors = macrossContent.Split(new string[] { "[/OR]" }, StringSplitOptions.RemoveEmptyEntries);

                    // Advanced check for empty result
                    string res = ors[this.random.Next(ors.Length)];
                    while (!macrossContent.Contains(res + "[/OR]"))
                    {
                        res = ors[this.random.Next(ors.Length)];
                    }

                    macrossContent = res;
                }

                // Вставка
                Content = Content.Insert(startPosition, macrossContent);
            }

            return Content;
        }
        /// <summary>
        /// Замена всех блоков
        /// </summary>
        /// <param name="PageIndex"></param>
        private string ReplaceBlocksMacroses(int PageIndex, string Content)
        {
            // [BLOCK]
            if (Content.Contains("[BLOCK]"))
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000477") + "\r\n");
                this.ReplaceBlocksMacroses(PageIndex, 0, ref Content);
            }

            // [CATEGORIES]
            if (Content.Contains("[CATEGORIES]"))
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000478") + "\r\n");
                ReplaceBlocksMacroses(PageIndex, 1, ref Content);
            }

            // [CATEGORYMENUBLOCK]
            if (Content.Contains("[CATEGORYMENUBLOCK]"))
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000479") + "\r\n");
                this.ReplaceBlocksMacroses(PageIndex, 2, ref Content);
            }

            // [STATICBLOCK]
            if (Content.Contains("[STATICBLOCK]"))
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000480") + "\r\n");
                this.ReplaceBlocksMacroses(PageIndex, 3, ref Content);
            }

            // [MENUBLOCK]
            if (Content.Contains("[MENUBLOCK]"))
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000481") + "\r\n");
                this.ReplaceBlocksMacroses(PageIndex, 4, ref Content);
            }

            // [NETBLOCK]
            if (Content.Contains("[NETBLOCK]"))
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000482") + "\r\n");
                this.ReplaceBlocksMacroses(PageIndex, 5, ref Content);
            }

            // [USERBLOCK1]
            if (Content.Contains("[USERBLOCK1]"))
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000483") + "\r\n");
                this.ReplaceBlocksMacroses(PageIndex, 6, ref Content);
            }

            // [USERBLOCK2]
            if (Content.Contains("[USERBLOCK2]"))
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000484") + "\r\n");
                this.ReplaceBlocksMacroses(PageIndex, 7, ref Content);
            }

            // [USERBLOCK3]
            if (Content.Contains("[USERBLOCK3]"))
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000485") + "\r\n");
                this.ReplaceBlocksMacroses(PageIndex, 8, ref Content);
            }

            // [USERBLOCK4]
            if (Content.Contains("[USERBLOCK4]"))
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000486") + "\r\n");
                this.ReplaceBlocksMacroses(PageIndex, 9, ref Content);
            }

            // [USERBLOCK5]
            if (Content.Contains("[USERBLOCK5]"))
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000487") + "\r\n");
                this.ReplaceBlocksMacroses(PageIndex, 10, ref Content);
            }

            // [USERBLOCK6]
            if (Content.Contains("[USERBLOCK6]"))
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000488") + "\r\n");
                this.ReplaceBlocksMacroses(PageIndex, 11, ref Content);
            }

            // [MAPBLOCK]
            if (Content.Contains("[MAPBLOCK]"))
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000489") + "\r\n");
                this.ReplaceBlocksMacroses(PageIndex, 12, ref Content);
            }

            // [CUSTOMBLOCK]
            if (Content.Contains("[CUSTOMBLOCK]"))
            {
                this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000500") + "\r\n");
                this.ReplaceBlocksMacroses(PageIndex, 13, ref Content);
            }

            return Content;
        }
        /// <summary>
        /// Замена конкретного блока
        /// </summary>
        /// <param name="PageIndex">Индекс стараницы</param>
        /// <param name="BlockType">Тип блока: 0 - [BLOCK]; 1 - [CATEGORIES]; 2 - [CATEGORYMENUBLOCK]; 3 - [STATICBLOCK]; 4 - [MENUBLOCK];
        /// 5 - [NETBLOCK]; 6 - [USERBLOCK1]; 7 - [USERBLOCK2] ; 8 - [USERBLOCK3]; 9 - [USERBLOCK4]; 10 - [USERBLOCK5]; 11 - [USERBLOCK6];
        /// 12 - [MAPBLOCK]; 13 - [CUSTOMBLOCK]</param>
        private void ReplaceBlocksMacroses(int PageIndex, int BlockType, ref string MainContent)
        {
            //Random random = new Random(/*DateTime.Now.Millisecond*/);

            int blockCount = 0;
            int blockStartIndex = 0;

            string content = string.Empty;

            switch (BlockType)
            {
                //[BLOCK]
                case 0:
                    {
                        if (this.Pages[PageIndex].Type != 1 && this.Pages[PageIndex].Type != 2)
                        {
                            blockCount = random.Next(this.settings.MacrosesBlockMainMin, this.settings.MacrosesBlockMainMax);
                        }
                        else if (this.Pages[PageIndex].Type == 4)
                        {
                            blockCount = this.Pages[PageIndex].HTMLSiteMapPageCount;
                        }
                        else
                        {
                            blockCount = random.Next(this.settings.MacrosesBlockPageMin, this.settings.MacrosesBlockPageMax);
                        }
                        while (MainContent.Contains("[BLOCK]") && MainContent.Contains("[/BLOCK]"))
                        {
                            content = MainContent.Substring(MainContent.IndexOf("[BLOCK]"), MainContent.IndexOf("[/BLOCK]") + 8 - MainContent.IndexOf("[BLOCK]"));

                            blockStartIndex = MainContent.IndexOf("[BLOCK]");

                            MainContent = MainContent.Remove(blockStartIndex, content.Length);

                            content = content.Replace("[BLOCK]", string.Empty).Replace("[/BLOCK]", string.Empty);
                            string tempString = content;

                            for (int i = 0; i < blockCount - 1; i++)
                            {
                                content += tempString;
                            }

                            // Замена макросов
                            ReplaceKeywordTokens(PageIndex, ref content);
                            ReplaceRegularTokens(PageIndex, ref content);
                            ReplaceKeywordTokens(PageIndex, ref content);

                            MainContent = MainContent.Insert(blockStartIndex, content);
                        }
                        break;
                    }
                //[CATEGORIES]
                case 1:
                    {
                        if (this.settings.Categories)
                        {
                            blockCount = this.categories.Count;
                            while (MainContent.Contains("[CATEGORIES]") && MainContent.Contains("[/CATEGORIES]"))
                            {
                                content = MainContent.Substring(MainContent.IndexOf("[CATEGORIES]"), MainContent.IndexOf("[/CATEGORIES]") + 13 - MainContent.IndexOf("[CATEGORIES]"));

                                blockStartIndex = MainContent.IndexOf("[CATEGORIES]");

                                MainContent = MainContent.Remove(blockStartIndex, content.Length);

                                content = content.Replace("[CATEGORIES]", string.Empty).Replace("[/CATEGORIES]", string.Empty);
                                string tempString = content;

                                for (int i = 0; i < blockCount - 1; i++)
                                {
                                    content += tempString;
                                }

                                // Замена макросов
                                ReplaceKeywordTokens(PageIndex, ref content);
                                ReplaceRegularTokens(PageIndex, ref content);
                                ReplaceKeywordTokens(PageIndex, ref content);

                                MainContent = MainContent.Insert(blockStartIndex, content);
                            }
                        }
                        else
                        {
                            //Замена макросов категорий на макросы меню
                            MainContent = MainContent.Replace("[CATEGORIES]", "[MENUBLOCK]");
                            MainContent = MainContent.Replace("[CATEGORIESLINK]", "[MENULINK]");
                            MainContent = MainContent.Replace("[CATEGORIESKEYWORD]", "[MENUKEYWORD]");
                            MainContent = MainContent.Replace("[CATEGORIESBKEYWORD]", "[MENUBKEYWORD]");
                            MainContent = MainContent.Replace("[/CATEGORIES]", "[/MENUBLOCK]");
                        }
                        break;
                    }
                //[CATEGORYMENUBLOCK]
                case 2:
                    {
                        if (this.settings.Categories)
                        {
                            if (this.Pages[PageIndex].Type == 1)
                            {
                                if (this.settings.MacrosesCategoryMenuBlockPageMin == 0 & this.settings.MacrosesCategoryMenuBlockPageMax == 0)
                                {
                                    //Search for page count in this category
                                    blockCount = 0;
                                    for (int k = 0; k < this.Pages.Count; k++)
                                    {
                                        if (this.Pages[k].Category == this.Pages[PageIndex].Category && this.Pages[k].Type == 1)
                                        {
                                            blockCount++;
                                        }
                                    }
                                }
                                else
                                {
                                    blockCount = random.Next(this.settings.MacrosesCategoryMenuBlockPageMin, this.settings.MacrosesCategoryMenuBlockPageMax);
                                }
                            }
                            else if (this.Pages[PageIndex].Type == 3)
                            {
                                if (this.settings.MacrosesCategoryMenuBlockMainMin == 0 & this.settings.MacrosesCategoryMenuBlockMainMax == 0)
                                {
                                    //Search for page count in this category
                                    blockCount = 0;
                                    for (int k = 0; k < this.Pages.Count; k++)
                                    {
                                        if (this.Pages[k].Category == this.Pages[PageIndex].Category && this.Pages[k].Type == 1)
                                        {
                                            blockCount++;
                                        }
                                    }
                                }
                                else
                                {
                                    blockCount = random.Next(this.settings.MacrosesCategoryMenuBlockMainMin, this.settings.MacrosesCategoryMenuBlockMainMax);
                                }
                            }
                            else// if (this.Pages[PageIndex].Type != 1 && this.Pages[PageIndex].Type != 2)
                            {
                                blockCount = random.Next(this.settings.MacrosesCategoryMenuBlockMainMin, this.settings.MacrosesCategoryMenuBlockMainMax);
                            }
                            while (MainContent.Contains("[CATEGORYMENUBLOCK]") && MainContent.Contains("[/CATEGORYMENUBLOCK]"))
                            {
                                content = MainContent.Substring(MainContent.IndexOf("[CATEGORYMENUBLOCK]"), MainContent.IndexOf("[/CATEGORYMENUBLOCK]") + 20 - MainContent.IndexOf("[CATEGORYMENUBLOCK]"));

                                blockStartIndex = MainContent.IndexOf("[CATEGORYMENUBLOCK]");

                                MainContent = MainContent.Remove(blockStartIndex, content.Length);

                                content = content.Replace("[CATEGORYMENUBLOCK]", string.Empty).Replace("[/CATEGORYMENUBLOCK]", string.Empty);
                                string tempString = content;

                                for (int i = 0; i < blockCount - 1; i++)
                                {
                                    content += tempString;
                                }

                                // Замена макросов
                                this.ReplaceKeywordTokens(PageIndex, ref content);
                                this.ReplaceRegularTokens(PageIndex, ref content);
                                this.ReplaceKeywordTokens(PageIndex, ref content);

                                MainContent = MainContent.Insert(blockStartIndex, content);
                            }
                        }
                        else
                        {
                            // Замена макросов категорий на макросы меню
                            MainContent = MainContent.Replace("[CATEGORYMENUBLOCK]", "[MENUBLOCK]");
                            MainContent = MainContent.Replace("[CATEGORYMENULINK]", "[MENULINK]");
                            MainContent = MainContent.Replace("[CATEGORYMENUKEYWORD]", "[MENUKEYWORD]");
                            MainContent = MainContent.Replace("[CATEGORYMENUBKEYWORD]", "[MENUBKEYWORD]");
                            MainContent = MainContent.Replace("[/CATEGORYMENUBLOCK]", "[/MENUBLOCK]");
                        }

                        break;
                    }

                // [STATICBLOCK]
                case 3:
                    {
                        if (this.settings.StaticPages)
                        {
                            blockCount = this.settings.StaticPagesList.Length;
                            while (MainContent.Contains("[STATICBLOCK]") && MainContent.Contains("[/STATICBLOCK]"))
                            {
                                content = MainContent.Substring(MainContent.IndexOf("[STATICBLOCK]"), MainContent.IndexOf("[/STATICBLOCK]") + 14 - MainContent.IndexOf("[STATICBLOCK]"));

                                blockStartIndex = MainContent.IndexOf("[STATICBLOCK]");

                                MainContent = MainContent.Remove(blockStartIndex, content.Length);

                                content = content.Replace("[STATICBLOCK]", string.Empty).Replace("[/STATICBLOCK]", string.Empty);
                                string tempString = content;

                                for (int i = 0; i < blockCount - 1; i++)
                                {
                                    content += tempString;
                                }

                                // Замена макросов
                                this.ReplaceKeywordTokens(PageIndex, ref content);
                                this.ReplaceRegularTokens(PageIndex, ref content);
                                this.ReplaceKeywordTokens(PageIndex, ref content);

                                MainContent = MainContent.Insert(blockStartIndex, content);
                            }
                        }
                        else
                        {
                            // Замена макросов внешних ссылок
                            MainContent = MainContent.Replace("[STATICBLOCK]", "[MENUBLOCK]");
                            MainContent = MainContent.Replace("[STATICLINK]", "[MENULINK]");
                            MainContent = MainContent.Replace("[STATICKEYWORD]", "[MENUKEYWORD]");
                            MainContent = MainContent.Replace("[STATICBKEYWORD]", "[MENUBKEYWORD]");
                            MainContent = MainContent.Replace("[/STATICBLOCK]", "[/MENUBLOCK]");
                        }

                        break;
                    }

                // [NETBLOCK]
                case 5:
                    {
                        if (this.settings.LinksExternal)
                        {
                            if (this.Pages[PageIndex].Type == 0)
                            {
                                if (this.settings.MacrosesNetBlockMainMin == 0 && this.settings.MacrosesNetBlockMainMax == 0)
                                {
                                    blockCount = this.settings.LinksExternalList.Length;
                                }
                                else
                                {
                                    blockCount = random.Next(this.settings.MacrosesNetBlockMainMin, this.settings.MacrosesNetBlockMainMax);
                                }
                            }
                            else
                            {
                                if (this.settings.MacrosesNetBlockPageMin == 0 && this.settings.MacrosesNetBlockPageMax == 0)
                                {
                                    blockCount = this.settings.LinksExternalList.Length;
                                }
                                else
                                {
                                    blockCount = random.Next(this.settings.MacrosesNetBlockPageMin, this.settings.MacrosesNetBlockPageMax);
                                }
                            }

                            while (MainContent.Contains("[NETBLOCK]") && MainContent.Contains("[/NETBLOCK]"))
                            {
                                content = MainContent.Substring(MainContent.IndexOf("[NETBLOCK]"), MainContent.IndexOf("[/NETBLOCK]") + 11 - MainContent.IndexOf("[NETBLOCK]"));

                                blockStartIndex = MainContent.IndexOf("[NETBLOCK]");

                                MainContent = MainContent.Remove(blockStartIndex, content.Length);

                                content = content.Replace("[NETBLOCK]", string.Empty).Replace("[/NETBLOCK]", string.Empty);
                                string tempString = content;

                                for (int i = 0; i < blockCount - 1; i++)
                                {
                                    content += tempString;
                                }

                                // Замена макросов
                                this.ReplaceKeywordTokens(PageIndex, ref content);
                                this.ReplaceRegularTokens(PageIndex, ref content);
                                this.ReplaceKeywordTokens(PageIndex, ref content);

                                MainContent = MainContent.Insert(blockStartIndex, content);
                            }
                        }
                        else
                        {
                            // Замена макросов статических страниц на макросы меню
                            MainContent = MainContent.Replace("[NETBLOCK]", "[MENUBLOCK]");
                            MainContent = MainContent.Replace("[NETLINK]", "[MENULINK]");
                            MainContent = MainContent.Replace("[NETKEYWORD]", "[MENUKEYWORD]");
                            MainContent = MainContent.Replace("[NETBKEYWORD]", "[MENUBKEYWORD]");
                            MainContent = MainContent.Replace("[/NETBLOCK]", "[/MENUBLOCK]");
                        }

                        break;
                    }

                // [MENUBLOCK]
                case 4:
                    {
                        if (this.Pages[PageIndex].Type != 1 && this.Pages[PageIndex].Type != 2)
                        {
                            blockCount = random.Next(this.settings.MacrosesMenuBlockMainMin, this.settings.MacrosesMenuBlockMainMax);
                        }
                        else
                        {
                            if (this.settings.MacrosesMenuBlockPageMin == 0 && this.settings.MacrosesMenuBlockPageMax == 0)
                            {
                                blockCount = 0;
                                for (int k = 0; k < this.Pages.Count; k++)
                                {
                                    if (this.Pages[k].Type == 1)
                                    {
                                        blockCount++;
                                    }
                                }
                            }
                            else
                            {
                                blockCount = random.Next(this.settings.MacrosesMenuBlockPageMin, this.settings.MacrosesMenuBlockPageMax);
                            }
                        }
                        while (MainContent.Contains("[MENUBLOCK]") && MainContent.Contains("[/MENUBLOCK]"))
                        {
                            content = MainContent.Substring(MainContent.IndexOf("[MENUBLOCK]"), MainContent.IndexOf("[/MENUBLOCK]") + 12 - MainContent.IndexOf("[MENUBLOCK]"));

                            blockStartIndex = MainContent.IndexOf("[MENUBLOCK]");

                            MainContent = MainContent.Remove(blockStartIndex, content.Length);

                            content = content.Replace("[MENUBLOCK]", string.Empty).Replace("[/MENUBLOCK]", string.Empty);
                            string tempString = content;

                            for (int i = 0; i < blockCount - 1; i++)
                            {
                                content += tempString;
                            }

                            //Замена макросов
                            ReplaceKeywordTokens(PageIndex, ref content);
                            ReplaceRegularTokens(PageIndex, ref content);
                            ReplaceKeywordTokens(PageIndex, ref content);

                            MainContent = MainContent.Insert(blockStartIndex, content);
                        }
                        break;
                    }
                //[USERBLOCK1]
                case 6:
                    {
                        if (this.Pages[PageIndex].Type != 1 && this.Pages[PageIndex].Type != 2)
                        {
                            blockCount = random.Next(this.settings.MacrosesUserBlock1MainMin, this.settings.MacrosesUserBlock1MainMax);
                        }
                        else
                        {
                            blockCount = random.Next(this.settings.MacrosesUserBlock1PageMin, this.settings.MacrosesUserBlock1PageMax);
                        }
                        while (MainContent.Contains("[USERBLOCK1]") && MainContent.Contains("[/USERBLOCK1]"))
                        {
                            content = MainContent.Substring(MainContent.IndexOf("[USERBLOCK1]"), MainContent.IndexOf("[/USERBLOCK1]") + 13 - MainContent.IndexOf("[USERBLOCK1]"));

                            blockStartIndex = MainContent.IndexOf("[USERBLOCK1]");

                            MainContent = MainContent.Remove(blockStartIndex, content.Length);

                            content = content.Replace("[USERBLOCK1]", string.Empty).Replace("[/USERBLOCK1]", string.Empty);
                            string tempString = content;

                            for (int i = 0; i < blockCount - 1; i++)
                            {
                                content += tempString;
                            }

                            //Замена макросов
                            ReplaceKeywordTokens(PageIndex, ref content);
                            ReplaceRegularTokens(PageIndex, ref content);
                            ReplaceKeywordTokens(PageIndex, ref content);

                            MainContent = MainContent.Insert(blockStartIndex, content);
                        }
                        break;
                    }
                //[USERBLOCK2]
                case 7:
                    {
                        if (this.Pages[PageIndex].Type != 1 && this.Pages[PageIndex].Type != 2)
                        {
                            blockCount = random.Next(this.settings.MacrosesUserBlock2MainMin, this.settings.MacrosesUserBlock2MainMax);
                        }
                        else
                        {
                            blockCount = random.Next(this.settings.MacrosesUserBlock2PageMin, this.settings.MacrosesUserBlock2PageMax);
                        }
                        while (MainContent.Contains("[USERBLOCK2]") && MainContent.Contains("[/USERBLOCK2]"))
                        {
                            content = MainContent.Substring(MainContent.IndexOf("[USERBLOCK2]"), MainContent.IndexOf("[/USERBLOCK2]") + 13 - MainContent.IndexOf("[USERBLOCK2]"));

                            blockStartIndex = MainContent.IndexOf("[USERBLOCK2]");

                            MainContent = MainContent.Remove(blockStartIndex, content.Length);

                            content = content.Replace("[USERBLOCK2]", string.Empty).Replace("[/USERBLOCK2]", string.Empty);
                            string tempString = content;

                            for (int i = 0; i < blockCount - 1; i++)
                            {
                                content += tempString;
                            }

                            //Замена макросов
                            ReplaceKeywordTokens(PageIndex, ref content);
                            ReplaceRegularTokens(PageIndex, ref content);
                            ReplaceKeywordTokens(PageIndex, ref content);

                            MainContent = MainContent.Insert(blockStartIndex, content);
                        }

                        break;
                    }

                // [USERBLOCK3]
                case 8:
                    {
                        if (this.Pages[PageIndex].Type != 1 && this.Pages[PageIndex].Type != 2)
                        {
                            blockCount = random.Next(this.settings.MacrosesUserBlock3MainMin, this.settings.MacrosesUserBlock3MainMax);
                        }
                        else
                        {
                            blockCount = random.Next(this.settings.MacrosesUserBlock3PageMin, this.settings.MacrosesUserBlock3PageMax);
                        }
                        while (MainContent.Contains("[USERBLOCK3]") && MainContent.Contains("[/USERBLOCK3]"))
                        {
                            content = MainContent.Substring(MainContent.IndexOf("[USERBLOCK3]"), MainContent.IndexOf("[/USERBLOCK3]") + 13 - MainContent.IndexOf("[USERBLOCK3]"));

                            blockStartIndex = MainContent.IndexOf("[USERBLOCK3]");

                            MainContent = MainContent.Remove(blockStartIndex, content.Length);

                            content = content.Replace("[USERBLOCK3]", string.Empty).Replace("[/USERBLOCK3]", string.Empty);
                            string tempString = content;

                            for (int i = 0; i < blockCount - 1; i++)
                            {
                                content += tempString;
                            }

                            // Замена макросов
                            this.ReplaceKeywordTokens(PageIndex, ref content);
                            this.ReplaceRegularTokens(PageIndex, ref content);
                            this.ReplaceKeywordTokens(PageIndex, ref content);

                            MainContent = MainContent.Insert(blockStartIndex, content);
                        }

                        break;
                    }

                // [USERBLOCK4]
                case 9:
                    {
                        if (this.Pages[PageIndex].Type != 1 && this.Pages[PageIndex].Type != 2)
                        {
                            blockCount = random.Next(this.settings.MacrosesUserBlock4MainMin, this.settings.MacrosesUserBlock4MainMax);
                        }
                        else
                        {
                            blockCount = random.Next(this.settings.MacrosesUserBlock4PageMin, this.settings.MacrosesUserBlock4PageMax);
                        }
                        while (MainContent.Contains("[USERBLOCK4]") && MainContent.Contains("[/USERBLOCK4]"))
                        {
                            content = MainContent.Substring(MainContent.IndexOf("[USERBLOCK4]"), MainContent.IndexOf("[/USERBLOCK4]") + 13 - MainContent.IndexOf("[USERBLOCK4]"));

                            blockStartIndex = MainContent.IndexOf("[USERBLOCK4]");

                            MainContent = MainContent.Remove(blockStartIndex, content.Length);

                            content = content.Replace("[USERBLOCK4]", string.Empty).Replace("[/USERBLOCK4]", string.Empty);
                            string tempString = content;

                            for (int i = 0; i < blockCount - 1; i++)
                            {
                                content += tempString;
                            }

                            //Замена макросов
                            ReplaceKeywordTokens(PageIndex, ref content);
                            ReplaceRegularTokens(PageIndex, ref content);
                            ReplaceKeywordTokens(PageIndex, ref content);

                            MainContent = MainContent.Insert(blockStartIndex, content);
                        }
                        break;
                    }
                //[USERBLOCK5]
                case 10:
                    {
                        if (this.Pages[PageIndex].Type != 1 && this.Pages[PageIndex].Type != 2)
                        {
                            blockCount = random.Next(this.settings.MacrosesUserBlock5MainMin, this.settings.MacrosesUserBlock5MainMax);
                        }
                        else
                        {
                            blockCount = random.Next(this.settings.MacrosesUserBlock5PageMin, this.settings.MacrosesUserBlock5PageMax);
                        }
                        while (MainContent.Contains("[USERBLOCK5]") && MainContent.Contains("[/USERBLOCK5]"))
                        {
                            content = MainContent.Substring(MainContent.IndexOf("[USERBLOCK5]"), MainContent.IndexOf("[/USERBLOCK5]") + 13 - MainContent.IndexOf("[USERBLOCK5]"));

                            blockStartIndex = MainContent.IndexOf("[USERBLOCK5]");

                            MainContent = MainContent.Remove(blockStartIndex, content.Length);

                            content = content.Replace("[USERBLOCK5]", string.Empty).Replace("[/USERBLOCK5]", string.Empty);
                            string tempString = content;

                            for (int i = 0; i < blockCount - 1; i++)
                            {
                                content += tempString;
                            }

                            //Замена макросов
                            ReplaceKeywordTokens(PageIndex, ref content);
                            ReplaceRegularTokens(PageIndex, ref content);
                            ReplaceKeywordTokens(PageIndex, ref content);

                            MainContent = MainContent.Insert(blockStartIndex, content);
                        }
                        break;
                    }
                //[USERBLOCK6]
                case 11:
                    {
                        if (this.Pages[PageIndex].Type != 1 && this.Pages[PageIndex].Type != 2)
                        {
                            blockCount = random.Next(this.settings.MacrosesUserBlock6MainMin, this.settings.MacrosesUserBlock6MainMax);
                        }
                        else
                        {
                            blockCount = random.Next(this.settings.MacrosesUserBlock6PageMin, this.settings.MacrosesUserBlock6PageMax);
                        }
                        while (MainContent.Contains("[USERBLOCK6]") && MainContent.Contains("[/USERBLOCK6]"))
                        {
                            content = MainContent.Substring(MainContent.IndexOf("[USERBLOCK6]"), MainContent.IndexOf("[/USERBLOCK6]") + 13 - MainContent.IndexOf("[USERBLOCK6]"));

                            blockStartIndex = MainContent.IndexOf("[USERBLOCK6]");

                            MainContent = MainContent.Remove(blockStartIndex, content.Length);

                            content = content.Replace("[USERBLOCK6]", string.Empty).Replace("[/USERBLOCK6]", string.Empty);
                            string tempString = content;

                            for (int i = 0; i < blockCount - 1; i++)
                            {
                                content += tempString;
                            }

                            //Замена макросов
                            ReplaceKeywordTokens(PageIndex, ref content);
                            ReplaceRegularTokens(PageIndex, ref content);
                            ReplaceKeywordTokens(PageIndex, ref content);

                            MainContent = MainContent.Insert(blockStartIndex, content);
                        }
                        break;
                    }
                //[MAPBLOCK]
                case 12:
                    {
                        blockCount = this.Pages[PageIndex].HTMLSiteMapPageCount;
                        while (MainContent.Contains("[MAPBLOCK]") && MainContent.Contains("[/MAPBLOCK]"))
                        {
                            content = MainContent.Substring(MainContent.IndexOf("[MAPBLOCK]"), MainContent.IndexOf("[/MAPBLOCK]") + 11 - MainContent.IndexOf("[MAPBLOCK]"));

                            blockStartIndex = MainContent.IndexOf("[MAPBLOCK]");

                            MainContent = MainContent.Remove(blockStartIndex, content.Length);

                            content = content.Replace("[MAPBLOCK]", string.Empty).Replace("[/MAPBLOCK]", string.Empty);
                            string tempString = content;

                            for (int i = 0; i < blockCount - 1; i++)
                            {
                                content += tempString;
                            }

                            //Замена макросов
                            ReplaceKeywordTokens(PageIndex, ref content);
                            ReplaceRegularTokens(PageIndex, ref content);
                            ReplaceKeywordTokens(PageIndex, ref content);

                            MainContent = MainContent.Insert(blockStartIndex, content);
                        }
                        break;
                    }

                // [CUSTOMBLOCK]
                case 13:
                    {
                        if (SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages.Count > 0)
                        {
                            blockCount = SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages.Count;
                            while (MainContent.Contains("[CUSTOMBLOCK]") && MainContent.Contains("[/CUSTOMBLOCK]"))
                            {
                                content = MainContent.Substring(MainContent.IndexOf("[CUSTOMBLOCK]"), MainContent.IndexOf("[/CUSTOMBLOCK]") + 14 - MainContent.IndexOf("[CUSTOMBLOCK]"));

                                blockStartIndex = MainContent.IndexOf("[CUSTOMBLOCK]");

                                MainContent = MainContent.Remove(blockStartIndex, content.Length);

                                content = content.Replace("[CUSTOMBLOCK]", string.Empty).Replace("[/CUSTOMBLOCK]", string.Empty);
                                string tempString = content;

                                for (int i = 0; i < blockCount - 1; i++)
                                {
                                    content += tempString;
                                }

                                // Замена макросов
                                this.ReplaceKeywordTokens(PageIndex, ref content);
                                this.ReplaceRegularTokens(PageIndex, ref content);
                                this.ReplaceKeywordTokens(PageIndex, ref content);
                                MainContent = MainContent.Insert(blockStartIndex, content);
                            }
                        }
                        else
                        {
                            // Замена макросов внешних ссылок
                            MainContent = MainContent.Replace("[CUSTOMBLOCK]", "[MENUBLOCK]");
                            MainContent = MainContent.Replace("[CUSTOMLINK]", "[MENULINK]");
                            MainContent = MainContent.Replace("[CUSTOMKEYWORD]", "[MENUKEYWORD]");
                            MainContent = MainContent.Replace("[CUSTOMBKEYWORD]", "[MENUBKEYWORD]");
                            MainContent = MainContent.Replace("[/CUSTOMBLOCK]", "[/MENUBLOCK]");
                        }

                        break;
                    }
            }
        }

        private void ReplaceTagTokens(int PageIndex, ref string Content)
        {
            // [TAG]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [TAG]...\r\n");
            }

            if (this.Pages[PageIndex].Type < 2)
            {
                Content = Content.Replace("[TAG]", this.Pages[PageIndex].Tag);
            }
            else
            {
                Content = Content.Replace("[TAG]", "[RTAG]");
            }

            Content = this.ReplaceRandomTagTokens(Content);
        }

        private string SelectRandomTag(List<string> UsedPageTags)
        {
            string selectedTag = this.Tags.Count == 0 ? string.Empty : this.Tags[this.random.Next(this.usedTags)];

            if (this.Tags.Count != 0 && this.Tags.Count == UsedPageTags.Count)
            {
                UsedPageTags.Clear();
            }

            if (selectedTag != string.Empty && !UsedPageTags.Contains(selectedTag))
            {
                UsedPageTags.Add(selectedTag);
            }
            else if (selectedTag != string.Empty && UsedPageTags.Contains(selectedTag))
            {
                while (UsedPageTags.Contains(selectedTag))
                {
                    selectedTag = this.Tags[this.random.Next(this.usedTags)];
                }
            }

            return selectedTag;
        }

        private string ReplaceRandomTagTokens(string Content)
        {
            // [RTAGLINK]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RTAGLINK]...\r\n");
            }

            int startPosition = 0;

            while (Content.Contains("[RTAGLINK]"))
            {
                startPosition = Content.IndexOf("[RTAGLINK]");
                Content = Content.Remove(startPosition, 10);

                int pageIndex = this.random.Next(this.Pages.Count);
                while (this.Pages[pageIndex].Type >= 2)
                {
                    pageIndex = this.random.Next(this.Pages.Count);
                }

                Content = Content.Insert(startPosition, this.Pages[pageIndex].URL);
            }

            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [NEXTRTAGLINK], [RTAG], [RTAGPRECOPY], [RTAGPOSTCOPY]...\r\n");
            }

            startPosition = 0;

            List<string> usedPageTags = new List<string>();

            // Replace tokens before [NEXTRTAGLINK]
            if (Content.Contains("[NEXTRTAGLINK]"))
            {
                while (Content.Contains("[RTAG]") &&
                       Content.IndexOf("[RTAG]") < Content.IndexOf("[NEXTRTAGLINK]"))
                {
                    startPosition = Content.IndexOf("[RTAG]");
                    Content = Content.Remove(startPosition, 6);

                    string selectedTag = SelectRandomTag(usedPageTags);

                    Content = Content.Insert(startPosition, selectedTag);
                }
            }

            // Replace tokens after [NEXTRTAGLINK]
            while (Content.Contains("[RTAG]"))
            {
                try
                {
                    int index = this.random.Next(this.usedTags);
                    string selectedTag = string.Empty;

                    // Search for first token
                    KeyValuePair<string, int> firstToken = new KeyValuePair<string, int>(string.Empty, Content.Length);
                    string[] tokens = new string[] { "[RTAG]" };

                    for (int i = 0; i < tokens.Length; i++)
                    {
                        if (Content.Contains(tokens[i]))
                        {
                            int tokenIndex = Content.IndexOf(tokens[i]);
                            if (tokenIndex < firstToken.Value)
                            {
                                firstToken = new KeyValuePair<string, int>(tokens[i], tokenIndex);
                            }
                        }
                    }

                    bool link = Content.Contains("[NEXTRTAGLINK]") &&
                                Content.IndexOf("[NEXTRTAGLINK]") < Content.IndexOf(firstToken.Key);

                    switch (firstToken.Key)
                    {
                        case "[RTAG]":
                            {
                                selectedTag = SelectRandomTag(usedPageTags);

                                break;
                            }
                    }


                    int sPosition = 0;

                    // [RTAGPRECOPY]
                    while (Content.IndexOf("[RTAGPRECOPY]") < Content.IndexOf(firstToken.Key) &&
                           Content.IndexOf("[RTAGPRECOPY]") != -1)
                    {
                        sPosition = Content.IndexOf("[RTAGPRECOPY]");
                        Content = Content.Remove(sPosition, 13);
                        Content = Content.Insert(sPosition, selectedTag);
                    }

                    // [RTAGPOSTCOPY]
                    while (Content.IndexOf("[RTAGPOSTCOPY]") > Content.IndexOf(firstToken.Key) &&
                           Content.IndexOf("[RTAGPOSTCOPY]") != -1)
                    {
                        if (Content.IndexOf("[NEXTRTAGLINK]", Content.IndexOf(firstToken.Key)) != -1)
                        {
                            while (Content.IndexOf("[RTAGPOSTCOPY]", Content.IndexOf(firstToken.Key)) <
                                   Content.IndexOf("[NEXTRTAGLINK]", Content.IndexOf(firstToken.Key)))
                            {
                                sPosition = Content.IndexOf("[RTAGPOSTCOPY]");
                                Content = Content.Remove(sPosition, 14);
                                Content = Content.Insert(sPosition, selectedTag);
                            }

                            break;
                        }

                        sPosition = Content.IndexOf("[RTAGPOSTCOPY]");
                        Content = Content.Remove(sPosition, 14);
                        Content = Content.Insert(sPosition, selectedTag);
                    }

                    // Replace token
                    startPosition = Content.IndexOf(firstToken.Key);
                    Content = Content.Remove(startPosition, firstToken.Key.Length);
                    Content = Content.Insert(startPosition, selectedTag);

                    // Replace link
                    if (link)
                    {
                        int linkIndex = Content.IndexOf("[NEXTRTAGLINK]");
                        Content = Content.Remove(linkIndex, 14);

                        // Поиск страницы
                        int pageIndex = 0;
                        for (int k = 0; k < this.Pages.Count; k++)
                        {
                            if (this.Pages[k].Type < 2)
                            {
                                if (this.Pages[k].Tag == selectedTag)
                                {
                                    pageIndex = k;
                                    break;
                                }
                            }
                        }

                        // Insert
                        Content = Content.Insert(linkIndex, this.settings.LinksRelativeURLs ? this.Pages[pageIndex].RelativeURL : this.Pages[pageIndex].URL);
                    }
                }
                catch (Exception)
                {
                }
            }

            return Content.Replace("[NEXTRTAGLINK]", "[MAINLINK]");
        }

        private void ReplaceKeywordTokens(int PageIndex, ref string Content)
        {
            int startPosition = 0;

            // [MAINKEYWORD]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [MAINKEYWORD]...\r\n");
            }

            while (Content.Contains("[MAINKEYWORD]"))
            {
                startPosition = Content.IndexOf("[MAINKEYWORD]");
                Content = Content.Remove(startPosition, 13);
                Content = Content.Insert(startPosition, this.Pages[0].Keywords[this.random.Next(this.Pages[0].Keywords.Count)]);
            }

            // [MAINBKEYWORD]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [MAINBKEYWORD]...\r\n");
            }

            while (Content.Contains("[MAINBKEYWORD]"))
            {
                startPosition = Content.IndexOf("[MAINBKEYWORD]");
                Content = Content.Remove(startPosition, 14);
                string keyword = this.Pages[0].Keywords[this.random.Next(this.Pages[0].Keywords.Count)];
                if (keyword.Length > 1)
                {
                    Content = Content.Insert(startPosition, keyword.Substring(0, 1).ToUpper() + keyword.Substring(1));
                }
                else
                {
                    Content = Content.Insert(startPosition, keyword.ToUpper());
                }
            }

            // [MAINKEYWORDCOM]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [MAINKEYWORDCOM]...\r\n");
            }

            while (Content.Contains("[MAINKEYWORDCOM]"))
            {
                startPosition = Content.IndexOf("[MAINKEYWORDCOM]");
                Content = Content.Remove(startPosition, 16);
                Content = Content.Insert(startPosition, this.Pages[0].Keywords[this.random.Next(this.Pages[0].Keywords.Count)].Replace(" ", ", "));
            }

            // [MAINBKEYWORDCOM]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [MAINBKEYWORDCOM]...\r\n");
            }

            while (Content.Contains("[MAINBKEYWORDCOM]"))
            {
                startPosition = Content.IndexOf("[MAINBKEYWORDCOM]");
                Content = Content.Remove(startPosition, 17);
                string keyword = this.Pages[0].Keywords[this.random.Next(this.Pages[0].Keywords.Count)];
                if (keyword.Length > 1)
                {
                    Content = Content.Insert(startPosition,
                                             keyword.Substring(0, 1).ToUpper() + keyword.Substring(1).Replace(" ", ", "));
                }
                else
                {
                    Content = Content.Insert(startPosition, keyword.ToUpper());
                }
            }

            // If page doesn't contains keywords - replace keyword tokens and return
            if (this.Pages[PageIndex].Keywords.Count == 0)
            {
                Content = Content.Replace("[KEYWORD]", string.Empty).Replace("[BKEYWORD]", string.Empty).Replace("[REVERSEKEYWORD]", string.Empty)
                    .Replace("[BREVERSEKEYWORD]", string.Empty).Replace("[BBKEYWORD]", string.Empty).Replace("[BSBKEYWORD]", string.Empty)
                    .Replace("[KEYWORDCOM]", string.Empty).Replace("[BKEYWORDCOM]", string.Empty).Replace("[PLUSKEYWORD]", string.Empty)
                    .Replace("[MINUSKEYWORD]", string.Empty).Replace("[UNDERKEYWORD]", string.Empty).Replace("[ALLKEYWORDS]", string.Empty)
                    .Replace("[ALLBKEYWORDS]", string.Empty).Replace("[ALLKEYWORDSCOM]", string.Empty).Replace("[ALLBKEYWORDSCOM]", string.Empty);
                return;
            }

            // [KEYWORD]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [KEYWORD]...\r\n");
            }

            while (Content.Contains("[KEYWORD]"))
            {
                startPosition = Content.IndexOf("[KEYWORD]");
                Content = Content.Remove(startPosition, 9);
                Content = Content.Insert(startPosition,
                                         this.Pages[PageIndex].Keywords[
                                             this.random.Next(this.Pages[PageIndex].Keywords.Count)]);
            }

            // [BKEYWORD]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [BKEYWORD]...\r\n");
            }

            while (Content.Contains("[BKEYWORD]"))
            {
                startPosition = Content.IndexOf("[BKEYWORD]");
                Content = Content.Remove(startPosition, 10);
                string keyword = this.Pages[PageIndex].Keywords[this.random.Next(this.Pages[PageIndex].Keywords.Count)];
                if (keyword.Length > 1)
                {
                    Content = Content.Insert(startPosition, keyword.Substring(0, 1).ToUpper() + keyword.Substring(1));
                }
                else
                {
                    Content = Content.Insert(startPosition, keyword.ToUpper());
                }
            }

            // [REVERSEKEYWORD]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [REVERSEKEYWORD]...\r\n");
            }

            while (Content.Contains("[REVERSEKEYWORD]"))
            {
                startPosition = Content.IndexOf("[REVERSEKEYWORD]");
                Content = Content.Remove(startPosition, 16);
                string[] parts =
                    this.Pages[PageIndex].Keywords[this.random.Next(this.Pages[PageIndex].Keywords.Count)]
                        .Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                string keyword = string.Empty;
                for (int i = 0; i < parts.Length; i++)
                {
                    keyword += parts[parts.Length - 1 - i] + " ";
                }

                Content = Content.Insert(startPosition, keyword.Trim());
            }

            // [BREVERSEKEYWORD]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [BREVERSEKEYWORD]...\r\n");
            }

            while (Content.Contains("[BREVERSEKEYWORD]"))
            {
                startPosition = Content.IndexOf("[BREVERSEKEYWORD]");
                Content = Content.Remove(startPosition, 17);
                string[] parts =
                    this.Pages[PageIndex].Keywords[this.random.Next(this.Pages[PageIndex].Keywords.Count)]
                        .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string keyword = string.Empty;
                for (int i = 0; i < parts.Length; i++)
                {
                    keyword += parts[parts.Length - 1 - i] + " ";
                }

                Content = Content.Insert(startPosition, keyword.Substring(0, 1).ToUpper() + keyword.Substring(1));
            }

            // [BBKEYWORD]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [BBKEYWORD]...\r\n");
            }

            while (Content.Contains("[BBKEYWORD]"))
            {
                startPosition = Content.IndexOf("[BBKEYWORD]");
                Content = Content.Remove(startPosition, 11);
                string keyword = this.Pages[PageIndex].Keywords[this.random.Next(this.Pages[PageIndex].Keywords.Count)];
                if (keyword.Contains(" "))
                {
                    string[] tempKeywords = keyword.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    keyword = string.Empty;
                    for (int i = 0; i < tempKeywords.Length; i++)
                    {
                        if (tempKeywords[i].Length > 1)
                        {
                            keyword += tempKeywords[i].Substring(0, 1).ToUpper() + tempKeywords[i].Substring(1) + " ";
                        }
                        else
                        {
                            keyword += tempKeywords[i].ToUpper() + " ";
                        }
                    }

                    keyword = keyword.Trim();
                }
                else
                {
                    keyword = keyword.Substring(0, 1).ToUpper() + keyword.Substring(1);
                }

                Content = Content.Insert(startPosition, keyword);
            }

            // [BSBKEYWORD]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [BSBKEYWORD]...\r\n");
            }

            while (Content.Contains("[BSBKEYWORD]"))
            {
                startPosition = Content.IndexOf("[BSBKEYWORD]");
                Content = Content.Remove(startPosition, 12);
                string keyword = this.Pages[PageIndex].Keywords[this.random.Next(this.Pages[PageIndex].Keywords.Count)];
                if (keyword.Contains(" "))
                {
                    string[] tempKeywords = keyword.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    keyword = string.Empty;
                    for (int i = 0; i < tempKeywords.Length; i++)
                    {
                        if (tempKeywords[i].Length > 2)
                        {
                            keyword += tempKeywords[i].Substring(0, 1).ToUpper() + tempKeywords[i].Substring(1) + " ";
                        }
                        else
                        {
                            keyword += tempKeywords[i] + " ";
                        }
                    }

                    keyword = keyword.Trim();
                }
                else
                {
                    keyword = keyword.Substring(0, 1).ToUpper() + keyword.Substring(1);
                }

                Content = Content.Insert(startPosition, keyword);
            }

            // [KEYWORDCOM]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [KEYWORDCOM]...\r\n");
            }

            while (Content.Contains("[KEYWORDCOM]"))
            {
                startPosition = Content.IndexOf("[KEYWORDCOM]");
                Content = Content.Remove(startPosition, 12);
                Content = Content.Insert(startPosition, this.Pages[PageIndex].Keywords[this.random.Next(this.Pages[PageIndex].Keywords.Count)].Replace(" ", ", "));
            }

            // [BKEYWORDCOM]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [BKEYWORDCOM]...\r\n");
            }

            while (Content.Contains("[BKEYWORDCOM]"))
            {
                startPosition = Content.IndexOf("[BKEYWORDCOM]");
                Content = Content.Remove(startPosition, 13);
                string keyword = this.Pages[PageIndex].Keywords[this.random.Next(this.Pages[PageIndex].Keywords.Count)];
                if (keyword.Length > 1)
                {
                    Content = Content.Insert(startPosition,
                                             keyword.Substring(0, 1).ToUpper() + keyword.Substring(1).Replace(" ", ", "));
                }
                else
                {
                    Content = Content.Insert(startPosition, keyword.ToUpper());
                }
            }

            // [PLUSKEYWORD]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [PLUSKEYWORD]...\r\n");
            }

            while (Content.Contains("[PLUSKEYWORD]"))
            {
                startPosition = Content.IndexOf("[PLUSKEYWORD]");
                Content = Content.Remove(startPosition, 13);
                Content = Content.Insert(startPosition, this.Pages[PageIndex].Keywords[this.random.Next(
                    this.Pages[PageIndex].Keywords.Count)].Replace(" ", "+").Replace("!", string.Empty).Replace("?", string.Empty)
                    .Replace("&", string.Empty).Replace("\"", string.Empty).Replace("'", string.Empty).Replace("/", string.Empty).Replace("\\", string.Empty));
            }

            // [MINUSKEYWORD]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [MINUSKEYWORD]...\r\n");
            }

            while (Content.Contains("[MINUSKEYWORD]"))
            {
                startPosition = Content.IndexOf("[MINUSKEYWORD]");
                Content = Content.Remove(startPosition, 14);
                Content = Content.Insert(startPosition, this.Pages[PageIndex].Keywords[this.random.Next(
                    this.Pages[PageIndex].Keywords.Count)].Replace(" ", "-").Replace("!", string.Empty).Replace("?", string.Empty)
                    .Replace("&", string.Empty).Replace("\"", string.Empty).Replace("'", string.Empty).Replace("/", string.Empty).Replace("\\", string.Empty));
            }

            // [UNDERKEYWORD]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [UNDERKEYWORD]...\r\n");
            }

            while (Content.Contains("[UNDERKEYWORD]"))
            {
                startPosition = Content.IndexOf("[UNDERKEYWORD]");
                Content = Content.Remove(startPosition, 14);
                Content = Content.Insert(startPosition, this.Pages[PageIndex].Keywords[this.random.Next(
                    this.Pages[PageIndex].Keywords.Count)].Replace(" ", "_").Replace("!", string.Empty).Replace("?", string.Empty)
                    .Replace("&", string.Empty).Replace("\"", string.Empty).Replace("'", string.Empty).Replace("/", string.Empty).Replace("\\", string.Empty));
            }

            // [ALLKEYWORDS]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [ALLKEYWORDS]...\r\n");
            }
            if (Content.Contains("[ALLKEYWORDS]"))
            {
                StringBuilder allKeywords = new StringBuilder(100);
                for (int i = 0; i < this.Pages[PageIndex].Keywords.Count; i++)
                {
                    allKeywords.Append(this.Pages[PageIndex].Keywords[i]);
                    if ((i + 1) != this.Pages[PageIndex].Keywords.Count)
                    {
                        allKeywords.Append(" ");
                    }
                }

                Content = Content.Replace("[ALLKEYWORDS]", allKeywords.ToString());
            }

            // [ALLBKEYWORDS]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [ALLBKEYWORDS]...\r\n");
            }

            if (Content.Contains("[ALLBKEYWORDS]"))
            {
                StringBuilder allKeywords = new StringBuilder(100);
                for (int i = 0; i < this.Pages[PageIndex].Keywords.Count; i++)
                {
                    if (this.Pages[PageIndex].Keywords[i].Length > 1)
                    {
                        allKeywords.Append(this.Pages[PageIndex].Keywords[i].Substring(0, 1).ToUpper() + this.Pages[PageIndex].Keywords[i].Substring(1));
                    }
                    else
                    {
                        allKeywords.Append(this.Pages[PageIndex].Keywords[i].ToUpper());
                    }

                    if ((i + 1) != this.Pages[PageIndex].Keywords.Count)
                    {
                        allKeywords.Append(" ");
                    }
                }

                Content = Content.Replace("[ALLBKEYWORDS]", allKeywords.ToString());
            }

            // [ALLKEYWORDSCOM]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [ALLKEYWORDSCOM]...\r\n");
            }

            if (Content.Contains("[ALLKEYWORDSCOM]"))
            {
                StringBuilder allKeywords = new StringBuilder(100);
                for (int i = 0; i < this.Pages[PageIndex].Keywords.Count; i++)
                {
                    allKeywords.Append(this.Pages[PageIndex].Keywords[i]);
                    if ((i + 1) != this.Pages[PageIndex].Keywords.Count)
                    {
                        allKeywords.Append(", ");
                    }
                }

                Content = Content.Replace("[ALLKEYWORDSCOM]", allKeywords.ToString());
            }

            // [ALLBKEYWORDSCOM]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [ALLBKEYWORDSCOM]...\r\n");
            }

            if (Content.Contains("[ALLBKEYWORDS]"))
            {
                StringBuilder allKeywords = new StringBuilder(100);
                for (int i = 0; i < this.Pages[PageIndex].Keywords.Count; i++)
                {
                    if (this.Pages[PageIndex].Keywords[i].Length > 1)
                    {
                        allKeywords.Append(this.Pages[PageIndex].Keywords[i].Substring(0, 1).ToUpper() + this.Pages[PageIndex].Keywords[i].Substring(1));
                    }
                    else
                    {
                        allKeywords.Append(this.Pages[PageIndex].Keywords[i].ToUpper());
                    }
                    if ((i + 1) != this.Pages[PageIndex].Keywords.Count)
                    {
                        allKeywords.Append(", ");
                    }
                }

                Content = Content.Replace("[ALLBKEYWORDS]", allKeywords.ToString());
            }
        }

        private string ReplaceBraketsTokens(string Content)
        {
            int startPosition = 0;
            string tempString = string.Empty;

            // Замена случайного макроса [[xx|yy]]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [[xx|yy]]...\r\n");
            }

            while (Content.Contains("[[") && Content.Contains("]]"))
            {
                startPosition = Content.IndexOf("[[");
                int endPosition = Content.IndexOf("]]");

                if (endPosition < startPosition)
                {
                    endPosition = Content.IndexOf("]]", startPosition);
                }

                int mixedEndPosition = Content.IndexOf("]]]");
                if (mixedEndPosition < startPosition)
                {
                    mixedEndPosition = Content.IndexOf("]]]", startPosition);
                }

                if (endPosition == -1 && mixedEndPosition == -1)
                {
                    break;
                }

                tempString = Content.Substring(startPosition, (endPosition == mixedEndPosition ? mixedEndPosition + 3 : endPosition + 2) - startPosition);
                Content = Content.Remove(startPosition, (endPosition == mixedEndPosition ? mixedEndPosition + 3 : endPosition + 2) - startPosition);

                tempString = tempString.Replace("[[", string.Empty).Replace("]]", string.Empty);
                string[] macroses = tempString.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                Content = Content.Insert(startPosition, macroses[this.random.Next(macroses.Length)]);
            }

            return Content;
        }

        private void ReplaceRegularTokens(int PageIndex, ref string Content)
        {
            // Макросы из блоков
            // Random random = new Random(DateTime.Now.Millisecond);
            int startPosition = 0;
            int tempIndex = 0;
            string tempString = string.Empty;

            // Замена блочных макросов
            Content = this.ReplaceBlocksMacroses(PageIndex, Content);

            // Replace brakets
            Content = ReplaceBraketsTokens(Content);

            // [CATEGORIES]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [CATEGORIESLINK], [CATEGORIESKEYWORD], [CATEGORIESBKEYWORD]...\r\n");
            }
            while (Content.Contains("[CATEGORIESLINK]"))
            {
                startPosition = Content.IndexOf("[CATEGORIESLINK]");
                Content = Content.Remove(startPosition, 16);

                // Поиск ссылки на категорию
                for (int i = 0; i < this.Pages.Count; i++)
                {
                    if (this.Pages[i].Type == 3 && this.Pages[i].Keywords[0] == this.categories[tempIndex])
                    {
                        Content = Content.Insert(startPosition, this.settings.LinksRelativeURLs ? this.Pages[i].RelativeURL : this.Pages[i].URL);
                        break;
                    }
                }

                if (Content.Contains("[CATEGORIESLINK]") && Content.Contains("[CATEGORIESKEYWORD]"))
                {
                    startPosition = Content.IndexOf("[CATEGORIESKEYWORD]");
                    while (Content.Contains("[CATEGORIESKEYWORD]") && Content.IndexOf("[CATEGORIESKEYWORD]") < Content.IndexOf("[CATEGORIESLINK]"))
                    {
                        Content = Content.Remove(startPosition, 19);
                        Content = Content.Insert(startPosition, this.categories[tempIndex]);
                    }
                }
                else
                {
                    Content = Content.Replace("[CATEGORIESKEYWORD]", this.categories[tempIndex]);
                }

                if (Content.Contains("[CATEGORIESLINK]") && Content.Contains("[CATEGORIESBKEYWORD]"))
                {
                    while (Content.Contains("[CATEGORIESBKEYWORD]") && Content.IndexOf("[CATEGORIESBKEYWORD]") < Content.IndexOf("[CATEGORIESLINK]"))
                    {
                        startPosition = Content.IndexOf("[CATEGORIESBKEYWORD]");
                        Content = Content.Remove(startPosition, 20);
                        Content = Content.Insert(startPosition, this.categories[tempIndex].Substring(0, 1).ToUpper() + this.categories[tempIndex].Substring(1));
                    }
                }
                else
                {
                    Content = Content.Replace("[CATEGORIESBKEYWORD]", this.categories[tempIndex].Substring(0, 1).ToUpper() + this.categories[tempIndex].Substring(1));
                }

                tempIndex++;
                if (this.categories.Count <= tempIndex)
                {
                    tempIndex = 0;
                }
            }
            tempIndex = 0;
            while (Content.Contains("[CATEGORIESKEYWORD]") || Content.Contains("[CATEGORIESBKEYWORD]"))
            {
                if (Content.Contains("[CATEGORIESKEYWORD]") && !Content.Contains("[CATEGORIESBKEYWORD]"))
                {
                    startPosition = Content.IndexOf("[CATEGORIESKEYWORD]");
                    Content = Content.Remove(startPosition, 19);
                    Content = Content.Insert(startPosition, this.categories[tempIndex]);
                }
                else if (!Content.Contains("[CATEGORIESKEYWORD]") && Content.Contains("[CATEGORIESBKEYWORD]"))
                {
                    startPosition = Content.IndexOf("[CATEGORIESBKEYWORD]");
                    Content = Content.Remove(startPosition, 20);
                    Content = Content.Insert(startPosition, this.categories[tempIndex].Substring(0, 1).ToUpper() + this.categories[tempIndex].Substring(1));
                }
                else
                {
                    if (Content.Contains("[CATEGORIESKEYWORD]") && Content.IndexOf("[CATEGORIESKEYWORD]") < Content.IndexOf("[CATEGORIESBKEYWORD]"))
                    {
                        startPosition = Content.IndexOf("[CATEGORIESBKEYWORD]");
                        Content = Content.Remove(startPosition, 19);
                        Content = Content.Insert(startPosition, this.categories[tempIndex]);
                    }

                    if (Content.Contains("[CATEGORIESBKEYWORD]") && Content.IndexOf("[CATEGORIESBKEYWORD]") < Content.IndexOf("[CATEGORIESKEYWORD]"))
                    {
                        startPosition = Content.IndexOf("[CATEGORIESBKEYWORD]");
                        Content = Content.Remove(startPosition, 20);
                        Content = Content.Insert(startPosition, this.categories[tempIndex].Substring(0, 1).ToUpper() + this.categories[tempIndex].Substring(1));
                    }
                }

                tempIndex++;
                if (this.categories.Count <= tempIndex)
                {
                    tempIndex = 0;
                }
            }

            // [CATEGORYMENUBLOCK]
            tempIndex = 0;
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [CATEGORYMENULINK], [CATEGORYMENUKEYWORD], [CATEGORYMENUBKEYWORD]...\r\n");
            }
            while (Content.Contains("[CATEGORYMENULINK]"))
            {
                startPosition = Content.IndexOf("[CATEGORYMENULINK]");
                Content = Content.Remove(startPosition, 18);

                // Поиск ссылки на категорию
                if (tempIndex >= this.Pages.Count)
                {
                    tempIndex = 0;
                }
                for (int i = tempIndex; i < this.Pages.Count; i++)
                {
                    if (this.Pages[i].Type == 1 && this.Pages[i].Category == this.Pages[PageIndex].Category)
                    {
                        tempIndex = i;
                        Content = Content.Insert(startPosition, this.settings.LinksRelativeURLs ? this.Pages[tempIndex].RelativeURL : this.Pages[tempIndex].URL);
                        break;
                    }
                }

                if (Content.Contains("[CATEGORYMENULINK]") && Content.Contains("[CATEGORYMENUKEYWORD]"))
                {
                    startPosition = Content.IndexOf("[CATEGORYMENUKEYWORD]");
                    while (Content.Contains("[CATEGORYMENUKEYWORD]") && Content.IndexOf("[CATEGORYMENUKEYWORD]") < Content.IndexOf("[CATEGORYMENULINK]"))
                    {
                        Content = Content.Remove(startPosition, 21);
                        Content = Content.Insert(startPosition, this.Pages[tempIndex].Keywords[0]);
                    }
                }
                else
                {
                    Content = Content.Replace("[CATEGORYMENUKEYWORD]", this.Pages[tempIndex].Keywords[0]);
                }

                if (Content.Contains("[CATEGORYMENULINK]") && Content.Contains("[CATEGORYMENUBKEYWORD]"))
                {
                    while (Content.Contains("[CATEGORYMENUBKEYWORD]") && Content.IndexOf("[CATEGORYMENUBKEYWORD]") < Content.IndexOf("[CATEGORYMENULINK]"))
                    {
                        startPosition = Content.IndexOf("[CATEGORYMENUBKEYWORD]");
                        Content = Content.Remove(startPosition, 22);
                        Content = Content.Insert(startPosition, this.Pages[tempIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[tempIndex].Keywords[0].Substring(1));
                    }
                }
                else
                {
                    Content = Content.Replace("[CATEGORYMENUBKEYWORD]", this.Pages[tempIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[tempIndex].Keywords[0].Substring(1));
                }

                tempIndex++;
            }
            tempIndex = 0;
            while (Content.Contains("[CATEGORYMENUKEYWORD]") || Content.Contains("[CATEGORYMENUBKEYWORD]"))
            {
                // Поиск ссылки на категорию
                if (tempIndex >= this.Pages.Count)
                {
                    tempIndex = 0;
                }

                for (int i = tempIndex; i < this.Pages.Count; i++)
                {
                    if (this.Pages[i].Type == 1 && this.Pages[i].Category == this.Pages[PageIndex].Category)
                    {
                        if (tempIndex == 0)
                        {
                            tempIndex = i + 1;
                        }
                        break;
                    }
                }

                if (Content.Contains("[CATEGORYMENUKEYWORD]") && !Content.Contains("[CATEGORYMENUBKEYWORD]"))
                {
                    startPosition = Content.IndexOf("[CATEGORYMENUKEYWORD]");
                    Content = Content.Remove(startPosition, 21);
                    Content = Content.Insert(startPosition, this.Pages[tempIndex].Keywords[0]);
                }
                else if (!Content.Contains("[CATEGORYMENUKEYWORD]") && Content.Contains("[CATEGORYMENUBKEYWORD]"))
                {
                    startPosition = Content.IndexOf("[CATEGORYMENUBKEYWORD]");
                    Content = Content.Remove(startPosition, 22);
                    Content = Content.Insert(startPosition, this.Pages[tempIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[tempIndex].Keywords[0].Substring(1));
                }
                else
                {
                    if (Content.Contains("[CATEGORYMENUKEYWORD]") && Content.IndexOf("[CATEGORYMENUKEYWORD]") < Content.IndexOf("[CATEGORYMENUBKEYWORD]"))
                    {
                        startPosition = Content.IndexOf("[CATEGORYMENUKEYWORD]");
                        Content = Content.Remove(startPosition, 21);
                        Content = Content.Insert(startPosition, this.Pages[tempIndex].Keywords[0]);
                    }
                    if (Content.Contains("[CATEGORYMENUBKEYWORD]") && Content.IndexOf("[CATEGORYMENUBKEYWORD]") < Content.IndexOf("[CATEGORYMENUKEYWORD]"))
                    {
                        startPosition = Content.IndexOf("[CATEGORYMENUBKEYWORD]");
                        Content = Content.Remove(startPosition, 22);
                        Content = Content.Insert(startPosition, this.Pages[tempIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[tempIndex].Keywords[0].Substring(1));
                    }
                }
                tempIndex++;
            }

            // [STATICBLOCK]
            tempIndex = 0;
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [STATICLINK], [STATICKEYWORD], [STATICBKEYWORD]...\r\n");
            }

            while (Content.Contains("[STATICLINK]"))
            {
                startPosition = Content.IndexOf("[STATICLINK]");
                Content = Content.Remove(startPosition, 12);
                //Поиск ссылки на статическую страницу
                int staticPageIndex = 0;
                for (int i = 0; i < this.Pages.Count; i++)
                {
                    if (this.Pages[i].Type == 2)
                    {
                        if (tempIndex == staticPageIndex)
                        {
                            staticPageIndex = i;
                            break;
                        }
                        else
                        {
                            staticPageIndex++;
                        }
                    }
                }
                Content = Content.Insert(startPosition, this.settings.LinksRelativeURLs ? this.Pages[staticPageIndex].RelativeURL : this.Pages[staticPageIndex].URL);

                if (Content.Contains("[STATICLINK]") && Content.Contains("[STATICKEYWORD]"))
                {
                    startPosition = Content.IndexOf("[STATICKEYWORD]");
                    while (Content.Contains("[STATICKEYWORD]") && Content.IndexOf("[STATICKEYWORD]") < Content.IndexOf("[STATICLINK]"))
                    {
                        Content = Content.Remove(startPosition, 15);
                        Content = Content.Insert(startPosition, this.Pages[staticPageIndex].Keywords[0]);
                    }
                }
                else
                {
                    Content = Content.Replace("[STATICKEYWORD]", this.Pages[staticPageIndex].Keywords[0]);
                }

                if (Content.Contains("[STATICLINK]") && Content.Contains("[STATICBKEYWORD]"))
                {
                    while (Content.Contains("[STATICBKEYWORD]") && Content.IndexOf("[STATICBKEYWORD]") < Content.IndexOf("[STATICLINK]"))
                    {
                        startPosition = Content.IndexOf("[STATICBKEYWORD]");
                        Content = Content.Remove(startPosition, 16);
                        Content = Content.Insert(startPosition, this.Pages[staticPageIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[staticPageIndex].Keywords[0].Substring(1));
                    }
                }
                else
                {
                    Content = Content.Replace("[STATICBKEYWORD]", this.Pages[staticPageIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[staticPageIndex].Keywords[0].Substring(1));
                }

                tempIndex++;
                if (this.settings.StaticPagesList.Length <= tempIndex)
                {
                    tempIndex = 0;
                }
            }
            tempIndex = 0;
            while (Content.Contains("[STATICKEYWORD]") || Content.Contains("[STATICBKEYWORD]"))
            {
                //Поиск ссылки на статическую страницу
                int staticPageIndex = 0;
                for (int i = 0; i < this.Pages.Count; i++)
                {
                    if (this.Pages[i].Type == 2)
                    {
                        if (tempIndex == staticPageIndex)
                        {
                            staticPageIndex = i;
                            break;
                        }
                        else
                        {
                            staticPageIndex++;
                        }
                    }
                }

                if (Content.Contains("[STATICKEYWORD]") && !Content.Contains("[STATICBKEYWORD]"))
                {
                    startPosition = Content.IndexOf("[STATICKEYWORD]");
                    Content = Content.Remove(startPosition, 15);
                    Content = Content.Insert(startPosition, this.Pages[staticPageIndex].Keywords[0]);
                }
                else if (!Content.Contains("[STATICKEYWORD]") && Content.Contains("[STATICBKEYWORD]"))
                {
                    startPosition = Content.IndexOf("[STATICBKEYWORD]");
                    Content = Content.Remove(startPosition, 16);
                    Content = Content.Insert(startPosition, this.Pages[staticPageIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[staticPageIndex].Keywords[0].Substring(1));
                }
                else
                {
                    if (Content.Contains("[STATICKEYWORD]") && Content.IndexOf("[STATICKEYWORD]") < Content.IndexOf("[STATICBKEYWORD]"))
                    {
                        startPosition = Content.IndexOf("[STATICKEYWORD]");
                        Content = Content.Remove(startPosition, 15);
                        Content = Content.Insert(startPosition, this.Pages[staticPageIndex].Keywords[0]);
                    }
                    if (Content.Contains("[STATICBKEYWORD]") && Content.IndexOf("[STATICBKEYWORD]") < Content.IndexOf("[STATICKEYWORD]"))
                    {
                        startPosition = Content.IndexOf("[STATICBKEYWORD]");
                        Content = Content.Remove(startPosition, 16);
                        Content = Content.Insert(startPosition, this.Pages[staticPageIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[staticPageIndex].Keywords[0].Substring(1));
                    }
                }

                tempIndex++;
                if (this.settings.StaticPagesList.Length <= tempIndex)
                {
                    tempIndex = 0;
                }
            }
            //[CUSTOMBLOCK]
            tempIndex = 0;
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [CUSTOMLINK], [CUSTOMKEYWORD], [CUSTOMBKEYWORD]...\r\n");
            }
            while (Content.Contains("[CUSTOMLINK]"))
            {
                startPosition = Content.IndexOf("[CUSTOMLINK]");
                Content = Content.Remove(startPosition, 12);
                //Поиск ссылки на кастом страницу
                int customPageIndex = 0;
                for (int i = 0; i < this.Pages.Count; i++)
                {
                    if (this.Pages[i].Type == 7)
                    {
                        if (tempIndex == customPageIndex)
                        {
                            customPageIndex = i;
                            break;
                        }
                        else
                        {
                            customPageIndex++;
                        }
                    }
                }
                Content = Content.Insert(startPosition, this.settings.LinksRelativeURLs ? this.Pages[customPageIndex].RelativeURL : this.Pages[customPageIndex].URL);

                if (Content.Contains("[CUSTOMLINK]") && Content.Contains("[CUSTOMKEYWORD]"))
                {
                    startPosition = Content.IndexOf("[CUSTOMKEYWORD]");
                    while (Content.Contains("[CUSTOMKEYWORD]") && Content.IndexOf("[CUSTOMKEYWORD]") < Content.IndexOf("[CUSTOMLINK]"))
                    {
                        Content = Content.Remove(startPosition, 15);
                        Content = Content.Insert(startPosition, this.Pages[customPageIndex].Keywords[0]);
                    }
                }
                else
                {
                    Content = Content.Replace("[CUSTOMKEYWORD]", this.Pages[customPageIndex].Keywords[0]);
                }

                if (Content.Contains("[CUSTOMLINK]") && Content.Contains("[CUSTOMBKEYWORD]"))
                {
                    while (Content.Contains("[CUSTOMBKEYWORD]") && Content.IndexOf("[CUSTOMBKEYWORD]") < Content.IndexOf("[CUSTOMLINK]"))
                    {
                        startPosition = Content.IndexOf("[CUSTOMBKEYWORD]");
                        Content = Content.Remove(startPosition, 16);
                        Content = Content.Insert(startPosition, this.Pages[customPageIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[customPageIndex].Keywords[0].Substring(1));
                    }
                }
                else
                {
                    Content = Content.Replace("[CUSTOMBKEYWORD]", this.Pages[customPageIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[customPageIndex].Keywords[0].Substring(1));
                }

                tempIndex++;
                if (SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages.Count <= tempIndex)
                {
                    tempIndex = 0;
                }
            }
            tempIndex = 0;
            while (Content.Contains("[CUSTOMKEYWORD]") || Content.Contains("[CUSTOMBKEYWORD]"))
            {
                //Поиск ссылки на кастом страницу
                int customPageIndex = 0;
                for (int i = 0; i < this.Pages.Count; i++)
                {
                    if (this.Pages[i].Type == 7)
                    {
                        if (tempIndex == customPageIndex)
                        {
                            customPageIndex = i;
                            break;
                        }
                        else
                        {
                            customPageIndex++;
                        }
                    }
                }

                if (Content.Contains("[CUSTOMKEYWORD]") && !Content.Contains("[CUSTOMBKEYWORD]"))
                {
                    startPosition = Content.IndexOf("[CUSTOMKEYWORD]");
                    Content = Content.Remove(startPosition, 15);
                    Content = Content.Insert(startPosition, this.Pages[customPageIndex].Keywords[0]);
                }
                else if (!Content.Contains("[CUSTOMKEYWORD]") && Content.Contains("[CUSTOMBKEYWORD]"))
                {
                    startPosition = Content.IndexOf("[CUSTOMBKEYWORD]");
                    Content = Content.Remove(startPosition, 16);
                    Content = Content.Insert(startPosition, this.Pages[customPageIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[customPageIndex].Keywords[0].Substring(1));
                }
                else
                {
                    if (Content.Contains("[CUSTOMKEYWORD]") && Content.IndexOf("[CUSTOMKEYWORD]") < Content.IndexOf("[CUSTOMBKEYWORD]"))
                    {
                        startPosition = Content.IndexOf("[CUSTOMKEYWORD]");
                        Content = Content.Remove(startPosition, 15);
                        Content = Content.Insert(startPosition, this.Pages[customPageIndex].Keywords[0]);
                    }
                    if (Content.Contains("[CUSTOMBKEYWORD]") && Content.IndexOf("[CUSTOMBKEYWORD]") < Content.IndexOf("[CUSTOMKEYWORD]"))
                    {
                        startPosition = Content.IndexOf("[CUSTOMBKEYWORD]");
                        Content = Content.Remove(startPosition, 16);
                        Content = Content.Insert(startPosition, this.Pages[customPageIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[customPageIndex].Keywords[0].Substring(1));
                    }
                }

                tempIndex++;
                /*if (SharedData.WorkSpaces[wsIndex].Templates[tempIndex].CustomPages.Count <= tempIndex)
                {
                    tempIndex = 0;
                }*/
            }

            // [MAPBLOCK]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [MAPLINK], [MAPKEYWORD], [MAPBKEYWORD]...\r\n");
            }
            if (this.Pages[PageIndex].Type == 2 || this.Pages[PageIndex].Type == 4)
            {
                int startLink = this.Pages[PageIndex].HTMLSiteMapPageStart;
                while (Content.Contains("[MAPLINK]"))
                {
                    if (this.Pages.Count <= startLink)
                    {
                        break;
                    }

                    if (this.settings.StaticPages && !this.settings.StaticPagesIncludeIntoSiteMap)
                    {
                        while (this.Pages[startLink].Type == 2)
                        {
                            startLink++;

                            if (this.Pages.Count <= startLink)
                            {
                                startLink = 0;
                            }
                        }
                    }

                    if ((this.Pages[PageIndex].HTMLSiteMapPageStart + this.Pages[PageIndex].HTMLSiteMapPageCount) < startLink)
                    {
                        startLink = this.Pages[PageIndex].HTMLSiteMapPageStart;
                    }

                    startPosition = Content.IndexOf("[MAPLINK]");
                    Content = Content.Remove(startPosition, 9);
                    Content = Content.Insert(startPosition, this.settings.LinksRelativeURLs ? this.Pages[startLink].RelativeURL : this.Pages[startLink].URL);

                    if (Content.Contains("[MAPLINK]") && Content.Contains("[MAPKEYWORD]"))
                    {
                        startPosition = Content.IndexOf("[MAPKEYWORD]");
                        while (Content.Contains("[MAPKEYWORD]") && Content.IndexOf("[MAPKEYWORD]") < Content.IndexOf("[MAPLINK]"))
                        {
                            Content = Content.Remove(startPosition, 12);
                            if (this.Pages[startLink].Keywords.Count == 0)
                            {
                                Content = Content.Insert(startPosition, this.Pages[0].Keywords[0]);
                            }
                            else
                            {
                                Content = Content.Insert(startPosition, this.Pages[startLink].Keywords[0]);
                            }
                        }
                    }
                    else
                    {
                        if (this.Pages[startLink].Keywords.Count == 0)
                        {
                            Content = Content.Replace("[MAPKEYWORD]", this.Pages[0].Keywords[0]);
                        }
                        else
                        {
                            Content = Content.Replace("[MAPKEYWORD]", this.Pages[startLink].Keywords[0]);
                        }
                    }

                    if (Content.Contains("[MAPLINK]") && Content.Contains("[MAPBKEYWORD]"))
                    {
                        while (Content.Contains("[MAPBKEYWORD]") && Content.IndexOf("[MAPBKEYWORD]") < Content.IndexOf("[MAPLINK]"))
                        {
                            startPosition = Content.IndexOf("[MAPBKEYWORD]");
                            Content = Content.Remove(startPosition, 13);
                            if (this.Pages[startLink].Keywords.Count == 0)
                            {
                                Content = Content.Insert(startPosition, this.Pages[0].Keywords[0]);
                            }
                            else
                            {
                                if (this.Pages[startLink].Keywords[0].Length > 1)
                                {
                                    Content = Content.Insert(startPosition, this.Pages[startLink].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[startLink].Keywords[0].Substring(1));
                                }
                                else
                                {
                                    Content = Content.Insert(startPosition, this.Pages[startLink].Keywords[0].ToUpper());
                                }
                            }
                        }
                    }
                    else
                    {
                        if (this.Pages[startLink].Keywords.Count == 0)
                        {
                            Content = Content.Replace("[MAPBKEYWORD]", this.Pages[0].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[0].Keywords[0].Substring(1));
                        }
                        else
                        {
                            Content = Content.Replace("[MAPBKEYWORD]", this.Pages[startLink].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[startLink].Keywords[0].Substring(1));
                        }
                    }

                    if (Content.Contains("[MAPLINK]") && Content.Contains("[MAPKEYWORDCOM]"))
                    {
                        startPosition = Content.IndexOf("[MAPKEYWORDCOM]");
                        while (Content.Contains("[MAPKEYWORDCOM]") && Content.IndexOf("[MAPKEYWORDCOM]") < Content.IndexOf("[MAPLINK]"))
                        {
                            Content = Content.Remove(startPosition, 15);
                            if (this.Pages[startLink].Keywords.Count == 0)
                            {
                                Content = Content.Insert(startPosition, this.Pages[0].Keywords[0].Replace(" ", ", "));
                            }
                            else
                            {
                                Content = Content.Insert(startPosition, this.Pages[startLink].Keywords[0].Replace(" ", ", "));
                            }
                        }
                    }
                    else
                    {
                        if (this.Pages[startLink].Keywords.Count == 0)
                        {
                            Content = Content.Replace("[MAPKEYWORDCOM]", this.Pages[0].Keywords[0].Replace(" ", ", "));
                        }
                        else
                        {
                            Content = Content.Replace("[MAPKEYWORDCOM]", this.Pages[startLink].Keywords[0].Replace(" ", ", "));
                        }
                    }

                    if (Content.Contains("[MAPLINK]") && Content.Contains("[MAPBKEYWORDCOM]"))
                    {
                        while (Content.Contains("[MAPBKEYWORDCOM]") && Content.IndexOf("[MAPBKEYWORDCOM]") < Content.IndexOf("[MAPLINK]"))
                        {
                            startPosition = Content.IndexOf("[MAPBKEYWORDCOM]");
                            Content = Content.Remove(startPosition, 16);
                            if (this.Pages[startLink].Keywords.Count == 0)
                            {
                                if (this.Pages[0].Keywords[0].Length > 1)
                                {
                                    Content = Content.Insert(startPosition, this.Pages[0].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[startLink].Keywords[0].Substring(1).Replace(" ", ", "));
                                }
                                else
                                {
                                    Content = Content.Insert(startPosition, this.Pages[0].Keywords[0].ToUpper());
                                }
                            }
                            else
                            {
                                if (this.Pages[startLink].Keywords[0].Length > 1)
                                {
                                    Content = Content.Insert(startPosition, this.Pages[startLink].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[startLink].Keywords[0].Substring(1).Replace(" ", ", "));
                                }
                                else
                                {
                                    Content = Content.Insert(startPosition, this.Pages[startLink].Keywords[0].ToUpper());
                                }
                            }
                        }
                    }
                    else
                    {
                        if (this.Pages[startLink].Keywords.Count == 0)
                        {
                            if (this.Pages[0].Keywords[0].Length > 1)
                            {
                                Content = Content.Replace("[MAPBKEYWORDCOM]", this.Pages[0].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[0].Keywords[0].Substring(1).Replace(" ", ", "));
                            }
                            else
                            {
                                Content = Content.Replace("[MAPBKEYWORDCOM]", this.Pages[0].Keywords[0].ToUpper());
                            }
                        }
                        else
                        {
                            if (this.Pages[startLink].Keywords[0].Length > 1)
                            {
                                Content = Content.Replace("[MAPBKEYWORDCOM]", this.Pages[startLink].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[startLink].Keywords[0].Substring(1).Replace(" ", ", "));
                            }
                            else
                            {
                                Content = Content.Replace("[MAPBKEYWORDCOM]", this.Pages[startLink].Keywords[0].ToUpper());
                            }
                        }
                    }

                    startLink++;
                }
            }
            else
            {
                Content = Content.Replace("[MAPLINK]", "[MENULINK]");
                Content = Content.Replace("[MAPKEYWORD]", "[MENUKEYWORD]");
                Content = Content.Replace("[MAPBKEYWORD]", "[MENUBKEYWORD]");
            }
            // [MENUBLOCK]
            tempIndex = 0;
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [MENULINK], [MENUKEYWORD], [MENUBKEYWORD]...\r\n");
            }
            while (Content.Contains("[MENULINK]"))
            {
                startPosition = Content.IndexOf("[MENULINK]");
                Content = Content.Remove(startPosition, 10);
                // Поиск индекса страницы для вставки
                while (this.Pages[tempIndex].Type != 1)
                {
                    tempIndex = random.Next(this.Pages.Count);
                }
                Content = Content.Insert(startPosition, this.settings.LinksRelativeURLs ? this.Pages[tempIndex].RelativeURL : this.Pages[tempIndex].URL);

                if (Content.Contains("[MENULINK]") && Content.Contains("[MENUKEYWORD]"))
                {
                    while (Content.Contains("[MENUKEYWORD]") && Content.IndexOf("[MENUKEYWORD]") < Content.IndexOf("[MENULINK]"))
                    {
                        startPosition = Content.IndexOf("[MENUKEYWORD]");
                        Content = Content.Remove(startPosition, 13);
                        Content = Content.Insert(startPosition, this.Pages[tempIndex].Keywords[0]);
                    }
                }
                else
                {
                    Content = Content.Replace("[MENUKEYWORD]", this.Pages[tempIndex].Keywords[0]);
                }

                if (Content.Contains("[MENULINK]") && Content.Contains("[MENUBKEYWORD]"))
                {
                    while (Content.Contains("[MENUBKEYWORD]") && Content.IndexOf("[MENUBKEYWORD]") < Content.IndexOf("[MENULINK]"))
                    {
                        startPosition = Content.IndexOf("[MENUBKEYWORD]");
                        Content = Content.Remove(startPosition, 14);
                        Content = Content.Insert(startPosition, this.Pages[tempIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[tempIndex].Keywords[0].Substring(1));
                    }
                }
                else
                {
                    Content = Content.Replace("[MENUBKEYWORD]", this.Pages[tempIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[tempIndex].Keywords[0].Substring(1));
                }

                if (Content.Contains("[MENULINK]") && Content.Contains("[MENUKEYWORDCOM]"))
                {
                    while (Content.Contains("[MENUKEYWORDCOM]") && Content.IndexOf("[MENUKEYWORDCOM]") < Content.IndexOf("[MENULINK]"))
                    {
                        startPosition = Content.IndexOf("[MENUKEYWORDCOM]");
                        Content = Content.Remove(startPosition, 16);
                        Content = Content.Insert(startPosition, this.Pages[tempIndex].Keywords[0].Replace(" ", ", "));
                    }
                }
                else
                {
                    Content = Content.Replace("[MENUKEYWORDCOM]", this.Pages[tempIndex].Keywords[0].Replace(" ", ", "));
                }

                if (Content.Contains("[MENULINK]") && Content.Contains("[MENUBKEYWORDCOM]"))
                {
                    while (Content.Contains("[MENUBKEYWORDCOM]") && Content.IndexOf("[MENUBKEYWORDCOM]") < Content.IndexOf("[MENULINK]"))
                    {
                        startPosition = Content.IndexOf("[MENUBKEYWORDCOM]");
                        Content = Content.Remove(startPosition, 17);
                        Content = Content.Insert(startPosition, this.Pages[tempIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[tempIndex].Keywords[0].Substring(1).Replace(" ", ", "));
                    }
                }
                else
                {
                    Content = Content.Replace("[MENUBKEYWORDCOM]", this.Pages[tempIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[tempIndex].Keywords[0].Substring(1).Replace(" ", ", "));
                }

                tempIndex++;
                if (this.Pages.Count <= tempIndex)
                {
                    tempIndex = 0;
                }
                else
                {
                    if (this.Pages[tempIndex].Type != 1)
                    {
                        tempIndex = 0;
                    }
                }
            }
            Content = Content.Replace("[MENUKEYWORD]", "[MAINKEYWORD]");
            Content = Content.Replace("[MENUBKEYWORD]", "[MAINBKEYWORD]");
            Content = Content.Replace("[MENUKEYWORDCOM]", "[MAINKEYWORD]");
            Content = Content.Replace("[MENUBKEYWORDCOM]", "[MAINBKEYWORD]");

            // [NETBLOCK]
            Content = this.ReplaceNetTokens(Content);

            //Вероятностные макросы
            //[15%]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [15%]...\r\n");
            }
            while (Content.Contains("[15%]") && Content.Contains("[/15%]"))
            {
                if (random.Next(0, 100) <= 15)
                {
                    startPosition = Content.IndexOf("[15%]");

                    tempString = Content.Substring(startPosition, Content.IndexOf("[/15%]") + 6 - startPosition);

                    tempString = tempString.Replace("[15%]", string.Empty).Replace("[/15%]", string.Empty);

                    Content = Content.Remove(Content.IndexOf("[15%]"), Content.IndexOf("[/15%]") + 6 - Content.IndexOf("[15%]"));

                    Content = Content.Insert(startPosition, tempString);
                }
                else
                {
                    Content = Content.Remove(Content.IndexOf("[15%]"), Content.IndexOf("[/15%]") + 6 - Content.IndexOf("[15%]"));
                }
            }
            //[25%]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [25%]...\r\n");
            }
            while (Content.Contains("[25%]") && Content.Contains("[/25%]"))
            {
                if (random.Next(0, 100) <= 25)
                {
                    startPosition = Content.IndexOf("[25%]");

                    tempString = Content.Substring(startPosition, Content.IndexOf("[/25%]") + 6 - startPosition);

                    tempString = tempString.Replace("[25%]", string.Empty).Replace("[/25%]", string.Empty);

                    Content = Content.Remove(Content.IndexOf("[25%]"), Content.IndexOf("[/25%]") + 6 - Content.IndexOf("[25%]"));

                    Content = Content.Insert(startPosition, tempString);
                }
                else
                {
                    Content = Content.Remove(Content.IndexOf("[25%]"), Content.IndexOf("[/25%]") + 6 - Content.IndexOf("[25%]"));
                }
            }
            //[50%]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [50%]...\r\n");
            }
            while (Content.Contains("[50%]") && Content.Contains("[/50%]"))
            {
                if (random.Next(0, 100) <= 50)
                {
                    startPosition = Content.IndexOf("[50%]");

                    int count = Content.IndexOf("[/50%]") + 6 - startPosition;

                    tempString = Content.Substring(startPosition, count);

                    tempString = tempString.Replace("[50%]", string.Empty).Replace("[/50%]", string.Empty);

                    Content = Content.Remove(Content.IndexOf("[50%]"), Content.IndexOf("[/50%]") + 6 - Content.IndexOf("[50%]"));

                    Content = Content.Insert(startPosition, tempString);
                }
                else
                {
                    Content = Content.Remove(Content.IndexOf("[50%]"), Content.IndexOf("[/50%]") + 6 - Content.IndexOf("[50%]"));
                }
            }
            //[75%]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [75%]...\r\n");
            }
            while (Content.Contains("[75%]") && Content.Contains("[/75%]"))
            {
                if (random.Next(0, 100) <= 75)
                {
                    startPosition = Content.IndexOf("[75%]");

                    tempString = Content.Substring(startPosition, Content.IndexOf("[/75%]") + 6 - startPosition);

                    tempString = tempString.Replace("[75%]", string.Empty).Replace("[/75%]", string.Empty);

                    Content = Content.Remove(Content.IndexOf("[75%]"), Content.IndexOf("[/75%]") + 6 - Content.IndexOf("[75%]"));

                    Content = Content.Insert(startPosition, tempString);
                }
                else
                {
                    Content = Content.Remove(Content.IndexOf("[75%]"), Content.IndexOf("[/75%]") + 6 - Content.IndexOf("[75%]"));
                }
            }
            //[85%]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [85%]...\r\n");
            }
            while (Content.Contains("[85%]") && Content.Contains("[/85%]"))
            {
                if (random.Next(0, 100) <= 85)
                {
                    startPosition = Content.IndexOf("[85%]");

                    tempString = Content.Substring(startPosition, Content.IndexOf("[/85%]") + 6 - startPosition);

                    tempString = tempString.Replace("[85%]", string.Empty).Replace("[/85%]", string.Empty);

                    Content = Content.Remove(Content.IndexOf("[85%]"), Content.IndexOf("[/85%]") + 6 - Content.IndexOf("[85%]"));

                    Content = Content.Insert(startPosition, tempString);
                }
                else
                {
                    Content = Content.Remove(Content.IndexOf("[85%]"), Content.IndexOf("[/85%]") + 6 - Content.IndexOf("[85%]"));
                }
            }
            //Остальные макросы

            //[REDIRECT]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [REDIRECT]...\r\n");
            }
            Content = Content.Replace("[REDIRECT]", RedirectCode(PageIndex));
            //[COUNTER]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [COUNTER]...\r\n");
            }
            Content = Content.Replace("[COUNTER]", this.settings.EntranceCounter);
            //[SITE]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [SITE]...\r\n");
            }
            if (this.settings.MacrosesSite.Length > index)
            {
                Content = Content.Replace("[SITE]", this.settings.MacrosesSite[index]);
            }
            else
            {
                Content = Content.Replace("[SITE]", "[MAINLINK]");
            }
            //[TITLE]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [TITLE]...\r\n");
            }
            if (this.settings.MacrosesTitle.Length > index)
            {
                Content = Content.Replace("[TITLE]", this.settings.MacrosesTitle[index]);
            }
            else
            {
                if (this.settings.MacrosesTitle.Length > 0)
                {
                    Content = Content.Replace("[TITLE]", this.settings.MacrosesTitle[0]);
                }
                else
                {
                    Content = Content.Replace("[TITLE]", string.Empty);
                }
            }
            //Customs
            //[CUSTOM]
            if (this.Pages[PageIndex].Type == 7)
            {
                Content = Content.Replace("[CUSTOM]", this.Pages[PageIndex].Keywords[0]);
            }
            else
            {
                Content = Content.Replace("[CUSTOM]", "[RCUSTOM]");
            }
            //[BCUSTOM]
            if (this.Pages[PageIndex].Type == 7)
            {
                if (this.Pages[PageIndex].Keywords[0].Length > 1)
                {
                    Content = Content.Replace("[BCUSTOM]", this.Pages[PageIndex].Keywords[0].Substring(0, 1).ToUpper() + this.Pages[PageIndex].Keywords[0].Substring(1));
                }
                else
                {
                    Content = Content.Replace("[BCUSTOM]", this.Pages[PageIndex].Keywords[0].ToUpper());
                }
            }
            else
            {
                Content = Content.Replace("[BCUSTOM]", "[RCUSTOM]");
            }
            //[RCUSTOM]
            if (SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages.Count > 0)
            {
                string[] keywords =
                    SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages[random.Next(0, SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages.Count)].CustomKeywords;

                if (keywords.Length != 0)
                {
                    Content = Content.Replace("[RCUSTOM]", keywords[this.random.Next(keywords.Length)]);
                }
                else
                {
                    Content = Content.Replace("[RCUSTOM]", "[KEYWORD]");
                }
            }
            else
            {
                Content = Content.Replace("[RCUSTOM]", "[KEYWORD]");
            }

            // [RBCUSTOM]
            if (SharedData.WorkSpaces[this.wsIndex].Templates[this.templateIndex].CustomPages.Count > 0)
            {
                string[] keywords =
                    SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages[random.Next(0, SharedData.WorkSpaces[wsIndex].Templates[templateIndex].CustomPages.Count)].CustomKeywords;

                if (keywords.Length != 0)
                {
                    string keyword = keywords[this.random.Next(keywords.Length)];
                    Content = Content.Replace("[RBCUSTOM]", keyword.Length > 1 ? keyword.Substring(0, 1).ToUpper() + keyword.Substring(1) : keyword.ToUpper());
                }
                else
                {
                    Content = Content.Replace("[RBCUSTOM]", "[BKEYWORD]");
                }
            }
            else
            {
                Content = Content.Replace("[RBCUSTOM]", "[BKEYWORD]");
            }

            // [CATEGORYLINK]
            if (this.categories.Count > 0)
            {
                if (this.Pages[PageIndex].Category == -1)
                {
                    Content = Content.Replace("[CATEGORYLINK]", "[MAINLINK]");
                }
                else
                {
                    // Searching for link
                    for (int k = 0; k < this.Pages.Count; k++)
                    {
                        if (this.Pages[PageIndex].Type == 1)
                        {
                            if (this.Pages[k].Type == 3 && this.Pages[k].Category == this.Pages[PageIndex].Category)
                            {
                                Content = Content.Replace("[CATEGORYLINK]",
                                                          this.settings.LinksRelativeURLs
                                                              ? this.Pages[PageIndex].RelativeURL.Substring(0, this.Pages[PageIndex].RelativeURL.LastIndexOf("../") + 3) +
                                                                this.Pages[k].RelativeURL.Replace("../", string.Empty)
                                                              : this.Pages[k].URL);
                                break;
                            }
                        }
                    }
                    Content = Content.Replace("[CATEGORYLINK]", "[MAINLINK]");
                }
            }
            else
            {
                Content = Content.Replace("[CATEGORYLINK]", "[MAINLINK]");
            }

            // [NEXTPAGELINK]
            if (Content.Contains("[NEXTPAGELINK]"))
            {
                if (this.categories.Count > 0)
                {
                    if (this.Pages[PageIndex].Category == -1)
                    {
                        Content = Content.Replace("[NEXTPAGELINK]", "[MAINLINK]");
                        Content = Content.Replace("[NEXTPAGELINKKEYWORD]", "[MAINKEYWORD]");
                        Content = Content.Replace("[NEXTPAGELINKBKEYWORD]", "[MAINBKEYWORD]");
                    }
                    else
                    {
                        // Searching for link
                        for (int k = PageIndex + 1; k < this.Pages.Count; k++)
                        {
                            if (this.Pages[PageIndex].Type == 1)
                            {
                                if (this.Pages[k].Type == 1 && this.Pages[k].Category == this.Pages[PageIndex].Category)
                                {
                                    Content = Content.Replace("[NEXTPAGELINK]", this.settings.LinksRelativeURLs ? this.Pages[k].RelativeURL : this.Pages[k].URL);
                                    if (this.Pages[k].Keywords.Count > 0)
                                    {
                                        string keyword =
                                            this.Pages[k].Keywords[random.Next(this.Pages[k].Keywords.Count)];
                                        Content = Content.Replace("[NEXTPAGELINKKEYWORD]", keyword);
                                        if (keyword.Length > 1)
                                        {
                                            keyword = keyword.Substring(0, 1).ToUpper() + keyword.Substring(1);
                                        }
                                        else
                                        {
                                            keyword = keyword.ToUpper();
                                        }

                                        Content = Content.Replace("[NEXTPAGELINKBKEYWORD]", keyword);
                                    }
                                    else
                                    {
                                        Content = Content.Replace("[NEXTPAGELINKKEYWORD]", "[MAINKEYWORD]");
                                        Content = Content.Replace("[NEXTPAGELINKBKEYWORD]", "[MAINBKEYWORD]");
                                    }

                                    break;
                                }
                            }
                        }
                        Content = Content.Replace("[NEXTPAGELINK]", "[MAINLINK]");
                        Content = Content.Replace("[NEXTPAGELINKKEYWORD]", "[MAINKEYWORD]");
                        Content = Content.Replace("[NEXTPAGELINKBKEYWORD]", "[MAINBKEYWORD]");

                    }
                }
                else
                {
                    if (PageIndex != 0 && PageIndex + 1 < this.Pages.Count)
                    {
                        Content = Content.Replace("[NEXTPAGELINK]", this.settings.LinksRelativeURLs ? this.Pages[PageIndex + 1].RelativeURL : this.Pages[PageIndex + 1].URL);
                        if (this.Pages[PageIndex + 1].Keywords.Count > 0)
                        {
                            string keyword =
                                this.Pages[PageIndex + 1].Keywords[random.Next(this.Pages[PageIndex + 1].Keywords.Count)];
                            Content = Content.Replace("[NEXTPAGELINKKEYWORD]", keyword);
                            if (keyword.Length > 1)
                            {
                                keyword = keyword.Substring(0, 1).ToUpper() + keyword.Substring(1);
                            }
                            else
                            {
                                keyword = keyword.ToUpper();
                            }

                            Content = Content.Replace("[NEXTPAGELINKBKEYWORD]", keyword);
                        }
                        else
                        {
                            Content = Content.Replace("[NEXTPAGELINKKEYWORD]", "[MAINKEYWORD]");
                            Content = Content.Replace("[NEXTPAGELINKBKEYWORD]", "[MAINBKEYWORD]");
                        }
                    }
                    else
                    {
                        Content = Content.Replace("[NEXTPAGELINK]", "[MAINLINK]");
                        Content = Content.Replace("[NEXTPAGELINKKEYWORD]", "[MAINKEYWORD]");
                        Content = Content.Replace("[NEXTPAGELINKBKEYWORD]", "[MAINBKEYWORD]");
                    }
                }
            }
            else
            {
                Content = Content.Replace("[NEXTPAGELINK]", "[MAINLINK]");
                Content = Content.Replace("[NEXTPAGELINKKEYWORD]", "[MAINKEYWORD]");
                Content = Content.Replace("[NEXTPAGELINKBKEYWORD]", "[MAINBKEYWORD]");
            }

            // [PREVPAGELINK]
            if (Content.Contains("[PREVPAGELINK]"))
            {
                if (this.categories.Count > 0)
                {
                    if (this.Pages[PageIndex].Category == -1)
                    {
                        Content = Content.Replace("[PREVPAGELINK]", "[MAINLINK]");
                        Content = Content.Replace("[PREVPAGELINKKEYWORD]", "[MAINKEYWORD]");
                        Content = Content.Replace("[PREVPAGELINKBKEYWORD]", "[MAINBKEYWORD]");
                    }
                    else
                    {
                        // Searching for link
                        for (int k = PageIndex - 1; k >= 0; k--)
                        {
                            if (this.Pages[PageIndex].Type == 1 && PageIndex - 1 >= 0)
                            {
                                if (this.Pages[k].Type == 1 && this.Pages[k].Category == this.Pages[PageIndex].Category)
                                {
                                    Content = Content.Replace("[PREVPAGELINK]", this.settings.LinksRelativeURLs ? this.Pages[k].RelativeURL : this.Pages[k].URL);
                                    if (this.Pages[k].Keywords.Count > 0)
                                    {
                                        string keyword =
                                            this.Pages[k].Keywords[random.Next(this.Pages[k].Keywords.Count)];
                                        Content = Content.Replace("[PREVPAGELINKKEYWORD]", keyword);
                                        if (keyword.Length > 1)
                                        {
                                            keyword = keyword.Substring(0, 1).ToUpper() + keyword.Substring(1);
                                        }
                                        else
                                        {
                                            keyword = keyword.ToUpper();
                                        }
                                        Content = Content.Replace("[PREVPAGELINKBKEYWORD]", keyword);
                                    }
                                    else
                                    {
                                        Content = Content.Replace("[PREVPAGELINKKEYWORD]", "[MAINKEYWORD]");
                                        Content = Content.Replace("[PREVPAGELINKBKEYWORD]", "[MAINBKEYWORD]");
                                    }
                                    break;
                                }
                            }
                        }
                        Content = Content.Replace("[PREVPAGELINK]", "[MAINLINK]");
                        Content = Content.Replace("[PREVPAGELINKKEYWORD]", "[MAINKEYWORD]");
                        Content = Content.Replace("[PREVPAGELINKBKEYWORD]", "[MAINBKEYWORD]");
                    }
                }
                else
                {
                    if (PageIndex - 1 >= 0)
                    {
                        Content = Content.Replace("[PREVPAGELINK]", this.settings.LinksRelativeURLs ? this.Pages[PageIndex - 1].RelativeURL : this.Pages[PageIndex - 1].URL);
                        if (this.Pages[PageIndex - 1].Keywords.Count > 0)
                        {
                            string keyword =
                                this.Pages[PageIndex - 1].Keywords[random.Next(this.Pages[PageIndex - 1].Keywords.Count)];
                            Content = Content.Replace("[PREVPAGELINKKEYWORD]", keyword);
                            if (keyword.Length > 1)
                            {
                                keyword = keyword.Substring(0, 1).ToUpper() + keyword.Substring(1);
                            }
                            else
                            {
                                keyword = keyword.ToUpper();
                            }

                            Content = Content.Replace("[PREVPAGELINKBKEYWORD]", keyword);
                        }
                        else
                        {
                            Content = Content.Replace("[PREVPAGELINKKEYWORD]", "[MAINKEYWORD]");
                            Content = Content.Replace("[PREVPAGELINKBKEYWORD]", "[MAINBKEYWORD]");
                        }
                    }
                    else
                    {
                        Content = Content.Replace("[PREVPAGELINK]", "[MAINLINK]");
                        Content = Content.Replace("[PREVPAGELINKKEYWORD]", "[MAINKEYWORD]");
                        Content = Content.Replace("[PREVPAGELINKBKEYWORD]", "[MAINBKEYWORD]");
                    }
                }
            }
            else
            {
                Content = Content.Replace("[PREVPAGELINK]", "[MAINLINK]");
                Content = Content.Replace("[PREVPAGELINKKEYWORD]", "[MAINKEYWORD]");
                Content = Content.Replace("[PREVPAGELINKBKEYWORD]", "[MAINBKEYWORD]");
            }

            // [NEXTPAGECIRCLELINK]
            if (this.categories.Count > 0)
            {
                if (this.Pages[PageIndex].Category == -1)
                {
                    Content = Content.Replace("[NEXTPAGECIRCLELINK]", "[MAINLINK]");
                }
                else
                {
                    // Searching for link
                    for (int k = PageIndex + 1; k < this.Pages.Count; k++)
                    {
                        if (this.Pages[PageIndex].Type == 1)
                        {
                            if (this.Pages[k].Type == 1 && this.Pages[k].Category == this.Pages[PageIndex].Category)
                            {
                                Content = Content.Replace("[NEXTPAGECIRCLELINK]", this.settings.LinksRelativeURLs ? this.Pages[k].RelativeURL : this.Pages[k].URL);
                                if (this.Pages[k].Keywords.Count > 0)
                                {
                                    string keyword = this.Pages[k].Keywords[random.Next(this.Pages[k].Keywords.Count)];
                                    Content = Content.Replace("[NEXTPAGECIRCLELINKKEYWORD]", keyword);
                                    if (keyword.Length > 1)
                                    {
                                        keyword = keyword.Substring(0, 1).ToUpper() + keyword.Substring(1);
                                    }
                                    else
                                    {
                                        keyword = keyword.ToUpper();
                                    }

                                    Content
                                        = Content.Replace("[NEXTPAGECIRCLELINKBKEYWORD]", keyword);
                                }
                                else
                                {
                                    Content = Content.Replace("[NEXTPAGECIRCLELINKKEYWORD]", "[MAINKEYWORD]");
                                    Content
                                        = Content.Replace("[NEXTPAGECIRCLELINKBKEYWORD]", "[MAINBKEYWORD]");
                                }

                                break;
                            }
                        }
                    }

                    for (int k = 0; k < PageIndex; k++)
                    {
                        if (this.Pages[PageIndex].Type == 1)
                        {
                            if (this.Pages[k].Type == 1 && this.Pages[k].Category == this.Pages[PageIndex].Category)
                            {
                                Content = Content.Replace("[NEXTPAGECIRCLELINK]", this.settings.LinksRelativeURLs ? this.Pages[k].RelativeURL : this.Pages[k].URL);
                                if (this.Pages[k].Keywords.Count > 0)
                                {
                                    string keyword = this.Pages[k].Keywords[random.Next(this.Pages[k].Keywords.Count)];
                                    Content = Content.Replace("[NEXTPAGECIRCLELINKKEYWORD]", keyword);
                                    if (keyword.Length > 1)
                                    {
                                        keyword = keyword.Substring(0, 1).ToUpper() + keyword.Substring(1);
                                    }
                                    else
                                    {
                                        keyword = keyword.ToUpper();
                                    }

                                    Content = Content.Replace("[NEXTPAGECIRCLELINKBKEYWORD]", keyword);
                                }
                                else
                                {
                                    Content = Content.Replace("[NEXTPAGECIRCLELINKKEYWORD]", "[MAINKEYWORD]");
                                    Content = Content.Replace("[NEXTPAGECIRCLELINKBKEYWORD]", "[MAINBKEYWORD]");
                                }
                                break;
                            }
                        }
                    }
                    Content = Content.Replace("[NEXTPAGECIRCLELINK]", "[MAINLINK]");
                    Content = Content.Replace("[NEXTPAGECIRCLELINKKEYWORD]", "[MAINKEYWORD]");
                    Content = Content.Replace("[NEXTPAGECIRCLELINKBKEYWORD]", "[MAINBKEYWORD]");
                }
            }
            else
            {
                int nextPageIndex = 0;
                if (PageIndex != 0 && PageIndex + 1 < this.Pages.Count)
                {
                    nextPageIndex = PageIndex + 1;
                }
                else if (PageIndex != 0)
                {
                    for (int k = 0; k < this.Pages.Count; k++)
                    {
                        if (this.Pages[k].Type == 1)
                        {
                            nextPageIndex = k;
                            break;
                        }
                    }
                }
                else
                {
                    Content = Content.Replace("[NEXTPAGECIRCLELINK]", "[MAINLINK]");
                    Content = Content.Replace("[NEXTPAGECIRCLELINKKEYWORD]", "[MAINKEYWORD]");
                    Content = Content.Replace("[NEXTPAGECIRCLELINKBKEYWORD]", "[MAINBKEYWORD]");
                }

                Content = Content.Replace("[NEXTPAGECIRCLELINK]", this.settings.LinksRelativeURLs ? this.Pages[nextPageIndex].RelativeURL : this.Pages[nextPageIndex].URL);
                if (this.Pages[nextPageIndex].Keywords.Count > 0)
                {
                    string keyword =
                        this.Pages[nextPageIndex].Keywords[random.Next(this.Pages[nextPageIndex].Keywords.Count)];
                    Content = Content.Replace("[NEXTPAGECIRCLELINKKEYWORD]", keyword);
                    if (keyword.Length > 1)
                    {
                        keyword = keyword.Substring(0, 1).ToUpper() + keyword.Substring(1);
                    }
                    else
                    {
                        keyword = keyword.ToUpper();
                    }

                    Content = Content.Replace("[NEXTPAGECIRCLELINKBKEYWORD]", keyword);
                }
                else
                {
                    Content = Content.Replace("[NEXTPAGECIRCLELINKKEYWORD]", "[MAINKEYWORD]");
                    Content = Content.Replace("[NEXTPAGECIRCLELINKBKEYWORD]", "[MAINBKEYWORD]");
                }
            }

            // [PREVPAGECIRCLELINK]
            if (this.categories.Count > 0)
            {
                if (this.Pages[PageIndex].Category == -1)
                {
                    Content = Content.Replace("[PREVPAGECIRCLELINK]", "[MAINLINK]");
                    Content = Content.Replace("[PREVPAGECIRCLELINKKEYWORD]", "[MAINKEYWORD]");
                    Content = Content.Replace("[PREVPAGECIRCLELINKBKEYWORD]", "[MAINBKEYWORD]");
                }
                else
                {
                    // searching for link
                    for (int k = PageIndex - 1; k >= 0; k--)
                    {
                        if (this.Pages[PageIndex].Type == 1 && PageIndex - 1 >= 0)
                        {
                            if (this.Pages[k].Type == 1 && this.Pages[k].Category == this.Pages[PageIndex].Category)
                            {
                                Content = Content.Replace("[PREVPAGECIRCLELINK]", this.settings.LinksRelativeURLs ? this.Pages[k].RelativeURL : this.Pages[k].URL);
                                if (this.Pages[k].Keywords.Count > 0)
                                {
                                    string keyword = this.Pages[k].Keywords[random.Next(this.Pages[k].Keywords.Count)];
                                    Content = Content.Replace("[PREVPAGECIRCLELINKKEYWORD]", keyword);
                                    if (keyword.Length > 1)
                                    {
                                        keyword = keyword.Substring(0, 1).ToUpper() + keyword.Substring(1);
                                    }
                                    else
                                    {
                                        keyword = keyword.ToUpper();
                                    }

                                    Content = Content.Replace("[PREVPAGECIRCLELINKBKEYWORD]", keyword);
                                }
                                else
                                {
                                    Content = Content.Replace("[PREVPAGECIRCLELINKKEYWORD]", "[MAINKEYWORD]");
                                    Content = Content.Replace("[PREVPAGECIRCLELINKBKEYWORD]", "[MAINBKEYWORD]");
                                }
                                break;
                            }
                        }
                    }
                    for (int k = this.Pages.Count - 1; k >= PageIndex; k--)
                    {
                        if (this.Pages[PageIndex].Type == 1 && PageIndex - 1 >= 0)
                        {
                            if (this.Pages[k].Type == 1 && this.Pages[k].Category == this.Pages[PageIndex].Category)
                            {
                                Content = Content.Replace("[PREVPAGECIRCLELINK]", this.settings.LinksRelativeURLs ? this.Pages[k].RelativeURL : this.Pages[k].URL);
                                if (this.Pages[k].Keywords.Count > 0)
                                {
                                    string keyword = this.Pages[k].Keywords[random.Next(this.Pages[k].Keywords.Count)];
                                    Content = Content.Replace("[PREVPAGECIRCLELINKKEYWORD]", keyword);
                                    if (keyword.Length > 1)
                                    {
                                        keyword = keyword.Substring(0, 1).ToUpper() + keyword.Substring(1);
                                    }
                                    else
                                    {
                                        keyword = keyword.ToUpper();
                                    }

                                    Content = Content.Replace("[PREVPAGECIRCLELINKBKEYWORD]", keyword);
                                }
                                else
                                {
                                    Content = Content.Replace("[PREVPAGECIRCLELINKKEYWORD]", "[MAINKEYWORD]");
                                    Content = Content.Replace("[PREVPAGECIRCLELINKBKEYWORD]", "[MAINBKEYWORD]");
                                }
                                break;
                            }
                        }
                    }

                    Content = Content.Replace("[PREVPAGECIRCLELINK]", "[MAINLINK]");
                    Content = Content.Replace("[PREVPAGECIRCLELINKKEYWORD]", "[MAINKEYWORD]");
                    Content = Content.Replace("[PREVPAGECIRCLELINKBKEYWORD]", "[MAINBKEYWORD]");
                }
            }
            else
            {
                int prevPageIndex = 0;
                if (PageIndex - 1 > 0)
                {
                    prevPageIndex = PageIndex - 1;
                }
                else if (PageIndex - 1 == 0)
                {
                    for (int k = 0; k < this.Pages.Count; k++)
                    {
                        if (this.Pages[this.Pages.Count - 1 - k].Type == 1)
                        {
                            prevPageIndex = k;
                            break;
                        }
                    }
                }
                else
                {
                    Content = Content.Replace("[PREVPAGECIRCLELINK]", "[MAINLINK]");
                    Content = Content.Replace("[PREVPAGECIRCLELINKKEYWORD]", "[MAINKEYWORD]");
                    Content = Content.Replace("[PREVPAGECIRCLELINKBKEYWORD]", "[MAINBKEYWORD]");
                }

                Content = Content.Replace("[PREVPAGECIRCLELINK]", this.settings.LinksRelativeURLs ? this.Pages[prevPageIndex].RelativeURL : this.Pages[prevPageIndex].URL);
                if (this.Pages[prevPageIndex].Keywords.Count > 0)
                {
                    string keyword =
                        this.Pages[prevPageIndex].Keywords[random.Next(this.Pages[prevPageIndex].Keywords.Count)];
                    Content = Content.Replace("[PREVPAGECIRCLELINKKEYWORD]", keyword);
                    if (keyword.Length > 1)
                    {
                        keyword = keyword.Substring(0, 1).ToUpper() + keyword.Substring(1);
                    }
                    else
                    {
                        keyword = keyword.ToUpper();
                    }

                    Content = Content.Replace("[PREVPAGECIRCLELINKBKEYWORD]", keyword);
                }
                else
                {
                    Content = Content.Replace("[PREVPAGECIRCLELINKKEYWORD]", "[MAINKEYWORD]");
                    Content = Content.Replace("[PREVPAGECIRCLELINKBKEYWORD]", "[MAINBKEYWORD]");
                }
            }

            // Макросы ссылок продолжения главной
            if ((this.Pages[PageIndex].Type == 0 || this.Pages[PageIndex].Type == 6) && (this.settings.PagesStaticIndexContinues || this.settings.PagesDynamicIndexContinues))
            {
                // [INDEXLINK]
                if (MainSettings.Debug)
                {
                    this.Log.Append(DateTime.Now.ToString() + " Working on: [INDEXLINK]...\r\n");
                }
                //Поиск первой страницы продолжения главной
                for (int k = 0; k < this.Pages.Count; k++)
                {
                    if (this.Pages[k].Type == 6)
                    {
                        Content = Content.Replace("[INDEXLINK]", this.settings.LinksRelativeURLs ? this.Pages[k].RelativeURL : this.Pages[k].URL);
                        break;
                    }
                }
                //[NEXTINDEXLINK]
                if (MainSettings.Debug)
                {
                    this.Log.Append(DateTime.Now.ToString() + " Working on: [NEXTINDEXLINK]...\r\n");
                }
                if (this.Pages.Count > (PageIndex + 1))
                {
                    if (this.Pages[PageIndex + 1].Type == 6)
                    {
                        Content = Content.Replace("[NEXTINDEXLINK]", this.settings.LinksRelativeURLs ? this.Pages[PageIndex + 1].RelativeURL : this.Pages[PageIndex + 1].URL);
                    }
                    else
                    {
                        Content = Content.Replace("[NEXTINDEXLINK]", "[MAINLINK]");
                    }
                }
                else
                {
                    Content = Content.Replace("[NEXTINDEXLINK]", "[MAINLINK]");
                }
                //[PREVINDEXLINK]
                if (MainSettings.Debug)
                {
                    this.Log.Append(DateTime.Now.ToString() + " Working on: [PREVINDEXLINK]...\r\n");
                }
                if (PageIndex != 0)
                {
                    if (this.Pages[PageIndex - 1].Type == 6)
                    {
                        Content = Content.Replace("[PREVINDEXLINK]", this.settings.LinksRelativeURLs ? this.Pages[PageIndex - 1].RelativeURL : this.Pages[PageIndex - 1].URL);
                    }
                    else
                    {
                        Content = Content.Replace("[PREVINDEXLINK]", "[MAINLINK]");
                    }
                }
                else
                {
                    Content = Content.Replace("[PREVINDEXLINK]", "[MAINLINK]");
                }
                //[RINDEXLINK]
                if (MainSettings.Debug)
                {
                    this.Log.Append(DateTime.Now.ToString() + " Working on: [RINDEXLINK]...\r\n");
                }
                int firstIndexContinuePageIndex = 0;
                //Поиск первой страницы продолжения главной
                for (int k = 0; k < this.Pages.Count; k++)
                {
                    if (this.Pages[k].Type == 6)
                    {
                        firstIndexContinuePageIndex = k;
                        break;
                    }
                }
                while (Content.Contains("[RINDEXLINK]"))
                {
                    startPosition = Content.IndexOf("[RINDEXLINK]");
                    Content = Content.Remove(startPosition, 12);

                    if (this.settings.PagesStaticIndexContinues)
                    {
                        int idx = firstIndexContinuePageIndex + random.Next((this.keywords.Count/this.settings.PagesStaticKeysPerPageOnContinues) - 1);
                        Content = Content.Insert(startPosition, this.settings.LinksRelativeURLs ? this.Pages[idx].RelativeURL : this.Pages[idx].URL);
                    }
                    else
                    {
                        int idx = firstIndexContinuePageIndex + random.Next((this.keywords.Count/this.settings.PagesDynamicKeysPerPageOnContinues) - 1);
                        Content = Content.Insert(startPosition, this.settings.LinksRelativeURLs ? this.Pages[idx].RelativeURL : this.Pages[idx].URL);
                    }
                }
                //[LASTINDEXLINK]
                if (MainSettings.Debug)
                {
                    this.Log.Append(DateTime.Now.ToString() + " Working on: [LASTINDEXLINK]...\r\n");
                }
                int lastIndexContinuePageIndex = 0;
                //Поиск первой страницы продолжения главной
                if (this.Pages[this.Pages.Count - 1].Type == 6)
                {
                    lastIndexContinuePageIndex = this.Pages.Count - 1;
                }
                else
                {
                    for (int k = 0; k < this.Pages.Count - 2; k++)
                    {
                        if (this.Pages[k].Type == 6)
                        {
                            if (this.Pages[k + 1].Type != 6)
                            {
                                lastIndexContinuePageIndex = k;
                            }
                            break;
                        }
                    }
                }
                Content = Content.Replace("[LASTINDEXLINK]", this.settings.LinksRelativeURLs ? this.Pages[lastIndexContinuePageIndex].RelativeURL : this.Pages[lastIndexContinuePageIndex].URL);
            }
            else
            {
                Content = Content.Replace("[INDEXLINK]", "[MAINLINK]");
                Content = Content.Replace("[NEXTINDEXLINK]", "[MAINLINK]");
                Content = Content.Replace("[PREVINDEXLINK]", "[MAINLINK]");
                Content = Content.Replace("[RINDEXLINK]", "[MAINLINK]");
                Content = Content.Replace("[LASTINDEXLINK]", "[MAINLINK]");
            }
            //[RSS]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RSS]...\r\n");
            }
            if (this.settings.RSS)
            {
                Content = Content.Replace("[RSS]", this.Pages[0].URL.Substring(0, this.Pages[0].URL.LastIndexOf("/") + 1) + this.Settings.RSSFileName);
            }
            else
            {
                Content = Content.Replace("[RSS]", "[MAINLINK]");
            }

            // [SITEMAP]
            if (this.settings.SiteMap && (this.settings.SiteMapType != 0))
            {
                // [SITEMAP]
                if (MainSettings.Debug)
                {
                    this.Log.Append(DateTime.Now.ToString() + " Working on: [SITEMAP]...\r\n");
                }

                //Поиск первой страницы
                int startSitemapIndex = 0;
                for (int k = 0; k < this.Pages.Count; k++)
                {
                    if (this.Pages[k].Type == 4)
                    {
                        startSitemapIndex = k;
                        break;
                    }
                }
                Content = Content.Replace("[SITEMAP]", this.settings.LinksRelativeURLs ? this.Pages[startSitemapIndex].RelativeURL : this.Pages[startSitemapIndex].URL);

                //[RSITEMAP]
                if (MainSettings.Debug)
                {
                    this.Log.Append(DateTime.Now.ToString() + " Working on: [RSITEMAP]...\r\n");
                }
                while (Content.Contains("[RSITEMAP]"))
                {
                    startPosition = Content.IndexOf("[RSITEMAP]");
                    Content = Content.Remove(startPosition, 10);
                    int idx = random.Next(startSitemapIndex, this.Pages.Count);
                    Content = Content.Insert(startPosition, this.settings.LinksRelativeURLs ? this.Pages[idx].RelativeURL : this.Pages[idx].URL);
                }
                //[LASTSITEMAP]
                if (MainSettings.Debug)
                {
                    this.Log.Append(DateTime.Now.ToString() + " Working on: [LASTSITEMAP]...\r\n");
                }
                Content = Content.Replace("[LASTSITEMAP]", this.settings.LinksRelativeURLs ? this.Pages[this.Pages.Count - 1].RelativeURL : this.Pages[this.Pages.Count - 1].URL);

                //Макросы продолжения карт сайтов, доступны только на страницах карты сайта
                if (this.Pages[PageIndex].Type == 4 && this.settings.SiteMapHTMLType == 1)
                {
                    //[NEXTSITEMAP]
                    if (MainSettings.Debug)
                    {
                        this.Log.Append(DateTime.Now.ToString() + " Working on: [NEXTSITEMAP]...\r\n");
                    }
                    //Поиск следующей страницы карты сайта
                    if (this.Pages.Count > (PageIndex + 1))
                    {
                        Content = Content.Replace("[NEXTSITEMAP]", this.settings.LinksRelativeURLs ? this.Pages[PageIndex + 1].RelativeURL : this.Pages[PageIndex + 1].URL);
                    }
                    else
                    {
                        Content = Content.Replace("[NEXTSITEMAP]", string.Empty);
                    }
                    //[PREVSITEMAP]
                    if (MainSettings.Debug)
                    {
                        this.Log.Append(DateTime.Now.ToString() + " Working on: [PREVSITEMAP]...\r\n");
                    }
                    if (this.Pages[PageIndex - 1].Type == 4)
                    {
                        Content = Content.Replace("[PREVSITEMAP]", this.settings.LinksRelativeURLs ? this.Pages[PageIndex - 1].RelativeURL : this.Pages[PageIndex - 1].URL);
                    }
                    else
                    {
                        Content = Content.Replace("[PREVSITEMAP]", "[MAINLINK]");
                    }
                }
                else
                {
                    Content = Content.Replace("[NEXTSITEMAP]", "[MAINLINK]");
                    Content = Content.Replace("[PREVSITEMAP]", "[MAINLINK]");
                }
            }
            else
            {
                Content = Content.Replace("[SITEMAP]", "[MAINLINK]");
                Content = Content.Replace("[NEXTSITEMAP]", "[MAINLINK]");
                Content = Content.Replace("[PREVSITEMAP]", "[MAINLINK]");
                Content = Content.Replace("[RSITEMAP]", "[MAINLINK]");
                Content = Content.Replace("[LASTSITEMAP]", "[MAINLINK]");
            }
            //Макросы ссылок продолжения категорий, доступны только на страницах категорийs
            if ((this.settings.PagesStaticCategoriesContinues || this.settings.PagesDynamicCategoriesContinues) && (this.Pages[PageIndex].Type == 3 || this.Pages[PageIndex].Type == 5))
            {
                //[CATEGORYCONTINUELINK]
                if (MainSettings.Debug)
                {
                    this.Log.Append(DateTime.Now.ToString() + " Working on: [CATEGORYCONTINUELINK]...\r\n");
                }
                int firstPageIndex = 0;
                for (int k = 0; k < this.Pages.Count; k++)
                {
                    if (this.Pages[k].Type == 5 && this.Pages[k].Category == this.Pages[PageIndex].Category)
                    {
                        firstPageIndex = k;
                        break;
                    }
                }
                Content = Content.Replace("[CATEGORYCONTINUELINK]", this.settings.LinksRelativeURLs ? this.Pages[firstPageIndex].RelativeURL : this.Pages[firstPageIndex].URL);
                //[NEXTCATEGORYCONTINUELINK]
                if (MainSettings.Debug)
                {
                    this.Log.Append(DateTime.Now.ToString() + " Working on: [NEXTCATEGORYCONTINUELINK]...\r\n");
                }
                if (this.Pages[PageIndex].Type == 3)
                {
                    for (int k = PageIndex; k < this.Pages.Count; k++)
                    {
                        if (this.Pages[k].Type == 5 && this.Pages[k].Category == this.Pages[PageIndex].Category)
                        {
                            Content = Content.Replace("[NEXTCATEGORYCONTINUELINK]", this.settings.LinksRelativeURLs ? this.Pages[k].RelativeURL : this.Pages[k].URL);
                            break;
                        }
                    }
                }
                else
                {
                    if (PageIndex + 1 > this.Pages.Count)
                    {
                        if (this.Pages[PageIndex + 1].Type == 5 && this.Pages[PageIndex + 1].Category == this.Pages[PageIndex].Category)
                        {
                            Content = Content.Replace("[NEXTCATEGORYCONTINUELINK]", this.settings.LinksRelativeURLs ? this.Pages[PageIndex + 1].RelativeURL : this.Pages[PageIndex + 1].URL);
                        }
                        else
                        {
                            Content = Content.Replace("[NEXTCATEGORYCONTINUELINK]", "[MAINLINK]");
                        }
                    }
                    else
                    {
                        Content = Content.Replace("[NEXTCATEGORYCONTINUELINK]", "[MAINLINK]");
                    }
                }
                //[PREVCATEGORYCONTINUELINK]
                if (MainSettings.Debug)
                {
                    this.Log.Append(DateTime.Now.ToString() + " Working on: [PREVCATEGORYCONTINUELINK]...\r\n");
                }
                if (this.Pages[PageIndex].Type == 3)
                {
                    Content = Content.Replace("[PREVCATEGORYCONTINUELINK]", "[MAINLINK]");
                }
                else
                {
                    if (this.Pages[PageIndex - 1].Type == 5 && this.Pages[PageIndex - 1].Category == this.Pages[PageIndex].Category)
                    {
                        Content = Content.Replace("[PREVCATEGORYCONTINUELINK]", this.settings.LinksRelativeURLs ? this.Pages[PageIndex - 1].RelativeURL : this.Pages[PageIndex - 1].URL);
                    }
                    else
                    {
                        //Поиск страницы категории
                        for (int k = 0; k < this.Pages.Count; k++)
                        {
                            if (this.Pages[k].Type == 3 && this.Pages[k].Category == this.Pages[PageIndex].Category)
                            {
                                Content = Content.Replace("[PREVCATEGORYCONTINUELINK]", this.settings.LinksRelativeURLs ? this.Pages[k].RelativeURL : this.Pages[k].URL);
                                break;
                            }
                        }
                    }
                }
                //[RCATEGORYCONTINUELINK]
                if (MainSettings.Debug)
                {
                    this.Log.Append(DateTime.Now.ToString() + " Working on: [RCATEGORYCONTINUELINK]...\r\n");
                }
                //Поиск последней страницы
                int lastPageIndex = 0;
                for (int k = firstPageIndex; k < this.Pages.Count; k++)
                {
                    if (this.Pages[k].Type == 5 && this.Pages[k].Category == this.Pages[PageIndex].Category)
                    {
                        lastPageIndex = k;
                    }
                }
                //Замена
                while (Content.Contains("[RCATEGORYCONTINUELINK]"))
                {
                    startPosition = Content.IndexOf("[RCATEGORYCONTINUELINK]");
                    Content = Content.Remove(startPosition, 23);
                    int idx = random.Next(firstPageIndex, lastPageIndex + 1);
                    Content = Content.Insert(startPosition, this.settings.LinksRelativeURLs ? this.Pages[idx].RelativeURL : this.Pages[idx].URL);
                }
                //[LASTCATEGORYCONTINUELINK]
                if (MainSettings.Debug)
                {
                    this.Log.Append(DateTime.Now.ToString() + " Working on: [LASTCATEGORYCONTINUELINK]...\r\n");
                }
                //Замена
                Content = Content.Replace("[LASTCATEGORYCONTINUELINK]", this.settings.LinksRelativeURLs ? this.Pages[lastPageIndex].RelativeURL : this.Pages[lastPageIndex].URL);
            }
            else
            {
                Content = Content.Replace("[CATEGORYCONTINUELINK]", "[MAINLINK]");
                Content = Content.Replace("[NEXTCATEGORYCONTINUELINK]", "[MAINLINK]");
                Content = Content.Replace("[PREVCATEGORYCONTINUELINK]", "[MAINLINK]");
                Content = Content.Replace("[RCATEGORYCONTINUELINK]", "[MAINLINK]");
                Content = Content.Replace("[LASTCATEGORYCONTINUELINK]", "[MAINLINK]");
            }

            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [NEXTRCATEGORYLINK], [RCATEGORY], [RBCATEGORY]...\r\n");
            }
            while (Content.Contains("[RCATEGORY]") || Content.Contains("[RBCATEGORY]"))
            {
                try
                {
                    //[NEXTRCATEGORYLINK]
                    bool link = false;
                    int keywordIndex = 0;
                    startPosition = 0;

                    if (Content.Contains("[RCATEGORY]") && Content.Contains("[RBCATEGORY]"))
                    {
                        if (Content.Contains("[NEXTRCATEGORYLINK]") && Content.IndexOf("[NEXTRCATEGORYLINK]") < Content.IndexOf("[RCATEGORY]") && Content.IndexOf("[NEXTRCATEGORYLINK]") < Content.IndexOf("[RBCATEGORY]"))
                        {
                            link = true;
                        }
                        if (Content.IndexOf("[RCATEGORY]") < Content.IndexOf("[RBCATEGORY]"))
                        {
                            startPosition = Content.IndexOf("[RCATEGORY]");
                            Content = Content.Remove(startPosition, 11);
                        }
                        else if (Content.IndexOf("[RBCATEGORY]") < Content.IndexOf("[RCATEGORY]"))
                        {
                            startPosition = Content.IndexOf("[RBCATEGORY]");
                            Content = Content.Remove(startPosition, 12);
                        }

                        keywordIndex = random.Next(this.categories.Count);
                        string selectedKeyword = this.categories[keywordIndex];

                        if (Content.IndexOf("[RBCATEGORY]") < Content.IndexOf("[RCATEGORY]"))
                        {
                            selectedKeyword = selectedKeyword.Substring(0, 1).ToUpper() + selectedKeyword.Substring(1);
                        }
                        Content = Content.Insert(startPosition, selectedKeyword);
                    }
                    else if (Content.Contains("[RCATEGORY]") && !Content.Contains("[RBCATEGORY]"))
                    {
                        //[NEXTRCATEGORYLINK]
                        link = false;
                        keywordIndex = 0;
                        if (Content.Contains("[NEXTRCATEGORYLINK]") && Content.IndexOf("[NEXTRCATEGORYLINK]") < Content.IndexOf("[RCATEGORY]"))
                        {
                            link = true;
                        }

                        startPosition = Content.IndexOf("[RCATEGORY]");
                        Content = Content.Remove(startPosition, 11);

                        keywordIndex = random.Next(this.categories.Count);

                        Content = Content.Insert(startPosition, this.categories[keywordIndex]);
                    }
                    else
                    {
                        //[NEXTRCATEGORYLINK]
                        link = false;
                        keywordIndex = 0;
                        if (Content.Contains("[NEXTRCATEGORYLINK]") && Content.IndexOf("[NEXTRCATEGORYLINK]") < Content.IndexOf("[RBCATEGORY]"))
                        {
                            link = true;
                        }

                        startPosition = Content.IndexOf("[RBCATEGORY]");
                        Content = Content.Remove(startPosition, 12);

                        keywordIndex = random.Next(this.categories.Count);
                        string selectedKeyword = this.categories[keywordIndex];

                        selectedKeyword = selectedKeyword.Substring(0, 1).ToUpper() + selectedKeyword.Substring(1);
                        Content = Content.Insert(startPosition, selectedKeyword);
                    }

                    if (link)
                    {
                        startPosition = Content.IndexOf("[NEXTRCATEGORYLINK]");
                        Content = Content.Remove(startPosition, 19);
                        //Поиск страницы
                        int pageIndex = 0;
                        for (int k = 0; k < this.Pages.Count; k++)
                        {
                            if (this.Pages[k].Type == 3)
                            {
                                for (int l = 0; l < this.Pages[k].Keywords.Count; l++)
                                {
                                    if (this.Pages[k].Keywords[l] == this.categories[keywordIndex])
                                    {
                                        pageIndex = k;
                                        break;
                                    }
                                }
                            }
                            if (pageIndex != 0)
                            {
                                break;
                            }
                        }

                        // Вставка
                        Content = Content.Insert(startPosition, this.settings.LinksRelativeURLs ? this.Pages[pageIndex].RelativeURL : this.Pages[pageIndex].URL);
                    }
                }
                catch (Exception)
                {
                }
            }

            Content = Content.Replace("[NEXTRCATEGORYLINK]", "[MAINLINK]");

            // [KEYWORDLINK]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [KEYWORDLINK]...\r\n");
            }
            Content = Content.Replace("[KEYWORDLINK]", this.settings.LinksRelativeURLs ? this.Pages[PageIndex].RelativeURL : this.Pages[PageIndex].URL);

            // [RKEYWORDLINK]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RKEYWORDLINK]...\r\n");
            }
            while (Content.Contains("[RKEYWORDLINK]"))
            {
                startPosition = Content.IndexOf("[RKEYWORDLINK]");
                Content = Content.Remove(startPosition, 14);

                int selectedIndex = 0;
                while (this.Pages[selectedIndex].Type != 1)
                {
                    selectedIndex = this.random.Next(this.Pages.Count);
                }

                Content = Content.Insert(startPosition, this.settings.LinksRelativeURLs ? this.Pages[selectedIndex].RelativeURL : this.Pages[selectedIndex].URL);
            }

            // Replace random keyword tokens
            this.ReplaceRandomKeywordTokens(PageIndex, ref Content);

            // Replace tags
            this.ReplaceTagTokens(PageIndex, ref Content);

            // [MAINLINK]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [MAINLINK]...\r\n");
            }
            switch (this.settings.MacrosesMainLinkType)
            {
                    //Full url
                case 0:
                    {
                        // Notice do not use relative URL in this mode
                        Content = Content.Replace("[MAINLINK]", /*this.settings.LinksRelativeURLs ? this.Pages[0].RelativeURL :*/ this.Pages[0].URL);
                        break;
                    }
                    // /
                case 1:
                    {
                        Content = Content.Replace("[MAINLINK]", "/");
                        break;
                    }
                    //Index
                case 2:
                    {
                        string prefix = this.Pages[PageIndex].RelativeURL.Contains("../")
                                            ? this.Pages[PageIndex].RelativeURL.Substring(0, this.Pages[PageIndex].RelativeURL.LastIndexOf("../") + 3)
                                            : string.Empty;

                        if (this.settings.PagesDoorwayType == 0)
                        {
                            //Static
                            Content = Content.Replace("[MAINLINK]", prefix + "index" + this.settings.PagesStaticExtension);
                        }
                        else
                        {
                            //Dynamic
                            Content = Content.Replace("[MAINLINK]", prefix + "index.php");
                        }
                        break;
                    }
                    //Other
                case 3:
                    {
                        Content = Content.Replace("[MAINLINK]", this.settings.MacrosesMainLink);
                        break;
                    }
            }

            // [URL]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [URL]...\r\n");
            }

            if (this.settings.GeneralDoorwayUrls.Length > index)
            {
                Content = Content.Replace("[URL]", this.settings.GeneralDoorwayUrls[index]);
            }
            else
            {
                Content = Content.Replace("[URL]", "/");
            }

            // [HOST]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [HOST]...\r\n");
            }

            if (this.settings.GeneralDoorwayUrls.Length > index)
            {
                Content = Content.Replace("[HOST]", this.settings.GeneralDoorwayUrls[index].Replace("http://", string.Empty).Replace("https://", string.Empty).Trim(' ', '/'));
            }
            else
            {
                Content = Content.Replace("[HOST]", string.Empty);
            }

            // [RUDATE]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RUDATE]...\r\n");
            }

            Content = Content.Replace("[RUDATE]", DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString());
            
            // [RRUDATE]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RRUDATE]...\r\n");
            }
            while (Content.Contains("[RRUDATE]"))
            {
                startPosition = Content.IndexOf("[RRUDATE]");
                Content = Content.Remove(startPosition, 9);
                Content = Content.Insert(startPosition, random.Next(1, 32).ToString() + "." + random.Next(1, 13).ToString() + "." + random.Next(DateTime.Now.Year - 5, DateTime.Now.Year + 6).ToString());
            }
            //[USDATE]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [USDATE]...\r\n");
            }
            Content = Content.Replace("[USDATE]", DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString());
            //[RUSDATE]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RUSDATE]...\r\n");
            }
            while (Content.Contains("[RUSDATE]"))
            {
                startPosition = Content.IndexOf("[RUSDATE]");
                Content = Content.Remove(startPosition, 9);
                Content = Content.Insert(startPosition, random.Next(1, 13).ToString() + "/" + random.Next(1, 32).ToString() + "/" + random.Next(DateTime.Now.Year - 5, DateTime.Now.Year + 6).ToString());
            }
            //[RUTIME]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RUTIME]...\r\n");
            }
            Content = Content.Replace("[RUTIME]", DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString());
            //[RRUTIME]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RRUTIME]...\r\n");
            }
            while (Content.Contains("[RRUTIME]"))
            {
                startPosition = Content.IndexOf("[RRUTIME]");
                Content = Content.Remove(startPosition, 9);
                Content = Content.Insert(startPosition, random.Next(0, 25).ToString() + ":" + random.Next(0, 61).ToString());
            }
            //[USTIME]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [USTIME]...\r\n");
            }
            if (DateTime.Now.Hour < 12)
            {
                tempString = "AM";
            }
            else
            {
                tempString = "PM";
            }
            Content = Content.Replace("[USTIME]", (DateTime.Now.Hour % 12).ToString() + ":" + DateTime.Now.Minute.ToString() + " " + tempString);

            // [RUSTIME]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RUSTIME]...\r\n");
            }

            while (Content.Contains("[RUSTIME]"))
            {
                tempString = DateTime.Now.Hour < 12 ? "AM" : "PM";

                startPosition = Content.IndexOf("[RUSTIME]");
                Content = Content.Remove(startPosition, 9);
                Content = Content.Insert(startPosition, this.random.Next(0, 12).ToString() + ":" + this.random.Next(0, 61).ToString() + " " + tempString);
            }
            //[CENDAY]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [CENDAY]...\r\n");
            }
            Content = Content.Replace("[CENDAY]", StringHelper.GetEnDay(DateTime.Now.Day % 7, false));
            //[CBENDAY]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [CBENDAY]...\r\n");
            }
            Content = Content.Replace("[CBENDAY]", StringHelper.GetEnDay(DateTime.Now.Day % 7, true));
            //[RENDAY]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RENDAY]...\r\n");
            }
            while (Content.Contains("[RENDAY]"))
            {
                startPosition = Content.IndexOf("[RENDAY]");
                Content = Content.Remove(startPosition, 7);
                Content = Content.Insert(startPosition, StringHelper.GetEnDay(random.Next(0, 8), false));
            }
            //[RBENDAY]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RBENDAY]...\r\n");
            }
            while (Content.Contains("[RBENDAY]"))
            {
                startPosition = Content.IndexOf("[RBENDAY]");
                Content = Content.Remove(startPosition, 9);
                Content = Content.Insert(startPosition, StringHelper.GetEnDay(random.Next(0, 8), true));
            }

            //[CRUDAY]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [CRUDAY]...\r\n");
            }
            Content = Content.Replace("[CRUDAY]", StringHelper.GetRuDay(DateTime.Now.Day % 7, false));
            //[CBRUDAY]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [CBRUDAY]...\r\n");
            }
            Content = Content.Replace("[CBRUDAY]", StringHelper.GetRuDay(DateTime.Now.Day % 7, true));
            //[RRUDAY]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RRUDAY]...\r\n");
            }
            while (Content.Contains("[RRUDAY]"))
            {
                startPosition = Content.IndexOf("[RRUDAY]");
                Content = Content.Remove(startPosition, 8);
                Content = Content.Insert(startPosition, StringHelper.GetRuDay(random.Next(0, 8), false));
            }
            //[RBRUDAY]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RBRUDAY]...\r\n");
            }
            while (Content.Contains("[RBRUDAY]"))
            {
                startPosition = Content.IndexOf("[RBRUDAY]");
                Content = Content.Remove(startPosition, 9);
                Content = Content.Insert(startPosition, StringHelper.GetRuDay(random.Next(0, 8), true));
            }
            //[CENMONTH]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [CENMONTH]...\r\n");
            }
            Content = Content.Replace("[CENMONTH]", StringHelper.GetEnMonth(DateTime.Now.Month - 1, false));
            //[CBENMONTH]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [CBENMONTH]...\r\n");
            }
            Content = Content.Replace("[CBENMONTH]", StringHelper.GetEnMonth(DateTime.Now.Month - 1, true));
            //[RENMONTH]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RENMONTH]...\r\n");
            }
            while (Content.Contains("[RENMONTH]"))
            {
                startPosition = Content.IndexOf("[RENMONTH]");
                Content = Content.Remove(startPosition, 10);
                Content = Content.Insert(startPosition, StringHelper.GetEnMonth(random.Next(0, 12), false));
            }
            //[RBENMONTH]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RBENMONTH]...\r\n");
            }
            while (Content.Contains("[RBENMONTH]"))
            {
                startPosition = Content.IndexOf("[RBENMONTH]");
                Content = Content.Remove(startPosition, 11);
                Content = Content.Insert(startPosition, StringHelper.GetEnMonth(random.Next(0, 12), true));
            }
            //[CRUMONTH]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [CRUMONTH]...\r\n");
            }
            Content = Content.Replace("[CRUMONTH]", StringHelper.GetRuMonth(DateTime.Now.Month - 1, false));
            //[CBRUMONTH]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [CBRUMONTH]...\r\n");
            }
            Content = Content.Replace("[CBRUMONTH]", StringHelper.GetRuMonth(DateTime.Now.Month - 1, true));
            //[RRUMONTH]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RRUMONTH]...\r\n");
            }
            while (Content.Contains("[RRUMONTH]"))
            {
                startPosition = Content.IndexOf("[RRUMONTH]");
                Content = Content.Remove(startPosition, 10);
                Content = Content.Insert(startPosition, StringHelper.GetRuMonth(random.Next(0, 12), false));
            }
            //[RBRUMONTH]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RBRUMONTH]...\r\n");
            }
            while (Content.Contains("[RBRUMONTH]"))
            {
                startPosition = Content.IndexOf("[RBRUMONTH]");
                Content = Content.Remove(startPosition, 11);
                Content = Content.Insert(startPosition, StringHelper.GetRuMonth(random.Next(0, 12), true));
            }
            //[CYEAR]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [CYEAR]...\r\n");
            }
            Content = Content.Replace("[CYEAR]", DateTime.Now.Year.ToString());
            //[RYEAR]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RYEAR]...\r\n");
            }
            while (Content.Contains("[RYEAR]"))
            {
                startPosition = Content.IndexOf("[RYEAR]");
                Content = Content.Remove(startPosition, 7);
                Content = Content.Insert(startPosition, random.Next(DateTime.Now.Year - 5, DateTime.Now.Year + 6).ToString());
            }
            //[NEXTYEAR]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [NEXTYEAR]...\r\n");
            }
            Content = Content.Replace("[NEXTYEAR]", (DateTime.Now.Year + 1).ToString());
            //[RNEXTYEAR]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RNEXTYEAR]...\r\n");
            }
            while (Content.Contains("[RNEXTYEAR]"))
            {
                startPosition = Content.IndexOf("[RNEXTYEAR]");
                Content = Content.Remove(startPosition, 11);
                Content = Content.Insert(startPosition, random.Next(DateTime.Now.Year + 1, DateTime.Now.Year + 6).ToString());
            }
            //[PASTYEAR]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [PASTYEAR]...\r\n");
            }
            Content = Content.Replace("[PASTYEAR]", (DateTime.Now.Year - 1).ToString());
            //[RPASTYEAR]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RPASTYEAR]...\r\n");
            }
            while (Content.Contains("[RPASTYEAR]"))
            {
                startPosition = Content.IndexOf("[RPASTYEAR]");
                Content = Content.Remove(startPosition, 11);
                Content = Content.Insert(startPosition, random.Next(DateTime.Now.Year - 5, DateTime.Now.Year).ToString());
            }

            //[N]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [N]...\r\n");
            }
            while (Content.Contains("[N]"))
            {
                startPosition = Content.IndexOf("[N]");
                Content = Content.Remove(startPosition, 3);
                Content = Content.Insert(startPosition, random.Next(1, 101).ToString());
            }
            //[N10]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [N10]...\r\n");
            }
            while (Content.Contains("[N10]"))
            {
                startPosition = Content.IndexOf("[N10]");
                Content = Content.Remove(startPosition, 5);
                Content = Content.Insert(startPosition, random.Next(1, 11).ToString());
            }
            //[N12]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [N12]...\r\n");
            }
            while (Content.Contains("[N12]"))
            {
                startPosition = Content.IndexOf("[N12]");
                Content = Content.Remove(startPosition, 5);
                Content = Content.Insert(startPosition, random.Next(1, 13).ToString());
            }
            //[N31]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [N31]...\r\n");
            }
            while (Content.Contains("[N31]"))
            {
                startPosition = Content.IndexOf("[N31]");
                Content = Content.Remove(startPosition, 5);
                Content = Content.Insert(startPosition, random.Next(1, 32).ToString());
            }
            //[N50]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [N50]...\r\n");
            }
            while (Content.Contains("[N50]"))
            {
                startPosition = Content.IndexOf("[N50]");
                Content = Content.Remove(startPosition, 5);
                Content = Content.Insert(startPosition, random.Next(1, 51).ToString());
            }
            //[N=???]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [N=???]...\r\n");
            }
            while (Content.Contains("[N="))
            {
                startPosition = Content.IndexOf("[N=");
                int macrossNumber = int.Parse(Content.Substring(startPosition + 3, Content.IndexOf("]", startPosition) - startPosition - 3));
                Content = Content.Remove(startPosition, Content.IndexOf("]") + 1 - startPosition);
                Content = Content.Insert(startPosition, random.Next(1, macrossNumber).ToString());
            }

            // [IMAGE], [RIMAGE]
            Content = imageRepository.ReplaceTokens(Content);

            //[RCATEGORYLINK]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [RCATEGORYLINK]...\r\n");
            }
            tempIndex = 0;
            while (Content.Contains("[RCATEGORYLINK]"))
            {
                startPosition = Content.IndexOf("[RCATEGORYLINK]");
                Content = Content.Remove(startPosition, 15);

                int categoryIndex = random.Next(this.categories.Count);
                for (int i = 0; i < this.Pages.Count; i++)
                {
                    if (this.Pages[i].Type == 3 && this.Pages[i].Category == categoryIndex)
                    {
                        categoryIndex = i;
                        break;
                    }
                }

                Content = Content.Insert(startPosition,
                                         this.settings.LinksRelativeURLs
                                             ? (this.Pages[PageIndex].RelativeURL.Contains("../")
                                                    ? this.Pages[PageIndex].RelativeURL.Substring(0, this.Pages[PageIndex].RelativeURL.LastIndexOf("../") + 3) +
                                                      this.Pages[categoryIndex].RelativeURL.Replace("../", string.Empty)
                                                    : this.Pages[categoryIndex].RelativeURL.Replace("../", string.Empty))
                                             : this.Pages[categoryIndex].URL);
            }
            //[CATEGORY]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [CATEGORY], [BCATEGORY], [RCATEGORY], [RBCATEGORY]...\r\n");
            }
            if (this.categories.Count > 0)
            {
                if (this.Pages[PageIndex].Category == -1)
                {
                    Content = Content.Replace("[CATEGORY]", string.Empty);
                    Content = Content.Replace("[BCATEGORY]", string.Empty);
                    Content = Content.Replace("[RCATEGORY]", string.Empty);
                    Content = Content.Replace("[RBCATEGORY]", string.Empty);
                }
                else
                {
                    //[CATEGORY]
                    Content = Content.Replace("[CATEGORY]", this.categories[this.Pages[PageIndex].Category]);
                    //[BCATEGORY]
                    Content = Content.Replace("[BCATEGORY]", this.categories[this.Pages[PageIndex].Category].Substring(0, 1).ToUpper() + this.categories[this.Pages[PageIndex].Category].Substring(1));
                    //[RCATEGORY]
                    while (Content.Contains("[RCATEGORY]"))
                    {
                        startPosition = Content.IndexOf("[RCATEGORY]");
                        Content = Content.Remove(startPosition, 11);
                        Content = Content.Insert(startPosition, this.categories[random.Next(this.categories.Count)]);
                    }
                    //[RBCATEGORY]
                    while (Content.Contains("[RCATEGORY]"))
                    {
                        startPosition = Content.IndexOf("[RBCATEGORY]");
                        Content = Content.Remove(startPosition, 12);
                        tempString = this.categories[random.Next(this.categories.Count)];
                        Content = Content.Insert(startPosition, tempString.Substring(0, 1).ToUpper() + tempString.Substring(1));
                    }
                }
            }
            else
            {
                Content = Content.Replace("[CATEGORY]", string.Empty);
                Content = Content.Replace("[BCATEGORY]", string.Empty);
                Content = Content.Replace("[RCATEGORY]", string.Empty);
                Content = Content.Replace("[RBCATEGORY]", string.Empty);
            }

            // Replace random text tokens
            Content = textTokensReplacer.ReplaceRandomTextTokens(Content);

            // Replace random letters tokens
            Content = textTokensReplacer.ReplaceRandomLettersTokens(Content);

            // [SITEREGULARPAGENUMBER]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [SITEREGULARPAGENUMBER]...\r\n");
            }

            if (Content.Contains("[SITEREGULARPAGENUMBER]"))
            {
                // Find number of this current regular page
                if (this.Pages[PageIndex].Type != 1)
                {
                    Content = Content.Replace("[SITEREGULARPAGENUMBER]", "[SITEPAGENUMBER]");
                }
                else
                {
                    int pageNumber = 0;
                    for (int g = 0; g <= PageIndex; g++)
                    {
                        if (this.Pages[g].Type == 1)
                        {
                            pageNumber++;
                        }
                    }
                    Content = Content.Replace("[SITEREGULARPAGENUMBER]", pageNumber.ToString());
                }
            }

            // [SITEPAGENUMBER]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [SITEPAGENUMBER]...\r\n");
            }

            if (Content.Contains("[SITEPAGENUMBER]"))
            {
                Content = Content.Replace("[SITEPAGENUMBER]", (PageIndex + 1).ToString());
            }

            //Порядковые номера
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [SN], [SNRESET]...\r\n");
            }

            int CurrentSN = 1;
            while (Content.Contains("[SN]") || Content.Contains("[SNRESET]"))
            {
                if (Content.Contains("[SN]") && Content.Contains("[SNRESET]"))
                {
                    if (Content.IndexOf("[SN]") < Content.IndexOf("[SNRESET]") && Content.IndexOf("[SN]") >= 0)
                    {
                        startPosition = Content.IndexOf("[SN]");
                        Content = Content.Remove(startPosition, 4);
                        Content = Content.Insert(startPosition, CurrentSN.ToString());
                        CurrentSN++;
                    }
                    if (Content.IndexOf("[SNRESET]") < Content.IndexOf("[SN]") && Content.IndexOf("[SNRESET]") >= 0)
                    {
                        CurrentSN = 1;
                        Content = Content.Remove(Content.IndexOf("[SNRESET]"), 9);
                    }
                }
                else if (!Content.Contains("[SN]") && Content.Contains("[SNRESET]"))
                {
                    if (Content.IndexOf("[SNRESET]") >= 0)
                    {
                        CurrentSN = 1;
                        Content = Content.Remove(Content.IndexOf("[SNRESET]"), 9);
                    }
                }
                else if (Content.Contains("[SN]") && !Content.Contains("[SNRESET]"))
                {
                    if (Content.IndexOf("[SN]") >= 0)
                    {
                        startPosition = Content.IndexOf("[SN]");
                        Content = Content.Remove(startPosition, 4);
                        Content = Content.Insert(startPosition, CurrentSN.ToString());
                        CurrentSN++;
                    }
                }
            }
        }

        private void ReplaceRandomKeywordTokens(int PageIndex, ref string Content)
        {
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() +
                                " Working on: [NEXTRKEYWORDLINK], [RKEYWORD], [RBKEYWORD], [RKEYWORDCOM], [RBKEYWORDCOM], [RKEYWORDPRECOPY], [RBKEYWORDPRECOPY], [RKEYWORDPOSTCOPY], [RBKEYWORDPOSTCOPY]...\r\n");
            }

            int startPosition = 0;

            // Replace tokens before [NEXTRKEYWORDLINK]
            if (Content.Contains("[NEXTRKEYWORDLINK]"))
            {
                while (Content.Contains("[RKEYWORD]") &&
                       Content.IndexOf("[RKEYWORD]") < Content.IndexOf("[NEXTRKEYWORDLINK]"))
                {
                    startPosition = Content.IndexOf("[RKEYWORD]");
                    Content = Content.Remove(startPosition, 10);

                    string item = this.keywords[this.random.Next(this.keywords.Count)];
                    if (!this.Pages[PageIndex].RKeywords.RandomKeywords.Contains(item))
                    {
                        this.Pages[PageIndex].RKeywords.RandomKeywords.Add(item);
                    }

                    Content = Content.Insert(startPosition, item);
                }

                while (Content.Contains("[RBKEYWORD]") &&
                       Content.IndexOf("[RBKEYWORD]") < Content.IndexOf("[NEXTRKEYWORDLINK]"))
                {
                    startPosition = Content.IndexOf("[RBKEYWORD]");
                    Content = Content.Remove(startPosition, 11);
                    string keyword = this.keywords[this.random.Next(this.keywords.Count)];

                    if (keyword.Length > 1)
                    {
                        keyword = keyword.Substring(0, 1).ToUpper() + keyword.Substring(1);
                    }
                    else
                    {
                        keyword = keyword.ToUpper();
                    }

                    if (!this.Pages[PageIndex].RKeywords.RandomBigKeywords.Contains(keyword))
                    {
                        this.Pages[PageIndex].RKeywords.RandomBigKeywords.Add(keyword);
                    }
                    Content = Content.Insert(startPosition, keyword);
                }

                while (Content.Contains("[RKEYWORDCOM]") &&
                       Content.IndexOf("[RKEYWORDCOM]") < Content.IndexOf("[NEXTRKEYWORDLINK]"))
                {
                    startPosition = Content.IndexOf("[RKEYWORDCOM]");
                    Content = Content.Remove(startPosition, 13);

                    Content = Content.Insert(startPosition,
                                             this.keywords[this.random.Next(this.keywords.Count)].Replace(" ", ", "));
                }

                while (Content.Contains("[RBKEYWORDCOM]") &&
                       Content.IndexOf("[RBKEYWORDCOM]") < Content.IndexOf("[NEXTRKEYWORDLINK]"))
                {
                    startPosition = Content.IndexOf("[RBKEYWORDCOM]");
                    Content = Content.Remove(startPosition, 14);
                    string keyword = this.keywords[this.random.Next(this.keywords.Count)];

                    if (keyword.Length > 1)
                    {
                        Content = Content.Insert(startPosition, (keyword.Substring(0, 1).ToUpper() +
                                                                 keyword.Substring(1)).Replace(" ", ", "));
                    }
                    else
                    {
                        Content = Content.Insert(startPosition, keyword.ToUpper());
                    }
                }
            }

            // Replace tokens after [NEXTRKEYWORDLINK]
            while (Content.Contains("[RKEYWORD]") || Content.Contains("[RBKEYWORD]") ||
                   Content.Contains("[RKEYWORDCOM]") || Content.Contains("[RBKEYWORDCOM]"))
            {
                try
                {
                    int keywordIndex = this.random.Next(this.keywords.Count);
                    string selectedKeyword = string.Empty;

                    startPosition = 0;

                    // Search for first token
                    KeyValuePair<string, int> firstToken = new KeyValuePair<string, int>(string.Empty, Content.Length);
                    string[] tokens = new string[] {"[RKEYWORD]", "[RBKEYWORD]", "[RKEYWORDCOM]", "[RBKEYWORDCOM]"};

                    for (int i = 0; i < tokens.Length; i++)
                    {
                        if (Content.Contains(tokens[i]))
                        {
                            int tokenIndex = Content.IndexOf(tokens[i]);
                            if (tokenIndex < firstToken.Value)
                            {
                                firstToken = new KeyValuePair<string, int>(tokens[i], tokenIndex);
                            }
                        }
                    }

                    bool link = Content.Contains("[NEXTRKEYWORDLINK]") &&
                                Content.IndexOf("[NEXTRKEYWORDLINK]") < Content.IndexOf(firstToken.Key);

                    switch (firstToken.Key)
                    {
                        case "[RKEYWORD]":
                            {
                                selectedKeyword = this.keywords[keywordIndex];
                                if (!this.Pages[PageIndex].RKeywords.RandomKeywords.Contains(selectedKeyword))
                                {
                                    this.Pages[PageIndex].RKeywords.RandomKeywords.Add(selectedKeyword);
                                }
                                break;
                            }

                        case "[RBKEYWORD]":
                            {
                                if (this.keywords[keywordIndex].Length > 1)
                                {
                                    selectedKeyword = this.keywords[keywordIndex].Substring(0, 1).ToUpper() +
                                                      this.keywords[keywordIndex].Substring(1);
                                }
                                else
                                {
                                    selectedKeyword = this.keywords[keywordIndex].ToUpper();
                                }

                                if (!this.Pages[PageIndex].RKeywords.RandomBigKeywords.Contains(selectedKeyword))
                                {
                                    this.Pages[PageIndex].RKeywords.RandomBigKeywords.Add(selectedKeyword);
                                }
                                break;
                            }

                        case "[RKEYWORDCOM]":
                            {
                                selectedKeyword = this.keywords[keywordIndex].Replace(" ", ", ");

                                break;
                            }

                        case "[RBKEYWORDCOM]":
                            {
                                if (this.keywords[keywordIndex].Length > 1)
                                {
                                    selectedKeyword =
                                        (this.keywords[keywordIndex].Substring(0, 1).ToUpper() +
                                         this.keywords[keywordIndex].Substring(1)).Replace(" ", ", ");
                                }
                                else
                                {
                                    selectedKeyword = this.keywords[keywordIndex].ToUpper();
                                }

                                break;
                            }
                    }

                    int sPosition = 0;

                    // [RKEYWORDPRECOPY]
                    while (Content.IndexOf("[RKEYWORDPRECOPY]") < Content.IndexOf(firstToken.Key) &&
                           Content.IndexOf("[RKEYWORDPRECOPY]") != -1)
                    {
                        sPosition = Content.IndexOf("[RKEYWORDPRECOPY]");
                        Content = Content.Remove(sPosition, 17);
                        Content = Content.Insert(sPosition, selectedKeyword);
                    }

                    // [RBKEYWORDPRECOPY]
                    while (Content.IndexOf("[RBKEYWORDPRECOPY]") < Content.IndexOf(firstToken.Key) &&
                           Content.IndexOf("[RBKEYWORDPRECOPY]") != -1)
                    {
                        sPosition = Content.IndexOf("[RBKEYWORDPRECOPY]");
                        Content = Content.Remove(sPosition, 18);
                        if (selectedKeyword.Length > 1)
                        {
                            Content = Content.Insert(sPosition,
                                                     selectedKeyword.Substring(0, 1).ToUpper() +
                                                     selectedKeyword.Substring(1));
                        }
                        else
                        {
                            Content = Content.Insert(sPosition, selectedKeyword.ToUpper());
                        }
                    }

                    // [RKEYWORDPOSTCOPY]
                    while (Content.IndexOf("[RKEYWORDPOSTCOPY]") > Content.IndexOf(firstToken.Key) &&
                           Content.IndexOf("[RKEYWORDPOSTCOPY]") != -1
                           && (Content.IndexOf("[RKEYWORDPOSTCOPY]") < Content.IndexOfAny(Content.IndexOf(firstToken.Key) + firstToken.Key.Length, tokens)
                           || Content.IndexOfAny(Content.IndexOf(firstToken.Key) + firstToken.Key.Length, tokens) == -1))
                    {
                        if (Content.IndexOf("[NEXTRKEYWORDLINK]", Content.IndexOf(firstToken.Key)) != -1)
                        {
                            while (Content.IndexOf("[RKEYWORDPOSTCOPY]", Content.IndexOf(firstToken.Key)) <
                                   Content.IndexOf("[NEXTRKEYWORDLINK]", Content.IndexOf(firstToken.Key)))
                            {
                                sPosition = Content.IndexOf("[RKEYWORDPOSTCOPY]");
                                Content = Content.Remove(sPosition, 18);
                                Content = Content.Insert(sPosition, selectedKeyword);
                            }

                            break;
                        }

                        sPosition = Content.IndexOf("[RKEYWORDPOSTCOPY]");
                        Content = Content.Remove(sPosition, 18);
                        Content = Content.Insert(sPosition, selectedKeyword);
                    }

                    // [RBKEYWORDPOSTCOPY]
                    while (Content.IndexOf("[RBKEYWORDPOSTCOPY]") > Content.IndexOf(firstToken.Key) &&
                           Content.IndexOf("[RBKEYWORDPOSTCOPY]") != -1
                           && (Content.IndexOf("[RBKEYWORDPOSTCOPY]") < Content.IndexOfAny(Content.IndexOf(firstToken.Key) + firstToken.Key.Length, tokens)
                           || Content.IndexOfAny(Content.IndexOf(firstToken.Key) + firstToken.Key.Length, tokens) == -1))
                    {
                        if (Content.IndexOf("[NEXTRKEYWORDLINK]", Content.IndexOf(firstToken.Key)) != -1)
                        {
                            while (Content.IndexOf("[RBKEYWORDPOSTCOPY]", Content.IndexOf(firstToken.Key)) <
                                   Content.IndexOf("[NEXTRKEYWORDLINK]", Content.IndexOf(firstToken.Key)))
                            {
                                sPosition = Content.IndexOf("[RBKEYWORDPOSTCOPY]");
                                Content = Content.Remove(sPosition, 19);
                                if (selectedKeyword.Length > 1)
                                {
                                    Content = Content.Insert(sPosition,
                                                             selectedKeyword.Substring(0, 1).ToUpper() +
                                                             selectedKeyword.Substring(1));
                                }
                                else
                                {
                                    Content = Content.Insert(sPosition, selectedKeyword.ToUpper());
                                }
                            }

                            break;
                        }

                        sPosition = Content.IndexOf("[RBKEYWORDPOSTCOPY]");
                        Content = Content.Remove(sPosition, 19);
                        if (selectedKeyword.Length > 1)
                        {
                            Content = Content.Insert(sPosition,
                                                     selectedKeyword.Substring(0, 1).ToUpper() +
                                                     selectedKeyword.Substring(1));
                        }
                        else
                        {
                            Content = Content.Insert(sPosition, selectedKeyword.ToUpper());
                        }
                    }

                    // Replace token
                    startPosition = Content.IndexOf(firstToken.Key);
                    Content = Content.Remove(startPosition, firstToken.Key.Length);
                    Content = Content.Insert(startPosition, selectedKeyword);

                    // Replace link
                    if (link)
                    {
                        int linkIndex = Content.IndexOf("[NEXTRKEYWORDLINK]");
                        Content = Content.Remove(linkIndex, 18);

                        // Поиск страницы
                        int pageIndex = 0;
                        for (int k = 0; k < this.Pages.Count; k++)
                        {
                            if (this.Pages[k].Type < 2)
                            {
                                for (int l = 0; l < this.Pages[k].Keywords.Count; l++)
                                {
                                    if (this.Pages[k].Keywords[l] == this.keywords[keywordIndex])
                                    {
                                        pageIndex = k;
                                        break;
                                    }
                                }
                            }
                            if (pageIndex != 0)
                            {
                                break;
                            }
                        }

                        // Вставка
                        Content = Content.Insert(linkIndex, this.settings.LinksRelativeURLs ? this.Pages[pageIndex].RelativeURL : this.Pages[pageIndex].URL);
                    }
                }
                catch (Exception)
                {
                }
            }

            Content = Content.Replace("[NEXTRKEYWORDLINK]", "[MAINLINK]");

            // Replace Used Random keyword tokens
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [REXISTKEYWORD], [RBEXISTKEYWORD]...\r\n");
            }

            startPosition = 0;
            bool existingRandomKeywordsNotFound = false;

            if (this.Pages[PageIndex].RKeywords.RandomKeywords.Count != 0)
            {
                while (Content.Contains("[REXISTKEYWORD]"))
                {
                    startPosition = Content.IndexOf("[REXISTKEYWORD]");
                    Content = Content.Remove(startPosition, 15);

                    string item = this.Pages[PageIndex].RKeywords.RandomKeywords[this.random.Next(this.Pages[PageIndex].RKeywords.RandomKeywords.Count)];

                    Content = Content.Insert(startPosition, item);
                }
            }
            else if(Content.Contains("[REXISTKEYWORD]"))
            {
                Content = Content.Replace("[REXISTKEYWORD]", "[RKEYWORD]");
                existingRandomKeywordsNotFound = true;
            }

            if (this.Pages[PageIndex].RKeywords.RandomBigKeywords.Count != 0)
            {
                while (Content.Contains("[RBEXISTKEYWORD]"))
                {
                    startPosition = Content.IndexOf("[RBEXISTKEYWORD]");
                    Content = Content.Remove(startPosition, 16);

                    string item = this.Pages[PageIndex].RKeywords.RandomBigKeywords[this.random.Next(this.Pages[PageIndex].RKeywords.RandomBigKeywords.Count)];

                    Content = Content.Insert(startPosition, item);
                }
            }
            else if (Content.Contains("[RBEXISTKEYWORD]"))
            {
                Content = Content.Replace("[RBEXISTKEYWORD]", "[RBKEYWORD]");
                existingRandomKeywordsNotFound = true;
            }

            if (existingRandomKeywordsNotFound)
            {
                ReplaceRandomKeywordTokens(PageIndex, ref Content);
            }
        }

        private List<string> NetLinks { get; set; }
        private List<string[]> NetLinksAnchors { get; set; }

        private void PrepareNetLinks()
        {
            if (this.NetLinks != null && this.NetLinksAnchors != null)
            {
                return;
            }

            this.NetLinks = new List<string>(this.settings.LinksExternalList.Length);
            this.NetLinksAnchors = new List<string[]>(this.settings.LinksExternalList.Length);
            if (this.settings.LinksExternal)
            {
                for (int i = 0; i < this.settings.LinksExternalList.Length; i++)
                {
                    string[] tempStrings = this.settings.LinksExternalList[i].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    this.NetLinks.Add(tempStrings[0]);

                    if (tempStrings.Length > 1)
                    {
                        this.NetLinksAnchors.Add(new string[tempStrings.Length - 1]);
                        for (int k = 1; k < tempStrings.Length; k++)
                        {
                            this.NetLinksAnchors[this.NetLinksAnchors.Count - 1][k - 1] = tempStrings[k];
                        }
                    }
                    else
                    {
                        this.NetLinksAnchors.Add(new string[] { string.Empty });
                    }
                }
            }
        }

        private string ReplaceNetTokens(string Content)
        {
            // [NETBLOCK]
            if (MainSettings.Debug)
            {
                this.Log.Append(DateTime.Now.ToString() + " Working on: [NETLINK], [NETKEYWORD], [NETBKEYWORD]...\r\n");
            }

            // Подготовка ссылок
            if (Content.Contains("[NETLINK]") || Content.Contains("[NETKEYWORD]") || Content.Contains("[NETBKEYWORD]"))
            {
                this.PrepareNetLinks();

                int tempIndex = this.random.Next(this.NetLinks.Count);
                int startPosition = 0;

                if (textTokensReplacer.Context == null)
                {
                    textTokensReplacer.Context = GetTextTokensReplacerContext();
                }

                while (Content.Contains("[NETLINK]"))
                {
                    startPosition = Content.IndexOf("[NETLINK]");
                    Content = Content.Remove(startPosition, 9);
                    Content = Content.Insert(startPosition, this.NetLinks[tempIndex]);

                    if (Content.Contains("[NETLINK]") && Content.Contains("[NETKEYWORD]"))
                    {
                        while (Content.Contains("[NETKEYWORD]") && Content.IndexOf("[NETKEYWORD]") < Content.IndexOf("[NETLINK]"))
                        {
                            startPosition = Content.IndexOf("[NETKEYWORD]");
                            Content = Content.Remove(startPosition, 12);

                            string anchor = textTokensReplacer.ReplaceRandomTextTokens(this.NetLinksAnchors[tempIndex][this.random.Next(this.NetLinksAnchors[tempIndex].Length)]);

                            Content = Content.Insert(startPosition, anchor);
                        }
                    }
                    else
                    {
                        string anchor = textTokensReplacer.ReplaceRandomTextTokens(this.NetLinksAnchors[tempIndex][this.random.Next(this.NetLinksAnchors[tempIndex].Length)]);
                        Content = Content.Replace("[NETKEYWORD]", anchor);
                    }

                    if (Content.Contains("[NETLINK]") && Content.Contains("[NETBKEYWORD]"))
                    {
                        while (Content.Contains("[NETBKEYWORD]") && Content.IndexOf("[NETBKEYWORD]") < Content.IndexOf("[NETLINK]"))
                        {
                            startPosition = Content.IndexOf("[NETBKEYWORD]");
                            Content = Content.Remove(startPosition, 13);
                            string anchor = textTokensReplacer.ReplaceRandomTextTokens(this.NetLinksAnchors[tempIndex][this.random.Next(this.NetLinksAnchors[tempIndex].Length)]);
                            if (anchor.Length > 1)
                            {
                                Content = Content.Insert(startPosition, anchor.Substring(0, 1).ToUpper() + anchor.Substring(1));
                            }
                            else
                            {
                                Content = Content.Insert(startPosition, anchor.ToUpper());
                            }
                        }
                    }
                    else
                    {
                        string anchor = textTokensReplacer.ReplaceRandomTextTokens(this.NetLinksAnchors[tempIndex][this.random.Next(this.NetLinksAnchors[tempIndex].Length)]);
                        if (anchor.Length > 1)
                        {
                            Content = Content.Replace("[NETBKEYWORD]", anchor.Substring(0, 1).ToUpper() + anchor.Substring(1));
                        }
                        else
                        {
                            Content = Content.Replace("[NETBKEYWORD]", anchor.ToUpper());
                        }
                    }

                    tempIndex++;
                    if (this.NetLinks.Count <= tempIndex)
                    {
                        tempIndex = 0;
                    }
                }

                if (Content.Contains("[NETKEYWORD]") || Content.Contains("[NETBKEYWORD]"))
                {
                    Content = Content.Replace("[NETKEYWORD]", "[MAINKEYWORD]").Replace("[NETBKEYWORD]", "[MAINBKEYWORD]");
                }
            }

            return Content;
        }

        private void LoadFileMacrosesContent()
        {
            for (int i = 0; i < this.settings.FileMacroses.Length; i++)
            {
                try
                {
                    if (!File.Exists(this.settings.FileMacroses[i].File))
                    {
                        this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000494") + this.settings.FileMacroses[i].File + View.UILanguageResources.GetString("S0000495") + this.settings.FileMacroses[i].Macross + View.UILanguageResources.GetString("S0000496") + ".\r\n");
                        continue;
                    }

                    this.fileMacroses.Add(new FileMacrossData()
                                              {
                                                  Macross = this.settings.FileMacroses[i].Macross,
                                                  Content =
                                                      File.ReadAllLines(this.settings.FileMacroses[i].File,
                                                                        (this.settings.FileMacroses[i].EncodingType == 0
                                                                             ? Encoding.Default
                                                                             : Encoding.UTF8)),
                                                  Type = this.settings.FileMacroses[i].Type
                                              });
                }
                catch (Exception){}
            }
        }

        private void ReplaceEncryptMacroses(int PageIndex)
        {
            int startPosition = 0;

            while (this.Pages[PageIndex].Content.Contains("[ENCRYPT]") && this.Pages[PageIndex].Content.Contains("[/ENCRYPT]"))
            {
                startPosition = this.Pages[PageIndex].Content.IndexOf("[ENCRYPT]");
                string encryptedContent = this.Pages[PageIndex].Content.Substring(startPosition + 9, this.Pages[PageIndex].Content.IndexOf("[/ENCRYPT]", startPosition) - 9 - startPosition);
                this.Pages[PageIndex].Content = this.Pages[PageIndex].Content.Remove(startPosition, this.Pages[PageIndex].Content.IndexOf("[/ENCRYPT]", startPosition) + 10 - startPosition);
                
                // Inserting
                this.Pages[PageIndex].Content = this.Pages[PageIndex].Content.Insert(startPosition, "<script language=javascript>document.write(unescape('" + encryptedContent.ToHex() + "'))</script>");
            }
        }

        private void ReplaceFileMacroses(int PageIndex)
        {
            // Replace custom tokens
            if (this.settings.FileMacroses.Length == 0)
            {
                return;
            }

            int startPosition = 0;

            for (int i = 0; i < this.fileMacroses.Count; i++)
            {
                // Замена
                while (this.Pages[PageIndex].Content.Contains(this.fileMacroses[i].Macross))
                {
                    startPosition = this.Pages[PageIndex].Content.IndexOf(this.fileMacroses[i].Macross);
                    this.Pages[PageIndex].Content = this.Pages[PageIndex].Content.Remove(startPosition, this.fileMacroses[i].Macross.Length);
                    try
                    {
                        switch (this.fileMacroses[i].Type)
                        {
                            case 2:
                            case 3:
                                {
                                    this.Pages[PageIndex].Content = this.Pages[PageIndex].Content.Insert(startPosition, this.fileMacroses[i].GetString(index));
                                    break;
                                }

                            default:
                                {
                                    this.Pages[PageIndex].Content = this.Pages[PageIndex].Content.Insert(startPosition, this.fileMacroses[i].GetString());
                                    break;
                                }
                        }
                    }
                    catch (Exception) { }
                }
            }
        }

        private string RedirectCode(int PageIndex)
        {
            string redirectString = ReplaceMacrosesInRedirectCode(PageIndex, this.settings.EntranceCode);
            switch (this.settings.EntranceInsertType)
            {
                //Document.write
                case 1:
                    {
                        if (this.settings.EntranceJSEncrypt)
                        {
                            redirectString = EncryptString(redirectString);
                        }
                        redirectString = "document.write(\"" + redirectString + "\");";
                        break;
                    }
                //JS File
                case 2:
                    {
                        if (this.settings.EntranceJSEncrypt)
                        {
                            redirectString = EncryptString(redirectString);
                        }
                        //Запись файла
                        if (this.settings.PagesDoorwayType == 0)
                        {
                            //Статический дор
                            if (!Directory.Exists(this.Pages[PageIndex].Name.Substring(0, this.Pages[PageIndex].Name.LastIndexOf("\\") + 1)))
                            {
                                string directory = this.Pages[PageIndex].Name.Substring(0, this.Pages[PageIndex].Name.LastIndexOf("\\") + 1);
                                Directory.CreateDirectory(directory);
                                IOHelper.SetFileDirectoryDate(directory, random, settings);
                            }
                            File.WriteAllText(this.Pages[PageIndex].Name.Substring(0, this.Pages[PageIndex].Name.LastIndexOf(".") + 1) + "js", redirectString, Encoding.Default);
                        }
                        else
                        {
                            //Динамический дор
                            if (!Directory.Exists(this.Pages[PageIndex].Name.Substring(0, this.Pages[PageIndex].Name.LastIndexOf("\\") + 1)))
                            {
                                string directory = this.Pages[PageIndex].Name.Substring(0, this.Pages[PageIndex].Name.LastIndexOf("\\") + 1);
                                Directory.CreateDirectory(directory);
                                IOHelper.SetFileDirectoryDate(directory, random, settings);
                            }

                            string fileName = this.Pages[PageIndex].Name.Substring(0, this.Pages[PageIndex].Name.LastIndexOf("\\") + 1) + "script-" + (PageIndex + 1).ToString() + ".js";
                            File.WriteAllText(fileName, redirectString, Encoding.Default);
                            IOHelper.SetFileDirectoryDate(fileName,random,settings);
                        }
                        //Скрипт для вставки в страницу
                        redirectString = "<script language=\"javascript\" src=\"";
                        if (this.settings.PagesDoorwayType == 0)
                        {
                            //Статический дор
                            if (this.Pages[PageIndex].URL.Contains("."))
                            {
                                redirectString += this.Pages[PageIndex].URL.Substring(0, this.Pages[PageIndex].URL.LastIndexOf("."));
                            }
                            else
                            {
                                redirectString += this.Pages[PageIndex].URL + "index";
                            }
                        }
                        else
                        {
                            //Динамический дор
                            redirectString += this.Pages[PageIndex].URL.Substring(0, this.Pages[PageIndex].URL.LastIndexOf("/") + 1) + "script-" + (PageIndex + 1).ToString();
                        }

                        redirectString += ".js\"></script>";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return redirectString;
        }

        private string EncryptString(string InputText)
        {
            StringBuilder temp = new StringBuilder("var temp=\"\",i,c=0,out=\"\"; var str=\"", 500);

            for (int i = 0; i < InputText.Length; i++)
            {
                temp.Append(((int)InputText[i]).ToString() + "!");
            }

            temp.Append("\";l=str.length;while(c<=str.length-1){while(str.charAt(c)!='!')temp=temp+str.charAt(c++);c++; out=out+String.fromCharCode(temp);temp=\"\";}document.write(out);");
            return temp.ToString();
        }

        private string ReplaceMacrosesInRedirectCode(int PageIndex, string InputText)
        {
            string redirectString = InputText;
            //Замена макросов кейвордов в коде редиректа
            if (this.Pages[PageIndex].Type != 2 || this.Pages[PageIndex].Type != 4 ||
                this.Pages[PageIndex].Type != 5 || this.Pages[PageIndex].Type != 6)
            {
                redirectString = redirectString.Replace("[MINUSKEYWORD]", this.Pages[0].Keywords[0].Replace(" ", "-"));
                redirectString = redirectString.Replace("[PLUSKEYWORD]", this.Pages[0].Keywords[0].Replace(" ", "+"));
                redirectString = redirectString.Replace("[UNDERKEYWORD]", this.Pages[0].Keywords[0].Replace(" ", "_"));
            }
            else
            {
                redirectString = redirectString.Replace("[MINUSKEYWORD]", this.Pages[PageIndex].Keywords[0].Replace(" ", "-"));
                redirectString = redirectString.Replace("[PLUSKEYWORD]", this.Pages[PageIndex].Keywords[0].Replace(" ", "+"));
                redirectString = redirectString.Replace("[UNDERKEYWORD]", this.Pages[PageIndex].Keywords[0].Replace(" ", "_"));
            }
            return redirectString;
        }
       
        private void MakeRSS()
        {
            //Генерирование
            StringBuilder rss = new StringBuilder(1500);
            rss.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><rss version=\"2.0\" xmlns:content=\"http://purl.org/rss/1.0/modules/content/\" xmlns:wfw=\"http://wellformedweb.org/CommentAPI/\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:atom=\"http://www.w3.org/2005/Atom\" xmlns:sy=\"http://purl.org/rss/1.0/modules/syndication/\"	xmlns:slash=\"http://purl.org/rss/1.0/modules/slash/\">");
            rss.Append("\r\n<channel>");
            if (this.settings.MacrosesTitle.Length > 0)
            {
                rss.Append("\r\n<title>" + this.settings.MacrosesTitle[0] + "</title>");
            }
            else
            {
                rss.Append("\r\n<title>" + this.keywords[0] + "</title>");
            }
            rss.Append("\r\n<atom:link href=\"" + this.settings.GeneralDoorwayUrls[index] + Settings.RSSFileName + "\" rel=\"self\" type=\"application/rss+xml\" />");
            rss.Append("\r\n<link>" + this.settings.GeneralDoorwayUrls[index] + "</link>");
            rss.Append("\r\n<description></description>");
            rss.Append("\r\n<lastBuildDate>" + StringHelper.GetEnDay(random.Next(0, 6), true).ToString().Substring(0, 3) + ", " + random.Next(1, 29).ToString() + " " +
                       StringHelper.GetEnMonth(random.Next(0, DateTime.Now.Month), true).ToString().Substring(0, 3) + " " + DateTime.Now.Year.ToString() + " " + StringHelper.GetTwoDigitNumber(random.Next(1, 25)) + ":" +
                       StringHelper.GetTwoDigitNumber(random.Next(0, 60)) + ":" + StringHelper.GetTwoDigitNumber(random.Next(0, 60)) + " +0000</lastBuildDate>");
            rss.Append("\r\n<language>en</language>");
            rss.Append("\r\n<sy:updatePeriod>hourly</sy:updatePeriod>");
            rss.Append("\r\n<sy:updateFrequency>1</sy:updateFrequency>");
            //Pages
            //Поиск первой страницы обычных страниц
            int startPageIndex = 0;
            for (int i = 0; i < this.Pages.Count; i++)
            {
                if (this.Pages[i].Type == 1)
                {
                    startPageIndex = i;
                    break;
                }
            }
            //Changin rss count
            if ((this.Pages.Count - startPageIndex) < this.settings.RSSCount)
            {
                this.settings.RSSCount = this.Pages.Count - startPageIndex;
            }
            //Генерирование страниц
            for (int i = 0; i < this.settings.RSSCount; i++)
            {
                rss.Append("\r\n<item>");
                rss.Append("\r\n<title>" + this.Pages[startPageIndex + i].Keywords[0] + "</title>");
                rss.Append("\r\n<link>" + this.Pages[startPageIndex + i].URL + "</link>");
                rss.Append("\r\n<pubDate>" + StringHelper.GetEnDay(random.Next(0, 6), true).ToString().Substring(0, 3) + ", " + random.Next(1, 29).ToString() + " " +
                           StringHelper.GetEnMonth(random.Next(0, DateTime.Now.Month), true).ToString().Substring(0, 3) + " " + DateTime.Now.Year.ToString() + " " + StringHelper.GetTwoDigitNumber(random.Next(1, 25)) + ":" +
                           StringHelper.GetTwoDigitNumber(random.Next(0, 60)) + ":" + StringHelper.GetTwoDigitNumber(random.Next(0, 60)) + " +0000</pubDate>");
                rss.Append("\r\n<guid isPermaLink=\"false\">" + this.Pages[startPageIndex + i].URL + "</guid>");
                rss.Append("\r\n<description><![CDATA[");
                try
                {
                    string rssContent = this.CleanHTML(this.Pages[(startPageIndex + i) < this.Pages.Count ? (startPageIndex + i) : (this.random.Next(this.Pages.Count))].Content);

                    rssContent = rssContent.Replace("\\", string.Empty).Replace("/", string.Empty)
                        .Replace("'", string.Empty).Replace("\"", string.Empty).Replace(":", string.Empty).Replace("<p>", string.Empty)
                        .Replace("?", string.Empty).Replace("*", string.Empty).Replace("<", string.Empty).Replace("</p>", string.Empty)
                        .Replace(">", string.Empty).Replace("|", string.Empty).Replace("№", string.Empty)
                        .Replace("«", string.Empty).Replace("»", string.Empty).Replace("&", string.Empty);

                    while (rssContent.Contains("  "))
                    {
                        rssContent = rssContent.Replace("  ", " ");
                    }

                    while (rssContent.Contains("\t\t"))
                    {
                        rssContent = rssContent.Replace("\t\t", "\t");
                    }

                    while (rssContent.Contains(" \t"))
                    {
                        rssContent = rssContent.Replace(" \t", " ");
                    }

                    while (rssContent.Contains("\t\r"))
                    {
                        rssContent = rssContent.Replace("\t\r", "\r");
                    }

                    while (rssContent.Contains("\t\n"))
                    {
                        rssContent = rssContent.Replace("\t\n", "\n");
                    }

                    while (rssContent.Contains(" \r"))
                    {
                        rssContent = rssContent.Replace(" \r", "\r");
                    }

                    while (rssContent.Contains(" \n"))
                    {
                        rssContent = rssContent.Replace(" \n", "\n");
                    }

                    while (rssContent.Contains("\r\n\r\n"))
                    {
                        rssContent = rssContent.Replace("\r\n\r\n", "\r\n");
                    }

                    while (rssContent.Contains("\r\r"))
                    {
                        rssContent = rssContent.Replace("\r\r", "\r");
                    }

                    while (rssContent.Contains("\n\n"))
                    {
                        rssContent = rssContent.Replace("\n\n", "\n");
                    }
                    
                    // Limiting size
                    if (rssContent.Length > 500)
                    {
                        rssContent = rssContent.Substring(0, 500);
                    }

                    rss.Append(rssContent);
                }
                catch (Exception)
                {
                }
                rss.Append("[...]]]></description>");
                rss.Append("\r\n</item>");
            }

            rss.Append("\r\n</channel>\r\n</rss>");

            // Запись файла
            File.WriteAllText(Path.Combine(this.doorwayFolder, Settings.RSSFileName), rss.ToString(), Encoding.UTF8);
            IOHelper.SetFileDirectoryDate(Path.Combine(this.doorwayFolder, Settings.RSSFileName), random, settings);
            this.Log.Append(DateTime.Now.ToString() + View.UILanguageResources.GetString("S0000490") + ".\r\n");
        }

        private string CleanHTML(string HTMLString)
        {
            string content = HTMLString;

            // Удаление стилей и скриптов
            while (content.Contains("<style") && content.Contains("</style>"))
            {
                content = content.Remove(content.IndexOf("<style"), content.IndexOf("</style>", content.IndexOf("<style")) + 8 - content.IndexOf("<style"));
            }

            while (content.Contains("<script") && content.Contains("</script>"))
            {
                content = content.Remove(content.IndexOf("<script"), content.IndexOf("</script>", content.IndexOf("<script")) + 9 - content.IndexOf("<script"));
            }

            while (content.Contains("<STYLE") && content.Contains("</STYLE>"))
            {
                content = content.Remove(content.IndexOf("<STYLE"), content.IndexOf("</STYLE>", content.IndexOf("<STYLE")) + 8 - content.IndexOf("<STYLE"));
            }

            while (content.Contains("<SCRIPT") && content.Contains("</SCRIPT>"))
            {
                content = content.Remove(content.IndexOf("<SCRIPT"), content.IndexOf("</SCRIPT>", content.IndexOf("<SCRIPT")) + 9 - content.IndexOf("<SCRIPT"));
            }

            Regex myReg = new Regex("<[\\/\\!]*?[^<>]*?>", RegexOptions.IgnoreCase);
            content = myReg.Replace(content, string.Empty);

            // уборка лишних пробелов
            myReg = new Regex("\\x20+");
            content = myReg.Replace(content, " ");

            // уборка функций
            while (content.Contains("function"))
            {
                try
                {
                    content = content.Remove(content.IndexOf("function"), content.IndexOf("}", content.IndexOf("function")) + 1 - content.IndexOf("function"));
                }
                catch (Exception)
                {
                    break;
                }
            }

            // уборка табов и переходов на новую строку
            while (content.Contains("\t\t"))
            {
                content = content.Replace("\t\t", "\t");
            }

            while (content.Contains("\t\r\n"))
            {
                content = content.Replace("\t\r\n", "\r\n");
            }

            while (content.Contains("\r\n\t"))
            {
                content = content.Replace("\r\n\t", "\r\n");
            }

            while (content.Contains("\r\n\r\n"))
            {
                content = content.Replace("\r\n\r\n", "\r\n");
            }

            // уборка комментариев
            while (content.Contains("<!--"))
            {
                try
                {
                    content = content.Remove(content.IndexOf("<!--"), content.IndexOf("-->", content.IndexOf("<!--")) + 3 - content.IndexOf("<!--"));
                }
                catch (Exception)
                {
                    break;
                }
            }

            while (content.Contains("@import url"))
            {
                try
                {
                    content = content.Remove(content.IndexOf("@import url"), content.IndexOf(";", content.IndexOf("@import url")) + 1 - content.IndexOf("@import url"));
                }
                catch (Exception)
                {
                    break;
                }
            }

            while (content.Contains("&nbsp;"))
            {
                content = content.Replace("&nbsp;", " ");
            }

            while (content.Contains(" \r\n"))
            {
                content = content.Replace(" \r\n", "\r\n");
            }

            // уборка табов и переходов на новую строку
            while (content.Contains("\t\r\n"))
            {
                content = content.Replace("\t\r\n", "\r\n");
            }

            while (content.Contains("\t\t"))
            {
                content = content.Replace("\t\t", "\t");
            }


            while (content.Contains("\r\n\r\n"))
            {
                content = content.Replace("\r\n\r\n", "\r\n");
            }

            while (content.Contains("//"))
            {
                content = content.Replace("//", string.Empty);
            }

            // уборка лишних пробелов
            myReg = new Regex("\\x20+");
            content = myReg.Replace(content, " ");

            return content;
        }

        private void ReplaceTextTokens(int PageIndex, ref string Content)
        {
            // Замена макросов на разных типах страниц
            if (this.Pages[PageIndex].Type == 2 || this.Pages[PageIndex].Type == 4)
            {
                Content = Content.Replace("[TEXT]", "[TEXTNOKEYS]");
                Content = Content.Replace("[BTEXT]", "[TEXTNOKEYS]");
                Content = Content.Replace("[MINITEXT]", "[MINITEXTNOKEYS]");
                Content = Content.Replace("[MICROTEXT]", "[MICROTEXTNOKEYS]");
            }

            // Замена макросов
            int startPosition = 0;
            while (Content.Contains("[TEXT]"))
            {
                startPosition = Content.IndexOf("[TEXT]");
                Content = Content.Remove(startPosition, 6);
                Content = Content.Insert(startPosition, MakeContent(PageIndex, GenerateText(0), true).Replace("{", string.Empty).Replace("}", string.Empty));
            }
            while (Content.Contains("[BTEXT]"))
            {
                startPosition = Content.IndexOf("[BTEXT]");
                Content = Content.Remove(startPosition, 7);
                string content = MakeContent(PageIndex, GenerateText(0), true).Replace("{", string.Empty).Replace("}", string.Empty).Trim();

                if (content.Length != 0)
                {
                    content = content.Substring(0, 1).ToUpper() + content.Substring(1);
                }

                if (!(content.EndsWith(".") || content.EndsWith("!") || content.EndsWith("?")))
                {
                    content += ".";
                }

                Content = Content.Insert(startPosition, content);
            }
            while (Content.Contains("[TEXTNOKEYS]"))
            {
                startPosition = Content.IndexOf("[TEXTNOKEYS]");
                Content = Content.Remove(startPosition, 12);
                Content = Content.Insert(startPosition, MakeContent(PageIndex, GenerateText(0), false).Replace("{", string.Empty).Replace("}", string.Empty));
            }
            while (Content.Contains("[MINITEXT]"))
            {
                startPosition = Content.IndexOf("[MINITEXT]");
                Content = Content.Remove(startPosition, 10);
                Content = Content.Insert(startPosition, MakeContent(PageIndex, GenerateText(1), true).Replace("{", string.Empty).Replace("}", string.Empty));
            }
            while (Content.Contains("[MINITEXTNOKEYS]"))
            {
                startPosition = Content.IndexOf("[MINITEXTNOKEYS]");
                Content = Content.Remove(startPosition, 16);
                Content = Content.Insert(startPosition, MakeContent(PageIndex, GenerateText(1), false).Replace("{", string.Empty).Replace("}", string.Empty));
            }
            while (Content.Contains("[MICROTEXT]"))
            {
                startPosition = Content.IndexOf("[MICROTEXT]");
                Content = Content.Remove(startPosition, 11);
                Content = Content.Insert(startPosition, MakeContent(PageIndex, GenerateText(2), true).Replace("{", string.Empty).Replace("}", string.Empty));
            }
            while (Content.Contains("[MICROTEXTNOKEYS]"))
            {
                startPosition = Content.IndexOf("[MICROTEXTNOKEYS]");
                Content = Content.Remove(startPosition, 17);
                Content = Content.Insert(startPosition, MakeContent(PageIndex, GenerateText(2), false).Replace("{", string.Empty).Replace("}", string.Empty));
            }
        }

        private void InsertKeywords(int PageIndex, ref List<string> Text)
        {
            int keywordCount = 0;

            // Список кейвордов
            List<string> keywords = new List<string>();
            if (!this.settings.TextGenrationPersentageForEachKeyword)
            {
                // Количество вхождений для всех кейвордов
                keywordCount = (int)(((double)Text.Count / 100.0f) * random.Next((int)this.settings.TextGenrationKeywordsPercentageMin, (int)this.settings.TextGenrationKeywordsPercentageMax));
                for (int k = 0; k < keywordCount; k++)
                {
                    if (this.Pages[PageIndex].Keywords.Count == 1)
                    {
                        keywords.Add(this.Pages[PageIndex].Keywords[0]);
                    }
                    else
                    {
                        keywords.Add(this.Pages[PageIndex].Keywords[random.Next(this.Pages[PageIndex].Keywords.Count)]);
                    }
                }
            }
            else
            {
                // Количество вхождений для каждого кейворда
                for (int i = 0; i < this.Pages[PageIndex].Keywords.Count; i++)
                {
                    keywordCount = (int)(((double)Text.Count / 100.0f) * random.Next((int)this.settings.TextGenrationKeywordsPercentageMin, (int)this.settings.TextGenrationKeywordsPercentageMax));
                    for (int k = 0; k < keywordCount; k++)
                    {
                        keywords.Add(this.Pages[PageIndex].Keywords[i]);
                    }
                }

                // Перемешивание
                for (int i = 0; i < keywords.Count; i++)
                {
                    string tempString = keywords[i];
                    int kk = random.Next(keywords.Count);
                    keywords[i] = keywords[kk];
                    keywords[kk] = tempString;
                }
            }

            // Изменения порядка слов в кейвордах
            if (this.settings.KeywordsWordsReorder)
            {
                for (int i = 0; i < keywords.Count; i++)
                {
                    if (random.Next(0, 100) < this.settings.KeywordsWordsReorderPercentage)
                    {
                        if (keywords[i].Contains(" "))
                        {
                            string[] words = keywords[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            // Перемешивание
                            for (int k = 0; k < words.Length; k++)
                            {
                                string tempString = words[k];
                                int kk = random.Next(words.Length);
                                words[k] = words[kk];
                                words[kk] = tempString;
                            }

                            // Сборка
                            keywords[i] = string.Empty;
                            for (int k = 0; k < words.Length; k++)
                            {
                                keywords[i] += words[k] + " ";
                            }

                            // Удаление последнего пробела
                            keywords[i] = keywords[i].Trim();
                        }
                    }
                }
            }

            // Замена кейвордов синонимами
            if (this.settings.KeywordsSynonyms)
            {
                try
                {
                    for (int i = 0; i < keywords.Count; i++)
                    {
                        if (random.Next(0, 100) < this.settings.KeywordsSynonymsPercentage)
                        {
                            keywords[i] = SharedData.WorkSpaces[wsIndex].Keywords[synonymsIndex].Items[random.Next(SharedData.WorkSpaces[wsIndex].Keywords[synonymsIndex].Items.Length)].Trim();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            // Выделение тегами кейвордов
            if (this.settings.TextGenrationSelectKeywords)
            {
                for (int i = 0; i < keywords.Count; i++)
                {
                    if (random.Next(0, 100) < (int)this.settings.TextGenrationSelectPercentage)
                    {
                        string selectedTag = this.settings.TextGenrationSelectKeywordsTags[random.Next(this.settings.TextGenrationSelectKeywordsTags.Length)];
                        keywords[i] = "<" + selectedTag + ">" + keywords[i].Trim() + "</" + selectedTag + ">";
                    }
                }
            }

            // Обрамление ссылками
            if (this.settings.LinksInternal)
            {
                if (this.settings.LinksInternalType == 0 || this.settings.LinksInternalType == 2)
                {
                    for (int i = 0; i < keywords.Count; i++)
                    {
                        if (random.Next(0, 100) < (int)this.settings.TextGenrationSelectPercentage)
                        {
                            string selectedTag = this.settings.TextGenrationSelectKeywordsTags[random.Next(this.settings.TextGenrationSelectKeywordsTags.Length)];
                            keywords[i] = "<a href=\"" + this.Pages[PageIndex].URL + "\">" + keywords[i].Trim() + "</a>";
                        }
                    }
                }
            }

            // "другие" кейворды
            List<string> otherKeywords = new List<string>();
            if (this.settings.TextGenrationInsertOtherKeywords)
            {
                keywordCount = (int)(((float)Text.Count / 100.0f) * (float)this.settings.TextGenrationInsertOtherKeywordsPercentage);
                for (int k = 0; k < keywordCount; k++)
                {
                    int selectedPage = 0;
                    do
                    {
                        selectedPage = random.Next(this.Pages.Count);
                    } while (this.Pages[selectedPage].Type != 1);

                    if (this.settings.TextGenrationSelectKeywords)
                    {
                        if (random.Next(0, 100) < (int)this.settings.TextGenrationSelectPercentage)
                        {
                            string selectedTag = this.settings.TextGenrationSelectKeywordsTags[random.Next(this.settings.TextGenrationSelectKeywordsTags.Length)];
                            if (this.settings.LinksInternal)
                            {
                                otherKeywords.Add("<a href=\"" + this.Pages[selectedPage].URL + "\">" + "<" + selectedTag + ">" + this.Pages[selectedPage].Keywords[random.Next(this.Pages[selectedPage].Keywords.Count)].Trim() + "</" + selectedTag + ">" + "</a>");
                            }
                            else
                            {
                                otherKeywords.Add("<" + selectedTag + ">" + this.Pages[selectedPage].Keywords[random.Next(this.Pages[selectedPage].Keywords.Count)].Trim() + "</" + selectedTag + ">");
                            }
                        }
                        else
                        {
                            if (this.settings.LinksInternal)
                            {
                                otherKeywords.Add("<a href=\"" + this.Pages[selectedPage].URL + "\">" + this.Pages[selectedPage].Keywords[random.Next(this.Pages[selectedPage].Keywords.Count)].Trim() + "</a>");
                            }
                            else
                            {
                                otherKeywords.Add(this.Pages[selectedPage].Keywords[random.Next(this.Pages[selectedPage].Keywords.Count)].Trim());
                            }
                        }
                    }
                    else
                    {
                        if (this.settings.LinksInternal)
                        {
                            otherKeywords.Add("<a href=\"" + this.Pages[selectedPage].URL + "\">" + this.Pages[selectedPage].Keywords[random.Next(this.Pages[selectedPage].Keywords.Count)].Trim() + "</a>");
                        }
                        else
                        {
                            otherKeywords.Add(this.Pages[selectedPage].Keywords[random.Next(this.Pages[selectedPage].Keywords.Count)].Trim());
                        }
                    }
                }
            }

            //Вставка кейвордов
            switch (this.settings.TextGenrationInsertKeywordsType)
            {
                //Random
                case 0:
                    {
                        for (int i = 0; i < keywords.Count; i++)
                        {
                            Text.Insert(random.Next(Text.Count), keywords[i]);
                        }
                        break;
                    }
                //With Step
                case 1:
                    {
                        int step = Text.Count / keywords.Count;
                        int startPosition = step;
                        int confusion = 0;
                        int currentKeyword = 0;
                        do
                        {
                            startPosition += step;
                            confusion = random.Next(this.settings.TextGenrationInsertKeywordsСonfusion);
                            if (random.Next(0, 2) == 0)
                            {
                                startPosition += confusion;
                            }
                            else
                            {
                                startPosition -= confusion;
                            }
                            if (currentKeyword >= keywords.Count)
                            {
                                break;
                            }
                            if (startPosition > Text.Count)
                            {
                                startPosition -= Text.Count;
                            }
                            Text.Insert(startPosition, keywords[currentKeyword]);
                            currentKeyword++;
                        } while (currentKeyword < keywords.Count);
                        break;
                    }
                //At the beginning of sentences
                case 2:
                    {
                        int usedKeywords = 0;
                        //Вставка кеев в начало предложений
                        for (int i = 0; i < Text.Count - 2; i++)
                        {
                            if (Text[i].Contains(".") || Text[i].Contains("!") || Text[i].Contains("?"))
                            {
                                if (usedKeywords >= keywords.Count)
                                {
                                    break;
                                }
                                Text.Insert(i + 1, keywords[usedKeywords]);
                                usedKeywords++;
                            }
                        }
                        //Вставка оставшихся кеев
                        while (usedKeywords < keywords.Count)
                        {
                            Text.Insert(random.Next(Text.Count), keywords[usedKeywords]);
                            usedKeywords++;
                        }
                        break;
                    }
                //After punctuation marks
                case 3:
                    {
                        int usedKeywords = 0;
                        //Вставка кеев в начало предложений
                        for (int i = 0; i < Text.Count - 2; i++)
                        {
                            bool found = false;
                            if (usedKeywords >= keywords.Count)
                            {
                                break;
                            }
                            //Поиск знака препинания
                            for (int k = 0; k < this.settings.TextGenrationPunctuationMarksList.Length; k++)
                            {
                                if (Text[i].Contains(this.settings.TextGenrationPunctuationMarksList[k]))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            //Вставка
                            if (found)
                            {
                                Text.Insert(i + 1, keywords[usedKeywords]);
                                usedKeywords++;
                            }
                        }
                        //Вставка оставшихся кеев
                        while (usedKeywords < keywords.Count)
                        {
                            Text.Insert(random.Next(Text.Count), keywords[usedKeywords]);
                            usedKeywords++;
                        }
                        break;
                    }
            }
            //Вставка "других" кейвордов
            for (int i = 0; i < otherKeywords.Count; i++)
            {
                Text.Insert(random.Next(Text.Count), otherKeywords[i]);
            }
        }

        private string MakeContent(int PageIndex, List<string> Text, bool Insert)
        {
            //Удаление лишних пробелов
            for (int i = 0; i < Text.Count; i++)
            {
                Text[i] = Text[i].Trim();
            }
            //Вставка знаков препинаний
            if (this.settings.TextGenration != 5 && this.settings.TextGenrationPunctuationMarks && this.settings.TextGenrationPunctuationMarksList.Length != 0)
            {
                int punctuationMarks = random.Next(this.settings.TextGenrationPunctuationMarksInsertMin, this.settings.TextGenrationPunctuationMarksInsertMax);
                for (int i = 0; i < punctuationMarks; i++)
                {
                    int wordPosition = random.Next(Text.Count);
                    if (ContainsPunctuationMarks(Text[wordPosition]))
                    {
                        continue;
                    }
                    Text[wordPosition] += this.settings.TextGenrationPunctuationMarksList[random.Next(this.settings.TextGenrationPunctuationMarksList.Length)];
                }
            }
            //Вставка знаков препинаний
            if (this.settings.TextGenration != 5)
            {
                int dotPosition = 0;
                if (this.settings.TextGenrationSentencesLengthType == 0)
                {
                    //Random
                    for (int i = 0; i < random.Next(this.settings.TextGenrationSentencesCount / 2, this.settings.TextGenrationSentencesCount); i++)
                    {
                        if (!Text[dotPosition].Contains(".") && !Text[dotPosition].Contains("!") && !Text[dotPosition].Contains("?"))
                        {
                            dotPosition = random.Next(Text.Count);
                            //Поиск знаков препинания
                            if (this.settings.TextGenrationPunctuationMarks)
                            {
                                if (ContainsPunctuationMarks(Text[dotPosition]))
                                {
                                    continue;
                                }
                            }
                            string mark = string.Empty;
                            switch (random.Next(5))
                            {
                                case 0:
                                    {
                                        mark = "!";
                                        break;
                                    }
                                case 1:
                                    {
                                        mark = "?";
                                        break;
                                    }
                                default:
                                    {
                                        mark = ".";
                                        break;
                                    }
                            }
                            Text[dotPosition] += mark;
                        }
                    }
                }
                else
                {
                    //Step
                    while (dotPosition < Text.Count)
                    {
                        dotPosition += this.settings.TextGenrationSentencesLength;
                        //Расчет позиции
                        if (random.Next(0, 2) == 0)
                        {
                            //+
                            dotPosition += random.Next(this.settings.TextGenrationSentencesLengthСonfusion);
                        }
                        else
                        {
                            //-
                            dotPosition -= random.Next(this.settings.TextGenrationSentencesLengthСonfusion);
                        }
                        if (dotPosition < 0 || dotPosition >= Text.Count)
                        {
                            break;
                        }
                        if (!Text[dotPosition].Contains(".") && !Text[dotPosition].Contains("!") && !Text[dotPosition].Contains("?"))
                        {
                            //Поиск знаков препинания
                            if (this.settings.TextGenrationPunctuationMarks)
                            {
                                if (ContainsPunctuationMarks(Text[dotPosition]))
                                {
                                    continue;
                                }
                            }
                            string mark = string.Empty;
                            switch (random.Next(5))
                            {
                                case 0:
                                    {
                                        mark = "!";
                                        break;
                                    }
                                case 1:
                                    {
                                        mark = "?";
                                        break;
                                    }
                                default:
                                    {
                                        mark = ".";
                                        break;
                                    }
                            }
                            Text[dotPosition] += mark;
                        }
                    }
                }
            }
            //Вставка кейвордов
            if (Insert)
            {
                InsertKeywords(PageIndex, ref Text);
            }

            // External links
            if (settings.LinksExternal && settings.LinksExternalList.Length != 0 && settings.LinksExternalInText)
            {
                int linksCount = 0;

                if (settings.LinksExternalInTextIndexPageMinimum < 0)
                {
                    settings.LinksExternalInTextIndexPageMinimum = 0;
                }
                if (settings.LinksExternalInTextIndexPageMaximum < settings.LinksExternalInTextIndexPageMinimum)
                {
                    settings.LinksExternalInTextIndexPageMaximum = settings.LinksExternalInTextIndexPageMinimum;
                }

                if (settings.LinksExternalInTextRegularPageMinimum < 0)
                {
                    settings.LinksExternalInTextRegularPageMinimum = 0;
                }
                if (settings.LinksExternalInTextRegularPageMaximum < settings.LinksExternalInTextRegularPageMinimum)
                {
                    settings.LinksExternalInTextRegularPageMaximum = settings.LinksExternalInTextRegularPageMinimum;
                }

                if (Pages[PageIndex].Type == 1)
                {
                    // Regular pages
                    linksCount = random.Next(settings.LinksExternalInTextRegularPageMinimum, settings.LinksExternalInTextRegularPageMaximum == 1 ? 2 : settings.LinksExternalInTextRegularPageMaximum);
                    if (linksCount == 2 && settings.LinksExternalInTextRegularPageMaximum == 1)
                    {
                        linksCount = 1;
                    }
                }
                else
                {
                    // The rest of pages types
                    linksCount = random.Next(settings.LinksExternalInTextIndexPageMinimum, settings.LinksExternalInTextIndexPageMaximum == 1 ? 2 : settings.LinksExternalInTextIndexPageMaximum);
                    if (linksCount == 2 && settings.LinksExternalInTextIndexPageMaximum == 1)
                    {
                        linksCount = 1;
                    }
                }

                // Prepare links
                this.PrepareNetLinks();

                // Insert link
                for (int i = 0; i < linksCount; i++)
                {
                    int position = this.random.Next(Text.Count);
                    int linkIndex = this.random.Next(NetLinks.Count);

                    string link = NetLinks[linkIndex];
                    string linkAnchor = NetLinksAnchors[linkIndex][this.random.Next(NetLinksAnchors[linkIndex].Length)];

                    Text.Insert(position, string.Format("<a href=\"{0}\">{1}</a>", link, linkAnchor));
                }
            }

            //Разбивка на параграфы
            if (this.settings.TextGenrationParagraphs)
            {
                int paragraphCount = random.Next(this.settings.TextGenrationParagraphsMin, this.settings.TextGenrationParagraphsMax) - 1;
                //Большие буквы в начале параграфов
                if (this.settings.TextGenrationSentencesMakeBigLetters && (Text[0].Length > 1))
                {
                    Text[0] = "<p>" + Text[0].Substring(0, 1).ToUpper() + Text[0].Substring(1);
                }
                else
                {
                    Text[0] = "<p>" + Text[0];
                }
                Text[Text.Count - 1] = Text[Text.Count - 1] + "</p>";

                int paragraphUsedCount = 1;
                do
                {
                    int paragraphPosition = random.Next(1, Text.Count - 1);
                    if (!Text[paragraphPosition].Contains("</p>") && !Text[paragraphPosition + 1].Contains("<p>"))
                    {
                        Text[paragraphPosition] += "</p>";
                        //Большие буквы в начале параграфов
                        if (this.settings.TextGenrationSentencesMakeBigLetters && (Text[paragraphPosition + 1].Length > 1))
                        {
                            Text[paragraphPosition + 1] = "<p>" + Text[paragraphPosition + 1].Substring(0, 1).ToUpper() + Text[paragraphPosition + 1].Substring(1);
                        }
                        else
                        {
                            Text[paragraphPosition + 1] = "<p>" + Text[paragraphPosition + 1];
                        }
                    }
                    paragraphUsedCount++;
                } while (paragraphUsedCount < paragraphCount);
                //Окончание предложений в конце параграфов
                if (this.settings.TextGenration != 5)
                {
                    for (int i = 0; i < Text.Count; i++)
                    {
                        if (Text[i].Contains("</p>"))
                        {
                            if (!Text[i].EndsWith(".</p>") && !Text[i].EndsWith("!</p>") && !Text[i].EndsWith("?</p>"))
                            {
                                string mark = string.Empty;
                                switch (random.Next(5))
                                {
                                    case 0:
                                        {
                                            mark = "!";
                                            break;
                                        }
                                    case 1:
                                        {
                                            mark = "?";
                                            break;
                                        }
                                    default:
                                        {
                                            mark = ".";
                                            break;
                                        }
                                }
                                Text[i] = Text[i].Replace("</p>", mark + "</p>");
                            }
                        }
                    }
                }
            }
            else
            {
                if (this.settings.TextGenrationSentencesMakeBigLetters)
                {
                    if (Text[0].Length > 1)
                    {
                        Text[0] = Text[0].Substring(0, 1).ToUpper() + Text[0].Substring(1);
                    }
                    else
                    {
                        Text[0] = Text[0].ToUpper();
                    }
                }
            }
            //Большие буквы
            if (this.settings.TextGenrationSentencesMakeBigLetters)
            {
                for (int i = 0; i < Text.Count - 1; i++)
                {
                    if ((Text[i].Contains(".") || Text[i].Contains("!") || Text[i].Contains("?")) && (!Text[i].Contains("<a href=") && !Text[i].Contains("</a>")))
                    {
                        if (Text[i + 1].Length > 1)
                        {
                            Text[i + 1] = Text[i + 1].Substring(0, 1).ToUpper() + Text[i + 1].Substring(1);
                        }
                        else
                        {
                            Text[i + 1] = Text[i + 1].ToUpper();
                        }
                    }
                }
            }
            //Выделение фраз
            if (this.settings.TextGenrationSelectPhrases)
            {
                for (int i = 0; i < Text.Count / (int)this.settings.TextGenrationSelectPercentage; i++)
                {
                    //Поиск
                    int startPosition = 0;
                    int endPosition = 0;
                    do
                    {
                        startPosition = random.Next(Text.Count);
                        if (this.settings.LinksInternal)
                        {
                            endPosition = startPosition + random.Next(this.settings.LinksInternalMinLength, this.settings.LinksInternalMaxLength);
                        }
                        else
                        {
                            endPosition = startPosition + random.Next(5);
                        }
                    } while (endPosition >= Text.Count);
                    //Поиск тегов
                    bool found = false;
                    for (int k = startPosition; k < endPosition + 1; k++)
                    {
                        if (Text[k].Contains("<p>") || Text[k].Contains("</p>"))
                        {
                            found = true;
                            break;
                        }
                        //теги
                        if (this.settings.TextGenrationSelectKeywords)
                        {
                            for (int l = 0; l < this.settings.TextGenrationSelectKeywordsTags.Length; l++)
                            {
                                if (Text[k].Contains(this.settings.TextGenrationSelectKeywordsTags[l]))
                                {
                                    found = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!found)
                    {
                        if (random.Next(100) < (int)this.settings.TextGenrationSelectPercentage)
                        {
                            //Выделение фраз тегами
                            string tag = this.settings.TextGenrationSelectPhrasesTags[random.Next(this.settings.TextGenrationSelectPhrasesTags.Length)];
                            Text[startPosition] = "<" + tag + ">" + Text[startPosition];
                            Text[endPosition] += "</" + tag + ">";
                        }
                        if(this.settings.LinksInternal && this.settings.LinksInternalType != 0)
                        {
                            if (random.Next(100) < (int)this.settings.TextGenrationSelectPercentage)
                            {
                                //Выделение фраз ссылками
                                Text[startPosition] = "<a href=\"" + this.Pages[random.Next(this.Pages.Count)].URL + "\">" + Text[startPosition];
                                Text[endPosition] += "</a>";
                            }
                        }
                    }
                }
            }
            //Создание строки
            StringBuilder sb = new StringBuilder(10000);
            for (int i = 0; i < Text.Count; i++)
            {
                sb.Append(Text[i] + " ");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Генерирование текста
        /// </summary>
        /// <param name="Type">0 - [TEXT]; 1 - [MINITEXT]; 2 - [MICROTEXT]</param>
        /// <returns></returns>
        private List<string> GenerateText(int Type)
        {
            List<string> Text = new List<string>();
            switch (Type)
            {
                case 0:
                    {
                        Text = this.TG.Out(random.Next(this.settings.TextGenrationTextLengthMin, this.settings.TextGenrationTextLengthMax));
                        break;
                    }

                case 1:
                    {
                        Text = this.TG.Out(random.Next(this.settings.TextGenrationTextLengthMin / 2, this.settings.TextGenrationTextLengthMax / 2));
                        break;
                    }
                    
                case 2:
                    {
                        Text = this.TG.Out(random.Next(this.settings.TextGenrationTextLengthMin / 5, this.settings.TextGenrationTextLengthMax / 5));
                        break;
                    }
            }

            // Очистка от всех знаков препинания
            /*if (this.settings.TextGenration != 5)
            {
                for (int i = 0; i < Text.Count; i++)
                {
                    Text[i] = Text[i].Replace(".", " ").Replace(",", " ").Replace("!", " ").Replace("?", " ").Replace(":", " ")
                    .Replace(";", " ").Replace("-", " ").Replace("(", " ").Replace(")", " ").Replace("[", " ").Replace("]", " ")
                    .Replace("{", " ").Replace("}", " ");
                }
            }*/

            return Text;
        }

        private bool ContainsPunctuationMarks(string Text)
        {
            for (int i = 0; i < this.settings.TextGenrationPunctuationMarksList.Length; i++)
            {
                if (Text.Contains(this.settings.TextGenrationPunctuationMarksList[i]))
                {
                    return true;
                }
            }
            return false;
        }

        private void MakeRobots()
        {
            string content = this.settings.RobotsContent;
            if (this.settings.SiteMap && (this.settings.SiteMapType == 1 || this.settings.SiteMapType == 3))
            {
                if (content.Contains("Sitemap:"))
                {
                    content = content.Substring(0, content.LastIndexOf("Sitemap:"));
                }
            }
            else
            {
                if (content.Contains("Sitemap:"))
                {
                    if (this.index < this.settings.GeneralDoorwayUrls.Length)
                    {
                        content = content.Replace("[SITEMAP]", this.settings.GeneralDoorwayUrls[index] + "sitemap.xml");
                    }
                    else
                    {
                        content = content.Substring(0, content.LastIndexOf("Sitemap:"));
                    }
                }
            }

            // Replacing host token
            if (this.index < this.settings.GeneralDoorwayUrls.Length && content.Contains("[HOST]"))
            {
                string host = this.settings.GeneralDoorwayUrls[index].ToLower().Replace("http://", string.Empty).Replace("https://", string.Empty);

                if (host.Contains("/"))
                {
                    host = host.Substring(0, host.IndexOf("/"));
                }

                content = content.Replace("[HOST]", host);
            }

            File.WriteAllText(Path.Combine(this.doorwayFolder, "robots.txt"), content, Encoding.Default);
            IOHelper.SetFileDirectoryDate(Path.Combine(this.doorwayFolder, "robots.txt"), random, settings);
        }

        private void MakePHPFile()
        {
            string phpData = (this.Settings.PagesDoorwayType == 1) ? Resource.ResourceManager.GetString("PHPText") : Resource.ResourceManager.GetString("PHPSQLite");

            // Замена макросов
            phpData = phpData.Replace("[PAGE]", this.settings.PagesDynamicPageNames);
            phpData = phpData.Replace("[CATEGORY]", this.settings.PagesDynamicCategoriesNames);
            phpData = phpData.Replace("[STATICPAGE]", this.settings.PagesDynamicStaticPageNames);
            phpData = phpData.Replace("[INDECXONTINUE]", this.settings.PagesDynamicIndexContinuesNames);
            phpData = phpData.Replace("[CATEGORYCONTINUE1]", this.settings.PagesDynamicCategoriesContinuesNames1);
            phpData = phpData.Replace("[CATEGORYCONTINUE2]", this.settings.PagesDynamicCategoriesContinuesNames2);

            // Сохранение
            File.WriteAllText(Path.Combine(this.doorwayFolder, "index.php"), phpData, Encoding.Default);
            IOHelper.SetFileDirectoryDate(Path.Combine(this.doorwayFolder, "index.php"), random, this.settings);
        }

        /// <summary>
        /// Очистка памяти
        /// </summary>
        public void ClearMemory()
        {
            if (this.Pages != null)
            {
                this.PagesCount = this.Pages.Count;
            }

            this.Pages = null;
            if (this.TG != null)
            {
                this.TG.Dispose();
            }

            this.TG = null;
            this.textTokensReplacerContext = null;
            GC.Collect();
        }

        protected TextTokensReplacerContext textTokensReplacerContext { get; set; }
        protected TextTokensReplacerContext GetTextTokensReplacerContext()
        {
            if (textTokensReplacerContext != null)
            {
                return textTokensReplacerContext;
            }

            textTokensReplacerContext = new TextTokensReplacerContext()
                                            {
                                                Random = this.random,
                                                Log = this.Log,
                                                Settings = this.settings,
                                                TextIndex = this.textIndex,
                                                WorkspaceIndex = this.wsIndex
                                            };

            return textTokensReplacerContext;
        }
        #endregion
    }
}
