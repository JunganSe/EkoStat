namespace EkoStatApi.Models;

public class Tag
{
    // Keys
    public int Id { get; set; }
    public int UserId { get; set; }

    // Data
    public string Name { get; set; } = null!;

    // Navigation
    public ICollection<Article> Articles { get; set; } = new List<Article>();
    public User User { get; set; } = null!;
}
