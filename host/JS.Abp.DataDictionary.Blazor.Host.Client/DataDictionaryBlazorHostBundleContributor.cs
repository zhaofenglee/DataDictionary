using Volo.Abp.Bundling;

namespace JS.Abp.DataDictionary.Blazor.Host.Client;

public class DataDictionaryBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
