namespace BCA.CarAuctionManagement.Tests.Unit.Tests.Domain.Models.Validators;

using BCA.CarAuctionManagement.Domain.Common.Constants;
using BCA.CarAuctionManagement.Domain.Models.Base;
using BCA.CarAuctionManagement.Domain.Models.Validators;
using BCA.CarAuctionManagement.Tests.Unit.Extensions;

public class PagedFilterValidatorTests : BaseTests
{
    private readonly PagedFilterValidator validator = new();

    [Fact]
    public void Validate_ValidModel_ShouldBeValid()
    {
        // Arrange
        var model = new PagedFilter(Pagination.PageNumber, Pagination.PageSize);

        // Act
        var result = validator.Validate(model);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_InvalidModel_ShouldBeInvalid()
    {
        // Arrange
        var model = fixture.For<PagedFilter>()
            .With(x => x.PageNumber, default)
            .With(x => x.PageSize, default)
            .Create();

        // Act
        var result = validator.Validate(model);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
