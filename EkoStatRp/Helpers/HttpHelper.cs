namespace EkoStatRp.Helpers;

public class HttpHelper
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CookieOptions _cookieOptions;

    public HttpContext HttpContext => _httpContextAccessor.HttpContext 
        ?? throw new NullReferenceException(nameof(HttpContext));

    public HttpHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _cookieOptions = new CookieOptions()
        {
            Secure = true,
            Expires = DateTimeOffset.Now.AddDays(Constants.Cookies.CookieLifetimeDays)
        };
    }

    public string GetApiUrl()
    {
        return _configuration.GetValue<string>(Constants.AppsettingsKeys.ApiUrl);
    }

    public void SetSessionData(string key, string value)
    {
        if (_httpContextAccessor.HttpContext == null)
            throw new NullReferenceException(nameof(HttpContext));
        _httpContextAccessor.HttpContext.Session.SetString(key, value);
    }

    public string? GetSessionData(string key)
    {
        if (_httpContextAccessor.HttpContext == null)
            throw new NullReferenceException(nameof(HttpContext));
        return _httpContextAccessor.HttpContext.Session.GetString(key);
    }

    public void SetCookie(string key, string value)
    {
        HttpContext.Response.Cookies.Append(key, value, _cookieOptions);
    }

    public string? GetCookie(string key)
    {
        HttpContext.Request.Cookies.TryGetValue(key, out string? value);
        return value; // Value blir null om kakan inte hittades.
    }

    public void DeleteCookie(string key)
    {
        HttpContext.Response.Cookies.Delete(key);
    }
}
