namespace BCA.CarAuctionManagement.Api.Dto;

public record PagedResult<T>(
    int PageNumber,
    int TotalPages,
    int TotalItems,
    IEnumerable<T> Entries) : BaseDto
{
}
