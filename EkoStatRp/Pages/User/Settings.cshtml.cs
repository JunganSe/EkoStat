using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class Settings : PageModelBase<Settings>
{
    public Settings(HttpHelper httpHelper, UserHelper userHelper, ILogger<Settings> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
