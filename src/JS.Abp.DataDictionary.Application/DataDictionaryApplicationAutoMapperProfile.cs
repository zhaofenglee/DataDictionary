using AutoMapper;
using JS.Abp.DataDictionary.DataDictionaries;
using JS.Abp.DataDictionary.DataDictionaryItems;
using JS.Abp.DataDictionary.Shared;
using System;

namespace JS.Abp.DataDictionary;

public class DataDictionaryApplicationAutoMapperProfile : Profile
{
    public DataDictionaryApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<JS.Abp.DataDictionary.DataDictionaries.DataDictionary, DataDictionaryDto>();
        CreateMap<JS.Abp.DataDictionary.DataDictionaries.DataDictionary, DataDictionaryExcelDto>();

        CreateMap<DataDictionaryItem, DataDictionaryItemDto>();
        CreateMap<DataDictionaryItem, DataDictionaryItemExcelDto>();

        CreateMap<DataDictionaryItemWithNavigationProperties, DataDictionaryItemWithNavigationPropertiesDto>();
        CreateMap<JS.Abp.DataDictionary.DataDictionaries.DataDictionary, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayText));
    }
}
