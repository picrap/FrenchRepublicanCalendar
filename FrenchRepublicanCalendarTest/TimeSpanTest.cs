// It's the French republican calendar!
// https://github.com/picrap/FrenchRepublicanCalendar
// Released under MIT license

namespace FrenchRepublicanCalendarTest
{
    using System;
    using FrenchRepublicanCalendar;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TimeSpanTest
    {
        [TestMethod]
        public void OneDayTest()
        {
            var ts = new FrenchRepublicanTimeSpan(TimeSpan.FromDays(1).Ticks);
            Assert.AreEqual(10d, ts.TotalHours);
        }
        [TestMethod]
        public void QuarterDayTest()
        {
            var ts = new FrenchRepublicanTimeSpan(TimeSpan.FromDays(0.25).Ticks);
            Assert.AreEqual(2, ts.Hours);
            Assert.AreEqual(50, ts.Minutes);
        }
    }
}