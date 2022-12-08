using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace JS.Abp.DataDictionary.Web.Menus;

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
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(DataDictionaryMenus.Prefix, displayName: "DataDictionary", "~/DataDictionary", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
