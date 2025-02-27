namespace BCA.CarAuctionManagement.Domain.Models.Validators;

using BCA.CarAuctionManagement.Domain.Common.Validators;
using BCA.CarAuctionManagement.Domain.Models.Base;

using FluentValidation;

using Constants = Common.Constants;

internal class PagedFilterValidator : BaseValidator<PagedFilter>
{
    public PagedFilterValidator()
    {
        RuleFor(filter => filter.PageNumber)
            .GreaterThanOrEqualTo(Constants.Pagination.PageNumber)
            .WithMessage(ValidationMessages.InvalidField);

        RuleFor(filter => filter.PageSize)
             .InclusiveBetween(Constants.Pagination.MinPageSize, Constants.Pagination.MaxPageSize)
             .WithMessage(ValidationMessages.InvalidField);
    }
}
