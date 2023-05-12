using EkoStatLibrary.Dtos;
using EkoStatLibrary.Extensions.DtoExtensions;
using EkoStatLibrary.Models;

namespace EkoStatLibrary.DtoContainers;

public class ReportSegment
{
    public TimePeriod TimePeriod { get; init; }
    public List<EntryGroupByArticle> EntryGroupsByArticle { get; init; }

    public ReportSegment(TimePeriod timePeriod, List<EntryResponseDto> entries)
    {
        TimePeriod = timePeriod;
        EntryGroupsByArticle = entries
            .Where(e => e.Timestamp >= TimePeriod.Start)
            .Where(e => e.Timestamp < TimePeriod.End)
            .GroupByArticle();
    }
}
