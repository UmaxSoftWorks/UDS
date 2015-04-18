using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextGenerator
{
    abstract class TextGenerator : IDisposable
    {
        abstract public void Load(string Text);
        abstract public List<string> Out(int Length);
        abstract public void Dispose();
    }
}
