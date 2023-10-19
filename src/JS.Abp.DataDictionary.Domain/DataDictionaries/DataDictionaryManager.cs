using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public class DataDictionaryManager : DomainService
    {
        protected IDataDictionaryRepository _dataDictionaryRepository;

        public DataDictionaryManager(IDataDictionaryRepository dataDictionaryRepository)
        {
            _dataDictionaryRepository = dataDictionaryRepository;
        }

        public virtual async Task<DataDictionary> CreateAsync(
        string code, string displayText, bool isActive, string? description = null)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), DataDictionaryConsts.CodeMaxLength, DataDictionaryConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(displayText, nameof(displayText));
            Check.Length(displayText, nameof(displayText), DataDictionaryConsts.DisplayTextMaxLength, DataDictionaryConsts.DisplayTextMinLength);
            Check.Length(description, nameof(description), DataDictionaryConsts.DescriptionMaxLength);

            var dataDictionary = new DataDictionary(
             GuidGenerator.Create(),
             code, displayText, isActive, description
             );

            return await _dataDictionaryRepository.InsertAsync(dataDictionary);
        }

        public virtual async Task<DataDictionary> UpdateAsync(
            Guid id,
            string displayText, bool isActive, string? description = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(displayText, nameof(displayText));
            Check.Length(displayText, nameof(displayText), DataDictionaryConsts.DisplayTextMaxLength, DataDictionaryConsts.DisplayTextMinLength);
            Check.Length(description, nameof(description), DataDictionaryConsts.DescriptionMaxLength);

            var dataDictionary = await _dataDictionaryRepository.GetAsync(id);

            dataDictionary.DisplayText = displayText;
            dataDictionary.IsActive = isActive;
            dataDictionary.Description = description;

            dataDictionary.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _dataDictionaryRepository.UpdateAsync(dataDictionary);
        }

    }
}