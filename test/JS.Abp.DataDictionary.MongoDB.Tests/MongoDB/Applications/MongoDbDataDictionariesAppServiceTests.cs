using JS.Abp.DataDictionary.DataDictionaries;
using JS.Abp.DataDictionary.MongoDB;
using Xunit;

namespace JS.Abp.DataDictionary.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDbDataDictionariesAppServiceTests : DataDictionariesAppServiceTests<DataDictionaryMongoDbTestModule>
{

}
