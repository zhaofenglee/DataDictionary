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
                    code: "2204a7e1fcf642c1aa28",
                    displayText: "d2c18a8429ed4e91a991d7318a9b7625441e3dfa760e4464a9",
                    description: "b2d46c44b9774be496eae44247f918e4160e4351082f48e3be",
                    isActive: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("5af1ac84-7282-456f-94c5-822c5e4e0382"));
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
                    code: "eb15ccc2bbcc4581a729",
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