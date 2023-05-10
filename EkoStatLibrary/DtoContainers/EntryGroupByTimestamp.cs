using EkoStatLibrary.Dtos;

namespace EkoStatLibrary.DtoContainers;

public class EntryGroupByTimestamp
{
    public DateTime Timestamp { get; init; }
    public List<EntryResponseDto> Entries { get; init; } = new();

    public EntryGroupByTimestamp(List<EntryResponseDto> entries)
    {
        if (entries.Count > 0)
        {
            EnsureSameTimestamp(entries);
            Timestamp = entries.First().Timestamp;
            Entries = entries;
        }
    }

    private void EnsureSameTimestamp(List<EntryResponseDto> entries)
    {
        var firstTimestamp = entries.First().Timestamp;
        if (!entries.All(e => e.Timestamp == firstTimestamp))
            throw new ArgumentException("Not all entries have the same timestamp.");
    }
}
