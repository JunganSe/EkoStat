namespace EkoStatApi.Dtos;

public class EntryResponseDto
{
    public int? Id { get; set; }
    public string? Comment { get; set; }
    public DateTimeOffset? TimeStamp { get; set; }
    public double? Count { get; set; }
    public decimal? CostPerArticle { get; set; }
    public ArticleResponseDto? Article { get; set; }
    public UnitResponseDto? Unit { get; set; }
}
