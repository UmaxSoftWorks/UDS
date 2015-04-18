using Settings;

namespace Doorway_Studio
{
    public class Preset
    {
        public Preset(int ID, string Name, string Comment)
        {
            this.id = ID;
            this.name = Name;
            this.comment = Comment;

            this.statistics = new ItemStatistics();
        }
        #region Data
        private int id;
        private string name;
        private string comment;
        private PresetSettings settings;

        private int templateID;
        private int textID;

        private ItemStatistics statistics;
        #endregion

        #region Properties
        /// <summary>
        /// ID of Presets
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
                return settings;
            }
            set
            {
                settings = value;
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
