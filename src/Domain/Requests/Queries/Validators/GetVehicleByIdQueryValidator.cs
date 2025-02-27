namespace BCA.CarAuctionManagement.Domain.Requests.Queries.Validators;

using BCA.CarAuctionManagement.Domain.Common.Validators;

using FluentValidation;

internal class GetVehicleByIdQueryValidator : BaseValidator<GetVehicleByIdQuery>
{
    public GetVehicleByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(ValidationMessages.InvalidField);
    }
}
