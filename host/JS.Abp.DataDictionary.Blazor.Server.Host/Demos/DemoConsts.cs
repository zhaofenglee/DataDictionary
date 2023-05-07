namespace JS.Abp.DataDictionary.Blazor.Server.Host.Demos
{
    public static class DemoConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Demo." : string.Empty);
        }

        public const int NameMaxLength = 128;
        public const int DisplayNameMaxLength = 128;
    }
}