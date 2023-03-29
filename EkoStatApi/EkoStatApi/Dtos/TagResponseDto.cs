namespace EkoStatApi.Dtos;

public class TagResponseDto
{
    public int? Id { get; set; }
    public string? Name { get; set; } = null!;
    public List<int>? ArticleIds { get; set; }
    public List<ArticleResponseDto>? Articles { get; set; }
    public int? UserId { get; set; }
}
