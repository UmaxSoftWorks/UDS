using System;
using System.Collections.Generic;
using System.Linq;
using Settings;

namespace TextGenerator
{
    class TGCommaConnection : TextGenerator
    {
        PresetSettings settings;
        Random MainRandom;

        public TGCommaConnection(PresetSettings Settings)
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
            int currentPart = 0;
            int currentSentense = this.MainRandom.Next(this.sentenses.Length);

            List<string> text = new List<string>(Length);
            do
            {
                string[] parts =
                    this.sentenses[currentSentense].Split(new char[] { ',', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length <= currentPart)
                {
                    text.Add(".");
                    currentPart = 0;
                    currentSentense = this.MainRandom.Next(this.sentenses.Length);
                }

                text.AddRange(parts[currentPart].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                text.Add(",");

                currentPart++;
                currentSentense++;
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
