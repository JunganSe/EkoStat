namespace EkoStatRp.Models;

public class TimePeriod
{
    private readonly int _roundingPrecision = 2;

    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public TimeSpan Duration => End - Start;
    public double Days => Math.Round(Duration.TotalDays, _roundingPrecision);
    public double Weeks => Math.Round(Days / 7, _roundingPrecision);
    public double Months => Math.Round(Days / 30.44, _roundingPrecision);
    public double Years => Math.Round(Days / 365.25, _roundingPrecision);

    public TimePeriod(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

}
