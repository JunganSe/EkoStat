using EkoStatLibrary.Dtos;
using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.Tags;

[BindProperties]
public class TagsIndex : PageModelBase<TagsIndex>
{
    public List<TagResponseDto> Tags { get; set; } = new();
    public TagRequestDto NewTag { get; set; } = new();

    public TagsIndex(HttpHelper httpHelper, UserHelper userHelper, ApiHandler apiHandler, DtoHelper dtoHelper, ILogger<TagsIndex> logger)
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

            string url = LibraryConstants.ApiEndpoints.TagsByUser + userId;
            Tags = await httpClient.GetFromJsonAsync<List<TagResponseDto>>(url) ?? new();
        }
        catch (Exception ex)
        {
            // TODO: Meddela användaren.
            _logger.LogError(ex, "Fail: Get Tags.");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            using var httpClient = _httpHelper.GetHttpClient();
            string url = LibraryConstants.ApiEndpoints.TagCreate;

            NewTag.UserId = GetUserId();
            var response = await httpClient.PostAsJsonAsync(url, NewTag);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            // TODO: Meddela användaren.
            _logger.LogError(ex, "Fail: Create tag.");
        }
        return RedirectToPage();
    }
}
