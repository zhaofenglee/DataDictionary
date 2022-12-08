using JS.Abp.DataDictionary.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace JS.Abp.DataDictionary.Permissions;

public class DataDictionaryPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DataDictionaryPermissions.GroupName, L("Permission:DataDictionary"));

        var dataDictionaryPermission = myGroup.AddPermission(DataDictionaryPermissions.DataDictionaries.Default, L("Permission:DataDictionaries"));
        dataDictionaryPermission.AddChild(DataDictionaryPermissions.DataDictionaries.Create, L("Permission:Create"));
        dataDictionaryPermission.AddChild(DataDictionaryPermissions.DataDictionaries.Edit, L("Permission:Edit"));
        dataDictionaryPermission.AddChild(DataDictionaryPermissions.DataDictionaries.Delete, L("Permission:Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DataDictionaryResource>(name);
    }
}
