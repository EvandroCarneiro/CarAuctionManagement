namespace BCA.CarAuctionManagement.Api.Mappers;

using BCA.CarAuctionManagement.Domain.Common.Mappers;

using Domain = Domain.Models.Vehicles;
using Dto = Api.Dto.v1;

public static class VehicleMapper
{
    public static Domain.Vehicle Map(this Dto.Vehicle source) => Mapper.Map(source, mapToDomain);

    public static Dto.Vehicle Map(this Domain.Vehicle source) => Mapper.Map(source, mapToDto);

    public static IEnumerable<Dto.Vehicle> Map(this IEnumerable<Domain.Vehicle> source) => Mapper.Map(source, mapToDto);

    private static readonly Func<Dto.Vehicle, Domain.Vehicle> mapToDomain =
        source => new Domain.Vehicle(
            source.Type.Map(),
            source.ExternalId,
            source.Manufacturer,
            source.Model,
            source.Year,
            source.StartingBid,
            source.NumberOfDoors,
            source.NumberOfSeats,
            source.LoadCapacity);

    private static readonly Func<Domain.Vehicle, Dto.Vehicle> mapToDto =
       source => new Dto.Vehicle(
           source.Id,
           source.CreatedDate,
           source.Type.Map(),
           source.ExternalId,
           source.Manufacturer,
           source.Model,
           source.Year,
           source.StartingBid,
           source.NumberOfDoors,
           source.NumberOfSeats,
           source.LoadCapacity);
}
