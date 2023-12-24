using Volo.Abp.Modularity;

namespace JS.Abp.DataDictionary;

[DependsOn(
    typeof(DataDictionaryDomainModule),
    typeof(DataDictionaryTestBaseModule)
)]
public class DataDictionaryDomainTestModule : AbpModule
{

}
