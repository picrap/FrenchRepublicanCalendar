namespace CalendarTest
{
    using FrenchRepublicanCalendar;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LeapTest
    {
        [TestMethod]
        public void I_IsNotLeap()
        {
            Assert.IsFalse(FrenchRepublicanDateTime.IsLeapYear(1));
        }
        [TestMethod]
        public void II_IsNotLeap()
        {
            Assert.IsFalse(FrenchRepublicanDateTime.IsLeapYear(2));
        }
        [TestMethod]
        public void III_IsLeap()
        {
            Assert.IsTrue(FrenchRepublicanDateTime.IsLeapYear(3));
        }
        [TestMethod]
        public void VII_IsLeap()
        {
            Assert.IsTrue(FrenchRepublicanDateTime.IsLeapYear(7));
        }
        [TestMethod]
        public void XI_IsLeap()
        {
            Assert.IsTrue(FrenchRepublicanDateTime.IsLeapYear(11));
        }
    }
}