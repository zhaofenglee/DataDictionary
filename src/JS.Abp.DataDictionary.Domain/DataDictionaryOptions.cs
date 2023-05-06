namespace JS.Abp.DataDictionary;

public class DataDictionaryOptions
{
    public int CacheExpirationTime { get;set; }
    
    public DataDictionaryOptions()
    {
        CacheExpirationTime = 10;
    }
}