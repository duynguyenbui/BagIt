
using System.Collections;

using Microsoft.AspNetCore.Http.HttpResults;

namespace BagIt.Cloud.API;

public static class CloudApi
{
    public static IEndpointRouteBuilder MapCloudApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api");

        api.MapGet("/commondb", GetCommonDbs);
        api.MapGet("/claims", GetClaims);

        api.AllowAnonymous();

        return app;
    }

    public static async Task<Ok<List<KeyValuePair<string, string>>>> GetClaims(HttpContext context)
    {
        var claims = context.User.Claims
            .Select(claim => new KeyValuePair<string, string>(claim.Type, claim.Value))
            .ToList();

        return TypedResults.Ok(claims is { Count: > 0 } ? claims : []);
    }


    private static async Task<Ok<List<CustomerDatabase>>> GetCommonDbs([AsParameters] CommonServices services)
    {
        var customerdbs = await services.Context.CustomerDatabases.ToListAsync() ?? [];

        return TypedResults.Ok(customerdbs);
    }
}
