using AutoMapper;
using JS.Abp.DataDictionary.Blazor.Server.Host.Demos;
using JS.Abp.DataDictionary.DataDictionaries;
using JS.Abp.DataDictionary.DataDictionaryItems;
using JS.Abp.DataDictionary.Shared;
using System;

namespace JS.Abp.DataDictionary;

public class DataDictionaryHostAutoMapperProfile : Profile
{
    public DataDictionaryHostAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Demo, DemoDto>();
        CreateMap<Demo, DemoExcelDto>();
        CreateMap<DemoDto, DemoUpdateDto>();
    }
}
