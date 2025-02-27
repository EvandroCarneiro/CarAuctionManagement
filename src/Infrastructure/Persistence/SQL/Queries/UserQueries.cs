namespace BCA.CarAuctionManagement.Infrastructure.Persistence.SQL.Queries;

internal static class UserQueries
{
    public const string Insert = @"
        INSERT INTO [User]
            ([Id]
            ,[CreatedDate]
            ,[Name])
         VALUES
            (@Id
            ,@CreatedDate
            ,@Name)";

    public const string GetById = @"
        SELECT [Id] AS [UserId]
            ,[CreatedDate]
            ,[Name]
        FROM [User]
        WHERE [Id] = @Id";
}
