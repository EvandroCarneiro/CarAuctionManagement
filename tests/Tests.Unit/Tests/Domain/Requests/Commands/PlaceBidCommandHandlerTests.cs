namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Commands;

using System;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Domain.Models.Vehicles;
using BCA.CarAuctionManagement.Domain.Requests.Commands;
using BCA.CarAuctionManagement.Tests.Unit.Extensions;

using NSubstitute;

public class PlaceBidCommandHandlerTests : BaseTests
{
    private readonly IAuctionRepository auctionRepository;
    private readonly IUserRepository userRepository;
    private readonly PlaceBidCommandHandler handler;

    public PlaceBidCommandHandlerTests()
    {
        auctionRepository = fixture.Freeze<IAuctionRepository>();
        userRepository = fixture.Freeze<IUserRepository>();
        handler = fixture.Create<PlaceBidCommandHandler>();
    }

    [Fact]
    public async Task Handle_NotFoundAuction_ShouldReturnResultFail()
    {
        // Arrange
        var bid = fixture.Create<Bid>();

        var request = new PlaceBidCommand(Guid.NewGuid(), bid);

        auctionRepository.GetByIdAsync(request.AuctionId)
            .Returns((Auction)null);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        await auctionRepository.Received(1).GetByIdAsync(request.AuctionId);
        await auctionRepository.DidNotReceive().AddBidAsync(Arg.Any<Guid>(), Arg.Any<Bid>());
    }

    [Fact]
    public async Task Handle_ExistentAndActiveAuctionAndExistentUserAndValidBid_ShouldReturnResultOk()
    {
        // Arrange
        var auctionId = Guid.NewGuid();

        var vehicle = fixture.Create<Vehicle>();

        var auction = fixture.For<Auction>()
            .With(x => x.Id, auctionId)
            .With(x => x.Active, true)
            .With(x => x.StartedDate, DateTimeOffset.UtcNow)
            .With(x => x.ClosedDate, null)
            .With(x => x.Bids, [])
            .With(x => x.VehicleId, vehicle.Id)
            .With(x => x.Vehicle, vehicle)
            .Create();

        var user = fixture.Create<User>();

        var bid = fixture.For<Bid>()
            .With(x => x.Amount, vehicle.StartingBid)
            .With(x => x.UserId, user.Id)
            .Create();

        var command = new PlaceBidCommand(auctionId, bid);

        auctionRepository.GetByIdAsync(auctionId)
            .Returns(auction);

        userRepository.GetByIdAsync(user.Id)
            .Returns(user);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        auction.Bids.Should().HaveCount(1);

        await auctionRepository.Received(1).GetByIdAsync(auctionId);
        await userRepository.Received(1).GetByIdAsync(user.Id);
        await auctionRepository.Received(1).AddBidAsync(auctionId, bid);
    }

    [Fact]
    public async Task Handle_ExistentAndActiveAuctionAndNotFoundUserAndValidBid_ShouldReturnResultFail()
    {
        // Arrange
        var auctionId = Guid.NewGuid();

        var auction = fixture.For<Auction>()
            .With(x => x.Id, auctionId)
            .With(x => x.Active, true)
            .With(x => x.StartedDate, DateTimeOffset.UtcNow)
            .With(x => x.ClosedDate, null)
            .With(x => x.Bids, [])
            .Create();

        var userId = Guid.NewGuid();

        var bid = fixture.For<Bid>()
            .With(x => x.UserId, userId)
            .Create();

        var command = new PlaceBidCommand(auctionId, bid);

        auctionRepository.GetByIdAsync(auctionId)
            .Returns(auction);

        userRepository.GetByIdAsync(userId)
            .Returns((User)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        await auctionRepository.Received(1).GetByIdAsync(auctionId);
        await userRepository.Received(1).GetByIdAsync(userId);
        await auctionRepository.DidNotReceive().AddBidAsync(Arg.Any<Guid>(), Arg.Any<Bid>());
    }

    [Fact]
    public async Task Handle_ExistentAndNotActiveAuctionAndExistentUserAndValidBid_ShouldReturnResultFail()
    {
        // Arrange
        var auctionId = Guid.NewGuid();

        var auction = fixture.For<Auction>()
            .With(x => x.Id, auctionId)
            .With(x => x.Active, false)
            .With(x => x.StartedDate, null)
            .With(x => x.ClosedDate, null)
            .Create();

        var user = fixture.Create<User>();

        var bid = fixture.For<Bid>()
            .With(x => x.UserId, user.Id)
            .Create();

        var command = new PlaceBidCommand(auctionId, bid);

        auctionRepository.GetByIdAsync(auctionId)
            .Returns(auction);

        userRepository.GetByIdAsync(user.Id)
            .Returns(user);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        await auctionRepository.Received(1).GetByIdAsync(auctionId);
        await userRepository.Received(1).GetByIdAsync(user.Id);
        await auctionRepository.DidNotReceive().AddBidAsync(Arg.Any<Guid>(), Arg.Any<Bid>());
    }

    [Fact]
    public async Task Handle_ExistentAndAlreadyClosedAuctionAndExistentUserAndValidBid_ShouldReturnResultFail()
    {
        // Arrange
        var auctionId = Guid.NewGuid();

        var auction = fixture.For<Auction>()
            .With(x => x.Id, auctionId)
            .With(x => x.Active, false)
            .With(x => x.StartedDate, DateTimeOffset.UtcNow)
            .With(x => x.ClosedDate, DateTimeOffset.UtcNow)
            .Create();

        var user = fixture.Create<User>();

        var bid = fixture.For<Bid>()
            .With(x => x.UserId, user.Id)
            .Create();

        var command = new PlaceBidCommand(auctionId, bid);

        auctionRepository.GetByIdAsync(auctionId)
            .Returns(auction);

        userRepository.GetByIdAsync(user.Id)
            .Returns(user);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        await auctionRepository.Received(1).GetByIdAsync(auctionId);
        await userRepository.Received(1).GetByIdAsync(user.Id);
        await auctionRepository.DidNotReceive().AddBidAsync(Arg.Any<Guid>(), Arg.Any<Bid>());
    }

    [Fact]
    public async Task Handle_ExistentAndActiveAuctionAndExistentUserAndBidAmountSmallerThanVehicleStartingBid_ShouldReturnResultFail()
    {
        // Arrange
        var auctionId = Guid.NewGuid();

        var vehicle = fixture.Create<Vehicle>();

        var auction = fixture.For<Auction>()
            .With(x => x.Id, auctionId)
            .With(x => x.Active, true)
            .With(x => x.StartedDate, DateTimeOffset.UtcNow)
            .With(x => x.ClosedDate, null)
            .With(x => x.Bids, [])
            .With(x => x.VehicleId, vehicle.Id)
            .With(x => x.Vehicle, vehicle)
            .Create();

        var user = fixture.Create<User>();

        var bid = fixture.For<Bid>()
            .With(x => x.Amount, auction.Vehicle.StartingBid - 1)
            .With(x => x.UserId, user.Id)
            .Create();

        var command = new PlaceBidCommand(auctionId, bid);

        auctionRepository.GetByIdAsync(auctionId)
            .Returns(auction);

        userRepository.GetByIdAsync(user.Id)
            .Returns(user);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        await auctionRepository.Received(1).GetByIdAsync(auctionId);
        await userRepository.Received(1).GetByIdAsync(user.Id);
        await auctionRepository.DidNotReceive().AddBidAsync(Arg.Any<Guid>(), Arg.Any<Bid>());
    }

    [Fact]
    public async Task Handle_ExistentAndActiveAuctionAndExistentUserAndBidAmountSmallerThanHighestBid_ShouldReturnResultFail()
    {
        // Arrange
        var auctionId = Guid.NewGuid();

        var vehicle = fixture.Create<Vehicle>();

        var lastBid = fixture.For<Bid>()
            .With(x => x.Amount, vehicle.StartingBid + 10)
            .Create();

        var auction = fixture.For<Auction>()
            .With(x => x.Id, auctionId)
            .With(x => x.Active, true)
            .With(x => x.StartedDate, DateTimeOffset.UtcNow)
            .With(x => x.ClosedDate, null)
            .With(x => x.Bids, [lastBid])
            .With(x => x.VehicleId, vehicle.Id)
            .With(x => x.Vehicle, vehicle)
            .Create();

        var user = fixture.Create<User>();

        var bid = fixture.For<Bid>()
            .With(x => x.Amount, auction.Vehicle.StartingBid)
            .With(x => x.UserId, user.Id)
            .Create();

        var command = new PlaceBidCommand(auctionId, bid);

        auctionRepository.GetByIdAsync(auctionId)
            .Returns(auction);

        userRepository.GetByIdAsync(user.Id)
            .Returns(user);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        await auctionRepository.Received(1).GetByIdAsync(auctionId);
        await userRepository.Received(1).GetByIdAsync(user.Id);
        await auctionRepository.DidNotReceive().AddBidAsync(Arg.Any<Guid>(), Arg.Any<Bid>());
    }
}
