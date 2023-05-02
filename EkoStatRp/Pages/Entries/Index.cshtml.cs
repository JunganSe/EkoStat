using EkoStatLibrary.Dtos;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.Entries;

[BindProperties]
public class IndexModel : PageModelBase<IndexModel>
{
    public List<EntryResponseDto> Entries { get; set; } = new();

    public IndexModel(HttpHelper httpHelper, UserHelper userHelper, ILogger<IndexModel> logger)
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
            string url = $"{_apiUrl}{Constants.ApiEndpoints.EntriesByUser}/{userId}";
            Entries = await httpClient.GetFromJsonAsync<List<EntryResponseDto>>(url) ?? new();
        }
        catch (Exception ex)
        {
            // TODO: Meddela användaren.
            _logger.LogError(ex, "Fail: Get Entries.");
        }
        return Page();
    }
}
