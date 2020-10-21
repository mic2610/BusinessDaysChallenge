using System;
using BusinessDaysChallenge.Business.Models;

namespace BusinessDaysChallenge.Web.Models
{
    public class DaysModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int DaysBetween { get; set; }

        public PublicHoliday PublicHoliday { get; set; }
    }
}