using System.Security.Claims;

namespace EkoStatRp.Helpers;

public class UserHelper
{
    private readonly HttpHelper _httpHelper;

    public UserHelper(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    public bool IsLoggedIn(ClaimsPrincipal user)
    {
        // TODO: Kontrollera med user.
        var userId = _httpHelper.GetSessionData(Constants.SessionData.UserId);
        if (userId == null)
            return false;
        return true;
    }

    public int GetUserId(ClaimsPrincipal user)
    {
        // TODO: Kontrollera med user.
        var userIdData = _httpHelper.GetSessionData(Constants.SessionData.UserId);
        if (int.TryParse(userIdData, out int id))
            return id;
        return 0;
    }
}
