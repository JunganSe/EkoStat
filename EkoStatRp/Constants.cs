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
        public const string EntriesIndex = "/Entries/EntriesIndex";
        public const string EntriesAdd = "/Entries/EntriesAdd";
        public const string ArticlesIndex = "/Articles/ArticlesIndex";
        public const string TagsIndex = "/Tags/TagsIndex";
        public const string ReportsIndex = "/Reports/ReportsIndex";
        public const string ReportsCreate = "/Reports/ReportsCreate";
        public const string Settings = "/User/Settings";
        public const string About = "/About";
        public const string Register = "/User/Register";
        public const string Login = "/User/Login";
        public const string Logout = "/User/Logout";
    }
    public static class SessionData
    {
        public const string UserId = "UserId";
        public const string UserName = "UserName";
    }
    public static class ApiEndpoints
    {
        public const string ArticlesByUser = "/Articles/ByUser";
        public const string ArticleCreate = "/Articles";

        public const string EntriesByUser = "/Entries/ByUser";

        public const string TagsByUser = "/Tags/Minimal/ByUser";
        public const string TagCreate = "/Tags";
    }
}
