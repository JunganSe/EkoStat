using EkoStatLibrary.Dtos;
using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using EkoStatRp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.Entries;

[BindProperties]
public class EntriesIndex : PageModelBase<EntriesIndex>
{
    public EntriesFilterViewModel FilterViewModel { get; set; } = new();
    public List<EntryGroup> EntryGroups { get; set; } = new();

    public EntriesIndex(HttpHelper httpHelper, UserHelper userHelper, ApiHandler apiHandler, DtoHelper dtoHelper, ILogger<EntriesIndex> logger)
        : base(httpHelper, userHelper, apiHandler, dtoHelper, logger)
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
            FilterViewModel.Articles = await httpClient.GetFromJsonAsync<List<ArticleResponseDto>>(articlesUrl) ?? new();
            SetTempData(nameof(FilterViewModel.Articles), FilterViewModel.Articles);

            string tagsUrl = LibraryConstants.ApiEndpoints.TagsByUser + userId;
            FilterViewModel.Tags = await httpClient.GetFromJsonAsync<List<TagResponseDto>>(tagsUrl) ?? new();
            SetTempData(nameof(FilterViewModel.Tags), FilterViewModel.Tags);

            string url = LibraryConstants.ApiEndpoints.EntriesByUser + userId;
            var entries = await httpClient.GetFromJsonAsync<List<EntryResponseDto>>(url) ?? new();
            EntryGroups = _dtoHelper.GroupEntries(entries);
        }
        catch (Exception ex)
        {
            // TODO: Meddela användaren.
            _logger.LogError(ex, "Fail: Get Entries.");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            using var httpClient = _httpHelper.GetHttpClient();
            var userId = GetUserId();

            var articleIds = Request.Form["articleIds"].ToList();
            FilterViewModel.Filter.ArticleIds = _dtoHelper.ParseStringsToInts(articleIds);

            var tagIds = Request.Form["tagIds"].ToList();
            FilterViewModel.Filter.TagIds = _dtoHelper.ParseStringsToInts(tagIds);

            string url = LibraryConstants.ApiEndpoints.EntriesFiltered + userId;
            var response = await httpClient.PostAsJsonAsync(url, FilterViewModel.Filter);
            response.EnsureSuccessStatusCode();
            var entries = await response.Content.ReadFromJsonAsync<List<EntryResponseDto>>() ?? new();
            EntryGroups = _dtoHelper.GroupEntries(entries);

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
