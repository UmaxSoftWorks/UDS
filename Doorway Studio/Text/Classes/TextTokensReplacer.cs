using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Doorway_Studio.Helpers;

namespace Doorway_Studio.Classes
{
    class TextTokensReplacer
    {
        public TextTokensReplacer()
        {
            this.LoadedFilesContent = new Dictionary<string, string[]>();
        }

        public TextTokensReplacerContext Context { get; set; }

        protected Dictionary<string, string[]> LoadedFilesContent { get; set; }

        protected string[] LoadTextLines(string FilePath)
        {
            FilePath = FilePath.ToLower();

            if (!File.Exists(FilePath))
            {
                return null;
            }

            if (!this.LoadedFilesContent.ContainsKey(FilePath))
            {
                LoadedFilesContent.Add(FilePath, File.ReadAllLines(FilePath, Encoding.Default));
            }

            return LoadedFilesContent[FilePath];
        }

        public string ReplaceRandomTextFileTokens(string Content)
        {
            if (Context == null)
            {
                throw new NullReferenceException("Context");
            }

            int startPosition = 0;
            int endPosition = 0;

            // [RWORD=???]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RWORD=???]...\r\n");
            }

            while (Content.Contains("[RWORD="))
            {
                startPosition = Content.IndexOf("[RWORD=");
                endPosition = Content.IndexOf("]", startPosition);

                string fileName = Content.Substring(startPosition + 7, endPosition - 7 - startPosition);
                Content = Content.Remove(startPosition, endPosition + 1 - startPosition);

                string[] lines = LoadTextLines(fileName);
                if (lines != null && lines.Length != 0)
                {
                    string word = lines[Context.Random.Next(lines.Length)].GetWord().Clear();
                    Content = Content.Insert(startPosition, word);
                }
            }

            // [RBWORD=???]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RBWORD=???]...\r\n");
            }

            while (Content.Contains("[RBWORD="))
            {
                startPosition = Content.IndexOf("[RBWORD=");
                endPosition = Content.IndexOf("]", startPosition);

                string fileName = Content.Substring(startPosition + 8, endPosition - 8 - startPosition);
                Content = Content.Remove(startPosition, endPosition + 1 - startPosition);

                string[] lines = LoadTextLines(fileName);
                if (lines != null && lines.Length != 0)
                {
                    string word = lines[Context.Random.Next(lines.Length)].GetWord().Clear();

                    if (word.Length <= 1)
                    {
                        word = word.ToUpper();
                    }
                    else
                    {
                        word = word.Substring(0, 1).ToUpper() + word.Substring(1);
                    }

                    Content = Content.Insert(startPosition, word);
                }
            }

            // [RPHRASE=???]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RPHRASE=???]...\r\n");
            }

            while (Content.Contains("[RPHRASE="))
            {
                startPosition = Content.IndexOf("[RPHRASE=");
                endPosition = Content.IndexOf("]", startPosition);

                string fileName = Content.Substring(startPosition + 9, endPosition - 9 - startPosition);
                Content = Content.Remove(startPosition, endPosition + 1 - startPosition);

                string[] lines = LoadTextLines(fileName);
                if (lines != null && lines.Length != 0)
                {
                    string line = lines[Context.Random.Next(lines.Length)];

                    int length = Context.Random.Next(80, 130);

                    if (line.Length > length)
                    {
                        line = line.Substring(0, length);
                    }

                    Content = Content.Insert(startPosition, line);
                }
            }

            // [RBPHRASE=???]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RBPHRASE=???]...\r\n");
            }

            while (Content.Contains("[RBPHRASE="))
            {
                startPosition = Content.IndexOf("[RBPHRASE=");
                endPosition = Content.IndexOf("]", startPosition);

                string fileName = Content.Substring(startPosition + 10, endPosition - 10 - startPosition);
                Content = Content.Remove(startPosition, endPosition + 1 - startPosition);

                string[] lines = LoadTextLines(fileName);
                if (lines != null && lines.Length != 0)
                {
                    string line = lines[Context.Random.Next(lines.Length)];

                    int length = Context.Random.Next(80, 130);

                    if (line.Length > length)
                    {
                        line = line.Substring(0, length);
                    }

                    if (line.Length <= 1)
                    {
                        line = line.ToUpper();
                    }
                    else
                    {
                        line = line.Substring(0, 1).ToUpper() + line.Substring(1);
                    }

                    Content = Content.Insert(startPosition, line);
                }
            }

            // [RTEXT=???]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RTEXT=???]...\r\n");
            }

            while (Content.Contains("[RTEXT="))
            {
                startPosition = Content.IndexOf("[RTEXT=");
                endPosition = Content.IndexOf("]", startPosition);

                string fileName = Content.Substring(startPosition + 7, endPosition - 7 - startPosition);
                Content = Content.Remove(startPosition, endPosition + 1 - startPosition);

                string[] lines = LoadTextLines(fileName);
                if (lines != null && lines.Length != 0)
                {
                    int currentLength = 0;
                    int length = Context.Random.Next(Context.Settings.TextGenrationTextLengthMin, Context.Settings.TextGenrationTextLengthMax);

                    List<string> text = new List<string>();

                    int startLine = Context.Random.Next(lines.Length);

                    while (currentLength < length)
                    {
                        currentLength += lines[startLine].GetWordCount();
                        text.Add(lines[startLine]);
                        startLine++;

                        if (lines.Length <= startLine)
                        {
                            startLine = 0;
                        }
                    }

                    Content = Content.Insert(startPosition, text.AsString());
                }
            }

            // [RBTEXT=???]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RBTEXT=???]...\r\n");
            }

            while (Content.Contains("[RBTEXT="))
            {
                startPosition = Content.IndexOf("[RBTEXT=");
                endPosition = Content.IndexOf("]", startPosition);

                string fileName = Content.Substring(startPosition + 8, endPosition - 8 - startPosition);
                Content = Content.Remove(startPosition, endPosition + 1 - startPosition);

                string[] lines = LoadTextLines(fileName);
                if (lines != null && lines.Length != 0)
                {
                    int currentLength = 0;
                    int length = Context.Random.Next(Context.Settings.TextGenrationTextLengthMin, Context.Settings.TextGenrationTextLengthMax);

                    List<string> text = new List<string>();

                    int startLine = Context.Random.Next(lines.Length);

                    while (currentLength < length)
                    {
                        currentLength += lines[startLine].GetWordCount();
                        text.Add(lines[startLine]);
                        startLine++;

                        if (lines.Length <= startLine)
                        {
                            startLine = 0;
                        }
                    }

                    string completeText = text.AsString();

                    if (completeText.Length <= 1)
                    {
                        completeText = completeText.ToUpper();
                    }
                    else
                    {
                        completeText = completeText.Substring(0, 1).ToUpper() + completeText.Substring(1);
                    }

                    Content = Content.Insert(startPosition, completeText);
                }
            }

            // [RMINITEXT=???]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RMINITEXT=???]...\r\n");
            }

            while (Content.Contains("[RMINITEXT="))
            {
                startPosition = Content.IndexOf("[RMINITEXT=");
                endPosition = Content.IndexOf("]", startPosition);

                string fileName = Content.Substring(startPosition + 11, endPosition - 11 - startPosition);
                Content = Content.Remove(startPosition, endPosition + 1 - startPosition);

                string[] lines = LoadTextLines(fileName);
                if (lines != null && lines.Length != 0)
                {
                    int currentLength = 0;
                    int length = Context.Random.Next(Context.Settings.TextGenrationTextLengthMin / 2, Context.Settings.TextGenrationTextLengthMax / 2);

                    List<string> text = new List<string>();

                    int startLine = Context.Random.Next(lines.Length);

                    while (currentLength < length)
                    {
                        currentLength += lines[startLine].GetWordCount();
                        text.Add(lines[startLine]);
                        startLine++;

                        if (lines.Length <= startLine)
                        {
                            startLine = 0;
                        }
                    }

                    Content = Content.Insert(startPosition, text.AsString().Trim());
                }
            }
            // [RBMINITEXT=???]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RBMINITEXT=???]...\r\n");
            }

            while (Content.Contains("[RBMINITEXT="))
            {
                startPosition = Content.IndexOf("[RBMINITEXT=");
                endPosition = Content.IndexOf("]", startPosition);

                string fileName = Content.Substring(startPosition + 12, endPosition - 12 - startPosition);
                Content = Content.Remove(startPosition, endPosition + 1 - startPosition);

                string[] lines = LoadTextLines(fileName);
                if (lines != null && lines.Length != 0)
                {
                    int currentLength = 0;
                    int length = Context.Random.Next(Context.Settings.TextGenrationTextLengthMin / 2, Context.Settings.TextGenrationTextLengthMax / 2);

                    List<string> text = new List<string>();

                    int startLine = Context.Random.Next(lines.Length);

                    while (currentLength < length)
                    {
                        currentLength += lines[startLine].GetWordCount();
                        text.Add(lines[startLine]);
                        startLine++;

                        if (lines.Length <= startLine)
                        {
                            startLine = 0;
                        }
                    }

                    string completeText = text.AsString().Trim();

                    if (completeText.Length <= 1)
                    {
                        completeText = completeText.ToUpper();
                    }
                    else
                    {
                        completeText = completeText.Substring(0, 1).ToUpper() + completeText.Substring(1);
                    }

                    Content = Content.Insert(startPosition, completeText);
                }
            }

            // [RLINE=???]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RLINE=???]...\r\n");
            }

            while (Content.Contains("[RLINE="))
            {
                startPosition = Content.IndexOf("[RLINE=");
                endPosition = Content.IndexOf("]", startPosition);

                string fileName = Content.Substring(startPosition + 7, endPosition - 7 - startPosition);
                Content = Content.Remove(startPosition, endPosition + 1 - startPosition);

                string[] lines = LoadTextLines(fileName);
                if (lines != null && lines.Length != 0)
                {
                    Content = Content.Insert(startPosition, lines[Context.Random.Next(lines.Length)]);
                }
            }

            // [RBLINE=???]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RBLINE=???]...\r\n");
            }

            while (Content.Contains("[RBLINE="))
            {
                startPosition = Content.IndexOf("[RBLINE=");
                endPosition = Content.IndexOf("]", startPosition);

                string fileName = Content.Substring(startPosition + 8, endPosition - 8 - startPosition);
                Content = Content.Remove(startPosition, endPosition + 1 - startPosition);

                string[] lines = LoadTextLines(fileName);
                if (lines != null && lines.Length != 0)
                {
                    string line = lines[Context.Random.Next(lines.Length)];

                    if (line.Length <= 1)
                    {
                        line = line.ToUpper();
                    }
                    else
                    {
                        line = line.Substring(0, 1).ToUpper() + line.Substring(1);
                    }

                    Content = Content.Insert(startPosition, line);
                }
            }

            // [RPARAGRAPH=???]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RPARAGRAPH=???]...\r\n");
            }

            while (Content.Contains("[RPARAGRAPH="))
            {
                // 3 to 5 non empty lines
                startPosition = Content.IndexOf("[RPARAGRAPH=");
                endPosition = Content.IndexOf("]", startPosition);

                string fileName = Content.Substring(startPosition + 12, endPosition - 12 - startPosition);
                Content = Content.Remove(startPosition, endPosition + 1 - startPosition);

                string[] lines = LoadTextLines(fileName);
                if (lines != null && lines.Length != 0)
                {
                    int linesCount = Context.Random.Next(3, 5);
                    int startLine = Context.Random.Next(lines.Length);

                    List<string> text = new List<string>();

                    while (text.Count < linesCount)
                    {
                        string line = lines[startLine];
                        if (!string.IsNullOrEmpty(line))
                        {
                            text.Add(line);
                        }
                        startLine++;

                        if (lines.Length <= startLine)
                        {
                            startLine = 0;
                        }
                    }

                    Content = Content.Insert(startPosition, text.AsString().Trim());
                }
            }

            // [RBPARAGRAPH=???]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RBPARAGRAPH=???]...\r\n");
            }

            while (Content.Contains("[RBPARAGRAPH="))
            {
                // 3 to 5 non empty lines
                startPosition = Content.IndexOf("[RBPARAGRAPH=");
                endPosition = Content.IndexOf("]", startPosition);

                string fileName = Content.Substring(startPosition + 13, endPosition - 13 - startPosition);
                Content = Content.Remove(startPosition, endPosition + 1 - startPosition);

                string[] lines = LoadTextLines(fileName);
                if (lines != null && lines.Length != 0)
                {
                    int linesCount = Context.Random.Next(3, 5);
                    int startLine = Context.Random.Next(lines.Length);

                    List<string> text = new List<string>();

                    while (text.Count < linesCount)
                    {
                        string line = lines[startLine];
                        if (!string.IsNullOrEmpty(line))
                        {
                            text.Add(line);
                        }
                        startLine++;

                        if (lines.Length <= startLine)
                        {
                            startLine = 0;
                        }
                    }

                    string completeText = text.AsString().Trim();

                    if (completeText.Length <= 1)
                    {
                        completeText = completeText.ToUpper();
                    }
                    else
                    {
                        completeText = completeText.Substring(0, 1).ToUpper() + completeText.Substring(1);
                    }

                    Content = Content.Insert(startPosition, completeText);
                }
            }

            return Content;
        }

        public string ReplaceRandomLettersTokens(string Content)
        {
            if (Context == null)
            {
                throw new NullReferenceException("Context");
            }

            int startPosition = 0;

            // [RENLETTER]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RENLETTER]...\r\n");
            }

            while (Content.Contains("[RENLETTER]"))
            {
                startPosition = Content.IndexOf("[RENLETTER]");
                Content = Content.Remove(startPosition, 11);
                Content = Content.Insert(startPosition, StringHelper.MakeRandomEnglishLetter(Context.Random));
            }

            // [RBENLETTER]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RBENLETTER]...\r\n");
            }

            while (Content.Contains("[RBENLETTER]"))
            {
                startPosition = Content.IndexOf("[RBENLETTER]");
                Content = Content.Remove(startPosition, 12);
                Content = Content.Insert(startPosition, StringHelper.MakeRandomEnglishLetter(Context.Random).ToUpper());
            }

            // [RRULETTER]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RRULETTER]...\r\n");
            }

            while (Content.Contains("[RRULETTER]"))
            {
                startPosition = Content.IndexOf("[RRULETTER]");
                Content = Content.Remove(startPosition, 11);
                Content = Content.Insert(startPosition, StringHelper.MakeRandomRussianLetter(Context.Random));
            }

            // [RBRULETTER]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RBRULETTER]...\r\n");
            }

            while (Content.Contains("[RBRULETTER]"))
            {
                startPosition = Content.IndexOf("[RBRULETTER]");
                Content = Content.Remove(startPosition, 12);
                Content = Content.Insert(startPosition, StringHelper.MakeRandomRussianLetter(Context.Random).ToUpper());
            }

            return Content;
        }

        public string ReplaceRandomTextTokens(string Content)
        {
            if (Context == null)
            {
                throw new NullReferenceException("Context");
            }

            int startPosition = 0;

            // [RWORD]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RWORD]...\r\n");
            }

            while (Content.Contains("[RWORD]"))
            {
                startPosition = Content.IndexOf("[RWORD]");
                Content = Content.Remove(startPosition, 7);
                Content = Content.Insert(startPosition, TextHelper.GetText(0, false, Context.Settings, Context.Random, Context.WorkspaceIndex, Context.TextIndex).Clear());
            }

            // [RBWORD]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RBWORD]...\r\n");
            }

            while (Content.Contains("[RBWORD]"))
            {
                startPosition = Content.IndexOf("[RBWORD]");
                Content = Content.Remove(startPosition, 8);
                Content = Content.Insert(startPosition, TextHelper.GetText(0, true, Context.Settings, Context.Random, Context.WorkspaceIndex, Context.TextIndex).Clear());
            }

            // [RPHRASE]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RPHRASE]...\r\n");
            }

            while (Content.Contains("[RPHRASE]"))
            {
                startPosition = Content.IndexOf("[RPHRASE]");
                Content = Content.Remove(startPosition, 9);
                Content = Content.Insert(startPosition, TextHelper.GetText(1, false, Context.Settings, Context.Random, Context.WorkspaceIndex, Context.TextIndex).Clear());
            }

            // [RBPHRASE]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RBPHRASE]...\r\n");
            }

            while (Content.Contains("[RBPHRASE]"))
            {
                startPosition = Content.IndexOf("[RBPHRASE]");
                Content = Content.Remove(startPosition, 10);
                Content = Content.Insert(startPosition, TextHelper.GetText(1, true, Context.Settings, Context.Random, Context.WorkspaceIndex, Context.TextIndex).Clear());
            }

            // [RTEXT]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RTEXT]...\r\n");
            }

            while (Content.Contains("[RTEXT]"))
            {
                startPosition = Content.IndexOf("[RTEXT]");
                Content = Content.Remove(startPosition, 7);
                Content = Content.Insert(startPosition, TextHelper.GetText(2, false, Context.Settings, Context.Random, Context.WorkspaceIndex, Context.TextIndex));
            }

            // [RBTEXT]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RBTEXT]...\r\n");
            }

            while (Content.Contains("[RBTEXT]"))
            {
                startPosition = Content.IndexOf("[RBTEXT]");
                Content = Content.Remove(startPosition, 8);
                Content = Content.Insert(startPosition, TextHelper.GetText(2, true, Context.Settings, Context.Random, Context.WorkspaceIndex, Context.TextIndex));
            }

            // [RMINITEXT]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RMINITEXT]...\r\n");
            }

            while (Content.Contains("[RMINITEXT]"))
            {
                startPosition = Content.IndexOf("[RMINITEXT]");
                Content = Content.Remove(startPosition, 11);
                Content = Content.Insert(startPosition, TextHelper.GetText(3, false, Context.Settings, Context.Random, Context.WorkspaceIndex, Context.TextIndex));
            }
            // [RBMINITEXT]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RBMINITEXT]...\r\n");
            }

            while (Content.Contains("[RBMINITEXT]"))
            {
                startPosition = Content.IndexOf("[RBMINITEXT]");
                Content = Content.Remove(startPosition, 12);
                Content = Content.Insert(startPosition, TextHelper.GetText(3, true, Context.Settings, Context.Random, Context.WorkspaceIndex, Context.TextIndex));
            }

            // [RLINE]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RLINE]...\r\n");
            }

            while (Content.Contains("[RLINE]"))
            {
                startPosition = Content.IndexOf("[RLINE]");
                Content = Content.Remove(startPosition, 7);
                Content = Content.Insert(startPosition, TextHelper.GetText(4, false, Context.Settings, Context.Random, Context.WorkspaceIndex, Context.TextIndex));
            }

            // [RBLINE]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RBLINE]...\r\n");
            }

            while (Content.Contains("[RBLINE]"))
            {
                startPosition = Content.IndexOf("[RBLINE]");
                Content = Content.Remove(startPosition, 8);
                Content = Content.Insert(startPosition, TextHelper.GetText(4, true, Context.Settings, Context.Random, Context.WorkspaceIndex, Context.TextIndex));
            }

            // [RPARAGRAPH]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RPARAGRAPH]...\r\n");
            }

            while (Content.Contains("[RPARAGRAPH]"))
            {
                startPosition = Content.IndexOf("[RPARAGRAPH]");
                Content = Content.Remove(startPosition, 12);
                Content = Content.Insert(startPosition, TextHelper.GetText(5, false, Context.Settings, Context.Random, Context.WorkspaceIndex, Context.TextIndex));
            }

            // [RBPARAGRAPH]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [RBPARAGRAPH]...\r\n");
            }

            while (Content.Contains("[RBPARAGRAPH]"))
            {
                startPosition = Content.IndexOf("[RBPARAGRAPH]");
                Content = Content.Remove(startPosition, 13);
                Content = Content.Insert(startPosition, TextHelper.GetText(5, true, Context.Settings, Context.Random, Context.WorkspaceIndex, Context.TextIndex));
            }

            return Content;
        }
    
        public string ReplaceFullFileContentTextTokens(string Content)
        {
            if (Context == null)
            {
                throw new NullReferenceException("Context");
            }

            int startPosition = 0;
            int endPosition = 0;

            // [TEXTFILE=???]
            if (MainSettings.Debug)
            {
                Context.Log.Append(DateTime.Now.ToString() + " Working on: [TEXTFILE=???]...\r\n");
            }

            while (Content.Contains("[TEXTFILE="))
            {
                startPosition = Content.IndexOf("[TEXTFILE=");
                endPosition = Content.IndexOf("]", startPosition);

                string fileName = Content.Substring(startPosition + 10, endPosition - 10 - startPosition);
                Content = Content.Remove(startPosition, endPosition + 1 - startPosition);

                string text = string.Empty;
                try
                {
                    text = File.ReadAllText(fileName, Encoding.Default);
                }
                catch (Exception) { }
                if (!string.IsNullOrEmpty(text))
                {
                    Content = Content.Insert(startPosition, text);
                }
            }

            return Content;
        }
    }
}
