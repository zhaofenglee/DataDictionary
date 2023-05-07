using Microsoft.EntityFrameworkCore;
using JS.Abp.DataDictionary.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using JS.Abp.DataDictionary.Blazor.Server.Host.Demos;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace JS.Abp.DataDictionary.Blazor.Server.Host.EntityFrameworkCore;

public class UnifiedDbContext : AbpDbContext<UnifiedDbContext>
{
    public DbSet<Demo> Demos { get; set; }
    public UnifiedDbContext(DbContextOptions<UnifiedDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigureIdentity();
        modelBuilder.ConfigureFeatureManagement();
        modelBuilder.ConfigureTenantManagement();
        modelBuilder.ConfigureDataDictionary();

        modelBuilder.Entity<Demo>(b =>
        {
            b.ToTable("AppDemos");
            b.ConfigureByConvention();
            b.Property(x => x.Name).HasColumnName(nameof(Demo.Name)).HasMaxLength(DemoConsts.NameMaxLength);
            b.Property(x => x.DisplayName).HasColumnName(nameof(Demo.DisplayName)).HasMaxLength(DemoConsts.DisplayNameMaxLength);
        });
    }
}
