using EkoStatLibrary.Dtos;
using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.Reports;

[BindProperties]
public class ReportsIndex : PageModelBase<ReportsIndex>
{
    public List<EntryResponseDto> Entries { get; set; } = new();
    public EntryFilterRequestDto Filter { get; set; } = new();
    public List<ArticleResponseDto> Articles { get; set; } = new();
    public List<TagResponseDto> Tags { get; set; } = new();

    public ReportsIndex(HttpHelper httpHelper, UserHelper userHelper, DtoHelper dtoHelper, ILogger<ReportsIndex> logger)
        : base(httpHelper, userHelper, dtoHelper, logger)
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

            string articlesUrl = $"{_apiUrl}{LibraryConstants.ApiEndpoints.ArticlesByUser}/{userId}";
            Articles = await httpClient.GetFromJsonAsync<List<ArticleResponseDto>>(articlesUrl) ?? new();
            SetTempData(nameof(Articles), Articles);

            string tagsUrl = $"{_apiUrl}{LibraryConstants.ApiEndpoints.TagsByUser}/{userId}";
            Tags = await httpClient.GetFromJsonAsync<List<TagResponseDto>>(tagsUrl) ?? new();
            SetTempData(nameof(Tags), Tags);
        }
        catch (Exception ex)
        {
            // TODO: Meddela användaren.
            _logger.LogError(ex, "Fail: Get stuff needed for reports.");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            using var httpClient = new HttpClient();
            var userId = GetUserId();

            var articleIds = Request.Form["articleIds"].ToList();
            Filter.ArticleIds = _dtoHelper.ParseStringsToInts(articleIds);

            var tagIds = Request.Form["tagIds"].ToList();
            Filter.TagIds = _dtoHelper.ParseStringsToInts(tagIds);

            string url = $"{_apiUrl}{LibraryConstants.ApiEndpoints.EntriesFiltered}/{userId}";
            var response = await httpClient.PostAsJsonAsync(url, Filter);
            response.EnsureSuccessStatusCode();
            Entries = await response.Content.ReadFromJsonAsync<List<EntryResponseDto>>() ?? new();

            Articles = GetTempData<List<ArticleResponseDto>>(nameof(Articles));
            Tags = GetTempData<List<TagResponseDto>>(nameof(Tags));
            return Page();
        }
        catch (Exception ex)
        {
            // TODO: Meddela användaren.
            _logger.LogError(ex, "Fail: Generate report.");
            return RedirectToPage();
        }
    }
}
