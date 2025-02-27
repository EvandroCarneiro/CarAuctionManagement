namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Commands;

using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Domain.Models.Vehicles;
using BCA.CarAuctionManagement.Domain.Requests.Commands;
using BCA.CarAuctionManagement.Tests.Unit.Extensions;

using NSubstitute;
using NSubstitute.ExceptionExtensions;

public class CreateAuctionCommandHandlerTests : BaseTests
{
    private readonly IAuctionRepository auctionRepository;
    private readonly IVehicleRepository vehicleRepository;
    private readonly CreateAuctionCommandHandler handler;

    public CreateAuctionCommandHandlerTests()
    {
        auctionRepository = fixture.Freeze<IAuctionRepository>();
        vehicleRepository = fixture.Freeze<IVehicleRepository>();
        handler = fixture.Create<CreateAuctionCommandHandler>();
    }

    [Fact]
    public async Task Handle_ValidModel_ShouldReturnResultOk()
    {
        // Arrange
        var vehicle = fixture.Create<Vehicle>();

        var model = fixture.For<Auction>()
            .With(x => x.VehicleId, vehicle.Id)
            .Create();

        var command = new CreateAuctionCommand(model);

        vehicleRepository.GetByIdAsync(vehicle.Id)
            .Returns(vehicle);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(model.Id);
        await vehicleRepository.Received(1).GetByIdAsync(vehicle.Id);
        await auctionRepository.Received(1).CreateAsync(model);
    }

    [Fact]
    public async Task Handle_VehicleNotFound_ShouldReturnResultFail()
    {
        // Arrange
        var model = fixture.Create<Auction>();

        var command = new CreateAuctionCommand(model);

        vehicleRepository.GetByIdAsync(Arg.Any<Guid>())
            .Returns((Vehicle)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        await vehicleRepository.Received(1).GetByIdAsync(model.VehicleId);
        await auctionRepository.DidNotReceive().CreateAsync(model);
    }

    [Fact]
    public async Task Handle_AuctionRepositoryException_ShouldThrowException()
    {
        // Arrange
        var vehicle = fixture.Create<Vehicle>();

        var model = fixture.For<Auction>()
            .With(x => x.VehicleId, vehicle.Id)
            .Create();

        var command = new CreateAuctionCommand(model);

        vehicleRepository.GetByIdAsync(vehicle.Id)
            .Returns(vehicle);

        auctionRepository.CreateAsync(Arg.Any<Auction>())
            .Throws(new Exception());

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>();
        await auctionRepository.Received(1).CreateAsync(model);
    }
}
