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

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public class MongoDataDictionaryRepository : MongoDbRepository<DataDictionaryMongoDbContext, DataDictionary, Guid>, IDataDictionaryRepository
    {
        public MongoDataDictionaryRepository(IMongoDbContextProvider<DataDictionaryMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
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
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, code, displayText, description, isActive);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DataDictionaryConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<DataDictionary>>()
                .PageBy<DataDictionary, IMongoQueryable<DataDictionary>>(skipCount, maxResultCount)
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
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, code, displayText, description, isActive);
            return await query.As<IMongoQueryable<DataDictionary>>().LongCountAsync(GetCancellationToken(cancellationToken));
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