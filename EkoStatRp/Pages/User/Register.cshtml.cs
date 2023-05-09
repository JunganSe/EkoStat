using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class Register : PageModelBase<Register>
{
    public Register(HttpHelper httpHelper, UserHelper userHelper, ApiHandler apiHandler, DtoHelper dtoHelper, ILogger<Register> logger)
        : base(httpHelper, userHelper, apiHandler, dtoHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
