// It's the French republican calendar!
// https://github.com/picrap/FrenchRepublicanCalendar
// Released under MIT license

namespace FrenchRepublicanCalendar
{
    using System;

    /// <summary>
    /// A version of <see cref="TimeSpan"/> which shows republican hours, minutes, and seconds
    /// </summary>
    public struct FrenchRepublicanTimeSpan
    {
        /// <summary>
        /// Gets the ticks
        /// </summary>
        public long Ticks { get; }

        /// <summary>
        /// Gets the days
        /// </summary>
        public int Days => (int)TotalDays;

        /// <summary>
        ///     Gets the hour (0-9)
        /// </summary>
        public int Hours => (int)(TotalHours % 10);

        /// <summary>
        ///     Gets the minute (0-99)
        /// </summary>
        public int Minutes => (int)(TotalMinutes % 100);

        /// <summary>
        ///     Gets the second (0-99)
        /// </summary>
        public int Seconds => (int)(TotalSeconds % 100);

        /// <summary>
        ///     Gets the millisecond (0-999)
        /// </summary>
        public int Milliseconds => (int)(TotalMilliseconds % 1000);

        /// <summary>
        /// Gets the total number of days
        /// </summary>
        public double TotalDays => TotalHours / 10;

        /// <summary>
        /// Gets the total number of hours
        /// </summary>
        public double TotalHours => TotalMinutes / 100;

        /// <summary>
        /// Gets the total number of minutes
        /// </summary>
        public double TotalMinutes => TotalSeconds / 100;

        /// <summary>
        /// Gets the total number of seconds
        /// </summary>
        public double TotalSeconds => TotalMilliseconds / 1000;

        /// <summary>
        /// Gets the total number of milliseconds
        /// </summary>
        public double TotalMilliseconds => Ticks / 8640d;

        /// <summary>
        /// Creates a new instance of <see cref="FrenchRepublicanTimeSpan"/>
        /// </summary>
        /// <param name="ticks"></param>
        public FrenchRepublicanTimeSpan(long ticks)
        {
            Ticks = ticks;
        }
    }
}
