using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace JS.Abp.DataDictionary.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class DataDictionaryBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "DataDictionary";
}
