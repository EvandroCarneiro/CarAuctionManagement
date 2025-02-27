namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Queries.Validators;

using BCA.CarAuctionManagement.Domain.Requests.Queries;
using BCA.CarAuctionManagement.Domain.Requests.Queries.Validators;

public class GetAuctionByIdQueryValidatorTests : BaseTests
{
    private readonly GetAuctionByIdQueryValidator validator = new();

    [Fact]
    public void Validate_ValidRequest_ShouldBeValid()
    {
        // Arrange
        var request = new GetAuctionByIdQuery(Guid.NewGuid());

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_InvalidRequest_ShouldBeInvalid()
    {
        // Arrange
        var request = new GetAuctionByIdQuery(Guid.Empty);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
