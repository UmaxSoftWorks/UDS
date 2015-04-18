using System;
using System.Collections.Generic;
using Settings;

namespace TextGenerator
{
    class TGBigram : TextGenerator
    {
        PresetSettings settings;
        Random MainRandom;

        public TGBigram(PresetSettings Settings)
        {
            settings = Settings;
            MainRandom = new Random();
        }
        string[] Words;
        string[] FullWords;
        int[] WordsHash;
        double[] WordsProbability;

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
            WordsProbability = new double[Words.Length];

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
            //Расчет вероятностей слов в биграмме
            for (int i = 0; i < Words.Length; i++)
            {
                if (i == 0)
                {
                    //First word
                    WordsProbability[i] = GetWordCount(i) / (double)Words.Length;
                }
                else
                {
                    //All other words
                    WordsProbability[i] = GetWordsCount(i - 1, i) / ((double)GetWordCount(i) * (double)Words.Length);
                }
            }
        }

        private double GetWordsCount(int PreviousWordIndex, int WordIndex)
        {
            double count = 0;

            for (int i = 1; i < Words.Length; i++)
            {
                if (WordsHash[PreviousWordIndex] == WordsHash[i - 1] && WordsHash[WordIndex] == WordsHash[i])
                {
                    count++;
                }
            }

            return count;
        }

        private double GetWordCount(int WordIndex)
        {
            double count = 0;

            for (int i = 0; i < Words.Length; i++)
            {
                if (WordsHash[WordIndex] == WordsHash[i])
                {
                    count++;
                }
            }

            return count;
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
            for (int i = 0; i < Words.Length - 1; i++)
            {
                if (((WordsProbability[CurrentWord] - WordsProbability[CurrentWord] / 4) <= WordsProbability[i] && WordsProbability[i] <= (WordsProbability[CurrentWord] + WordsProbability[CurrentWord] / 4)) && (WordsHash[i] != WordsHash[CurrentWord]) && (i != CurrentWord))
                {
                    resultsPositions.Add(i + 1);
                }
            }

            try
            {
                return resultsPositions[MainRandom.Next(resultsPositions.Count)];
            }
            catch (Exception)
            {
                return MainRandom.Next(WordsHash.Length);
            }
        }

        public override void Dispose()
        {
            try
            {
                Words = null;
                FullWords = null;
                WordsHash = null;
                WordsProbability = null;
            }
            catch (Exception)
            {
            }
        }
    }
}
