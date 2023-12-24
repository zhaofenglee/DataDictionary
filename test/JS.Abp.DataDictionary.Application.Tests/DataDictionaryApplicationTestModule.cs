using Volo.Abp.Modularity;

namespace JS.Abp.DataDictionary;

[DependsOn(
    typeof(DataDictionaryApplicationModule),
    typeof(DataDictionaryDomainTestModule)
    )]
public class DataDictionaryApplicationTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
       
    }
}
