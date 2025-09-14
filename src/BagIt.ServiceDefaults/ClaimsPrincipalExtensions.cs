using System.Security.Claims;

namespace BagIt.ServiceDefaults;

public static class ClaimsPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal principal)
     => principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
    public static string? GetUserName(this ClaimsPrincipal principal) =>
        principal.FindFirst(x => x.Type == ClaimTypes.Name)?.Value;
}