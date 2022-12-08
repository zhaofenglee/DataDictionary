using JS.Abp.DataDictionary.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace JS.Abp.DataDictionary.Pages;

public abstract class DataDictionaryPageModel : AbpPageModel
{
    protected DataDictionaryPageModel()
    {
        LocalizationResourceType = typeof(DataDictionaryResource);
    }
}
