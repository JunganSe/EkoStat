using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class Register : PageModelBase<Register>
{
    public Register(HttpHelper httpHelper, UserHelper userHelper, ApiHandler apiHandler, ILogger<Register> logger)
        : base(httpHelper, userHelper, apiHandler, logger)
    {
    }

    public void OnGet()
    {
    }
}
