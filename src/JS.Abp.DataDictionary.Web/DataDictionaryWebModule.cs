using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using JS.Abp.DataDictionary.Localization;
using JS.Abp.DataDictionary.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using JS.Abp.DataDictionary.Permissions;
using Volo.Abp.Mapperly;

namespace JS.Abp.DataDictionary.Web;

[DependsOn(
    typeof(DataDictionaryApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpMapperlyModule)
    )]
public class DataDictionaryWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(DataDictionaryResource), typeof(DataDictionaryWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DataDictionaryWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new DataDictionaryMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DataDictionaryWebModule>();
        });

        context.Services.AddMapperlyObjectMapper<DataDictionaryWebModule>();

        Configure<RazorPagesOptions>(options =>
        {
            //Configure authorization.
            options.Conventions.AuthorizePage("/DataDictionaryItems/Index", DataDictionaryPermissions.DataDictionaries.Default);
            options.Conventions.AuthorizePage("/DataDictionaries/Index", DataDictionaryPermissions.DataDictionaries.Default);
        });
    }
}