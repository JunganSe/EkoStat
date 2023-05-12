using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.User;

public class Logout : PageModelBase<Logout>
{
    public Logout(HttpHelper httpHelper, UserHelper userHelper, ApiHandler apiHandler, ILogger<Logout> logger)
        : base(httpHelper, userHelper, apiHandler, logger)
    {
    }

    public IActionResult OnGet()
    {
        _userHelper.Logout();
        return GoHome();
    }
}
