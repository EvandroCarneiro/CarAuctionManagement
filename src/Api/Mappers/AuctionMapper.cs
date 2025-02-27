namespace BCA.CarAuctionManagement.Api.Mappers;

using BCA.CarAuctionManagement.Domain.Common.Mappers;

using Domain = Domain.Models.Auctions;
using Dto = Api.Dto.v1;

public static class AuctionMapper
{
    public static Domain.Auction Map(this Dto.Auction source) => Mapper.Map(source, mapToDomain);

    public static Dto.Auction Map(this Domain.Auction source) => Mapper.Map(source, mapToDto);

    private static readonly Func<Dto.Auction, Domain.Auction> mapToDomain =
        source => new Domain.Auction(source.VehicleId);

    private static readonly Func<Domain.Auction, Dto.Auction> mapToDto =
       source => new Dto.Auction(
           source.Id,
           source.CreatedDate,
           source.VehicleId,
           source.Active,
           source.StartedDate,
           source.ClosedDate,
           source.UpdatedDate,
           source.Bids.Map());
}
