using JS.Abp.DataDictionary.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace JS.Abp.DataDictionary.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class DataDictionaryController : AbpControllerBase
{
    protected DataDictionaryController()
    {
        LocalizationResource = typeof(DataDictionaryResource);
    }
}
