using EkoStatLibrary.Dtos;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.Tags;

[BindProperties]
public class IndexModel : PageModelBase<IndexModel>
{
    private readonly string? _userId;

    public List<TagResponseDto> Tags { get; set; } = new();
    public TagRequestDto NewTag { get; set; } = new();

    public IndexModel(HttpHelper httpHelper, UserHelper userHelper, ILogger<IndexModel> logger)
        : base(httpHelper, userHelper, logger)
    {
        _userId = _httpHelper.GetSessionData(Constants.SessionData.UserId);
    }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!_userHelper.IsLoggedIn(User))
                return GoHome();

            using var httpClient = new HttpClient();
            string url = $"{_apiUrl}{Constants.ApiEndpoints.TagsByUser}/{_userId}";
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
        // TODO: try-catch?
        // TODO: null check på NewTag
        using var httpClient = new HttpClient();
        string url = _apiUrl + Constants.ApiEndpoints.TagPost;
        NewTag.UserId = int.Parse(_userId!); // TODO: Bättre konvertering.
        var response = await httpClient.PostAsJsonAsync(url, NewTag);

        if (!response.IsSuccessStatusCode)
        {
            // TODO: Meddela användaren.
            return Page();
        }

        return RedirectToPage("/Tags/Index"); // Ladda om sidan.
    }
}
