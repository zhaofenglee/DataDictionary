using JS.Abp.DataDictionary.Localization;
using JS.Abp.DataDictionary.Permissions;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace JS.Abp.DataDictionary.Blazor.Menus;

public class DataDictionaryMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<DataDictionaryResource>();
        //Add main menu items.
        //context.Menu.AddItem(new ApplicationMenuItem(DataDictionaryMenus.Prefix, displayName: "DataDictionary", "/DataDictionary", icon: "fa fa-globe"));
        
        var administration = context.Menu.GetAdministration();
        administration.AddItem(
          new ApplicationMenuItem(
               DataDictionaryMenus.DataDictionaries,
               l["Menu:DataDictionaries"],
               url: "/data-dictionaries",
               icon: "fa fa-file-alt",
               requiredPermissionName: DataDictionaryPermissions.DataDictionaries.Default,
               order:10)
       );

        return Task.CompletedTask;
    }
}
