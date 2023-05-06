using System;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Volo.Abp.Threading;

namespace JS.Abp.DataDictionary.DataDictionaries;

public class DataDictionaryStore:IDataDictionaryStore, ITransientDependency
{
    private readonly IDataDictionaryRepository _dataDictionaryRepository;
    private readonly IDistributedCache<DataDictionary, string> _cache;
    protected DataDictionaryOptions Options { get; }
    
    public DataDictionaryStore(IDataDictionaryRepository dataDictionaryRepository, 
        IDistributedCache<DataDictionary, string> cache,
        IOptions<DataDictionaryOptions> options)
    {
        _dataDictionaryRepository = dataDictionaryRepository;
        _cache = cache;
        Options = options.Value;
    }
    public virtual DataDictionary FindByCode(string code)
    {
        return AsyncHelper.RunSync(()=> FindByCodeAsync(code));;
    }

    public virtual async Task<DataDictionary> FindByCodeAsync(string code)
    {
        return await _cache.GetOrAddAsync(
            $"DataDictionary:{code}",
            async () => await GetDictFromDatabaseAsync(code),
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(Options.CacheExpirationTime)
            }
        );
    }
    
    private async Task<DataDictionary> GetDictFromDatabaseAsync(string code)
    {
        var queryable = await _dataDictionaryRepository.WithDetailsAsync(x => x.Items);
        var query = queryable.Where(x => x.Code == code);
        return query.FirstOrDefault();
    }
}