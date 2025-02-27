namespace BCA.CarAuctionManagement.Infrastructure.Persistence.DBOs;

using System;

internal class Bid
{
    public Guid BidId { get; set; }

    public Guid AuctionId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public decimal Amount { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }
}
