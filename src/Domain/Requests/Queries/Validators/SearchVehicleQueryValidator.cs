namespace BCA.CarAuctionManagement.Domain.Requests.Queries.Validators;

using BCA.CarAuctionManagement.Domain.Common.Validators;
using BCA.CarAuctionManagement.Domain.Models.Validators;

using FluentValidation;

internal class SearchVehicleQueryValidator : BaseValidator<SearchVehicleQuery>
{
    public SearchVehicleQueryValidator()
    {
        RuleFor(x => x.PagedFilter)
            .SetValidator(new PagedFilterValidator());

        RuleForEach(x => x.VehicleTypes)
            .IsInEnum()
            .WithMessage(ValidationMessages.InvalidField);
    }
}
