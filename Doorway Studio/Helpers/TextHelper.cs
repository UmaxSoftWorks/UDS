using System;
using Settings;

namespace Doorway_Studio.Helpers
{
    static class TextHelper
    {
        /// <summary>
        /// Копирование куска текста из оригинального источника
        /// </summary>
        /// <param name="Type">0 - RWORD; 1 - RPHRASE; 2 - RTEXT; 3 - RMINITEXT; 4 - RLINE; 5 - RPARAGRAPH</param>
        /// <returns></returns>
        public static string GetText(int Type, bool UpperCase, PresetSettings Settings, Random Random, int WorkspaceIndex, int TextIndex)
        {
            int startIndex =
                GetNextWordIndexInText(Random.Next(SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length), Random, WorkspaceIndex, TextIndex);
            string text = string.Empty;
            int endIndex = SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length;

            switch (Type)
            {
                case 0:
                    {
                        while ((endIndex >= SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length ||
                               SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.IndexOf(" ", endIndex) < 0))
                        {
                            startIndex =
                                GetNextWordIndexInText(
                                    Random.Next(SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length), Random, WorkspaceIndex, TextIndex);
                            endIndex = startIndex + 1;
                        }

                        break;
                    }

                case 1:
                    {
                        while (endIndex >= SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length ||
                               SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.IndexOf(" ", endIndex) < 0)
                        {
                            startIndex =
                                GetNextWordIndexInText(
                                    Random.Next(SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length), Random, WorkspaceIndex, TextIndex);
                            endIndex = startIndex + 1 + Random.Next(80, 130);
                        }

                        break;
                    }

                case 2:
                    {
                        while (endIndex >= SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length ||
                               SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.IndexOf(" ", endIndex) < 0)
                        {
                            startIndex =
                                GetNextWordIndexInText(
                                    Random.Next(SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length), Random, WorkspaceIndex, TextIndex);
                            endIndex = startIndex + 1 +
                                       Random.Next(6 * Settings.TextGenrationTextLengthMin,
                                                   6 * Settings.TextGenrationTextLengthMax);
                        }

                        break;
                    }

                case 3:
                    {
                        while (endIndex >= SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length ||
                               SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.IndexOf(" ", endIndex) < 0)
                        {
                            startIndex =
                                GetNextWordIndexInText(
                                    Random.Next(SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length), Random, WorkspaceIndex, TextIndex);
                            endIndex = startIndex + 1 +
                                       Random.Next(3 * Settings.TextGenrationTextLengthMin,
                                                   3 * Settings.TextGenrationTextLengthMax);
                        }

                        break;
                    }

                case 4:
                    {
                        DateTime endTime = DateTime.Now.AddSeconds(1);
                        while (endIndex >= SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length ||
                               SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.IndexOf( /*"\r\n"*/"\n", endIndex) <
                               0)
                        {
                            do
                            {
                                if (endTime < DateTime.Now)
                                {
                                    // RWORD
                                    startIndex =
                                        GetNextWordIndexInText(
                                            Random.Next(SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length), Random, WorkspaceIndex, TextIndex);
                                    endIndex = SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length;

                                    while (endIndex >= SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length ||
                                           SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.IndexOf(" ", endIndex) <
                                           0)
                                    {
                                        startIndex =
                                            GetNextWordIndexInText(
                                                Random.Next(SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length), Random, WorkspaceIndex, TextIndex);
                                        endIndex = startIndex + 1;
                                    }
                                }
                                else
                                {
                                    startIndex =
                                        GetNextLineIndexInText(
                                            Random.Next(SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length), Random, WorkspaceIndex, TextIndex) +
                                        2;
                                    endIndex = GetNextLineIndexInText(startIndex, Random, WorkspaceIndex, TextIndex);
                                }

                            } while (endIndex <= startIndex);
                        }

                        break;
                    }

                case 5:
                    {
                        while (endIndex >= SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length ||
                               SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.IndexOf("\n", endIndex) < 0)
                        {
                            DateTime endTime = DateTime.Now.AddSeconds(1);
                            do
                            {
                                if (endTime < DateTime.Now)
                                {
                                    if (endTime.AddSeconds(1) < DateTime.Now)
                                    {
                                        // RWORD
                                        startIndex =
                                            GetNextWordIndexInText(
                                                Random.Next(SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length), Random, WorkspaceIndex, TextIndex);
                                        endIndex = SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length;

                                        while (endIndex >= SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length ||
                                               SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.IndexOf(" ", endIndex) < 0)
                                        {
                                            startIndex =
                                                GetNextWordIndexInText(
                                                    Random.Next(
                                                        SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length), Random, WorkspaceIndex, TextIndex);
                                            endIndex = startIndex + 1;
                                        }
                                    }
                                    else
                                    {
                                        startIndex =
                                            GetNextLineIndexInText(
                                                Random.Next(
                                                    SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length), Random, WorkspaceIndex, TextIndex) + 2;
                                        endIndex = GetNextLineIndexInText(startIndex, Random, WorkspaceIndex, TextIndex);
                                    }
                                }
                                else
                                {
                                    startIndex =
                                        GetNextParagraphIndexInText(
                                            Random.Next(
                                                SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length), Random, WorkspaceIndex, TextIndex) + 4;
                                    endIndex = GetNextParagraphIndexInText(startIndex, Random, WorkspaceIndex, TextIndex);
                                }

                            } while (endIndex <= startIndex);
                        }

                        break;
                    }
            }

            switch (Type)
            {
                case 4:
                case 5:
                    {
                        text = SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts
                            .Substring(startIndex, endIndex - startIndex);
                        break;
                    }

                default:
                    {
                        text = SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts
                            .Substring(startIndex, SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts
                            .IndexOf(" ", endIndex) - startIndex);
                        break;
                    }
            }

            text = text.Replace("\r", " ").Replace("\n", " ");
            while (text.Contains("  "))
            {
                text = text.Replace("  ", " ");
            }

            text = text.Trim();

            text = text.Replace("{", string.Empty).Replace("}", string.Empty).Trim();

            if (Type == 0)
            {
                text = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
            }

            if (UpperCase)
            {
                if (text.Length > 1)
                {
                    text = text.Substring(0, 1).ToUpper() + text.Substring(1);
                }
                else
                {
                    text = text.ToUpper();
                }
            }

            return text.Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        /// <summary>
        /// Gets next word index in text
        /// </summary>
        /// <param name="StartIndex">Index of character from where start looking</param>
        /// <returns>index of first character of next word</returns>
        public static int GetNextWordIndexInText(int StartIndex, Random Random, int WorkspaceIndex, int TextIndex)
        {
            if (StartIndex >= SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length)
            {
                StartIndex = Random.Next(SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length);
            }

            int spacePosition = SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.IndexOf(" ", StartIndex);

            if (spacePosition != -1 &&
                (spacePosition + 1) < SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length)
            {
                if (SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts[spacePosition + 1] == ' ' ||
                    SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts[spacePosition + 1] == '-')
                {
                    spacePosition = GetNextWordIndexInText(StartIndex + 1, Random, WorkspaceIndex, TextIndex);
                }
            }

            return spacePosition == -1 ? SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length : spacePosition;
        }

        public static int GetNextLineIndexInText(int StartIndex, Random Random, int WorkspaceIndex, int TextIndex)
        {
            if (StartIndex >= SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length)
            {
                StartIndex = Random.Next(SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length);
            }

            int linePosition = SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.IndexOf(/*"\r\n"*/"\n", StartIndex) - 1;

            return linePosition == -1 || linePosition >= SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length
                       ? SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length
                       : linePosition;
        }

        public static int GetNextParagraphIndexInText(int StartIndex, Random Random, int WorkspaceIndex, int TextIndex)
        {
            if (StartIndex >= SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length)
            {
                StartIndex = Random.Next(SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length);
            }

            int linePosition = SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.IndexOf("\r\n\r\n", StartIndex);

            return linePosition == -1 || linePosition >= SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length
                       ? SharedData.WorkSpaces[WorkspaceIndex].Texts[TextIndex].Texts.Length
                       : linePosition;
        }
    }
}
