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

    public static class PartialViews
    {
        public const string NavigationMenu = "_NavigationMenuPartial";
        public const string EditButtons = "_EditButtonsPartial";
        public const string ValidationScripts = "_ValidationScriptsPartial";
        public const string MessageBox = "_MessageBoxPartial";
    }

    public static class Cookies
    {
        public const int CookieLifetimeDays = 30;
        public const string UserId = "UserId";
    }

    public static class FontAwesome
    {
        public const string Edit = "fa-regular fa-pen-to-square";
        public const string Delete = "fa-regular fa-trash-can";
    }
}
