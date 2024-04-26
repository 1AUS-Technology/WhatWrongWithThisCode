using Microsoft.Data.SqlClient;
using System.Threading;

namespace DroneManagement.Health;

public interface IDatabaseVersionInquirer
{
    Task<string?> QueryDatabaseVersion(string connectionString, string query, CancellationToken cancellationToken = default);
}

public class DatabaseVersionInquirer : IDatabaseVersionInquirer
{
    public async Task<string?> QueryDatabaseVersion(string connectionString, string query, CancellationToken cancellationToken = default)
    {
        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

        using var command = connection.CreateCommand();
        command.CommandText = query;

        var databaseVersion = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false) as string;

        return databaseVersion;
    }
}


