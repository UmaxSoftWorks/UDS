using System.Collections.Generic;
using Doorway_Studio.Classes;

namespace Doorway_Studio
{
    public class Template
    {
        public Template(int ID, string Name, string Comment, int EncodingType)
        {
            this.ID = ID;
            this.Name = Name;
            this.Comment = Comment;
            this.EncodingType = EncodingType;

            this.Index = string.Empty;
            this.Categories = new List<TemplatePage>();
            this.Pages = new List<TemplatePage>();
            this.StaticPages = new List<TemplatePage>();
            this.CustomPages = new List<TemplatePage>();
            this.Map = new List<TemplatePage>();
            this.Images = new List<string>();
            this.Files = new List<string>();

            this.Statistics = new ItemStatistics();
        }

        #region Properties

        /// <summary>
        /// ID of Presets
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Index
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// Categories
        /// </summary>
        public List<TemplatePage> Categories { get; set; }

        /// <summary>
        /// Pages
        /// </summary>
        public List<TemplatePage> Pages { get; set; }

        /// <summary>
        /// StaticPages
        /// </summary>
        public List<TemplatePage> StaticPages { get; set; }

        /// <summary>
        /// CustomPages
        /// </summary>
        public List<TemplatePage> CustomPages { get; set; }

        /// <summary>
        /// Images
        /// </summary>
        public List<string> Images { get; set; }

        /// <summary>
        /// Files
        /// </summary>
        public List<string> Files { get; set; }

        /// <summary>
        /// Encoding
        /// </summary>
        public int EncodingType { get; set; }

        /// <summary>
        /// Статистика
        /// </summary>
        public ItemStatistics Statistics { get; set; }

        /// <summary>
        /// HTML SiteMap
        /// </summary>
        public List<TemplatePage> Map { get; set; }
        #endregion
    }
}
