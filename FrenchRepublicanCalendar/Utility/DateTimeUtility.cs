
namespace FrenchRepublicanCalendar.Utility
{
    using System;

    public static class DateTimeUtility
    {
        // https://stackoverflow.com/a/5254812/67004
        private const double JulianDateStart = 2415018.5;

        public static double ToJulianDate(this DateTime date)
        {
            return date.ToOADate() + JulianDateStart;
        }

        public static DateTime FromJulianDate(double jd)
        {
            return DateTime.FromOADate(jd - JulianDateStart);
        }
    }
}
