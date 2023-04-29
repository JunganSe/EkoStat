using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class HomeModel : PageModelBase<HomeModel>
{
    public HomeModel(HttpHelper httpHelper, UserHelper userHelper, ILogger<HomeModel> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
