using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using JS.Abp.DataDictionary.DataDictionaryItems;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using JS.Abp.DataDictionary.EntityFrameworkCore;

namespace JS.Abp.DataDictionary.DataDictionaries
{
    public class EfCoreDataDictionaryRepository : EfCoreRepository<IDataDictionaryDbContext, DataDictionary, Guid>, IDataDictionaryRepository
    {
        public EfCoreDataDictionaryRepository(IDbContextProvider<IDataDictionaryDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public override async Task<DataDictionary> GetAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            var dataDictionary = await base.GetAsync(id, includeDetails, cancellationToken);
            if (includeDetails)
            {
                var dataDictionaryItems = (await GetDbContextAsync()).DataDictionaryItems
                    .Where(e => e.DataDictionaryId == dataDictionary.Id);
                dataDictionary.AddItem(new Collection<DataDictionaryItem>(dataDictionaryItems.ToList()));
            }
            return dataDictionary;
        }

        public async Task<DataDictionary?> FindByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            var dataDictionary = (await GetQueryableAsync()).FirstOrDefault(e => e.Code == code && e.IsActive);
            if (dataDictionary == null)
            {
                return null;
            }
            var dataDictionaryItem =  (await GetDbContextAsync()).DataDictionaryItems.Where(e => e.DataDictionaryId == dataDictionary.Id && e.IsActive);
            dataDictionary.AddItem(new Collection<DataDictionaryItem>(dataDictionaryItem.ToList()));
            return dataDictionary;
        }

        public async Task<List<DataDictionary>> GetListAsync(
            string filterText = null,
            string code = null,
            string displayText = null,
            string description = null,
            bool? isStatic = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, displayText, description, isStatic);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DataDictionaryConsts.GetDefaultSorting(false) : sorting);
            var dataDictionaries = await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
            
            // Load Items collection for each DataDictionary
            var dbContext = await GetDbContextAsync();
            foreach (var dataDictionary in dataDictionaries)
            {
                var items = dbContext.DataDictionaryItems
                    .Where(e => e.DataDictionaryId == dataDictionary.Id);
                dataDictionary.AddItem(new Collection<DataDictionaryItem>(items.ToList()));
            }
            
            return dataDictionaries;
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string displayText = null,
            string description = null,
            bool? isStatic = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, code, displayText, description, isStatic);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<DataDictionary> ApplyFilter(
            IQueryable<DataDictionary> query,
            string filterText = null,
            string code = null,
            string displayText = null,
            string description = null,
            bool? isActive = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.DisplayText.Contains(filterText) || e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(displayText), e => e.DisplayText.Contains(displayText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(isActive.HasValue, e => e.IsActive == isActive);
        }
    }
}