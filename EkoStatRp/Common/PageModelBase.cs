using EkoStatLibrary.Helpers;
using EkoStatRp.Helpers;
using EkoStatRp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace EkoStatRp.Common;

public class PageModelBase<TPageModel> : PageModel
{
    protected readonly HttpHelper _httpHelper;
    protected readonly UserHelper _userHelper;
    protected readonly DtoHelper _dtoHelper;
    protected readonly ILogger<TPageModel> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public MessageBox MessageBox { get; set; } = new MessageBox("");

    public PageModelBase(HttpHelper httpHelper, UserHelper userHelper, DtoHelper dtoHelper, ILogger<TPageModel> logger)
    {
        _httpHelper = httpHelper;
        _userHelper = userHelper;
        _dtoHelper = dtoHelper;
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
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

    protected void SetTempData(string key, object value)
    {
        string json = JsonSerializer.Serialize(value, _jsonOptions);
        TempData[key] = json;
    }

    protected T GetTempData<T>(string key, bool keepData = true)
    {
        try
        {
            string json = (keepData) 
                ? (string)(TempData.Peek(key) ?? "{}")
                : (string)(TempData[key] ?? "{}");
            return JsonSerializer.Deserialize<T>(json, _jsonOptions)!;
        }
        catch (Exception ex)
        {
            throw new JsonException($"Failed to deserialize 'TempData[{key}]' to type '{typeof(T)}'.", ex);
        }
    }
}
