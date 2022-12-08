using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace JS.Abp.DataDictionary;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DataDictionaryHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class DataDictionaryConsoleApiClientModule : AbpModule
{

}
