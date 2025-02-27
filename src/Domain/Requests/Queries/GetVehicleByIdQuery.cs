namespace BCA.CarAuctionManagement.Domain.Requests.Queries;

using System.Threading;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Common.Results;
using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Vehicles;
using BCA.CarAuctionManagement.Domain.Requests.Base;

using FluentResults;

using MediatR;

public class GetVehicleByIdQuery(Guid id) : Query<Result<Vehicle>>
{
    public Guid Id { get; } = id;
}

internal class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, Result<Vehicle>>
{
    private readonly IVehicleRepository vehicleRepository;

    public GetVehicleByIdQueryHandler(IVehicleRepository vehicleRepository)
    {
        this.vehicleRepository = vehicleRepository;
    }

    public async Task<Result<Vehicle>> Handle(GetVehicleByIdQuery request, CancellationToken _)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(request.Id);

        if (vehicle is null)
        {
            return Result.Fail(ResultErrors.NotFound("Vehicle not found"));
        }

        return Result.Ok(vehicle);
    }
}
