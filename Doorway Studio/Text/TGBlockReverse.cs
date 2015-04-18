using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Settings;

namespace TextGenerator
{
    class TGBlockReverse : TextGenerator
    {
        PresetSettings settings;
        Random MainRandom;
        public TGBlockReverse(PresetSettings Settings)
        {
            this.settings = Settings;
            this.MainRandom = new Random();
        }

        private string[] Blocks;
        public override void Load(string Input)
        {
            if (string.IsNullOrEmpty(Input))
            {
                throw new Exception("Input text == string.Empty!");
            }

            // Уборка мусора
            Input = Input.Replace("\t", " ").Replace("  ", " ");

            // Уборка знаков препинаний
            if (this.settings.TextGenrationMRAIPunctuationMarksConsideration == 1)
            {
                Input = Input.Replace("-", " ").Replace("(", " ").Replace(")", " ").Replace("[", " ").Replace("]", " ").Replace("{", " ").Replace("}", " ");
            }

            this.Blocks = Input.Split(new string[] { "-------" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < this.Blocks.Length; i++)
            {
                this.Blocks[i] = this.Blocks[i].Trim('-');
            }
        }

        public override List<string> Out(int Length)
        {
            int index = this.MainRandom.Next(this.Blocks.Length);

            List<string> text = new List<string>(Length);
            while (text.Count < Length)
            {
                text.AddRange(this.Blocks[index].Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries));
            }

            // Cleaning
            /*for (int i = 0; i < text.Count - 2; i++)
            {
                CleanRepeatingWords(ref text, i, i + 1);
            }
            for (int i = 0; i < text.Count - 2; i++)
            {
                CleanRepeatingWord(ref text, i);
            }*/

            // Combine
            StringBuilder stringBuilder = new StringBuilder(text.Count*10);
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
                    symbol = generatedtext.Substring(generatedtext.IndexOf(parts[i]) + parts[i].Length + 1, 1);
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
            string[] parts = Text.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

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
                Blocks = null;
            }
            catch (Exception)
            {
            }
        }
    }
}
