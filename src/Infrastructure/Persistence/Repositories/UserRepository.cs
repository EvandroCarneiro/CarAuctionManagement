namespace BCA.CarAuctionManagement.Infrastructure.Persistence.Repositories;

using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Infrastructure.Dependencies.Database.SQL;
using BCA.CarAuctionManagement.Infrastructure.Persistence.Mappers;
using BCA.CarAuctionManagement.Infrastructure.Persistence.Repositories.Base;
using BCA.CarAuctionManagement.Infrastructure.Persistence.SQL.Queries;

internal class UserRepository(ISqlDb sqlDb) : BaseRepository(sqlDb), IUserRepository
{
    public async Task CreateAsync(User user)
    {
        await InsertAsync(UserQueries.Insert, new
        {
            user.Id,
            user.CreatedDate,
            user.Name,
        });
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var result = await GetSingleOrDefaultAsync<DBOs.User>(UserQueries.GetById, new { Id = id });

        return result.Map();
    }
}
