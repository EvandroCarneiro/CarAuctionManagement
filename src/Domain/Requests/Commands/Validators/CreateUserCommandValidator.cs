namespace BCA.CarAuctionManagement.Domain.Requests.Commands.Validators;

using BCA.CarAuctionManagement.Domain.Common.Validators;
using BCA.CarAuctionManagement.Domain.Models.Validators.Auctions;

using FluentValidation;

internal class CreateUserCommandValidator : BaseValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.User)
            .NotNull()
            .WithMessage(ValidationMessages.RequiredField)
            .SetValidator(new UserValidator());
    }
}
