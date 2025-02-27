namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Commands.Validators;

using System;

using BCA.CarAuctionManagement.Domain.Requests.Commands;
using BCA.CarAuctionManagement.Domain.Requests.Commands.Validators;

public class CloseAuctionCommandValidatorTests : BaseTests
{
    private readonly CloseAuctionCommandValidator validator = new();

    [Fact]
    public void Validate_ValidRequest_ShouldBeValid()
    {
        // Arrange
        var request = new CloseAuctionCommand(Guid.NewGuid());

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_InvalidRequest_ShouldBeInvalid()
    {
        // Arrange
        var request = new CloseAuctionCommand(Guid.Empty);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
