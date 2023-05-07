using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.Reports;

public class ReportsSaved : PageModelBase<ReportsSaved>
{
    public ReportsSaved(HttpHelper httpHelper, UserHelper userHelper, DtoHelper dtoHelper, ILogger<ReportsSaved> logger)
        : base(httpHelper, userHelper, dtoHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
