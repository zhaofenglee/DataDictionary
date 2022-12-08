using JS.Abp.DataDictionary.DataDictionaries;
using JS.Abp.DataDictionary.DataDictionaryItems;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace JS.Abp.DataDictionary.EntityFrameworkCore;

public static class DataDictionaryDbContextModelCreatingExtensions
{
    public static void ConfigureDataDictionary(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
        if (builder.IsHostDatabase())
        {
            builder.Entity<JS.Abp.DataDictionary.DataDictionaries.DataDictionary>(b =>
            {
                b.ToTable(DataDictionaryDbProperties.DbTablePrefix + "DataDictionaries", DataDictionaryDbProperties.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Code).HasColumnName(nameof(JS.Abp.DataDictionary.DataDictionaries.DataDictionary.Code)).IsRequired().HasMaxLength(JS.Abp.DataDictionary.DataDictionaries.DataDictionaryConsts.CodeMaxLength);
                b.Property(x => x.DisplayText).HasColumnName(nameof(JS.Abp.DataDictionary.DataDictionaries.DataDictionary.DisplayText)).IsRequired().HasMaxLength(JS.Abp.DataDictionary.DataDictionaries.DataDictionaryConsts.DisplayTextMaxLength);
                b.Property(x => x.Description).HasColumnName(nameof(JS.Abp.DataDictionary.DataDictionaries.DataDictionary.Description)).HasMaxLength(JS.Abp.DataDictionary.DataDictionaries.DataDictionaryConsts.DescriptionMaxLength);
                b.Property(x => x.IsStatic).HasColumnName(nameof(JS.Abp.DataDictionary.DataDictionaries.DataDictionary.IsStatic));
                b.HasMany(x => x.Items).WithOne(x => x.DataDictionary).HasForeignKey(x => x.DataDictionaryId).IsRequired();
                //b.HasIndex(x => x.Code);
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<DataDictionaryItem>(b =>
            {
                b.ToTable(DataDictionaryDbProperties.DbTablePrefix + "DataDictionaryItems", DataDictionaryDbProperties.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Sequence).HasColumnName(nameof(DataDictionaryItem.Sequence)).IsRequired();
                b.Property(x => x.Code).HasColumnName(nameof(DataDictionaryItem.Code)).IsRequired().HasMaxLength(DataDictionaryItemConsts.CodeMaxLength);
                b.Property(x => x.DisplayText).HasColumnName(nameof(DataDictionaryItem.DisplayText)).IsRequired().HasMaxLength(DataDictionaryItemConsts.DisplayTextMaxLength);
                b.Property(x => x.Description).HasColumnName(nameof(DataDictionaryItem.Description)).HasMaxLength(DataDictionaryItemConsts.DescriptionMaxLength);
                b.Property(x => x.IsStatic).HasColumnName(nameof(DataDictionaryItem.IsStatic));
                //b.HasOne<JS.Abp.DataDictionary.DataDictionaries.DataDictionary>().WithMany().HasForeignKey(x => x.DataDictionaryId).OnDelete(DeleteBehavior.NoAction);
            });
        }

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(DataDictionaryDbProperties.DbTablePrefix + "Questions", DataDictionaryDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
    }
}
