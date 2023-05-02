using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages;

public class About : PageModelBase<About>
{
    public About(HttpHelper httpHelper, UserHelper userHelper, ILogger<About> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
