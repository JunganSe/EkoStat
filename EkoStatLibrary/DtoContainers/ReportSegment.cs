using EkoStatLibrary.Dtos;
using EkoStatLibrary.Enums;
using EkoStatLibrary.Extensions.DtoExtensions;
using EkoStatLibrary.Models;

namespace EkoStatLibrary.DtoContainers;

public class ReportSegment
{
    public TimePeriod TimePeriod { get; init; }
    public List<EntryGroupByArticle> EntryGroups { get; init; }
    public decimal? CostThreshold { get; private set; }

    public ReportSegment(TimePeriod timePeriod, List<EntryGroupByArticle> entryGroups)
    {
        EnsureEntriesAreWithinTimePeriod(timePeriod, entryGroups);
        TimePeriod = timePeriod;
        EntryGroups = entryGroups;
    }

    private void EnsureEntriesAreWithinTimePeriod(TimePeriod timePeriod, List<EntryGroupByArticle> entryGroups)
    {
        bool ok = entryGroups
            .SelectMany(eg => eg.Entries)
            .All(e =>
                e.Timestamp >= timePeriod.Start
                && e.Timestamp < timePeriod.End);
        if (!ok)
            throw new ArgumentException("Not all entries are withing the time period.");
    }

    public void SetCostThreshold(decimal? limit, LimitType limitType)
    {
        CostThreshold = GetCostThreshold(limit, limitType);
    }

    private decimal GetCostThreshold(decimal? limit, LimitType limitType)
    {
        if (limit == null)
            return decimal.MaxValue;

        if (limitType == LimitType.Fixed)
            return (decimal)limit;
        else if (limitType == LimitType.Percentile)
            return GetArticleCostPercentileValue((decimal)limit);

        return decimal.MaxValue;

        decimal GetArticleCostPercentileValue(decimal percentile)
        {
            if (!EntryGroups.Any())
                return decimal.MaxValue;

            decimal lowestCost = EntryGroups.Select(eg => eg.TotalCost).Min();
            decimal highestCost = EntryGroups.Select(eg => eg.TotalCost).Max();
            decimal costSpan = highestCost - lowestCost;
            return lowestCost + costSpan * percentile / 100;
        }
    }
}
