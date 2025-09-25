using Microsoft.AspNetCore.Http.HttpResults;

namespace BagIt.Cloud.API;

public static class CloudApi
{
    public static IEndpointRouteBuilder MapCloudApi(this IEndpointRouteBuilder app)
    {
        var commonApi = app.MapGroup("/api/common");
        var customerApi = app.MapGroup("/api/customer");

        commonApi.MapGet("/commondb", GetCommonDbs).AllowAnonymous();
        customerApi.MapGet("/tenant", GetTenant).RequireAuthorization("TenantRequired");

        return app;
    }

    private static async Task<Results<Ok<string>, BadRequest<string>>> GetTenant(
        [AsParameters] CloudServices services
    )
    {
        var db = services.CustomerContext?.Database;
        if (db is null) return TypedResults.BadRequest("Cannot create connection of this tenant");

        var can = await db.CanConnectAsync();
        return can
            ? TypedResults.Ok($"connected: {can}")
            : TypedResults.BadRequest("Cannot create connection of this tenant");
    }

    private static async Task<Ok<List<CustomerDatabase>>> GetCommonDbs([AsParameters] CloudServices services)
    {
        var customerdbs = await services.CommonContext.CustomerDatabases.ToListAsync() ?? [];

        return TypedResults.Ok(customerdbs);
    }
}
