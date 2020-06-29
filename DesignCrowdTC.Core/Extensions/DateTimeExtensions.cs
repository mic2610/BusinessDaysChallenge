using System;

namespace DesignCrowdTC.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsWeekDay(this DateTime date) => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
    }
}