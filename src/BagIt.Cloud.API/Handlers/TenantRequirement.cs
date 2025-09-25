using Microsoft.AspNetCore.Authorization;

namespace BagIt.Cloud.API.Handlers;

public sealed class TenantRequirement : IAuthorizationRequirement { }