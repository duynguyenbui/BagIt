using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BagIt.ServiceDefaults;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddKeycloakAuthentication(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;
        
        // "Identity": {
        //    "Audience": "cloud.api"
        // }
        var identitySection = configuration.GetSection("Identity");

        if (!identitySection.Exists())
        {
            // No identity section, so no authentication
            return services;
        }
        
        var realm = identitySection["Realm"] ?? "BagIt";
        
        services.AddAuthentication()
            .AddKeycloakJwtBearer("keycloak", realm: realm, options =>
            {
                var audience = identitySection.GetRequiredValue("Audience");
                
                options.RequireHttpsMetadata = false;
                options.Audience = audience;
            });

        services.AddAuthorization();
        
        return services;
    }
}