using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class Settings : PageModelBase<Settings>
{
    public Settings(HttpHelper httpHelper, UserHelper userHelper, DtoHelper dtoHelper, ILogger<Settings> logger)
        : base(httpHelper, userHelper, dtoHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
