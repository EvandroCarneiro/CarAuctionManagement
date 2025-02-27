namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Commands;

using System;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Vehicles;
using BCA.CarAuctionManagement.Domain.Requests.Commands;

using NSubstitute;
using NSubstitute.ExceptionExtensions;

public class CreateVehicleCommandHandlerTests : BaseTests
{
    private readonly IVehicleRepository vehicleRepository;
    private readonly CreateVehicleCommandHandler handler;

    public CreateVehicleCommandHandlerTests()
    {
        vehicleRepository = fixture.Freeze<IVehicleRepository>();
        handler = fixture.Create<CreateVehicleCommandHandler>();
    }

    [Fact]
    public async Task Handle_ValidModel_ShouldReturnResultOk()
    {
        // Arrange
        var model = fixture.Create<Vehicle>();

        var command = new CreateVehicleCommand(model);

        vehicleRepository.CheckExistentByExternalId(model.ExternalId)
            .Returns(false);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(model.Id);
        await vehicleRepository.Received(1).CreateAsync(model);
    }

    [Fact]
    public async Task Handle_ExistentExternalId_ShouldReturnResultFail()
    {
        // Arrange
        var model = fixture.Create<Vehicle>();

        var command = new CreateVehicleCommand(model);

        vehicleRepository.CheckExistentByExternalId(model.ExternalId)
            .Returns(true);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        await vehicleRepository.Received(1).CheckExistentByExternalId(model.ExternalId);
        await vehicleRepository.DidNotReceive().CreateAsync(model);
    }

    [Fact]
    public async Task Handle_RepositoryException_ShouldThrowException()
    {
        // Arrange
        var model = fixture.Create<Vehicle>();

        var command = new CreateVehicleCommand(model);

        vehicleRepository.CreateAsync(Arg.Any<Vehicle>())
            .Throws(new Exception());

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>();
        await vehicleRepository.Received(1).CreateAsync(model);
    }
}
