namespace BCA.CarAuctionManagement.Domain.Requests.Queries.Validators;

using BCA.CarAuctionManagement.Domain.Common.Validators;

using FluentValidation;

internal class GetAuctionByIdQueryValidator : BaseValidator<GetAuctionByIdQuery>
{
    public GetAuctionByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(ValidationMessages.InvalidField);
    }
}
