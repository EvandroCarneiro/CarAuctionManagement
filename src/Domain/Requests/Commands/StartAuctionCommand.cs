namespace BCA.CarAuctionManagement.Domain.Requests.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Common.Results;
using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Requests.Base;

using FluentResults;

using MediatR;

public class StartAuctionCommand(Guid auctionId) : Command<Result>
{
    public Guid AuctionId { get; } = auctionId;
}

internal class StartAuctionCommandHandler : IRequestHandler<StartAuctionCommand, Result>
{
    private readonly IAuctionRepository auctionRepository;

    public StartAuctionCommandHandler(IAuctionRepository auctionRepository)
    {
        this.auctionRepository = auctionRepository;
    }

    public async Task<Result> Handle(StartAuctionCommand request, CancellationToken _)
    {
        var auction = await auctionRepository.GetByIdAsync(request.AuctionId);

        if (auction == null)
        {
            return Result.Fail(ResultErrors.NotFound("Auction not found"));
        }

        if (auction.Active)
        {
            return Result.Fail("Auction already started");
        }

        var activeAuctionId = await auctionRepository.GetActiveAuctionIdByVehicleIdAsync(auction.VehicleId);

        if (activeAuctionId.HasValue)
        {
            return Result.Fail($"There is an auction already started for this vehicle: {activeAuctionId}");
        }

        var result = auction.Start();

        if (result.IsFailed)
        {
            return result;
        }

        await auctionRepository.UpdateAsync(auction);

        return Result.Ok();
    }
}
