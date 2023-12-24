using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace JS.Abp.DataDictionary;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpCachingModule),
    typeof(DataDictionaryDomainSharedModule)
)]
public class DataDictionaryDomainModule : AbpModule
{

}
