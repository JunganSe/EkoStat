using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EkoStatRp.Common;

public class PageModelBase<T> : PageModel
{
    protected readonly HttpHelper _httpHelper;
    protected readonly UserHelper _userHelper;
    protected readonly ILogger<T> _logger;
    protected readonly string _apiUrl;

    public PageModelBase(HttpHelper httpHelper, UserHelper userHelper, ILogger<T> logger)
    {
        _httpHelper = httpHelper;
        _userHelper = userHelper;
        _logger = logger;
        _apiUrl = _httpHelper.GetApiUrl();
    }

    protected int? GetUserId()
    {
        var userIdData = _httpHelper.GetSessionData(Constants.SessionData.UserId);
        if (int.TryParse(userIdData, out int id))
            return id;
        return null;
    }

    protected RedirectToPageResult GoHome()
    {
        string page = Constants.RazorPages.Home;
        return new RedirectToPageResult(page);
    }
}
