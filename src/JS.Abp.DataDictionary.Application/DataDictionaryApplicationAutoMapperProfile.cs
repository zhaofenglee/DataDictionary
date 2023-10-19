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

CreateMap<DataDictionaryItemWithNavigationProperties, DataDictionaryItemDto>()
    .ForMember(dest => dest.Sequence, opt => opt.MapFrom(src => src.DataDictionaryItem.Sequence))
    .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.DataDictionaryItem.Code))
    .ForMember(dest => dest.DisplayText, opt => opt.MapFrom(src => src.DataDictionaryItem.DisplayText))
    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.DataDictionaryItem.Description))
    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.DataDictionaryItem.IsActive))
    .ForMember(dest => dest.DataDictionaryId, opt => opt.MapFrom(src => src.DataDictionaryItem.DataDictionaryId))
    .ForMember(dest => dest.ConcurrencyStamp, opt => opt.MapFrom(src => src.DataDictionaryItem.ConcurrencyStamp))
    .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.DataDictionary.IsDeleted))
    .ForMember(dest => dest.DeleterId, opt => opt.MapFrom(src => src.DataDictionary.DeleterId))
    .ForMember(dest => dest.DeletionTime, opt => opt.MapFrom(src => src.DataDictionary.DeletionTime))
    .ForMember(dest => dest.LastModificationTime, opt => opt.MapFrom(src => src.DataDictionary.LastModificationTime))
    .ForMember(dest => dest.LastModifierId, opt => opt.MapFrom(src => src.DataDictionary.LastModifierId))
    .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.DataDictionary.CreationTime))
    .ForMember(dest => dest.CreatorId, opt => opt.MapFrom(src => src.DataDictionary.CreatorId))
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DataDictionary.Id));
        CreateMap<JS.Abp.DataDictionary.DataDictionaries.DataDictionary, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayText));
    }
}
