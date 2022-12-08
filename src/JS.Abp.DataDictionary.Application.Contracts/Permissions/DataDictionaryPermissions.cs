using Volo.Abp.Reflection;

namespace JS.Abp.DataDictionary.Permissions;

public class DataDictionaryPermissions
{
    public const string GroupName = "DataDictionary";
    public static class DataDictionaries
    {
        public const string Default = GroupName + ".DataDictionaries";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(DataDictionaryPermissions));
    }
}
