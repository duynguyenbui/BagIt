namespace BagIt.Cloud.API;

public class TenantProvider
{
    private const string TenantHeader = "X-TenantId";

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CommonContext _commonContext;

    public TenantProvider(IHttpContextAccessor httpContextAccessor, CommonContext commonContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _commonContext = commonContext;
    }

    public string TenantId => _httpContextAccessor.HttpContext.Request.Headers[TenantHeader];

    public string GetTenantConnection()
    {
        if (!int.TryParse(TenantId, out var tenantId))
            return string.Empty;

        return _commonContext.CustomerDatabases
            .FirstOrDefault(t => t.Id == tenantId)
            ?.ConnectionString ?? string.Empty;
    }
}
