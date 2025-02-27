namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Commands.Validators;

using BCA.CarAuctionManagement.Domain.Models.Auctions;
using BCA.CarAuctionManagement.Domain.Requests.Commands;
using BCA.CarAuctionManagement.Domain.Requests.Commands.Validators;

public class PlaceBidCommandValidatorTests : BaseTests
{
    private readonly PlaceBidCommandValidator validator = new();

    [Fact]
    public void Validate_ValidRequest_ShouldBeValid()
    {
        // Arrange
        var model = fixture.Create<Bid>();

        var request = new PlaceBidCommand(Guid.NewGuid(), model);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_InvalidRequest_ShouldBeInvalid()
    {
        // Arrange
        var request = new PlaceBidCommand(Guid.Empty, null);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
