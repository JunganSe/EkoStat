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
        var nodaStart = new LocalDateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, start.Second);
        var nodaEnd = new LocalDateTime(end.Year, end.Month, end.Day, end.Hour, end.Minute, end.Second);
        _nodaPeriod = Period.Between(nodaStart, nodaEnd);
    }

}
