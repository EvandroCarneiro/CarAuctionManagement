namespace BCA.CarAuctionManagement.Infrastructure.Persistence.Repositories;

using System;
using System.Text;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Base;
using BCA.CarAuctionManagement.Domain.Models.Enums;
using BCA.CarAuctionManagement.Domain.Models.Vehicles;
using BCA.CarAuctionManagement.Infrastructure.Dependencies.Database.SQL;
using BCA.CarAuctionManagement.Infrastructure.Persistence.Mappers;
using BCA.CarAuctionManagement.Infrastructure.Persistence.Repositories.Base;
using BCA.CarAuctionManagement.Infrastructure.Persistence.SQL.Queries;

internal class VehicleRepository(ISqlDb sqlDb) : BaseRepository(sqlDb), IVehicleRepository
{
    public async Task CreateAsync(Vehicle vehicle)
    {
        await InsertAsync(VehicleQueries.Insert, new
        {
            vehicle.Id,
            vehicle.CreatedDate,
            vehicle.Type,
            vehicle.ExternalId,
            vehicle.Manufacturer,
            vehicle.Model,
            vehicle.Year,
            vehicle.StartingBid,
            vehicle.NumberOfDoors,
            vehicle.NumberOfSeats,
            vehicle.LoadCapacity,
        });
    }

    public async Task<bool> CheckExistentByExternalId(string externalId)
    {
        var result = await GetSingleOrDefaultAsync<bool>(VehicleQueries.CheckExistentByExternalId, new
        {
            ExternalId = externalId
        });

        return result;
    }

    public async Task<Vehicle> GetByIdAsync(Guid id)
    {
        var result = await GetSingleOrDefaultAsync<DBOs.Vehicle>(VehicleQueries.GetById, new { Id = id });

        return result.Map();
    }

    public async Task<PagedResult<Vehicle>> SearchAsync(
        PagedFilter pagedFilter,
        IEnumerable<VehicleType> vehicleTypes,
        IEnumerable<string> manufacturers,
        IEnumerable<string> models,
        IEnumerable<int> years)
    {
        var searchQuery = new StringBuilder(VehicleQueries.BaseGet);
        var filters = new List<string>();
        var whereClause = new StringBuilder();

        if (HasFilter(vehicleTypes)) filters.Add(VehicleQueries.FilterByVehicleTypes);
        if (HasFilter(manufacturers)) filters.Add(VehicleQueries.FilterByManufacturers);
        if (HasFilter(models)) filters.Add(VehicleQueries.FilterByModels);
        if (HasFilter(years)) filters.Add(VehicleQueries.FilterByYears);

        foreach (var item in filters)
        {
            whereClause.Append(whereClause.Length == 0 ? $"WHERE {item} " : $"AND {item} ");
        }

        searchQuery.Append(whereClause);

        var query = string.Format(BaseQueries.GetPagedBaseQuery, searchQuery, BaseQueries.OrderBy1Query);

        var result = await GetAsync<DBOs.Vehicle>(query, new
        {
            pagedFilter.PageSize,
            pagedFilter.PageNumber,
            Types = vehicleTypes,
            Manufacturers = manufacturers,
            Models = models,
            Years = years
        });

        return new PagedResult<Vehicle>(result.Map().ToList(), pagedFilter.PageNumber, result.FirstOrDefault()?.TotalItems ?? 0);

        static bool HasFilter<T>(IEnumerable<T> source) => source is not null && source.Any();
    }
}
