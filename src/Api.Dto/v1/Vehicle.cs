namespace BCA.CarAuctionManagement.Api.Dto.v1;

using BCA.CarAuctionManagement.Api.Dto.v1.Enums;

public record Vehicle(
   Guid Id,
   DateTimeOffset CreatedDate,
   VehicleType Type,
   string ExternalId,
   string Manufacturer,
   string Model,
   int Year,
   decimal StartingBid,
   int? NumberOfDoors,
   int? NumberOfSeats,
   decimal? LoadCapacity) : BaseDto
{
}
