namespace EkoStatLibrary;

public static class Constants
{
    public static class Http
    {
        public const int TimeoutSeconds = 10;
    }
    public static class ApiEndpoints
    {
        public const string ArticleGet = "Articles/";
        public const string ArticlesByUser = "Articles/ByUser/";
        public const string ArticlesByEntry = "Articles/ByEntry/";
        public const string ArticlesByTag = "Articles/ByTag/";
        public const string ArticleCreate = "Articles/";
        public const string ArticleCreateMultiple = "Articles/Multiple/";
        public const string ArticleUpdate = "Articles/";
        public const string ArticleDelete = "Articles/";

        public const string EntryGet = "Entries/";
        public const string EntriesByArticle = "Entries/ByArticle/";
        public const string EntriesByTag = "Entries/ByTag/";
        public const string EntriesByUser = "Entries/ByUser/";
        public const string EntryLatestByUser = "Entries/LatestByUser/";
        public const string EntriesFiltered = "Entries/Filtered/";
        public const string EntryCreate = "Entries/";
        public const string EntryCreateMultiple = "Entries/Multiple/";
        public const string EntryUpdate = "Entries/";
        public const string EntryDelete = "Entries/";

        public const string TagGet = "Tags/";
        public const string TagsByArticle = "Tags/ByArticle/";
        public const string TagsByUser = "Tags/ByUser/";
        public const string TagCreate = "Tags/";
        public const string TagCreateMultiple = "Tags/Multiple/";
        public const string TagUpdate = "Tags/";
        public const string TagDelete = "Tags/";

        public const string UnitGet = "Units/";
        public const string UnitsAll = "Units/All/";
        public const string UnitCreate = "Units/";
        public const string UnitCreateMultiple = "Units/Multiple/";
        public const string UnitUpdate = "Units/";
        public const string UnitDelete = "Units/";

        public const string UserGet = "Users/";
        public const string UserGetAll = "Users/All/";
        public const string UserCreate = "Users/";
        public const string UserUpdate = "Users/";
        public const string UserDelete = "Users/";
    }
}
