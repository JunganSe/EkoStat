namespace EkoStatRp.Helpers;

public class HttpHelper
{
    private readonly IConfiguration _configuration;

    public HttpHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetApiUrl()
    {
        return _configuration.GetValue<string>(Constants.AppsettingsJsonNames.ApiUrl);
    }
}
