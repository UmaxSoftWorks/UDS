using System;
using System.Collections.Generic;
using System.Linq;
using Settings;

namespace TextGenerator
{
    class TGRandomCommaConnection : TextGenerator
    {
        PresetSettings settings;
        Random MainRandom;

        public TGRandomCommaConnection(PresetSettings Settings)
        {
            this.settings = Settings;
            this.MainRandom = new Random();
        }

        string[] sentenses;
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

            this.sentenses = Input.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public override List<string> Out(int Length)
        {
            List<string> text = new List<string>(Length);
            do
            {
                string[] parts =
                    this.sentenses[this.MainRandom.Next(this.sentenses.Length)].Split(new char[] {',', ';', ':'},
                                                                                      StringSplitOptions.RemoveEmptyEntries);
                text.AddRange(parts[this.MainRandom.Next(parts.Length)].Split(new char[] {' '},
                                                                              StringSplitOptions.RemoveEmptyEntries));
                text.Add((this.MainRandom.Next(5) == 0) ? "." : ",");
            } while (text.Count < Length);
            //Cleaning
            /*for (int i = 0; i < text.Count - 2; i++)
            {
                CleanRepeatingWords(ref text, i, i + 1);
            }
            for (int i = 0; i < text.Count - 2; i++)
            {
                CleanRepeatingWord(ref text, i);
            }*/
            
            return text;
        }

        public override void Dispose()
        {
            try
            {
                this.sentenses = null;
            }
            catch (Exception)
            {
            }
        }
    }
}
