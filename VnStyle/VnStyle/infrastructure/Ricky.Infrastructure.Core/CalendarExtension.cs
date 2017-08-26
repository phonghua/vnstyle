using System;

namespace Ricky.Infrastructure.Core
{
    public static class Calendar
    {
        public static DateTime ThisYear(this DateTime dateTime, bool end = false)
        {
            if (end) return new DateTime(dateTime.Year, 12, 31, 23, 59, 59);
            return new DateTime(dateTime.Year, 1, 1, 0, 0, 0);
        }

        public static DateTime PreviousYear(this DateTime dateTime, bool end = false)
        {
            if (end) return (new DateTime(dateTime.Year, 12, 31, 23, 59, 59)).AddYears(-1);
            return (new DateTime(dateTime.Year, 1, 1, 0, 0, 0)).AddYears(-1);
        }

        public static DateTime PreviousTwoYear(this DateTime dateTime, bool end = false)
        {
            if (end) return (new DateTime(dateTime.Year, 12, 31, 23, 59, 59)).AddYears(-2);
            return (new DateTime(dateTime.Year, 1, 1, 0, 0, 0)).AddYears(-2);
        }

        public static DateTime CurrentQuarter(this DateTime dateTime, bool end = false)
        {
            var quarter = (dateTime.Month - 1) / 3 + 1;
            if (end)
            {
                var monthInNextQuater = (quarter * 3) + 1;
                var year = dateTime.Year;
                if (monthInNextQuater > 12)
                {
                    monthInNextQuater = 1;
                    year++;
                }
                var firstNextQuarter = new DateTime(year, monthInNextQuater, 1, 0, 0, 0);
                return firstNextQuarter.AddMinutes(-1);
            }
            return new DateTime(dateTime.Year, ((quarter - 1) * 3) + 1, 1, 0, 0, 0);
        }

        public static DateTime PreviousQuarter(this DateTime dateTime, bool end = false)
        {
            var currentQuarter = dateTime.CurrentQuarter();
            var previousQuarter = currentQuarter.AddMonths(-3);
            if (end == false) return previousQuarter;
            return currentQuarter.AddMinutes(-1);
        }

        public static DateTime ThisMonth(this DateTime dateTime, bool end = false)
        {
            var thisMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0); ;
            if (end) return thisMonth.AddMonths(1).AddMinutes(-1);
            return thisMonth;
        }

        public static DateTime PreviousMonth(this DateTime dateTime, bool end = false)
        {
            var thisMonth = dateTime.ThisMonth();
            var previousMonth = thisMonth.AddMonths(-1);
            if (end == false) return previousMonth;
            return previousMonth.AddMonths(1).AddMinutes(-1);
        }

        public static DateTime ThisDay(this DateTime dateTime, bool end = false)
        {
            if (end == false) return dateTime.Date;
            else return dateTime.AddDays(1).AddMinutes(-1);
        }

        public static DateTime Yesterday(this DateTime dateTime, bool end = false)
        {
            var yesterday = dateTime.Date.AddDays(-1);
            if (end == false) return yesterday;
            return yesterday.AddDays(1).AddMinutes(-1);
        }

        public static DateTime ThisWeek(this DateTime dateTime, bool end = false)
        {
            var day = dateTime.Date;
            var subTract = 0;
            switch (day.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    subTract = -4;
                    break;
                case DayOfWeek.Monday:
                    subTract = 0;
                    break;
                case DayOfWeek.Saturday:
                    subTract = -5;
                    break;
                case DayOfWeek.Sunday:
                    subTract = -6;
                    break;
                case DayOfWeek.Thursday:
                    subTract = -3;
                    break;
                case DayOfWeek.Tuesday:
                    subTract = -1;
                    break;
                case DayOfWeek.Wednesday:
                    subTract = -2;
                    break;
                default:
                    break;
            }
            if (end == false) return day.AddDays(subTract);
            return day.AddDays(8 + subTract).AddMinutes(-1);
        }

        public static DateTime LastWeek(this DateTime dateTime, bool end = false)
        {
            var thisWeek = dateTime.ThisWeek();
            if (end == false) return thisWeek.AddDays(-7);
            return thisWeek.AddMinutes(-1);
        }
    }
}
