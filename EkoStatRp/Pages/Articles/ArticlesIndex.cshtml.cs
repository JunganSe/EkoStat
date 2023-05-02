using EkoStatLibrary.Dtos;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.Articles;

[BindProperties]
public class ArticlesIndex : PageModelBase<ArticlesIndex>
{
    public List<ArticleResponseDto> Articles { get; set; } = new();
    public ArticleRequestDto NewArticle { get; set; } = new();

    public ArticlesIndex(HttpHelper httpHelper, UserHelper userHelper, ILogger<ArticlesIndex> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!IsLoggedIn())
                return GoHome();

            using var httpClient = new HttpClient();
            var userId = GetUserId();
            string url = $"{_apiUrl}{Constants.ApiEndpoints.ArticlesByUser}/{userId}";
            Articles = await httpClient.GetFromJsonAsync<List<ArticleResponseDto>>(url) ?? new();
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
            string url = _apiUrl + Constants.ApiEndpoints.ArticleCreate;
            NewArticle.UserId = GetUserId();
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
