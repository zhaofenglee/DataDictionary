using JS.Abp.DataDictionary.DataDictionaries;
using JS.Abp.DataDictionary.DataDictionaryItems;
using JS.Abp.DataDictionary.Web.Pages.DataDictionary.DataDictionaries;
using JS.Abp.DataDictionary.Web.Pages.DataDictionary.DataDictionaryItems;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace JS.Abp.DataDictionary.Web;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DataDictionaryItemDtoToDataDictionaryItemUpdateViewModelMapper : MapperBase<DataDictionaryItemDto, DataDictionaryItemUpdateViewModel>
{
    public override partial DataDictionaryItemUpdateViewModel Map(DataDictionaryItemDto source);
    public override partial void Map(DataDictionaryItemDto source, DataDictionaryItemUpdateViewModel destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DataDictionaryItemUpdateViewModelToDataDictionaryItemUpdateDtoMapper : MapperBase<DataDictionaryItemUpdateViewModel, DataDictionaryItemUpdateDto>
{
    public override partial DataDictionaryItemUpdateDto Map(DataDictionaryItemUpdateViewModel source);
    public override partial void Map(DataDictionaryItemUpdateViewModel source, DataDictionaryItemUpdateDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DataDictionaryItemCreateViewModelToDataDictionaryItemCreateDtoMapper : MapperBase<DataDictionaryItemCreateViewModel, DataDictionaryItemCreateDto>
{
    public override partial DataDictionaryItemCreateDto Map(DataDictionaryItemCreateViewModel source);
    public override partial void Map(DataDictionaryItemCreateViewModel source, DataDictionaryItemCreateDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DataDictionaryDtoToDataDictionaryUpdateViewModelMapper : MapperBase<DataDictionaryDto, DataDictionaryUpdateViewModel>
{
    public override partial DataDictionaryUpdateViewModel Map(DataDictionaryDto source);
    public override partial void Map(DataDictionaryDto source, DataDictionaryUpdateViewModel destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DataDictionaryUpdateViewModelToDataDictionaryUpdateDtoMapper : MapperBase<DataDictionaryUpdateViewModel, DataDictionaryUpdateDto>
{
    public override partial DataDictionaryUpdateDto Map(DataDictionaryUpdateViewModel source);
    public override partial void Map(DataDictionaryUpdateViewModel source, DataDictionaryUpdateDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DataDictionaryCreateViewModelToDataDictionaryCreateDtoMapper : MapperBase<DataDictionaryCreateViewModel, DataDictionaryCreateDto>
{
    public override partial DataDictionaryCreateDto Map(DataDictionaryCreateViewModel source);
    public override partial void Map(DataDictionaryCreateViewModel source, DataDictionaryCreateDto destination);
}

