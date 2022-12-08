using AutoMapper;
using JS.Abp.DataDictionary.DataDictionaries;
using JS.Abp.DataDictionary.DataDictionaryItems;

namespace JS.Abp.DataDictionary.Blazor;

public class DataDictionaryBlazorAutoMapperProfile : Profile
{
    public DataDictionaryBlazorAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<DataDictionaryDto, DataDictionaryUpdateDto>();

        CreateMap<DataDictionaryItemDto, DataDictionaryItemUpdateDto>();
    }
}
