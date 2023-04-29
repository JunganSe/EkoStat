using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class LogoutModel : PageModelBase<LogoutModel>
{
    public LogoutModel(HttpHelper httpHelper, UserHelper userHelper, ILogger<LogoutModel> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
        HttpContext.Session.Remove(Constants.SessionData.UserId);
    }
}
