using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace JS.Abp.DataDictionary;

[DependsOn(
    typeof(DataDictionaryDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
    )]
public class DataDictionaryApplicationContractsModule : AbpModule
{

}
