using System.ComponentModel.DataAnnotations;

namespace EkoStatApi.Dtos;

public class EntryRequestDto
{
    [MaxLength(200)]
    public string? Comment { get; set; }

    [Required]
    public DateTimeOffset? TimeStamp { get; set; }

    [Required]
    public double? Count { get; set; }

    [Required]
    public decimal? CostPerArticle { get; set; }

    [Required]
    public int? ArticleId { get; set; }

    [Required]
    public int? UnitId { get; set; }

    [Required]
    public int? UserId { get; set; }
}
