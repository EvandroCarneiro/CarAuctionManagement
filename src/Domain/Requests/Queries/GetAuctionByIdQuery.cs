namespace BCA.CarAuctionManagement.Domain.Requests.Queries;

using System.Threading;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Common.Results;
using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Domain.Requests.Base;

using FluentResults;

using MediatR;

public class GetAuctionByIdQuery(Guid id) : Query<Result<Auction>>
{
    public Guid Id { get; } = id;
}

internal class GetAuctionByIdQueryHandler : IRequestHandler<GetAuctionByIdQuery, Result<Auction>>
{
    private readonly IAuctionRepository auctionRepository;

    public GetAuctionByIdQueryHandler(IAuctionRepository auctionRepository)
    {
        this.auctionRepository = auctionRepository;
    }

    public async Task<Result<Auction>> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken)
    {
        var auction = await auctionRepository.GetByIdAsync(request.Id);

        if (auction is null)
        {
            return Result.Fail(ResultErrors.NotFound("Auction not found"));
        }

        return Result.Ok(auction);
    }
}