using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class RegisterModel : PageModelBase<RegisterModel>
{
    public RegisterModel(HttpHelper httpHelper, UserHelper userHelper, ILogger<RegisterModel> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
