using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages;

public class Home : PageModelBase<Home>
{
    public Home(HttpHelper httpHelper, UserHelper userHelper, DtoHelper dtoHelper, ILogger<Home> logger)
        : base(httpHelper, userHelper, dtoHelper, logger)
    {
    }

    public void OnGet()
    {

    }
}
