using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class Logout : PageModelBase<Logout>
{
    public Logout(HttpHelper httpHelper, UserHelper userHelper, ILogger<Logout> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
        HttpContext.Session.Remove(Constants.SessionData.UserId);
    }
}
