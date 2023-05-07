using System.Collections.Generic;
using System.Threading.Tasks;
using JS.Abp.DataDictionary.Localization;
using JS.Abp.DataDictionary.MultiTenancy;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;

namespace JS.Abp.DataDictionary.Blazor.Server.Host.Menus;

public class DataDictionaryMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        var moduleMenu = AddModuleMenuItem(context);
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        return Task.CompletedTask;
    }
    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var moduleMenu = new ApplicationMenuItem(
            Menus.DataDictionaryMenus.Demos,
                context.GetLocalizer<DataDictionaryResource>()["Menu:Demos"],
                "/Demos",
                icon: "fa fa-file-alt"
        );

        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }

   
}
