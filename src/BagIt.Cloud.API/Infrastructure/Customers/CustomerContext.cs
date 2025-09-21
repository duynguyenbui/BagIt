namespace BagIt.Cloud.API.Infrastructure.Customers;

public class CustomerContext : DbContext
{
    public CustomerContext()
    {
    }

    public CustomerContext(DbContextOptions options) : base(options)
    {
    }
}
