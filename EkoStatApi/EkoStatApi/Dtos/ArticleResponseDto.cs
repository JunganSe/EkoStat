namespace EkoStatApi.Dtos;

public class ArticleResponseDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public List<int>? EntryIds { get; set; }
    public List<int>? TagIds { get; set; }
    public int? UserId { get; set; }
}
