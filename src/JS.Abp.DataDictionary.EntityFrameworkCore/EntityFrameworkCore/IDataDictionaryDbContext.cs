using JS.Abp.DataDictionary.DataDictionaryItems;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace JS.Abp.DataDictionary.EntityFrameworkCore;

[ConnectionStringName(DataDictionaryDbProperties.ConnectionStringName)]
public interface IDataDictionaryDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    DbSet<JS.Abp.DataDictionary.DataDictionaries.DataDictionary> Dictionaries { get; set; }

    DbSet<DataDictionaryItem> DataDictionaryItems { get; set; }
}
