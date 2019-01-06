// It's the French republican calendar!
// https://github.com/picrap/FrenchRepublicanCalendar
// Released under MIT license

namespace FrenchRepublicanCalendar
{
    using System;
    using System.Globalization;

    /// <summary>
    ///     Implementation of <see cref="Calendar" /> for french republican calendar
    /// </summary>
    public class FrenchRepublicanCalendar : Calendar
    {
        /// <inheritdoc />
        public override int[] Eras { get; } = {1};

        /// <inheritdoc />
        public override DateTime AddMonths(DateTime time, int months)
        {
            return new FrenchRepublicanDateTime(time).AddMonths(months).DateTime;
        }

        /// <inheritdoc />
        public override DateTime AddYears(DateTime time, int years)
        {
            return time.AddYears(years);
        }

        /// <inheritdoc />
        public override int GetDayOfMonth(DateTime time)
        {
            return new FrenchRepublicanDateTime(time).DayOfMonth;
        }

        /// <inheritdoc />
        public override DayOfWeek GetDayOfWeek(DateTime time)
        {
            return (DayOfWeek) (new FrenchRepublicanDateTime(time).DayOfMonth % 10); // WTF? The calendar want a day of 7-days week?
            // this won't make sense at all
        }

        /// <inheritdoc />
        public override int GetDayOfYear(DateTime time)
        {
            var frenchRepublicanDateTime = new FrenchRepublicanDateTime(time);
            return ((int) frenchRepublicanDateTime.Month - 1) * 30 + frenchRepublicanDateTime.DayOfMonth;
        }

        /// <inheritdoc />
        public override int GetDaysInMonth(int year, int month, int era)
        {
            if (month == 13)
                return IsLeapYear(year) ? 6 : 5;
            return 30; // this is why I love this calendar
        }

        /// <inheritdoc />
        public override int GetDaysInYear(int year, int era)
        {
            return IsLeapYear(year) ? 366 : 365;
        }

        /// <inheritdoc />
        public override int GetEra(DateTime time)
        {
            return 1;
        }

        /// <inheritdoc />
        public override int GetMonth(DateTime time)
        {
            return (int) new FrenchRepublicanDateTime(time).Month;
        }

        /// <inheritdoc />
        public override int GetMonthsInYear(int year, int era)
        {
            return 13;
        }

        /// <inheritdoc />
        public override int GetYear(DateTime time)
        {
            return new FrenchRepublicanDateTime(time).Year;
        }

        /// <inheritdoc />
        public override bool IsLeapDay(int year, int month, int day, int era)
        {
            return day == 6 && IsLeapMonth(year, month, era);
        }

        /// <inheritdoc />
        public override bool IsLeapMonth(int year, int month, int era)
        {
            return month == 13 && IsLeapYear(year, era);
        }

        /// <inheritdoc />
        public override bool IsLeapYear(int year, int era)
        {
            return FrenchRepublicanDateTime.IsLeapYear(year);
        }

        /// <inheritdoc />
        public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
        {
            return new FrenchRepublicanDateTime(year, (FrenchRepublicanMonth) month, day, hour, minute, second, millisecond).DateTime;
        }
    }
}