using System;
using System.Collections.Generic;
using Settings;

namespace TextGenerator
{
    class TGRandom : TextGenerator
    {
        PresetSettings setting;
        Random MainRandom;
        public TGRandom(PresetSettings Setings)
        {
            setting = Setings;
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
            Input = Input.ToLower().Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace("  ", " ");
            //Уборка знаний препинаний
            if (this.setting.TextGenrationMRAIPunctuationMarksConsideration == 1)
            {
                Input = Input.Replace("-", " ").Replace("(", " ").Replace(")", " ").Replace("[", " ").Replace("]", " ").Replace("{", " ").Replace("}", " ");
            }
            //Разбивка на части
            switch (this.setting.TextGenrationMRAIType)
            {
                case 0:
                    {
                        //Words
                        Words = Input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        break;
                    }
                case 1:
                    {
                        //Part of sentences
                        Words = Input.Split(new char[] { '.', ',', '!', '?', ':', ';' }, StringSplitOptions.RemoveEmptyEntries);
                        break;
                    }
                case 2:
                    {
                        //Sentences
                        Words = Input.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
                        break;
                    }
            }
        }

        public override List<string> Out(int Length)
        {
            int currentWordIndex = MainRandom.Next(Words.Length);

            List<string> text = new List<string>(Length);

            do
            {
                currentWordIndex = MainRandom.Next(Words.Length);
                if (this.setting.TextGenrationMRAIConstructionType == 0)
                {
                    text.Add(Words[currentWordIndex]);
                }
                else
                {
                    int wordPlus = MainRandom.Next(this.setting.TextGenrationMRAIConstructionInsertWordsMin, this.setting.TextGenrationMRAIConstructionInsertWordsMax);
                    for (int k = currentWordIndex; k < currentWordIndex + wordPlus; k++)
                    {
                        if (k < Words.Length)
                        {
                            text.Add(Words[k]);
                        }
                        else { break; }
                    }
                }
            } while (text.Count < Length);
            //Разбивка на слова
            /*List<string> newText = new List<string>(text.Count);
            for (int i = 0; i < text.Count; i++)
            {
                string[] tempWords = text[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //Добавление
                for (int k = 0; k < tempWords.Length; k++)
                {
                    newText.Add(tempWords[k]);
                }
            }*/
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

        private void CleanRepeatingWords(ref List<string> Text, int WordIndexOne, int WordIndexTwo)
        {
            int repeatCount = 0;
            //Counting
            for (int i = 0; i < Text.Count - 2; i++)
            {
                try
                {
                    if (Text[i] == Text[WordIndexOne] && Text[i + 1] == Text[WordIndexTwo])
                    {
                        if (i != WordIndexOne)
                        {
                            repeatCount++;
                        }
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }
            //Cleaning
            if (repeatCount >= 3)
            {
                for (int i = 0; i < Text.Count - 2; i++)
                {
                    try
                    {
                        if (Text[i] == Text[WordIndexOne] && Text[i + 1] == Text[WordIndexTwo])
                        {
                            if (i != WordIndexOne)
                            {
                                try
                                {
                                    Text.RemoveAt(i);
                                    Text.RemoveAt(i);
                                }
                                catch (Exception)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }
        }

        private void CleanRepeatingWord(ref List<string> Text, int WordIndex)
        {
            for (int i = 0; i < Text.Count - 2; i++)
            {
                try
                {
                    if (Text[i] == Text[WordIndex] && Text[i + 1] == Text[WordIndex])
                    {
                        Text.RemoveAt(i);
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }
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
