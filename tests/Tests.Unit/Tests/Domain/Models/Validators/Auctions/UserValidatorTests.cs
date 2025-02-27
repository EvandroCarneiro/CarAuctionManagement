namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Models.Validators.Auctions;

using System;

using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Domain.Models.Validators.Auctions;
using BCA.CarAuctionManagement.Tests.Unit.Extensions;

public class UserValidatorTests : BaseTests
{
    private readonly UserValidator validator = new();

    [Fact]
    public void Validate_ValidNewModel_ShouldBeValid()
    {
        // Arrange
        var model = new User(fixture.Create<string>());

        // Act
        var result = validator.Validate(model);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_ValidExistentModel_ShouldBeValid()
    {
        // Arrange
        var model = fixture.Create<User>();

        // Act
        var result = validator.Validate(model);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_InvalidModel_ShouldBeInvalid()
    {
        // Arrange
        var model = fixture.For<User>()
            .With(x => x.Id, Guid.Empty)
            .With(x => x.CreatedDate, default)
            .With(x => x.Name, null)
            .Create();

        // Act
        var result = validator.Validate(model);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
