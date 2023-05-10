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

    public List<int> ParseStringsToInts(List<string> strings)
    {
        // Kastar FormatException om en sträng inte kan parseas.
        return strings.Select(int.Parse).ToList();
    }

    public List<EntryGroupByTimestamp> GroupEntries(List<EntryResponseDto> entries)
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
}
