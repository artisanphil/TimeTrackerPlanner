using System;
namespace TimeTrackerPlanerMVC.Models
{
    public static partial class DateTimeExtensions
    {
        public static DateTime FirstDayOfWeek(this DateTime dt)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return dt.AddDays(-diff).Date;
        }

        public static DateTime getDateByWeek(this int WeekList)
        {
            DateTime weekDate = DateTime.Now;
            if (WeekList >= 1)
            {
                weekDate = DateTime.Now.AddDays(WeekList * 7);
            }
            DateTime datePlanned = DateTimeExtensions.FirstDayOfWeek(weekDate);

            return datePlanned;
        }
    }
}
