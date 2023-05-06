using System.Security.Claims;

namespace EkoStatRp.Helpers;

public class UserHelper
{
    private readonly HttpHelper _httpHelper;

    public UserHelper(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    public void Login(int userId)
    {
        // TODO: Använd ClaimsPrincipal user, ta bort userId.
        _httpHelper.SetCookie(Constants.Cookies.UserId, userId.ToString());
    }

    public void Logout()
    {
        // TODO: Använd identiity.
        _httpHelper.DeleteCookie(Constants.Cookies.UserId);
    }

    public bool IsLoggedIn(ClaimsPrincipal user)
    {
        // TODO: Kontrollera med user.
        var userId = _httpHelper.GetCookie(Constants.Cookies.UserId);
        if (userId == null)
            return false;
        return true;
    }

    public int GetUserId(ClaimsPrincipal user)
    {
        // TODO: Kontrollera med user.
        var userIdData = _httpHelper.GetCookie(Constants.Cookies.UserId);
        if (int.TryParse(userIdData, out int id))
            return id;
        return 0;
    }
}
