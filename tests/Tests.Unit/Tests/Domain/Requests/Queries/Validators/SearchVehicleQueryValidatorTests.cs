namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Requests.Queries.Validators;

using System;

using BCA.CarAuctionManagement.Domain.Common.Constants;
using BCA.CarAuctionManagement.Domain.Models.Base;
using BCA.CarAuctionManagement.Domain.Models.Enums;
using BCA.CarAuctionManagement.Domain.Requests.Queries;
using BCA.CarAuctionManagement.Domain.Requests.Queries.Validators;

public class SearchVehicleQueryValidatorTests : BaseTests
{
    private readonly SearchVehicleQueryValidator validator = new();

    [Fact]
    public void Validate_ValidRequest_ShouldBeValid()
    {
        // Arrange
        var request = new SearchVehicleQuery(
            new PagedFilter(Pagination.PageNumber, Pagination.PageSize),
            fixture.CreateMany<VehicleType>(),
            fixture.CreateMany<string>(),
            fixture.CreateMany<string>(),
            fixture.CreateMany<int>());

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_InvalidRequest_ShouldBeInvalid()
    {
        // Arrange
        var request = new SearchVehicleQuery(
            new PagedFilter(0, 0),
            [VehicleType.Undefined],
            null,
            null,
            null);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
