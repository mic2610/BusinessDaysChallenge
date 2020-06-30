using System;
using DesignCrowdTC.Business.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignCrowdTC.Business.Tests.Utilities
{
    public class BusinessDayCounterTests
    {
        [TestClass]
        public class WeekdaysBetweenTwoDates
        {
            [TestMethod]
            public void ReturnsValidCount()
            {
                // Arrange
                var businessDayCounter = new BusinessDayCounter();
                var startDate = new DateTime(2013, 10, 7);
                var endDate = new DateTime(2014, 1, 1);

                // Act
                var weekdaysBetweenTwoDates = businessDayCounter.WeekdaysBetweenTwoDates(startDate, endDate);

                // Assert
                Assert.AreEqual(61, weekdaysBetweenTwoDates);
            }

            [TestMethod]
            public void ReturnsInvalidCount()
            {
                // Arrange
                var businessDayCounter = new BusinessDayCounter();
                var startDate = new DateTime(2013, 10, 7);
                var endDate = new DateTime(2013, 10, 5);

                // Act
                var weekdaysBetweenTwoDates = businessDayCounter.WeekdaysBetweenTwoDates(startDate, endDate);

                // Assert
                Assert.AreEqual(0, weekdaysBetweenTwoDates);
            }
        }

        [TestClass]
        public class BusinessDaysBetweenTwoDates
        {
            [TestMethod]
            public void ReturnsValidCount()
            {
                // Arrange
                var businessDayCounter = new BusinessDayCounter();
                var publicHolidays = new[] { new DateTime(2013, 12, 25), new DateTime(2013, 12, 26), new DateTime(2014, 1, 1) };
                var startDate = new DateTime(2013, 10, 7);
                var endDate = new DateTime(2013, 10, 9);

                // Act
                var businessDaysBetweenTwoDates = businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, publicHolidays);

                // Assert
                Assert.AreEqual(1, businessDaysBetweenTwoDates);
            }
        }
    }
}
