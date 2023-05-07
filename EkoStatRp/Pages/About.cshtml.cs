using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages;

public class About : PageModelBase<About>
{
    public About(HttpHelper httpHelper, UserHelper userHelper, DtoHelper dtoHelper, ILogger<About> logger)
        : base(httpHelper, userHelper, dtoHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
