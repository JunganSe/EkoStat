using EkoStatLibrary.Helpers;
using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.Reports;

public class ReportsIndex : PageModelBase<ReportsIndex>
{
    public ReportsIndex(HttpHelper httpHelper, UserHelper userHelper, DtoHelper dtoHelper, ILogger<ReportsIndex> logger)
        : base(httpHelper, userHelper, dtoHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
