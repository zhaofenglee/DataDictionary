using JS.Abp.DataDictionary.Blazor.Server.Host.Demos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace JS.Abp.DataDictionary.Blazor.Server.Host;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DemoToDemoDtoMapper : MapperBase<Demo, DemoDto>
{
    public override partial DemoDto Map(Demo source);
    public override partial void Map(Demo source, DemoDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DemoToDemoExcelDtoMapper : MapperBase<Demo, DemoExcelDto>
{
    public override partial DemoExcelDto Map(Demo source);
    public override partial void Map(Demo source, DemoExcelDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DemoDtoToDemoUpdateDtoMapper : MapperBase<DemoDto, DemoUpdateDto>
{
    public override partial DemoUpdateDto Map(DemoDto source);
    public override partial void Map(DemoDto source, DemoUpdateDto destination);
}

