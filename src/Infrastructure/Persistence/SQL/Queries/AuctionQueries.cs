namespace BCA.CarAuctionManagement.Infrastructure.Persistence.SQL.Queries;

internal static class AuctionQueries
{
    public const string Insert = @"
        INSERT INTO [Auction]
           ([Id]
           ,[CreatedDate]
           ,[VehicleId]
           ,[Active])
        VALUES
           (@Id
           ,@CreatedDate
           ,@VehicleId
           ,@Active)";

    public const string Update = @"
        UPDATE [Auction]
        SET [Active] = @Active
            ,[StartedDate] = @StartedDate
            ,[ClosedDate] = @ClosedDate
            ,[UpdatedDate] = @UpdatedDate
         WHERE [Id] = @Id";

    private const string BaseGet = @"
        SELECT [Auction].[Id] AS [AuctionId]
            ,[Auction].[CreatedDate]
            ,[Auction].[Active]
            ,[Auction].[StartedDate]
            ,[Auction].[ClosedDate]
            ,[Auction].[UpdatedDate]
            ,[Auction].[VehicleId]
            ,[Vehicle].[CreatedDate]
            ,[Vehicle].[Type]
            ,[Vehicle].[ExternalId]
            ,[Vehicle].[Manufacturer]
            ,[Vehicle].[Model]
            ,[Vehicle].[Year]
            ,[Vehicle].[StartingBid]
            ,[Vehicle].[NumberOfDoors]
            ,[Vehicle].[NumberOfSeats]
            ,[Vehicle].[LoadCapacity]
        FROM [Auction]
        INNER JOIN [Vehicle] ON [Vehicle].[Id] = [Auction].[VehicleId]";

    public const string GetById = @$"
        {BaseGet}
        WHERE [Auction].[Id] = @Id";

    public const string GetActiveAuctionIdByVehicleId = @$"
        SELECT [Id]
        FROM [Auction]
        WHERE [VehicleId] = @VehicleId
        AND [Active] = 1";

    public const string InsertBid = @"
        INSERT INTO [Bid]
           ([Id]
           ,[AuctionId]
           ,[CreatedDate]
           ,[Amount]
           ,[UserId])
         VALUES
            (@Id
            ,@AuctionId
            ,@CreatedDate
            ,@Amount
            ,@UserId)";

    public const string GetBidsByAuctionId = @"
        SELECT [Bid].[Id] AS [BidId]
            ,[Bid].[AuctionId]
            ,[Bid].[CreatedDate]
            ,[Bid].[Amount]
            ,[Bid].[UserId]
            ,[User].[Id]
            ,[User].[CreatedDate]
            ,[User].[Name]
        FROM [Bid]
        INNER JOIN [User] ON [User].[Id] = [Bid].[UserId]
        WHERE [AuctionId] = @AuctionId";
}
