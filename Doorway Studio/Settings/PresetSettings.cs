using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.IO;
using System.Text;
using System.Windows.Forms.Design;
using Settings.Automator;

namespace Settings
{
    public class PresetSettings : GlobalizedObject
    {
        public PresetSettings()
        {
            this.GeneralCreateDoorways = 1;
            this.GeneralThreads = 1;

            this.GeneralImageType = 0;
            this.GeneralImageSizeMinWidth = 100;
            this.GeneralImageSizeMaxWidth = 500;
            this.GeneralImageSizeMinHeight = 100;
            this.GeneralImageSizeMaxHeight = 500;
            this.GeneralGenerateImagesCount = 25;

            this.GeneralImageNamingType = 0;
            this.GeneralImageNamingFile = string.Empty;

            this.GeneralArchive = 0;
            this.GeneralArchiveName = string.Empty;

            this.GeneralSaveTo = string.Empty;
            this.GeneralCreateSubFolders = true;
            this.GeneralSubFoldersType = 0;

            this.GeneralDoorwayUrls = new string[0];

            this.GeneralFileDateStart = DateTime.Now;
            this.GeneralFileDateEnd = DateTime.Now;

            this.FileMacroses = new FileMacross[0];

            this.KeywordsSelectType = 0;
            this.KeywordsSelectMin = 500;
            this.KeywordsSelectMax = 1000;

            this.KeywordsReorder = false;
            this.KeywordsWordsReorder = false;
            this.KeywordsWordsReorderPercentage = 25;

            this.KeywordsSynonyms = false;
            this.KeywordsSynonymsPercentage = 25;

            this.KeywordsMerge = false;
            this.KeywordsMergeType = 2;
            this.KeywordsMergeMin = 1;
            this.KeywordsMergeMax = 1;

            this.Categories = false;
            this.CategoriesType = 0;
            this.CategoriesDynamicMin = 4;
            this.CategoriesDynamicMax = 7;
            this.CategoriesDynamicExcludeKeywords = false;
            this.CategoriesStaticList = new string[0];
            this.CategoriesDistribute = 0;

            this.PagesDoorwayType = 0;

            this.PagesStaticExtension = ".html";
            this.PagesStaticSeparator = "-";
            this.PagesStaticNamesTypes = 0;

            this.PagesStaticPageNames = "[Name]";
            this.PagesStaticCategoriesNames = "[Name]";
            this.PagesStaticIndexContinues = false;
            this.PagesStaticIndexContinuesNames = "[Name]-[N]";
            this.PagesStaticCategoriesContinues = false;
            this.PagesStaticCategoriesContinuesNames = "[CName]-[N]";
            this.PagesStaticKeysPerPageOnContinues = 10;

            this.PagesDynamicPageNames = "page";
            this.PagesDynamicCategoriesNames = "category";
            this.PagesDynamicIndexContinues = false;
            this.PagesDynamicIndexContinuesNames = "index";
            this.PagesDynamicCategoriesContinues = false;
            this.PagesDynamicCategoriesContinuesNames1 = "category";
            this.PagesDynamicCategoriesContinuesNames2 = "page";
            this.PagesDynamicKeysPerPageOnContinues = 10;

            this.StaticPages = false;
            this.StaticPagesList = new string[0];
            this.StaticPagesIncludeIntoSiteMap = false;

            this.RSS = false;
            this.RSSCount = 10;
            this.RSSFileName = "feed.xml";

            this.SiteMap = false;
            this.SiteMapType = 0;
            this.SiteMapHTMLType = 0;
            this.SiteMapHTMLName = "Map-[N]";
            this.SiteMapHTMLLinksMin = 50;
            this.SiteMapHTMLLinksMax = 100;

            this.Robots = false;
            this.RobotsType = 0;
            this.RobotsContent = string.Empty;

            this.TextGenration = 0;

            this.TextGenrationTextLengthMin = 200;
            this.TextGenrationTextLengthMax = 300;

            this.TextGenrationKeywordsMoreThanOneOnPage = false;
            this.TextGenrationKeywordsOnPageMin = 1;
            this.TextGenrationKeywordsOnPageMax = 3;

            this.TextGenrationKeywordsPercentageMin = 4;
            this.TextGenrationKeywordsPercentageMax = 7;
            this.TextGenrationInsertKeywordsType = 0;
            this.TextGenrationInsertKeywordsСonfusion = 3;
            this.TextGenrationPersentageForEachKeyword = false;
            this.TextGenrationInsertOtherKeywords = false;
            this.TextGenrationInsertOtherKeywordsPercentage = 3;

            this.TextGenrationSelectKeywords = false;
            this.TextGenrationSelectKeywordsTags = new string[] {"strong", "b", "i", "em"};
            this.TextGenrationSelectPhrases = false;
            this.TextGenrationSelectPhrasesTags = new string[] { "strong", "b", "i", "em" };
            this.TextGenrationSelectPercentage = 10;

            this.TextGenrationPunctuationMarks = false;
            this.TextGenrationPunctuationMarksInsertMin = 5;
            this.TextGenrationPunctuationMarksInsertMax = 7;
            this.TextGenrationPunctuationMarksList = new string[]{".", ",", "!", "?", ";", ":", "-"};

            this.TextGenrationSentencesCount = 10;
            this.TextGenrationSentencesMakeBigLetters = false;
            this.TextGenrationSentencesLengthType = 0;
            this.TextGenrationSentencesLength = 15;
            this.TextGenrationSentencesLengthСonfusion = 3;

            this.TextGenrationParagraphs = false;
            this.TextGenrationParagraphsMin = 1;
            this.TextGenrationParagraphsMax = 3;

            this.TextGenrationCGTextAnalyseType = 0;
            this.TextGenrationCGTextAnalyseCutWordsLength = 6;

            this.TextGenrationCGConstructionType = 0;
            this.TextGenrationCGConstructionInsertWordsMin = 2;
            this.TextGenrationCGConstructionInsertWordsMax = 5;

            this.TextGenrationCGPunctuationMarksConsideration = 0;
            this.TextGenrationCGConsiderProbability = false;

            this.TextGenrationMRAIType = 0;
            this.TextGenrationMRAIPunctuationMarksConsideration = 0;
            this.TextGenrationMRAIRadius  = 3;
            this.TextGenrationMRAIConstructionType = 0;
            this.TextGenrationMRAIConstructionInsertWordsMin = 2;
            this.TextGenrationMRAIConstructionInsertWordsMax = 5;

            this.MacrosesMainLinkType = 0;
            this.MacrosesMainLink = string.Empty;
            this.MacrosesTitle = new string[0];
            this.MacrosesSite = new string[0];

            this.EntranceType = 0;
            this.EntranceInsertType = 0;
            this.EntranceAcceptorAdressType = 0;
            this.EntranceAcceptorAdress = string.Empty;
            this.EntranceJSEncrypt = false;
            this.EntranceCode = string.Empty;
            this.EntranceCounter = string.Empty;

            this.LinksRelativeURLs = false;

            this.LinksInternal = false;
            this.LinksInternalType = 0;
            this.LinksInternalMinLength = 1;
            this.LinksInternalMaxLength = 3;

            this.LinksExternal = false;
            this.LinksExternalList = new string[0];
            this.LinksExternalInText = false;
            this.LinksExternalInTextIndexPageMinimum = 0;
            this.LinksExternalInTextIndexPageMaximum = 1;
            this.LinksExternalInTextRegularPageMinimum = 0;
            this.LinksExternalInTextRegularPageMaximum = 1;

            this.Spam = false;
            this.SpamUrlTypeList = new string[] {"[LINK]", "<a href=\"[LINK]\">[KEY]</a>", "[URL][LINK][/URL]", "[URL=[LINK]][KEY][/URL]"};
            this.SpamSaveToFile = false;
            this.SpamSaveToFileType = 0;
            this.SpamSaveToFilePath = string.Empty;
            this.SpamSaveEncoding = 0;

            this.TagSettings = new TagSettings[0];

            this.FTPUpload = false;
            this.FTPUploadType = 0;
            this.FTPUploadSaveTo = string.Empty;
            this.FTPDelete = false;
            this.FTPUploadArchive = 0;
            this.FTPUploadArchiveName = string.Empty;
            this.FTPUploadInBackground = false;
            this.FTPThreads = 1;
            this.FTPSettings = new FTPSettings[0];

            this.XRumerUse = false;
            this.XRumerDirectory = string.Empty;
            this.XRumerText = string.Empty;
            this.XRumerTemplate = string.Empty;
        }

        #region General
        /// <summary>
        /// Кол-во дорвеев
        /// </summary>
        [Category("General"), DefaultValueAttribute(1)]
        public int GeneralCreateDoorways { get; set; }

        /// <summary>
        /// Кол-во потоков на задание
        /// </summary>
        [Category("General"), DefaultValueAttribute(1)]
        public int GeneralThreads { get; set; }

        /// <summary>
        /// Тип использования картинок
        /// </summary>
        [Category("General"), DefaultValueAttribute(0)]
        public int GeneralImageType { get; set; }

        /// <summary>
        /// Минимальная ширина генерируемых картинок
        /// </summary>
        [Category("General"), DefaultValueAttribute(100)]
        public int GeneralImageSizeMinWidth { get; set; }

        /// <summary>
        /// Минимальная высота генерируемых картинок
        /// </summary>
        [Category("General"), DefaultValueAttribute(100)]
        public int GeneralImageSizeMinHeight { get; set; }

        /// <summary>
        /// Максимальная ширина генерируемых картинок
        /// </summary>
        [Category("General"), DefaultValueAttribute(500)]
        public int GeneralImageSizeMaxWidth { get; set; }

        /// <summary>
        /// Максимальная высота генерируемых картинок
        /// </summary>
        [Category("General"), DefaultValueAttribute(500)]
        public int GeneralImageSizeMaxHeight { get; set; }

        /// <summary>
        /// Количество картинок, которое надо сгенерировать
        /// </summary>
        [Category("General"), DefaultValueAttribute(25)]
        public int GeneralGenerateImagesCount { get; set; }

        /// <summary>
        /// Тип генерирования имен картинок: 0 - случайно; 1 - из файла;2 - из файла (транслит)
        /// </summary>
        [Category("General"), DefaultValueAttribute(0)]
        public int GeneralImageNamingType { get; set; }

        /// <summary>
        /// Путь у файлу со списком имен для картинок
        /// </summary>
        [Category("General"), DefaultValueAttribute("")]
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string GeneralImageNamingFile { get; set; }

        /// <summary>
        /// Сжимать дорвеи в архив
        /// </summary>
        [Category("General"), DefaultValue(0)]
        public int GeneralArchive { get; set; }

        /// <summary>
        /// Имя архива
        /// </summary>
        [Category("General"), DefaultValue("archive.zip")]
        public string GeneralArchiveName { get; set; }

        /// <summary>
        /// Путь куда сохранять доры
        /// </summary>
        [Category("General")]
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string GeneralSaveTo { get; set; }

        /// <summary>
        /// Путь куда сохранять доры
        /// </summary>
        [Category("General"), DefaultValue(true)]
        public bool GeneralCreateSubFolders { get; set; }

        /// <summary>
        /// Тип имен саб-папок
        /// </summary>
        [Category("General"), DefaultValue(0)]
        public int GeneralSubFoldersType { get; set; }

        /// <summary>
        /// Ссылки на дорвеи
        /// </summary>
        [Category("General"), Browsable(false)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string[] GeneralDoorwayUrls { get; set; }

        /// <summary>
        /// Gets or sets file date start
        /// </summary>
        [Category("General")]
        public DateTime GeneralFileDateStart { get; set; }

        /// <summary>
        /// Gets or sets file date end
        /// </summary>
        [Category("General")]
        public DateTime GeneralFileDateEnd { get; set; }
        #endregion

        #region FileMacroses
        [Category("FileTokens"), Browsable(false)]
        public FileMacross[] FileMacroses { get; set; }
        #endregion

        #region Keywords
        /// <summary>
        /// ID кейвордов, с которыми генерировать дорген
        /// </summary>
        [Category("Keywords"), DefaultValue(0), Browsable(false)]
        public int KeywordsID { get; set; }

        /// <summary>
        /// Тип выборки кейвордов из общего числа для конкретного дорвея.
        /// 0 - все; 1 - первые; 2 - последние; 3 - случайно; 4 - последовательно
        /// </summary>
        [Category("Keywords"), DefaultValue(0)]
        public int KeywordsSelectType { get; set; }

        /// <summary>
        /// Минимально выбираемое количество кейвордов
        /// </summary>
        [Category("Keywords"), DefaultValue(500)]
        public int KeywordsSelectMin { get; set; }

        /// <summary>
        /// Миксимально выбираемое количество кейвордов
        /// </summary>
        [Category("Keywords"), DefaultValue(1000)]
        public int KeywordsSelectMax { get; set; }

        /// <summary>
        /// Перемешивание кейвордов
        /// </summary>
        [Category("Keywords"), DefaultValue(false)]
        public bool KeywordsReorder { get; set; }

        /// <summary>
        /// Перемешивание слов в кейвордах
        /// </summary>
        [Category("Keywords"), DefaultValue(false)]
        public bool KeywordsWordsReorder { get; set; }

        /// <summary>
        /// Количество кейвордов в процентах, в котоых нужно перемешивать слова
        /// </summary>
        [Category("Keywords"), DefaultValue(25)]
        public int KeywordsWordsReorderPercentage { get; set; }

        /// <summary>
        /// Использовать синонимы
        /// </summary>
        [Category("Keywords"), DefaultValue(false)]
        public bool KeywordsSynonyms { get; set; }

        /// <summary>
        /// Количество кейвордов в процентах, которые нужно заменить синонимами
        /// </summary>
        [Category("Keywords"), DefaultValue(25)]
        public int KeywordsSynonymsPercentage { get; set; }

        /// <summary>
        /// ID кейвордов, кейвордами откуда нужно синонимизировать кейворды
        /// </summary>
        [Category("Keywords"), DefaultValue(0)]
        public int KeywordsSynonymsID { get; set; }

        /// <summary>
        /// Склеивать кейворды
        /// </summary>
        [Category("Keywords"), DefaultValue(false)]
        public bool KeywordsMerge { get; set; }

        /// <summary>
        /// ID кейвордов, с которыми нужно склеитьвать кейворды
        /// </summary>
        [Category("Keywords"), DefaultValue(0)]
        public int KeywordsMergeID { get; set; }

        /// <summary>
        /// Тип склейки кейвордов
        /// </summary>
        [Category("Keywords"), DefaultValue(2)]
        public int KeywordsMergeType { get; set; }

        /// <summary>
        /// Минимальное количество кейвордов для склейки
        /// </summary>
        [Category("Keywords"), DefaultValue(1)]
        public int KeywordsMergeMin { get; set; }

        /// <summary>
        /// Максимальное количество кейвордов для склейки
        /// </summary>
        [Category("Keywords"), DefaultValue(1)]
        public int KeywordsMergeMax { get; set; }
        #endregion

        #region Categories
        /// <summary>
        /// Использовать категории
        /// </summary>
        [Category("Categories"), DefaultValue(false)]
        public bool Categories { get; set; }

        /// <summary>
        /// Тип категорий
        /// </summary>
        [Category("Categories"), DefaultValue(0)]
        public int CategoriesType { get; set; }

        /// <summary>
        /// Список статических категорий
        /// </summary>
        [Category("Categories")]
        public string[] CategoriesStaticList { get; set; }

        [Category("Categories"), DefaultValue(false)]
        public bool CategoriesDynamicExcludeKeywords { get; set; }

        /// <summary>
        /// Минимальное количество динамических категорий
        /// </summary>
        [Category("Categories"), DefaultValue(4)]
        public int CategoriesDynamicMin { get; set; }

        /// <summary>
        /// Максимальное количество динамических категорий
        /// </summary>
        [Category("Categories"), DefaultValue(7)]
        public int CategoriesDynamicMax { get; set; }

        /// <summary>
        /// Тип распределения кейвордов по категориям
        /// </summary>
        [Category("Categories"), DefaultValue(0)]
        public int CategoriesDistribute { get; set; }

        /// <summary>
        /// ID кейвордов, по которым распределяются страницы по категориям,
        /// например список городов
        /// </summary>
        [Category("Categories"), DefaultValue(0)]
        public int CategoriesDistributeContainsID { get; set; }
        #endregion

        #region Pages
        /// <summary>
        /// Тип дорвея
        /// </summary>
        [Category("Pages"), DefaultValue(0)]
        public int PagesDoorwayType { get; set; }

        /// <summary>
        /// Расширение файлов
        /// </summary>
        [Category("Pages"), DefaultValue(".html")]
        public string PagesStaticExtension { get; set; }

        /// <summary>
        /// Разделитель
        /// </summary>
        [Category("Pages"), DefaultValue("-")]
        public string PagesStaticSeparator { get; set; }

        /// <summary>
        /// Тип имен
        /// </summary>
        [Category("Pages"), DefaultValue(0)]
        public int PagesStaticNamesTypes { get; set; }

        /// <summary>
        /// Имена страниц
        /// </summary>
        [Category("Pages"), DefaultValue("[Name]")]
        public string PagesStaticPageNames { get; set; }

        /// <summary>
        /// Имена категорий
        /// </summary>
        [Category("Pages"), DefaultValue("[Name]")]
        public string PagesStaticCategoriesNames { get; set; }

        /// <summary>
        /// Продолжение индексов
        /// </summary>
        [Category("Pages"), DefaultValue(false)]
        public bool PagesStaticIndexContinues { get; set; }

        /// <summary>
        /// Имена страниц продолжений индексов
        /// </summary>
        [Category("Pages"), DefaultValue("[Name]-[N]")]
        public string PagesStaticIndexContinuesNames { get; set; }

        /// <summary>
        /// Продолжение категорий
        /// </summary>
        [Category("Pages"), DefaultValue(false)]
        public bool PagesStaticCategoriesContinues { get; set; }

        /// <summary>
        /// Имена страниц продолжений категорий
        /// </summary>
        [Category("Pages"), DefaultValue("[CName]-[N]")]
        public string PagesStaticCategoriesContinuesNames { get; set; }

        /// <summary>
        /// Количество кейвордов на странице продолжений
        /// </summary>
        [Category("Pages"), DefaultValue(10)]
        public int PagesStaticKeysPerPageOnContinues { get; set; }

        /// <summary>
        /// Имена страниц
        /// </summary>
        [Category("Pages"), DefaultValue("page")]
        public string PagesDynamicPageNames { get; set; }

        /// <summary>
        /// Имена категорий
        /// </summary>
        [Category("Pages"), DefaultValue("category")]
        public string PagesDynamicCategoriesNames { get; set; }

        /// <summary>
        /// Имена статических страниц
        /// </summary>
        [Category("Pages"), DefaultValue("spage")]
        public string PagesDynamicStaticPageNames { get; set; }

        /// <summary>
        /// Продолжение индексов
        /// </summary>
        [Category("Pages"), DefaultValue(false)]
        public bool PagesDynamicIndexContinues { get; set; }

        /// <summary>
        /// Имена страниц продолжений индексов
        /// </summary>
        [Category("Pages"), DefaultValue("index")]
        public string PagesDynamicIndexContinuesNames { get; set; }

        /// <summary>
        /// Продолжение категорий
        /// </summary>
        [Category("Pages"), DefaultValue(false)]
        public bool PagesDynamicCategoriesContinues { get; set; }

        /// <summary>
        /// Имена страниц продолжений категорий, имя 1
        /// </summary>
        [Category("Pages"), DefaultValue("category")]
        public string PagesDynamicCategoriesContinuesNames1 { get; set; }

        /// <summary>
        /// Имена страниц продолжений категорий, имя 2
        /// </summary>
        [Category("Pages"), DefaultValue("page")]
        public string PagesDynamicCategoriesContinuesNames2 { get; set; }

        /// <summary>
        /// Количество кейвордов на странице продолжений
        /// </summary>
        [Category("Pages"), DefaultValue(10)]
        public int PagesDynamicKeysPerPageOnContinues { get; set; }
        #endregion

        #region StaticPages
        /// <summary>
        /// Использовать статические страницы
        /// </summary>
        [Category("StaticPages"), DefaultValue(false)]
        public bool StaticPages { get; set; }

        /// <summary>
        /// Список статических страниц
        /// </summary>
        [Category("StaticPages")]
        public string[] StaticPagesList { get; set; }

        /// <summary>
        /// Включать статические страницы в карту сайта
        /// </summary>
        [Category("StaticPages"), DefaultValue(false)]
        public bool StaticPagesIncludeIntoSiteMap { get; set; }
        #endregion

        #region RSS
        /// <summary>
        /// Создавать RSS
        /// </summary>
        [Category("RSS"), DefaultValue(false)]
        public bool RSS { get; set; }

        /// <summary>
        /// Количество записей в RSS
        /// </summary>
        [Category("RSS"), DefaultValue(10)]
        public int RSSCount { get; set; }

        [Category("RSS"), DefaultValue("feed.xml")]
        public string RSSFileName { get; set; }
        #endregion

        #region SiteMap
        /// <summary>
        /// Создание карты сайта
        /// </summary>
        [Category("SiteMap"), DefaultValue(false)]
        public bool SiteMap { get; set; }

        /// <summary>
        /// Тип карты сайта
        /// </summary>
        [Category("SiteMap"), DefaultValue(0)]
        public int SiteMapType { get; set; }

        /// <summary>
        /// Тип ХТМЛ карты сайта
        /// </summary>
        [Category("SiteMap"), DefaultValue(0)]
        public int SiteMapHTMLType { get; set; }

        /// <summary>
        /// Имя ХТМЛ карты сайта
        /// </summary>
        [Category("SiteMap"), DefaultValue("Map-[N]")]
        public string SiteMapHTMLName { get; set; }

        /// <summary>
        /// Минимальное количество ссылок на карте сайты
        /// </summary>
        [Category("SiteMap"), DefaultValue(50)]
        public int SiteMapHTMLLinksMin { get; set; }

        /// <summary>
        /// Максимальное количество ссылок на карте сайты
        /// </summary>
        [Category("SiteMap"), DefaultValue(100)]
        public int SiteMapHTMLLinksMax { get; set; }
        #endregion

        #region robots.txt
        /// <summary>
        /// Создание robots.txt
        /// </summary>
        [Category("Robots"), DefaultValue(false)]
        public bool Robots { get; set; }

        /// <summary>
        /// Тип robots.txt
        /// </summary>
        [Category("Robots"), DefaultValue(0)]
        public int RobotsType { get; set; }

        /// <summary>
        /// Содержимое robots.txt
        /// </summary>
        [Category("Robots")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string RobotsContent { get; set; }
        #endregion

        #region TextGenerating
        /// <summary>
        /// Тип генерирования текста
        /// </summary>
        [Category("Text"), DefaultValue(0)]
        public int TextGenration { get; set; }

        /// <summary>
        /// Минимальная длина сгенерированного текста
        /// </summary>
        [Category("Text"), DefaultValue(200)]
        public int TextGenrationTextLengthMin { get; set; }

        /// <summary>
        /// Максимальная длина сгенерированного текста
        /// </summary>
        [Category("Text"), DefaultValue(300)]
        public int TextGenrationTextLengthMax { get; set; }

        /// <summary>
        /// Более одного кейворда на странице
        /// </summary>
        [Category("Text"), DefaultValue(false)]
        public bool TextGenrationKeywordsMoreThanOneOnPage { get; set; }

        /// <summary>
        /// Минимальное количество кейвордов на странице
        /// </summary>
        [Category("Text"), DefaultValue(1)]
        public int TextGenrationKeywordsOnPageMin { get; set; }

        /// <summary>
        /// Максимальное количество кейвордов на странице
        /// </summary>
        [Category("Text"), DefaultValue(3)]
        public int TextGenrationKeywordsOnPageMax { get; set; }

        /// <summary>
        /// Минимальный % вхождения кейворда на странице
        /// </summary>
        [Category("Text"), DefaultValue(4)]
        public decimal TextGenrationKeywordsPercentageMin { get; set; }

        /// <summary>
        /// Максимальный % вхождения кейворда на странице
        /// </summary>
        [Category("Text"), DefaultValue(7)]
        public decimal TextGenrationKeywordsPercentageMax { get; set; }

        /// <summary>
        /// Тип вставки кейвордов в страницу
        /// </summary>
        [Category("Text"), DefaultValue(0)]
        public int TextGenrationInsertKeywordsType { get; set; }

        /// <summary>
        /// Погрешность (±) при шаге вставки кейвордов в текст
        /// </summary>
        [Category("Text"), DefaultValue(3)]
        public int TextGenrationInsertKeywordsСonfusion { get; set; }

        /// <summary>
        /// Использование вставки кейвордов в % для всех кейвордов
        /// </summary>
        [Category("Text"), DefaultValue(false)]
        public bool TextGenrationPersentageForEachKeyword { get; set; }

        /// <summary>
        /// Вставка в текст кейвордов с других страниц
        /// </summary>
        [Category("Text"), DefaultValue(false)]
        public bool TextGenrationInsertOtherKeywords { get; set; }

        /// <summary>
        /// % вставляемых "чужих" кейвордов в генерируемый текст
        /// </summary>
        [Category("Text"), DefaultValue(3)]
        public decimal TextGenrationInsertOtherKeywordsPercentage { get; set; }

        /// <summary>
        /// Выделение тегами кейвордов
        /// </summary>
        [Category("Text"), DefaultValue(false)]
        public bool TextGenrationSelectKeywords { get; set; }

        /// <summary>
        /// Теги для выделения кейвордов
        /// </summary>
        [Category("Text")]
        public string[] TextGenrationSelectKeywordsTags { get; set; }

        /// <summary>
        /// Выделение тегами фраз
        /// </summary>
        [Category("Text"), DefaultValue(false)]
        public bool TextGenrationSelectPhrases { get; set; }

        /// <summary>
        /// Теги для выделения фраз
        /// </summary>
        [Category("Text")]
        public string[] TextGenrationSelectPhrasesTags { get; set; }

        /// <summary>
        /// Количество кейвордов/фраз, которые надо выделять, в %
        /// </summary>
        [Category("Text"), DefaultValue(10)]
        public decimal TextGenrationSelectPercentage { get; set; }

        /// <summary>
        /// Вставка знаков препинания
        /// </summary>
        [Category("Text"), DefaultValue(false)]
        public bool TextGenrationPunctuationMarks { get; set; }

        /// <summary>
        /// Минимальное количество знаков препинания в тексте
        /// </summary>
        [Category("Text"), DefaultValue(5)]
        public int TextGenrationPunctuationMarksInsertMin { get; set; }

        /// <summary>
        /// Максимальное количество знаков препинания в тексте
        /// </summary>
        [Category("Text"), DefaultValue(7)]
        public int TextGenrationPunctuationMarksInsertMax { get; set; }

        /// <summary>
        /// Список знаков препинания которые необходимо вставлять в текст
        /// </summary>
        [Category("Text")]
        public string[] TextGenrationPunctuationMarksList { get; set; }

        /// <summary>
        /// Тип длины предложений
        /// </summary>
        [Category("Text"), DefaultValue(0)]
        public int TextGenrationSentencesLengthType { get; set; }

        /// <summary>
        /// Количество предложений
        /// </summary>
        [Category("Text"), DefaultValue(10)]
        public int TextGenrationSentencesCount { get; set; }

        /// <summary>
        /// Длина предложений
        /// </summary>
        [Category("Text"), DefaultValue(15)]
        public int TextGenrationSentencesLength { get; set; }

        /// <summary>
        /// Погрешность (±) в длине предложений
        /// </summary>
        [Category("Text"), DefaultValue(3)]
        public int TextGenrationSentencesLengthСonfusion { get; set; }
        
        /// <summary>
        /// Делать большие буквы после знаков препинания
        /// </summary>
        [Category("Text"), DefaultValue(false)]
        public bool TextGenrationSentencesMakeBigLetters { get; set; }

        /// <summary>
        /// Использование параграфов,
        /// разбитие текста на параграфы
        /// </summary>
        [Category("Text"), DefaultValue(false)]
        public bool TextGenrationParagraphs { get; set; }

        /// <summary>
        /// Минимальное количество параграфов
        /// </summary>
        [Category("Text"), DefaultValue(1)]
        public int TextGenrationParagraphsMin { get; set; }

        /// <summary>
        /// Максимальное количество параграфов
        /// </summary>
        [Category("Text"), DefaultValue(3)]
        public int TextGenrationParagraphsMax { get; set; }

        /// <summary>
        /// Тип анализа слов при цепях и графах
        /// </summary>
        [Category("Text"), DefaultValue(0)]
        public int TextGenrationCGTextAnalyseType { get; set; }

        /// <summary>
        /// Длина слов, которые необходимо укоротить для анализа не полных слов
        /// в цепях и графах
        /// </summary>
        [Category("Text"), DefaultValue(6)]
        public int TextGenrationCGTextAnalyseCutWordsLength { get; set; }

        /// <summary>
        /// Подтип генерирования текста
        /// </summary>
        [Category("Text"), DefaultValue(0)]
        public int TextGenrationCGConstructionType { get; set; }

        /// <summary>
        /// Минимальное количество всталяемых без изменения слов из текста
        /// при "длинном" типе создания текста
        /// </summary>
        [Category("Text"), DefaultValue(2)]
        public int TextGenrationCGConstructionInsertWordsMin { get; set; }

        /// <summary>
        /// Максимальное количество всталяемых без изменения слов из текста
        /// при "длинном" типе создания текста
        /// </summary>
        [Category("Text"), DefaultValue(5)]
        public int TextGenrationCGConstructionInsertWordsMax { get; set; }

        /// <summary>
        /// Учет знаков препинания при анализе текста:
        /// 0 - учет, 1- не учитывать
        /// </summary>
        [Category("Text"), DefaultValue(0)]
        public int TextGenrationCGPunctuationMarksConsideration { get; set; }

        /// <summary>
        /// Учет вероятности при построении текста
        /// </summary>
        [Category("Text"), DefaultValue(false)]
        public bool TextGenrationCGConsiderProbability { get; set; }

        /// <summary>
        /// Тип выделения и вставки текста
        /// </summary>
        [Category("Text"), DefaultValue(0)]
        public int TextGenrationMRAIType { get; set; }

        /// <summary>
        /// Учет знаков препинания при анализе текста:
        /// 0 - учет, 1- не учитывать
        /// </summary>
        [Category("Text"), DefaultValue(0)]
        public int TextGenrationMRAIPunctuationMarksConsideration { get; set; }

        /// <summary>
        /// Радиус в котором перемешиваются куски текста
        /// </summary>
        [Category("Text"), DefaultValue(3)]
        public int TextGenrationMRAIRadius { get; set; }

        /// <summary>
        /// Подтип генерирования текста
        /// </summary>
        [Category("Text"), DefaultValue(0)]
        public int TextGenrationMRAIConstructionType { get; set; }

        /// <summary>
        /// Минимальное количество всталяемых без изменения слов из текста
        /// при "длинном" типе создания текста
        /// </summary>
        [Category("Text"), DefaultValue(2)]
        public int TextGenrationMRAIConstructionInsertWordsMin { get; set; }

        /// <summary>
        /// Максимальное количество всталяемых без изменения слов из текста
        /// при "длинном" типе создания текста
        /// </summary>
        [Category("Text"), DefaultValue(5)]
        public int TextGenrationMRAIConstructionInsertWordsMax { get; set; }
        #endregion

        #region Macroses
        /// <summary>
        /// Тип ссылки на главную
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesMainLinkType { get; set; }

        /// <summary>
        /// Пользовательская ссылка на главную
        /// </summary>
        [Category("Tokens")]
        public string MacrosesMainLink { get; set; }

        /// <summary>
        /// [SITE]
        /// </summary>
        [Category("Tokens")]
        public string[] MacrosesSite { get; set; }

        /// <summary>
        /// [TITLE]
        /// </summary>
        [Category("Tokens")]
        public string[] MacrosesTitle { get; set; }

        /// <summary>
        /// Минимальное количество блоков [BLOCK] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesBlockMainMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [BLOCK] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesBlockMainMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [BLOCK] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesBlockPageMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [BLOCK] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesBlockPageMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [MENUBLOCK] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesMenuBlockMainMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [MENUBLOCK] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesMenuBlockMainMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [MENUBLOCK] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesMenuBlockPageMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [MENUBLOCK] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesMenuBlockPageMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [USERBLOCK1] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock1MainMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [USERBLOCK1] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock1MainMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [USERBLOCK1] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock1PageMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [USERBLOCK1] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock1PageMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [USERBLOCK2] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock2MainMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [USERBLOCK2] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock2MainMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [USERBLOCK2] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock2PageMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [USERBLOCK2] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock2PageMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [USERBLOCK3] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock3MainMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [USERBLOCK3] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock3MainMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [USERBLOCK3] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock3PageMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [USERBLOCK3] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock3PageMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [USERBLOCK4] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock4MainMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [USERBLOCK4] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock4MainMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [USERBLOCK4] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock4PageMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [USERBLOCK4] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock4PageMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [USERBLOCK5] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock5MainMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [USERBLOCK5] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock5MainMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [USERBLOCK5] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock5PageMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [USERBLOCK5] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock5PageMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [USERBLOCK6] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock6MainMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [USERBLOCK6] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock6MainMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [USERBLOCK6] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock6PageMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [USERBLOCK6] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesUserBlock6PageMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [CATEGORYMENUBLOCK] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesCategoryMenuBlockMainMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [CATEGORYMENUBLOCK] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesCategoryMenuBlockMainMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [CATEGORYMENUBLOCK] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesCategoryMenuBlockPageMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [CATEGORYMENUBLOCK] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesCategoryMenuBlockPageMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [NETBLOCK] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesNetBlockMainMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [NETBLOCK] на главной/категориях
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesNetBlockMainMax { get; set; }

        /// <summary>
        /// Минимальное количество блоков [NETBLOCK] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesNetBlockPageMin { get; set; }

        /// <summary>
        /// Максимальное количество блоков [NETBLOCK] на обычных страницах
        /// </summary>
        [Category("Tokens"), DefaultValue(0)]
        public int MacrosesNetBlockPageMax { get; set; }
        #endregion

        #region Entrance
        /// <summary>
        /// Тип редиректа
        /// </summary>
        [Category("Entrance"), DefaultValue(0)]
        public int EntranceType { get; set; }

        /// <summary>
        /// Тип вставки кода в страницу
        /// </summary>
        [Category("Entrance"), DefaultValue(0)]
        public int EntranceInsertType { get; set; }

        /// <summary>
        /// Тип адреса сайта-акцептора
        /// </summary>
        [Category("Entrance"), DefaultValue(0)]
        public int EntranceAcceptorAdressType { get; set; }

        /// <summary>
        /// Адрес сайт-акцептора
        /// </summary>
        [Category("Entrance")]
        public string EntranceAcceptorAdress { get; set; }
        
        /// <summary>
        /// Шифровать JS файлы
        /// </summary>
        [Category("Entrance"), DefaultValue(false)]
        public bool EntranceJSEncrypt { get; set; }

        /// <summary>
        /// ХТМЛ код редирект/фрейма/кнопки вход
        /// </summary>
        [Category("Entrance")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string EntranceCode { get; set; }

        /// <summary>
        /// ХТМЛ код счетчика
        /// </summary>
        [Category("Entrance")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string EntranceCounter { get; set; }
        #endregion

        #region Links
        /// <summary>
        /// Use relative links, even if domain name is specified
        /// </summary>
        [Category("Linking"), DefaultValue(false)]
        public bool LinksRelativeURLs { get; set; }
        /// <summary>
        /// Создавать внутренние ссылки
        /// </summary>
        [Category("Linking"), DefaultValue(false)]
        public bool LinksInternal { get; set; }

        /// <summary>
        /// Тип внутренних ссылок
        /// </summary>
        [Category("Linking"), DefaultValue(0)]
        public int LinksInternalType { get; set; }

        /// <summary>
        /// Минимальная длина ссылки, слов
        /// </summary>
        [Category("Linking"), DefaultValue(1)]
        public int LinksInternalMinLength { get; set; }

        /// <summary>
        /// Максимальная длина ссылки, слов
        /// </summary>
        [Category("Linking"), DefaultValue(3)]
        public int LinksInternalMaxLength { get; set; }

        /// <summary>
        /// Использовать внешние ссылки
        /// </summary>
        [Category("Linking"), DefaultValue(false)]
        public bool LinksExternal { get; set; }

        /// <summary>
        /// Список внешних ссылок
        /// </summary>
        [Category("Linking")]
        public string[] LinksExternalList { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [TEXT] and others should contain external links
        /// </summary>
        [Category("Linking"), DefaultValue(false)]
        public bool LinksExternalInText { get; set; }

        /// <summary>
        /// Gets or sets number of minimum occurrences of external links in text
        /// </summary>
        [Category("Linking"), DefaultValue(0)]
        public int LinksExternalInTextIndexPageMinimum { get; set; }

        /// <summary>
        /// Gets or sets number of maximum occurrences of external links in text
        /// </summary>
        [Category("Linking"), DefaultValue(1)]
        public int LinksExternalInTextIndexPageMaximum { get; set; }

        /// <summary>
        /// Gets or sets number of minimum occurrences of external links in text
        /// </summary>
        [Category("Linking"), DefaultValue(0)]
        public int LinksExternalInTextRegularPageMinimum { get; set; }

        /// <summary>
        /// Gets or sets number of maximum occurrences of external links in text
        /// </summary>
        [Category("Linking"), DefaultValue(1)]
        public int LinksExternalInTextRegularPageMaximum { get; set; }
        #endregion

        #region Spam
        /// <summary>
        /// Создавать спам-ссылки
        /// </summary>
        [Category("Spam"), DefaultValue(false)]
        public bool Spam { get; set; }

        /// <summary>
        /// Список типов ссылок для спам-ссылок
        /// </summary>
        [Category("Spam")]
        public string[] SpamUrlTypeList { get; set; }

        /// <summary>
        /// Сохранять спам ссылки в файлы
        /// </summary>
        [Category("Spam"), DefaultValue(false)]
        public bool SpamSaveToFile { get; set; }

        /// <summary>
        /// Тип сохранения спам-сылок в файлы: 0 - Один файл/дорвей
        /// 1 - Много файлов/Дорвей, 2 - Один файл/Дорвеи, 3- Много файлов/Дорвеи
        /// </summary>
        [Category("Spam"), DefaultValue(0)]
        public int SpamSaveToFileType { get; set; }

        /// <summary>
        /// Путь папки, в которую будут сохранятся файлы
        /// </summary>
        [Category("Spam")]
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string SpamSaveToFilePath { get; set; }

        /// <summary>
        /// Кодировка файлов с спам-ссылками
        /// </summary>
        [Category("Spam"), DefaultValue(0)]
        public int SpamSaveEncoding { get; set; }
        #endregion

        #region Tags
        [Category("Tags"), Browsable(false)]
        public TagSettings[] TagSettings { get; set; }
        #endregion

        #region FTP
        /// <summary>
        /// Загрузка дорвея по фтп
        /// </summary>
        [Category("FTP"), DefaultValue(false)]
        public bool FTPUpload { get; set; }

        /// <summary>
        /// Тип выгрузки
        /// </summary>
        [Category("FTP"), DefaultValue(0)]
        public int FTPUploadType { get; set; }

        /// <summary>
        /// Путь к папке, куда сохранять проекты
        /// </summary>
        [Category("FTP")]
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string FTPUploadSaveTo { get; set; }

        /// <summary>
        /// Удаление файлов после загрузки
        /// </summary>
        [Category("FTP"), DefaultValue(false)]
        public bool FTPDelete { get; set; }

        /// <summary>
        /// Тип заархивированного архива при загрузка по фтп
        /// </summary>
        [Category("FTP"), DefaultValue(0)]
        public int FTPUploadArchive { get; set; }

        /// <summary>
        /// Имя сжатого дорвея (архива)
        /// </summary>
        [Category("FTP"), DefaultValue("doorway.zip")]
        public string FTPUploadArchiveName { get; set; }

        /// <summary>
        /// Загружать дорвей в бэкграунде
        /// </summary>
        [Category("FTP"), DefaultValue(false)]
        public bool FTPUploadInBackground { get; set; }

        /// <summary>
        /// Количество потоков на выгрузку файлов
        /// </summary>
        [Category("FTP"), DefaultValue(1)]
        public int FTPThreads { get; set; }

        [Category("FTP"), Browsable(false)]
        public FTPSettings[] FTPSettings { get; set; }
        #endregion

        #region XRumer
        /// <summary>
        /// Create XRumer files
        /// </summary>
        [Category("XRumer"), DefaultValue(false)]
        public bool XRumerUse { get; set; }

        /// <summary>
        /// Directory for XRumer files
        /// </summary>
        [Category("XRumer"), DefaultValue("")]
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string XRumerDirectory { get; set; }

        /// <summary>
        /// XRumer template
        /// </summary>
        [Category("XRumer"), DefaultValue("")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string XRumerText { get; set; }

        /// <summary>
        /// XRumer template
        /// </summary>
        [Category("XRumer"), DefaultValue("")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string XRumerTemplate { get; set; }
        #endregion

        public override string ToString()
        {
            StringBuilder data = new StringBuilder(10000);
            StringBuilder temp = new StringBuilder();

            // General
            data.Append("GCreateDoorways=" + GeneralCreateDoorways.ToString() + "\r\n");
            data.Append("GThreads=" + GeneralThreads.ToString() + "\r\n");
            data.Append("GImageType=" + GeneralImageType.ToString() + "\r\n");
            data.Append("GImageSizeMinWidth=" + GeneralImageSizeMinWidth.ToString() + "\r\n");
            data.Append("GImageSizeMinHeight=" + GeneralImageSizeMinHeight.ToString() + "\r\n");
            data.Append("GImageSizeMaxWidth=" + GeneralImageSizeMaxWidth.ToString() + "\r\n");
            data.Append("GImageSizeMaxHeight=" + GeneralImageSizeMaxHeight.ToString() + "\r\n");
            data.Append("GGenerateImagesCount=" + GeneralGenerateImagesCount.ToString() + "\r\n");
            data.Append("GGenerateImagesNamingType=" + GeneralImageNamingType.ToString() + "\r\n");
            data.Append("GGenerateImagesNamingFile=" + GeneralImageNamingFile + "\r\n");
            data.Append("GArchive=" + GeneralArchive.ToString() + "\r\n");
            data.Append("GArchiveName=" + GeneralArchiveName + "\r\n");
            data.Append("GSaveTo=" + GeneralSaveTo + "\r\n");
            data.Append("GCreateSubFolders=" + GeneralCreateSubFolders.ToString() + "\r\n");
            data.Append("GSubFoldersType=" + GeneralSubFoldersType.ToString() + "\r\n");
            temp = new StringBuilder(1000);
            for (int i = 0; i < GeneralDoorwayUrls.Length; i++)
            {
                temp.Append(GeneralDoorwayUrls[i] + "¥");
            }
            data.Append("GDoorwayUrls=" + temp.ToString() + "\r\n");
            data.Append("GFileDateStart=" + this.GeneralFileDateStart.ToString() + "\r\n");
            data.Append("GFileDateEnd=" + this.GeneralFileDateEnd.ToString() + "\r\n");

            // File Macroses
            temp = new StringBuilder(1000);
            for (int i = 0; i < FileMacroses.Length; i++)
            {
                if (i > 0)
                {
                    temp.Append("|");
                }
                temp.Append(FileMacroses[i].Macross);
                temp.Append("¥");
                temp.Append(FileMacroses[i].File);
                temp.Append("¥");
                temp.Append(FileMacroses[i].EncodingType.ToString());
                temp.Append("¥");
                temp.Append(FileMacroses[i].Type.ToString());
            }
            data.Append("FileMacroses=" + temp.ToString() + "\r\n");
            //Keywords
            data.Append("KID=" + KeywordsID.ToString() + "\r\n");
            data.Append("KSelectType=" + KeywordsSelectType.ToString() + "\r\n");
            data.Append("KSelectMin=" + KeywordsSelectMin.ToString() + "\r\n");
            data.Append("KSelectMax=" + KeywordsSelectMax.ToString() + "\r\n");
            data.Append("KReorder=" + KeywordsReorder.ToString() + "\r\n");
            data.Append("KWordsReorder=" + KeywordsWordsReorder.ToString() + "\r\n");
            data.Append("KWordsReorder%=" + KeywordsWordsReorderPercentage.ToString() + "\r\n");
            data.Append("KSynonyms=" + KeywordsSynonyms.ToString() + "\r\n");
            data.Append("KSynonyms%=" + KeywordsSynonymsPercentage.ToString() + "\r\n");
            data.Append("KSynonymsID=" + KeywordsSynonymsID.ToString() + "\r\n");
            data.Append("KMerge=" + KeywordsMerge.ToString() + "\r\n");
            data.Append("KMergeID=" + KeywordsMergeID.ToString() + "\r\n");
            data.Append("KMergeType=" + KeywordsMergeType.ToString() + "\r\n");
            data.Append("KMergeMin=" + KeywordsMergeMin.ToString() + "\r\n");
            data.Append("KMergeMax=" + KeywordsMergeMax.ToString() + "\r\n");
            //Categories
            data.Append("Categories=" + Categories.ToString() + "\r\n");
            data.Append("CType=" + CategoriesType.ToString() + "\r\n");
            temp = new StringBuilder(1000);
            for (int i = 0; i < CategoriesStaticList.Length; i++)
            {
                temp.Append(CategoriesStaticList[i] + "¥");
            }
            data.Append("CStaticList=" + temp.ToString() + "\r\n");
            data.Append("CDynamicExcludeKeys=" + CategoriesDynamicExcludeKeywords.ToString() + "\r\n");
            data.Append("CDynamicMin=" + CategoriesDynamicMin.ToString() + "\r\n");
            data.Append("CDynamicMax=" + CategoriesDynamicMax.ToString() + "\r\n");
            data.Append("CDistribute=" + CategoriesDistribute.ToString() + "\r\n");
            data.Append("CDistributeCID=" + CategoriesDistributeContainsID.ToString() + "\r\n");
            //Pages
            data.Append("PDoorwayType=" + PagesDoorwayType.ToString() + "\r\n");
            data.Append("PStaticExtension=" + PagesStaticExtension.ToString() + "\r\n");
            data.Append("PStaticSeparator=" + PagesStaticSeparator + "\r\n");
            data.Append("PStaticNamesTypes=" + PagesStaticNamesTypes.ToString() + "\r\n");
            data.Append("PStaticPageNames=" + PagesStaticPageNames + "\r\n");
            data.Append("PStaticCategoriesNames=" + PagesStaticCategoriesNames + "\r\n");
            data.Append("PStaticIndexContinues=" + PagesStaticIndexContinues.ToString() + "\r\n");
            data.Append("PStaticIndexContinuesNames=" + PagesStaticIndexContinuesNames + "\r\n");
            data.Append("PStaticCategoriesContinues=" + PagesStaticCategoriesContinues.ToString() + "\r\n");
            data.Append("PStaticCategoriesContinuesNames=" + PagesStaticCategoriesContinuesNames + "\r\n");
            data.Append("PStaticKeysPerPageOnContinues=" + PagesStaticKeysPerPageOnContinues.ToString() + "\r\n");
            data.Append("PDynamicPageNames=" + PagesDynamicPageNames + "\r\n");
            data.Append("PDynamicCategoriesNames=" + PagesDynamicCategoriesNames + "\r\n");
            data.Append("PDynamicStaticPageNames=" + PagesDynamicStaticPageNames + "\r\n");
            data.Append("PDynamicIndexContinues=" + PagesDynamicIndexContinues.ToString() + "\r\n");
            data.Append("PDynamicIndexContinuesNames=" + PagesDynamicIndexContinuesNames + "\r\n");
            data.Append("PDynamicCategoriesContinues=" + PagesDynamicCategoriesContinues.ToString() + "\r\n");
            data.Append("PDynamicCategoriesContinuesNames1=" + PagesDynamicCategoriesContinuesNames1 + "\r\n");
            data.Append("PDynamicCategoriesContinuesNames2=" + PagesDynamicCategoriesContinuesNames2 + "\r\n");
            data.Append("PDynamicKeysPerPageOnContinues=" + PagesDynamicKeysPerPageOnContinues.ToString() + "\r\n");
            //Static Pages
            data.Append("StaticPages=" + StaticPages.ToString() + "\r\n");
            temp = new StringBuilder(1000);
            for (int i = 0; i < StaticPagesList.Length; i++)
            {
                temp.Append(StaticPagesList[i] + "¥");
            }
            data.Append("StaticPagesList=" + temp.ToString() + "\r\n");
            data.Append("StaticPagesIncludeIntoSiteMap=" + StaticPagesIncludeIntoSiteMap.ToString() + "\r\n");
            //RSS
            data.Append("RSS=" + RSS.ToString() + "\r\n");
            data.Append("RSSCount=" + RSSCount.ToString() + "\r\n");
            data.Append("RSSFileName=" + RSSFileName.ToString() + "\r\n");
            //SiteMap
            data.Append("SiteMap=" + SiteMap.ToString() + "\r\n");
            data.Append("SiteMapType=" + SiteMapType.ToString() + "\r\n");
            data.Append("SiteMapHTMLType=" + SiteMapHTMLType.ToString() + "\r\n");
            data.Append("SiteMapHTMLName=" + SiteMapHTMLName + "\r\n");
            data.Append("SiteMapHTMLLinksMin=" + SiteMapHTMLLinksMin.ToString() + "\r\n");
            data.Append("SiteMapHTMLLinksMax=" + SiteMapHTMLLinksMax.ToString() + "\r\n");
            //Robots.txt
            data.Append("Robots=" + Robots.ToString() + "\r\n");
            data.Append("RobotsType=" + RobotsType.ToString() + "\r\n");
            data.Append("RobotsContent=" + RobotsContent.Replace("\r\n", "¥") + "\r\n");
            //Text Generating
            data.Append("TG=" + TextGenration.ToString() + "\r\n");
            data.Append("TGTextLengthMin=" + TextGenrationTextLengthMin.ToString() + "\r\n");
            data.Append("TGTextLengthMax=" + TextGenrationTextLengthMax.ToString() + "\r\n");
            data.Append("TGKeywordsMoreThanOneOnPage=" + TextGenrationKeywordsMoreThanOneOnPage.ToString() + "\r\n");
            data.Append("TGKeywordsOnPageMin=" + TextGenrationKeywordsOnPageMin.ToString() + "\r\n");
            data.Append("TGKeywordsOnPageMax=" + TextGenrationKeywordsOnPageMax.ToString() + "\r\n");
            data.Append("TGKeywords%Min=" + TextGenrationKeywordsPercentageMin.ToString() + "\r\n");
            data.Append("TGKeywords%Max=" + TextGenrationKeywordsPercentageMax.ToString() + "\r\n");
            data.Append("TGInsertKeywordsType=" + TextGenrationInsertKeywordsType.ToString() + "\r\n");
            data.Append("TGInsertKeywordsСonfusion=" + TextGenrationInsertKeywordsСonfusion.ToString() + "\r\n");
            data.Append("TG%ForEachKeyword=" + TextGenrationPersentageForEachKeyword.ToString() + "\r\n");
            data.Append("TGInsertOtherKeywords=" + TextGenrationInsertOtherKeywords.ToString() + "\r\n");
            data.Append("TGInsertOtherKeywords%=" + TextGenrationInsertOtherKeywordsPercentage.ToString() + "\r\n");
            data.Append("TGSelectKeywords=" + TextGenrationSelectKeywords.ToString() + "\r\n");
            temp = new StringBuilder(100);
            for (int i = 0; i < TextGenrationSelectKeywordsTags.Length; i++)
            {
                temp.Append(TextGenrationSelectKeywordsTags[i] + "¥");
            }
            data.Append("TGSelectKeywordsTags=" + temp.ToString() + "\r\n");
            data.Append("TGSelectPhrases=" + TextGenrationSelectPhrases.ToString() + "\r\n");
            temp = new StringBuilder(100);
            for (int i = 0; i < TextGenrationSelectPhrasesTags.Length; i++)
            {
                temp.Append(TextGenrationSelectPhrasesTags[i] + "¥");
            }
            data.Append("TGSelectPhrasesTags=" + temp.ToString() + "\r\n");
            data.Append("TGSelect%=" + TextGenrationSelectPercentage.ToString() + "\r\n");
            data.Append("TGPunctuationMarks=" + TextGenrationPunctuationMarks.ToString() + "\r\n");
            data.Append("TGPunctuationMarksInsertMin=" + TextGenrationPunctuationMarksInsertMin.ToString() + "\r\n");
            data.Append("TGPunctuationMarksInsertMax=" + TextGenrationPunctuationMarksInsertMax.ToString() + "\r\n");
            temp = new StringBuilder(100);
            for (int i = 0; i < TextGenrationPunctuationMarksList.Length; i++)
            {
                temp.Append(TextGenrationPunctuationMarksList[i] + "¥");
            }
            data.Append("TGPunctuationMarksList=" + temp.ToString() + "\r\n");
            data.Append("TGSentencesLengthType=" + TextGenrationSentencesLengthType.ToString() + "\r\n");
            data.Append("TGSentencesCount=" + TextGenrationSentencesCount.ToString() + "\r\n");
            data.Append("TGSentencesLength=" + TextGenrationSentencesLength.ToString() + "\r\n");
            data.Append("TGSentencesLengthСonfusion=" + TextGenrationSentencesLengthСonfusion.ToString() + "\r\n");
            data.Append("TGSentencesMakeBigLetters=" + TextGenrationSentencesMakeBigLetters.ToString() + "\r\n");

            data.Append("TGParagraphs=" + TextGenrationParagraphs.ToString() + "\r\n");
            data.Append("TGParagraphsMin=" + TextGenrationParagraphsMin.ToString() + "\r\n");
            data.Append("TGParagraphsMax=" + TextGenrationParagraphsMax.ToString() + "\r\n");

            data.Append("TGCGTextAnalyseType=" + TextGenrationCGTextAnalyseType.ToString() + "\r\n");
            data.Append("TGCGTextAnalyseCutWordsLength=" + TextGenrationCGTextAnalyseCutWordsLength.ToString() + "\r\n");
            data.Append("TGCGConstructionType=" + TextGenrationCGConstructionType.ToString() + "\r\n");
            data.Append("TGCGConstructionInsertWordsMin=" + TextGenrationCGConstructionInsertWordsMin.ToString() + "\r\n");
            data.Append("TGCGConstructionInsertWordsMax=" + TextGenrationCGConstructionInsertWordsMax.ToString() + "\r\n");
            data.Append("TGCGPunctuationMarksConsideration=" + TextGenrationCGPunctuationMarksConsideration.ToString() + "\r\n");
            data.Append("TGCGConsiderProbability=" + TextGenrationCGConsiderProbability.ToString() + "\r\n");

            data.Append("TGMRAIType=" + TextGenrationMRAIType.ToString() + "\r\n");
            data.Append("TGMRAIPunctuationMarksConsideration=" + TextGenrationMRAIPunctuationMarksConsideration.ToString() + "\r\n");
            data.Append("TGMRAIRadius=" + TextGenrationMRAIRadius.ToString() + "\r\n");
            data.Append("TGMRAIConstructionType=" + TextGenrationMRAIConstructionType.ToString() + "\r\n");
            data.Append("TGMRAIConstructionInsertWordsMin=" + TextGenrationMRAIConstructionInsertWordsMin.ToString() + "\r\n");
            data.Append("TGMRAIConstructionInsertWordsMax=" + TextGenrationMRAIConstructionInsertWordsMax.ToString() + "\r\n");
            //Macroses
            data.Append("MMainlinkType=" + MacrosesMainLinkType.ToString() + "\r\n");
            data.Append("MMainlink=" + MacrosesMainLink + "\r\n");

            temp = new StringBuilder(400);
            for (int i = 0; i < MacrosesSite.Length; i++)
            {
                temp.Append(MacrosesSite[i] + "¥");
            }
            data.Append("MSite=" + temp.ToString() + "\r\n");

            temp = new StringBuilder(400);
            for (int i = 0; i < MacrosesTitle.Length; i++)
            {
                temp.Append(MacrosesTitle[i] + "¥");
            }
            data.Append("MTitle=" + temp.ToString() + "\r\n");

            data.Append("MBlockMainMin=" + MacrosesBlockMainMin.ToString() + "\r\n");
            data.Append("MBlockMainMax=" + MacrosesBlockMainMax.ToString() + "\r\n");
            data.Append("MBlockPageMin=" + MacrosesBlockPageMin.ToString() + "\r\n");
            data.Append("MBlockPageMax=" + MacrosesBlockPageMax.ToString() + "\r\n");

            data.Append("MMenuBlockMainMin=" + MacrosesMenuBlockMainMin.ToString() + "\r\n");
            data.Append("MMenuBlockMainMax=" + MacrosesMenuBlockMainMax.ToString() + "\r\n");
            data.Append("MMenuBlockPageMin=" + MacrosesMenuBlockPageMin.ToString() + "\r\n");
            data.Append("MMenuBlockPageMax=" + MacrosesMenuBlockPageMax.ToString() + "\r\n");

            data.Append("MUserBlock1MainMin=" + MacrosesUserBlock1MainMin.ToString() + "\r\n");
            data.Append("MUserBlock1MainMax=" + MacrosesUserBlock1MainMax.ToString() + "\r\n");
            data.Append("MUserBlock1PageMin=" + MacrosesUserBlock1PageMin.ToString() + "\r\n");
            data.Append("MUserBlock1PageMax=" + MacrosesUserBlock1PageMax.ToString() + "\r\n");

            data.Append("MUserBlock2MainMin=" + MacrosesUserBlock2MainMin.ToString() + "\r\n");
            data.Append("MUserBlock2MainMax=" + MacrosesUserBlock2MainMax.ToString() + "\r\n");
            data.Append("MUserBlock2PageMin=" + MacrosesUserBlock2PageMin.ToString() + "\r\n");
            data.Append("MUserBlock2PageMax=" + MacrosesUserBlock2PageMax.ToString() + "\r\n");

            data.Append("MUserBlock3MainMin=" + MacrosesUserBlock3MainMin.ToString() + "\r\n");
            data.Append("MUserBlock3MainMax=" + MacrosesUserBlock3MainMax.ToString() + "\r\n");
            data.Append("MUserBlock3PageMin=" + MacrosesUserBlock3PageMin.ToString() + "\r\n");
            data.Append("MUserBlock3PageMax=" + MacrosesUserBlock3PageMax.ToString() + "\r\n");

            data.Append("MUserBlock4MainMin=" + MacrosesUserBlock4MainMin.ToString() + "\r\n");
            data.Append("MUserBlock4MainMax=" + MacrosesUserBlock4MainMax.ToString() + "\r\n");
            data.Append("MUserBlock4PageMin=" + MacrosesUserBlock4PageMin.ToString() + "\r\n");
            data.Append("MUserBlock4PageMax=" + MacrosesUserBlock4PageMax.ToString() + "\r\n");

            data.Append("MUserBlock5MainMin=" + MacrosesUserBlock5MainMin.ToString() + "\r\n");
            data.Append("MUserBlock5MainMax=" + MacrosesUserBlock5MainMax.ToString() + "\r\n");
            data.Append("MUserBlock5PageMin=" + MacrosesUserBlock5PageMin.ToString() + "\r\n");
            data.Append("MUserBlock5PageMax=" + MacrosesUserBlock5PageMax.ToString() + "\r\n");

            data.Append("MUserBlock6MainMin=" + MacrosesUserBlock6MainMin.ToString() + "\r\n");
            data.Append("MUserBlock6MainMax=" + MacrosesUserBlock6MainMax.ToString() + "\r\n");
            data.Append("MUserBlock6PageMin=" + MacrosesUserBlock6PageMin.ToString() + "\r\n");
            data.Append("MUserBlock6PageMax=" + MacrosesUserBlock6PageMax.ToString() + "\r\n");

            data.Append("MCategoryMenuBlockMainMin=" + MacrosesCategoryMenuBlockMainMin.ToString() + "\r\n");
            data.Append("MCategoryMenuBlockMainMax=" + MacrosesCategoryMenuBlockMainMax.ToString() + "\r\n");
            data.Append("MCategoryMenuBlockPageMin=" + MacrosesCategoryMenuBlockPageMin.ToString() + "\r\n");
            data.Append("MCategoryMenuBlockPageMax=" + MacrosesCategoryMenuBlockPageMax.ToString() + "\r\n");

            data.Append("MNetBlockMainMin=" + MacrosesNetBlockMainMin.ToString() + "\r\n");
            data.Append("MNetBlockMainMax=" + MacrosesNetBlockMainMax.ToString() + "\r\n");
            data.Append("MNetBlockPageMin=" + MacrosesNetBlockPageMin.ToString() + "\r\n");
            data.Append("MNetBlockPageMax=" + MacrosesNetBlockPageMax.ToString() + "\r\n");
            //Entrance
            data.Append("EType=" + EntranceType.ToString() + "\r\n");
            data.Append("EInsertType=" + EntranceInsertType.ToString() + "\r\n");
            data.Append("EAcceptorAdressType=" + EntranceAcceptorAdressType.ToString() + "\r\n");
            data.Append("EJSEncrypt=" + EntranceJSEncrypt.ToString() + "\r\n");
            data.Append("ECode=" + EntranceCode.Replace("\r\n", "¥") + "\r\n");
            data.Append("ECounter=" + EntranceCounter.Replace("\r\n", "¥") + "\r\n");
            //Links
            data.Append("LRelativeURLs=" + LinksRelativeURLs.ToString() + "\r\n");
            data.Append("LInternal=" + LinksInternal.ToString() + "\r\n");
            data.Append("LInternalType=" + LinksInternalType.ToString() + "\r\n");
            data.Append("LInternalMinLength=" + LinksInternalMinLength.ToString() + "\r\n");
            data.Append("LInternalMaxLength=" + LinksInternalMaxLength.ToString() + "\r\n");
            data.Append("LExternal=" + LinksExternal.ToString() + "\r\n");
            temp = new StringBuilder(400);
            for (int i = 0; i < LinksExternalList.Length; i++)
            {
                temp.Append(LinksExternalList[i] + "¥");
            }
            data.Append("LExternalList=" + temp.ToString() + "\r\n");

            data.Append("LExternalInText=" + LinksExternalInText.ToString() + "\r\n");
            data.Append("LExternalInTextIndexMinimum=" + LinksExternalInTextIndexPageMinimum.ToString() + "\r\n");
            data.Append("LExternalInTextIndexMaximum=" + LinksExternalInTextIndexPageMaximum.ToString() + "\r\n");
            data.Append("LExternalInTextRegularMinimum=" + LinksExternalInTextRegularPageMinimum.ToString() + "\r\n");
            data.Append("LExternalInTextRegularMaximum=" + LinksExternalInTextRegularPageMaximum.ToString() + "\r\n");

            //SPAM
            data.Append("Spam=" + Spam.ToString() + "\r\n");
            temp = new StringBuilder(400);
            for (int i = 0; i < SpamUrlTypeList.Length; i++)
            {
                temp.Append(SpamUrlTypeList[i] + "¥");
            }
            data.Append("SpamUrls=" + temp.ToString() + "\r\n");
            data.Append("SpamSaveToFiles=" + SpamSaveToFile.ToString() + "\r\n");
            data.Append("SpamSaveToFilesType=" + SpamSaveToFileType.ToString() + "\r\n");
            data.Append("SpamSaveToFilesPath=" + SpamSaveToFilePath + "\r\n");
            data.Append("SpamSaveEncoding=" + SpamSaveEncoding.ToString() + "\r\n");

            // Tags
            temp = new StringBuilder(400);
            for (int i = 0; i < TagSettings.Length; i++)
            {
                if (i > 0)
                {
                    temp.Append("|");
                }
                temp.Append(TagSettings[i].File);
                temp.Append("¥");
                temp.Append(TagSettings[i].EncodingType.ToString());
            }

            data.Append("TagSettings=" + temp.ToString() + "\r\n");
            //FTP
            data.Append("FTPUpload=" + FTPUpload.ToString() + "\r\n");
            data.Append("FTPUploadType=" + FTPUploadType.ToString() + "\r\n");
            data.Append("FTPUploadSaveTo=" + FTPUploadSaveTo + "\r\n");

            data.Append("FTPDelete=" + FTPDelete.ToString() + "\r\n");
            data.Append("FTPUploadArchive=" + FTPUploadArchive.ToString() + "\r\n");
            data.Append("FTPUploadArchiveName=" + FTPUploadArchiveName + "\r\n");
            data.Append("FTPUploadThreads=" + FTPThreads.ToString() + "\r\n");
            data.Append("FTPUploadInBackground=" + FTPUploadInBackground.ToString() + "\r\n");
            temp = new StringBuilder(400);
            for (int i = 0; i < FTPSettings.Length; i++)
            {
                if (i > 0)
                {
                    temp.Append("|");
                }
                temp.Append(FTPSettings[i].Host);
                temp.Append("¥");
                temp.Append(FTPSettings[i].Login);
                temp.Append("¥");
                temp.Append(FTPSettings[i].Password);
                temp.Append("¥");
                temp.Append(FTPSettings[i].Folder);
            }
            data.Append("FTPSettings=" + temp.ToString() + "\r\n");

            // XRumer
            data.Append("XRumerUse=" + XRumerUse.ToString() + "\r\n");
            data.Append("XRumerDirectory=" + this.XRumerDirectory + "\r\n");
            data.Append("XRumerText=" + this.XRumerText.Replace("\r\n", "¥") + "\r\n");
            data.Append("XRumerTemplate=" + this.XRumerTemplate.Replace("\r\n", "¥") + "\r\n");

            return data.ToString();
        }

        public static PresetSettings Load(string Path)
        {
            PresetSettings settings = new PresetSettings();
            string[] data = File.ReadAllLines(Path, Encoding.UTF8);
            for (int i = 0; i < data.Length; i++)
            {
                try
                {
                    if (data[i].StartsWith("GCreateDoorways="))
                    {
                        settings.GeneralCreateDoorways = int.Parse(data[i].Substring(16));
                    }
                    else if (data[i].StartsWith("GThreads="))
                    {
                        settings.GeneralThreads = int.Parse(data[i].Substring(9));
                    }
                    else if (data[i].StartsWith("GImageType="))
                    {
                        settings.GeneralImageType = int.Parse(data[i].Substring(11));
                    }
                    else if (data[i].StartsWith("GImageSizeMinWidth="))
                    {
                        settings.GeneralImageSizeMinWidth = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("GImageSizeMinHeight="))
                    {
                        settings.GeneralImageSizeMinHeight = int.Parse(data[i].Substring(20));
                    }
                    else if (data[i].StartsWith("GImageSizeMaxWidth="))
                    {
                        settings.GeneralImageSizeMaxWidth = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("GImageSizeMaxHeight="))
                    {
                        settings.GeneralImageSizeMaxHeight = int.Parse(data[i].Substring(20));
                    }
                    else if (data[i].StartsWith("GGenerateImagesCount="))
                    {
                        settings.GeneralGenerateImagesCount = int.Parse(data[i].Substring(21));
                    }
                    else if (data[i].StartsWith("GGenerateImagesNamingType="))
                    {
                        settings.GeneralImageNamingType = int.Parse(data[i].Substring(26));
                    }
                    else if (data[i].StartsWith("GGenerateImagesNamingFile="))
                    {
                        settings.GeneralImageNamingFile = data[i].Substring(26);
                    }
                    else if (data[i].StartsWith("GArchive="))
                    {
                        settings.GeneralArchive = int.Parse(data[i].Substring(9));
                    }
                    else if (data[i].StartsWith("GArchiveName="))
                    {
                        settings.GeneralArchiveName = data[i].Substring(13);
                    }
                    else if (data[i].StartsWith("GSaveTo="))
                    {
                        settings.GeneralSaveTo = data[i].Substring(8);
                    }
                    else if (data[i].StartsWith("GCreateSubFolders="))
                    {
                        settings.GeneralCreateSubFolders = bool.Parse(data[i].Substring(18));
                    }
                    else if (data[i].StartsWith("GSubFoldersType="))
                    {
                        settings.GeneralSubFoldersType = int.Parse(data[i].Substring(16));
                    }
                    else if (data[i].StartsWith("GDoorwayUrls="))
                    {
                        settings.GeneralDoorwayUrls = data[i].Substring(13).Split(new char[] { '¥' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else if (data[i].StartsWith("GFileDateStart="))
                    {
                        settings.GeneralFileDateStart = DateTime.Parse(data[i].Substring(15));
                    }
                    else if (data[i].StartsWith("GFileDateEnd="))
                    {
                        settings.GeneralFileDateEnd = DateTime.Parse(data[i].Substring(13));
                    }
                    else if (data[i].StartsWith("FileMacroses="))
                    {
                        string[] fileData = data[i].Substring(13).Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        settings.FileMacroses = new FileMacross[fileData.Length];
                        for (int j = 0; j < settings.FileMacroses.Length; j++)
                        {
                            settings.FileMacroses[j] = new FileMacross();
                            try
                            {
                                string[] fmData = fileData[j].Split('¥');
                                if (fmData.Length == 4)
                                {
                                    settings.FileMacroses[j].Macross = fmData[0];
                                    settings.FileMacroses[j].File = fmData[1];
                                    settings.FileMacroses[j].EncodingType = int.Parse(fmData[2]);
                                    settings.FileMacroses[j].Type = int.Parse(fmData[3]);
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    else if (data[i].StartsWith("KID="))
                    {
                        settings.KeywordsID = int.Parse(data[i].Substring(4));
                    }
                    else if (data[i].StartsWith("KSelectType="))
                    {
                        settings.KeywordsSelectType = int.Parse(data[i].Substring(12));
                    }
                    else if (data[i].StartsWith("KSelectMin="))
                    {
                        settings.KeywordsSelectMin = int.Parse(data[i].Substring(11));
                    }
                    else if (data[i].StartsWith("KSelectMax="))
                    {
                        settings.KeywordsSelectMax = int.Parse(data[i].Substring(11));
                    }
                    else if (data[i].StartsWith("KReorder="))
                    {
                        settings.KeywordsReorder = bool.Parse(data[i].Substring(9));
                    }
                    else if (data[i].StartsWith("KWordsReorder="))
                    {
                        settings.KeywordsWordsReorder = bool.Parse(data[i].Substring(14));
                    }
                    else if (data[i].StartsWith("KWordsReorder%="))
                    {
                        settings.KeywordsWordsReorderPercentage = int.Parse(data[i].Substring(15));
                    }
                    else if (data[i].StartsWith("KSynonyms="))
                    {
                        settings.KeywordsSynonyms = bool.Parse(data[i].Substring(10));
                    }
                    else if (data[i].StartsWith("KSynonyms%="))
                    {
                        settings.KeywordsSynonymsPercentage = int.Parse(data[i].Substring(11));
                    }
                    else if (data[i].StartsWith("KSynonymsID="))
                    {
                        settings.KeywordsSynonymsID = int.Parse(data[i].Substring(12));
                    }
                    else if (data[i].StartsWith("KMerge="))
                    {
                        settings.KeywordsMerge = bool.Parse(data[i].Substring(7));
                    }
                    else if (data[i].StartsWith("KMergeID="))
                    {
                        settings.KeywordsMergeID = int.Parse(data[i].Substring(9));
                    }
                    else if (data[i].StartsWith("KMergeType="))
                    {
                        settings.KeywordsMergeType = int.Parse(data[i].Substring(11));
                    }
                    else if (data[i].StartsWith("KMergeMin="))
                    {
                        settings.KeywordsMergeMin = int.Parse(data[i].Substring(10));
                    }
                    else if (data[i].StartsWith("KMergeMax="))
                    {
                        settings.KeywordsMergeMax = int.Parse(data[i].Substring(10));
                    }
                    else if (data[i].StartsWith("Categories="))
                    {
                        settings.Categories = bool.Parse(data[i].Substring(11));
                    }
                    else if (data[i].StartsWith("CType="))
                    {
                        settings.CategoriesType = int.Parse(data[i].Substring(6));
                    }
                    else if (data[i].StartsWith("CStaticList="))
                    {
                        settings.CategoriesStaticList = data[i].Substring(12).Split(new char[] { '¥' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else if (data[i].StartsWith("CDynamicExcludeKeys="))
                    {
                        settings.CategoriesDynamicExcludeKeywords = bool.Parse(data[i].Substring(20));
                    }
                    else if (data[i].StartsWith("CDynamicMin="))
                    {
                        settings.CategoriesDynamicMin = int.Parse(data[i].Substring(12));
                    }
                    else if (data[i].StartsWith("CDynamicMax="))
                    {
                        settings.CategoriesDynamicMax = int.Parse(data[i].Substring(12));
                    }
                    else if (data[i].StartsWith("CDistribute="))
                    {
                        settings.CategoriesDistribute = int.Parse(data[i].Substring(12));
                    }
                    else if (data[i].StartsWith("CDistributeCID="))
                    {
                        settings.CategoriesDistributeContainsID = int.Parse(data[i].Substring(15));
                    }
                    else if (data[i].StartsWith("PDoorwayType="))
                    {
                        settings.PagesDoorwayType = int.Parse(data[i].Substring(13));
                    }
                    else if (data[i].StartsWith("PStaticExtension="))
                    {
                        settings.PagesStaticExtension = data[i].Substring(17);
                    }
                    else if (data[i].StartsWith("PStaticSeparator="))
                    {
                        settings.PagesStaticSeparator = data[i].Substring(17);
                    }
                    else if (data[i].StartsWith("PStaticNamesTypes="))
                    {
                        settings.PagesStaticNamesTypes = int.Parse(data[i].Substring(18));
                    }
                    else if (data[i].StartsWith("PStaticPageNames="))
                    {
                        settings.PagesStaticPageNames = data[i].Substring(17);
                    }
                    else if (data[i].StartsWith("PStaticCategoriesNames="))
                    {
                        settings.PagesStaticCategoriesNames = data[i].Substring(23);
                    }
                    else if (data[i].StartsWith("PStaticIndexContinues="))
                    {
                        settings.PagesStaticIndexContinues = bool.Parse(data[i].Substring(22));
                    }
                    else if (data[i].StartsWith("PStaticIndexContinuesNames="))
                    {
                        settings.PagesStaticIndexContinuesNames = data[i].Substring(27);
                    }
                    else if (data[i].StartsWith("PStaticCategoriesContinues="))
                    {
                        settings.PagesStaticCategoriesContinues = bool.Parse(data[i].Substring(27));
                    }
                    else if (data[i].StartsWith("PStaticCategoriesContinuesNames="))
                    {
                        settings.PagesStaticCategoriesContinuesNames = data[i].Substring(32);
                    }
                    else if (data[i].StartsWith("PStaticKeysPerPageOnContinues="))
                    {
                        settings.PagesStaticKeysPerPageOnContinues = int.Parse(data[i].Substring(30));
                    }
                    else if (data[i].StartsWith("PDynamicPageNames="))
                    {
                        settings.PagesDynamicPageNames = data[i].Substring(18);
                    }
                    else if (data[i].StartsWith("PDynamicCategoriesNames="))
                    {
                        settings.PagesDynamicCategoriesNames = data[i].Substring(24);
                    }
                    else if (data[i].StartsWith("PDynamicStaticPageNames="))
                    {
                        settings.PagesDynamicStaticPageNames = data[i].Substring(24);
                    }
                    else if (data[i].StartsWith("PDynamicIndexContinues="))
                    {
                        settings.PagesDynamicIndexContinues = bool.Parse(data[i].Substring(23));
                    }
                    else if (data[i].StartsWith("PDynamicIndexContinuesNames="))
                    {
                        settings.PagesDynamicIndexContinuesNames = data[i].Substring(28);
                    }
                    else if (data[i].StartsWith("PDynamicCategoriesContinues="))
                    {
                        settings.PagesDynamicCategoriesContinues = bool.Parse(data[i].Substring(28));
                    }
                    else if (data[i].StartsWith("PDynamicCategoriesContinuesNames1="))
                    {
                        settings.PagesDynamicCategoriesContinuesNames1 = data[i].Substring(34);
                    }
                    else if (data[i].StartsWith("PDynamicCategoriesContinuesNames2="))
                    {
                        settings.PagesDynamicCategoriesContinuesNames2 = data[i].Substring(34);
                    }
                    else if (data[i].StartsWith("PDynamicKeysPerPageOnContinues="))
                    {
                        settings.PagesDynamicKeysPerPageOnContinues = int.Parse(data[i].Substring(31));
                    }
                    else if (data[i].StartsWith("StaticPages="))
                    {
                        settings.StaticPages = bool.Parse(data[i].Substring(12));
                    }
                    else if (data[i].StartsWith("StaticPagesList="))
                    {
                        settings.StaticPagesList = data[i].Substring(16).Split(new char[] { '¥' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else if (data[i].StartsWith("StaticPagesIncludeIntoSiteMap="))
                    {
                        settings.StaticPagesIncludeIntoSiteMap = bool.Parse(data[i].Substring(30));
                    }
                    else if (data[i].StartsWith("RSS="))
                    {
                        settings.RSS = bool.Parse(data[i].Substring(4));
                    }
                    else if (data[i].StartsWith("RSSCount="))
                    {
                        settings.RSSCount = int.Parse(data[i].Substring(9));
                    }
                    else if (data[i].StartsWith("RSSFileName="))
                    {
                        settings.RSSFileName = data[i].Substring(12);
                    }
                    else if (data[i].StartsWith("SiteMap="))
                    {
                        settings.SiteMap = bool.Parse(data[i].Substring(8));
                    }
                    else if (data[i].StartsWith("SiteMapType="))
                    {
                        settings.SiteMapType = int.Parse(data[i].Substring(12));
                    }
                    else if (data[i].StartsWith("SiteMapHTMLType="))
                    {
                        settings.SiteMapHTMLType = int.Parse(data[i].Substring(16));
                    }
                    else if (data[i].StartsWith("SiteMapHTMLName="))
                    {
                        settings.SiteMapHTMLName = data[i].Substring(16);
                    }
                    else if (data[i].StartsWith("SiteMapHTMLLinksMin="))
                    {
                        settings.SiteMapHTMLLinksMin = int.Parse(data[i].Substring(20));
                    }
                    else if (data[i].StartsWith("SiteMapHTMLLinksMax="))
                    {
                        settings.SiteMapHTMLLinksMax = int.Parse(data[i].Substring(20));
                    }
                    else if (data[i].StartsWith("Robots="))
                    {
                        settings.Robots = bool.Parse(data[i].Substring(7));
                    }
                    else if (data[i].StartsWith("RobotsType="))
                    {
                        settings.RobotsType = int.Parse(data[i].Substring(11));
                    }
                    else if (data[i].StartsWith("RobotsContent="))
                    {
                        settings.RobotsContent = data[i].Substring(14).Replace("¥", "\r\n");
                    }
                    else if (data[i].StartsWith("TG="))
                    {
                        settings.TextGenration = int.Parse(data[i].Substring(3));
                    }
                    else if (data[i].StartsWith("TGTextLengthMin="))
                    {
                        settings.TextGenrationTextLengthMin = int.Parse(data[i].Substring(16));
                    }
                    else if (data[i].StartsWith("TGTextLengthMax="))
                    {
                        settings.TextGenrationTextLengthMax = int.Parse(data[i].Substring(16));
                    }
                    else if (data[i].StartsWith("TGKeywordsMoreThanOneOnPage="))
                    {
                        settings.TextGenrationKeywordsMoreThanOneOnPage = bool.Parse(data[i].Substring(28));
                    }
                    else if (data[i].StartsWith("TGKeywordsOnPageMin="))
                    {
                        settings.TextGenrationKeywordsOnPageMin = int.Parse(data[i].Substring(20));
                    }
                    else if (data[i].StartsWith("TGKeywordsOnPageMax="))
                    {
                        settings.TextGenrationKeywordsOnPageMax = int.Parse(data[i].Substring(20));
                    }
                    else if (data[i].StartsWith("TGKeywords%Min="))
                    {
                        settings.TextGenrationKeywordsPercentageMin = decimal.Parse(data[i].Substring(15));
                    }
                    else if (data[i].StartsWith("TGKeywords%Max="))
                    {
                        settings.TextGenrationKeywordsPercentageMax = decimal.Parse(data[i].Substring(15));
                    }
                    else if (data[i].StartsWith("TGInsertKeywordsType="))
                    {
                        settings.TextGenrationInsertKeywordsType = int.Parse(data[i].Substring(21));
                    }
                    else if (data[i].StartsWith("TGInsertKeywordsСonfusion="))
                    {
                        settings.TextGenrationInsertKeywordsСonfusion = int.Parse(data[i].Substring(26));
                    }
                    else if (data[i].StartsWith("TG%ForEachKeyword="))
                    {
                        settings.TextGenrationPersentageForEachKeyword = bool.Parse(data[i].Substring(18));
                    }
                    else if (data[i].StartsWith("TGInsertOtherKeywords="))
                    {
                        settings.TextGenrationInsertOtherKeywords = bool.Parse(data[i].Substring(22));
                    }
                    else if (data[i].StartsWith("TGInsertOtherKeywords%="))
                    {
                        settings.TextGenrationInsertOtherKeywordsPercentage = decimal.Parse(data[i].Substring(23));
                    }
                    else if (data[i].StartsWith("TGSelectKeywords="))
                    {
                        settings.TextGenrationSelectKeywords = bool.Parse(data[i].Substring(17));
                    }
                    else if (data[i].StartsWith("TGSelectKeywordsTags="))
                    {
                        settings.TextGenrationSelectKeywordsTags = data[i].Substring(21).Split(new char[] { '¥' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else if (data[i].StartsWith("TGSelectPhrases="))
                    {
                        settings.TextGenrationSelectPhrases = bool.Parse(data[i].Substring(16));
                    }
                    else if (data[i].StartsWith("TGSelectPhrasesTags="))
                    {
                        settings.TextGenrationSelectPhrasesTags = data[i].Substring(20).Split(new char[] { '¥' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else if (data[i].StartsWith("TGSelect%="))
                    {
                        settings.TextGenrationSelectPercentage = decimal.Parse(data[i].Substring(10));
                    }
                    else if (data[i].StartsWith("TGPunctuationMarks="))
                    {
                        settings.TextGenrationPunctuationMarks = bool.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("TGPunctuationMarksInsertMin="))
                    {
                        settings.TextGenrationPunctuationMarksInsertMin = int.Parse(data[i].Substring(28));
                    }
                    else if (data[i].StartsWith("TGPunctuationMarksInsertMax="))
                    {
                        settings.TextGenrationPunctuationMarksInsertMax = int.Parse(data[i].Substring(28));
                    }
                    else if (data[i].StartsWith("TGPunctuationMarksList="))
                    {
                        settings.TextGenrationPunctuationMarksList = data[i].Substring(23).Split(new char[] { '¥' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else if (data[i].StartsWith("TGSentencesLengthType="))
                    {
                        settings.TextGenrationSentencesLengthType = int.Parse(data[i].Substring(22));
                    }
                    else if (data[i].StartsWith("TGSentencesCount="))
                    {
                        settings.TextGenrationSentencesCount = int.Parse(data[i].Substring(17));
                    }
                    else if (data[i].StartsWith("TGSentencesLength="))
                    {
                        settings.TextGenrationSentencesLength = int.Parse(data[i].Substring(18));
                    }
                    else if (data[i].StartsWith("TGSentencesLengthСonfusion="))
                    {
                        settings.TextGenrationSentencesLengthСonfusion = int.Parse(data[i].Substring(27));
                    }
                    else if (data[i].StartsWith("TGSentencesMakeBigLetters="))
                    {
                        settings.TextGenrationSentencesMakeBigLetters = bool.Parse(data[i].Substring(26));
                    }
                    else if (data[i].StartsWith("TGParagraphs="))
                    {
                        settings.TextGenrationParagraphs = bool.Parse(data[i].Substring(13));
                    }
                    else if (data[i].StartsWith("TGParagraphsMin="))
                    {
                        settings.TextGenrationParagraphsMin = int.Parse(data[i].Substring(16));
                    }
                    else if (data[i].StartsWith("TGParagraphsMax="))
                    {
                        settings.TextGenrationParagraphsMax = int.Parse(data[i].Substring(16));
                    }
                    else if (data[i].StartsWith("TGCGTextAnalyseType="))
                    {
                        settings.TextGenrationCGTextAnalyseType = int.Parse(data[i].Substring(20));
                    }
                    else if (data[i].StartsWith("TGCGTextAnalyseCutWordsLength="))
                    {
                        settings.TextGenrationCGTextAnalyseCutWordsLength = int.Parse(data[i].Substring(30));
                    }
                    else if (data[i].StartsWith("TGCGConstructionType="))
                    {
                        settings.TextGenrationCGConstructionType = int.Parse(data[i].Substring(21));
                    }
                    else if (data[i].StartsWith("TGCGConstructionInsertWordsMin="))
                    {
                        settings.TextGenrationCGConstructionInsertWordsMin = int.Parse(data[i].Substring(31));
                    }
                    else if (data[i].StartsWith("TGCGConstructionInsertWordsMax="))
                    {
                        settings.TextGenrationCGConstructionInsertWordsMax = int.Parse(data[i].Substring(31));
                    }
                    else if (data[i].StartsWith("TGCGPunctuationMarksConsideration="))
                    {
                        settings.TextGenrationCGPunctuationMarksConsideration = int.Parse(data[i].Substring(34));
                    }
                    else if (data[i].StartsWith("TGCGConsiderProbability="))
                    {
                        settings.TextGenrationCGConsiderProbability = bool.Parse(data[i].Substring(24));
                    }
                    else if (data[i].StartsWith("TGMRAIType="))
                    {
                        settings.TextGenrationMRAIType = int.Parse(data[i].Substring(11));
                    }
                    else if (data[i].StartsWith("TGMRAIPunctuationMarksConsideration="))
                    {
                        settings.TextGenrationMRAIPunctuationMarksConsideration = int.Parse(data[i].Substring(36));
                    }
                    else if (data[i].StartsWith("TGMRAIRadius="))
                    {
                        settings.TextGenrationMRAIRadius = int.Parse(data[i].Substring(13));
                    }
                    else if (data[i].StartsWith("TGMRAIConstructionType="))
                    {
                        settings.TextGenrationMRAIConstructionType = int.Parse(data[i].Substring(23));
                    }
                    else if (data[i].StartsWith("TGMRAIConstructionInsertWordsMin="))
                    {
                        settings.TextGenrationMRAIConstructionInsertWordsMin = int.Parse(data[i].Substring(33));
                    }
                    else if (data[i].StartsWith("TGMRAIConstructionInsertWordsMax="))
                    {
                        settings.TextGenrationMRAIConstructionInsertWordsMax = int.Parse(data[i].Substring(33));
                    }
                    else if (data[i].StartsWith("MMainlinkType="))
                    {
                        settings.MacrosesMainLinkType = int.Parse(data[i].Substring(14));
                    }
                    else if (data[i].StartsWith("MMainlink="))
                    {
                        settings.MacrosesMainLink = data[i].Substring(10);
                    }
                    else if (data[i].StartsWith("MSite="))
                    {
                        settings.MacrosesSite = data[i].Substring(6).Split(new char[] { '¥' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else if (data[i].StartsWith("MTitle="))
                    {
                        settings.MacrosesTitle = data[i].Substring(7).Split(new char[] { '¥' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else if (data[i].StartsWith("MBlockMainMin="))
                    {
                        settings.MacrosesBlockMainMin = int.Parse(data[i].Substring(14));
                    }
                    else if (data[i].StartsWith("MBlockMainMax="))
                    {
                        settings.MacrosesBlockMainMax = int.Parse(data[i].Substring(14));
                    }
                    else if (data[i].StartsWith("MBlockPageMin="))
                    {
                        settings.MacrosesBlockPageMin = int.Parse(data[i].Substring(14));
                    }
                    else if (data[i].StartsWith("MBlockPageMax="))
                    {
                        settings.MacrosesBlockPageMax = int.Parse(data[i].Substring(14));
                    }
                    else if (data[i].StartsWith("MMenuBlockMainMin="))
                    {
                        settings.MacrosesMenuBlockMainMin = int.Parse(data[i].Substring(18));
                    }
                    else if (data[i].StartsWith("MMenuBlockMainMax="))
                    {
                        settings.MacrosesMenuBlockMainMax = int.Parse(data[i].Substring(18));
                    }
                    else if (data[i].StartsWith("MMenuBlockPageMin="))
                    {
                        settings.MacrosesMenuBlockPageMin = int.Parse(data[i].Substring(18));
                    }
                    else if (data[i].StartsWith("MMenuBlockPageMax="))
                    {
                        settings.MacrosesMenuBlockPageMax = int.Parse(data[i].Substring(18));
                    }
                    else if (data[i].StartsWith("MUserBlock1MainMin="))
                    {
                        settings.MacrosesUserBlock1MainMin = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock1MainMax="))
                    {
                        settings.MacrosesUserBlock1MainMax = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock1PageMin="))
                    {
                        settings.MacrosesUserBlock1PageMin = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock1PageMax="))
                    {
                        settings.MacrosesUserBlock1PageMax = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock2MainMin="))
                    {
                        settings.MacrosesUserBlock2MainMin = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock2MainMax="))
                    {
                        settings.MacrosesUserBlock2MainMax = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock2PageMin="))
                    {
                        settings.MacrosesUserBlock2PageMin = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock2PageMax="))
                    {
                        settings.MacrosesUserBlock2PageMax = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock3MainMin="))
                    {
                        settings.MacrosesUserBlock3MainMin = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock3MainMax="))
                    {
                        settings.MacrosesUserBlock3MainMax = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock3PageMin="))
                    {
                        settings.MacrosesUserBlock3PageMin = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock3PageMax="))
                    {
                        settings.MacrosesUserBlock3PageMax = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock4MainMin="))
                    {
                        settings.MacrosesUserBlock4MainMin = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock4MainMax="))
                    {
                        settings.MacrosesUserBlock4MainMax = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock4PageMin="))
                    {
                        settings.MacrosesUserBlock4PageMin = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock4PageMax="))
                    {
                        settings.MacrosesUserBlock4PageMax = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock5MainMin="))
                    {
                        settings.MacrosesUserBlock5MainMin = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock5MainMax="))
                    {
                        settings.MacrosesUserBlock5MainMax = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock5PageMin="))
                    {
                        settings.MacrosesUserBlock5PageMin = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock5PageMax="))
                    {
                        settings.MacrosesUserBlock5PageMax = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock6MainMin="))
                    {
                        settings.MacrosesUserBlock6MainMin = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock6MainMax="))
                    {
                        settings.MacrosesUserBlock6MainMax = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock6PageMin="))
                    {
                        settings.MacrosesUserBlock6PageMin = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MUserBlock6PageMax="))
                    {
                        settings.MacrosesUserBlock6PageMax = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("MCategoryMenuBlockMainMin="))
                    {
                        settings.MacrosesCategoryMenuBlockMainMin = int.Parse(data[i].Substring(26));
                    }
                    else if (data[i].StartsWith("MCategoryMenuBlockMainMax="))
                    {
                        settings.MacrosesCategoryMenuBlockMainMax = int.Parse(data[i].Substring(26));
                    }
                    else if (data[i].StartsWith("MCategoryMenuBlockPageMin="))
                    {
                        settings.MacrosesCategoryMenuBlockPageMin = int.Parse(data[i].Substring(26));
                    }
                    else if (data[i].StartsWith("MCategoryMenuBlockPageMax="))
                    {
                        settings.MacrosesCategoryMenuBlockPageMax = int.Parse(data[i].Substring(26));
                    }
                    else if (data[i].StartsWith("MNetBlockMainMin="))
                    {
                        settings.MacrosesNetBlockMainMin = int.Parse(data[i].Substring(17));
                    }
                    else if (data[i].StartsWith("MNetBlockMainMax="))
                    {
                        settings.MacrosesNetBlockMainMax = int.Parse(data[i].Substring(17));
                    }
                    else if (data[i].StartsWith("MNetBlockPageMin="))
                    {
                        settings.MacrosesNetBlockPageMin = int.Parse(data[i].Substring(17));
                    }
                    else if (data[i].StartsWith("MNetBlockPageMax="))
                    {
                        settings.MacrosesNetBlockPageMax = int.Parse(data[i].Substring(17));
                    }
                    else if (data[i].StartsWith("EType="))
                    {
                        settings.EntranceType = int.Parse(data[i].Substring(6));
                    }
                    else if (data[i].StartsWith("EInsertType="))
                    {
                        settings.EntranceInsertType = int.Parse(data[i].Substring(12));
                    }
                    else if (data[i].StartsWith("EAcceptorAdressType="))
                    {
                        settings.EntranceAcceptorAdressType = int.Parse(data[i].Substring(20));
                    }
                    else if (data[i].StartsWith("EJSEncrypt="))
                    {
                        settings.EntranceJSEncrypt = bool.Parse(data[i].Substring(11));
                    }
                    else if (data[i].StartsWith("ECode="))
                    {
                        settings.EntranceCode = data[i].Substring(6).Replace("¥", "\r\n");
                    }
                    else if (data[i].StartsWith("ECounter="))
                    {
                        settings.EntranceCounter = data[i].Substring(9).Replace("¥", "\r\n");
                    }
                    else if (data[i].StartsWith("LRelativeURLs="))
                    {
                        settings.LinksRelativeURLs = bool.Parse(data[i].Substring(14));
                    }
                    else if (data[i].StartsWith("LInternal="))
                    {
                        settings.LinksInternal = bool.Parse(data[i].Substring(10));
                    }
                    else if (data[i].StartsWith("LInternalType="))
                    {
                        settings.LinksInternalType = int.Parse(data[i].Substring(14));
                    }
                    else if (data[i].StartsWith("LInternalMinLength="))
                    {
                        settings.LinksInternalMinLength = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("LInternalMaxLength="))
                    {
                        settings.LinksInternalMaxLength = int.Parse(data[i].Substring(19));
                    }
                    else if (data[i].StartsWith("LExternal="))
                    {
                        settings.LinksExternal = bool.Parse(data[i].Substring(10));
                    }
                    else if (data[i].StartsWith("LExternalList="))
                    {
                        settings.LinksExternalList = data[i].Substring(14).Split(new char[] { '¥' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else if (data[i].StartsWith("LExternalInText="))
                    {
                        settings.LinksExternalInText = bool.Parse(data[i].Substring(16));
                    }
                    else if (data[i].StartsWith("LExternalInTextIndexMinimum="))
                    {
                        settings.LinksExternalInTextIndexPageMinimum = int.Parse(data[i].Substring(28));
                    }
                    else if (data[i].StartsWith("LExternalInTextIndexMaximum="))
                    {
                        settings.LinksExternalInTextIndexPageMaximum = int.Parse(data[i].Substring(28));
                    }
                    else if (data[i].StartsWith("LExternalInTextRegularMinimum="))
                    {
                        settings.LinksExternalInTextRegularPageMinimum = int.Parse(data[i].Substring(30));
                    }
                    else if (data[i].StartsWith("LExternalInTextRegularMaximum="))
                    {
                        settings.LinksExternalInTextRegularPageMaximum = int.Parse(data[i].Substring(30));
                    }
                    else if (data[i].StartsWith("Spam="))
                    {
                        settings.Spam = bool.Parse(data[i].Substring(5));
                    }
                    else if (data[i].StartsWith("SpamUrls="))
                    {
                        settings.SpamUrlTypeList = data[i].Substring(9).Split(new char[] { '¥' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else if (data[i].StartsWith("SpamSaveToFiles="))
                    {
                        settings.SpamSaveToFile = bool.Parse(data[i].Substring(16));
                    }
                    else if (data[i].StartsWith("SpamSaveToFilesType="))
                    {
                        settings.SpamSaveToFileType = int.Parse(data[i].Substring(20));
                    }
                    else if (data[i].StartsWith("SpamSaveToFilesPath="))
                    {
                        settings.SpamSaveToFilePath = data[i].Substring(20);
                    }
                    else if (data[i].StartsWith("SpamSaveEncoding="))
                    {
                        settings.SpamSaveEncoding = int.Parse(data[i].Substring(17));
                    }
                    else if (data[i].StartsWith("TagSettings="))
                    {
                        string[] tempData = data[i].Substring(12).Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        settings.TagSettings = new TagSettings[tempData.Length];
                        for (int j = 0; j < tempData.Length; j++)
                        {
                            settings.TagSettings[j] = new TagSettings();
                            string[] tempSettings = tempData[j].Split('¥');
                            if (tempSettings.Length == 2)
                            {
                                settings.TagSettings[j].File = tempSettings[0];
                                settings.TagSettings[j].EncodingType = int.Parse(tempSettings[1]);
                            }
                        }
                    }
                    else if (data[i].StartsWith("FTPUpload="))
                    {
                        settings.FTPUpload = bool.Parse(data[i].Substring(10));
                    }
                    else if (data[i].StartsWith("FTPUploadType="))
                    {
                        settings.FTPUploadType = int.Parse(data[i].Substring(14));
                    }
                    else if (data[i].StartsWith("FTPUploadSaveTo="))
                    {
                        settings.FTPUploadSaveTo = data[i].Substring(16);
                    }
                    else if (data[i].StartsWith("FTPDelete="))
                    {
                        settings.FTPDelete = bool.Parse(data[i].Substring(10));
                    }
                    else if (data[i].StartsWith("FTPUploadArchive="))
                    {
                        settings.FTPUploadArchive = int.Parse(data[i].Substring(17));
                    }
                    else if (data[i].StartsWith("FTPUploadArchiveName="))
                    {
                        settings.FTPUploadArchiveName = data[i].Substring(21);
                    }
                    else if (data[i].StartsWith("FTPUploadInBackground="))
                    {
                        settings.FTPUploadInBackground = bool.Parse(data[i].Substring(22));
                    }
                    else if (data[i].StartsWith("FTPUploadThreads="))
                    {
                        settings.FTPThreads = int.Parse(data[i].Substring(17));
                    }
                    else if (data[i].StartsWith("FTPSettings="))
                    {
                        string[] tempData = data[i].Substring(12).Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        settings.FTPSettings = new FTPSettings[tempData.Length];
                        for (int j = 0; j < tempData.Length; j++)
                        {
                            settings.FTPSettings[j] = new FTPSettings();
                            string[] tempSettings = tempData[j].Split('¥');
                            if (tempSettings.Length == 4)
                            {
                                settings.FTPSettings[j].Host = tempSettings[0];
                                settings.FTPSettings[j].Login = tempSettings[1];
                                settings.FTPSettings[j].Password = tempSettings[2];
                                settings.FTPSettings[j].Folder = tempSettings[3];
                            }
                        }
                    }
                    else if (data[i].StartsWith("XRumerUse="))
                    {
                        settings.XRumerUse = bool.Parse(data[i].Substring(10));
                    }
                    else if (data[i].StartsWith("XRumerDirectory="))
                    {
                        settings.XRumerDirectory = data[i].Substring(16);
                    }
                    else if (data[i].StartsWith("XRumerText="))
                    {
                        settings.XRumerText = data[i].Substring(11).Replace("¥", "\r\n");
                    }
                    else if (data[i].StartsWith("XRumerTemplate="))
                    {
                        settings.XRumerTemplate = data[i].Substring(15).Replace("¥", "\r\n");
                    }
                }
                catch (Exception)
                {
                }
            }
            return settings;
        }

        public static string XRumerDefaultTemplate
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                sb.AppendLine("<XRumerProject>");
                sb.AppendLine("<PrimarySection>");
                sb.AppendLine("<ProjectName>[LINK]</ProjectName>");
                sb.AppendLine("<NickName></NickName>");
                sb.AppendLine("<RealName></RealName>");
                sb.AppendLine("<Password></Password>");
                sb.AppendLine("<EmailAddress></EmailAddress>");
                sb.AppendLine("<EmailPassword></EmailPassword>");
                sb.AppendLine("<EmailLogin></EmailLogin>");
                sb.AppendLine("<EmailPOP></EmailPOP>");
                sb.AppendLine("<Homepage>[LINK]</Homepage>");
                sb.AppendLine("<ICQ></ICQ>");
                sb.AppendLine("<City></City>");
                sb.AppendLine("<Country></Country>");
                sb.AppendLine("<Occupation></Occupation>");
                sb.AppendLine("<Interests></Interests>");
                sb.AppendLine("<Signature>[URL=[LINK]][KEY][/URL]</Signature>");
                sb.AppendLine("<Gender>0</Gender>");
                sb.AppendLine("<UnknownFields></UnknownFields>");
                sb.AppendLine("<PollTitle></PollTitle>");
                sb.AppendLine("<PollOption1></PollOption1>");
                sb.AppendLine("<PollOption2></PollOption2>");
                sb.AppendLine("<PollOption3></PollOption3>");
                sb.AppendLine("<PollOption4></PollOption4>");
                sb.AppendLine("<PollOption5></PollOption5>");
                sb.AppendLine("</PrimarySection>");
                sb.AppendLine("<SecondarySection>");
                sb.AppendLine("<Subject1>[KEY]</Subject1>");
                sb.AppendLine("<Subject2>[KEY]</Subject2>");
                sb.AppendLine("<PostText>[TEXT]</PostText>");
                sb.AppendLine("<Prior></Prior>");
                sb.AppendLine("<OnlyPriors>false</OnlyPriors>");
                sb.AppendLine("</SecondarySection>");
                sb.AppendLine("</XRumerProject>");

                return sb.ToString();
            }
        }
    }
}
