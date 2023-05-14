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

    public ReportSegment(TimePeriod timePeriod, List<EntryResponseDto> entries)
    {
        TimePeriod = timePeriod;
        EntryGroups = entries
            .Where(e => e.Timestamp >= TimePeriod.Start)
            .Where(e => e.Timestamp < TimePeriod.End)
            .GroupByArticle();
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
            decimal lowestCost = EntryGroups.Select(eg => eg.TotalCost).Min();
            decimal highestCost = EntryGroups.Select(eg => eg.TotalCost).Max();
            decimal costSpan = highestCost - lowestCost;
            return lowestCost + costSpan * percentile / 100;
        }
    }
}
