using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public class DataDictionaryManager : DomainService, IDataDictionaryManager
    {
        private readonly IDataDictionaryRepository _dataDictionaryRepository;
        private readonly IDistributedCache<DataDictionary> _cache;
        public DataDictionaryManager(IDataDictionaryRepository dataDictionaryRepository, IDistributedCache<DataDictionary> cache)
        {
            _dataDictionaryRepository = dataDictionaryRepository;
            _cache = cache;
        }

        public async Task<DataDictionary> CreateAsync(
        string code, string displayText, string description, bool isStatic)
        {
            var dataDictionary = new DataDictionary(
             GuidGenerator.Create(),
             code, displayText, description, isStatic
             );

            return await _dataDictionaryRepository.InsertAsync(dataDictionary);
        }

        public async Task<DataDictionary> UpdateAsync(
            Guid id,
            string code, string displayText, string description, bool isStatic, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _dataDictionaryRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var dataDictionary = await AsyncExecuter.FirstOrDefaultAsync(query);

            dataDictionary.Code = code;
            dataDictionary.DisplayText = displayText;
            dataDictionary.Description = description;
            dataDictionary.IsStatic = isStatic;

            dataDictionary.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _dataDictionaryRepository.UpdateAsync(dataDictionary);
        }

    }
}