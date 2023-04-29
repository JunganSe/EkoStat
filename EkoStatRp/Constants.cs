namespace EkoStatRp;

internal static class Constants
{
    public static class AppsettingsJsonNames
    {
        public const string ApiUrl = "ApiUrl";
    }
    public static class RazorPages
    {
        public const string Home = "/Index";
    }
    public static class SessionData
    {
        public const string UserId = "UserId";
        public const string UserName = "UserName";
    }
    public static class ApiEndpoints
    {
        public const string TagsByUser = "/Tags/Minimal/ByUser";
        public const string TagPost = "/Tags";
    }
}
