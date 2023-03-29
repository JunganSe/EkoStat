namespace EkoStatApi.Dtos;

public class UserResponseDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public List<int>? TagIds { get; set; }
    public List<TagResponseDto>? Tags { get; set; }
    public List<int>? ArticleIds { get; set; }
    public List<ArticleResponseDto>? Articles { get; set; }
    public List<int>? EntryIds { get; set; }
    public List<EntryResponseDto>? Entries { get; set; }
}
