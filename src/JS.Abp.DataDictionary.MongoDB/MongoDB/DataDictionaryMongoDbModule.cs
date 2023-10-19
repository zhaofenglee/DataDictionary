using JS.Abp.DataDictionary.DataDictionaryItems;
using JS.Abp.DataDictionary.DataDictionaries;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace JS.Abp.DataDictionary.MongoDB;

[DependsOn(
    typeof(DataDictionaryDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class DataDictionaryMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<DataDictionaryMongoDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, MongoQuestionRepository>();
             */
            options.AddRepository<JS.Abp.DataDictionary.DataDictionaries.DataDictionary, DataDictionaries.MongoDataDictionaryRepository>();

            options.AddRepository<DataDictionaryItem, DataDictionaryItems.MongoDataDictionaryItemRepository>();

        });
    }
}