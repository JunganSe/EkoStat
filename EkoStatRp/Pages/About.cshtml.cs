using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages;

public class About : PageModelBase<About>
{
    public About(HttpHelper httpHelper, UserHelper userHelper, ApiHandler apiHandler, ILogger<About> logger)
        : base(httpHelper, userHelper, apiHandler, logger)
    {
    }

    public void OnGet()
    {
    }
}
