using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace JS.Abp.DataDictionary.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(DataDictionaryBlazorModule)
    )]
public class DataDictionaryBlazorServerModule : AbpModule
{

}
