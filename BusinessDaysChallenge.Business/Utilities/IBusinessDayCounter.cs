using System;
using System.Collections.Generic;
using BusinessDaysChallenge.Business.Models;

namespace BusinessDaysChallenge.Business.Utilities
{
    public interface IBusinessDayCounter
    {
        int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate);

        int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHoliday> publicHolidays);
    }
}