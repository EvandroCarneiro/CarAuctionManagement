namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Commands.Validators;

using System.Reflection.Emit;

using BCA.CarAuctionManagement.Domain.Models.Enums;
using BCA.CarAuctionManagement.Domain.Models.Vehicles;
using BCA.CarAuctionManagement.Domain.Requests.Commands;
using BCA.CarAuctionManagement.Domain.Requests.Commands.Validators;
using BCA.CarAuctionManagement.Tests.Unit.Extensions;

public class CreateVehicleCommandValidatorTests : BaseTests
{
    private readonly CreateVehicleCommandValidator validator = new();

    [Theory]
    [InlineData(VehicleType.Hatchback)]
    [InlineData(VehicleType.Sedan)]
    [InlineData(VehicleType.SUV)]
    [InlineData(VehicleType.Truck)]
    public void Validate_ValidRequest_ShouldBeValid(VehicleType vehicleType)
    {
        // Arrange
        var modelBuilder = fixture.For<Vehicle>()
            .With(x => x.Type, vehicleType);

        modelBuilder = vehicleType switch
        {
            VehicleType.Hatchback or VehicleType.Sedan => modelBuilder
                .With(x => x.NumberOfSeats, null)
                .With(x => x.LoadCapacity, null),
            VehicleType.SUV => modelBuilder
                .With(x => x.NumberOfDoors, null)
                .With(x => x.LoadCapacity, null),
            VehicleType.Truck => modelBuilder
                .With(x => x.NumberOfSeats, null)
                .With(x => x.NumberOfDoors, null),
            _ => modelBuilder
        };

        var model = modelBuilder.Create();

        var request = new CreateVehicleCommand(model);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_InvalidRequest_ShouldBeInvalid()
    {
        // Arrange
        var request = new CreateVehicleCommand(null);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
