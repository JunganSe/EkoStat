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

    public ArticlesIndex(HttpHelper httpHelper, UserHelper userHelper, ApiHandler apiHandler, DtoHelper dtoHelper, ILogger<ArticlesIndex> logger)
        : base(httpHelper, userHelper, apiHandler, dtoHelper, logger)
    {
    }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!IsLoggedIn())
                return GoHome();

            var userId = GetUserId();
            Articles = await _apiHandler.GetArticlesByUserAsync(userId);
            Tags = await _apiHandler.GetTagsByUserAsync(userId);
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
            var tagIds = Request.Form["tagIds"].ToList();
            NewArticle.TagIds = _dtoHelper.ParseStringsToInts(tagIds);
            NewArticle.UserId = GetUserId();
            var response = await _apiHandler.CreateArticleAsync(NewArticle);
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
