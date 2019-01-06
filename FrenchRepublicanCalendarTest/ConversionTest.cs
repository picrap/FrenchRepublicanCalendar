// It's the French republican calendar!
// https://github.com/picrap/FrenchRepublicanCalendar
// Released under MIT license

namespace FrenchRepublicanCalendarTest
{
    using System;
    using FrenchRepublicanCalendar;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ConversionTest
    {
        [TestMethod]
        public void Day1()
        {
            var d = new DateTime(1792, 9, 22);
            var frd = new FrenchRepublicanDateTime(d);
            Assert.AreEqual(1, frd.Decade);
            Assert.AreEqual(FrenchRepublicanDayOfDecade.Primidi, frd.DayOfDecade);
            Assert.AreEqual(FrenchRepublicanMonth.Vendémiaire, frd.Month);
            Assert.AreEqual(1, frd.Year);
        }

        [TestMethod]
        public void Day1Too()
        {
            var d = new DateTime(1792, 9, 22, 12, 0, 0);
            var frd = new FrenchRepublicanDateTime(d);
            Assert.AreEqual(1, frd.Decade);
            Assert.AreEqual(FrenchRepublicanDayOfDecade.Primidi, frd.DayOfDecade);
            Assert.AreEqual(FrenchRepublicanMonth.Vendémiaire, frd.Month);
            Assert.AreEqual(1, frd.Year);
        }

        [TestMethod]
        public void Day2()
        {
            var d = new DateTime(1792, 9, 23);
            var frd = new FrenchRepublicanDateTime(d);
            Assert.AreEqual(1, frd.Decade);
            Assert.AreEqual(FrenchRepublicanDayOfDecade.Duodi, frd.DayOfDecade);
            Assert.AreEqual(FrenchRepublicanMonth.Vendémiaire, frd.Month);
            Assert.AreEqual(1, frd.Year);
        }

        [TestMethod]
        public void Day2Too()
        {
            var d = new DateTime(1792, 9, 23, 12, 0, 0);
            var frd = new FrenchRepublicanDateTime(d);
            Assert.AreEqual(1, frd.Decade);
            Assert.AreEqual(FrenchRepublicanDayOfDecade.Duodi, frd.DayOfDecade);
            Assert.AreEqual(FrenchRepublicanMonth.Vendémiaire, frd.Month);
            Assert.AreEqual(1, frd.Year);
        }

        [TestMethod]
        public void MyBirthDay()
        {
            var d = new DateTime(1970, 7, 4);
            var frd = new FrenchRepublicanDateTime(d);
            Assert.AreEqual(2, frd.Decade);
            Assert.AreEqual(FrenchRepublicanDayOfDecade.Sextidi, frd.DayOfDecade);
            Assert.AreEqual(FrenchRepublicanMonth.Messidor, frd.Month);
            Assert.AreEqual(178, frd.Year);
        }

        [TestMethod]
        public void AllIsQuietOnNewYearsDay()
        {
            var d = new DateTime(2019, 1, 1, 12, 0, 0);
            var frd = new FrenchRepublicanDateTime(d);
            Assert.AreEqual(2, frd.Decade);
            Assert.AreEqual(FrenchRepublicanDayOfDecade.Duodi, frd.DayOfDecade);
            Assert.AreEqual(FrenchRepublicanMonth.Nivôse, frd.Month);
            Assert.AreEqual(227, frd.Year);
        }
    }
}