using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace JS.Abp.DataDictionary.Blazor.WebAssembly;

[DependsOn(
    typeof(DataDictionaryBlazorModule),
    typeof(DataDictionaryHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
)]
public class DataDictionaryBlazorWebAssemblyModule : AbpModule
{

}
