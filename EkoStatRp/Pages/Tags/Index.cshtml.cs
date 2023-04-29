using EkoStatLibrary.Dtos;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EkoStatRp.Pages.Tags;

[BindProperties]
public class IndexModel : PageModel
{
    private readonly HttpHelper _httpHelper;
    private readonly UserHelper _userHelper;
    private readonly ILogger<IndexModel> _logger;
    private readonly string _apiUrl;
    private readonly string? _userId;

    public List<TagResponseDto> Tags { get; set; } = new();

    public IndexModel(HttpHelper httpHelper, UserHelper userHelper, ILogger<IndexModel> logger)
    {
        _httpHelper = httpHelper;
        _userHelper = userHelper;
        _logger = logger;
        _apiUrl = _httpHelper.GetApiUrl();
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
