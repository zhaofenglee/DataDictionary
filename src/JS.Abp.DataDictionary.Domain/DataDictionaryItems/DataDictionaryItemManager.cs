using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public class DataDictionaryItemManager : DomainService
    {
        private readonly IDataDictionaryItemRepository _dataDictionaryItemRepository;

        public DataDictionaryItemManager(IDataDictionaryItemRepository dataDictionaryItemRepository)
        {
            _dataDictionaryItemRepository = dataDictionaryItemRepository;
        }

        public async Task<DataDictionaryItem> CreateAsync(
        Guid? dataDictionaryId, string code, string displayText, string description, bool isStatic, int? sequence = null)
        {
            var dataDictionaryItem = new DataDictionaryItem(
             GuidGenerator.Create(),
             dataDictionaryId, code, displayText, description, isStatic,sequence
             );

            return await _dataDictionaryItemRepository.InsertAsync(dataDictionaryItem);
        }

        public async Task<DataDictionaryItem> UpdateAsync(
            Guid id,
            Guid? dataDictionaryId, string code, string displayText, string description, bool isStatic,int? sequence = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _dataDictionaryItemRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var dataDictionaryItem = await AsyncExecuter.FirstOrDefaultAsync(query);

            dataDictionaryItem.DataDictionaryId = dataDictionaryId;
            dataDictionaryItem.Code = code;
            dataDictionaryItem.DisplayText = displayText;
            dataDictionaryItem.Description = description;
            dataDictionaryItem.IsStatic = isStatic;
            dataDictionaryItem.Sequence = sequence;
            dataDictionaryItem.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _dataDictionaryItemRepository.UpdateAsync(dataDictionaryItem);
        }

    }
}