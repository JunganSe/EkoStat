using EkoStatLibrary.DtoContainers;
using EkoStatLibrary.Dtos;

namespace EkoStatLibrary.Extensions.DtoExtensions;

public static class EntryGroupExtensions
{
    public static DateTime? GetEarliestTimestamp(this List<EntryGroupByTimestamp> entryGroups) 
        => (entryGroups.Any())
            ? entryGroups.Select(e => e.Timestamp).Min()
            : null;
    public static DateTime? GetEarliestTimestamp(this List<EntryGroupByArticle> entryGroups) 
        => (entryGroups.Any())
            ? entryGroups.SelectMany(eg => eg.Entries).Select(e => e.Timestamp).Min()
            : null;

    public static DateTime? GetLatestTimestamp(this List<EntryGroupByTimestamp> entryGroups) 
        => (entryGroups.Any())
            ? entryGroups.Select(e => e.Timestamp).Max()
            : null;
    public static DateTime? GetLatestTimestamp(this List<EntryGroupByArticle> entryGroups) 
        => (entryGroups.Any())
            ? entryGroups.SelectMany(eg => eg.Entries).Select(e => e.Timestamp).Max()
            : null;

    public static int GetEntriesCount(this List<EntryGroupByTimestamp> entryGroups)
        => entryGroups.Sum(eg => eg.Entries.Count);
    public static int GetEntriesCount(this List<EntryGroupByArticle> entryGroups)
        => entryGroups.Sum(eg => eg.Entries.Count);

    public static decimal GetTotalCost(this List<EntryGroupByTimestamp> entryGroups)
        => entryGroups.Sum(eg => eg.Entries.Sum(e => e.CostPerArticle * e.Count));
    public static decimal GetTotalCost(this List<EntryGroupByArticle> entryGroups)
        => entryGroups.Sum(eg => eg.Entries.Sum(e => e.CostPerArticle * e.Count));
}

public static class TagExtensions
{
    public static string GetNamesAsString(this List<TagResponseDto> tags)
        => string.Join(", ", tags.Select(t => t.Name));
}