using EkoStatLibrary.Dtos;
using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.Articles;

[BindProperties]
public class ArticlesIndex : PageModelBase<ArticlesIndex>
{
    private readonly DtoHelper _dtoHelper;

    public List<ArticleResponseDto> Articles { get; set; } = new();
    public List<TagResponseDto> Tags { get; set; } = new();
    public ArticleRequestDto NewArticle { get; set; } = new();

    public ArticlesIndex(HttpHelper httpHelper, UserHelper userHelper, DtoHelper dtoHelper, ILogger<ArticlesIndex> logger)
        : base(httpHelper, userHelper, logger)
    {
        _dtoHelper = dtoHelper;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!IsLoggedIn())
                return GoHome();

            using var httpClient = new HttpClient();
            var userId = GetUserId();

            string articlesUrl = $"{_apiUrl}{LibraryConstants.ApiEndpoints.ArticlesByUser}/{userId}";
            Articles = await httpClient.GetFromJsonAsync<List<ArticleResponseDto>>(articlesUrl) ?? new();

            string tagsUrl = $"{_apiUrl}{LibraryConstants.ApiEndpoints.TagsByUser}/{userId}";
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
            using var httpClient = new HttpClient();
            string url = _apiUrl + LibraryConstants.ApiEndpoints.ArticleCreate;
            NewArticle.UserId = GetUserId();
            NewArticle.TagIds = _dtoHelper.ParseValidToInt(Request.Form["tagIds"]);
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
