using System.ComponentModel.DataAnnotations;

namespace EkoStatApi.Dtos;

public class UserRequestDto
{
    [Required]
    [MinLength(2)]
    public string? Name { get; set; }
}
