using System;
using System.Collections.Generic;
using DesignCrowdTC.Core.Extensions;

namespace DesignCrowdTC.Business.Utilities
{
    public class BusinessDayCounter
    {
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, Predicate<DateTime> condition = null)
        {
            // TODO: Do unit tests
            var days = 0;
            if (secondDate <= firstDate)
                return days;

            // Increment days as long as it's a weekend
            for (var date = firstDate.AddDays(1); date < secondDate; date = date.AddDays(1))
            {
                // Post increment dates to only count the dates in between
                if (date.IsWeekDay() && (condition == null || condition(date)))
                    days++;
            }

            return days;
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            var publicHolidaysLookup = publicHolidays.SafeToDictionary(ph => ph.ToShortDateString());
            return WeekdaysBetweenTwoDates(firstDate, secondDate, time => !publicHolidaysLookup.ContainsKey(time.ToShortDateString()));
        }
    }
}
