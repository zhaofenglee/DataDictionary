using System;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace JS.Abp.DataDictionary.DataDictionaries;

public abstract class DataDictionaryPropertyManagerTests<TStartupModule> : DataDictionaryDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly DataDictionaryPropertyManager _dataDictionaryPropertyManager;

    protected DataDictionaryPropertyManagerTests()
    {
        _dataDictionaryPropertyManager = GetRequiredService<DataDictionaryPropertyManager>();
    }

    [Fact]
    public async Task GetAsync()
    {
        //Act
        var result = await _dataDictionaryPropertyManager.GetAsync(new DemoTest()
        {
            Name = "001",
            DisplayName = "1"
        });
        // Assert
        result.DisplayName.ShouldBe("B001");
    }
}
