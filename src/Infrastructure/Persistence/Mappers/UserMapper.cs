namespace BCA.CarAuctionManagement.Infrastructure.Persistence.Mappers;

using System;

using BCA.CarAuctionManagement.Domain.Common.Mappers;

using Domain = Domain.Models.Auctions;

internal static class UserMapper
{
    public static Domain.User Map(this DBOs.User source) => Mapper.Map(source, mapToDomain);

    private static readonly Func<DBOs.User, Domain.User> mapToDomain =
        source => new Domain.User(
            source.UserId, 
            source.CreatedDate, 
            source.Name);
}
