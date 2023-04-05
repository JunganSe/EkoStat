using System.ComponentModel.DataAnnotations;

namespace EkoStatApi.Dtos;

public class UnitRequestDto
{
    [Required]
    [MinLength(1)]
    public string? Name { get; set; }
}
