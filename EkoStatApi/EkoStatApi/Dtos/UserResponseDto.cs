namespace EkoStatApi.Dtos;

internal class UserResponseDto
{
    public string? Name { get; set; }
    public List<int>? TagIds { get; set; }
    public List<int>? ArticleIds { get; set; }
    public List<int>? EntryIds { get; set; }
}
