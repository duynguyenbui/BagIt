using Microsoft.AspNetCore.Authorization;

namespace BagIt.Cloud.API.Handlers;

public class TenantAuthorizationHandler(TenantProvider tenantProvider) : AuthorizationHandler<TenantRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TenantRequirement requirement)
    {
        var tenant = tenantProvider.GetTenantConnection();

        if (!string.IsNullOrEmpty(tenant))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
