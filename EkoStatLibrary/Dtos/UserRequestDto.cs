using System.ComponentModel.DataAnnotations;

namespace EkoStatLibrary.Dtos;

public class UserRequestDto
{
    [Required]
    [MinLength(2)]
    public string? Name { get; set; }
}
