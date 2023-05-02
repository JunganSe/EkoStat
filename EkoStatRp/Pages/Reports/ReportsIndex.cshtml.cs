using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.Reports;

public class ReportsIndex : PageModelBase<ReportsIndex>
{
    public ReportsIndex(HttpHelper httpHelper, UserHelper userHelper, ILogger<ReportsIndex> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
