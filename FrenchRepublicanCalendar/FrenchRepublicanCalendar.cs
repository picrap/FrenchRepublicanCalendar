
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
            throw new NotImplementedException();
        }

        public override int GetDayOfYear(DateTime time)
        {
            throw new NotImplementedException();
        }

        public override int GetDaysInMonth(int year, int month, int era)
        {
            throw new NotImplementedException();
        }

        public override int GetDaysInYear(int year, int era)
        {
            throw new NotImplementedException();
        }

        public override int GetEra(DateTime time)
        {
            return 1;
        }

        public override int GetMonth(DateTime time)
        {
            return (int) new FrenchRepublicanDateTime(time).Month;
        }

        public override int GetMonthsInYear(int year, int era)
        {
            throw new NotImplementedException();
        }

        public override int GetYear(DateTime time)
        {
            throw new NotImplementedException();
        }

        public override bool IsLeapDay(int year, int month, int day, int era)
        {
            throw new NotImplementedException();
        }

        public override bool IsLeapMonth(int year, int month, int era)
        {
            throw new NotImplementedException();
        }

        public override bool IsLeapYear(int year, int era)
        {
            throw new NotImplementedException();
        }

        public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
        {
            throw new NotImplementedException();
        }
    }
}