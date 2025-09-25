var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sqlserver")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var sqlserverpw = sqlServer.Resource.PasswordParameter.Value;

var commondb = sqlServer.AddDatabase("commondb");
var keycloakdb = sqlServer.AddDatabase("keycloakdb");
var tenant1 = sqlServer.AddDatabase("tenant1");
var tenant2 = sqlServer.AddDatabase("tenant2");

var keycloak = builder.AddKeycloak("keycloak", 8080)
    .WithReference(keycloakdb).WaitFor(keycloakdb)  
    .WithEnvironment("KC_DB_VENDOR", "mssql")
    .WithEnvironment("KC_DB", "mssql")
    .WithEnvironment("KC_DB_USERNAME", "sa")
    .WithEnvironment("KC_DB_PASSWORD", sqlserverpw)
    .WithEnvironment("KC_DB_URL", Extensions.GetSqlServerJdbcConnectionString(keycloakdb))
    .WithRealmImport("../../realms");

var cloudapi = builder.AddProject<Projects.BagIt_Cloud_API>("cloudapi")
    .WithReference(commondb).WaitFor(commondb)
    .WithReference(keycloak)// .WaitFor(keycloak)
    .WithReference(tenant1).WaitFor(tenant1)
    .WithReference(tenant2).WaitFor(tenant2);

builder.Build().Run();