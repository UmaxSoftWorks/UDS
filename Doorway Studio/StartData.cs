using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doorway_Studio
{
    public class StartData
    {
        public StartData()
        {
            return;
        }
        public StartData(int ThreadIndex, int Start, int End)
        {
            threadIndex = ThreadIndex;
            start = Start;
            end = End;
        }

        #region Data
        protected int threadIndex;
        protected int start;
        protected int end;
        #endregion
        #region Properties
        /// <summary>
        /// Индекс Треда
        /// </summary>
        public int ThreadIndex
        {
            get
            {
                return this.threadIndex;
            }
        }
        /// <summary>
        /// Индекс дорвея, с которого начинать генерирование
        /// </summary>
        public int Start
        {
            get
            {
                return this.start;
            }
        }
        /// <summary>
        /// Индекс дорвея, до которого генерировать
        /// </summary>
        public int End
        {
            get
            {
                return this.end;
            }
        }
        #endregion
    }

    public class FTPStartData : StartData
    {
        public FTPStartData(int ThreadIndex, int Start, int End, string[] Files)
        {
            this.threadIndex = ThreadIndex;
            this.start = Start;
            this.end = End;

            files = Files;
        }

        protected string[] files;
        /// <summary>
        /// Пути к файлам
        /// </summary>
        public string[] Files
        {
            get
            {
                return files;
            }
            set
            {
                files = value;
            }
        }
    }
}
