namespace BCA.CarAuctionManagement.Infrastructure.Persistence.Mappers;

using System;
using System.Collections.Generic;

using BCA.CarAuctionManagement.Domain.Common.Mappers;

using Domain = Domain.Models.Vehicles;

internal static class VehicleMapper
{
    public static Domain.Vehicle Map(this DBOs.Vehicle source) => Mapper.Map(source, mapToDomain);

    public static IEnumerable<Domain.Vehicle> Map(this IEnumerable<DBOs.Vehicle> source) => Mapper.Map(source, mapToDomain);

    private static readonly Func<DBOs.Vehicle, Domain.Vehicle> mapToDomain =
        source => new Domain.Vehicle(
            source.VehicleId,
            source.CreatedDate,
            VehicleTypeMapper.Map(source.Type),
            source.ExternalId,
            source.Manufacturer,
            source.Model,
            source.Year,
            source.StartingBid,
            source.NumberOfDoors,
            source.NumberOfSeats,
            source.LoadCapacity);
}
