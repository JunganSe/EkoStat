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

    public List<TimePeriod> GetTimePeriodsByMonth(DateTime start, DateTime end)
    {
        var timePeriods = new List<TimePeriod>();
        var startDate = DateOnly.FromDateTime(start);
        var endDate = DateOnly.FromDateTime(end);

        var monthStartDate = GetMonthStartOnOrAfterDate(startDate);

        if (startDate < monthStartDate)
            timePeriods.Add(new TimePeriod(startDate, monthStartDate));

        while (monthStartDate.AddMonths(1) <= endDate)
        {
            timePeriods.Add(new TimePeriod(monthStartDate, monthStartDate.AddMonths(1)));
            monthStartDate = monthStartDate.AddMonths(1);
        }

        if (monthStartDate < endDate)
            timePeriods.Add(new TimePeriod(monthStartDate, endDate));

        return timePeriods;

        DateOnly GetMonthStartOnOrAfterDate(DateOnly date)
        {
            if (date.Day > 1)
                date = date.AddMonths(1);
            return date.AddDays(1 - date.Day);
        }
    }

    public List<TimePeriod> GetTimePeriodsByYear(DateTime start, DateTime end)
    {
        var timePeriods = new List<TimePeriod>();
        var startDate = DateOnly.FromDateTime(start);
        var endDate = DateOnly.FromDateTime(end);

        var yearStartDate = new DateOnly(startDate.Year + 1, 1, 1);

        if (startDate < yearStartDate)
            timePeriods.Add(new TimePeriod(startDate, yearStartDate));

        while (yearStartDate.AddYears(1) <= endDate)
        {
            timePeriods.Add(new TimePeriod(yearStartDate, yearStartDate.AddYears(1)));
            yearStartDate = yearStartDate.AddYears(1);
        }

        if (yearStartDate < endDate)
            timePeriods.Add(new TimePeriod(yearStartDate, endDate));

        return timePeriods;
    }
}
