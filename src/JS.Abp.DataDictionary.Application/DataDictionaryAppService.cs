using JS.Abp.DataDictionary.Localization;
using Volo.Abp.Application.Services;

namespace JS.Abp.DataDictionary;

public abstract class DataDictionaryAppService : ApplicationService
{
    protected DataDictionaryAppService()
    {
        LocalizationResource = typeof(DataDictionaryResource);
        ObjectMapperContext = typeof(DataDictionaryApplicationModule);
    }
}
