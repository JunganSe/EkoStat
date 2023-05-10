using EkoStatLibrary.Dtos;

namespace EkoStatLibrary.DtoContainers;

public class EntryGroupByTimestamp
{
    public DateTime Timestamp { get; set; }
    public List<EntryResponseDto> Entries { get; set; } = new();

    public EntryGroupByTimestamp(DateTime timestamp)
    {
        Timestamp = timestamp;
    }
}
