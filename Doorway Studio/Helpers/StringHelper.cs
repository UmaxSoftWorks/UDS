using System;
using System.Collections.Generic;

namespace Doorway_Studio.Helpers
{
    static class StringHelper
    {
        static string[] EnLetters { get; set; }
        static string[] RuLetters { get; set; }

        static StringHelper()
        {
            EnLetters = new string[] {"q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m"};
            RuLetters = new string[]
                            {"й", "ц", "у", "к", "е", "н", "г", "ш", "щ", "з", "х", "ъ", "ф", "ы", "в", "а", "п", "р", "о", "л", "д", "ж", "э", "я", "ч", "с", "м", "и", "т", "ь", "б", "ю", "ё"};
        }

        public static string MakeRandomString(int MinLength, int MaxLength, Random Random)
        {
            try
            {
                int fileNameLenth = Random.Next(MinLength, MaxLength);
                string fileName = string.Empty;
                while (fileName.Length < fileNameLenth)
                {
                    int rnd = Random.Next(0, 100);
                    if (rnd < 25)
                    {
                        fileName += ((char)Random.Next(48, 58)).ToString();
                    }
                    else if (25 < rnd && rnd < 35)
                    {
                        fileName += "-";
                    }
                    else
                    {
                        fileName += ((char)Random.Next(97, 123)).ToString();
                    }
                    if (fileName == "-")
                    {
                        fileName = string.Empty;
                    }
                }
                return fileName;
            }
            catch (Exception)
            {
            }
            return string.Empty;
        }

        public static string Translit(this string Text)
        {
            string newText = string.Empty;
            for (int i = 0; i < Text.Length; i++)
            {
                switch (Text[i])
                {
                    case 'А':
                        {
                            newText += "A";
                            break;
                        }
                    case 'а':
                        {
                            newText += "a";
                            break;
                        }
                    case 'Б':
                        {
                            newText += "B";
                            break;
                        }
                    case 'б':
                        {
                            newText += "b";
                            break;
                        }
                    case 'В':
                        {
                            newText += "V";
                            break;
                        }
                    case 'в':
                        {
                            newText += "v";
                            break;
                        }
                    case 'Г':
                    case 'Ґ':
                        {
                            newText += "G";
                            break;
                        }
                    case 'г':
                    case 'ґ':
                        {
                            newText += "g";
                            break;
                        }
                    case 'Д':
                        {
                            newText += "D";
                            break;
                        }
                    case 'д':
                        {
                            newText += "d";
                            break;
                        }
                    case 'Е':
                    case 'Э':
                    case 'Ё':
                    case 'Є':
                        {
                            newText += "E";
                            break;
                        }
                    case 'е':
                    case 'э':
                    case 'ё':
                    case 'є':
                        {
                            newText += "e";
                            break;
                        }
                    case 'Ж':
                        {
                            newText += "Zh";
                            break;
                        }
                    case 'ж':
                        {
                            newText += "zh";
                            break;
                        }
                    case 'З':
                        {
                            newText += "Z";
                            break;
                        }
                    case 'з':
                        {
                            newText += "z";
                            break;
                        }
                    case 'І':
                    case 'Ї':
                    case 'И':
                        {
                            newText += "I";
                            break;
                        }
                    case 'і':
                    case 'ї':
                    case 'и':
                        {
                            newText += "i";
                            break;
                        }
                    case 'Й':
                    case 'Ы':
                        {
                            newText += "Y";
                            break;
                        }
                    case 'й':
                    case 'ы':
                        {
                            newText += "y";
                            break;
                        }
                    case 'К':
                        {
                            newText += "K";
                            break;
                        }
                    case 'к':
                        {
                            newText += "k";
                            break;
                        }
                    case 'Л':
                        {
                            newText += "L";
                            break;
                        }
                    case 'л':
                        {
                            newText += "l";
                            break;
                        }
                    case 'М':
                        {
                            newText += "m";
                            break;
                        }
                    case 'м':
                        {
                            newText += "m";
                            break;
                        }
                    case 'Н':
                        {
                            newText += "N";
                            break;
                        }
                    case 'н':
                        {
                            newText += "n";
                            break;
                        }
                    case 'О':
                        {
                            newText += "O";
                            break;
                        }
                    case 'о':
                        {
                            newText += "o";
                            break;
                        }
                    case 'П':
                        {
                            newText += "P";
                            break;
                        }
                    case 'п':
                        {
                            newText += "p";
                            break;
                        }
                    case 'Р':
                        {
                            newText += "R";
                            break;
                        }
                    case 'р':
                        {
                            newText += "r";
                            break;
                        }
                    case 'С':
                        {
                            newText += "S";
                            break;
                        }
                    case 'с':
                        {
                            newText += "s";
                            break;
                        }
                    case 'Т':
                        {
                            newText += "T";
                            break;
                        }
                    case 'т':
                        {
                            newText += "t";
                            break;
                        }
                    case 'У':
                    case 'Ў':
                        {
                            newText += "U";
                            break;
                        }
                    case 'у':
                    case 'ў':
                        {
                            newText += "u";
                            break;
                        }
                    case 'Ф':
                        {
                            newText += "F";
                            break;
                        }
                    case 'ф':
                        {
                            newText += "f";
                            break;
                        }
                    case 'Х':
                        {
                            newText += "H";
                            break;
                        }
                    case 'х':
                        {
                            newText += "h";
                            break;
                        }
                    case 'Ц':
                        {
                            newText += "Ts";
                            break;
                        }
                    case 'ц':
                        {
                            newText += "ts";
                            break;
                        }
                    case 'Ч':
                        {
                            newText += "Ch";
                            break;
                        }
                    case 'ч':
                        {
                            newText += "ch";
                            break;
                        }
                    case 'Ш':
                        {
                            newText += "Sh";
                            break;
                        }
                    case 'ш':
                        {
                            newText += "sh";
                            break;
                        }
                    case 'Щ':
                        {
                            newText += "Sch";
                            break;
                        }
                    case 'щ':
                        {
                            newText += "sch";
                            break;
                        }
                    case 'ь':
                    case 'ъ':
                        {
                            break;
                        }
                    case 'Ю':
                        {
                            newText += "Yu";
                            break;
                        }
                    case 'ю':
                        {
                            newText += "yu";
                            break;
                        }
                    case 'Я':
                        {
                            newText += "Ya";
                            break;
                        }
                    case 'я':
                        {
                            newText += "ya";
                            break;
                        }
                    //Всякие "плохие" символы
                    case '\'':
                    case '"':
                    case ':':
                    case '?':
                    case '*':
                    case '<':
                    case '>':
                    case '|':
                    case '№':
                    case '«':
                    case '»':
                        {
                            break;
                        }
                    default:
                        {
                            newText += Text[i].ToString();
                            break;
                        }
                }
            }
            return newText;
        }

        public static string MakeURLRelative(this string Url)
        {
            string[] parts = Url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            string prefix = string.Empty;
            for (int i = 0; i < parts.Length - 1; i++)
            {
                prefix += "../";
            }
            
            return prefix + Url;
        }

        public static string MakeRandomEnglishLetter(Random Random)
        {
            return EnLetters[Random.Next(EnLetters.Length)];
        }

        public static string MakeRandomRussianLetter(Random Random)
        {
            return RuLetters[Random.Next(RuLetters.Length)];
        }

        public static string GetEnMonth(int Month, bool UpperCase)
        {
            string monthName = string.Empty;
            switch (Month)
            {
                case 0:
                default:
                    {
                        monthName = "january";
                        break;
                    }
                case 1:
                    {
                        monthName = "february";
                        break;
                    }
                case 2:
                    {
                        monthName = "march";
                        break;
                    }
                case 3:
                    {
                        monthName = "april";
                        break;
                    }
                case 4:
                    {
                        monthName = "may";
                        break;
                    }
                case 5:
                    {
                        monthName = "june";
                        break;
                    }
                case 6:
                    {
                        monthName = "july";
                        break;
                    }
                case 7:
                    {
                        monthName = "august";
                        break;
                    }
                case 8:
                    {
                        monthName = "september";
                        break;
                    }
                case 9:
                    {
                        monthName = "october";
                        break;
                    }
                case 10:
                    {
                        monthName = "november";
                        break;
                    }
                case 11:
                    {
                        monthName = "december";
                        break;
                    }
            }

            if (UpperCase)
            {
                monthName = monthName.Substring(0, 1).ToUpper() + monthName.Substring(1);
            }

            return monthName;
        }

        public static string GetRuMonth(int Month, bool UpperCase)
        {
            string monthName = string.Empty;
            switch (Month)
            {
                case 0:
                default:
                    {
                        monthName = "январь";
                        break;
                    }
                case 1:
                    {
                        monthName = "февраль";
                        break;
                    }
                case 2:
                    {
                        monthName = "март";
                        break;
                    }
                case 3:
                    {
                        monthName = "апрель";
                        break;
                    }
                case 4:
                    {
                        monthName = "май";
                        break;
                    }
                case 5:
                    {
                        monthName = "июнь";
                        break;
                    }
                case 6:
                    {
                        monthName = "июль";
                        break;
                    }
                case 7:
                    {
                        monthName = "август";
                        break;
                    }
                case 8:
                    {
                        monthName = "сентябрь";
                        break;
                    }
                case 9:
                    {
                        monthName = "октябрь";
                        break;
                    }
                case 10:
                    {
                        monthName = "ноябрь";
                        break;
                    }
                case 11:
                    {
                        monthName = "декабрь";
                        break;
                    }
            }

            if (UpperCase)
            {
                monthName = monthName.Substring(0, 1).ToUpper() + monthName.Substring(1);
            }

            return monthName;
        }

        public static string GetEnDay(int Day, bool UpperCase)
        {
            string dayName = string.Empty;
            switch (Day)
            {
                case 0:
                default:
                    {
                        dayName = "monday";
                        break;
                    }
                case 1:
                    {
                        dayName = "tuesday";
                        break;
                    }
                case 2:
                    {
                        dayName = "wednesday";
                        break;
                    }
                case 3:
                    {
                        dayName = "thursday";
                        break;
                    }
                case 4:
                    {
                        dayName = "friday";
                        break;
                    }
                case 5:
                    {
                        dayName = "saturday";
                        break;
                    }
                case 6:
                    {
                        dayName = "sunday";
                        break;
                    }
            }

            if (UpperCase)
            {
                dayName = dayName.Substring(0, 1).ToUpper() + dayName.Substring(1);
            }

            return dayName;
        }

        public static string GetRuDay(int Day, bool UpperCase)
        {
            string dayName = string.Empty;
            switch (Day)
            {
                case 0:
                default:
                    {
                        dayName = "понедельник";
                        break;
                    }
                case 1:
                    {
                        dayName = "вторник";
                        break;
                    }
                case 2:
                    {
                        dayName = "среда";
                        break;
                    }
                case 3:
                    {
                        dayName = "четверг";
                        break;
                    }
                case 4:
                    {
                        dayName = "пятница";
                        break;
                    }
                case 5:
                    {
                        dayName = "суббота";
                        break;
                    }
                case 6:
                    {
                        dayName = "воскресенье";
                        break;
                    }
            }

            if (UpperCase)
            {
                dayName = dayName.Substring(0, 1).ToUpper() + dayName.Substring(1);
            }

            return dayName;
        }

        public static string GetTwoDigitNumber(int Number)
        {
            string number = Number.ToString();
            if (number.Length >= 2)
            {
                return number;
            }
            else
            {
                return "0" + number;
            }
        }

        public static string Clear(this string Content)
        {
            return Content.Replace(".", string.Empty).Replace(",", string.Empty)
                .Replace("`", string.Empty).Replace("!", string.Empty).Replace(";", string.Empty).Replace(":", string.Empty)
                .Replace("'", string.Empty).Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty)
                .Replace("\\", string.Empty).Replace("/", string.Empty).Replace("|", string.Empty);
        }

        /// <summary>
        /// Get random word from string
        /// </summary>
        /// <returns></returns>
        public static string GetWord(this string Content)
        {
            if (string.IsNullOrEmpty(Content))
            {
                return string.Empty;
            }

            if (!Content.Contains(" "))
            {
                return Content;
            }

            Random random = new Random(Environment.TickCount);

            string word = string.Empty;

            do
            {
                int startIndex = Content.IndexOf(" ", random.Next(Content.Length));
                if(startIndex == -1 || startIndex == Content.Length)
                {
                    continue;
                }

                int endIndex = Content.IndexOf(" ", startIndex + 1);
                if (endIndex == -1)
                {
                    endIndex = Content.Length;
                }

                word = Content.Substring(startIndex + 1, endIndex - startIndex - 1);
            } while (string.IsNullOrEmpty(word));

            return word;
        }

        public static int GetWordCount(this string Value)
        {
            if (string.IsNullOrEmpty(Value))
            {
                return 0;
            }

            string[] words = Value.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        public static int IndexOfAny(this string Value, params string[] Entities)
        {
            return Value.IndexOfAny(0, Entities);
        }

        public static int IndexOfAny(this string Value, int startIndex, params string[] Entities)
        {
            int index = -1;

            for (int i = 0; i < Entities.Length; i++)
            {
                int idx = Value.IndexOf(Entities[i], startIndex);
                if (idx != -1)
                {
                    if (idx < index || index == -1)
                    {
                        index = idx;
                    }
                }
            }

            return index;
        }

        public static string ToHex(this string Value)
        {
            string[] temp = new string[Value.Length];
            for (int i = 0; i < Value.Length; i++)
            {
                temp[i] = "%" + ((int)Value[i]).ToString("X");
            }

            Value = string.Empty;
            for (int i = 0; i < temp.Length; i++)
            {
                Value += temp[i];
            }

            return Value;
        }

        public static bool ContainsAny(this string Value, IEnumerable<string> Items)
        {
            foreach (string item in Items)
            {
                if (Value.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
