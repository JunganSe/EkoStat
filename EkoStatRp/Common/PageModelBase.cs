using EkoStatLibrary.Helpers;
using EkoStatRp.Helpers;
using EkoStatRp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EkoStatRp.Common;

public class PageModelBase<T> : PageModel
{
    protected readonly HttpHelper _httpHelper;
    protected readonly UserHelper _userHelper;
    protected readonly DtoHelper _dtoHelper;
    protected readonly ILogger<T> _logger;
    protected readonly string _apiUrl;

    public MessageBox MessageBox { get; set; } = new MessageBox("");

    public PageModelBase(HttpHelper httpHelper, UserHelper userHelper, DtoHelper dtoHelper, ILogger<T> logger)
    {
        _httpHelper = httpHelper;
        _userHelper = userHelper;
        _dtoHelper = dtoHelper;
        _logger = logger;
        _apiUrl = _httpHelper.GetApiUrl();
    }

    protected int GetUserId()
    {
        return _userHelper.GetUserId(User);
    }

    protected bool IsLoggedIn()
    {
        return _userHelper.IsLoggedIn(User);
    }

    protected RedirectToPageResult GoHome()
    {
        string page = Constants.RazorPages.Home;
        return new RedirectToPageResult(page);
    }

    protected RedirectToPageResult GoLogin()
    {
        string page = Constants.RazorPages.Login;
        return new RedirectToPageResult(page);
    }
}
