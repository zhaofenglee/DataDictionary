using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using JS.Abp.DataDictionary.DataDictionaryItems;
using JS.Abp.DataDictionary.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public class MongoDataDictionaryRepository : MongoDbRepository<DataDictionaryMongoDbContext, DataDictionary, Guid>, IDataDictionaryRepository
    {
        public MongoDataDictionaryRepository(IMongoDbContextProvider<DataDictionaryMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<DataDictionary?> FindByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            var dataDictionary = await (await GetQueryableAsync(cancellationToken)).FirstOrDefaultAsync(e => e.Code ==code && e.IsActive, cancellationToken: cancellationToken);
            if (dataDictionary == null)
            {
                return null;
            }
            var dataDictionaryItem =  (await GetQueryableAsync<DataDictionaryItem>(cancellationToken)).Where(e => e.DataDictionaryId == dataDictionary.Id && e.IsActive);
            dataDictionary.AddItem(new Collection<DataDictionaryItem>(dataDictionaryItem.ToList()));
            return dataDictionary;
        }

        public virtual async Task<List<DataDictionary>> GetListAsync(
            string? filterText = null,
            string? code = null,
            string? displayText = null,
            string? description = null,
            bool? isActive = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync(cancellationToken)), filterText, code, displayText, description, isActive);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DataDictionaryConsts.GetDefaultSorting(false) : sorting);
            return await query
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            string? displayText = null,
            string? description = null,
            bool? isActive = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync(cancellationToken)), filterText, code, displayText, description, isActive);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<DataDictionary> ApplyFilter(
            IQueryable<DataDictionary> query,
            string? filterText = null,
            string? code = null,
            string? displayText = null,
            string? description = null,
            bool? isActive = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code!.Contains(filterText!) || e.DisplayText!.Contains(filterText!) || e.Description!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(displayText), e => e.DisplayText.Contains(displayText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(isActive.HasValue, e => e.IsActive == isActive);
        }
    }
}