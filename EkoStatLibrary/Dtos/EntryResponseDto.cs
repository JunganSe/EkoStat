#pragma warning disable CS8618

namespace EkoStatLibrary.Dtos;

public class EntryResponseDto
{
    public int Id { get; set; }
    public string Comment { get; set; }
    public DateTime Timestamp { get; set; }
    public int Count { get; set; }
    public double Size { get; set; }
    public decimal CostPerArticle { get; set; }
    public ArticleResponseDto Article { get; set; }
    public UnitResponseDto Unit { get; set; }
}
