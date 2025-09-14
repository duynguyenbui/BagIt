namespace BagIt.Cloud.API.Infrastructure.EntityConfigurations;

class CustomerDatabaseEntityConfiguration : IEntityTypeConfiguration<CustomerDatabase>
{
    public void Configure(EntityTypeBuilder<CustomerDatabase> builder)
    {
        builder.ToTable("CustomerDatabases");

        builder.Property(cd => cd.ConnectionString)
            .HasColumnType("nvarchar(max)");
    }
}
