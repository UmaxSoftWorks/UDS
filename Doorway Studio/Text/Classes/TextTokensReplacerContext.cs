using System;
using System.Text;
using Settings;

namespace Doorway_Studio.Classes
{
    class TextTokensReplacerContext
    {
        public Random Random { get; set; }
        public StringBuilder Log { get; set; }
        public PresetSettings Settings { get; set; }
        public int WorkspaceIndex { get; set; }
        public int TextIndex { get; set; }
    }
}
