namespace Doorway_Studio
{
    public class KeyWord
    {
        public KeyWord(int ID, string Name, string Comment)
        {
            this.id = ID;
            this.name = Name;
            this.comment = Comment;
            this.items = new string[0];

            this.statistics = new ItemStatistics();
        }
        #region Data
        private int id;
        private string name;
        private string comment;
        private string[] items;

        private ItemStatistics statistics;
        #endregion

        #region Properties
        /// <summary>
        /// ID of KeyWords
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
        /// Кейворды
        /// </summary>
        public string[] Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
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
