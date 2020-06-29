using System;
using System.Collections.Generic;
using DesignCrowdTC.Core.Extensions;

namespace DesignCrowdTC.App.Utilities
{
    public class BusinessDayCounter
    {
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IDictionary<string, DateTime> publicHolidaysLookup = null)
        {
            // TODO: Do unit tests
            var days = 0;
            if (secondDate <= firstDate)
                return days;

            // Increment days as long as it's a weekend
            for (var date = firstDate.AddDays(1); date < secondDate; date = date.AddDays(1))
            {
                // Post increment dates to only count the dates in between
                if (date.IsWeekDay() && (publicHolidaysLookup == null || !publicHolidaysLookup.ContainsKey(date.ToShortDateString())))
                    days++;
            }

            return days;
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            return WeekdaysBetweenTwoDates(firstDate, secondDate, publicHolidays.SafeToDictionary(ph => ph.ToShortDateString()));
        }
    }
}
