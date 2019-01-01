#region Arx One

// Arx One
// The ass kicking online backup
// (c) Arx One 2009-2018

#endregion

namespace FrenchRepublicanCalendar
{
    using System;

    public class FrenchRepublicanDateTime
    {
        public FrenchRepublicanDayOfDecade? DayOfDecade { get; }
        public FrenchRepublicanDayOfSansCulottide? DayOfSansCulottide { get; }
        public int? Decade { get; }
        public FrenchRepublicanMonth Month { get; }
        public int Year { get; }

        public int Hour { get; }
        public int Minute { get; }
        public int Second { get; }
        public int Milliseconds { get; }
        public long Ticks { get; }

        public FrenchRepublicanDateTime(DateTime dateTime)
        {
            var jd = dateTime.ToJulianDate();
            var d = Fourmilab.Calendar.jd_to_french_revolutionary(jd);
            Year = (int)d[0];
            Month = (FrenchRepublicanMonth)d[1];
            if (Month == FrenchRepublicanMonth.SansColuttides)
                DayOfSansCulottide = (FrenchRepublicanDayOfSansCulottide)d[3];
            else
            {
                Decade = (int)d[2];
                DayOfDecade = (FrenchRepublicanDayOfDecade)d[3];
            }
        }
    }
}
