using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatRp.Pages.User;

public class Login : PageModelBase<Login>
{
    public Login(HttpHelper httpHelper, UserHelper userHelper, DtoHelper dtoHelper, ILogger<Login> logger)
        : base(httpHelper, userHelper, dtoHelper, logger)
    {
    }

    public IActionResult OnGet()
    {
        _userHelper.Login(1);
        return GoHome();
    }
}
