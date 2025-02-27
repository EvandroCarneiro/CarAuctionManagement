namespace BCA.CarAuctionManagement.Infrastructure.Persistence.Mappers;

using Domain = Domain.Models.Enums;

internal static class VehicleTypeMapper
{
    public static Domain.VehicleType Map(short source)
        => Enum.TryParse<Domain.VehicleType>(source.ToString(), out var result)
        ? result
        : throw new InvalidCastException();
}
