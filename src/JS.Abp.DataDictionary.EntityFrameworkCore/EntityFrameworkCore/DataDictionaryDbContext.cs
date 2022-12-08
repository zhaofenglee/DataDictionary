using JS.Abp.DataDictionary.DataDictionaryItems;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace JS.Abp.DataDictionary.EntityFrameworkCore;

[ConnectionStringName(DataDictionaryDbProperties.ConnectionStringName)]
public class DataDictionaryDbContext : AbpDbContext<DataDictionaryDbContext>, IDataDictionaryDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<DataDictionaryItem> DataDictionaryItems { get; set; }
    public DbSet<JS.Abp.DataDictionary.DataDictionaries.DataDictionary> Dictionaries { get; set; }
    public DataDictionaryDbContext(DbContextOptions<DataDictionaryDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDataDictionary();
    }
}
