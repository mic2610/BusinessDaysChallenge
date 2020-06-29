using System;
using DesignCrowdTC.App.Utilities;

namespace DesignCrowdTC.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var startDate = new DateTime(2013, 10, 7);
            var endDate = new DateTime(2013, 10, 9);
            var businessDayCounter = new BusinessDayCounter();
            var weekdaysBetweenTwoDates = businessDayCounter.WeekdaysBetweenTwoDates(startDate, endDate);
            Console.WriteLine($"Weekdays in between {startDate.ToShortDateString()} and {endDate.ToShortDateString()} is {weekdaysBetweenTwoDates} weekdays");
            Console.Read();


            var startDate2 = new DateTime(2013, 10, 5);
            var endDate2 = new DateTime(2013, 10, 14);
            var weekdaysBetweenTwoDates2 = businessDayCounter.WeekdaysBetweenTwoDates(startDate2, endDate2);
            Console.WriteLine($"Weekdays in between {startDate2.ToShortDateString()} and {endDate2.ToShortDateString()} is {weekdaysBetweenTwoDates2} weekdays");
            Console.Read();


            var startDate3 = new DateTime(2013, 10, 7);
            var endDate3 = new DateTime(2014, 1, 1);
            var weekdaysBetweenTwoDates3 = businessDayCounter.WeekdaysBetweenTwoDates(startDate3, endDate3);
            Console.WriteLine($"Weekdays in between {startDate3.ToShortDateString()} and {endDate3.ToShortDateString()} is {weekdaysBetweenTwoDates3} weekdays");
            Console.Read();


            var startDate4 = new DateTime(2013, 10, 7);
            var endDate4 = new DateTime(2013, 10, 5);
            var weekdaysBetweenTwoDates4 = businessDayCounter.WeekdaysBetweenTwoDates(startDate4, endDate4);
            Console.WriteLine($"Weekdays in between {startDate4.ToShortDateString()} and {endDate4.ToShortDateString()} is {weekdaysBetweenTwoDates4} weekdays");
            Console.Read();
        }
    }
}
