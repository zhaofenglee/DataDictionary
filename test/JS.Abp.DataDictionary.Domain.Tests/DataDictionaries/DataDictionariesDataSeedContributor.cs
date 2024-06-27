using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using JS.Abp.DataDictionary.DataDictionaries;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public class DataDictionariesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IDataDictionaryRepository _dataDictionaryRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public DataDictionariesDataSeedContributor(IDataDictionaryRepository dataDictionaryRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _dataDictionaryRepository = dataDictionaryRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _dataDictionaryRepository.InsertAsync(new DataDictionary
            (
                id: Guid.Parse("5af1ac84-7282-456f-94c5-822c5e4e0382"),
                code: "DemoType",
                displayText: "d2c18a8429ed4e91a991d7318a9b7625441e3dfa760e4464a9",
                description: "b2d46c44b9774be496eae44247f918e4160e4351082f48e3be",
                isActive: false
            ));

            await _dataDictionaryRepository.InsertAsync(new DataDictionary
            (
                id: Guid.Parse("2b8aec84-cfcd-44b1-b694-4ba362374778"),
                code: "DemoType",
                displayText: "c187548f65d9441eb2b66fb2221bacba89b1ec8d0a6d42e3ad",
                description: "346ee7e6fb3b489a90c4a781c12c82519afc80a8ebf84c7aae",
                isActive: true
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}