namespace BCA.CarAuctionManagement.Domain.Requests.Commands;

using System.Threading;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Common.Results;
using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Vehicles;
using BCA.CarAuctionManagement.Domain.Requests.Base;

using FluentResults;

using MediatR;

public class CreateVehicleCommand(Vehicle vehicle) : Command<Result<Guid>>
{
    public Vehicle Vehicle { get; } = vehicle;
}

internal class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Result<Guid>>
{
    private readonly IVehicleRepository vehicleRepository;

    public CreateVehicleCommandHandler(IVehicleRepository vehicleRepository)
    {
        this.vehicleRepository = vehicleRepository;
    }

    public async Task<Result<Guid>> Handle(CreateVehicleCommand request, CancellationToken _)
    {
        var duplicatedExternalId = await vehicleRepository.CheckExistentByExternalId(request.Vehicle.ExternalId);

        if (duplicatedExternalId)
        {
            return Result.Fail(ResultErrors.Conflict("ExternalId already exists"));
        }

        await vehicleRepository.CreateAsync(request.Vehicle);

        return Result.Ok(request.Vehicle.Id);
    }
}