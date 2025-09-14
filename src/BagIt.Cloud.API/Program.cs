var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddApplicationServices();

var app = builder.Build();

app.UseExceptionHandler();
app.MapDefaultEndpoints();

app.MapCloudApi();

app.Run();