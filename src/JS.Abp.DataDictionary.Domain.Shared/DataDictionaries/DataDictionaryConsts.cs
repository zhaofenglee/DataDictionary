namespace JS.Abp.DataDictionary.DataDictionaries
{
    public static class DataDictionaryConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "DataDictionary." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;
        public const int DisplayTextMinLength = 1;
        public const int DisplayTextMaxLength = 50;
        public const int DescriptionMaxLength = 50;
    }
}