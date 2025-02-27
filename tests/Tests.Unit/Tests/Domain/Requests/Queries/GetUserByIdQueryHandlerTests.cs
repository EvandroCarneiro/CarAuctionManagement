namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Queries;

using System;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Domain.Requests.Queries;

using NSubstitute;

public class GetUserByIdQueryHandlerTests : BaseTests
{
    private readonly IUserRepository userRepository;
    private readonly GetUserByIdQueryHandler handler;

    public GetUserByIdQueryHandlerTests()
    {
        userRepository = fixture.Freeze<IUserRepository>();
        handler = fixture.Create<GetUserByIdQueryHandler>();
    }

    [Fact]
    public async Task Handle_ExistentModel_ShouldReturnResultOkWithModel()
    {
        // Arrange
        var model = fixture.Create<User>();

        var request = new GetUserByIdQuery(model.Id);

        userRepository.GetByIdAsync(model.Id)
            .Returns(model);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(model);
        await userRepository.Received(1).GetByIdAsync(model.Id);
    }

    [Fact]
    public async Task Handle_ModelNotFound_ShouldReturnResultFail()
    {
        // Arrange
        var modelId = Guid.NewGuid();

        var request = new GetUserByIdQuery(modelId);

        userRepository.GetByIdAsync(modelId)
            .Returns((User)null);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        await userRepository.Received(1).GetByIdAsync(modelId);
    }
}
