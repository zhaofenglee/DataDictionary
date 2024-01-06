using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using JS.Abp.DataDictionary.EntityFrameworkCore;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public class EfCoreDataDictionaryItemRepository : EfCoreRepository<IDataDictionaryDbContext, DataDictionaryItem, Guid>, IDataDictionaryItemRepository
    {
        public EfCoreDataDictionaryItemRepository(IDbContextProvider<IDataDictionaryDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<DataDictionaryItemWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(dataDictionaryItem => new DataDictionaryItemWithNavigationProperties
                {
                    DataDictionaryItem = dataDictionaryItem,
                    DataDictionary = dbContext.Dictionaries.FirstOrDefault(c => c.Id == dataDictionaryItem.DataDictionaryId)
                }).FirstOrDefault();
        }

        public async Task<List<DataDictionaryItemWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string code = null,
            string displayText = null,
            string description = null,
            bool? isActive = null,
            Guid? dataDictionaryId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText,sequenceMin, sequenceMax, code, displayText, description, isActive, dataDictionaryId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DataDictionaryItemConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

       

        protected virtual async Task<IQueryable<DataDictionaryItemWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from dataDictionaryItem in (await GetDbSetAsync())
                   join dataDictionary in (await GetDbContextAsync()).Dictionaries on dataDictionaryItem.DataDictionaryId
                       equals dataDictionary.Id into dataDictionaries
                   from dataDictionary in dataDictionaries.DefaultIfEmpty()

                   select new DataDictionaryItemWithNavigationProperties
                   {
                       DataDictionaryItem = dataDictionaryItem,
                       DataDictionary = dataDictionary
                   };
        }

        protected virtual IQueryable<DataDictionaryItemWithNavigationProperties> ApplyFilter(
            IQueryable<DataDictionaryItemWithNavigationProperties> query,
            string filterText,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string code = null,
            string displayText = null,
            string description = null,
            bool? isActive = null,
            Guid? dataDictionaryId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.DataDictionaryItem.Code.Contains(filterText) || e.DataDictionaryItem.DisplayText.Contains(filterText) || e.DataDictionaryItem.Description.Contains(filterText))
                .WhereIf(sequenceMin.HasValue, e => e.DataDictionaryItem.Sequence >= sequenceMin.Value)
                .WhereIf(sequenceMax.HasValue, e => e.DataDictionaryItem.Sequence <= sequenceMax.Value)    
                .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.DataDictionaryItem.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(displayText), e => e.DataDictionaryItem.DisplayText.Contains(displayText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.DataDictionaryItem.Description.Contains(description))
                    .WhereIf(isActive.HasValue, e => e.DataDictionaryItem.IsActive == isActive)
                    .WhereIf(dataDictionaryId != null && dataDictionaryId != Guid.Empty, e => e.DataDictionary != null && e.DataDictionary.Id == dataDictionaryId);
        }
        
        
        public async Task<List<DataDictionaryItem>> GetListAsync(
            string filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string code = null,
            string displayText = null,
            string description = null,
            bool? isActive = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText,sequenceMin, sequenceMax, code, displayText, description, isActive);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DataDictionaryItemConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string code = null,
            string displayText = null,
            string description = null,
            bool? isActive = null,
            Guid? dataDictionaryId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, sequenceMin, sequenceMax,code, displayText, description, isActive, dataDictionaryId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<DataDictionaryItem> ApplyFilter(
            IQueryable<DataDictionaryItem> query,
            string filterText,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string code = null,
            string displayText = null,
            string description = null,
            bool? isActive = null,
            Guid? dataDictionaryId = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.DisplayText.Contains(filterText) || e.Description.Contains(filterText))
                    .WhereIf(sequenceMin.HasValue, e => e.Sequence >= sequenceMin.Value)
                    .WhereIf(sequenceMax.HasValue, e => e.Sequence <= sequenceMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(displayText), e => e.DisplayText.Contains(displayText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(isActive.HasValue, e => e.IsActive == isActive)
                    .WhereIf(dataDictionaryId != null && dataDictionaryId != Guid.Empty, e => e.DataDictionaryId == dataDictionaryId);
        }
    }
}