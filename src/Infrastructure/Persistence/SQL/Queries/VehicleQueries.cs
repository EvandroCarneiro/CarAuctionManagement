namespace BCA.CarAuctionManagement.Infrastructure.Persistence.SQL.Queries;

internal static class VehicleQueries
{
    public const string Insert = @"
        INSERT INTO [Vehicle]
            ([Id]
            ,[CreatedDate]
            ,[Type]
            ,[ExternalId]
            ,[Manufacturer]
            ,[Model]
            ,[Year]
            ,[StartingBid]
            ,[NumberOfDoors]
            ,[NumberOfSeats]
            ,[LoadCapacity])
        VALUES
            (@Id
            ,@CreatedDate
            ,@Type
            ,@ExternalId
            ,@Manufacturer
            ,@Model
            ,@Year
            ,@StartingBid
            ,@NumberOfDoors
            ,@NumberOfSeats
            ,@LoadCapacity)";

    public const string BaseGet = @"
        SELECT [Id] AS [VehicleId]
          ,[CreatedDate]
          ,[Type]
          ,[ExternalId]
          ,[Manufacturer]
          ,[Model]
          ,[Year]
          ,[StartingBid]
          ,[NumberOfDoors]
          ,[NumberOfSeats]
          ,[LoadCapacity]
        FROM [Vehicle]";

    public const string GetById = @$"
        {BaseGet}
        WHERE [Id] = @Id";

    public const string CheckExistentByExternalId = @$"
        SELECT 1 FROM [Vehicle]
        WHERE [ExternalId] = @ExternalId";

    public const string FilterByVehicleTypes = "[Type] IN @Types";

    public const string FilterByManufacturers = "[Manufacturer] IN @Manufacturers";

    public const string FilterByModels = "[Model] IN @Models";

    public const string FilterByYears = "[Year] IN @Years";
}
