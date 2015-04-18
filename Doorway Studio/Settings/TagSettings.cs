namespace Settings
{
    public class TagSettings
    {
        public TagSettings()
        {
            this.File = string.Empty;

            this.EncodingType = 0;
        }

        /// <summary>
        /// Путь к файлу
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Кодировка файла
        /// </summary>
        public int EncodingType { get; set; }
    }
}
