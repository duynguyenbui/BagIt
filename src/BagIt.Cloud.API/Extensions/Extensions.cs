using BagIt.Cloud.API.Infrastructure.Customers;

namespace BagIt.Cloud.API.Services;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddKeycloakAuthentication();
        builder.Services.AddProblemDetails();
        builder.Services.AddAuthorizationBuilder();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<TenantProvider>();

        builder.Services.AddDbContext<CommonContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("commondb") ?? throw new InvalidOperationException("Connection string 'database' not found.")));

        builder.Services.AddDbContext<CustomerContext>((sp, options) =>
        {
            var tenantProvider = sp.GetService<TenantProvider>();
            var connection = tenantProvider?.GetTenantConnection();

            options.UseSqlServer(builder.Configuration.GetConnectionString(connection));
        });

        builder.Services.AddScoped<CloudServices, CloudServices>();
        builder.Services.AddMigration<CommonContext, CommonContextSeed>();
    }
}
