using System;
using System.Text;
using Settings;

namespace Doorway_Studio.Images
{
    class ImageRepositoryContext
    {
        public Random Random { get; set; }
        public StringBuilder Log { get; set; }
        public PresetSettings Settings { get; set; }

        public int SiteIndex { get; set; }
        public int WorkSpaceIndex { get; set; }
        public int TemplateIndex { get; set; }

        public string SiteDirectory { get; set; }
    }
}
