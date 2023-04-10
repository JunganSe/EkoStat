namespace EkoStatApi.Dtos;

public class ArticleResponseDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public List<TagResponseDto>? Tags { get; set; }
    public int? UserId { get; set; }
}
