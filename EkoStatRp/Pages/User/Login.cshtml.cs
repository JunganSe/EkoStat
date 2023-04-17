using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EkoStatRp.Pages.User;

public class LoginModel : PageModel
{
    public void OnGet()
    {
        HttpContext.Session.SetString(Constants.Session.UserId, "1");
    }
}
