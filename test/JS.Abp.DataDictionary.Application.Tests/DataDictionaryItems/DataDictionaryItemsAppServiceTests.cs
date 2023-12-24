using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public abstract class DataDictionaryItemsAppServiceTests <TStartupModule> : DataDictionaryApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IDataDictionaryItemsAppService _dataDictionaryItemsAppService;
        private readonly IRepository<DataDictionaryItem, Guid> _dataDictionaryItemRepository;

        public DataDictionaryItemsAppServiceTests()
        {
            _dataDictionaryItemsAppService = GetRequiredService<IDataDictionaryItemsAppService>();
            _dataDictionaryItemRepository = GetRequiredService<IRepository<DataDictionaryItem, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _dataDictionaryItemsAppService.GetListAsync(new GetDataDictionaryItemsInput());

            // Assert
            result.TotalCount.ShouldBe(4);
            result.Items.Count.ShouldBe(4);
            result.Items.Any(x => x.DataDictionaryItem.Id == Guid.Parse("65a8377e-c1f2-4eae-8ed2-952842893d23")).ShouldBe(true);
            result.Items.Any(x => x.DataDictionaryItem.Id == Guid.Parse("46696fd3-74fd-48c9-a9dd-51d2b1b21b63")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _dataDictionaryItemsAppService.GetAsync(Guid.Parse("65a8377e-c1f2-4eae-8ed2-952842893d23"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("65a8377e-c1f2-4eae-8ed2-952842893d23"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new DataDictionaryItemCreateDto
            {
                Sequence = 1557718653,
                Code = "fbb5620525b0402eb3fa",
                DisplayText = "d5a48b42c7fc47e0a29d744f193b936c753e3400ac884a848c",
                Description = "11b8ff635a324b9c84017ad821b1ae9cdddf14b103d041d2bb0e498568b4c10852ec5e78173e41dba02f2dd0bf61039752b91cb1885c4984a3f1ab80b04000ef9163a3a49f0f4d1592a3798d41e76efd2a843cff1ed74a1ea129a7c47cfb2d562f90040f",
                IsActive = true
            };

            // Act
            var serviceResult = await _dataDictionaryItemsAppService.CreateAsync(input);

            // Assert
            var result = await _dataDictionaryItemRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Sequence.ShouldBe(1557718653);
            result.Code.ShouldBe("fbb5620525b0402eb3fa");
            result.DisplayText.ShouldBe("d5a48b42c7fc47e0a29d744f193b936c753e3400ac884a848c");
            result.Description.ShouldBe("11b8ff635a324b9c84017ad821b1ae9cdddf14b103d041d2bb0e498568b4c10852ec5e78173e41dba02f2dd0bf61039752b91cb1885c4984a3f1ab80b04000ef9163a3a49f0f4d1592a3798d41e76efd2a843cff1ed74a1ea129a7c47cfb2d562f90040f");
            result.IsActive.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new DataDictionaryItemUpdateDto()
            {
                Sequence = 814563513,
                Code = "20c642a7ae1b4afdb565",
                DisplayText = "a23d7e30b6a749c2ae3381ec100b29c1c94c8f098fd84d2694",
                Description = "098f1444e1f44eb9ad62190012024fb893c826f341084abc89245452d287836341d0e483c9c943eda64ccb4a9d991e0ed81ce85fd0944364bee45d517364460ea1fc00d712fd470f8d6b68473121d14145d0425a23c84d27931844339e5e293312e8fdf7",
                IsActive = true
            };

            // Act
            var serviceResult = await _dataDictionaryItemsAppService.UpdateAsync(Guid.Parse("65a8377e-c1f2-4eae-8ed2-952842893d23"), input);

            // Assert
            var result = await _dataDictionaryItemRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Sequence.ShouldBe(814563513);
            result.Code.ShouldBe("20c642a7ae1b4afdb565");
            result.DisplayText.ShouldBe("a23d7e30b6a749c2ae3381ec100b29c1c94c8f098fd84d2694");
            result.Description.ShouldBe("098f1444e1f44eb9ad62190012024fb893c826f341084abc89245452d287836341d0e483c9c943eda64ccb4a9d991e0ed81ce85fd0944364bee45d517364460ea1fc00d712fd470f8d6b68473121d14145d0425a23c84d27931844339e5e293312e8fdf7");
            result.IsActive.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _dataDictionaryItemsAppService.DeleteAsync(Guid.Parse("65a8377e-c1f2-4eae-8ed2-952842893d23"));

            // Assert
            var result = await _dataDictionaryItemRepository.FindAsync(c => c.Id == Guid.Parse("65a8377e-c1f2-4eae-8ed2-952842893d23"));

            result.ShouldBeNull();
        }
    }
}