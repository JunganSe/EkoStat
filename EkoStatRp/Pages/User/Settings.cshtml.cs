using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class Settings : PageModelBase<Settings>
{
    public Settings(HttpHelper httpHelper, UserHelper userHelper, ApiHandler apiHandler, ILogger<Settings> logger)
        : base(httpHelper, userHelper, apiHandler, logger)
    {
    }

    public void OnGet()
    {
    }
}
