﻿using Microsoft.Extensions.DependencyInjection;
using JS.Abp.DataDictionary.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace JS.Abp.DataDictionary.Blazor;

[DependsOn(
    typeof(DataDictionaryApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule)
    )]
public class DataDictionaryBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<DataDictionaryBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<DataDictionaryBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new DataDictionaryMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(DataDictionaryBlazorModule).Assembly);
        });
    }
}
