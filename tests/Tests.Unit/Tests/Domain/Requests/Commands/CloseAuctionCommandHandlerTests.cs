namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Commands;

using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Domain.Requests.Commands;
using BCA.CarAuctionManagement.Tests.Unit.Extensions;

using NSubstitute;

public class CloseAuctionCommandHandlerTests : BaseTests
{
    private readonly IAuctionRepository auctionRepository;
    private readonly CloseAuctionCommandHandler handler;

    public CloseAuctionCommandHandlerTests()
    {
        auctionRepository = fixture.Freeze<IAuctionRepository>();
        handler = fixture.Create<CloseAuctionCommandHandler>();
    }

    [Fact]
    public async Task Handle_NotFoundAuction_ShouldReturnResultFail()
    {
        // Arrange
        var request = new CloseAuctionCommand(Guid.NewGuid());

        auctionRepository.GetByIdAsync(request.AuctionId)
            .Returns((Auction)null);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        await auctionRepository.Received(1).GetByIdAsync(request.AuctionId);
        await auctionRepository.DidNotReceive().UpdateAsync(Arg.Any<Auction>());
    }

    [Fact]
    public async Task Handle_ExistentAndActiveAuction_ShouldReturnResultOk()
    {
        // Arrange
        var auction = fixture.For<Auction>()
            .With(x => x.Active, true)
            .With(x => x.StartedDate, DateTimeOffset.UtcNow)
            .With(x => x.ClosedDate, null)
            .With(x => x.Bids, [])
            .Create();

        var request = new CloseAuctionCommand(auction.Id);

        auctionRepository.GetByIdAsync(request.AuctionId)
            .Returns(auction);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        auction.ClosedDate.HasValue.Should().BeTrue();
        auction.Active.Should().BeFalse();
        auction.IsClosed.Should().BeTrue();

        await auctionRepository.Received(1).GetByIdAsync(request.AuctionId);
        await auctionRepository.Received(1).UpdateAsync(auction);
    }

    [Fact]
    public async Task Handle_ExistentAndNotActiveAuction_ShouldReturnResultFail()
    {
        // Arrange
        var auction = fixture.For<Auction>()
            .With(x => x.Active, false)
            .With(x => x.StartedDate, null)
            .With(x => x.ClosedDate, null)
            .With(x => x.Bids, [])
            .Create();

        var request = new CloseAuctionCommand(auction.Id);

        auctionRepository.GetByIdAsync(request.AuctionId)
            .Returns(auction);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();

        auction.Active.Should().BeFalse();

        await auctionRepository.Received(1).GetByIdAsync(request.AuctionId);
        await auctionRepository.DidNotReceive().UpdateAsync(Arg.Any<Auction>());
    }

    [Fact]
    public async Task Handle_ExistentAndAlreadyClosedAuction_ShouldReturnResultFail()
    {
        // Arrange
        var auction = fixture.For<Auction>()
            .With(x => x.Active, false)
            .With(x => x.StartedDate, DateTimeOffset.UtcNow)
            .With(x => x.ClosedDate, DateTimeOffset.UtcNow)
            .Create();

        var request = new CloseAuctionCommand(auction.Id);

        auctionRepository.GetByIdAsync(request.AuctionId)
            .Returns(auction);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();

        await auctionRepository.Received(1).GetByIdAsync(request.AuctionId);
        await auctionRepository.DidNotReceive().UpdateAsync(Arg.Any<Auction>());
    }
}
