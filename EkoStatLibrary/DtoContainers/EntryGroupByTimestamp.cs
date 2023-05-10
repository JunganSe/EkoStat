using EkoStatLibrary.Dtos;

namespace EkoStatLibrary.DtoContainers;

public class EntryGroupByTimestamp
{
    public DateTime? Timestamp { get; init; } = null;
    public List<EntryResponseDto> Entries { get; init; } = new();

    public EntryGroupByTimestamp(List<EntryResponseDto> entries)
    {
        if (entries.Count > 0)
        {
            EnsureSameTimestamp(entries);
            Timestamp = entries[0].Timestamp;
            Entries = entries;
        }
    }

    public void EnsureSameTimestamp(List<EntryResponseDto> entries)
    {
        var firstTimestamp = entries[0].Timestamp;
        if (!entries.All(e => e.Timestamp == firstTimestamp))
            throw new ArgumentException("Not all entries have the same timestamp.");
    }
}
