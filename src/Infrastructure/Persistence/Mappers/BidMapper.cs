namespace BCA.CarAuctionManagement.Infrastructure.Persistence.Mappers;

using System;
using System.Collections.Generic;

using BCA.CarAuctionManagement.Domain.Common.Mappers;

using Domain = Domain.Models.Auctions;

internal static class BidMapper
{
    public static IEnumerable<Domain.Bid> Map(this IEnumerable<DBOs.Bid> source) => Mapper.Map(source, mapToDomain);

    private static readonly Func<DBOs.Bid, Domain.Bid> mapToDomain =
        source => new Domain.Bid(
            source.CreatedDate,
            source.Amount,
            source.User.Map());
}
