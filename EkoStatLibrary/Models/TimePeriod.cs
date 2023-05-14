using NodaTime;

namespace EkoStatLibrary.Models;

public class TimePeriod
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }

    public int Years { get; private set; }
    public int Months { get; private set; }
    public int Days { get; private set; }
    public int TotalDays { get; private set; }

    public TimePeriod(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
        SetProperties();
    }

    public TimePeriod(DateOnly start, DateOnly end)
    {
        Start = new DateTime(start.Year, start.Month, start.Day);
        End = new DateTime(end.Year, end.Month, end.Day);
        SetProperties();
    }

    private void SetProperties()
    {
        var start = LocalDateTime.FromDateTime(Start);
        var end = LocalDateTime.FromDateTime(End);
        var period = Period.Between(start, end);
        Years = period.Years;
        Months = period.Months;
        Days = period.Days;
        TotalDays = Period.Between(start, end, PeriodUnits.Days).Days;
    }

}
