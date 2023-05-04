using EkoStatLibrary.Dtos;
using Microsoft.Extensions.Primitives;

namespace EkoStatRp.Helpers;

public class DtoHelper
{
    public string GetTagNamesAsString(List<TagResponseDto> tags)
    {
        var names = tags.Select(t => t.Name);
        return string.Join(", ", names);
    }

    // Hoppar över de som inte kunde konverteras.
    public List<int> ParseValidToInt(StringValues strings)
    {
        var output = new List<int>();
        foreach (var s in strings)
        {
            if (int.TryParse(s, out int parsedValue))
                output.Add(parsedValue);
        }
        return output;
    }

    public DateTimeOffset ConvertToDateTimeOffset(DateTime dateTime)
    {
        var offset = new TimeSpan(2, 0, 0); // TODO: Hämta utifrån tidszon och vinter/sommartid.
        return new DateTimeOffset(dateTime, offset);
    }
}
