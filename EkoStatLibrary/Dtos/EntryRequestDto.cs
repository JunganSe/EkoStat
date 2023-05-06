using System.ComponentModel.DataAnnotations;

namespace EkoStatLibrary.Dtos;

public class EntryRequestDto
{
    [Display(Name = "Comment (optional)")]
    [MaxLength(200)]
    public string? Comment { get; set; }

    [Required]
    [Display(Name = "Timestamp")]
    public DateTime Timestamp { get; set; }

    [Required]
    [Display(Name = "Count")]
    [Range(1, 1000000, ErrorMessage = "Must be {1} to {2}")]
    public int Count { get; set; }

    [Required]
    [Display(Name = "Size")]
    [Range(1, 1000000, ErrorMessage = "Must be {1} to {2}")]
    public double Size { get; set; }

    [Required]
    [Display(Name = "Cost per article")]
    [Range(typeof(decimal), "0", "1000000", ErrorMessage = "Must be {1} to {2}")]
    public decimal CostPerArticle { get; set; }

    [Required(ErrorMessage = "Required")]
    [Display(Name = "Article")]
    public int ArticleId { get; set; }

    [Required(ErrorMessage = "Required")]
    [Display(Name = "Unit")]
    public int UnitId { get; set; }

    [Required]
    public int UserId { get; set; }
}
