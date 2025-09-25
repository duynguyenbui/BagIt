namespace BagIt.Cloud.API.Infrastructure;

public partial class CommonContextSeed(
    IWebHostEnvironment env,
    ILogger<CommonContext> logger) : IDbSeeder<CommonContext>
{
    public async Task SeedAsync(CommonContext context)
    {
        context.Database.OpenConnection();

        if (!context.CustomerDatabases.Any())
        {
            var tenant1 = new CustomerDatabase("tenant1");
            var tenant2 = new CustomerDatabase("tenant2");

            await context.CustomerDatabases.AddRangeAsync([tenant1, tenant2]);

            logger.LogInformation($"Seeded Customer Database with {context.CustomerDatabases.Count()}");
            await context.SaveChangesAsync();
        }
    }
}
