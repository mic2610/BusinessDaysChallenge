using System;
using DesignCrowdTC.Business.Models;
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
            public void ValidCount()
            {
                // Arrange
                var businessDayCounter = new BusinessDayCounter();
                var publicHolidays = new[] { new PublicHoliday { OriginalDate = new DateTime(2013, 12, 25)}, new PublicHoliday { OriginalDate = new DateTime(2013, 12, 26)}, new PublicHoliday { OriginalDate = new DateTime(2014, 1, 1) }};
                var startDate = new DateTime(2013, 10, 7);
                var endDate = new DateTime(2013, 10, 9);

                // Act
                var businessDaysBetweenTwoDates = businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, publicHolidays);

                // Assert
                Assert.AreEqual(1, businessDaysBetweenTwoDates);
            }

            [TestMethod]
            public void ReturnsValidYearlyCount()
            {
                // Arrange
                var businessDayCounter = new BusinessDayCounter();
                var publicHolidays = new[] { new PublicHoliday { OriginalDate = new DateTime(2013, 4, 25), Name = "Anzac Day", Rule = PublicHolidayRule.Yearly }};
                var startDate = new DateTime(2013, 4, 20);
                var endDate = new DateTime(2013, 5, 1);

                // Act
                var businessDaysBetweenTwoDates = businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, publicHolidays);

                // Assert
                Assert.AreEqual(6, businessDaysBetweenTwoDates);
            }

            [TestMethod]
            public void ReturnsValidYearlyWeekdayCount()
            {
                // Arrange
                // Monday 3rd December 2022 is the public holiday
                var businessDayCounter = new BusinessDayCounter();
                var publicHolidays = new[] { new PublicHoliday { OriginalDate = new DateTime(2022, 1, 1), Name = "New Years Eve", Rule = PublicHolidayRule.YearlyWeekdayOnly } };

                // Thursday
                var startDate = new DateTime(2021, 12, 30);
                var endDate = new DateTime(2022, 1, 5);

                // Act
                var businessDaysBetweenTwoDates = businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, publicHolidays);

                // Assert
                Assert.AreEqual(2, businessDaysBetweenTwoDates);
            }

            [TestMethod]
            public void ReturnsValidOccurrenceCount()
            {
                // Arrange
                var businessDayCounter = new BusinessDayCounter();
                var publicHolidays = new[]
                {
                    new PublicHoliday
                    {
                        OriginalDate = new DateTime(2013, 6, 1),
                        Name = "Queen's Birthday",
                        Rule = PublicHolidayRule.CertainOccurrence,
                        Occurence =  new CertainOccurence { DayOccurence = 2, DayOfWeek = DayOfWeek.Monday }
                    }
                };

                // Thursday
                var startDate = new DateTime(2013, 5, 28);
                var endDate = new DateTime(2013, 6, 14);

                // Act
                var businessDaysBetweenTwoDates = businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, publicHolidays);

                // Assert
                Assert.AreEqual(11, businessDaysBetweenTwoDates);
            }
        }
    }
}
