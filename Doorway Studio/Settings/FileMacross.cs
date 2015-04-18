using Settings.Automator;

namespace Settings
{
    public class FileMacross : GlobalizedObject
    {
        public FileMacross()
        {
            this.Macross = string.Empty;
            this.File = string.Empty;

            this.EncodingType = 0;
            this.Type = 0;
        }

        /// <summary>
        /// Макрос
        /// </summary>
        public string Macross { get; set; }

        /// <summary>
        /// Путь к файлу
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Кодировка файла
        /// </summary>
        public int EncodingType { get; set; }

        /// <summary>
        /// Тип использования: 0 - случайно, 1 - последовательно, 2 - одна строка на дорвей; 3 - случайная строка/дорвей
        /// </summary>
        public int Type { get; set; }
    }
}