namespace BusinessDaysChallenge.Web.Models
{
    public class DaysCollectionViewModel
    {
        public DaysModel[] WeekdaysBetween { get; set; }

        public DaysModel[] BusinessDaysBetween { get; set; }

        public DaysModel[] PublicHolidaysDaysBetween { get; set; }
    }
}