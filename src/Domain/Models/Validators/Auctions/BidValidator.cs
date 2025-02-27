namespace BCA.CarAuctionManagement.Domain.Models.Validators.Auctions;

using BCA.CarAuctionManagement.Domain.Common.Validators;
using BCA.CarAuctionManagement.Domain.Models.Auctions;

using FluentValidation;

internal class BidValidator : BaseValidator<Bid>
{
    public BidValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(decimal.Zero)
            .WithMessage(ValidationMessages.InvalidField);

        RuleFor(x => x.User)
            .SetValidator(new UserValidator())
            .When(x => x.User is not null);
    }
}
