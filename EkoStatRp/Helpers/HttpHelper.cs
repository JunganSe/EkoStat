namespace EkoStatRp.Helpers;

public class HttpHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CookieOptions _cookieOptions;

    public HttpContext HttpContext => _httpContextAccessor.HttpContext 
        ?? throw new NullReferenceException(nameof(HttpContext));

    public HttpHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _cookieOptions = new CookieOptions()
        {
            Secure = true,
            Expires = DateTimeOffset.Now.AddDays(Constants.Cookies.CookieLifetimeDays)
        };
    }

    public void SetSessionData(string key, string value)
    {
        HttpContext.Session.SetString(key, value);
    }

    public string? GetSessionData(string key)
    {
        return HttpContext.Session.GetString(key);
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
