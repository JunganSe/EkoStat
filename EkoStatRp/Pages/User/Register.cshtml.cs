using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class Register : PageModelBase<Register>
{
    public Register(HttpHelper httpHelper, UserHelper userHelper, DtoHelper dtoHelper, ILogger<Register> logger)
        : base(httpHelper, userHelper, dtoHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
