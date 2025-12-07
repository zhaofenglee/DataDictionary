using JS.Abp.DataDictionary.DataDictionaries;
using JS.Abp.DataDictionary.DataDictionaryItems;
using JS.Abp.DataDictionary.Shared;
using Riok.Mapperly.Abstractions;
using System;
using Volo.Abp.Mapperly;

namespace JS.Abp.DataDictionary;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DataDictionaryToDataDictionaryDtoMapper : MapperBase<JS.Abp.DataDictionary.DataDictionaries.DataDictionary, DataDictionaryDto>
{
    public override partial DataDictionaryDto Map(JS.Abp.DataDictionary.DataDictionaries.DataDictionary source);
    public override partial void Map(JS.Abp.DataDictionary.DataDictionaries.DataDictionary source, DataDictionaryDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DataDictionaryToDataDictionaryExcelDtoMapper : MapperBase<JS.Abp.DataDictionary.DataDictionaries.DataDictionary, DataDictionaryExcelDto>
{
    public override partial DataDictionaryExcelDto Map(JS.Abp.DataDictionary.DataDictionaries.DataDictionary source);
    public override partial void Map(JS.Abp.DataDictionary.DataDictionaries.DataDictionary source, DataDictionaryExcelDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DataDictionaryItemToDataDictionaryItemDtoMapper : MapperBase<DataDictionaryItem, DataDictionaryItemDto>
{
    public override partial DataDictionaryItemDto Map(DataDictionaryItem source);
    public override partial void Map(DataDictionaryItem source, DataDictionaryItemDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DataDictionaryItemToDataDictionaryItemExcelDtoMapper : MapperBase<DataDictionaryItem, DataDictionaryItemExcelDto>
{
    public override partial DataDictionaryItemExcelDto Map(DataDictionaryItem source);
    public override partial void Map(DataDictionaryItem source, DataDictionaryItemExcelDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DataDictionaryItemWithNavigationPropertiesToDataDictionaryItemWithNavigationPropertiesDtoMapper : MapperBase<DataDictionaryItemWithNavigationProperties, DataDictionaryItemWithNavigationPropertiesDto>
{
    public override partial DataDictionaryItemWithNavigationPropertiesDto Map(DataDictionaryItemWithNavigationProperties source);
    public override partial void Map(DataDictionaryItemWithNavigationProperties source, DataDictionaryItemWithNavigationPropertiesDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DataDictionaryItemWithNavigationPropertiesToDataDictionaryItemDtoMapper : MapperBase<DataDictionaryItemWithNavigationProperties, DataDictionaryItemDto>
{
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionaryItem.Sequence), nameof(DataDictionaryItemDto.Sequence))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionaryItem.Code), nameof(DataDictionaryItemDto.Code))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionaryItem.DisplayText), nameof(DataDictionaryItemDto.DisplayText))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionaryItem.Description), nameof(DataDictionaryItemDto.Description))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionaryItem.IsActive), nameof(DataDictionaryItemDto.IsActive))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionaryItem.DataDictionaryId), nameof(DataDictionaryItemDto.DataDictionaryId))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionaryItem.ConcurrencyStamp), nameof(DataDictionaryItemDto.ConcurrencyStamp))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.IsDeleted), nameof(DataDictionaryItemDto.IsDeleted))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.DeleterId), nameof(DataDictionaryItemDto.DeleterId))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.DeletionTime), nameof(DataDictionaryItemDto.DeletionTime))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.LastModificationTime), nameof(DataDictionaryItemDto.LastModificationTime))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.LastModifierId), nameof(DataDictionaryItemDto.LastModifierId))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.CreationTime), nameof(DataDictionaryItemDto.CreationTime))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.CreatorId), nameof(DataDictionaryItemDto.CreatorId))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.Id), nameof(DataDictionaryItemDto.Id))]
    public override partial DataDictionaryItemDto Map(DataDictionaryItemWithNavigationProperties source);

    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionaryItem.Sequence), nameof(DataDictionaryItemDto.Sequence))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionaryItem.Code), nameof(DataDictionaryItemDto.Code))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionaryItem.DisplayText), nameof(DataDictionaryItemDto.DisplayText))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionaryItem.Description), nameof(DataDictionaryItemDto.Description))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionaryItem.IsActive), nameof(DataDictionaryItemDto.IsActive))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionaryItem.DataDictionaryId), nameof(DataDictionaryItemDto.DataDictionaryId))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionaryItem.ConcurrencyStamp), nameof(DataDictionaryItemDto.ConcurrencyStamp))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.IsDeleted), nameof(DataDictionaryItemDto.IsDeleted))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.DeleterId), nameof(DataDictionaryItemDto.DeleterId))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.DeletionTime), nameof(DataDictionaryItemDto.DeletionTime))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.LastModificationTime), nameof(DataDictionaryItemDto.LastModificationTime))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.LastModifierId), nameof(DataDictionaryItemDto.LastModifierId))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.CreationTime), nameof(DataDictionaryItemDto.CreationTime))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.CreatorId), nameof(DataDictionaryItemDto.CreatorId))]
    [MapProperty(nameof(DataDictionaryItemWithNavigationProperties.DataDictionary.Id), nameof(DataDictionaryItemDto.Id))]
    public override partial void Map(DataDictionaryItemWithNavigationProperties source, DataDictionaryItemDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DataDictionaryToLookupDtoMapper : MapperBase<JS.Abp.DataDictionary.DataDictionaries.DataDictionary, LookupDto<Guid?>>
{
    [MapProperty(nameof(JS.Abp.DataDictionary.DataDictionaries.DataDictionary.DisplayText), nameof(LookupDto<Guid?>.DisplayName))]
    public override partial LookupDto<Guid?> Map(JS.Abp.DataDictionary.DataDictionaries.DataDictionary source);

    [MapProperty(nameof(JS.Abp.DataDictionary.DataDictionaries.DataDictionary.DisplayText), nameof(LookupDto<Guid?>.DisplayName))]
    public override partial void Map(JS.Abp.DataDictionary.DataDictionaries.DataDictionary source, LookupDto<Guid?> destination);
}
