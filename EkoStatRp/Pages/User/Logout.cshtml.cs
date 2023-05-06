using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.User;

public class Logout : PageModelBase<Logout>
{
    public Logout(HttpHelper httpHelper, UserHelper userHelper, ILogger<Logout> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public IActionResult OnGet()
    {
        _userHelper.Logout();
        return GoHome();
    }
}
