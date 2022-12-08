using JS.Abp.DataDictionary.Localization;
using Volo.Abp.AspNetCore.Components;

namespace JS.Abp.DataDictionary.Blazor;

public abstract class DataDictionaryComponentBase : AbpComponentBase
{
    protected DataDictionaryComponentBase()
    {
        LocalizationResource = typeof(DataDictionaryResource);
    }
}
