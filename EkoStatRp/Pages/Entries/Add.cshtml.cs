using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.Entries;

public class AddModel : PageModelBase<AddModel>
{
    public AddModel(HttpHelper httpHelper, UserHelper userHelper, ILogger<AddModel> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
