namespace BCA.CarAuctionManagement.Domain.Requests.Commands.Validators;

using BCA.CarAuctionManagement.Domain.Common.Validators;
using BCA.CarAuctionManagement.Domain.Models.Validators.Auctions;

using FluentValidation;

internal class CreateAuctionCommandValidator : BaseValidator<CreateAuctionCommand>
{
    public CreateAuctionCommandValidator()
    {
        RuleFor(x => x.Auction)
            .NotNull()
            .WithMessage(ValidationMessages.RequiredField)
            .SetValidator(new AuctionValidator());
    }
}
