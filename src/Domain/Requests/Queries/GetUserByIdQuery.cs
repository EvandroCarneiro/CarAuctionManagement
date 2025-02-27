namespace BCA.CarAuctionManagement.Domain.Requests.Queries;

using System.Threading;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Common.Results;
using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Domain.Requests.Base;

using FluentResults;

using MediatR;

public class GetUserByIdQuery(Guid id) : Query<Result<User>>
{
    public Guid Id { get; } = id;
}

internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<User>>
{
    private readonly IUserRepository userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<Result<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id);

        if (user is null)
        {
            return Result.Fail(ResultErrors.NotFound("User not found"));
        }

        return Result.Ok(user);
    }
}