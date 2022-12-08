using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace JS.Abp.DataDictionary;

[Dependency(ReplaceServices = true)]
public class DataDictionaryBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "DataDictionary";
}
