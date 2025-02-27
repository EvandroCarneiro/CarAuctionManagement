namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Queries.Validators;

using System;

using BCA.CarAuctionManagement.Domain.Requests.Queries;
using BCA.CarAuctionManagement.Domain.Requests.Queries.Validators;

public class GetUserByIdQueryValidatorTests : BaseTests
{
    private readonly GetUserByIdQueryValidator validator = new();

    [Fact]
    public void Validate_ValidRequest_ShouldBeValid()
    {
        // Arrange
        var request = new GetUserByIdQuery(Guid.NewGuid());

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_InvalidRequest_ShouldBeInvalid()
    {
        // Arrange
        var request = new GetUserByIdQuery(Guid.Empty);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
