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

public class PlaceBidCommand(Guid auctionId, Bid bid) : Command<Result>
{
    public Guid AuctionId { get; } = auctionId;

    public Bid Bid { get; } = bid;
}

internal class PlaceBidCommandHandler : IRequestHandler<PlaceBidCommand, Result>
{
    private readonly IAuctionRepository auctionRepository;
    private readonly IUserRepository userRepository;

    public PlaceBidCommandHandler(IAuctionRepository auctionRepository, IUserRepository userRepository)
    {
        this.auctionRepository = auctionRepository;
        this.userRepository = userRepository;
    }

    public async Task<Result> Handle(PlaceBidCommand request, CancellationToken _)
    {
        var auction = await auctionRepository.GetByIdAsync(request.AuctionId);

        if (auction is null)
        {
            return Result.Fail(ResultErrors.NotFound("Auction not found"));
        }

        var user = await userRepository.GetByIdAsync(request.Bid.UserId);

        if (user is null)
        {
            return Result.Fail(ResultErrors.NotFound("User not found"));
        }

        request.Bid.SetUser(user);

        var result = auction.AddBid(request.Bid);

        if (result.IsFailed)
        {
            return result;
        }

        await auctionRepository.AddBidAsync(auction.Id, request.Bid);

        return Result.Ok();
    }
}