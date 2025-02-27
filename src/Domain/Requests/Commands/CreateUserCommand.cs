namespace BCA.CarAuctionManagement.Domain.Requests.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Domain.Requests.Base;

using FluentResults;

using MediatR;

public class CreateUserCommand(User user) : Command<Result<Guid>>
{
    public User User { get; } = user;
}

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUserRepository userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken _)
    {
        await userRepository.CreateAsync(request.User);

        return Result.Ok(request.User.Id);
    }
}
