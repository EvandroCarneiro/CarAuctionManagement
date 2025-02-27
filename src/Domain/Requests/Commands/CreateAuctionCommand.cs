namespace BCA.CarAuctionManagement.Domain.Requests.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Common.Results;
using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Domain.Requests.Base;

using FluentResults;

using MediatR;

public class CreateAuctionCommand(Auction auction) : Command<Result<Guid>>
{
    public Auction Auction { get; } = auction;
}

internal class CreateAuctionCommandHandler : IRequestHandler<CreateAuctionCommand, Result<Guid>>
{
    private readonly IAuctionRepository auctionRepository;
    private readonly IVehicleRepository vehicleRepository;

    public CreateAuctionCommandHandler(IAuctionRepository auctionRepository, IVehicleRepository vehicleRepository)
    {
        this.auctionRepository = auctionRepository;
        this.vehicleRepository = vehicleRepository;
    }

    public async Task<Result<Guid>> Handle(CreateAuctionCommand request, CancellationToken _)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(request.Auction.VehicleId);

        if (vehicle is null)
        {
            return Result.Fail("Invalid vehicle identifier");
        }

        await auctionRepository.CreateAsync(request.Auction);

        return Result.Ok(request.Auction.Id);
    }
}
