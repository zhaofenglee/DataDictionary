using JS.Abp.DataDictionary.DataDictionaryItems;
using JS.Abp.DataDictionary.DataDictionaries;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace JS.Abp.DataDictionary.MongoDB;

[ConnectionStringName(DataDictionaryDbProperties.ConnectionStringName)]
public class DataDictionaryMongoDbContext : AbpMongoDbContext, IDataDictionaryMongoDbContext
{
    public IMongoCollection<DataDictionaries.DataDictionary> DataDictionaries => Collection<DataDictionaries.DataDictionary>();
    public IMongoCollection<DataDictionaryItem> DataDictionaryItems => Collection<DataDictionaryItem>();
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureDataDictionary();

        modelBuilder.Entity<JS.Abp.DataDictionary.DataDictionaries.DataDictionary>(b => { b.CollectionName = DataDictionaryDbProperties.DbTablePrefix + "DataDictionaries"; });

        modelBuilder.Entity<DataDictionaryItem>(b => { b.CollectionName = DataDictionaryDbProperties.DbTablePrefix + "DataDictionaryItems"; });
    }
}