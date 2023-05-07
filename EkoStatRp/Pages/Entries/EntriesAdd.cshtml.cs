using EkoStatLibrary.Dtos;
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

            NewEntry.Timestamp = DateTime.Today; // TODO: Använd timestamp från senaste entry, om den entryn är minre än en timme gammal.

            string articlesUrl = $"{_apiUrl}{LibraryConstants.ApiEndpoints.ArticlesByUser}/{userId}";
            Articles = await httpClient.GetFromJsonAsync<List<ArticleResponseDto>>(articlesUrl) ?? new();

            string unitsUrl = $"{_apiUrl}{LibraryConstants.ApiEndpoints.UnitsAll}";
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
            using var httpClient = new HttpClient();
            string url = _apiUrl + LibraryConstants.ApiEndpoints.EntryCreate;
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
        var dtr = DateTime.Parse($"{date} {time}");
        return dtr;
    }
}
