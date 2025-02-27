namespace BCA.CarAuctionManagement.Domain.Requests.Commands.Validators;

using BCA.CarAuctionManagement.Domain.Common.Validators;
using BCA.CarAuctionManagement.Domain.Models.Validators.Auctions;

using FluentValidation;

internal class PlaceBidCommandValidator : BaseValidator<PlaceBidCommand>
{
    public PlaceBidCommandValidator()
    {
        RuleFor(x => x.AuctionId)
            .NotEmpty()
            .WithMessage(ValidationMessages.InvalidField);

        RuleFor(x => x.Bid)
            .NotNull()
            .WithMessage(ValidationMessages.RequiredField)
            .SetValidator(new BidValidator());
    }
}
