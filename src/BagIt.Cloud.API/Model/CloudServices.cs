using BagIt.Cloud.API.Infrastructure.Customers;
using BagIt.Cloud.API.Services;
using Microsoft.AspNetCore.Mvc;

public class CloudServices(CommonContext commonContext, CustomerContext customerContext, ILogger<CloudServices> logger)
{
    public CommonContext CommonContext { get; set; } = commonContext;
    public CustomerContext CustomerContext { get; set; } = customerContext;
    public ILogger<CloudServices> Logger { get; set; } = logger;
}
