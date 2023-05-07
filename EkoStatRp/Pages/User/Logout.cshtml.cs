using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.User;

public class Logout : PageModelBase<Logout>
{
    public Logout(HttpHelper httpHelper, UserHelper userHelper, DtoHelper dtoHelper, ILogger<Logout> logger)
        : base(httpHelper, userHelper, dtoHelper, logger)
    {
    }

    public IActionResult OnGet()
    {
        _userHelper.Logout();
        return GoHome();
    }
}
