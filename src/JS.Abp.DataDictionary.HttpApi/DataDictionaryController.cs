using JS.Abp.DataDictionary.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace JS.Abp.DataDictionary;

public abstract class DataDictionaryController : AbpControllerBase
{
    protected DataDictionaryController()
    {
        LocalizationResource = typeof(DataDictionaryResource);
    }
}
