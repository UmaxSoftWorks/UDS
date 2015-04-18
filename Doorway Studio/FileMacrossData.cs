using System;
using System.Collections.Generic;

namespace Doorway_Studio
{
    public class FileMacrossData
    {
        private int currentString;
        private Random random;

        public FileMacrossData()
        {
            this.currentString = -1;
            this.random = new Random();
            this.DoorwaysRandomLines = new List<int[,]>();
        }

        /// <summary>
        /// Макрос
        /// </summary>
        public string Macross { get; set; }

        /// <summary>
        /// Содержимое
        /// </summary>
        public string[] Content { get; set; }

        /// <summary>
        /// Тип использования: 0 - случайно, 1 - последовательно, 2 - одна строка на дорвей; 3 - случайная строка/дорвей
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Выбор одной строки, в соответствии с типом макроса
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            if (this.Content.Length == 0)
            {
                return string.Empty;
            }

            if (this.Type == 0)
            {
                return this.Content[this.random.Next(this.Content.Length)];
            }

            if ((this.currentString + 1) == this.Content.Length)
            {
                this.currentString = -1;
            }

            this.currentString++;

            return this.Content[this.currentString];
        }

        /// <summary>
        /// Возвращает строку из файлового макроса
        /// </summary>
        /// <param name="Index">Index - индекс дорвея, который обращается за получением строки</param>
        /// <returns></returns>
        public string GetString(int Index)
        {
            if (this.Content.Length == 0)
            {
                return string.Empty;
            }

            if (this.Type == 2)
            {
                if (Index >= this.Content.Length)
                {
                    Index = 0;
                }

                return this.Content[Index];
            }

            // 3
            // Searching for string for current doorway
            for (int i = 0; i < this.DoorwaysRandomLines.Count; i++)
            {
                if (this.DoorwaysRandomLines[i][0, 0] == Index)
                {
                    return this.Content[this.DoorwaysRandomLines[i][0, 1]];
                }
            }

            // Adding random line
            this.DoorwaysRandomLines.Add(new int[,] { { Index }, { this.random.Next(this.Content.Length) } });
            return this.Content[this.DoorwaysRandomLines[this.DoorwaysRandomLines.Count - 1][0, 1]];
        }

        private List<int[,]> DoorwaysRandomLines;
    }
}
