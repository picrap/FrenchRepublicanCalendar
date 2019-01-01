
namespace FrenchRepublicanCalendar
{
    using System;

    public class FrenchRepublicanCalendar : System.Globalization.Calendar
    {
        public override int[] Eras { get; } = new int[1];

        public override DateTime AddMonths(DateTime time, int months)
        {
            return new FrenchRepublicanDateTime(time).AddMonths(months).DateTime;
        }

        public override DateTime AddYears(DateTime time, int years)
        {
            return time.AddYears(years);
        }

        public override int GetDayOfMonth(DateTime time)
        {
            return new FrenchRepublicanDateTime(time).DayOfMonth;
        }

        public override DayOfWeek GetDayOfWeek(DateTime time)
        {
            return (DayOfWeek)(new FrenchRepublicanDateTime(time).DayOfMonth % 10); // WTF? The calendar want a day of 7-days week?
            // this won't make sense at all
        }

        public override int GetDayOfYear(DateTime time)
        {
            var frenchRepublicanDateTime = new FrenchRepublicanDateTime(time);
            return ((int)frenchRepublicanDateTime.Month - 1) * 30 + frenchRepublicanDateTime.DayOfMonth;
        }

        public override int GetDaysInMonth(int year, int month, int era)
        {
            if (month == 13)
                return IsLeapYear(year) ? 6 : 5;
            return 30; // this is why I love this calendar
        }

        public override int GetDaysInYear(int year, int era)
        {
            return IsLeapYear(year) ? 366 : 365;
        }

        public override int GetEra(DateTime time)
        {
            return 1;
        }

        public override int GetMonth(DateTime time)
        {
            return (int)new FrenchRepublicanDateTime(time).Month;
        }

        public override int GetMonthsInYear(int year, int era)
        {
            return 13;
        }

        public override int GetYear(DateTime time)
        {
            return new FrenchRepublicanDateTime(time).Year;
        }

        public override bool IsLeapDay(int year, int month, int day, int era)
        {
            return day == 6 && IsLeapMonth(year, month, era);
        }

        public override bool IsLeapMonth(int year, int month, int era)
        {
            return month == 13 && IsLeapYear(year, era);
        }

        public override bool IsLeapYear(int year, int era)
        {
            return FrenchRepublicanDateTime.IsLeapYear(year);
        }

        public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
        {
            return new FrenchRepublicanDateTime(year, (FrenchRepublicanMonth)month, day, hour, minute, second, millisecond).DateTime;
        }
    }
}