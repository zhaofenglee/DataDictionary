using JS.Abp.DataDictionary.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace JS.Abp.DataDictionary.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class DataDictionaryPageModel : AbpPageModel
{
    protected DataDictionaryPageModel()
    {
        LocalizationResourceType = typeof(DataDictionaryResource);
        ObjectMapperContext = typeof(DataDictionaryWebModule);
    }
}
