using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doorway_Studio
{
    public class ItemStatistics
    {
        public ItemStatistics()
        {
            this.years = new List<StatisticsYears>();
        }

        public static int GetWeekNumber(DateTime Date)
        {
            System.Globalization.CultureInfo currentCultureInfo = System.Globalization.CultureInfo.CurrentCulture;
            int weekNum = currentCultureInfo.Calendar.GetWeekOfYear(Date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            if (weekNum == 53)
            {
                weekNum = 0;
            }
            return weekNum;
        }
        public static List<int> WeeksInMonth(DateTime Date)
        {
            List<int> weeks = new List<int>();
            for (int i = 0; i < DateTime.DaysInMonth(Date.Year, Date.Month); i++)
            {
                int weekNum = GetWeekNumber(new DateTime(Date.Year, Date.Month, i + 1));
                if (weeks.Count == 0)
                {
                    weeks.Add(weekNum);
                }
                else
                {
                    bool found = false;
                    for (int j = 0; j < weeks.Count; j++)
                    {
                        if (weeks[j] == weekNum)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        weeks.Add(weekNum);
                    }
                }
            }
            return weeks;
        }

        private List<StatisticsYears> years;
        /// <summary>
        /// Года
        /// </summary>
        public List<StatisticsYears> Years
        {
            get
            {
                return years;
            }
            set
            {
                years = value;
            }
        }

        public class StatisticsYears
        {
            public StatisticsYears(int Year)
            {
                this.weeks = new List<StatisticsWeeks>();
                year = Year;
            }
            private int year;
            /// <summary>
            /// Год
            /// </summary>
            public int Year
            {
                get
                {
                    return year;
                }
                set
                {
                    year = value;
                }
            }

            private List<StatisticsWeeks> weeks;
            /// <summary>
            /// Недели
            /// </summary>
            public List<StatisticsWeeks> Weeks
            {
                get
                {
                    return weeks;
                }
                set
                {
                    weeks = value;
                }
            }
        }

        public class StatisticsWeeks
        {
            public StatisticsWeeks(int Week)
            {
                week = Week;
            }
            private int week;
            /// <summary>
            /// Неделя
            /// </summary>
            public int Week
            {
                get
                {
                    return week;
                }
                set
                {
                    week = value;
                }
            }
            //Данные для статистики
            private int td;
            private int tt;
            private int dd;
            private int dt;
            private int pd;

            /// <summary>
            /// Tasks Done
            /// </summary>
            public int TD
            {
                get
                {
                    return td;
                }
                set
                {
                    td = value;
                }
            }
            /// <summary>
            /// Tasks Total
            /// </summary>
            public int TT
            {
                get
                {
                    return tt;
                }
                set
                {
                    tt = value;
                }
            }
            /// <summary>
            /// Doorways Done
            /// </summary>
            public int DD
            {
                get
                {
                    return dd;
                }
                set
                {
                    dd = value;
                }
            }
            /// <summary>
            /// Doorways Total
            /// </summary>
            public int DT
            {
                get
                {
                    return dt;
                }
                set
                {
                    dt = value;
                }
            }
            /// <summary>
            /// Pages Done
            /// </summary>
            public int PD
            {
                get
                {
                    return pd;
                }
                set
                {
                    pd = value;
                }
            }
        }
    }
}
