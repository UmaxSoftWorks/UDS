using System.IO;
using System.Text;

namespace Automator.Items
{
    class Keyword
    {
        public Keyword()
        {
            this.ID = 0;
            this.Name = string.Empty;
        }

        public Keyword(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public static Keyword Load(string Path)
        {
            int id = int.Parse(Path.Substring(Path.LastIndexOf("\\") + 1));
            string name = string.Empty;

            string[] info = File.ReadAllLines(Path + "\\info.txt", Encoding.UTF8);

            for (int j = 0; j < info.Length; j++)
            {
                if (info[j].StartsWith("Name="))
                {
                    name = info[j].Substring(5);
                }
            }

            return new Keyword(id, name);
        }
    }
}
