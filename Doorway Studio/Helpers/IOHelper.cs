using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Settings;

namespace Doorway_Studio.Helpers
{
    static class IOHelper
    {
        private const int IOWaitTreshold = 5;
        private const int IOWaitTresholdPause = 100; // ms

        static IOHelper()
        {
            LockedDirectories = new List<string>();
        }

        public static void SetFileDirectoryDate(this string Path, Random Random, PresetSettings Settings)
        {
            int attemptCount = 0;

            while (File.Exists(Path) && attemptCount <= IOWaitTreshold)
            {
                try
                {
                    FileInfo info = new FileInfo(Path);
                    DateTime fileDate = MakeFileDate(Random, Settings);
                    info.CreationTime = fileDate;
                    info.LastWriteTime = fileDate;
                    info.LastAccessTime = fileDate;
                    return;
                }
                catch (Exception)
                {
                    attemptCount++;
                    Thread.Sleep(IOWaitTresholdPause);
                }
            }

            while (Directory.Exists(Path) && attemptCount <= IOWaitTreshold)
            {
                try
                {
                    DirectoryInfo info = new DirectoryInfo(Path);
                    DateTime fileDate = Settings.GeneralFileDateEnd;
                    info.CreationTime = fileDate;
                    info.LastWriteTime = fileDate;
                    info.LastAccessTime = fileDate;
                    return;
                }
                catch (Exception)
                {
                    attemptCount++;
                    Thread.Sleep(IOWaitTresholdPause);
                }
            }
        }

        public static void SetDirectoryDateRecursive(this string Path, PresetSettings Settings)
        {
            if (!Directory.Exists(Path))
            {
                return;
            }

            string[] directories = Directory.GetDirectories(Path, "*", SearchOption.TopDirectoryOnly);

            foreach (string directory in directories.OrderByDescending(d => d.Length))
            {
                DirectoryInfo info = new DirectoryInfo(directory);
                DateTime date = Settings.GeneralFileDateEnd;
                info.CreationTime = date;
                info.LastWriteTime = date;
                info.LastAccessTime = date;
            }

        }

        public static DateTime MakeFileDate(Random Random, PresetSettings Settings)
        {
            if (Settings == null)
            {
                return DateTime.Now;
            }

            if (Settings.GeneralFileDateEnd < Settings.GeneralFileDateStart)
            {
                return Settings.GeneralFileDateEnd;
            }

            if (Settings.GeneralFileDateStart == Settings.GeneralFileDateEnd)
            {
                return Settings.GeneralFileDateStart;
            }

            // Create date, for specified date
            if (Settings.GeneralFileDateStart.Date == Settings.GeneralFileDateEnd.Date)
            {
                if (Settings.GeneralFileDateEnd.Hour < Settings.GeneralFileDateStart.Hour ||
                    (Settings.GeneralFileDateEnd.Hour < Settings.GeneralFileDateStart.Hour && Settings.GeneralFileDateEnd.Minute < Settings.GeneralFileDateStart.Minute))
                {
                    return Settings.GeneralFileDateStart.Date.AddHours(Random.Next(0, 24)).AddMinutes(Random.Next(0, 60)).AddSeconds(Random.Next(0, 60));
                }

                return
                    Settings.GeneralFileDateStart.Date.AddHours(Random.Next(Settings.GeneralFileDateStart.Hour, Settings.GeneralFileDateEnd.Hour)).AddMinutes(
                        Random.Next(Settings.GeneralFileDateStart.Minute, Settings.GeneralFileDateEnd.Minute)).AddSeconds(Random.Next(0, Settings.GeneralFileDateStart.Second));
            }

            // Date for specified time window
            List<int> yearRange = Enumerable.Range(Settings.GeneralFileDateStart.Year, Settings.GeneralFileDateEnd.Year).ToList();
            List<int> monthRange = new List<int>();

            if (Settings.GeneralFileDateStart.Year == Settings.GeneralFileDateEnd.Year)
            {
                monthRange.AddRange(Enumerable.Range(Settings.GeneralFileDateStart.Month, Settings.GeneralFileDateEnd.Month - Settings.GeneralFileDateStart.Month));
            }
            else
            {
                for (int i = Settings.GeneralFileDateStart.Year; i <= Settings.GeneralFileDateEnd.Year; i++)
                {
                    if (i == Settings.GeneralFileDateStart.Year)
                    {
                        monthRange.AddRange(Enumerable.Range(Settings.GeneralFileDateStart.Month, 12 - Settings.GeneralFileDateStart.Month));
                    }
                    else if (i == Settings.GeneralFileDateEnd.Year)
                    {
                        monthRange.AddRange(Enumerable.Range(1, Settings.GeneralFileDateEnd.Month));
                    }
                    else
                    {
                        monthRange.AddRange(Enumerable.Range(1, 12));
                    }
                }
            }

            List<int> dayRange = new List<int>();
            // The same year and month

            if (Settings.GeneralFileDateStart.Year == Settings.GeneralFileDateEnd.Year && Settings.GeneralFileDateStart.Month == Settings.GeneralFileDateEnd.Month)
            {
                dayRange.AddRange(Enumerable.Range(Settings.GeneralFileDateStart.Day, Settings.GeneralFileDateEnd.Day - Settings.GeneralFileDateStart.Day));
            }
            else
            {
                int dayCount = (Settings.GeneralFileDateEnd - Settings.GeneralFileDateStart).Days;
                for (int i = 0; i < dayCount; i++)
                {
                    dayRange.Add(Settings.GeneralFileDateStart.AddDays(i).Day);
                }
            }

            // Generate date
            int year = yearRange[Random.Next(yearRange.Count)];

            int month = monthRange[Random.Next(monthRange.Count)];
            if (Settings.GeneralFileDateEnd.Month < month)
            {
                month = Settings.GeneralFileDateEnd.Month;
            }

            int day = dayRange[Random.Next(dayRange.Count)];
            if (Settings.GeneralFileDateEnd.Date < new DateTime(year, month, day).Date)
            {
                day = Settings.GeneralFileDateEnd.Day;
            }

            if (Settings.GeneralFileDateEnd.Hour < Settings.GeneralFileDateStart.Hour ||
                (Settings.GeneralFileDateEnd.Hour < Settings.GeneralFileDateStart.Hour && Settings.GeneralFileDateEnd.Minute < Settings.GeneralFileDateStart.Minute))
            {
                return new DateTime(year, month, day, Random.Next(0, 24), Random.Next(0, 60), Random.Next(0, 60));
            }

            return new DateTime(year, month, day, Random.Next(Settings.GeneralFileDateStart.Hour, Settings.GeneralFileDateEnd.Hour),
                                Random.Next(Settings.GeneralFileDateStart.Minute, Settings.GeneralFileDateEnd.Minute), Random.Next(0, Settings.GeneralFileDateStart.Second));
        }

        public static List<string> LockedDirectories { get; private set; }
    }
}
