using EkoStatLibrary.Dtos;

namespace EkoStatRp.Models;

public class EntriesFilterViewModel
{
    public EntryFilterRequestDto Filter { get; set; } = new();
    public List<ArticleResponseDto> Articles { get; set; } = new();
    public List<TagResponseDto> Tags { get; set; } = new();
}
