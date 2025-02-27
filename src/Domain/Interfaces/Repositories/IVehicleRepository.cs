namespace BCA.CarAuctionManagement.Domain.Interfaces.Repositories;

using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Interfaces.Base;
using BCA.CarAuctionManagement.Domain.Models.Base;
using BCA.CarAuctionManagement.Domain.Models.Enums;
using BCA.CarAuctionManagement.Domain.Models.Vehicles;

public interface IVehicleRepository : IRepository
{
    Task CreateAsync(Vehicle vehicle);

    Task<bool> CheckExistentByExternalId(string externalId);

    Task<Vehicle> GetByIdAsync(Guid id);

    Task<PagedResult<Vehicle>> SearchAsync(
        PagedFilter pagedFilter,
        IEnumerable<VehicleType> vehicleTypes,
        IEnumerable<string> manufacturers,
        IEnumerable<string> models,
        IEnumerable<int> years);
}
