using JS.Abp.DataDictionary.DataDictionaryItems;
using JS.Abp.DataDictionary.DataDictionaries;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace JS.Abp.DataDictionary.MongoDB;

[ConnectionStringName(DataDictionaryDbProperties.ConnectionStringName)]
public interface IDataDictionaryMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<DataDictionaryItem> DataDictionaryItems { get; }
    IMongoCollection<JS.Abp.DataDictionary.DataDictionaries.DataDictionary> DataDictionaries { get; }
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}