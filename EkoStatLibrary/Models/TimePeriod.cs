using NodaTime;

namespace EkoStatLibrary.Models;

public class TimePeriod
{
    private readonly Period _nodaPeriod;

    public DateTime Start { get; init; }
    public DateTime End { get; init; }

    public TimeSpan Duration => End - Start;
    public int Years => _nodaPeriod.Years;
    public int Months => _nodaPeriod.Months;
    public int Days => _nodaPeriod.Days;

    public TimePeriod(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
        var nodaStart = LocalDateTime.FromDateTime(start);
        var nodaEnd = LocalDateTime.FromDateTime(end);
        _nodaPeriod = Period.Between(nodaStart, nodaEnd);
    }

    public TimePeriod(DateOnly start, DateOnly end)
    {
        Start = new DateTime(start.Year, start.Month, start.Day);
        End = new DateTime(end.Year, end.Month, end.Day);
        var nodaStart = LocalDateTime.FromDateTime(Start);
        var nodaEnd = LocalDateTime.FromDateTime(End);
        _nodaPeriod = Period.Between(nodaStart, nodaEnd);
    }

}
