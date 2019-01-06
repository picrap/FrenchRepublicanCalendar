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
    using Utility;

    public struct FrenchRepublicanDateTime : IFormattable
    {
        public FrenchRepublicanDayOfDecade? DayOfDecade => Month != FrenchRepublicanMonth.SansColuttides ? (FrenchRepublicanDayOfDecade?)(DayOfMonth % 10) : null;
        public FrenchRepublicanDayOfSansCulottide? DayOfSansCulottide => Month == FrenchRepublicanMonth.SansColuttides ? (FrenchRepublicanDayOfSansCulottide?)DayOfMonth : null;
        public byte DayOfMonth { get; }
        public int Decade => DayOfMonth / 10 + 1;

        private readonly byte _month;
        public FrenchRepublicanMonth Month => (FrenchRepublicanMonth)_month;

        public short Year { get; }

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
            Year = (short)d[0];
            _month = (byte)d[1];
            DayOfMonth = (byte)(d[3] + (d[2] - 1) * 10);
            DateTime = dateTime;
        }

        public FrenchRepublicanDateTime(int year, FrenchRepublicanMonth month, int dayOfMonth,
            int hour = 0, int minute = 0, int second = 0, int millisecond = 0, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            Year = (short)year;
            _month = (byte)month;
            DayOfMonth = (byte)dayOfMonth;
            var jd = Fourmilab.Calendar.french_revolutionary_to_jd(year, (int)month, dayOfMonth / 10 + 1, DayOfMonth % 10);
            DateTime = Add(DateTimeUtility.FromJulianDate(jd), hour, minute, second, millisecond, kind);
        }

        public FrenchRepublicanDateTime(int year, FrenchRepublicanMonth month, int decade, FrenchRepublicanDayOfDecade dayOfDecade,
            int hour = 0, int minute = 0, int second = 0, int millisecond = 0, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            Year = (short)year;
            _month = (byte)month;
            DayOfMonth = (byte)((int)dayOfDecade + (decade - 1) * 10);
            var jd = Fourmilab.Calendar.french_revolutionary_to_jd(year, (int)month, decade, (int)dayOfDecade);
            DateTime = Add(DateTimeUtility.FromJulianDate(jd), hour, minute, second, millisecond, kind);
        }

        public FrenchRepublicanDateTime(int year, FrenchRepublicanMonth month, FrenchRepublicanDayOfSansCulottide dayOfSansCulottide,
            int hour = 0, int minute = 0, int second = 0, int millisecond = 0, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            Year = (short)year;
            _month = (byte)month;
            DayOfMonth = (byte)dayOfSansCulottide;
            var jd = Fourmilab.Calendar.french_revolutionary_to_jd(year, 13, 0, (int)dayOfSansCulottide);
            DateTime = Add(DateTimeUtility.FromJulianDate(jd), hour, minute, second, millisecond, kind);
        }

        private static DateTime Add(DateTime dateTime, int hour = 0, int minute = 0, int second = 0, int millisecond = 0,
            DateTimeKind kind = DateTimeKind.Unspecified)
        {
            return dateTime + new TimeSpan(hour, minute, second, millisecond);
        }

        public override string ToString() => ToString("G", CultureInfo.CurrentCulture);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.datetime.tostring?view=netframework-4.7.2#System_DateTime_ToString
            if (format == "d") return ToString("dd/MM/yyyy", formatProvider);
            if (format == "D") return ToString("dddd d MMMM yyyy", formatProvider);
            if (format == "f") return ToString("dddd d MMMM yyyy hh:mm", formatProvider);
            if (format == "F") return ToString("dddd d MMMM yyyy hh:mm:ss", formatProvider);
            if (format == "G") return ToString("dd/MM/yyyy hh:mm:ss", formatProvider);
            if (format == "g") return ToString("dd/MM/yyyy hh:mm", formatProvider);
            if (format == "m") return ToString("d MMMM", formatProvider);

            // https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings?view=netframework-4.7.2
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

        public FrenchRepublicanDateTime AddDays(int days)
        {
            return new FrenchRepublicanDateTime(DateTime + TimeSpan.FromDays(days));
        }

        public FrenchRepublicanDateTime AddMonths(int months)
        {
            var newMonth = (int)(Month - 1) + months;
            var newYear = Year + newMonth / 13;
            newMonth %= 13;
            var maxDayOfMonth = 30;
            if (newMonth == 12) // sans-culottides
                maxDayOfMonth = IsLeapYear(newYear) ? 6 : 5;

            var newDayOfMonth = Math.Min(DayOfMonth, maxDayOfMonth);
            return new FrenchRepublicanDateTime(newYear, (FrenchRepublicanMonth)(newMonth + 1), newDayOfMonth, Hour, Minute, Second, Millisecond, Kind);
        }

        public static bool IsLeapYear(int year)
        {
            // this is not something I'm proud of
            var lastDay = new FrenchRepublicanDateTime(year, FrenchRepublicanMonth.SansColuttides, 6);
            var dateTime = lastDay.DateTime;
            var frDateTime = new FrenchRepublicanDateTime(dateTime);
            return frDateTime.Year == year;
        }
    }
}
