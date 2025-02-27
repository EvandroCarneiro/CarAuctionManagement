namespace BCA.CarAuctionManagement.Domain.Models.Auctions;

using System;

using BCA.CarAuctionManagement.Domain.Models.Base;
using BCA.CarAuctionManagement.Domain.Models.Vehicles;

using FluentResults;

public class Auction : Entity<Guid>
{
    public Auction(Guid vehicleId)
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTimeOffset.UtcNow;
        Active = false;
        Bids = [];
        VehicleId = vehicleId;
    }

    public Auction(
        Guid id,
        DateTimeOffset createdDate,
        Vehicle vehicle,
        bool active,
        DateTimeOffset? startedDate,
        DateTimeOffset? closedDate,
        DateTimeOffset? updatedDate,
        ICollection<Bid> bids)
        : this(vehicle.Id)
    {
        Id = id;
        CreatedDate = createdDate;
        Vehicle = vehicle;
        Active = active;
        StartedDate = startedDate;
        ClosedDate = closedDate;
        UpdatedDate = updatedDate;
        Bids = bids;
    }

    public Guid VehicleId { get; private set; }

    public Vehicle Vehicle { get; private set; }

    public bool Active { get; private set; }

    public DateTimeOffset? StartedDate { get; private set; }

    public DateTimeOffset? ClosedDate { get; private set; }

    public DateTimeOffset? UpdatedDate { get; private set; }

    public ICollection<Bid> Bids { get; private set; }

    public bool IsClosed => ClosedDate.HasValue;

    internal Result Start()
    {
        if (IsClosed)
        {
            return Result.Fail("Auction already closed");
        }

        Active = true;
        Bids = [];
        StartedDate = DateTimeOffset.UtcNow;
        UpdatedDate = DateTimeOffset.UtcNow;

        return Result.Ok();
    }

    internal Result Close()
    {
        if (!Active)
        {
            return Result.Fail("Auction has not started");
        }

        if (IsClosed)
        {
            return Result.Fail("Auction already closed");
        }

        Active = false;
        ClosedDate = DateTimeOffset.UtcNow;
        UpdatedDate = DateTimeOffset.UtcNow;

        return Result.Ok();
    }

    internal Result AddBid(Bid bid)
    {
        var errorMessage = this switch
        {
            { IsClosed: true } => "Auction already closed",
            { Active: false } => "Auction has not started",
            _ => null
        };

        if (!string.IsNullOrEmpty(errorMessage))
        {
            return Result.Fail(errorMessage);
        }

        if (bid.Amount < Vehicle.StartingBid || bid.Amount <= Bids.MaxBy(x => x.Amount)?.Amount)
        {
            return Result.Fail("Invalid bid amount for vehicle");
        }

        Bids.Add(bid);
        UpdatedDate = DateTimeOffset.UtcNow;

        return Result.Ok();
    }
}
