using System;
using System.Collections;
using System.Collections.Generic;
using Settings;

namespace TextGenerator
{
    class TGMarkovClassic : TextGenerator
    {
        /// <summary>
        /// Summary description for Structs.
        /// </summary>
        public class Structs
        {
            public struct RootWord
            {
                public bool Start;
                public bool End;
                public string Word;
                public int ChildCount;
                public Hashtable Childs;
            }
            public struct Child
            {
                public int Occurrence;
                public string Word;
            }
        }

        ArrayList startindex = new ArrayList();
        Hashtable Words = new Hashtable(10240, .1f);

        PresetSettings settings;
        Random MainRandom;
        public TGMarkovClassic(PresetSettings Settings)
        {
            settings = Settings;
            MainRandom = new Random();
        }

        public override void Load(string Input)
        {
            if (Input == string.Empty)
            {
                throw new Exception("Input text == string.Empty!");
            }
            startindex = new ArrayList();
            Words = new Hashtable(10240, .1f);
            Structs.RootWord w = new Structs.RootWord();
            Structs.Child c = new Structs.Child();

            Input = Input.ToLower();

            if (this.settings.TextGenrationCGPunctuationMarksConsideration == 1)
            {
                Input = Input.Replace(".", " ").Replace(",", " ").Replace("!", " ").Replace("?", " ").Replace(":", " ")
                    .Replace(";", " ").Replace("-", " ").Replace("(", " ").Replace(")", " ").Replace("[", " ").Replace("]", " ")
                    .Replace("{", " ").Replace("}", " ");
            }

            string[] s = Input.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace("  ", " ").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            
            string s1 = string.Empty;
            bool NextisStart = false;
            for (int i = 0; i < s.Length; i++)
            {
                try
                {
                    s1 = s[i].ToLower();
                    w = new Structs.RootWord();
                    c = new Structs.Child();
                    if (Words.ContainsKey(s1))
                    {//Already Exists, add new child word or update count of existing child word
                        if (i < s.Length - 1)
                        {
                            w = (Structs.RootWord)Words[s1];
                            if (NextisStart)
                            {
                                w.Start = true;
                                NextisStart = false;
                                startindex.Add(s1);
                            }
                            if (w.Childs.ContainsKey(s[i + 1].ToLower())) //Exists, just update count
                            {
                                c = (Structs.Child)w.Childs[s[i + 1].ToLower()];
                                c.Occurrence++;
                                w.Childs.Remove(s[i + 1].ToLower());
                            }
                            else //Doesn't Exist, add new word
                            {
                                c.Word = s[i + 1];
                                c.Occurrence = 1;

                            }
                            w.ChildCount++;
                            w.Childs.Add(s[i + 1].ToLower(), c);

                            Words.Remove(s1);
                            Words.Add(s1, w);
                        }
                    }
                    else
                    {//New Word
                        w = new Structs.RootWord();
                        w.Childs = new Hashtable();
                        if (i == 0)
                        {
                            w.Start = true;
                            startindex.Add(s1);
                        }
                        w.Word = s[i];
                        if (i < s.Length - 1)
                        {
                            c.Word = s[i + 1];
                            c.Occurrence = 1;
                            w.Childs.Add(s[i + 1].ToLower(), c);
                            w.ChildCount = 1;
                        }
                        else
                            w.End = true;
                        if (s1.Substring(s1.Length - 1, 1) == ".")
                        {
                            w.End = true;
                            NextisStart = true;
                        }
                        else if (NextisStart)
                        {
                            w.Start = true;
                            NextisStart = false;
                            startindex.Add(s1);
                        }
                        Words.Add(s1, w);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private List<string> Output(int Length)
        {
            List<string> output = new List<string>(10);
            Structs.RootWord w = (Structs.RootWord)Words[((string)startindex[MainRandom.Next(startindex.Count)]).ToLower()];
            output.Add(w.Word + " ");
            Structs.Child c = new Structs.Child();
            ArrayList a = new ArrayList();
            int pos = 0;
            int rnd = 0; int min = 0; int max = 0;
            do
            {
                rnd = MainRandom.Next(w.ChildCount/* + 1*/);
                pos = 0;
                foreach (object x in w.Childs)
                {
                    c = (Structs.Child)w.Childs[((System.Collections.DictionaryEntry)x).Key];
                    min = pos;
                    pos += c.Occurrence; //bigger slice for more occurrences
                    max = pos;
                    if (min <= rnd & max >= rnd)
                    {
                        output.Add(c.Word);
                        w = (Structs.RootWord)Words[c.Word.ToLower()];
                        break;
                    }
                }
                //Выходит ли текст за указанное значение
                if (output.Count > Length)
                {
                    break;
                }
            } while (!w.End);
            return output;
        }

        public override List<string> Out(int Length)
        {
            List<string> text = new List<string>(Length * 2);
            List<string> output = new List<string>();
            do
            {
                output = Output(Length);

                for (int i = 0; i < output.Count; i++)
                {
                    text.Add(output[i]);
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

        public override void Dispose()
        {
            try
            {
                Words = null;
                startindex = null;
            }
            catch (Exception)
            {
            }
        }
    }
}
