using EkoStatLibrary.Dtos;

namespace EkoStatLibrary.Helpers;

public class DtoHelper
{
    public string GetTagNamesAsString(List<TagResponseDto> tags)
    {
        var names = tags.Select(t => t.Name);
        return string.Join(", ", names);
    }

    // Hoppar över de som inte kunde konverteras.
    public List<int> ParseValidStringsToInt(List<string> strings)
    {
        var output = new List<int>();
        foreach (var s in strings)
        {
            if (int.TryParse(s, out int parsedValue))
                output.Add(parsedValue);
        }
        return output;
    }

    public List<EntryGroup> GroupEntries(List<EntryResponseDto> entries)
    {
        var distinctTimestamps = entries
            .Select(e => e.Timestamp)
            .Distinct();

        var groups = distinctTimestamps
            .Select(dt => new EntryGroup(dt))
            .ToList();

        groups.ForEach(group => group.Entries 
            = entries.Where(e => e.Timestamp == group.Timestamp).ToList());

        return groups.OrderByDescending(e => e.Timestamp).ToList();
    }
}
