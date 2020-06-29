using System;
using System.Collections.Generic;

namespace DesignCrowdTC.Web.Utilities
{
    public class BusinessDayCounter
    {
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            // firstDate.DayOfWeek == DayOfWeek.Friday
            var dates = 0;
            if (secondDate <= firstDate)
                return dates;

            //for (int i = 0; i < UPPER; i++)
            //{
                
            //}

            return dates;
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime>
            publicHolidays)
        {
            //todo
            return 0;
        }
    }
}
