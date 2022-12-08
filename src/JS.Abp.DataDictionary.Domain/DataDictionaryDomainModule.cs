using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace JS.Abp.DataDictionary;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(DataDictionaryDomainSharedModule),
    typeof(AbpCachingModule)
)]
public class DataDictionaryDomainModule : AbpModule
{

}
