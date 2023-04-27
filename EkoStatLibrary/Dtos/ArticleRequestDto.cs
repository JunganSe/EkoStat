using System.ComponentModel.DataAnnotations;

namespace EkoStatLibrary.Dtos;

public class ArticleRequestDto
{
    [Required]
    [MinLength(2)]
    public string? Name { get; set; }

    public List<int>? TagIds { get; set; }

    [Required]
    public int? UserId { get; set; }
}
