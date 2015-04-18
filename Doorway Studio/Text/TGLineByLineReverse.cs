using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Settings;

namespace TextGenerator
{
    class TGLineByLineReverse : TextGenerator
    {
        private PresetSettings settings;
        private int CurrentLine;
        private string[] Lines;
        private Random MainRandom;

        public TGLineByLineReverse(PresetSettings Settings)
        {
            this.CurrentLine = 0;
            this.Lines = new string[0];
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
            this.Lines = Input.Split(new string[] { "\r\n", "\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);
        }

        public override List<string> Out(int Length)
        {
            List<string> text = new List<string>(Length);
            do
            {
                if (this.CurrentLine >= this.Lines.Length)
                {
                    this.CurrentLine = 0;
                }

                text.AddRange(this.Lines[CurrentLine].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                this.CurrentLine++;
            } while (text.Count < Length);

            // Combine
            StringBuilder stringBuilder = new StringBuilder(text.Count * 10);
            for (int i = 0; i < text.Count; i++)
            {
                stringBuilder.Append(text[i] + " ");
            }

            string generatedtext = stringBuilder.ToString();
            string[] parts = generatedtext.Split(new char[] { ',', '.', '!', ':', ';', '?' },
                                                            StringSplitOptions.RemoveEmptyEntries);

            string symbol = string.Empty;

            // Reverse
            for (int i = 0; i < parts.Length; i++)
            {
                try
                {
                    symbol = generatedtext.Substring(generatedtext.IndexOf(parts[i]) + parts[i].Length, 1);
                }
                catch (Exception)
                {
                    symbol = ".";
                }

                string phrase = this.ReverseWords(parts[i]).ToLower();

                if (i == 0)
                {
                    parts[i] = ((phrase.Length > 1)
                                    ? phrase.Substring(0, 1).ToUpper() + phrase.Substring(1)
                                    : phrase.ToUpper()) + symbol;
                }
                else
                {
                    if (parts[i - 1].EndsWith(".") || parts[i - 1].EndsWith("!") || parts[i - 1].EndsWith("?"))
                    {
                        parts[i] = ((phrase.Length > 1)
                                        ? phrase.Substring(0, 1).ToUpper() + phrase.Substring(1)
                                        : phrase.ToUpper()) + symbol;
                    }
                    else
                    {
                        parts[i] = phrase + symbol;
                    }
                }
            }

            return parts.ToList();
        }

        private string ReverseWords(string Text)
        {
            string[] parts = Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < parts.Length; i++)
            {
                stringBuilder.Append(parts[parts.Length - 1 - i] + " ");
            }

            return stringBuilder.ToString().ToLower().Trim();
        }

        public override void Dispose()
        {
            try
            {
                this.Lines = null;
            }
            catch (Exception) { }
        }
    }
}
