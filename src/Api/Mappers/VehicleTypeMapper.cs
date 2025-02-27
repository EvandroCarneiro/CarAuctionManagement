namespace BCA.CarAuctionManagement.Api.Mappers;

using Domain = Domain.Models.Enums;
using Dto = Api.Dto.v1.Enums;

public static class VehicleTypeMapper
{
    public static Dto.VehicleType Map(this Domain.VehicleType source)
        => Enum.TryParse<Dto.VehicleType>(source.ToString(), out var result)
        ? result
        : throw new InvalidCastException();

    public static Domain.VehicleType Map(this Dto.VehicleType source)
        => Enum.TryParse<Domain.VehicleType>(source.ToString(), out var result)
        ? result
        : throw new InvalidCastException();
}
