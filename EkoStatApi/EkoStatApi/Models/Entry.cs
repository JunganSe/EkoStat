namespace EkoStatApi.Models;

public class Entry
{
    // Keys
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public int UnitId { get; set; }
    public int UserId { get; set; }

    // Data
    public string Comment { get; set; } = null!;
    public DateTimeOffset TimeStamp { get; set; }
    public double Count { get; set; }
    public decimal CostPerArticle { get; set; }

    // Navigation
    public Article Article { get; set; } = null!;
    public Unit Unit { get; set; } = null!;
    public User User { get; set; } = null!;
}
