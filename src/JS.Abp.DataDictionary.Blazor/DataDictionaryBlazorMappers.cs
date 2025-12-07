using JS.Abp.DataDictionary.DataDictionaries;
using JS.Abp.DataDictionary.DataDictionaryItems;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace JS.Abp.DataDictionary.Blazor;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DataDictionaryDtoToDataDictionaryUpdateDtoMapper : MapperBase<DataDictionaryDto, DataDictionaryUpdateDto>
{
    public override partial DataDictionaryUpdateDto Map(DataDictionaryDto source);
    public override partial void Map(DataDictionaryDto source, DataDictionaryUpdateDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DataDictionaryItemDtoToDataDictionaryItemUpdateDtoMapper : MapperBase<DataDictionaryItemDto, DataDictionaryItemUpdateDto>
{
    public override partial DataDictionaryItemUpdateDto Map(DataDictionaryItemDto source);
    public override partial void Map(DataDictionaryItemDto source, DataDictionaryItemUpdateDto destination);
}

