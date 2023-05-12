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

    public EntriesAdd(HttpHelper httpHelper, UserHelper userHelper, ApiHandler apiHandler, ILogger<EntriesAdd> logger)
        : base(httpHelper, userHelper, apiHandler, logger)
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
            Units = await _apiHandler.GetAllUnitsAsync();
            var latestEntry = await _apiHandler.GetLatestEntryByUserAsync(userId);
            NewEntry.Timestamp = latestEntry?.Timestamp ?? DateTime.Today;
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
            NewEntry.Timestamp = GetFormTimestamp();
            NewEntry.UserId = GetUserId();
            var response = await _apiHandler.CreateEntryAsync(NewEntry);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            // TODO: Meddela anv�ndaren.
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
