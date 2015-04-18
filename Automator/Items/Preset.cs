using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Settings;

namespace Automator.Items
{
    class Preset
    {
        public Preset(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public PresetSettings Settings { get; set; }

        public int TemplateID { get; set; }

        public int TextID { get; set; }

        public static Preset Load(string Path)
        {
            int presetID = int.Parse(Path.Substring(Path.LastIndexOf("\\") + 1));
            string presetName = string.Empty;
            int presetTemplateID = 0;
            int presetTextID = 0;

            string[] info = File.ReadAllLines(Path + "\\info.txt", Encoding.UTF8);

            for (int j = 0; j < info.Length; j++)
            {
                if (info[j].StartsWith("Name="))
                {
                    presetName = info[j].Substring(5);
                }
                else if (info[j].StartsWith("TemplateID="))
                {
                    presetTemplateID = int.Parse(info[j].Substring(11));
                }
                else if (info[j].StartsWith("TextID="))
                {
                    presetTextID = int.Parse(info[j].Substring(7));
                }
            }

            Preset preset = new Preset(presetID, presetName);

            preset.TemplateID = presetTemplateID;
            preset.TextID = presetTextID;

            try
            {
                preset.Settings = PresetSettings.Load(Path + "\\settings.txt");
            }
            catch (Exception)
            {
                preset.Settings = new PresetSettings();
            }

            return preset;
        }
    }
}
