using EkoStatLibrary.Dtos;

namespace EkoStatLibrary.DtoContainers;

public class EntryGroup
{
    public DateTime Timestamp { get; set; }
    public List<EntryResponseDto> Entries { get; set; } = new();

    public EntryGroup(DateTime timestamp)
    {
        Timestamp = timestamp;
    }
}
