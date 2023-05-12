namespace EkoStatLibrary.Extensions;

public static class CommonExtensions
{
    public static List<int> ToInts(this List<string> strings)
        => strings.Select(int.Parse).ToList(); // Kastar FormatException om en sträng inte kan översättas.
}
