namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Models.Validators.Vehicles;

using BCA.CarAuctionManagement.Domain.Models.Enums;
using BCA.CarAuctionManagement.Domain.Models.Validators.Vehicles;
using BCA.CarAuctionManagement.Domain.Models.Vehicles;
using BCA.CarAuctionManagement.Tests.Unit.Extensions;

public class VehicleValidatorTests : BaseTests
{
    private readonly VehicleValidator validator = new();

    [Theory]
    [InlineData(VehicleType.Hatchback, true)]
    [InlineData(VehicleType.Sedan, true)]
    [InlineData(VehicleType.SUV, true)]
    [InlineData(VehicleType.Truck, true)]
    [InlineData(VehicleType.Undefined, false)]
    public void Validate_NewModelByType_IsValidShouldBeGivenValue(VehicleType vehicleType, bool valid)
    {
        // Arrange
        var model = new Vehicle(
            vehicleType,
            fixture.Create<string>(),
            fixture.Create<string>(),
            fixture.Create<string>(),
            fixture.Create<int>(),
            fixture.Create<decimal>(),
            vehicleType is VehicleType.Hatchback or VehicleType.Sedan ? fixture.Create<int>() : null,
            vehicleType is VehicleType.SUV ? fixture.Create<int>() : null,
            vehicleType is VehicleType.Truck ? fixture.Create<decimal>() : null);

        // Act
        var result = validator.Validate(model);

        // Assert
        result.IsValid.Should().Be(valid);
    }

    [Theory]
    [InlineData(VehicleType.Hatchback)]
    [InlineData(VehicleType.Sedan)]
    [InlineData(VehicleType.SUV)]
    [InlineData(VehicleType.Truck)]
    public void Validate_ValidExistentModel_ShouldBeValid(VehicleType vehicleType)
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

        // Act
        var result = validator.Validate(model);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_InvalidModel_ShouldBeInvalid()
    {
        // Arrange
        var model = fixture.For<Vehicle>()
            .With(x => x.Id, Guid.Empty)
            .With(x => x.CreatedDate, default)
            .With(x => x.Type, VehicleType.Undefined)
            .With(x => x.Manufacturer, null)
            .With(x => x.Model, null)
            .With(x => x.Year, default)
            .With(x => x.StartingBid, default)
            .With(x => x.NumberOfDoors, null)
            .With(x => x.NumberOfSeats, null)
            .With(x => x.LoadCapacity, null)
            .Create();

        // Act
        var result = validator.Validate(model);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}