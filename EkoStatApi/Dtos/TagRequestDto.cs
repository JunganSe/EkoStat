using System.ComponentModel.DataAnnotations;

namespace EkoStatApi.Dtos;

public class TagRequestDto
{
    [Required]
    [MinLength(2)]
    public string? Name { get; set; }
    
    [Required]
    public int? UserId { get; set; }
}
