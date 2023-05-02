using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages;

public class Home : PageModelBase<Home>
{
    public Home(HttpHelper httpHelper, UserHelper userHelper, ILogger<Home> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {

    }
}
