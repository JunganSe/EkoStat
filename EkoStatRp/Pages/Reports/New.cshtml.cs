using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.Reports;

public class NewModel : PageModelBase<NewModel>
{
    public NewModel(HttpHelper httpHelper, UserHelper userHelper, ILogger<NewModel> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
