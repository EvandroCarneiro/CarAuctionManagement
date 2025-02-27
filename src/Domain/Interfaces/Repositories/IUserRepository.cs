namespace BCA.CarAuctionManagement.Domain.Interfaces.Repositories;

using BCA.CarAuctionManagement.Domain.Interfaces.Base;
using BCA.CarAuctionManagement.Domain.Models.Auctions;

public interface IUserRepository : IRepository
{
    Task CreateAsync(User user);

    Task<User> GetByIdAsync(Guid id);
}
