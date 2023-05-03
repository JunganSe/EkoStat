#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace EkoStatLibrary.Dtos;

public class UnitRequestDto
{
    [Required]
    [MinLength(1)]
    public string Name { get; set; }

    [Required]
    [MinLength(1)]
    public string ShortName { get; set; }
}
