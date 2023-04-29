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

    public IndexModel(HttpHelper httpHelper, UserHelper userHelper, ILogger<IndexModel> logger)
        :base(httpHelper, userHelper, logger)
    {
        _userId = _httpHelper.GetSessionData(Constants.SessionData.UserId);
    }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            //if (!_userHelper.IsLoggedIn(User))
            //    return RedirectToPage("/Index");

            using var httpClient = new HttpClient();
            string url = _apiUrl + $"{Constants.ApiEndpoints.TagsByUser}/{_userId}";
            Tags = await httpClient.GetFromJsonAsync<List<TagResponseDto>>(url) ?? new();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");
            return Page();
        }
    }
}
