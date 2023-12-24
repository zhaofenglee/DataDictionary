using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace JS.Abp.DataDictionary.Blazor.Server;

[DependsOn(
    typeof(DataDictionaryBlazorModule),
    typeof(AbpAspNetCoreComponentsServerThemingModule)
    )]
public class DataDictionaryBlazorServerModule : AbpModule
{

}
