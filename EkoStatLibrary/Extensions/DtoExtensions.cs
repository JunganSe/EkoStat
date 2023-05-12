using EkoStatLibrary.DtoContainers;
using EkoStatLibrary.Dtos;

namespace EkoStatLibrary.Extensions.DtoExtensions;

public static class EntryGroupExtensions
{
    public static DateTime? GetEarliestTimestamp(this IEnumerable<EntryGroupByTimestamp> entryGroups) 
        => (entryGroups.Any())
            ? entryGroups.Select(e => e.Timestamp).Min()
            : null;
    public static DateTime? GetEarliestTimestamp(this IEnumerable<EntryGroupByArticle> entryGroups) 
        => (entryGroups.Any())
            ? entryGroups.SelectMany(eg => eg.Entries).Select(e => e.Timestamp).Min()
            : null;

    public static DateTime? GetLatestTimestamp(this IEnumerable<EntryGroupByTimestamp> entryGroups) 
        => (entryGroups.Any())
            ? entryGroups.Select(e => e.Timestamp).Max()
            : null;
    public static DateTime? GetLatestTimestamp(this IEnumerable<EntryGroupByArticle> entryGroups) 
        => (entryGroups.Any())
            ? entryGroups.SelectMany(eg => eg.Entries).Select(e => e.Timestamp).Max()
            : null;

    public static int GetEntriesCount(this IEnumerable<EntryGroupByTimestamp> entryGroups)
        => entryGroups.Sum(eg => eg.Entries.Count);
    public static int GetEntriesCount(this IEnumerable<EntryGroupByArticle> entryGroups)
        => entryGroups.Sum(eg => eg.Entries.Count);

    public static decimal GetTotalCost(this IEnumerable<EntryGroupByTimestamp> entryGroups)
        => entryGroups.Sum(eg => eg.Entries.Sum(e => e.CostPerArticle * e.Count));
    public static decimal GetTotalCost(this IEnumerable<EntryGroupByArticle> entryGroups)
        => entryGroups.Sum(eg => eg.Entries.Sum(e => e.CostPerArticle * e.Count));
}

public static class TagExtensions
{
    public static string GetNamesAsString(this IEnumerable<TagResponseDto> tags)
        => string.Join(", ", tags.Select(t => t.Name));
}

public static class EntryExtensions
{
    public static List<EntryGroupByTimestamp> GroupByTimestamp(this IEnumerable<EntryResponseDto> entries)
    {
        var distinctTimestamps = entries
            .Select(e => e.Timestamp)
            .Distinct();

        var groups = new List<EntryGroupByTimestamp>();
        foreach (var groupTimestamp in distinctTimestamps)
        {
            var groupedEntries = entries.Where(e => e.Timestamp == groupTimestamp).ToList();
            var newGroup = new EntryGroupByTimestamp(groupedEntries);
            groups.Add(newGroup);
        }

        return groups.OrderByDescending(e => e.Timestamp).ToList();
    }

    public static List<EntryGroupByArticle> GroupByArticle(this IEnumerable<EntryResponseDto> entries)
    {
        var distinctArticles = entries
            .GroupBy(e => e.Article.Id)
            .Select(group => group.First().Article);

        var groups = new List<EntryGroupByArticle>();
        foreach (var article in distinctArticles)
        {
            var groupedEntries = entries.Where(e => e.Article.Id == article.Id).ToList();
            var newGroup = new EntryGroupByArticle(groupedEntries);
            groups.Add(newGroup);
        }

        return groups.OrderBy(e => e.Article.Name).ToList();
    }
}
