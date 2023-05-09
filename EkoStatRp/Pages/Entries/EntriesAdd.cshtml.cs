using EkoStatLibrary.Dtos;
using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.Entries;

[BindProperties]
public class EntriesAdd : PageModelBase<EntriesAdd>
{
    public List<ArticleResponseDto> Articles { get; set; } = new();
    public List<UnitResponseDto> Units { get; set; } = new();
    public EntryRequestDto NewEntry { get; set; } = new();

    public EntriesAdd(HttpHelper httpHelper, UserHelper userHelper, ApiHandler apiHandler, DtoHelper dtoHelper, ILogger<EntriesAdd> logger)
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

            string latestEntryUrl = LibraryConstants.ApiEndpoints.EntriesLatestByUser + userId;
            var latestEntry = await httpClient.GetFromJsonAsync<EntryResponseDto>(latestEntryUrl);
            NewEntry.Timestamp = latestEntry?.Timestamp ?? DateTime.Today;

            string articlesUrl = LibraryConstants.ApiEndpoints.ArticlesByUser + userId;
            Articles = await httpClient.GetFromJsonAsync<List<ArticleResponseDto>>(articlesUrl) ?? new();

            string unitsUrl = LibraryConstants.ApiEndpoints.UnitsAll;
            Units = await httpClient.GetFromJsonAsync<List<UnitResponseDto>>(unitsUrl) ?? new();
        }
        catch (Exception ex)
        {
            // TODO: Meddela användaren.
            _logger.LogError(ex, "Fail: Get stuff needed to create entry.");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            using var httpClient = _httpHelper.GetHttpClient();
            string url = LibraryConstants.ApiEndpoints.EntryCreate;

            NewEntry.Timestamp = GetFormTimestamp();
            NewEntry.UserId = GetUserId();
            var response = await httpClient.PostAsJsonAsync(url, NewEntry);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            // TODO: Meddela användaren.
            _logger.LogError(ex, "Fail: Create entry.");
        }
        return RedirectToPage();
    }




    private DateTime GetFormTimestamp()
    {
        string date = Request.Form["inputDate"];
        string time = Request.Form["inputTime"];
        return DateTime.Parse($"{date} {time}");
    }
}
