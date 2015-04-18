using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Doorway_Studio;
using Settings;

namespace TextGenerator
{
    class TGMarkov : TextGenerator
    {
        PresetSettings settings;
        Random MainRandom;
        public TGMarkov(PresetSettings Settings)
        {
            settings = Settings;
            MainRandom = new Random();
        }
        string[] Words;
        string[] FullWords;
        int[] WordsHash;

        public override void Load(string Input)
        {
            if (Input == string.Empty)
            {
                throw new Exception("Input text == string.Empty!");
            }

            Input = Input.ToLower();

            if (this.settings.TextGenrationCGPunctuationMarksConsideration == 1)
            {
                Input = Input.Replace(".", " ").Replace(",", " ").Replace("!", " ").Replace("?", " ").Replace(":", " ")
                    .Replace(";", " ").Replace("-", " ").Replace("(", " ").Replace(")", " ").Replace("[", " ").Replace("]", " ")
                    .Replace("{", " ").Replace("}", " ");
            }

            Words = Input.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace("  ", " ").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            FullWords = new string[Words.Length];
            for (int i = 0; i < Words.Length; i++)
            {
                FullWords[i] = Words[i];
            }
            //Обработка входных слов
            if (this.settings.TextGenrationCGTextAnalyseType == 1)
            {
                for (int i = 0; i < Words.Length; i++)
                {
                    if (Words[i].Length > this.settings.TextGenrationCGTextAnalyseCutWordsLength)
                    {
                        Words[i] = Words[i].Substring(0, this.settings.TextGenrationCGTextAnalyseCutWordsLength);
                    }
                }
            }

            WordsHash = new int[Words.Length];

            for (int i = 0; i < Words.Length; i++)
            {
                WordsHash[i] = Words[i].GetHashCode();
            }

        }

        public override List<string> Out(int Length)
        {
            int currentWordIndex = MainRandom.Next(Words.Length);

            List<string> text = new List<string>(Length);

            text.Add(FullWords[currentWordIndex]);

            do
            {
                try
                {
                    currentWordIndex = GetNextWordPositionsInWorkTextWords(currentWordIndex);
                    /*if (Words[currentWordIndex] == (text[text.Count - 1]))
                    {
                        currentWordIndex++;
                    }*/
                    if (this.settings.TextGenrationCGConstructionType == 0)
                    {
                        text.Add(FullWords[currentWordIndex]);
                    }
                    else
                    {
                        int r = MainRandom.Next(this.settings.TextGenrationCGConstructionInsertWordsMin, this.settings.TextGenrationCGConstructionInsertWordsMax);
                        for (int i = 0; i < r; i++)
                        {
                            try
                            {
                                text.Add(FullWords[currentWordIndex + i]);
                            }
                            catch (Exception)
                            {
                                break;
                            }
                        }
                        currentWordIndex += r;
                    }
                }
                catch (Exception)
                {
                }
            } while (text.Count < Length);
            //Cleaning
            for (int i = 0; i < text.Count - 2; i++)
            {
                CleanRepeatingWords(ref text, i, i + 1);
            }
            for (int i = 0; i < text.Count - 2; i++)
            {
                CleanRepeatingWord(ref text, i);
            }
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

        private int GetNextWordPositionsInWorkTextWords(int CurrentWord)
        {
            List<int> resultsPositions = new List<int>();
            for (int i = 0; i < WordsHash.Length - 1; i++)
            {
                if ((WordsHash[i] == WordsHash[CurrentWord]) && (i != CurrentWord))
                {
                    resultsPositions.Add(i + 1);
                }
            }
            //Вероятности
            if (this.settings.TextGenrationCGConsiderProbability)
            {
                int[] resultsWordsCount = new int[resultsPositions.Count];
                for (int i = 0; i < resultsPositions.Count; i++)
                {
                    for (int j = 0; j < Words.Length; j++)
                    {
                        if (WordsHash[resultsPositions[i]] == WordsHash[j])
                        {
                            resultsWordsCount[i]++;
                        }
                    }
                }
                //Расчет вероятности
                int Max = 0;
                for (int i = 0; i < resultsWordsCount.Length; i++)
                {
                    Max += resultsWordsCount[i];
                }
                int CWord = 0;
                int CChance = MainRandom.Next(Max);
                for (int i = 0; i < resultsWordsCount.Length; i++)
                {
                    CChance -= resultsWordsCount[i];
                    if (CChance <= 0)
                    {
                        CWord = resultsPositions[i];
                        break;
                    }
                }
                try
                {
                    return CWord;
                }
                catch (Exception)
                {
                    return MainRandom.Next(WordsHash.Length);
                }
            }
            else
            {
                try
                {
                    return resultsPositions[MainRandom.Next(resultsPositions.Count)];
                }
                catch (Exception)
                {
                    return MainRandom.Next(WordsHash.Length);
                }
            }
        }

        public override void Dispose()
        {
            try
            {
                Words = null;
                FullWords = null;
                WordsHash = null;
            }
            catch (Exception)
            {
            }
        }
    }
}
