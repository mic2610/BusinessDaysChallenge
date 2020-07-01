using System;
using System.Collections.Generic;
using DesignCrowdTC.Business.Models;

namespace DesignCrowdTC.Business.Utilities
{
    public interface IBusinessDayCounter
    {
        int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate);

        int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHoliday> publicHolidays);
    }
}