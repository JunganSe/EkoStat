using System.ComponentModel.DataAnnotations;

namespace EkoStatLibrary.Dtos;

public class EntryFilterRequestDto
{
    [Display(Name = "Timestamp from")]
    public DateTime? TimestampFrom { get; set; }

    [Display(Name = "Timestamp until")]
    public DateTime? TimestampUntil { get; set; }

    [Display(Name = "Articles")]
    public List<int>? ArticleIds { get; set; }

    [Display(Name = "Tags")]
    public List<int>? TagIds { get; set; }

    [Display(Name = "Must have all tags")]
    public bool MustHaveAllTags { get; set; } = false;

    [Display(Name = "Minimum cost per article")]
    public decimal? CostPerArticleMin { get; set; }

    [Display(Name = "Maximum cost per article")]
    public decimal? CostPerArticleMax { get; set; }
}
