using EkoStatLibrary.Dtos;
using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using EkoStatRp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.Reports;

[BindProperties]
public class ReportsIndex : PageModelBase<ReportsIndex>
{
    public EntriesFilterViewModel FilterViewModel { get; set; } = new();
    public List<EntryResponseDto> Entries { get; set; } = new();

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
            FilterViewModel.Articles = await httpClient.GetFromJsonAsync<List<ArticleResponseDto>>(articlesUrl) ?? new();
            SetTempData(nameof(FilterViewModel.Articles), FilterViewModel.Articles);

            string tagsUrl = $"{_apiUrl}{LibraryConstants.ApiEndpoints.TagsByUser}/{userId}";
            FilterViewModel.Tags = await httpClient.GetFromJsonAsync<List<TagResponseDto>>(tagsUrl) ?? new();
            SetTempData(nameof(FilterViewModel.Tags), FilterViewModel.Tags);
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
            FilterViewModel.Filter.ArticleIds = _dtoHelper.ParseStringsToInts(articleIds);

            var tagIds = Request.Form["tagIds"].ToList();
            FilterViewModel.Filter.TagIds = _dtoHelper.ParseStringsToInts(tagIds);

            string url = $"{_apiUrl}{LibraryConstants.ApiEndpoints.EntriesFiltered}/{userId}";
            var response = await httpClient.PostAsJsonAsync(url, FilterViewModel.Filter);
            response.EnsureSuccessStatusCode();
            Entries = await response.Content.ReadFromJsonAsync<List<EntryResponseDto>>() ?? new();

            FilterViewModel.Articles = GetTempData<List<ArticleResponseDto>>(nameof(FilterViewModel.Articles));
            FilterViewModel.Tags = GetTempData<List<TagResponseDto>>(nameof(FilterViewModel.Tags));
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
