namespace BCA.CarAuctionManagement.Api.Mappers;

using BCA.CarAuctionManagement.Domain.Common.Mappers;

using Domain = Domain.Models.Auctions;
using Dto = Api.Dto.v1;

public static class UserMapper
{
    public static Domain.User Map(this Dto.User source) => Mapper.Map(source, mapToDomain);

    public static Dto.User Map(this Domain.User source) => Mapper.Map(source, mapToDto);

    private static readonly Func<Dto.User, Domain.User> mapToDomain =
        source => new Domain.User(source.Name);

    private static readonly Func<Domain.User, Dto.User> mapToDto =
       source => new Dto.User(
           source.Id,
           source.CreatedDate,
           source.Name);
}
