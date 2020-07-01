using System;
using DesignCrowdTC.Business.Models;

namespace DesignCrowdTC.Web.Models
{
    public class DaysModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int DaysBetween { get; set; }

        public PublicHoliday PublicHoliday { get; set; }
    }
}