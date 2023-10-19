using JS.Abp.DataDictionary.DataDictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using JS.Abp.DataDictionary.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace JS.Abp.DataDictionary.DataDictionaryItems
{
    public class MongoDataDictionaryItemRepository : MongoDbRepository<DataDictionaryMongoDbContext, DataDictionaryItem, Guid>, IDataDictionaryItemRepository
    {
        public MongoDataDictionaryItemRepository(IMongoDbContextProvider<DataDictionaryMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<DataDictionaryItemWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dataDictionaryItem = await (await GetMongoQueryableAsync(cancellationToken))
                .FirstOrDefaultAsync(e => e.Id == id, GetCancellationToken(cancellationToken));

            var dataDictionary = await (await GetMongoQueryableAsync<DataDictionaries.DataDictionary>(cancellationToken)).FirstOrDefaultAsync(e => e.Id == dataDictionaryItem.DataDictionaryId, cancellationToken: cancellationToken);

            return new DataDictionaryItemWithNavigationProperties
            {
                DataDictionaryItem = dataDictionaryItem,
                DataDictionary = dataDictionary,

            };
        }

        public virtual async Task<List<DataDictionaryItemWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string? code = null,
            string? displayText = null,
            string? description = null,
            bool? isActive = null,
            Guid? dataDictionaryId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, sequenceMin, sequenceMax, code, displayText, description, isActive, dataDictionaryId);
            var dataDictionaryItems = await query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DataDictionaryItemConsts.GetDefaultSorting(false) : sorting.Split('.').Last())
                .As<IMongoQueryable<DataDictionaryItem>>()
                .PageBy<DataDictionaryItem, IMongoQueryable<DataDictionaryItem>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

            var dbContext = await GetDbContextAsync(cancellationToken);
            return dataDictionaryItems.Select(s => new DataDictionaryItemWithNavigationProperties
            {
                DataDictionaryItem = s,
                DataDictionary = ApplyDataFilters<IMongoQueryable<DataDictionaries.DataDictionary>, DataDictionaries.DataDictionary>(dbContext.Collection<DataDictionaries.DataDictionary>().AsQueryable()).FirstOrDefault(e => e.Id == s.DataDictionaryId),

            }).ToList();
        }

        public virtual async Task<List<DataDictionaryItem>> GetListAsync(
            string? filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string? code = null,
            string? displayText = null,
            string? description = null,
            bool? isActive = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, sequenceMin, sequenceMax, code, displayText, description, isActive);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DataDictionaryItemConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<DataDictionaryItem>>()
                .PageBy<DataDictionaryItem, IMongoQueryable<DataDictionaryItem>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string? code = null,
            string? displayText = null,
            string? description = null,
            bool? isActive = null,
            Guid? dataDictionaryId = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, sequenceMin, sequenceMax, code, displayText, description, isActive, dataDictionaryId);
            return await query.As<IMongoQueryable<DataDictionaryItem>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<DataDictionaryItem> ApplyFilter(
            IQueryable<DataDictionaryItem> query,
            string? filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string? code = null,
            string? displayText = null,
            string? description = null,
            bool? isActive = null,
            Guid? dataDictionaryId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code!.Contains(filterText!) || e.DisplayText!.Contains(filterText!) || e.Description!.Contains(filterText!))
                    .WhereIf(sequenceMin.HasValue, e => e.Sequence >= sequenceMin!.Value)
                    .WhereIf(sequenceMax.HasValue, e => e.Sequence <= sequenceMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(displayText), e => e.DisplayText.Contains(displayText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(isActive.HasValue, e => e.IsActive == isActive)
                    .WhereIf(dataDictionaryId != null && dataDictionaryId != Guid.Empty, e => e.DataDictionaryId == dataDictionaryId);
        }
    }
}