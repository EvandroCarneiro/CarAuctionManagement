namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Queries;

using System;
using System.Threading.Tasks;

using BCA.CarAuctionManagement.Domain.Interfaces.Repositories;
using BCA.CarAuctionManagement.Domain.Models.Vehicles;
using BCA.CarAuctionManagement.Domain.Requests.Queries;

using NSubstitute;

public class GetVehicleByIdQueryHandlerTests : BaseTests
{
    private readonly IVehicleRepository vehicleRepository;
    private readonly GetVehicleByIdQueryHandler handler;

    public GetVehicleByIdQueryHandlerTests()
    {
        vehicleRepository = fixture.Freeze<IVehicleRepository>();
        handler = fixture.Create<GetVehicleByIdQueryHandler>();
    }

    [Fact]
    public async Task Handle_ExistentModel_ShouldReturnResultOkWithModel()
    {
        // Arrange
        var model = fixture.Create<Vehicle>();

        var request = new GetVehicleByIdQuery(model.Id);

        vehicleRepository.GetByIdAsync(model.Id)
            .Returns(model);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(model);
        await vehicleRepository.Received(1).GetByIdAsync(model.Id);
    }

    [Fact]
    public async Task Handle_ModelNotFound_ShouldReturnResultFail()
    {
        // Arrange
        var modelId = Guid.NewGuid();

        var request = new GetVehicleByIdQuery(modelId);

        vehicleRepository.GetByIdAsync(modelId)
            .Returns((Vehicle)null);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        await vehicleRepository.Received(1).GetByIdAsync(modelId);
    }
}
