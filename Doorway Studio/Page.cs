using System.Collections.Generic;
using Doorway_Studio.Classes;

namespace Doorway_Studio
{
    public class Page
    {
        public Page()
        {
            this.Keywords = new List<string>();
            this.Content = string.Empty;
            this.Name = string.Empty;
            this.URL = string.Empty;
            this.Tag = string.Empty;

            this.RelativeURL = string.Empty;
            this.RKeywords = new RandomKeywordCollection();
        }
        #region Data
        public RandomKeywordCollection RKeywords { get; protected set; }

        //For html sitemap
        private int htmlSiteMapPageStart;
        private int htmlSiteMapPageCount;
        #endregion
        #region Properties
        /// <summary>
        /// Контент страницы
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// URL
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Relative URL
        /// </summary>
        public string RelativeURL { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список кейвордов
        /// </summary>
        public List<string> Keywords { get; set; }

        /// <summary>
        /// Тип страницы:
        /// 0 - index;
        /// 1 - page;
        /// 2 - static page;
        /// 3 - category;
        /// 4 - sitemap;
        /// 5 - category continue;
        /// 6 - index continue;
        /// 7 - custom page;
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// Категория
        /// </summary>
        public int Category { get; set; }
        /// <summary>
        /// Номер страницы для категорий/продолжения главной/карты сайта
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Индекс начальной страницы для генерирования карты сайта
        /// </summary>
        public int HTMLSiteMapPageStart
        {
            get
            {
                return this.htmlSiteMapPageStart;
            }
            set
            {
                this.htmlSiteMapPageStart = value;
            }
        }
        /// <summary>
        /// Количество страниц/ссылок на этой странице карты сайта
        /// </summary>
        public int HTMLSiteMapPageCount
        {
            get
            {
                return this.htmlSiteMapPageCount;
            }
            set
            {
                this.htmlSiteMapPageCount = value;
            }
        }

        public string Tag { get; set; }
        #endregion
    }
}
