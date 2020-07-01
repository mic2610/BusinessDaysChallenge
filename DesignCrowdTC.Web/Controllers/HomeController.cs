using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DesignCrowdTC.Business.Models;
using DesignCrowdTC.Business.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DesignCrowdTC.Web.Models;

namespace DesignCrowdTC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusinessDayCounter _businessDayCounter;

        public HomeController(ILogger<HomeController> logger, IBusinessDayCounter businessDayCounter)
        {
            _logger = logger;
            _businessDayCounter = businessDayCounter;
        }

        public IActionResult Index()
        {
            // TASK 1: WeekdaysBetweenTwoDates
            var startDate = new DateTime(2013, 10, 7);
            var endDate = new DateTime(2013, 10, 9);
            var weekdaysBetweenTwoDates = _businessDayCounter.WeekdaysBetweenTwoDates(startDate, endDate);

            var startDate3 = new DateTime(2013, 10, 7);
            var endDate3 = new DateTime(2014, 1, 1);
            var weekdaysBetweenTwoDates3 = _businessDayCounter.WeekdaysBetweenTwoDates(startDate3, endDate3);

            var startDate4 = new DateTime(2013, 10, 7);
            var endDate4 = new DateTime(2013, 10, 5);
            var weekdaysBetweenTwoDates4 = _businessDayCounter.WeekdaysBetweenTwoDates(startDate4, endDate4);

            var weekdays = new[]
            {
                new DaysModel { DaysBetween = weekdaysBetweenTwoDates, StartDate = startDate, EndDate = endDate},
                new DaysModel { DaysBetween = weekdaysBetweenTwoDates3, StartDate = startDate3, EndDate = endDate3},
                new DaysModel { DaysBetween = weekdaysBetweenTwoDates4, StartDate = startDate4, EndDate = endDate4},
            };

            // TASK 2: BusinessDaysBetweenTwoDates
            var publicHolidays = new[] { new PublicHoliday { OriginalDate = new DateTime(2013, 12, 25) }, new PublicHoliday { OriginalDate = new DateTime(2013, 12, 26) }, new PublicHoliday { OriginalDate = new DateTime(2014, 1, 1) } };

            var startDate5 = new DateTime(2013, 10, 7);
            var endDate5 = new DateTime(2013, 10, 9);
            var businessDaysBetweenTwoDates5 = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate5, endDate5, publicHolidays);

            var startDate6 = new DateTime(2013, 12, 24);
            var endDate6 = new DateTime(2013, 12, 27);
            var businessDaysBetweenTwoDates6 = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate6, endDate6, publicHolidays);

            var startDate7 = new DateTime(2013, 10, 7);
            var endDate7 = new DateTime(2014, 1, 1);
            var businessDaysBetweenTwoDates7 = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate7, endDate7, publicHolidays);

            var businessDays = new[]
            {
                new DaysModel { DaysBetween = businessDaysBetweenTwoDates5, StartDate = startDate5, EndDate = endDate5 },
                new DaysModel { DaysBetween = businessDaysBetweenTwoDates6, StartDate = startDate6, EndDate = endDate6 },
                new DaysModel { DaysBetween = businessDaysBetweenTwoDates7, StartDate = startDate7, EndDate = endDate7 }
            };

            // TASK 3: Public holidays
            // queensBirthday
            var queensBirthday = new PublicHoliday
            {
                OriginalDate = new DateTime(2013, 6, 1),
                Name = "Queen's Birthday",
                Rule = PublicHolidayRule.CertainOccurrence,
                Occurence = new CertainOccurence { DayOccurence = 2, DayOfWeek = DayOfWeek.Monday }
            };

            var customPublicHolidays = new[] { queensBirthday };

            // Thursday
            var startDate8 = new DateTime(2013, 5, 28);
            var endDate8 = new DateTime(2013, 6, 14);
            var businessDaysBetweenTwoDates8 = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate8, endDate8, customPublicHolidays);


            // Anzac Day
            var anzacDay = new PublicHoliday { OriginalDate = new DateTime(2013, 4, 25), Name = "Anzac Day", Rule = PublicHolidayRule.Yearly };
            var customPublicHolidays2 = new[] { anzacDay };
            var startDate9 = new DateTime(2013, 4, 20);
            var endDate9 = new DateTime(2013, 5, 1);
            var businessDaysBetweenTwoDates9 = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate9, endDate9, customPublicHolidays2);


            // Monday 3rd December 2022 is the public holiday
            var newYearsEve = new PublicHoliday { OriginalDate = new DateTime(2022, 1, 1), Name = "New Years Eve", Rule = PublicHolidayRule.YearlyWeekdayOnly };
            var publicHolidays3 = new[] { newYearsEve };

            // Thursday
            var startDate10 = new DateTime(2021, 12, 30);
            var endDate10 = new DateTime(2022, 1, 5);
            var businessDaysBetweenTwoDates10 = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate10, endDate10, publicHolidays3);

            var publicHolidaysDaysBetween = new[]
            {
                new DaysModel { DaysBetween = businessDaysBetweenTwoDates8, StartDate = startDate8, EndDate = endDate8, PublicHoliday = queensBirthday },
                new DaysModel { DaysBetween = businessDaysBetweenTwoDates9, StartDate = startDate9, EndDate = endDate9, PublicHoliday = anzacDay },
                new DaysModel { DaysBetween = businessDaysBetweenTwoDates10, StartDate = startDate10, EndDate = endDate10, PublicHoliday = newYearsEve }
            };

            return View(new DaysCollectionViewModel
            {
                WeekdaysBetween = weekdays, BusinessDaysBetween = businessDays, PublicHolidaysDaysBetween = publicHolidaysDaysBetween
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
