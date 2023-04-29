namespace EkoStatRp.Helpers;

public class HttpHelper
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetApiUrl()
    {
        return _configuration.GetValue<string>(Constants.AppsettingsJsonNames.ApiUrl);
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
}
