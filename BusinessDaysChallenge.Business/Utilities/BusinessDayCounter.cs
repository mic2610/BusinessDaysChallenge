using System;
using System.Collections.Generic;
using BusinessDaysChallenge.Business.Models;
using BusinessDaysChallenge.Core.Extensions;

namespace BusinessDaysChallenge.Business.Utilities
{
    public class BusinessDayCounter : IBusinessDayCounter
    {
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            return ValidDaysBetweenTwoDates(firstDate, secondDate);
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHoliday> publicHolidays)
        {
            var publicHolidaysLookup = publicHolidays.SafeToDictionary(ph => ph.OriginalDate.ToShortDateString());
            return ValidDaysBetweenTwoDates(firstDate, secondDate, publicHolidaysLookup);
        }

        private int ValidDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IDictionary<string, PublicHoliday> publicHolidayLookup = null)
        {
            var days = 0;
            if (secondDate <= firstDate)
                return days;

            // Increment days as long as it's a weekday
            for (var date = firstDate.AddDays(1); date < secondDate; date = date.AddDays(1))
            {
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
                // Fallback to a comparison on a date if no rule is present
                case null:
                    isPublicHoliday = date == publicHoliday.OriginalDate || date.ToShortDateString() == publicHoliday.OriginalDate.ToShortDateString();
                    break;
                case PublicHolidayRule.Yearly:
                    isPublicHoliday = date.Day == publicHoliday.OriginalDate.Day;
                    break;
                case PublicHolidayRule.YearlyWeekdayOnly:
                {
                    isPublicHoliday = date.Day == publicHoliday.OriginalDate.Day;
                    if (isPublicHoliday && !date.IsWeekDay())
                    {
                        // Delay the public holiday to the first Monday if the currently selected public holiday falls on a Saturday or Sunday and then add back to the lookup
                        publicHoliday.OriginalDate = publicHoliday.OriginalDate.AddDays(date.DayOfWeek == DayOfWeek.Saturday ? 2 : 1);
                        publicHolidayLookup[publicHoliday.OriginalDate.ToShortDateString()] = publicHoliday;
                        isPublicHoliday = false;
                    }

                    break;
                }
                case PublicHolidayRule.CertainOccurrence when publicHoliday.Occurence != null:
                {
                    var publicHolidayOccurence = new DateTime(publicHoliday.OriginalDate.Year, publicHoliday.OriginalDate.Month, 1);
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

                    // Add new delayed public holiday to be processed on another date
                    publicHolidayLookup[publicHolidayOccurence.Date.ToShortDateString()] = new PublicHoliday { OriginalDate = publicHolidayOccurence.Date, Name = publicHoliday.Name };
                    break;
                }
            }

            return isPublicHoliday;
        }
    }
}