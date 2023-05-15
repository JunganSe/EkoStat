using EkoStatLibrary.Enums;
using EkoStatLibrary.Models;

namespace EkoStatLibrary.Helpers;

public class TimeHelper
{
    public List<TimePeriod> GetChosenTimePeriods(DateTime start, DateTime end, SegmentSize segmentSize)
    {
        return segmentSize switch
        {
            SegmentSize.None => new List<TimePeriod> { new TimePeriod(start, end.Date.AddDays(1)) },
            SegmentSize.Week => GetTimePeriodsByWeek(start, end),
            SegmentSize.Month => GetTimePeriodsByMonth(start, end),
            SegmentSize.Year => GetTimePeriodsByYear(start, end),
            _ => throw new ArgumentException("You're not supposed to be in here!")
        };
    }

    public List<TimePeriod> GetTimePeriodsByWeek(DateTime start, DateTime end, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
    {
        var timePeriods = new List<TimePeriod>();
        var startDate = start.Date;
        var endDate = end.Date.AddDays(1);
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

        DateTime GetFirstDayOfWeekOnOrAfterDate(DateTime date, DayOfWeek firstDayOfWeek)
        {
            while (date.DayOfWeek != firstDayOfWeek)
                date = date.AddDays(1);
            return date;
        }
    }

    public List<TimePeriod> GetTimePeriodsByMonth(DateTime start, DateTime end)
    {
        var timePeriods = new List<TimePeriod>();
        var startDate = start.Date;
        var endDate = end.Date.AddDays(1);
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

        DateTime GetMonthStartOnOrAfterDate(DateTime date)
        {
            if (date.Day > 1)
                date = date.AddMonths(1);
            return date.AddDays(1 - date.Day);
        }
    }

    public List<TimePeriod> GetTimePeriodsByYear(DateTime start, DateTime end)
    {
        var timePeriods = new List<TimePeriod>();
        var startDate = start.Date;
        var endDate = end.Date.AddDays(1);
        var yearStartDate = new DateTime(startDate.Year + 1, 1, 1);

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
