using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EkoStatRp.Pages.User;

public class LogoutModel : PageModel
{
    public void OnGet()
    {
        HttpContext.Session.Remove(Constants.Session.UserId);
    }
}
