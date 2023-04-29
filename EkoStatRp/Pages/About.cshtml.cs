using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages;

public class AboutModel : PageModelBase<AboutModel>
{
    public AboutModel(HttpHelper httpHelper, UserHelper userHelper, ILogger<AboutModel> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
