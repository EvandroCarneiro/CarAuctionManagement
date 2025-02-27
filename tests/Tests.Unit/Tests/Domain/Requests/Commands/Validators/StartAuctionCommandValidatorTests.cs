namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Commands.Validators;

using System;

using BCA.CarAuctionManagement.Domain.Requests.Commands;
using BCA.CarAuctionManagement.Domain.Requests.Commands.Validators;

public class StartAuctionCommandValidatorTests : BaseTests
{
    private readonly StartAuctionCommandValidator validator = new();

    [Fact]
    public void Validate_ValidRequest_ShouldBeValid()
    {
        // Arrange
        var request = new StartAuctionCommand(Guid.NewGuid());

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_InvalidRequest_ShouldBeInvalid()
    {
        // Arrange
        var request = new StartAuctionCommand(Guid.Empty);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
