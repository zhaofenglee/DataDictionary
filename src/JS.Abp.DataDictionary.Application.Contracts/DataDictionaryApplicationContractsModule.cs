using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace JS.Abp.DataDictionary;

[DependsOn(
    typeof(DataDictionaryDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class DataDictionaryApplicationContractsModule : AbpModule
{

}
