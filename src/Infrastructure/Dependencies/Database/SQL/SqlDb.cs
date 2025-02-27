namespace BCA.CarAuctionManagement.Infrastructure.Dependencies.Database.SQL;

using System.Data;
using System.Data.Common;

using BCA.CarAuctionManagement.Infrastructure.Settings;

using Microsoft.Data.SqlClient;

internal class SqlDb : ISqlDb
{
    private readonly Lazy<DbConnection> lazyConnection;

    public SqlDb(SqlServerDependencySetting sqlServerDependencySetting)
    {
        var connectionString = new SqlConnectionStringBuilder(sqlServerDependencySetting.ConnectionString);

        lazyConnection = new Lazy<DbConnection>(() => new SqlConnection(connectionString.ToString()));
    }

    public IDbConnection Connection => lazyConnection.Value;
}
