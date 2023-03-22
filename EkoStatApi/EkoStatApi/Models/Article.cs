namespace EkoStatApi.Models;

public class Article
{
    // Keys
    public int Id { get; set; }
    public int UserId { get; set; }

    // Data
    public string Name { get; set; } = null!;

    // Navigation
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public User User { get; set; } = null!;
}
