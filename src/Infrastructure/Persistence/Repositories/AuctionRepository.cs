namespace BCA.CarAuctionManagement.Infrastructure.Persistence.Repositories;

using System;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Infrastructure.Dependencies.Database.SQL;
using BCA.CarAuctionManagement.Infrastructure.Persistence.Mappers;
using BCA.CarAuctionManagement.Infrastructure.Persistence.Repositories.Base;
using BCA.CarAuctionManagement.Infrastructure.Persistence.SQL.Queries;

internal class AuctionRepository(ISqlDb sqlDb) : BaseRepository(sqlDb), IAuctionRepository
{
    public async Task CreateAsync(Auction auction)
    {
        await InsertAsync(AuctionQueries.Insert, new
        {
            auction.Id,
            auction.CreatedDate,
            auction.VehicleId,
            auction.Active
        });
    }

    public async Task UpdateAsync(Auction auction)
    {
        await UpdateAsync(AuctionQueries.Update, new
        {
            auction.Id,
            auction.Active,
            auction.StartedDate,
            auction.ClosedDate,
            auction.UpdatedDate,
        });
    }

    public async Task<Auction> GetByIdAsync(Guid id)
    {
        var result = await GetAsync<DBOs.Auction, DBOs.Vehicle, DBOs.Auction>(
            AuctionQueries.GetById,
            (auction, vehicle) =>
            {
                auction.Vehicle = vehicle;
                return auction;
            },
            splitOn: "VehicleId",
            new { Id = id });

        var auction = result.SingleOrDefault();

        if (auction is not null)
        {
            var bids = await GetAsync<DBOs.Bid, DBOs.User, DBOs.Bid>(
                AuctionQueries.GetBidsByAuctionId,
                (bid, user) =>
                {
                    bid.User = user;
                    return bid;
                },
                splitOn: "UserId",
                new { AuctionId = id });

            auction.Bids = bids.ToList();
        }

        return auction.Map();
    }

    public async Task<Guid?> GetActiveAuctionIdByVehicleIdAsync(Guid vehicleId)
    {
        var result = await GetSingleOrDefaultAsync<Guid?>(AuctionQueries.GetActiveAuctionIdByVehicleId, new
        {
            VehicleId = vehicleId
        });

        return result;
    }

    public async Task AddBidAsync(Guid auctionId, Bid bid)
    {
        await InsertAsync(AuctionQueries.InsertBid, new
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTimeOffset.UtcNow,
            AuctionId = auctionId,
            bid.Amount,
            UserId = bid.User.Id,
        });
    }
}
