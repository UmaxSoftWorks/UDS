using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Automator.Items;

namespace Automator
{
    class Data
    {
        private static Data instance;

        public static Data Instance
        {
            get { return instance ?? (instance = new Data()); }
        }

        private Data()
        {
            this.Items = new List<WorkSpace>();

            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Data")))
            {
                return;
            }

            // Загрузка
            string[] directories = Directory.GetDirectories(Path.Combine(Application.StartupPath, "Data"), "*", SearchOption.TopDirectoryOnly);

            for (int i = 0; i < directories.Length; i++)
            {
                try
                {
                    Items.Add(WorkSpace.Load(directories[i]));
                }
                catch (Exception) { }
            }

        }

        public List<WorkSpace> Items { get; set; }
    }
}
