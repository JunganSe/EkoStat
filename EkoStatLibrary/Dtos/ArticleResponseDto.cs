#pragma warning disable CS8618

namespace EkoStatLibrary.Dtos;

public class ArticleResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<TagResponseDto> Tags { get; set; } = new();
}
