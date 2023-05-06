using EkoStatLibrary.Dtos;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.Entries;

[BindProperties]
public class EntriesIndex : PageModelBase<EntriesIndex>
{
    private readonly DtoHelper _dtoHelper;

    public List<EntryGroup> EntryGroups { get; set; } = new();

    public EntriesIndex(HttpHelper httpHelper, UserHelper userHelper, DtoHelper dtoHelper, ILogger<EntriesIndex> logger)
        : base(httpHelper, userHelper, logger)
    {
        _dtoHelper = dtoHelper;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!IsLoggedIn())
                return GoHome();

            using var httpClient = new HttpClient();
            var userId = GetUserId();
            string url = $"{_apiUrl}{LibraryConstants.ApiEndpoints.EntriesByUser}/{userId}";
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
}
