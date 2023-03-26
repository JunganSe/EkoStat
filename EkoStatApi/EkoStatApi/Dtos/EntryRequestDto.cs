namespace EkoStatApi.Dtos;

public class EntryRequestDto
{
    public string? Name { get; set; }
    public string? Comment { get; set; }
    public DateTimeOffset? TimeStamp { get; set; }
    public double? Count { get; set; }
    public decimal? CostPerArticle { get; set; }
    public int? ArticleId { get; set; }
    public int? UnitId { get; set; }
    public int? UserId { get; set; }
}
