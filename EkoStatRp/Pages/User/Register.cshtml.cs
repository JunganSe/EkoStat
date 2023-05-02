using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class Register : PageModelBase<Register>
{
    public Register(HttpHelper httpHelper, UserHelper userHelper, ILogger<Register> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
