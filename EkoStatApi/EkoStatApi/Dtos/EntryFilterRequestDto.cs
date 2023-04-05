namespace EkoStatApi.Dtos;

public class EntryFilterRequestDto
{
    public List<int>? ArticleIds { get; set; }
    public List<int>? TagIds { get; set; }
    public bool? MustHaveAllTags { get; set; } = false;
    public decimal? PriceMin { get; set; }
    public decimal? PriceMax { get; set; }
    public DateTimeOffset? TimestampFrom { get; set; }
    public DateTimeOffset? TimestampUntil { get; set; }
}
