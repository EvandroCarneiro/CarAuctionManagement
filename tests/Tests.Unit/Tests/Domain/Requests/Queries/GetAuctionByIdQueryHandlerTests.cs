namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Queries;

using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Domain.Requests.Queries;

using NSubstitute;

public class GetAuctionByIdQueryHandlerTests : BaseTests
{
    private readonly IAuctionRepository auctionRepository;
    private readonly GetAuctionByIdQueryHandler handler;

    public GetAuctionByIdQueryHandlerTests()
    {
        auctionRepository = fixture.Freeze<IAuctionRepository>();
        handler = fixture.Create<GetAuctionByIdQueryHandler>();
    }

    [Fact]
    public async Task Handle_ExistentModel_ShouldReturnResultOkWithModel()
    {
        // Arrange
        var model = fixture.Create<Auction>();

        var request = new GetAuctionByIdQuery(model.Id);

        auctionRepository.GetByIdAsync(model.Id)
            .Returns(model);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(model);
        await auctionRepository.Received(1).GetByIdAsync(model.Id);
    }

    [Fact]
    public async Task Handle_ModelNotFound_ShouldReturnResultFail()
    {
        // Arrange
        var modelId = Guid.NewGuid();

        var request = new GetAuctionByIdQuery(modelId);

        auctionRepository.GetByIdAsync(modelId)
            .Returns((Auction)null);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        await auctionRepository.Received(1).GetByIdAsync(modelId);
    }
}
