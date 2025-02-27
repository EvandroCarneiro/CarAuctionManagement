namespace BCA.CarAuctionManagement.Infrastructure.Persistence.Repositories.Base;

using System;

using BCA.CarAuctionManagement.Infrastructure.Dependencies.Database.SQL;

using Dapper;

internal class BaseRepository(ISqlDb sqlDb)
{
    protected readonly ISqlDb sqlDb = sqlDb;

    public async Task InsertAsync(string query, object parameters)
        => await sqlDb.Connection.ExecuteAsync(query, parameters);

    public async Task UpdateAsync(string query, object parameters)
        => await sqlDb.Connection.ExecuteAsync(query, parameters);

    public async Task<T> GetSingleOrDefaultAsync<T>(string query, object parameters)
        => await sqlDb.Connection.QuerySingleOrDefaultAsync<T>(query, parameters);

    public async Task<IEnumerable<TReturn>> GetAsync<TFirst, TSecond, TReturn>(
        string query, Func<TFirst, TSecond, TReturn> map, string splitOn, object parameters)
        => await sqlDb.Connection.QueryAsync(query, map, parameters, splitOn: splitOn);

    public async Task<IEnumerable<T>> GetAsync<T>(string query, object parameters = null)
        => await sqlDb.Connection.QueryAsync<T>(query, parameters);
}
