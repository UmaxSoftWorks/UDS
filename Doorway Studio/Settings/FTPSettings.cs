using Settings.Automator;

namespace Settings
{
    public class FTPSettings : GlobalizedObject
    {
        public FTPSettings()
        {
            this.Host = string.Empty;
            this.Login = string.Empty;
            this.Password = string.Empty;
            this.Folder = string.Empty;
        }

        /// <summary>
        /// Хост
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Уладенная папка
        /// </summary>
        public string Folder { get; set; }
    }
}
