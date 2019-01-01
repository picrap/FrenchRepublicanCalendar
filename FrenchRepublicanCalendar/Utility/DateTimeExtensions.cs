
namespace FrenchRepublicanCalendar
{
    using System;

    public static  class DateTimeExtensions
    {
        public static double ToJulianDate(this DateTime date)
        {
            return date.ToOADate() + 2415018.5;
        }
    }
}
