using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using JS.Abp.DataDictionary.DataDictionaryItems;
using JS.Abp.DataDictionary.EntityFrameworkCore;
using Xunit;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public class DataDictionaryItemRepositoryTests : DataDictionaryEntityFrameworkCoreTestBase
    {
        private readonly IDataDictionaryItemRepository _dataDictionaryItemRepository;

        public DataDictionaryItemRepositoryTests()
        {
            _dataDictionaryItemRepository = GetRequiredService<IDataDictionaryItemRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _dataDictionaryItemRepository.GetListAsync(
                    code: "ecd5c8a8ebb84b73ac1b",
                    displayText: "33ad14c28e9a44008a8f2f25e115b844074f64a743a244958e",
                    description: "4c3ca927ffbc4e4f9bcffbdd7342f970656d84c42a06481abd09a43ec29d4ea7c117cd75c41d42fd8a4ca7acd79ddbcebc3248ca27d647e6a9d1235893e5cde8ee79da0d9f864bd4b2cf17d2c8adcb0e15d6c455daf9482d9659d2773ff44bb97d5fca91",
                    isActive: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("65a8377e-c1f2-4eae-8ed2-952842893d23"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _dataDictionaryItemRepository.GetCountAsync(
                    code: "00e6c07930414ea48d7d",
                    displayText: "7970709a08754faea54c7cfb404c07054083c3796b5544e896",
                    description: "5fcbfc3c9d9548d1aad7221461660147636f1e0e47824c63a586336be9ee05c9772475b3789843d88fd9c2824cf72c4b732def461fbe429d927546ed61654d193da1d9c820c042edbe4b240f5e99fca85d0ee9f67508430a90900a3eff13cbcefc52ee39",
                    isActive: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}