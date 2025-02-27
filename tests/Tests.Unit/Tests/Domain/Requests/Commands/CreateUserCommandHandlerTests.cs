namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Commands;

using System;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Domain.Requests.Commands;

using NSubstitute;
using NSubstitute.ExceptionExtensions;

public class CreateUserCommandHandlerTests : BaseTests
{
    private readonly IUserRepository userRepository;
    private readonly CreateUserCommandHandler handler;

    public CreateUserCommandHandlerTests()
    {
        userRepository = fixture.Freeze<IUserRepository>();
        handler = fixture.Create<CreateUserCommandHandler>();
    }

    [Fact]
    public async Task Handle_ValidModel_ShouldReturnResultOk()
    {
        // Arrange
        var model = fixture.Create<User>();

        var command = new CreateUserCommand(model);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(model.Id);
        await userRepository.Received(1).CreateAsync(model);
    }

    [Fact]
    public async Task Handle_RepositoryException_ShouldThrowException()
    {
        // Arrange
        var model = fixture.Create<User>();

        var command = new CreateUserCommand(model);

        userRepository.CreateAsync(Arg.Any<User>())
            .Throws(new Exception());

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>();
        await userRepository.Received(1).CreateAsync(model);
    }
}
