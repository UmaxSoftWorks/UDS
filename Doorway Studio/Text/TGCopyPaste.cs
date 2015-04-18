using System;
using System.Collections.Generic;
using System.Linq;
using Settings;

namespace TextGenerator
{
    class TGCopyPaste : TextGenerator
    {
        PresetSettings settings;
        Random MainRandom;
        public TGCopyPaste(PresetSettings Settings)
        {
            settings = Settings;
            MainRandom = new Random();
        }

        string[] Words;
        public override void Load(string Input)
        {
            if (string.IsNullOrEmpty(Input))
            {
                throw new Exception("Input text == string.Empty!");
            }
            //Уборка мусора
            Input = Input.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace("  ", " ");
            //Уборка знаков препинаний
            if (this.settings.TextGenrationMRAIPunctuationMarksConsideration == 1)
            {
                Input = Input.Replace("-", " ").Replace("(", " ").Replace(")", " ").Replace("[", " ").Replace("]", " ").Replace("{", " ").Replace("}", " ");
            }
            Words = Input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
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
                Words = null;
            }
            catch (Exception)
            {
            }
        }
    }
}
