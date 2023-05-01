using EkoStatLibrary.Dtos;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.Tags;

[BindProperties]
public class IndexModel : PageModelBase<IndexModel>
{
    public List<TagResponseDto> Tags { get; set; } = new();
    public TagRequestDto NewTag { get; set; } = new();

    public IndexModel(HttpHelper httpHelper, UserHelper userHelper, ILogger<IndexModel> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!_userHelper.IsLoggedIn(User))
                return GoHome();

            using var httpClient = new HttpClient();
            var userId = GetUserId();
            string url = $"{_apiUrl}{Constants.ApiEndpoints.TagsByUser}/{userId}";
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
            using var httpClient = new HttpClient();
            string url = _apiUrl + Constants.ApiEndpoints.TagPost;
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
