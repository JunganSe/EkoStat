using EkoStatLibrary.Dtos;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.Entries;

[BindProperties]
public class EntriesAdd : PageModelBase<EntriesAdd>
{
    public EntryRequestDto NewEntry { get; set; } = new();
    public List<ArticleResponseDto> Articles { get; set; } = new();
    public List<UnitResponseDto> Units { get; set; } = new();

    public EntriesAdd(HttpHelper httpHelper, UserHelper userHelper, ILogger<EntriesAdd> logger)
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

            NewEntry.Timestamp = DateTime.Today; // TODO: Anv�nd timestamp fr�n senaste entry, om den entryn �r minre �n en timme gammal.

            string articlesUrl = $"{_apiUrl}{LibraryConstants.ApiEndpoints.ArticlesByUser}/{userId}";
            Articles = await httpClient.GetFromJsonAsync<List<ArticleResponseDto>>(articlesUrl) ?? new();

            string unitsUrl = $"{_apiUrl}{LibraryConstants.ApiEndpoints.UnitsAll}";
            Units = await httpClient.GetFromJsonAsync<List<UnitResponseDto>>(unitsUrl) ?? new();
        }
        catch (Exception ex)
        {
            // TODO: Meddela anv�ndaren.
            _logger.LogError(ex, "Fail: Get stuff needed to create entry.");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            using var httpClient = new HttpClient();
            string url = _apiUrl + LibraryConstants.ApiEndpoints.EntryCreate;
            NewEntry.UserId = GetUserId();
            var response = await httpClient.PostAsJsonAsync(url, NewEntry);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            // TODO: Meddela anv�ndaren.
            _logger.LogError(ex, "Fail: Create entry.");
        }
        return RedirectToPage();
    }
}
