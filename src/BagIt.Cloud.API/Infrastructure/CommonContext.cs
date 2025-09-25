namespace BagIt.Cloud.API.Infrastructure;

/// <remarks>
/// Add migrations using the following command inside the 'Cloud.API' project directory:
///
/// dotnet ef migrations add --context CommonDbContext [migration-name]
/// </remarks>
public class CommonContext : DbContext
{
    public CommonContext(DbContextOptions<CommonContext> options) : base(options)
    {
    }

    public DbSet<CustomerDatabase> CustomerDatabases { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CustomerDatabaseEntityConfiguration());
    }
}
