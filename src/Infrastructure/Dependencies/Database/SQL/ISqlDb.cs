namespace BCA.CarAuctionManagement.Infrastructure.Dependencies.Database.SQL;

using System.Data;

internal interface ISqlDb
{
    public IDbConnection Connection { get; }
}
