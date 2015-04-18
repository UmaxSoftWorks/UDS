using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Automator.Items
{
    class WorkSpace
    {
        protected WorkSpace()
        {
            this.ID = 0;
            this.Name = string.Empty;

            this.Keywords = new List<Keyword>();
            this.Presets = new List<Preset>();
            this.Tasks = new List<Task>();
            this.Templates = new List<Template>();
            this.Texts = new List<Text>();
        }

        #region Properties
        /// <summary>
        /// Задания
        /// </summary>
        public List<Task> Tasks { get; set; }

        /// <summary>
        /// Кейворды
        /// </summary>
        public List<Keyword> Keywords { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        public List<Text> Texts { get; set; }

        /// <summary>
        /// Presets
        /// </summary>
        public List<Preset> Presets { get; set; }

        /// <summary>
        /// Templates
        /// </summary>
        public List<Template> Templates { get; set; }

        /// <summary>
        /// ID of WorkSpace
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Загрузка данных
        /// </summary>
        public static WorkSpace Load(string Path)
        {
            WorkSpace workSpace = new WorkSpace();

            workSpace.ID = int.Parse(Path.Substring(Path.LastIndexOf("\\") + 1));

            // Загрузка info.txt
            if (!File.Exists(Path + "\\info.txt"))
            {
                throw new Exception();
            }

            string[] data = File.ReadAllLines(Path + "\\info.txt", Encoding.UTF8);
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].StartsWith("Name="))
                {
                    workSpace.Name = data[i].Substring(5);
                }
            }

            // Загрузка кейвордов
            workSpace.LoadKeywords(Path + "\\Keywords");

            // Загрузка текста
            workSpace.LoadText(Path + "\\Text");

            // Загрузка пресетов
            workSpace.LoadPresets(Path + "\\Presets");

            // Загрузка шаблонов
            workSpace.LoadTemplates(Path + "\\Templates");

            // Загрузка заданий
            workSpace.LoadTasks(Path + "\\Tasks");

            return workSpace;
        }

        private void LoadKeywords(string Path)
        {
            if (Directory.Exists(Path))
            {
                string[] data = Directory.GetDirectories(Path, "*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < data.Length; i++)
                {
                    try
                    {
                        this.Keywords.Add(Keyword.Load(data[i]));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void LoadText(string Path)
        {
            if (Directory.Exists(Path))
            {
                string[] data = Directory.GetDirectories(Path, "*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < data.Length; i++)
                {
                    try
                    {
                        this.Texts.Add(Text.Load(data[i]));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void LoadPresets(string Path)
        {
            if (Directory.Exists(Path))
            {
                string[] data = Directory.GetDirectories(Path, "*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < data.Length; i++)
                {
                    try
                    {
                        this.Presets.Add(Preset.Load(data[i]));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void LoadTemplates(string Path)
        {
            if (Directory.Exists(Path))
            {
                string[] data = Directory.GetDirectories(Path, "*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < data.Length; i++)
                {
                    try
                    {
                        this.Templates.Add(Template.Load(data[i]));
                    }
                    catch (Exception){}
                }
            }
        }

        /// <summary>
        /// Загрузка заданий
        /// </summary>
        /// <param name="Path"></param>
        private void LoadTasks(string Path)
        {
            if (Directory.Exists(Path))
            {
                string[] data = Directory.GetDirectories(Path, "*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < data.Length; i++)
                {
                    try
                    {
                        this.Tasks.Add(Task.Load(data[i]));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        #endregion
    }
}
