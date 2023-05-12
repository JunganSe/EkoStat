using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages;

public class Home : PageModelBase<Home>
{
    public Home(HttpHelper httpHelper, UserHelper userHelper, ApiHandler apiHandler, ILogger<Home> logger)
        : base(httpHelper, userHelper, apiHandler, logger)
    {
    }

    public void OnGet()
    {

    }
}
