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
                    code: "1",
                    displayText: "A001",
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
                    code: "1",
                    displayText: "A001",
                    description: "4c3ca927ffbc4e4f9bcffbdd7342f970656d84c42a06481abd09a43ec29d4ea7c117cd75c41d42fd8a4ca7acd79ddbcebc3248ca27d647e6a9d1235893e5cde8ee79da0d9f864bd4b2cf17d2c8adcb0e15d6c455daf9482d9659d2773ff44bb97d5fca91",
                    isActive: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}