namespace BCA.CarAuctionManagement.Domain.Interfaces.Repositories;

using BCA.CarAuctionManagement.Domain.Interfaces.Base;
using BCA.CarAuctionManagement.Domain.Models.Auctions;

public interface IAuctionRepository : IRepository
{
    Task CreateAsync(Auction auction);

    Task UpdateAsync(Auction auction);

    Task<Auction> GetByIdAsync(Guid id);

    Task<Guid?> GetActiveAuctionIdByVehicleIdAsync(Guid vehicleId);

    Task AddBidAsync(Guid auctionId, Bid bid);
}
