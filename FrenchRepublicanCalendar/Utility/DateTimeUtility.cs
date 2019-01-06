
namespace FrenchRepublicanCalendar.Utility
{
    using System;

    /// <summary>
    /// Utilities to <see cref="DateTime"/>
    /// </summary>
    public static class DateTimeUtility
    {
        // https://stackoverflow.com/a/5254812/67004
        private const double JulianDateStart = 2415018.5;

        /// <summary>
        /// Converts a <see cref="DateTime"/> to Julian date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static double ToJulianDate(this DateTime date)
        {
            return date.ToOADate() + JulianDateStart;
        }

        /// <summary>
        /// Creates a <see cref="DateTime"/> from Julian date
        /// </summary>
        /// <param name="jd"></param>
        /// <returns></returns>
        public static DateTime FromJulianDate(double jd)
        {
            return DateTime.FromOADate(jd - JulianDateStart);
        }
    }
}
