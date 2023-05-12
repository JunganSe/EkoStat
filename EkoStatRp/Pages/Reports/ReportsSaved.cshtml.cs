using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.Reports;

public class ReportsSaved : PageModelBase<ReportsSaved>
{
    public ReportsSaved(HttpHelper httpHelper, UserHelper userHelper, ApiHandler apiHandler, ILogger<ReportsSaved> logger)
        : base(httpHelper, userHelper, apiHandler, logger)
    {
    }

    public void OnGet()
    {
    }
}
