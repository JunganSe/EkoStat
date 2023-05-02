using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.Reports;

public class ReportsCreate : PageModelBase<ReportsCreate>
{
    public ReportsCreate(HttpHelper httpHelper, UserHelper userHelper, ILogger<ReportsCreate> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
