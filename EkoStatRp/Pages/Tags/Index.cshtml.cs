using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EkoStatRp.Pages.Tags;

public class IndexModel : PageModel
{
    private readonly HttpHelper _httpHelper;
    private readonly string _apiUrl;
    private readonly string? _userId;

    public IndexModel(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
        _apiUrl = _httpHelper.GetApiUrl();
        _userId = _httpHelper.GetSessionData(Constants.SessionData.UserId);
    }

    public async Task OnGetAsync()
    {
        using var httpClient = new HttpClient();
        string url = _apiUrl + Constants.ApiEndpoints.TagsByUser + _userId;
    }
}
