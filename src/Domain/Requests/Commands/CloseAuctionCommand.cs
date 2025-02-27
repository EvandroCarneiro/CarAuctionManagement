namespace BCA.CarAuctionManagement.Domain.Requests.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Common.Results;
using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Requests.Base;

using FluentResults;

using MediatR;

public class CloseAuctionCommand(Guid auctionId) : Command<Result>
{
    public Guid AuctionId { get; } = auctionId;
}

internal class CloseAuctionCommandHandler : IRequestHandler<CloseAuctionCommand, Result>
{
    private readonly IAuctionRepository auctionRepository;

    public CloseAuctionCommandHandler(IAuctionRepository auctionRepository)
    {
        this.auctionRepository = auctionRepository;
    }

    public async Task<Result> Handle(CloseAuctionCommand request, CancellationToken _)
    {
        var auction = await auctionRepository.GetByIdAsync(request.AuctionId);

        if (auction == null)
        {
            return Result.Fail(ResultErrors.NotFound("Auction not found"));
        }

        var result = auction.Close();

        if (result.IsFailed)
        {
            return result;
        }

        await auctionRepository.UpdateAsync(auction);

        return Result.Ok();
    }
}
