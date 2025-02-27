namespace BCA.CarAuctionManagement.Infrastructure.Persistence.Mappers;

using System;

using BCA.CarAuctionManagement.Domain.Common.Mappers;

using Domain = Domain.Models.Auctions;

internal static class AuctionMapper
{
    public static Domain.Auction Map(this DBOs.Auction source) => Mapper.Map(source, mapToDomain);

    private static readonly Func<DBOs.Auction, Domain.Auction> mapToDomain =
        source => new Domain.Auction(
            source.AuctionId,
            source.CreatedDate,
            source.Vehicle.Map(),
            source.Active,
            source.StartedDate,
            source.ClosedDate,
            source.UpdatedDate,
            source.Bids.Map().ToList());
}
