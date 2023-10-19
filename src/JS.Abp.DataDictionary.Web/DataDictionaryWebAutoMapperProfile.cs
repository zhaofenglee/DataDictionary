using JS.Abp.DataDictionary.Web.Pages.DataDictionary.DataDictionaries;
using JS.Abp.DataDictionary.DataDictionaries;
using JS.Abp.DataDictionary.Web.Pages.DataDictionary.DataDictionaryItems;
using Volo.Abp.AutoMapper;
using JS.Abp.DataDictionary.DataDictionaryItems;
using AutoMapper;

namespace JS.Abp.DataDictionary.Web;

public class DataDictionaryWebAutoMapperProfile : Profile
{
    public DataDictionaryWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<DataDictionaryItemDto, DataDictionaryItemUpdateViewModel>();
        CreateMap<DataDictionaryItemUpdateViewModel, DataDictionaryItemUpdateDto>();
        CreateMap<DataDictionaryItemCreateViewModel, DataDictionaryItemCreateDto>();

        CreateMap<DataDictionaryDto, DataDictionaryUpdateViewModel>();
        CreateMap<DataDictionaryUpdateViewModel, DataDictionaryUpdateDto>();
        CreateMap<DataDictionaryCreateViewModel, DataDictionaryCreateDto>();
    }
}