using Microsoft.AspNetCore.Http.HttpResults;

namespace BagIt.Cloud.API;

public static class CloudApi
{
    public static IEndpointRouteBuilder MapCloudApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api");

        api.MapGet("/commondb", GetCommonDbs);
        api.MapGet("/claims", GetClaims);
        api.MapGet("/tenant", GetTenant);

        api.AllowAnonymous();

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


    public static async Task<Ok<List<KeyValuePair<string, string>>>> GetClaims(HttpContext context)
    {
        var claims = context.User.Claims
            .Select(claim => new KeyValuePair<string, string>(claim.Type, claim.Value))
            .ToList();

        return TypedResults.Ok(claims is { Count: > 0 } ? claims : []);
    }

    private static async Task<Ok<List<CustomerDatabase>>> GetCommonDbs([AsParameters] CloudServices services)
    {
        var customerdbs = await services.CommonContext.CustomerDatabases.ToListAsync() ?? [];

        return TypedResults.Ok(customerdbs);
    }
}
