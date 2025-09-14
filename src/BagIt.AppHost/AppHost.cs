var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sqlserver")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var sqlserverpw = sqlServer.Resource.PasswordParameter.Value;

var commondb = sqlServer.AddDatabase("commondb");
var keycloakdb = sqlServer.AddDatabase("keycloakdb");

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
    .WithReference(keycloak).WaitFor(keycloak);

builder.Build().Run();