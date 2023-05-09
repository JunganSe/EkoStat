using EkoStatLibrary.Dtos;
using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.Articles;

[BindProperties]
public class ArticlesIndex : PageModelBase<ArticlesIndex>
{
    public List<ArticleResponseDto> Articles { get; set; } = new();
    public List<TagResponseDto> Tags { get; set; } = new();
    public ArticleRequestDto NewArticle { get; set; } = new();

    public ArticlesIndex(HttpHelper httpHelper, UserHelper userHelper, DtoHelper dtoHelper, ILogger<ArticlesIndex> logger)
        : base(httpHelper, userHelper, dtoHelper, logger)
    {
    }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!IsLoggedIn())
                return GoHome();

            using var httpClient = _httpHelper.GetHttpClient();
            var userId = GetUserId();

            string articlesUrl = LibraryConstants.ApiEndpoints.ArticlesByUser + userId;
            Articles = await httpClient.GetFromJsonAsync<List<ArticleResponseDto>>(articlesUrl) ?? new();

            string tagsUrl = LibraryConstants.ApiEndpoints.TagsByUser + userId;
            Tags = await httpClient.GetFromJsonAsync<List<TagResponseDto>>(tagsUrl) ?? new();
        }
        catch (Exception ex)
        {
            // TODO: Meddela användaren.
            _logger.LogError(ex, "Fail: Get Articles.");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            using var httpClient = _httpHelper.GetHttpClient();
            string url = LibraryConstants.ApiEndpoints.ArticleCreate;

            NewArticle.UserId = GetUserId();
            var tagIds = Request.Form["tagIds"].ToList();
            NewArticle.TagIds = _dtoHelper.ParseStringsToInts(tagIds);
            var response = await httpClient.PostAsJsonAsync(url, NewArticle);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            // TODO: Meddela användaren.
            _logger.LogError(ex, "Fail: Create article.");
        }
        return RedirectToPage();
    }
}
