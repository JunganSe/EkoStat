#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace EkoStatLibrary.Dtos;

public class ArticleRequestDto
{
    [Required]
    [MinLength(2)]
    public string Name { get; set; }

    [Required]
    public List<int> TagIds { get; set; }

    [Required]
    public int UserId { get; set; }
}
