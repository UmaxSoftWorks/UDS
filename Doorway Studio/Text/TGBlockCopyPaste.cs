using System;
using System.Collections.Generic;
using System.Linq;
using Settings;

namespace TextGenerator
{
    class TGBlockCopyPaste : TextGenerator
    {
        PresetSettings settings;
        string[] Blocks;
        Random MainRandom;

        public TGBlockCopyPaste(PresetSettings Settings)
        {
            this.Blocks = new string[0];
            this.settings = Settings;
            this.MainRandom = new Random();
        }

        public override void Load(string Input)
        {
            if (string.IsNullOrEmpty(Input))
            {
                throw new Exception("Input text == string.Empty!");
            }

            Input = Input.Replace("\t", " ").Replace("  ", " ");

            this.Blocks = Input.Split(new string[] { "-------" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < this.Blocks.Length; i++)
            {
                this.Blocks[i] = this.Blocks[i].Trim('-');
            }
        }

        public override List<string> Out(int Length)
        {
            List<string> text = new List<string>(Length);
            do
            {
                text.AddRange(this.Blocks[this.MainRandom.Next(this.Blocks.Length)].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            } while (text.Count < Length);

            return text;
        }

        public override void Dispose()
        {
            try
            {
                this.Blocks = null;
            }
            catch (Exception)
            {
            }
        }
    }
}
