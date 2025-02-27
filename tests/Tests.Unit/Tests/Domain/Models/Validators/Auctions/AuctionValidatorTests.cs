namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Models.Validators.Auctions;

using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Domain.Models.Validators.Auctions;
using BCA.CarAuctionManagement.Tests.Unit.Extensions;
using BCA.CarAuctionManagement.Tests.Unit.Tests;

public class AuctionValidatorTests : BaseTests
{
    private readonly AuctionValidator validator = new();

    [Fact]
    public void Validate_ValidNewModel_ShouldBeValid()
    {
        // Arrange
        var model = new Auction(Guid.NewGuid());

        // Act
        var result = validator.Validate(model);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_ValidExistentModel_ShouldBeValid()
    {
        // Arrange
        var model = fixture.Create<Auction>();

        // Act
        var result = validator.Validate(model);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_InvalidModel_ShouldBeInvalid()
    {
        // Arrange
        var model = fixture.For<Auction>()
            .With(x => x.Id, Guid.Empty)
            .With(x => x.CreatedDate, default)
            .With(x => x.VehicleId, Guid.Empty)
            .With(x => x.Vehicle, null)
            .Create();

        // Act
        var result = validator.Validate(model);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
