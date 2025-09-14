using BagIt.Cloud.API.Services;
using Microsoft.AspNetCore.Mvc;

public class CommonServices(CommonContext context, ILogger<CommonServices> logger)
{
    public CommonContext Context { get; set; } = context;
    public ILogger<CommonServices> Logger { get; set; } = logger;
}
