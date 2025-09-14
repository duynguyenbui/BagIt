namespace BagIt.Cloud.API.Infrastructure;

public partial class CommonContextSeed(
    IWebHostEnvironment env,
    ILogger<CommonContext> logger) : IDbSeeder<CommonContext>
{
    public Task SeedAsync(CommonContext context)
    {
        return Task.CompletedTask;
    }
}
