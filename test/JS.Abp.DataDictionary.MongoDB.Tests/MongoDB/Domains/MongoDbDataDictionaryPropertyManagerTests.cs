using JS.Abp.DataDictionary.DataDictionaries;
using Xunit;

namespace JS.Abp.DataDictionary.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDbDataDictionaryPropertyManagerTests: DataDictionaryPropertyManagerTests<DataDictionaryMongoDbTestModule>
{
    
}