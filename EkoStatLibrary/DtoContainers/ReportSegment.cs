using EkoStatLibrary.Dtos;
using EkoStatLibrary.Helpers;
using EkoStatLibrary.Models;

namespace EkoStatLibrary.DtoContainers;

public class ReportSegment
{
    public TimePeriod TimePeriod { get; init; }
    public List<EntryGroupByArticle> EntryGroupsByArticle { get; init; }

    public ReportSegment(TimePeriod timePeriod, List<EntryResponseDto> entries)
    {
        TimePeriod = timePeriod;
        var relevantEntries = entries
            .Where(e => e.Timestamp >= TimePeriod.Start)
            .Where(e => e.Timestamp < TimePeriod.End)
            .ToList();
        EntryGroupsByArticle = new DtoHelper().GroupEntriesByArticle(relevantEntries);
    }
}
