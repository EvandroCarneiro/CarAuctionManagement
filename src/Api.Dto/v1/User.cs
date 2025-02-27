namespace BCA.CarAuctionManagement.Api.Dto.v1;

public record User(Guid Id, DateTimeOffset CreatedDate, string Name) : BaseDto
{
}
