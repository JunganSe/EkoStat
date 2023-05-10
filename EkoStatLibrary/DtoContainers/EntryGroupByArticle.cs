using EkoStatLibrary.Dtos;

namespace EkoStatLibrary.DtoContainers;

public class EntryGroupByArticle
{
    public decimal CostTotal => Entries.Sum(e => e.CostPerArticle * e.Count);

    public ArticleResponseDto Article { get; private set; }
    public List<EntryResponseDto> Entries { get; set; } = new();

    public EntryGroupByArticle(ArticleResponseDto article)
    {
        Article = article;
    }
}
