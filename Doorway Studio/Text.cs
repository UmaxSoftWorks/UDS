using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doorway_Studio
{
    public class Text
    {
        public Text(int ID, string Name, string Comment)
        {
            this.id = ID;
            this.name = Name;
            this.comment = Comment;
            this.texts = string.Empty;

            this.statistics = new ItemStatistics();
        }
        #region Data
        private int id;
        private string name;
        private string comment;
        private string texts;

        private ItemStatistics statistics;
        #endregion

        #region Properties
        /// <summary>
        /// ID of Text
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
        /// Текст
        /// </summary>
        public string Texts
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
    }
}
