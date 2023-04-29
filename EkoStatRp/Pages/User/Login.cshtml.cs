using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class LoginModel : PageModelBase<LoginModel>
{
    public LoginModel(HttpHelper httpHelper, UserHelper userHelper, ILogger<LoginModel> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
        HttpContext.Session.SetString(Constants.SessionData.UserId, "1");
    }
}
