namespace BagIt.AppHost.Extensions;

public static class Extensions
{
    public static ReferenceExpression GetSqlServerJdbcConnectionString(
        IResourceBuilder<SqlServerDatabaseResource> sqlServerDatabaseResourceBuilder)
    {
        var sqlServerDatabaseResource = sqlServerDatabaseResourceBuilder.Resource;
        var sqlServerServerResource = sqlServerDatabaseResource.Parent;
        var sqlServerEndpoint = sqlServerServerResource.PrimaryEndpoint;
        
        return ReferenceExpression.Create($"jdbc:sqlserver://{sqlServerEndpoint.Property(EndpointProperty.Host)}:{sqlServerEndpoint.Property(EndpointProperty.Port)};instanceName={sqlServerEndpoint.Property(EndpointProperty.Host)};databaseName={sqlServerDatabaseResource.Name};encrypt=false;trustServerCertificate=false;loginTimeout=30");
    }
}