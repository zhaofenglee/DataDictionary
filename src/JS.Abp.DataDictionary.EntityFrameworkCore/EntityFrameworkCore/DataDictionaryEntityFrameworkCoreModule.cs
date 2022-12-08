using JS.Abp.DataDictionary.DataDictionaryItems;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace JS.Abp.DataDictionary.EntityFrameworkCore;

[DependsOn(
    typeof(DataDictionaryDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class DataDictionaryEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<DataDictionaryDbContext>(options =>
        {
            options.AddRepository<JS.Abp.DataDictionary.DataDictionaries.DataDictionary, DataDictionaries.EfCoreDataDictionaryRepository>();

            options.AddRepository<DataDictionaryItem, DataDictionaryItems.EfCoreDataDictionaryItemRepository>();
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
        });
    }
}
