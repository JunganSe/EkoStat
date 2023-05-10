using EkoStatLibrary.DtoContainers;
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
    private readonly string _articlesKey;
    private readonly string _tagsKey;

    public EntriesFilterViewModel FilterViewModel { get; set; } = new();
    public List<EntryGroupByArticle> EntryGroups { get; set; } = new();

    public ReportsIndex(HttpHelper httpHelper, UserHelper userHelper, ApiHandler apiHandler, DtoHelper dtoHelper, ILogger<ReportsIndex> logger)
        : base(httpHelper, userHelper, apiHandler, dtoHelper, logger)
    {
        _articlesKey = "Articles";
        _tagsKey = "Tags";
    }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!IsLoggedIn())
                return GoHome();

            var userId = GetUserId();

            FilterViewModel.Articles = await _apiHandler.GetArticlesByUserAsync(userId);
            FilterViewModel.Tags = await _apiHandler.GetTagsByUserAsync(userId);

            SetTempData(_articlesKey, FilterViewModel.Articles);
            SetTempData(_tagsKey, FilterViewModel.Tags);
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
            var articleIds = Request.Form[Constants.Html.FilterFormArticleIds].ToList();
            FilterViewModel.Filter.ArticleIds = _dtoHelper.ParseStringsToInts(articleIds);

            var tagIds = Request.Form[Constants.Html.FilterFormTagIds].ToList();
            FilterViewModel.Filter.TagIds = _dtoHelper.ParseStringsToInts(tagIds);

            var userId = GetUserId();
            var entries = await _apiHandler.GetEntriesFilteredAsync(userId, FilterViewModel.Filter);
            EntryGroups = _dtoHelper.GroupEntriesByArticle(entries);

            FilterViewModel.Articles = GetTempData<List<ArticleResponseDto>>(_articlesKey);
            FilterViewModel.Tags = GetTempData<List<TagResponseDto>>(_tagsKey);
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
