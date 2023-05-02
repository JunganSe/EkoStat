using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class Login : PageModelBase<Login>
{
    public Login(HttpHelper httpHelper, UserHelper userHelper, ILogger<Login> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
        HttpContext.Session.SetString(Constants.SessionData.UserId, "1");
    }
}
