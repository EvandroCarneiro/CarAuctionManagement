namespace BCA.CarAuctionManagement.Domain.Requests.Commands.Validators;

using BCA.CarAuctionManagement.Domain.Common.Validators;

using FluentValidation;

internal class CloseAuctionCommandValidator : BaseValidator<CloseAuctionCommand>
{
    public CloseAuctionCommandValidator()
    {
        RuleFor(x => x.AuctionId)
            .NotEmpty()
            .WithMessage(ValidationMessages.InvalidField);
    }
}
