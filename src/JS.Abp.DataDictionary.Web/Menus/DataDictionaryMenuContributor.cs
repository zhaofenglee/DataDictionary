using JS.Abp.DataDictionary.Permissions;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using JS.Abp.DataDictionary.Localization;
using Volo.Abp.Authorization.Permissions;

namespace JS.Abp.DataDictionary.Web.Menus;

public class DataDictionaryMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return;
        }

        var moduleMenu = AddModuleMenuItem(context); //Do not delete `moduleMenu` variable as it will be used by ABP Suite!

        AddMenuItemDataDictionaries(context, moduleMenu);
    }

    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<DataDictionaryResource>();

        var moduleMenu = new ApplicationMenuItem(
            DataDictionaryMenus.Prefix,
            displayName: l["Menu:DataDictionary"],
            icon: "fa fa-globe");

        //Add main menu items.
        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }
   

    private static void AddMenuItemDataDictionaries(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        var administration = context.Menu.GetAdministration();
        administration.AddItem(
            new ApplicationMenuItem(
                Menus.DataDictionaryMenus.DataDictionaries,
                context.GetLocalizer<DataDictionaryResource>()["Menu:DataDictionaries"],
                "/DataDictionary/DataDictionaries",
                icon: "fa fa-file-alt",
                requiredPermissionName: DataDictionaryPermissions.DataDictionaries.Default
            )
        );
    }
}