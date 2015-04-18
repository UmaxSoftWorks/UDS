using System;
using System.Collections.Generic;
using Settings;

namespace TextGenerator
{
    class TGConceptualGraph : TextGenerator
    {
        PresetSettings settings;
        Random MainRandom;
        public TGConceptualGraph(PresetSettings Settings)
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
                Input = Input.Replace("-", " ").Replace("(", " ").Replace(")", " ").Replace("[", " ").Replace("]", " ").Replace("{", " ").Replace("}", " ");
            }

            FullWords = Input.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace("  ", " ").Split(new char[] { ',', '.', '!', '?', ':', ';' }, StringSplitOptions.RemoveEmptyEntries);

            //Обработка слов
            Words = new string[FullWords.Length];
            for (int i = 0; i < FullWords.Length; i++)
            {
                FullWords[i] = FullWords[i].Trim();
                if (FullWords[i].Contains(" "))
                {
                    Words[i] = FullWords[i].Substring(FullWords[i].LastIndexOf(" ") + 1);
                }
                else
                {
                    Words[i] = FullWords[i];
                }
                //Обрезание до нужной длины
                /*if (this.settings.TextGenrationCGTextAnalyseType == 1)
                {
                    if (Words[i].Length > this.settings.TextGenrationCGTextAnalyseCutWordsLength)
                    {
                        Words[i] = Words[i].Substring(0, this.settings.TextGenrationCGTextAnalyseCutWordsLength);
                    }
                }*/
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

            text.Add(FullWords[currentWordIndex] + " ");

            do
            {
                try
                {
                    currentWordIndex = GetNextPhrasePositionsInWorkTextWords(currentWordIndex);
                    /*if (Words[currentWordIndex] != text[text.Count - 1])
                    {
                        currentWordIndex++;
                    }*/
                    if (this.settings.TextGenrationCGConstructionType == 0)
                    {
                        //Разбивка на слова
                        string[] tempWords = Words[currentWordIndex + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        //Добавление
                        for (int k = 0; k < tempWords.Length; k++)
                        {
                            text.Add(tempWords[k]);
                        }
                    }
                    else
                    {
                        int r = MainRandom.Next(this.settings.TextGenrationCGConstructionInsertWordsMin, this.settings.TextGenrationCGConstructionInsertWordsMax);
                        for (int i = 0; i < r; i++)
                        {
                            try
                            {
                                //Разбивка на слова
                                string[] tempWords = Words[currentWordIndex + i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                //Добавление
                                for (int k = 0; k < tempWords.Length; k++)
                                {
                                    text.Add(tempWords[k]);
                                }
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
            } while (GetTextLength(ref text) < Length);
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

        private int GetTextLength(ref List<string> Text)
        {
            int count = 0;

            for (int i = 0; i < Text.Count; i++)
            {
                count += Text[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            }

            return count;
        }

        private int GetNextPhrasePositionsInWorkTextWords(int CurrentWord)
        {
            List<int> resultsPositions = new List<int>();


            for (int i = 0; i < Words.Length; i++)
            {
                if (WordsHash[i] == WordsHash[CurrentWord])
                {
                    if ((i != CurrentWord) && ((i + 1) < Words.Length))
                    {
                        resultsPositions.Add(i + 1);
                    }
                }
            }

            if (this.settings.TextGenrationCGConsiderProbability)
            {
                int[] ResultsWordsCount = new int[resultsPositions.Count];
                for (int i = 0; i < resultsPositions.Count; i++)
                {
                    for (int j = 0; j < Words.Length; j++)
                    {
                        if (Words[resultsPositions[i]].Contains(Words[j]))
                        {
                            ResultsWordsCount[i]++;
                        }
                    }
                }
                //Расчет вероятности
                int Max = 0;
                for (int i = 0; i < ResultsWordsCount.Length; i++)
                {
                    Max += ResultsWordsCount[i];
                }
                int CWord = 0;
                int CChance = MainRandom.Next(0, Max);
                for (int i = 0; i < ResultsWordsCount.Length; i++)
                {
                    CChance -= ResultsWordsCount[i];
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
                    return MainRandom.Next(Words.Length);
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
                    return MainRandom.Next(Words.Length);
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
