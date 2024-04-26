using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DroneManagement.Health;

public class SqlDatabaseServerHealthCheck(SqlServerHealthSettings options, IDatabaseVersionInquirer databaseVersionInquirer) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
    {
        var versionStoredInDatabase = await databaseVersionInquirer.QueryDatabaseVersion(options.ConnectionString, options.Query, cancellationToken);

        if (string.IsNullOrEmpty(versionStoredInDatabase))
        {
            return HealthCheckResult.Unhealthy("The database version is missing.");
        }

        if (versionStoredInDatabase == options.ExpectedVersionInSettingFile)
        {
            return HealthCheckResult.Healthy();
        }

        return HealthCheckResult.Unhealthy("The database version does not match the expected version.");
    }
}