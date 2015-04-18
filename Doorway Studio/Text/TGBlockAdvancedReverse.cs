using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Settings;

namespace TextGenerator
{
    class TGBlockAdvancedReverse : TextGenerator
    {
        private PresetSettings settings;
        private int CurrentLine;
        private string[] Blocks;
        private Random MainRandom;

        private List<string> StableItems { get; set; }

        public TGBlockAdvancedReverse(PresetSettings Settings)
        {
            this.CurrentLine = 0;
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
                if (this.Blocks.Length <= this.CurrentLine)
                {
                    this.CurrentLine = 0;
                }

                text.AddRange(this.Blocks[CurrentLine].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                this.CurrentLine++;
            } while (text.Count < Length);

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
                this.Blocks = null;
            }
            catch (Exception) { }
        }
    }
}
