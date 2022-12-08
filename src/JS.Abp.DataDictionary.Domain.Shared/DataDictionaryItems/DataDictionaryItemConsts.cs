namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public static class DataDictionaryItemConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "DataDictionaryItem." : string.Empty);
        }

        public const int CodeMaxLength = 20;
        public const int DisplayTextMaxLength = 50;
        public const int DescriptionMaxLength = 200;
    }
}