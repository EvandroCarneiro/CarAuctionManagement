namespace BCA.CarAuctionManagement.Api.Dto.v1;

using System;
using System.Collections.Generic;

public record Auction(
    Guid Id,
    DateTimeOffset CreatedDate,
    Guid VehicleId,
    bool Active,
    DateTimeOffset? StartedDate,
    DateTimeOffset? ClosedDate,
    DateTimeOffset? UpdatedDate,
    IEnumerable<Bid> Bids) : BaseDto
{
}
