using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public abstract class DataDictionariesAppServiceTests <TStartupModule> : DataDictionaryApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IDataDictionariesAppService _dataDictionariesAppService;
        private readonly IRepository<DataDictionary, Guid> _dataDictionaryRepository;

        public DataDictionariesAppServiceTests()
        {
            _dataDictionariesAppService = GetRequiredService<IDataDictionariesAppService>();
            _dataDictionaryRepository = GetRequiredService<IRepository<DataDictionary, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _dataDictionariesAppService.GetListAsync(new GetDataDictionariesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("5af1ac84-7282-456f-94c5-822c5e4e0382")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("2b8aec84-cfcd-44b1-b694-4ba362374778")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _dataDictionariesAppService.GetAsync(Guid.Parse("5af1ac84-7282-456f-94c5-822c5e4e0382"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("5af1ac84-7282-456f-94c5-822c5e4e0382"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new DataDictionaryCreateDto
            {
                Code = "ffc408daa43c4cecac72",
                DisplayText = "ebf18c39d94a4b4ab416a57911fec4af6453eafcaa9a43a0b6",
                Description = "8dbec3ae64d44f1a8d4b73d93f3b910fb597e81f823840a29b",
                IsActive = true
            };

            // Act
            var serviceResult = await _dataDictionariesAppService.CreateAsync(input);

            // Assert
            var result = await _dataDictionaryRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ffc408daa43c4cecac72");
            result.DisplayText.ShouldBe("ebf18c39d94a4b4ab416a57911fec4af6453eafcaa9a43a0b6");
            result.Description.ShouldBe("8dbec3ae64d44f1a8d4b73d93f3b910fb597e81f823840a29b");
            result.IsActive.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new DataDictionaryUpdateDto()
            {
                Code = "TestCode",
                DisplayText = "727e6ac75144463894450e0937adc57fe3181efefe9b409192",
                Description = "58bf6775a4964a5c937a5a79f230f46ebf20f282386b4fb4a7",
                IsActive = true
            };

            // Act
            var serviceResult = await _dataDictionariesAppService.UpdateAsync(Guid.Parse("5af1ac84-7282-456f-94c5-822c5e4e0382"), input);

            // Assert
            var result = await _dataDictionaryRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DisplayText.ShouldBe("727e6ac75144463894450e0937adc57fe3181efefe9b409192");
            result.Description.ShouldBe("58bf6775a4964a5c937a5a79f230f46ebf20f282386b4fb4a7");
            result.IsActive.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _dataDictionariesAppService.DeleteAsync(Guid.Parse("5af1ac84-7282-456f-94c5-822c5e4e0382"));

            // Assert
            var result = await _dataDictionaryRepository.FindAsync(c => c.Id == Guid.Parse("5af1ac84-7282-456f-94c5-822c5e4e0382"));

            result.ShouldBeNull();
        }
    }
}