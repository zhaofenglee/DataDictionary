using JS.Abp.DataDictionary.DataDictionaries;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using JS.Abp.DataDictionary.DataDictionaryItems;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public class DataDictionaryItemsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IDataDictionaryItemRepository _dataDictionaryItemRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly DataDictionariesDataSeedContributor _dataDictionariesDataSeedContributor;

        public DataDictionaryItemsDataSeedContributor(IDataDictionaryItemRepository dataDictionaryItemRepository, IUnitOfWorkManager unitOfWorkManager, DataDictionariesDataSeedContributor dataDictionariesDataSeedContributor)
        {
            _dataDictionaryItemRepository = dataDictionaryItemRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _dataDictionariesDataSeedContributor = dataDictionariesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _dataDictionariesDataSeedContributor.SeedAsync(context);

            await _dataDictionaryItemRepository.InsertAsync(new DataDictionaryItem
            (
                id: Guid.Parse("65a8377e-c1f2-4eae-8ed2-952842893d23"),
                sequence: 1,
                code: "1",
                displayText: "A001",
                description: "4c3ca927ffbc4e4f9bcffbdd7342f970656d84c42a06481abd09a43ec29d4ea7c117cd75c41d42fd8a4ca7acd79ddbcebc3248ca27d647e6a9d1235893e5cde8ee79da0d9f864bd4b2cf17d2c8adcb0e15d6c455daf9482d9659d2773ff44bb97d5fca91",
                isActive: true,
                dataDictionaryId: Guid.Parse("5af1ac84-7282-456f-94c5-822c5e4e0382")
            ));
            await _dataDictionaryItemRepository.InsertAsync(new DataDictionaryItem
            (
                id: Guid.Parse("65a8377e-c1f2-4eae-8ed2-952842893d24"),
                sequence: 2,
                code: "2",
                displayText: "A002",
                description: "4c3ca927ffbc4e4f9bcffbdd7342f970656d84c42a06481abd09a43ec29d4ea7c117cd75c41d42fd8a4ca7acd79ddbcebc3248ca27d647e6a9d1235893e5cde8ee79da0d9f864bd4b2cf17d2c8adcb0e15d6c455daf9482d9659d2773ff44bb97d5fca91",
                isActive: true,
                dataDictionaryId: Guid.Parse("5af1ac84-7282-456f-94c5-822c5e4e0382")
            ));

            await _dataDictionaryItemRepository.InsertAsync(new DataDictionaryItem
            (
                id: Guid.Parse("46696fd3-74fd-48c9-a9dd-51d2b1b21b63"),
                sequence: 1,
                code: "1",
                displayText: "B001",
                description: "5fcbfc3c9d9548d1aad7221461660147636f1e0e47824c63a586336be9ee05c9772475b3789843d88fd9c2824cf72c4b732def461fbe429d927546ed61654d193da1d9c820c042edbe4b240f5e99fca85d0ee9f67508430a90900a3eff13cbcefc52ee39",
                isActive: true,
                dataDictionaryId: Guid.Parse("2b8aec84-cfcd-44b1-b694-4ba362374778")
            ));
            await _dataDictionaryItemRepository.InsertAsync(new DataDictionaryItem
            (
                id: Guid.Parse("46696fd3-74fd-48c9-a9dd-51d2b1b21b64"),
                sequence: 2,
                code: "2",
                displayText: "B002",
                description: "5fcbfc3c9d9548d1aad7221461660147636f1e0e47824c63a586336be9ee05c9772475b3789843d88fd9c2824cf72c4b732def461fbe429d927546ed61654d193da1d9c820c042edbe4b240f5e99fca85d0ee9f67508430a90900a3eff13cbcefc52ee39",
                isActive: true,
                dataDictionaryId: Guid.Parse("2b8aec84-cfcd-44b1-b694-4ba362374778")
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}