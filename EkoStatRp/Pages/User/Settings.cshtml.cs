using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.User;

public class SettingsModel : PageModelBase<SettingsModel>
{
    public SettingsModel(HttpHelper httpHelper, UserHelper userHelper, ILogger<SettingsModel> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
