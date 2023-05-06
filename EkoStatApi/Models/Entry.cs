namespace EkoStatApi.Models;
#nullable disable

public class Entry
{
    // Keys
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public int UnitId { get; set; }
    public int UserId { get; set; }

    // Data
    public string Comment { get; set; }
    public DateTime Timestamp { get; set; }
    public int Count { get; set; }
    public double Size { get; set; }
    public decimal CostPerArticle { get; set; }

    // Navigation
    public Article Article { get; set; }
    public Unit Unit { get; set; }
    public User User { get; set; }
}
