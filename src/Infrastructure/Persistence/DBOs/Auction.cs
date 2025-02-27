namespace BCA.CarAuctionManagement.Infrastructure.Persistence.DBOs;

using System;
using System.Collections.Generic;

internal class Auction
{
    public Guid AuctionId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public Guid VehicleId { get; }

    public Vehicle Vehicle { get; set; }

    public bool Active { get; set; }

    public DateTimeOffset? StartedDate { get; set; }

    public DateTimeOffset? ClosedDate { get; set; }

    public DateTimeOffset? UpdatedDate { get; set; }

    public ICollection<Bid> Bids { get; set; } = [];
}
