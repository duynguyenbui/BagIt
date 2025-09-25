using BagIt.Cloud.API.Handlers;
using BagIt.Cloud.API.Infrastructure.Customers;

using Microsoft.AspNetCore.Authorization;

namespace BagIt.Cloud.API.Services;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddKeycloakAuthentication();
        builder.Services.AddScoped<IAuthorizationHandler, TenantAuthorizationHandler>();
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("TenantRequired", policy =>
            {
                policy.AddRequirements(new TenantRequirement());
            });
        });
        builder.Services.AddProblemDetails();
        builder.Services.AddAuthorizationBuilder();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddDbContext<CommonContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("commondb") ?? throw new InvalidOperationException("Connection string 'database' not found.")));
        
        builder.Services.AddScoped<TenantProvider>();
        
        builder.Services.AddDbContext<CustomerContext>((sp, options) =>
        {
            var tenantProvider = sp.GetService<TenantProvider>();
            var connection = tenantProvider?.GetTenantConnection();

            if (!string.IsNullOrEmpty(connection))
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString(connection));
            }
        });

        builder.Services.AddMigration<CommonContext, CommonContextSeed>();
        builder.Services.AddScoped<CloudServices, CloudServices>();
    }
}
