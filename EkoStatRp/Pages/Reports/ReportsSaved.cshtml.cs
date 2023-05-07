using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.Reports;

public class ReportsSaved : PageModelBase<ReportsSaved>
{
    public ReportsSaved(HttpHelper httpHelper, UserHelper userHelper, ILogger<ReportsSaved> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
