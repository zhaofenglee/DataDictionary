using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace JS.Abp.DataDictionary;

[DependsOn(
    typeof(DataDictionaryDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
    )]
public class DataDictionaryApplicationContractsModule : AbpModule
{

}
