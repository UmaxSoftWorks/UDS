using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Settings;

namespace TextGenerator
{
    class TGAdvancedReverse : TextGenerator
    {
        PresetSettings settings;
        Random MainRandom;
        public TGAdvancedReverse(PresetSettings Settings)
        {
            this.settings = Settings;
            this.MainRandom = new Random();
        }

        private string[] Words;
        private List<string> StableItems { get; set; }

        public override void Load(string Input)
        {
            if (string.IsNullOrEmpty(Input))
            {
                throw new Exception("Input text == string.Empty!");
            }

            // Уборка мусора
            Input = Input.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace("  ", " ");

            // Уборка знаков препинаний
            if (this.settings.TextGenrationMRAIPunctuationMarksConsideration == 1)
            {
                Input =
                    Input.Replace("-", " ").Replace("(", " ").Replace(")", " ").Replace("[", " ").Replace("]", " ").
                        Replace("{", " ").Replace("}", " ");
            }

            // Analyze text for stable words pairs
            Container container = new Container();
            Regex finder = new Regex(@"(\A|\s)((\w){1,3})(\s|\t|\A|\Z)(\w){2,21}(\W|\Z|\s)",
                                     RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection collection = finder.Matches(Input);

            foreach (Match match in collection)
            {
                container.Add(match.Value.Trim());
            }

            // Analyze
            this.StableItems = container.SortedItems;

            int validNumber = (int)((this.StableItems.Count / 100) * this.MainRandom.Next(5, 10));

            this.StableItems.RemoveRange(validNumber, this.StableItems.Count - validNumber);

            // Split original text
            this.Words = Input.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        }

        public override List<string> Out(int Length)
        {
            int currentWordIndex = MainRandom.Next(Words.Length);

            List<string> text = new List<string>(Length);
            text.Add(Words[currentWordIndex] + " ");
            do
            {
                currentWordIndex++;
                if (currentWordIndex >= Words.Length)
                {
                    currentWordIndex = 0;
                }

                text.Add(Words[currentWordIndex]);
            } while (text.Count < Length);

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
            StringBuilder stringBuilder = new StringBuilder(text.Count * 10);
            for (int i = 0; i < text.Count; i++)
            {
                stringBuilder.Append(text[i] + " ");
            }

            string generatedtext = stringBuilder.ToString().Trim();
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

            if (parts.Length == 1 || parts.Length == 2)
            {
                return Text;
            }

            StringBuilder stringBuilder = new StringBuilder(Text.Length);

            int index = parts.Length - 1;
            for (int i = index; i > 0; i--)
            {
                if (string.IsNullOrEmpty(parts[i - 1]))
                {
                    continue;
                }

                string pair = (parts[i - 1] + " " + parts[i]).ToLower();

                if (this.StableItems.Contains(pair.ToLower()))
                {
                    // Do not reverse stable words
                    stringBuilder.Append(parts[i - 1] + " " + parts[i]);
                    parts[i - 1] = string.Empty;
                }
                else
                {
                    stringBuilder.Append(parts[i] + " ");
                    if (i == 1)
                    {
                        if (parts[i - 1].Length <= 3)
                        {
                            stringBuilder.Insert(0, parts[i - 1] + " ");
                        }
                        else
                        {
                            stringBuilder.Append(parts[i - 1]);
                        }
                    }
                }
            }

            return stringBuilder.ToString().ToLower().Trim();
        }

        public override void Dispose()
        {
            try
            {
                Words = null;
            }
            catch (Exception)
            {
            }
        }
    }
}
