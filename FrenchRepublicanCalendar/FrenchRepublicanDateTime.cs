#region Arx One

// Arx One
// The ass kicking online backup
// (c) Arx One 2009-2018

#endregion

namespace FrenchRepublicanCalendar
{
    using System;
    using System.Globalization;
    using System.Text;

    public class FrenchRepublicanDateTime : IFormattable
    {
        public FrenchRepublicanDayOfDecade? DayOfDecade { get; }
        public FrenchRepublicanDayOfSansCulottide? DayOfSansCulottide { get; }

        public int DayOfMonth
        {
            get
            {
                if (DayOfSansCulottide.HasValue)
                    return (int)DayOfSansCulottide.Value;
                return (Decade.Value - 1) * 10 + (int)DayOfDecade.Value;
            }
        }

        public int? Decade { get; }
        public FrenchRepublicanMonth Month { get; }
        public int Year { get; }

        public int Hour => DateTime.Hour;
        public int Minute => DateTime.Minute;
        public int Second => DateTime.Second;
        public int Millisecond => DateTime.Millisecond;
        public long Ticks => DateTime.Ticks;
        public DateTimeKind Kind => DateTime.Kind;
        public DateTime DateTime { get; }

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
            DateTime = dateTime;
        }

        public FrenchRepublicanDateTime(int year, FrenchRepublicanMonth month, int dayOfMonth)
        {
            Year = year;
            Month = month;
            double jd;
            if (month == FrenchRepublicanMonth.SansColuttides)
            {
                DayOfSansCulottide = (FrenchRepublicanDayOfSansCulottide?)dayOfMonth;
                jd = Fourmilab.Calendar.french_revolutionary_to_jd(year, (int)month, 0, (int)DayOfSansCulottide.Value);
            }
            else
            {
                Decade = dayOfMonth / 10 + 1;
                DayOfDecade = (FrenchRepublicanDayOfDecade)(dayOfMonth % 10);
                jd = Fourmilab.Calendar.french_revolutionary_to_jd(year, (int)month, Decade.Value, (int)DayOfDecade.Value);
            }
            DateTime = DateTimeUtility.FromJulianDate(jd);
        }

        public FrenchRepublicanDateTime(int year, FrenchRepublicanMonth month, int decade, FrenchRepublicanDayOfDecade dayOfDecade)
        {
            Year = year;
            Month = month;
            Decade = decade;
            DayOfDecade = dayOfDecade;
            var jd = Fourmilab.Calendar.french_revolutionary_to_jd(year, (int)month, decade, (int)dayOfDecade);
            DateTime = DateTimeUtility.FromJulianDate(jd);
        }

        public FrenchRepublicanDateTime(int year, FrenchRepublicanMonth month, FrenchRepublicanDayOfSansCulottide dayOfSansCulottide)
        {
            Year = year;
            Month = month;
            DayOfSansCulottide = dayOfSansCulottide;
            var jd = Fourmilab.Calendar.french_revolutionary_to_jd(year, 13, 0, (int)dayOfSansCulottide);
            DateTime = DateTimeUtility.FromJulianDate(jd);
        }

        public override string ToString() => ToString("G", CultureInfo.CurrentCulture);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "d") return ToString("dd/MM/yyyy", formatProvider);
            if (format == "D") return ToString("dddd d MMMM yyyy", formatProvider);
            if (format == "f") return ToString("dddd d MMMM yyyy hh:mm", formatProvider);
            if (format == "F") return ToString("dddd d MMMM yyyy hh:mm:ss", formatProvider);
            if (format == "G") return ToString("dd/MM/yyyy hh:mm:ss", formatProvider);
            if (format == "g") return ToString("dd/MM/yyyy hh:mm", formatProvider);
            if (format == "m") return ToString("d MMMM", formatProvider);

            var stringBuilder = new StringBuilder();
            for (int index = 0; index < format.Length;)
            {
                // escape comes first
                if (format[index] == '\\')
                {
                    stringBuilder.Append(format[++index]);
                    index++;
                }
                else if (Capture(format, "yyyyy", ref index)) stringBuilder.AppendFormat("{0:D5}", Year);
                else if (Capture(format, "yyyy", ref index)) stringBuilder.AppendFormat("{0:D4}", Year);
                else if (Capture(format, "yyy", ref index)) stringBuilder.AppendFormat("{0:D3}", Year);
                else if (Capture(format, "yy", ref index)) stringBuilder.AppendFormat("{0:D2}", Year % 100);
                else if (Capture(format, "y", ref index)) stringBuilder.Append(Year % 100);
                else if (Capture(format, "MMMM", ref index)) stringBuilder.Append(Month.ToString());
                else if (Capture(format, "MMM", ref index)) stringBuilder.Append(Month.ToString());
                else if (Capture(format, "MM", ref index)) stringBuilder.AppendFormat("{0:D2}", (int)Month);
                else if (Capture(format, "M", ref index)) stringBuilder.Append((int)Month);
                else if (Capture(format, "dddd", ref index)) stringBuilder.Append(DayOfSansCulottide.HasValue ? DayOfSansCulottide.ToString() : DayOfDecade.ToString());
                else if (Capture(format, "ddd", ref index)) stringBuilder.Append(DayOfSansCulottide.HasValue ? DayOfSansCulottide.ToString() : DayOfDecade.ToString());
                else if (Capture(format, "dd", ref index)) stringBuilder.AppendFormat("{0:D2}", DayOfMonth);
                else if (Capture(format, "d", ref index)) stringBuilder.Append(DayOfMonth);
                else stringBuilder.Append(format[index++]);
            }
            return stringBuilder.ToString();
        }

        public bool Capture(string format, string capture, ref int index)
        {
            if (index + capture.Length >= format.Length)
                return false;
            if (format.Substring(index, capture.Length) == capture)
            {
                index += capture.Length;
                return true;
            }

            return false;
        }
    }
}
