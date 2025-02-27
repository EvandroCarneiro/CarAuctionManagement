namespace BCA.CarAuctionManagement.Api.Mappers;

using BCA.CarAuctionManagement.Domain.Common.Mappers;

using Domain = Domain.Models.Auctions;
using Dto = Api.Dto.v1;

public static class BidMapper
{
    public static Domain.Bid Map(this Dto.Bid source) => Mapper.Map(source, mapToDomain);

    public static IEnumerable<Dto.Bid> Map(this IEnumerable<Domain.Bid> source) => Mapper.Map(source, mapToDto);

    private static readonly Func<Dto.Bid, Domain.Bid> mapToDomain =
        source => new Domain.Bid(
            source.Amount,
            source.UserId);

    private static readonly Func<Domain.Bid, Dto.Bid> mapToDto =
       source => new Dto.Bid(
           source.CreatedDate,
           source.Amount,
           source.User.Id,
           source.User.Name);
}
