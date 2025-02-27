namespace BCA.CarAuctionManagement.Api.Dto.v1;

public record Bid(DateTimeOffset CreatedDate, decimal Amount, Guid UserId, string UserName) : BaseDto
{
}
