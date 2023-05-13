namespace EkoStatLibrary.Extensions.Common;

public static class CommonExtensions
{
    public static List<int> ToInts(this IEnumerable<string> strings)
        => strings.Select(int.Parse).ToList(); // Kastar FormatException om en sträng inte kan översättas.
}
