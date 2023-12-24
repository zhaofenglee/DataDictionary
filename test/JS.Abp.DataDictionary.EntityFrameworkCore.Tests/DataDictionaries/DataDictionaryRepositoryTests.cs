using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using JS.Abp.DataDictionary.DataDictionaries;
using JS.Abp.DataDictionary.EntityFrameworkCore;
using Xunit;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public class DataDictionaryRepositoryTests : DataDictionaryEntityFrameworkCoreTestBase
    {
        private readonly IDataDictionaryRepository _dataDictionaryRepository;

        public DataDictionaryRepositoryTests()
        {
            _dataDictionaryRepository = GetRequiredService<IDataDictionaryRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _dataDictionaryRepository.GetListAsync(
                    code: "DemoType",
                    displayText: "c187548f65d9441eb2b66fb2221bacba89b1ec8d0a6d42e3ad",
                    description: "346ee7e6fb3b489a90c4a781c12c82519afc80a8ebf84c7aae",
                    isActive: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("2b8aec84-cfcd-44b1-b694-4ba362374778"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _dataDictionaryRepository.GetCountAsync(
                    code: "DemoType",
                    displayText: "c187548f65d9441eb2b66fb2221bacba89b1ec8d0a6d42e3ad",
                    description: "346ee7e6fb3b489a90c4a781c12c82519afc80a8ebf84c7aae",
                    isActive: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}