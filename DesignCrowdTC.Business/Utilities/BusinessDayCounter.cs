using System;
using System.Collections.Generic;
using DesignCrowdTC.Business.Models;
using DesignCrowdTC.Core.Extensions;

namespace DesignCrowdTC.Business.Utilities
{
    public class BusinessDayCounter
    {
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            return ValidDaysBetweenTwoDates(firstDate, secondDate);
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            var publicHolidaysLookup = publicHolidays.SafeToDictionary(ph => ph.ToShortDateString());
            return ValidDaysBetweenTwoDates(firstDate, secondDate, date => publicHolidaysLookup.ContainsKey(date.ToShortDateString()));
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHoliday> publicHolidays)
        {
            var publicHolidaysLookup = publicHolidays.SafeToDictionary(ph => ph.Date.ToShortDateString());
            return ValidDaysBetweenTwoDates(firstDate, secondDate, publicHolidayLookup: publicHolidaysLookup);
        }

        private int ValidDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, Predicate<DateTime> exclusionCondition = null, IDictionary<string, PublicHoliday> publicHolidayLookup = null)
        {
            var days = 0;
            if (secondDate <= firstDate)
                return days;

            // Increment days as long as it's a weekend
            for (var date = firstDate.AddDays(1); date < secondDate; date = date.AddDays(1))
            {
                if (exclusionCondition != null && exclusionCondition(date))
                    continue;

                if (IsPublicHoliday(publicHolidayLookup, date))
                    continue;

                // Post increment dates to only count the dates in between
                if (date.IsWeekDay())
                    days++;
            }

            return days;
        }

        private bool IsPublicHoliday(IDictionary<string, PublicHoliday> publicHolidayLookup, DateTime date)
        {
            if (publicHolidayLookup == null || !publicHolidayLookup.TryGetValue(date.ToShortDateString(), out PublicHoliday publicHoliday))
                return false;

            var isPublicHoliday = false;
            switch (publicHoliday.Rule)
            {
                case null:
                    isPublicHoliday = date == publicHoliday.Date;
                    break;
                case PublicHolidayRule.Yearly:
                    isPublicHoliday = date.Day == publicHoliday.Date.Day;
                    break;
                case PublicHolidayRule.YearlyWeekdayOnly:
                {
                    isPublicHoliday = date.Day == publicHoliday.Date.Day;
                    if (isPublicHoliday && !date.IsWeekDay())
                    {
                        publicHoliday.Date = publicHoliday.Date.AddDays(date.DayOfWeek == DayOfWeek.Saturday ? 2 : 1);
                        publicHolidayLookup[publicHoliday.Date.ToShortDateString()] = publicHoliday;
                        isPublicHoliday = false;
                    }

                    break;
                }
                case PublicHolidayRule.CertainOccurrence when publicHoliday.Occurence != null:
                {
                    var publicHolidayOccurence = new DateTime(publicHoliday.Date.Year, publicHoliday.Date.Month, 1);
                    while (publicHolidayOccurence.DayOfWeek != publicHoliday.Occurence.DayOfWeek)
                        publicHolidayOccurence = publicHolidayOccurence.AddDays(1);

                    switch (publicHoliday.Occurence.DayOccurence)
                    {
                        case 2:
                            publicHolidayOccurence = publicHolidayOccurence.AddDays(7);
                            break;
                        case 3:
                            publicHolidayOccurence = publicHolidayOccurence.AddDays(14);
                            break;
                        default:
                        {
                            if (publicHoliday.Occurence.DayOccurence >= 4)
                                publicHolidayOccurence = publicHolidayOccurence.AddDays(21);
                            break;
                        }
                    }

                    publicHolidayLookup[publicHolidayOccurence.Date.ToShortDateString()] = new PublicHoliday { Date = publicHolidayOccurence.Date, Name = publicHoliday.Name };
                    break;
                }
            }

            return isPublicHoliday;
        }
    }
}