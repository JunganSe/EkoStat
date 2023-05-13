using EkoStatLibrary.Models;

namespace EkoStatLibrary.Helpers;

public class TimeHelper
{
    public List<TimePeriod> GetTimePeriodsByWeek(DateTime start, DateTime end, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
    {
        var timePeriods = new List<TimePeriod>();
        var startDate = DateOnly.FromDateTime(start);
        var endDate = DateOnly.FromDateTime(end);

        var weekStartDate = GetFirstDayOfWeekOnOrAfterDate(startDate, firstDayOfWeek);

        if (startDate < weekStartDate)
            timePeriods.Add(new TimePeriod(startDate, weekStartDate));

        while (weekStartDate.AddDays(7) <= endDate)
        {
            timePeriods.Add(new TimePeriod(weekStartDate, weekStartDate.AddDays(7)));
            weekStartDate = weekStartDate.AddDays(7);
        }

        if (weekStartDate < endDate)
            timePeriods.Add(new TimePeriod(weekStartDate, endDate));

        return timePeriods;

        DateOnly GetFirstDayOfWeekOnOrAfterDate(DateOnly date, DayOfWeek firstDayOfWeek)
        {
            while (date.DayOfWeek != firstDayOfWeek)
                date = date.AddDays(1);
            return date;
        }
    }
}
