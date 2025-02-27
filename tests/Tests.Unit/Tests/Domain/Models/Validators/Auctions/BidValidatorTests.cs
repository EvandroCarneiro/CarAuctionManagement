namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Models.Validators.Auctions;

using System;

using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Domain.Models.Validators.Auctions;
using BCA.CarAuctionManagement.Tests.Unit.Extensions;

public class BidValidatorTests : BaseTests
{
    private readonly BidValidator validator = new();

    [Fact]
    public void Validate_ValidNewModel_ShouldBeValid()
    {
        // Arrange
        var model = new Bid(fixture.Create<decimal>(), Guid.NewGuid());

        // Act
        var result = validator.Validate(model);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_ValidExistentModel_ShouldBeValid()
    {
        // Arrange
        var model = fixture.Create<Bid>();

        // Act
        var result = validator.Validate(model);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_InvalidModel_ShouldBeInvalid()
    {
        // Arrange
        var model = fixture.For<Bid>()
            .With(x => x.CreatedDate, default)
            .With(x => x.Amount, default)
            .With(x => x.UserId, Guid.Empty)
            .With(x => x.User, null)
            .Create();

        // Act
        var result = validator.Validate(model);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
