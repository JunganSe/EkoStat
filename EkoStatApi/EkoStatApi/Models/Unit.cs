namespace EkoStatApi.Models;

public class Unit
{
    // Keys
    public int Id { get; set; }

    // Data
    public string Name { get; set; } = null!;

    // Navigation
    public ICollection<Entry> Entries { get; set; } = new List<Entry>();
}
