﻿#region Arx One

// Arx One
// The ass kicking online backup
// (c) Arx One 2009-2018

#endregion

namespace FrenchRepublicanCalendar
{
    using System;
    using System.Globalization;
    using System.Text;
    using RomanNumerals;
    using RomanNumerals.Numerals;
    using Utility;

    /// <summary>
    /// Specific implementation of <see cref="DateTime"/> for French republican calendar
    /// Adds some extra properties
    /// </summary>
    public struct FrenchRepublicanDateTime : IFormattable
    {
        /// <summary>
        /// Gets the day of decade
        /// </summary>
        /// <remarks>
        /// The standard week has 10 days, it is named a "décade" (french term), each month is exactly 30 days, so 3 decades
        /// </remarks>
        /// <returns>A day of decade of null for 13th month</returns>
        public FrenchRepublicanDayOfDecade? DayOfDecade => Month != FrenchRepublicanMonth.SansColuttides ? (FrenchRepublicanDayOfDecade?)(DayOfMonth % 10) : null;
        /// <summary>
        /// Gets the day of sans-coluttide
        /// </summary>
        /// <remarks>
        /// Sans-coluttides are 5 or 6 days added after the 360 regular days.
        /// They're here to fill the year and ensure that 1er Vendémiaire happens on autumnal equinoxe
        /// </remarks>
        /// <returns>A day of null when not in 13th month</returns>
        public FrenchRepublicanDayOfSansCulottide? DayOfSansCulottide => Month == FrenchRepublicanMonth.SansColuttides ? (FrenchRepublicanDayOfSansCulottide?)DayOfMonth : null;

        private readonly byte _dayOfMonth;

        /// <summary>
        /// Get the day of month, in a numeric form (1 to 30)
        /// </summary>
        public int DayOfMonth => _dayOfMonth;

        /// <summary>
        /// Gets the decade (10-days week) of month (1 to 3)
        /// </summary>
        public int Decade => _dayOfMonth / 10 + 1;

        private readonly byte _month;

        /// <summary>
        /// Gets the month, as an enumerated value (1 to 13)
        /// </summary>
        public FrenchRepublicanMonth Month => (FrenchRepublicanMonth)_month;

        private readonly short _year;

        /// <summary>
        /// Gets the year (year I starts in 1792 AD)
        /// </summary>
        public int Year => _year;

        /// <summary>
        /// Gets the hour (0-23)
        /// </summary>
        public int Hour => DateTime.Hour;
        /// <summary>
        /// Gets the minute (0-59)
        /// </summary>
        public int Minute => DateTime.Minute;
        /// <summary>
        /// Gets the second (0-60)
        /// </summary>
        public int Second => DateTime.Second;
        /// <summary>
        /// Gets the millisecond (0-999)
        /// </summary>
        public int Millisecond => DateTime.Millisecond;
        /// <summary>
        /// Gets the ticks
        /// </summary>
        public long Ticks => DateTime.Ticks;
        /// <summary>
        /// Get the kind
        /// </summary>
        public DateTimeKind Kind => DateTime.Kind;
        /// <summary>
        /// Gets the corresponding <see cref="DateTime"/>
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// Creates an instance of <see cref="FrenchRepublicanDateTime"/> using a <see cref="DateTime"/>
        /// </summary>
        /// <param name="dateTime"></param>
        public FrenchRepublicanDateTime(DateTime dateTime)
        {
            var jd = dateTime.ToJulianDate();
            var d = Fourmilab.Calendar.jd_to_french_revolutionary(jd);
            _year = (short)d[0];
            _month = (byte)d[1];
            _dayOfMonth = (byte)(d[3] + (d[2] - 1) * 10);
            DateTime = dateTime;
        }

        /// <summary>
        /// Creates an instance of <see cref="FrenchRepublicanDateTime"/>
        /// </summary>
        public FrenchRepublicanDateTime(int year, FrenchRepublicanMonth month, int dayOfMonth,
            int hour = 0, int minute = 0, int second = 0, int millisecond = 0, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            _year = (short)year;
            _month = (byte)month;
            _dayOfMonth = (byte)dayOfMonth;
            var jd = Fourmilab.Calendar.french_revolutionary_to_jd(year, (int)month, dayOfMonth / 10 + 1, _dayOfMonth % 10);
            DateTime = Add(DateTimeUtility.FromJulianDate(jd), hour, minute, second, millisecond, kind);
        }

        /// <summary>
        /// Creates an instance of <see cref="FrenchRepublicanDateTime"/>
        /// </summary>
        public FrenchRepublicanDateTime(int year, FrenchRepublicanMonth month, int decade, FrenchRepublicanDayOfDecade dayOfDecade,
            int hour = 0, int minute = 0, int second = 0, int millisecond = 0, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            _year = (short)year;
            _month = (byte)month;
            _dayOfMonth = (byte)((int)dayOfDecade + (decade - 1) * 10);
            var jd = Fourmilab.Calendar.french_revolutionary_to_jd(year, (int)month, decade, (int)dayOfDecade);
            DateTime = Add(DateTimeUtility.FromJulianDate(jd), hour, minute, second, millisecond, kind);
        }

        /// <summary>
        /// Creates an instance of <see cref="FrenchRepublicanDateTime"/>
        /// </summary>
        public FrenchRepublicanDateTime(int year, FrenchRepublicanMonth month, FrenchRepublicanDayOfSansCulottide dayOfSansCulottide,
            int hour = 0, int minute = 0, int second = 0, int millisecond = 0, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            _year = (short)year;
            _month = (byte)month;
            _dayOfMonth = (byte)dayOfSansCulottide;
            var jd = Fourmilab.Calendar.french_revolutionary_to_jd(year, 13, 0, (int)dayOfSansCulottide);
            DateTime = Add(DateTimeUtility.FromJulianDate(jd), hour, minute, second, millisecond, kind);
        }

        /// <summary>
        /// Adds hours, minutes, seconds, milliseconds to given <see cref="DateTime"/>
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <param name="millisecond"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        private static DateTime Add(DateTime dateTime, int hour = 0, int minute = 0, int second = 0, int millisecond = 0,
            DateTimeKind kind = DateTimeKind.Unspecified)
        {
            return dateTime + new TimeSpan(hour, minute, second, millisecond);
        }

        /// <summary>
        /// Returns a literal representation of date
        /// </summary>
        /// <returns></returns>
        public override string ToString() => ToString("G", CultureInfo.CurrentCulture);

        /// <summary>
        /// Converts date to literal representation.
        /// The supported formats are the same as <see cref="DateTime"/>
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (formatProvider is null)
                formatProvider = CultureInfo.CurrentCulture;

            // https://docs.microsoft.com/en-us/dotnet/api/system.datetime.tostring?view=netframework-4.7.2#System_DateTime_ToString
            if (format == "d") return ToString("dd/MM/yyyy", formatProvider);
            if (format == "D") return ToString("dddd d MMMM yyyy", formatProvider);
            if (format == "f") return ToString("dddd d MMMM yyyy hh:mm tt", formatProvider);
            if (format == "F") return ToString("dddd d MMMM yyyy hh:mm:ss tt", formatProvider);
            //if (format == "g") return ToString("dd/MM/yyyy hh:mm tt", formatProvider);
            //if (format == "G") return ToString("dd/MM/yyyy hh:mm:ss tt", formatProvider);
            if (format == "g") return ToString(@"dddd, dd MMMM \a\n yyy, HH:mm", formatProvider);
            if (format == "G") return ToString(@"dddd, dd MMMM \a\n yyy, HH:mm:ss", formatProvider);
            if (format == "m") return ToString("d MMMM", formatProvider);
            if (format == "o") return ToString("yyyy-MM-ddTHH:mm:ss.fffffff", formatProvider);
            if (format == "R") return ToString("ddd, dd MMM yyyy HH:mm:ss", formatProvider); // Sun, 15 Jun 2008 21:15:07 GMT
            if (format == "s") return ToString("yyyy-MM-ddTHH:mm:ss", formatProvider);
            if (format == "t") return ToString("h:mm tt", formatProvider);
            if (format == "u") return ToString("yyyy-MM-dd HH:mm:ss z", formatProvider);
            if (format == "U") return ToString("dddd d MMMM yyyy hh:mm:ss tt", formatProvider);
            if (format == "y") return ToString("MMMM, yyyy", formatProvider);

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
                else if (Capture(format, "Y", ref index)) stringBuilder.Append(((uint)Year).ToRomanNumerals());
                else if (Capture(format, "YU", ref index)) stringBuilder.Append(((uint)Year).ToRomanNumerals(NumeralFlags.Unicode));
                else if (Capture(format, "yyyyy", ref index)) stringBuilder.AppendFormat("{0:D5}", Year);
                else if (Capture(format, "yyyy", ref index)) stringBuilder.AppendFormat("{0:D4}", Year);
                else if (Capture(format, "yyy", ref index)) stringBuilder.AppendFormat("{0:D3}", Year);
                else if (Capture(format, "yy", ref index)) stringBuilder.AppendFormat("{0:D2}", Year % 100);
                else if (Capture(format, "y", ref index)) stringBuilder.Append(Year % 100);
                else if (Capture(format, "MMMM", ref index)) stringBuilder.Append(GetLiteral(Month));
                else if (Capture(format, "MMM", ref index)) stringBuilder.Append(GetShortLiteral(Month));
                else if (Capture(format, "MM", ref index)) stringBuilder.AppendFormat("{0:D2}", (int)Month);
                else if (Capture(format, "M", ref index)) stringBuilder.Append((int)Month);
                else if (Capture(format, "dddd", ref index)) stringBuilder.Append(DayOfSansCulottide.HasValue ? GetLiteral(DayOfSansCulottide.Value) : GetLiteral(DayOfDecade.Value));
                else if (Capture(format, "ddd", ref index)) stringBuilder.Append(DayOfSansCulottide.HasValue ? GetShortLiteral(DayOfSansCulottide.Value) : GetShortLiteral(DayOfDecade.Value));
                else if (Capture(format, "dd", ref index)) stringBuilder.AppendFormat("{0:D2}", DayOfMonth);
                else if (Capture(format, "d", ref index)) stringBuilder.Append(DayOfMonth);
                else if (CaptureAny(format, ref index, out var captured,
                    "fffffff", "ffffff", "fffff", "ffff", "fff", "ff", "f",
                    "FFFFFFF", "FFFFFF", "FFFFF", "FFFF", "FFF", "FF", "F",
                    "hh", "h", "HH", "H",
                    "K",
                    "mm", "m",
                    "ss", "s",
                    "tt", "t",
                    "zzz", "zz", "z"))
                    stringBuilder.Append(DateTime.ToString(" " + captured).Substring(1));
                else stringBuilder.Append(format[index++]);
            }
            return stringBuilder.ToString();
        }

        private static string GetLiteral(FrenchRepublicanMonth month)
        {
            return month.ToString();
        }

        private static string GetLiteral(FrenchRepublicanDayOfDecade dayOfDecade)
        {
            return dayOfDecade.ToString();
        }

        private static string GetLiteral(FrenchRepublicanDayOfSansCulottide dayOfSansCulottide)
        {
            return dayOfSansCulottide.ToString();
        }

        private static string GetShortLiteral(FrenchRepublicanMonth month)
        {
            return GetLiteral(month).Substring(0, 3);
        }

        private static string GetShortLiteral(FrenchRepublicanDayOfDecade dayOfDecade)
        {
            return GetLiteral(dayOfDecade).Substring(0, 3);
        }

        private static string GetShortLiteral(FrenchRepublicanDayOfSansCulottide dayOfSansCulottide)
        {
            return GetLiteral(dayOfSansCulottide).Substring(0, 3);
        }

        private static bool CaptureAny(string format, ref int index, out string captured, params string[] captures)
        {
            foreach (var capture in captures)
            {
                if (Capture(format, capture, ref index))
                {
                    captured = capture;
                    return true;
                }
            }

            captured = null;
            return false;
        }

        private static bool Capture(string format, string capture, ref int index)
        {
            if (index + capture.Length > format.Length)
                return false;
            if (format.Substring(index, capture.Length) == capture)
            {
                index += capture.Length;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds days to this <see cref="FrenchRepublicanDateTime"/> and returns a new <see cref="FrenchRepublicanDateTime"/>
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public FrenchRepublicanDateTime AddDays(int days)
        {
            return new FrenchRepublicanDateTime(DateTime + TimeSpan.FromDays(days));
        }

        /// <summary>
        /// Adds months to this <see cref="FrenchRepublicanDateTime"/> and returns a new <see cref="FrenchRepublicanDateTime"/>
        /// </summary>
        /// <param name="months"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Tells if the given republican year is a leap year
        /// (in which case it has a 6th sans-culottide day)
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
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
