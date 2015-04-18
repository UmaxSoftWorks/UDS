using System;
using System.IO;
using System.Text;
using Settings;

namespace Automator.Items
{
    public class Task
    {
        public Task()
        {
            this.ID = 0;
            this.Name = string.Empty;
        }

        public Task(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        #region Properties
        /// <summary>
        /// ID of Task
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Настройки
        /// </summary>
        public PresetSettings Settings { get; set; }

        /// <summary>
        /// Время старта задания
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Время, когда выполнение задания завершилось
        /// </summary>
        public DateTime EndTime { get; set; }

        protected int Status { get { return 0; } }

        /// <summary>
        /// Template ID
        /// </summary>
        public int TemplateID { get; set; }

        /// <summary>
        /// Text ID
        /// </summary>
        public int TextID { get; set; }

        /// <summary>
        /// Preset ID
        /// </summary>
        public int PresetID { get; set; }
        #endregion

        public static Task Load(string Path)
        {
            int id = int.Parse(Path.Substring(Path.LastIndexOf("\\") + 1));
            string name = string.Empty;
            int presetID = 0;
            int templateID = 0;
            int textID = 0;
            DateTime startTime = new DateTime();
            DateTime endTime = new DateTime();


            string[] info = File.ReadAllLines(Path + "\\info.txt", Encoding.UTF8);

            for (int j = 0; j < info.Length; j++)
            {
                if (info[j].StartsWith("Name="))
                {
                    name = info[j].Substring(5);
                }
                else if (info[j].StartsWith("PresetID="))
                {
                    presetID = int.Parse(info[j].Substring(9));
                }
                else if (info[j].StartsWith("TemplateID="))
                {
                    templateID = int.Parse(info[j].Substring(11));
                }
                else if (info[j].StartsWith("TextID="))
                {
                    textID = int.Parse(info[j].Substring(7));
                }
                else if (info[j].StartsWith("StartTime="))
                {
                    startTime = DateTime.Parse(info[j].Substring(10));
                }
                else if (info[j].StartsWith("EndTime="))
                {
                    try
                    {
                        endTime = DateTime.Parse(info[j].Substring(8));
                    }
                    catch (Exception)
                    {
                        endTime = new DateTime();
                    }
                }
            }

            Task task = new Task(id, name);

            task.PresetID = presetID;
            task.TemplateID = templateID;
            task.TextID = textID;

            task.StartTime = startTime;
            task.EndTime = endTime;

            try
            {
                task.Settings = PresetSettings.Load(Path + "\\settings.txt");
            }
            catch (Exception)
            {
                task.Settings = new PresetSettings();
            }

            return task;
        }

        public void Save(string Path)
        {
            Directory.CreateDirectory(Path);
            File.SetAttributes(Path, FileAttributes.Normal);

            try
            {
                Directory.CreateDirectory(Path);
                File.SetAttributes(Path, FileAttributes.Normal);


                StringBuilder infoData = new StringBuilder(10000);
                infoData.Append("Name=" + this.Name + "\r\n");
                infoData.Append("Comm=\r\n");
                infoData.Append("PresetID=" + this.PresetID.ToString() + "\r\n");
                infoData.Append("TemplateID=" + this.TemplateID.ToString() + "\r\n");
                infoData.Append("TextID=" + this.TextID.ToString() + "\r\n");
                infoData.Append("Status=" + this.Status.ToString() + "\r\n");
                infoData.Append("StartTime=" + this.StartTime.ToString() + "\r\n");
                infoData.Append("EndTime=\r\nTaskLog=\r\n");

                if (File.Exists(Path + "\\info.txt"))
                {
                    File.SetAttributes(Path + "\\info.txt", FileAttributes.Normal);
                }

                File.WriteAllText(Path + "\\info.txt", infoData.ToString(), Encoding.UTF8);
                File.SetAttributes(Path + "\\info.txt", FileAttributes.Normal);

                if (File.Exists(Path + "\\settings.txt"))
                {
                    File.SetAttributes(Path + "\\settings.txt", FileAttributes.Normal);
                }

                File.WriteAllText(Path + "\\settings.txt", this.Settings.ToString(), Encoding.UTF8);
                File.SetAttributes(Path + "\\settings.txt", FileAttributes.Normal);
            }
            catch (Exception) {}
        }
    }
}
