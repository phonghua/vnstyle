using System;
using System.Collections.Generic;
using System.Linq;
using Ricky.Infrastructure.Core.Generic;

namespace Ricky.Infrastructure.Core
{
    public class DateTimeHelper
    {
        public static DateTime GetCurrentDateTime()
        {
            //return DateTime.UtcNow;
            return DateTime.Now;
        }

        public static DateTime ConvertDateTime(string text, string format = "yyyy/MM/dd HH:mm:ss")
        {
            return DateTime.ParseExact(text, format, System.Globalization.CultureInfo.InvariantCulture);
        }

        public static DateTime GetLocalDateTime(DateTime datetime, int offset)
        {
            return datetime.AddHours(offset);
        }

        public static int GetDateKey(DateTime dateTime)
        {
            return dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day;
        }

        public static int GetTimeKey(DateTime dateTime)
        {
            return dateTime.Hour * 60 * 60 + dateTime.Minute * 60 + dateTime.Second;
        }

        public static DateTime GetDateTimeCubeKey(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public static List<BaseEntityName> DaysInMonth()
        {
            var daysInMonth = new List<BaseEntityName>();
            for (int i = 0; i < 31; i++)
            {
                daysInMonth.Add(new BaseEntityName { Id = i + 1, Name = (i + 1).ToString() });
            }
            return daysInMonth;
        }

        public static List<BaseEntityName> MonthsInYear()
        {
            var monthsInYear = new List<BaseEntityName>();
            for (int i = 0; i < 12; i++)
            {
                monthsInYear.Add(new BaseEntityName { Id = i + 1, Name = (i + 1).ToString() });
            }
            return monthsInYear;
        }

        public static string GetDateTimeText(DateTime dateTime, int gmt = 0)
        {
            var datetimeNow = GetCurrentDateTime();
            var dateTimeRange = datetimeNow.Subtract(dateTime);
            if (dateTimeRange.TotalSeconds < 60) return "Vài giây trước";
            if (dateTimeRange.TotalMinutes < 10) return "Vài phút trước";
            if (dateTimeRange.TotalMinutes < 60) return string.Format("{0} phút trước", (int)dateTimeRange.TotalMinutes);
            if (dateTimeRange.TotalHours < 24) return string.Format("{0} giờ, {1} phút trước", ((int)dateTimeRange.TotalHours), (int)dateTimeRange.TotalMinutes - (((int)dateTimeRange.TotalHours) * 60));
            return string.Format("{0:dd/MM/yyyy HH:mm}", dateTime.AddHours(gmt));
        }

        public static string GetTimeText(TimeSpan? time, string emptyText)
        {
            if (!time.HasValue || (time.Value.Hours == 0 && time.Value.Minutes == 0)) return emptyText;
            //var dateTime = new DateTime(1, 1, 1, time.Value.Hours, time.Value.Minutes, time.Value.Seconds);
            if (time.Value.Hours == 0) return $"{time.Value.Minutes} phút";
            return $"{time.Value.Hours} giờ {time.Value.Minutes} phút";
        }



        private static List<BaseEntityName<int, string>> _openingTimes = new List<BaseEntityName<int, string>>();
        private static int _timeSlot = 15; // minutes
        public static List<BaseEntityName<int, string>> GetOpeningTimes()
        {
            if (!_openingTimes.Any())
            {
                _openingTimes = new List<BaseEntityName<int, string>>();
                DateTime time = new DateTime(1, 1, 1, 0, 0, 0);

                var interval = _timeSlot; // minutes
                var max = 24 * (60 / interval);
                for (int i = 0; i < max; i++)
                {
                    _openingTimes.Add(new BaseEntityName<int, string> { Id = i, Name = time.ToString("hh:mm tt") });
                    time = time.Add(new TimeSpan(0, interval, 0));
                }
            }
            return _openingTimes;
        }

        private static List<BaseEntityName<int, string>> _closingTimes = new List<BaseEntityName<int, string>>();
        public static List<BaseEntityName<int, string>> GetClosingTimes()
        {
            if (!_closingTimes.Any())
            {
                _closingTimes = new List<BaseEntityName<int, string>>();
                DateTime time = new DateTime(1, 1, 1, 0, 0, 0);

                var interval = _timeSlot; // minutes
                var max = 24 * (60 / interval);
                for (int i = 0; i < max; i++)
                {
                    _closingTimes.Add(new BaseEntityName<int, string> { Id = i, Name = time.ToString("hh:mm tt") });
                    time = time.Add(new TimeSpan(0, interval, 0));
                }
            }
            return _closingTimes;
        }

        public static bool IsOpeningHour(TimeSpan time, int openingHour, int closingHour)
        {
            var timeSlot = (time.Hours * (60 / _timeSlot) + (float)time.Minutes / (float)15);
            return (timeSlot > (float)openingHour && timeSlot < (float)closingHour);
        }
    }
}
