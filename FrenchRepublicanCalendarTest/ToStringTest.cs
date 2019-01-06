// It's the French republican calendar!
// https://github.com/picrap/FrenchRepublicanCalendar
// Released under MIT license

namespace FrenchRepublicanCalendarTest
{
    using System;
    using FrenchRepublicanCalendar;
    using FrenchRepublicanCalendar.Utility;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ToStringTest
    {
        [TestMethod]
        public void DefaultTest()
        {
            var d = new FrenchRepublicanDateTime(new DateTime(2019, 1, 6, 15, 53, 12, DateTimeKind.Local));
            var l = d.ToString();
            Assert.AreEqual("Septidi, 17 Nivôse an ⅭⅭⅩⅩⅦ, 15:53:12", l);
        }

        [TestMethod]
        public void To_o_Test()
        {
            var d = new FrenchRepublicanDateTime(new DateTime(2019, 1, 6, 15, 53, 12, DateTimeKind.Local));
            var l = d.ToString("o");
            Assert.AreEqual("0227-04-17T15:53:12.0000000", l);
        }

        [TestMethod]
        public void To_MMM_Test()
        {
            var d = new FrenchRepublicanDateTime(new DateTime(1973, 9, 28, 13, 15, 0, DateTimeKind.Local));
            var l = d.ToString("MMM");
            Assert.AreEqual("Ven", l);
        }

        [TestMethod]
        public void To_ddd_Test()
        {
            var d = new FrenchRepublicanDateTime(new DateTime(1973, 9, 27, 13, 0, 0, DateTimeKind.Local));
            var l = d.ToString("ddd");
            Assert.AreEqual("Sex", l); // funny, isn't it?
        }
    }
}