namespace BCA.CarAuctionManagement.Domain.Requests.Queries;

using System.Threading;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Base;
using BCA.CarAuctionManagement.Domain.Models.Enums;
using BCA.CarAuctionManagement.Domain.Models.Vehicles;
using BCA.CarAuctionManagement.Domain.Requests.Base;

using FluentResults;

using MediatR;

public class SearchVehicleQuery(
    PagedFilter pagedFilter,
    IEnumerable<VehicleType> vehicleTypes,
    IEnumerable<string> manufacturers,
    IEnumerable<string> models,
    IEnumerable<int> years) : PagedQuery<Result<PagedResult<Vehicle>>>(pagedFilter)
{
    public IEnumerable<VehicleType> VehicleTypes { get; } = vehicleTypes;
    public IEnumerable<string> Manufacturers { get; } = manufacturers;
    public IEnumerable<string> Models { get; } = models;
    public IEnumerable<int> Years { get; } = years;
}

internal class SearchVehicleQueryHandler : IRequestHandler<SearchVehicleQuery, Result<PagedResult<Vehicle>>>
{
    private readonly IVehicleRepository vehicleRepository;

    public SearchVehicleQueryHandler(IVehicleRepository vehicleRepository)
    {
        this.vehicleRepository = vehicleRepository;
    }

    public async Task<Result<PagedResult<Vehicle>>> Handle(SearchVehicleQuery request, CancellationToken _)
    {
        var pagedVehicles = await vehicleRepository.SearchAsync(
            request.PagedFilter,
            request.VehicleTypes,
            request.Manufacturers,
            request.Models,
            request.Years);

        return Result.Ok(pagedVehicles);
    }
}
