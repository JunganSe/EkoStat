using EkoStatLibrary.DtoContainers;
using EkoStatLibrary.Dtos;

namespace EkoStatLibrary.Helpers;

public class DtoHelper
{
    public string GetTagNamesAsString(List<TagResponseDto> tags)
    {
        var names = tags.Select(t => t.Name);
        return string.Join(", ", names);
    }

    public List<EntryGroupByTimestamp> GroupEntriesByTimestamp(List<EntryResponseDto> entries)
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

    public List<EntryGroupByArticle> GroupEntriesByArticle(List<EntryResponseDto> entries)
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
