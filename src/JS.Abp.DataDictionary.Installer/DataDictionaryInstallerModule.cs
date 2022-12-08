using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace JS.Abp.DataDictionary;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class DataDictionaryInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DataDictionaryInstallerModule>();
        });
    }
}
