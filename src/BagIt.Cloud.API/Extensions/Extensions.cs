using BagIt.Cloud.API.Infrastructure.Customers;

namespace BagIt.Cloud.API.Services;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddKeycloakAuthentication();
        builder.Services.AddProblemDetails();
        builder.Services.AddAuthorizationBuilder();

        builder.AddSqlServerDbContext<CommonContext>(connectionName: "commondb");
        // Configure Multi-Tenant DbContext to be connected across dbs

        builder.Services.AddMigration<CommonContext, CommonContextSeed>();
    }
}
