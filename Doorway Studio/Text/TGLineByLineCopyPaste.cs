using System;
using System.Collections.Generic;
using System.Linq;
using Settings;

namespace TextGenerator
{
    class TGLineByLineCopyPaste : TextGenerator
    {
        PresetSettings settings;
        int CurrentLine;
        string[] Lines;
        Random MainRandom;

        public TGLineByLineCopyPaste(PresetSettings Settings)
        {
            CurrentLine = 0;
            Lines = new string[0];
            settings = Settings;
            MainRandom = new Random();
        }

        public override void Load(string Input)
        {
            if (string.IsNullOrEmpty(Input))
            {
                throw new Exception("Input text == string.Empty!");
            }
            Input = Input.Replace("\t", " ").Replace("  ", " ");
            Lines = Input.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public override List<string> Out(int Length)
        {
            List<string> text = new List<string>(Length);
            do
            {
                if (CurrentLine >= Lines.Length)
                {
                    CurrentLine = 0;
                }
                text.AddRange(Lines[CurrentLine].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                CurrentLine++;
            } while (text.Count < Length);

            return text;
        }

        public override void Dispose()
        {
            try
            {
                Lines = null;
            }
            catch (Exception)
            {
            }
        }
    }
}
