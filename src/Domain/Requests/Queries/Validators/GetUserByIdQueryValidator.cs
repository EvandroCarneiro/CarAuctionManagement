namespace BCA.CarAuctionManagement.Domain.Requests.Queries.Validators;

using BCA.CarAuctionManagement.Domain.Common.Validators;

using FluentValidation;

internal class GetUserByIdQueryValidator : BaseValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(ValidationMessages.InvalidField);
    }
}
