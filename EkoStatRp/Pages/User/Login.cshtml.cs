using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.User;

public class Login : PageModelBase<Login>
{
    public Login(HttpHelper httpHelper, UserHelper userHelper, ILogger<Login> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public IActionResult OnGet()
    {
        _userHelper.Login(1);
        return GoHome();
    }
}
