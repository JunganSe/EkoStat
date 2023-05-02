using EkoStatRp.Common;
using EkoStatRp.Helpers;

namespace EkoStatRp.Pages.Entries;

public class EntriesAdd : PageModelBase<EntriesAdd>
{
    public EntriesAdd(HttpHelper httpHelper, UserHelper userHelper, ILogger<EntriesAdd> logger)
        : base(httpHelper, userHelper, logger)
    {
    }

    public void OnGet()
    {
    }
}
