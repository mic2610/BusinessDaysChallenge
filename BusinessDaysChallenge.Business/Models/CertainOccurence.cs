using System;

namespace BusinessDaysChallenge.Business.Models
{
    public class CertainOccurence
    {
        // E.g: Set to 2 for second Monday
        public int DayOccurence { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
    }
}