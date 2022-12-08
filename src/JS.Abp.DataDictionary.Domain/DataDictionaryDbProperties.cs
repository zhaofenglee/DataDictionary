namespace JS.Abp.DataDictionary;

public static class DataDictionaryDbProperties
{
    public static string DbTablePrefix { get; set; } = "App";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "DataDictionary";
}
