using System;
using System.Collections.Generic;
using Settings;

namespace TextGenerator
{
    class TGMix : TextGenerator
    {
        PresetSettings setting;
        Random MainRandom;
        public TGMix(PresetSettings Setings)
        {
            setting = Setings;
            MainRandom = new Random();
        }
        string[] Words;
        public override void Load(string Input)
        {
            if (Input == string.Empty)
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
                if (currentWordIndex >= Words.Length)
                {
                    currentWordIndex = 0;
                }
                // Простое добавление
                /*if (this.setting.TextGenrationMRAIConstructionType == 0)
                {*/
                    // Добавление
                    text.Add(Words[currentWordIndex]);
                    currentWordIndex++;
                /*}
                else
                {
                    // Сложное добавление
                    int wordPlus = MainRandom.Next(this.setting.TextGenrationMRAIConstructionInsertWordsMin, this.setting.TextGenrationMRAIConstructionInsertWordsMax);
                    for (int k = currentWordIndex; k < currentWordIndex + wordPlus; k++)
                    {
                        try
                        {
                            // Добавление
                            text.Add(Words[k]);
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                }*/
            } while (GetTextLength(ref text) < Length);
            //Перемешивание
            string temp = string.Empty;
            for (int i = 0; i < text.Count; i++)
            {
                currentWordIndex = MainRandom.Next(this.setting.TextGenrationMRAIRadius);
                //Усли 0 - то мешать с фразой меньше по счету, иначе с большей по счету
                if (MainRandom.Next(0, 2) == 0)
                {
                    if ((i - currentWordIndex) > 0)
                    {
                        temp = text[i];
                        text[i] = text[i - currentWordIndex];
                        text[i - currentWordIndex] = temp;
                    }
                    else
                    {
                        temp = text[text.Count - 1];
                        text[text.Count - 1] = text[text.Count - 1 - currentWordIndex];
                        text[text.Count - 1 - currentWordIndex] = temp;
                    }
                }
                else
                {
                    if ((i + currentWordIndex) < text.Count)
                    {
                        temp = text[i];
                        text[i] = text[i + currentWordIndex];
                        text[i + currentWordIndex] = temp;
                    }
                    else
                    {
                        temp = text[0];
                        if (currentWordIndex < text.Count)
                        {
                            text[text.Count - 1] = text[currentWordIndex];
                            text[currentWordIndex] = temp;
                        }
                        else
                        {
                            text[text.Count - 1] = text[0];
                            text[0] = temp;
                        }
                    }
                }
            }
            //Разбивка на слова
            List<string> newText = new List<string>(text.Count);
            for (int i = 0; i < text.Count; i++)
            {
                /*string[] tempWords = text[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //Добавление
                for (int k = 0; k < tempWords.Length; k++)
                {
                    newText.Add(tempWords[k]);
                }*/
                newText.AddRange(text[i].Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries));
            }
            //Cleaning
            for (int i = 0; i < text.Count - 2; i++)
            {
                CleanRepeatingWords(ref text, i, i + 1);
            }
            for (int i = 0; i < text.Count - 2; i++)
            {
                CleanRepeatingWord(ref text, i);
            }
            return newText;
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

        private int GetTextLength(ref List<string> Text)
        {
            int count = 0;

            for (int i = 0; i < Text.Count; i++)
            {
                count += Text[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            }

            return count;
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
