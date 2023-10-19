using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public class DataDictionaryItemManager : DomainService
    {
        protected IDataDictionaryItemRepository _dataDictionaryItemRepository;

        public DataDictionaryItemManager(IDataDictionaryItemRepository dataDictionaryItemRepository)
        {
            _dataDictionaryItemRepository = dataDictionaryItemRepository;
        }

        public virtual async Task<DataDictionaryItem> CreateAsync(
        Guid? dataDictionaryId, int sequence, bool isActive, string? code = null, string? displayText = null, string? description = null)
        {
            Check.Length(code, nameof(code), DataDictionaryItemConsts.CodeMaxLength);
            Check.Length(displayText, nameof(displayText), DataDictionaryItemConsts.DisplayTextMaxLength);
            Check.Length(description, nameof(description), DataDictionaryItemConsts.DescriptionMaxLength);

            var dataDictionaryItem = new DataDictionaryItem(
             GuidGenerator.Create(),
             dataDictionaryId, sequence, isActive, code, displayText, description
             );

            return await _dataDictionaryItemRepository.InsertAsync(dataDictionaryItem);
        }

        public virtual async Task<DataDictionaryItem> UpdateAsync(
            Guid id,
            Guid? dataDictionaryId, int sequence, bool isActive, string? code = null, string? displayText = null, string? description = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.Length(code, nameof(code), DataDictionaryItemConsts.CodeMaxLength);
            Check.Length(displayText, nameof(displayText), DataDictionaryItemConsts.DisplayTextMaxLength);
            Check.Length(description, nameof(description), DataDictionaryItemConsts.DescriptionMaxLength);

            var dataDictionaryItem = await _dataDictionaryItemRepository.GetAsync(id);

            dataDictionaryItem.DataDictionaryId = dataDictionaryId;
            dataDictionaryItem.Sequence = sequence;
            dataDictionaryItem.IsActive = isActive;
            dataDictionaryItem.Code = code;
            dataDictionaryItem.DisplayText = displayText;
            dataDictionaryItem.Description = description;

            dataDictionaryItem.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _dataDictionaryItemRepository.UpdateAsync(dataDictionaryItem);
        }

    }
}