namespace EkoStatApi.Dtos;

public class UnitResponseDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public List<int>? EntryIds { get; set; }
    public List<EntryResponseDto>? Entries { get; set; }
}
