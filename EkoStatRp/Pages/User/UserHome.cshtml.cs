using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class UserHome : PageModelBase<UserHome>
{
    public UserHome(HttpHelper httpHelper, UserHelper userHelper, ILogger<UserHome> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
