using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class Settings : PageModelBase<Settings>
{
    public Settings(HttpHelper httpHelper, UserHelper userHelper, ApiHandler apiHandler, DtoHelper dtoHelper, ILogger<Settings> logger)
        : base(httpHelper, userHelper, apiHandler, dtoHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
