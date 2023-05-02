namespace EkoStatApi.Models;
#nullable disable

public class Unit
{
    // Keys
    public int Id { get; set; }

    // Data
    public string Name { get; set; }
    public string ShortName { get; set; }

    // Navigation
    public ICollection<Entry> Entries { get; set; } = new List<Entry>();
}
